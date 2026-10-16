// TopDownMover.cs — JamKit
// 用途: 2D 俯视 8 向移动。WASD / 方向键 / 手柄输入 → 归一化向量 → Rigidbody2D 速度。
//       可选：朝向鼠标；可选：模拟土狼时间（离开输入源后短时间内保持原方向）。
//
// 兼容: 团结引擎 2022.3 / Unity 2022.3，纯 C#。
//   - 默认：旧版 Input Manager（Input.GetAxisRaw）
//   - 启用新 Input System：在 Project Settings → Player → "Active Input
//     Handling" 设为 "Both"（或 "Input System Package"），脚本会自动切换到
//     Keyboard.current / Gamepad.current 分支（编译期 #if ENABLE_INPUT_SYSTEM）
//
// 用法:
//   挂到带 Rigidbody2D 的角色 GameObject 上（勾选 FreezeRotation Z），
//   把 speed 调到 5~7 即可。

using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JamKit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownMover : MonoBehaviour
    {
        [Header("Speed")]
        [SerializeField, Min(0f)] private float speed = 5.5f;

        [Header("Input smoothing")]
        // Coyote time for the *last* non-zero input direction. When the player
        // briefly releases the stick, we keep the last direction (and decay it)
        // for this many seconds before stopping. 0 disables.
        [SerializeField, Range(0f, 0.5f)] private float coyoteSeconds = 0.08f;

        [Header("Aim (optional)")]
        [SerializeField] private bool faceMouse = true;

        private Rigidbody2D _rb;
        private Vector2 _moveInput;
        // Direction we're still applying during coyote time, even if no input.
        private Vector2 _coyoteDirection;
        // Seconds remaining on coyote hold.
        private float _coyoteTimer;
        // When true, Update() ignores raw input and SetMoveInput() is authoritative.
        // Use for cutscenes / AI takeover / replays. Default off.
        public bool InputOverridden { get; set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0f; // top-down: no falling
            _rb.freezeRotation = true; // we set rotation manually if faceMouse
        }

        private void Update()
        {
            if (!InputOverridden)
            {
                _moveInput = ReadMoveInput();

                // Clamp magnitude 1 (8-direction, no diagonal speedup)
                if (_moveInput.sqrMagnitude > 1f)
                    _moveInput.Normalize();

                // Refresh coyote latch whenever we have fresh input; otherwise
                // tick it down. The latch lets a brief input gap (e.g. flick-stick)
                // keep the character moving without a hard stop.
                if (_moveInput.sqrMagnitude > 0.0001f)
                {
                    _coyoteDirection = _moveInput;
                    _coyoteTimer = coyoteSeconds;
                }
                else
                {
                    _coyoteTimer -= Time.deltaTime;
                    if (_coyoteTimer > 0f)
                        _moveInput = _coyoteDirection;
                }
            }

            if (faceMouse)
                AimAtMouse();
        }

        private void FixedUpdate()
        {
            Vector2 vel = _moveInput * speed;
            _rb.velocity = vel;
        }

        /// <summary>
        /// Read raw move input. Branches at compile time on ENABLE_INPUT_SYSTEM
        /// (defined when the Input System package is installed and either
        /// "Input System Package" or "Both" is selected under
        /// Project Settings → Player → Active Input Handling).
        /// </summary>
        private Vector2 ReadMoveInput()
        {
#if ENABLE_INPUT_SYSTEM
            // ----- new Input System path -----
            // Try keyboard first (most jam players use WASD), then layer gamepad
            // left stick on top. Keyboard takes precedence if a key is held.
            Vector2 input = Vector2.zero;

            if (Keyboard.current != null)
            {
                float kx = 0f, ky = 0f;
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  kx -= 1f;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) kx += 1f;
                if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)  ky -= 1f;
                if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)    ky += 1f;
                input = new Vector2(kx, ky);
            }
            if (Gamepad.current != null)
            {
                Vector2 stick = Gamepad.current.leftStick.ReadValue();
                // Only use stick if it's pushed past a dead zone (avoids
                // noisy near-zero values returning 0.001, 0.002 due to
                // stick wear on used consoles).
                if (stick.sqrMagnitude > 0.04f) input = stick;
            }
            return input;
#else
            // ----- legacy Input Manager path (default) -----
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            return new Vector2(h, v);
#endif
        }

        private void AimAtMouse()
        {
            if (Camera.main == null) return;

            // Mouse position lookup: works with both Input Systems.
            // - Legacy Input: Input.mousePosition (Vector3, screen-space px).
            // - New Input System: Mouse.current.position.ReadValue() (Vector2Control, screen-space px).
            Vector3 mouseScreen = ReadMouseScreenPosition();

            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(mouseScreen);
            Vector2 dir = (mouseWorld - transform.position);
            if (dir.sqrMagnitude < 0.0001f) return;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f; // -90 because sprite "up"
            // Rotation is a *visual* concern, not physics. Drive at Time.deltaTime
            // (frame-rate dependent smoothing) rather than Time.fixedDeltaTime,
            // which made rotation feel choppy at non-50Hz physics rates.
            _rb.rotation = Mathf.LerpAngle(_rb.rotation, angle, 18f * Time.deltaTime);
        }

        private Vector3 ReadMouseScreenPosition()
        {
#if ENABLE_INPUT_SYSTEM
            if (Mouse.current != null)
            {
                Vector2 mp = Mouse.current.position.ReadValue();
                return new Vector3(mp.x, mp.y, 0f);
            }
            // No mouse — return last-known world center to avoid div-by-zero.
            if (Camera.main != null)
            {
                Vector3 c = Camera.main.transform.position;
                return Camera.main.WorldToScreenPoint(c);
            }
            return Vector3.zero;
#else
            return Input.mousePosition;
#endif
        }

        // Public helper for cutscenes / input override. Sets InputOverridden=true
        // so Update() doesn't clobber this on the next frame. Caller is responsible
        // for clearing InputOverridden when it wants raw input back.
        public void SetMoveInput(Vector2 input)
        {
            InputOverridden = true;
            _moveInput = Vector2.ClampMagnitude(input, 1f);
            if (_moveInput.sqrMagnitude > 0.0001f)
            {
                _coyoteDirection = _moveInput;
                _coyoteTimer = coyoteSeconds;
            }
        }

        /// <summary>Hand control back to the raw input axes (WASD / stick).</summary>
        public void ClearInputOverride()
        {
            InputOverridden = false;
            _coyoteTimer = 0f;
        }

        public Vector2 Velocity => _rb != null ? _rb.velocity : Vector2.zero;
    }
}