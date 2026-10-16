// PlatformerMover.cs — JamKit
// 用途: 2D 横版平台跳跃移动。含:
//       - 跳跃缓冲 (jump buffer)：按下跳跃后短时间内未起跳也照样起跳
//       - 土狼时间 (coyote time)：离开地面后短时间内仍可起跳
//       - 可变跳跃高度：松开跳跃键立即截断上升速度（手感更脆）
//       - 二段跳支持（可选开关）
//
// 兼容: 团结引擎 2022.3 / Unity 2022.3，纯 C#。
//   - 默认：旧版 Input Manager（Input.GetAxisRaw / Input.GetKeyDown）
//   - 启用新 Input System：在 Project Settings → Player → "Active Input
//     Handling" 设为 "Both"（或 "Input System Package"），脚本会自动切换到
//     Keyboard.current / Gamepad.current 分支（编译期 #if ENABLE_INPUT_SYSTEM）
//
// 用法:
//   角色需挂 Rigidbody2D（gravityScale 建议保留默认 1~3）、CapsuleCollider2D / BoxCollider2D，
//   加一个 GroundCheck 子物体（空 Transform）放到脚底。

using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace JamKit
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformerMover : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField, Min(0f)] private float moveSpeed = 7f;
        [SerializeField, Min(0f)] private float acceleration = 60f;
        [SerializeField, Min(0f)] private float deceleration = 70f;

        [Header("Jump")]
        [SerializeField, Min(0f)] private float jumpVelocity = 14f;
        [SerializeField] private bool variableJumpHeight = true;
        [SerializeField] private bool allowDoubleJump = false;
        [SerializeField, Range(0.05f, 0.4f)] private float coyoteTime = 0.10f;
        [SerializeField, Range(0.05f, 0.4f)] private float jumpBufferTime = 0.12f;

        [Header("Ground check")]
        [SerializeField] private Transform groundCheck;
        [SerializeField, Min(0f)] private float groundCheckRadius = 0.15f;
        // Default 0 ("Nothing") to force the integrator to set the right ground
        // layers in the inspector. ~0 (Everything) silently includes the player's
        // own layer, which makes GroundCheck return true on the player's own
        // collider and breaks the jump — infinite jumps from a standing start.
        [SerializeField] private LayerMask groundLayer = 0;

        private Rigidbody2D _rb;
        private bool _facingRight = true;
        private bool _isGrounded;
        private float _coyoteTimer;
        private float _jumpBufferTimer;
        private bool _jumpHeld;
        private bool _doubleJumpUsed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.freezeRotation = true;
            if (groundCheck == null)
            {
                // Best-effort fallback: place check under transform
                var go = new GameObject("GroundCheck");
                go.transform.SetParent(transform);
                go.transform.localPosition = new Vector3(0f, -0.5f, 0f);
                groundCheck = go.transform;
            }
        }

        private void Update()
        {
            // ----- Input -----
            float h = ReadHorizontalInput();
            bool jumpDown = ReadJumpDown();
            bool jumpUp   = ReadJumpUp();
            _jumpHeld = ReadJumpHeld();

            // Ground check
            bool wasGrounded = _isGrounded;
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer) != null;

            if (_isGrounded)
            {
                _coyoteTimer = coyoteTime;
                _doubleJumpUsed = false;
            }
            else if (wasGrounded && !_isGrounded)
            {
                _coyoteTimer = coyoteTime;
            }
            else
            {
                _coyoteTimer -= Time.deltaTime;
            }

            // Jump buffer
            if (jumpDown) _jumpBufferTimer = jumpBufferTime;
            else _jumpBufferTimer -= Time.deltaTime;

            if (_jumpBufferTimer > 0f)
            {
                if (_coyoteTimer > 0f)
                {
                    Jump();
                    _coyoteTimer = 0f;
                    _jumpBufferTimer = 0f;
                }
                else if (allowDoubleJump && !_doubleJumpUsed)
                {
                    Jump();
                    _doubleJumpUsed = true;
                    _jumpBufferTimer = 0f;
                }
            }

            // Variable jump height — cut upward velocity if released early
            if (variableJumpHeight && jumpUp && _rb.velocity.y > 0f)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.45f);
            }

            // Horizontal movement
            float targetVx = h * moveSpeed;
            float a = Mathf.Abs(targetVx) > 0.01f ? acceleration : deceleration;
            float newVx = Mathf.MoveTowards(_rb.velocity.x, targetVx, a * Time.deltaTime);
            _rb.velocity = new Vector2(newVx, _rb.velocity.y);

            // Flip sprite
            if (h > 0.01f && !_facingRight) Flip();
            else if (h < -0.01f && _facingRight) Flip();
        }

        // ============================================================
        //  Input System branches
        // ============================================================

        /// <summary>Reads horizontal axis (-1..+1). Prefers keyboard over stick.</summary>
        private float ReadHorizontalInput()
        {
#if ENABLE_INPUT_SYSTEM
            float h = 0f;
            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)  h -= 1f;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) h += 1f;
            }
            if (Gamepad.current != null)
            {
                float stickX = Gamepad.current.leftStick.x.ReadValue();
                if (Mathf.Abs(stickX) > 0.2f) h = stickX;
            }
            return Mathf.Clamp(h, -1f, 1f);
#else
            return Input.GetAxisRaw("Horizontal");
#endif
        }

        /// <summary>Jump button just-pressed (rising edge).</summary>
        private bool ReadJumpDown()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null)
            {
                if (Keyboard.current.spaceKey.wasPressedThisFrame) return true;
                if (Keyboard.current.wKey.wasPressedThisFrame) return true;
                if (Keyboard.current.upArrowKey.wasPressedThisFrame) return true;
            }
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonSouth.wasPressedThisFrame) return true; // A / Cross
            }
            return false;
#else
            return Input.GetKeyDown(KeyCode.Space) ||
                   Input.GetKeyDown(KeyCode.W) ||
                   Input.GetKeyDown(KeyCode.UpArrow);
#endif
        }

        /// <summary>Jump button just-released (falling edge).</summary>
        private bool ReadJumpUp()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null)
            {
                if (Keyboard.current.spaceKey.wasReleasedThisFrame) return true;
                if (Keyboard.current.wKey.wasReleasedThisFrame) return true;
                if (Keyboard.current.upArrowKey.wasReleasedThisFrame) return true;
            }
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonSouth.wasReleasedThisFrame) return true;
            }
            return false;
#else
            return Input.GetKeyUp(KeyCode.Space) ||
                   Input.GetKeyUp(KeyCode.W) ||
                   Input.GetKeyUp(KeyCode.UpArrow);
#endif
        }

        /// <summary>Jump button currently held.</summary>
        private bool ReadJumpHeld()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current != null)
            {
                if (Keyboard.current.spaceKey.isPressed) return true;
                if (Keyboard.current.wKey.isPressed) return true;
                if (Keyboard.current.upArrowKey.isPressed) return true;
            }
            if (Gamepad.current != null)
            {
                if (Gamepad.current.buttonSouth.isPressed) return true;
            }
            return false;
#else
            return Input.GetKey(KeyCode.Space) ||
                   Input.GetKey(KeyCode.W) ||
                   Input.GetKey(KeyCode.UpArrow);
#endif
        }

        // ============================================================

        private void Jump()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpVelocity);
        }

        private void Flip()
        {
            _facingRight = !_facingRight;
            var s = transform.localScale;
            s.x = -s.x;
            transform.localScale = s;
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheck == null) return;
            Gizmos.color = _isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}