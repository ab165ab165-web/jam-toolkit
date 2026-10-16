// JuiceManager.cs — JamKit
// 用途: 「手感动画」（juice / game feel）效果聚合。
//       - HitStop(seconds): 命停帧；Time.timeScale 临时归零后回弹。
//       - Shake(amplitude, duration): 主相机震屏（基于 Perlin 噪声）。
//
// 兼容: 团结引擎 2022.3 / Unity 2022.3，纯 C#，无第三方依赖。
// 注意: HitStop 会影响一切依赖 Time.deltaTime 的逻辑（移动、物理）；
//       默认 duration 0.05s 一般足够让玩家「feel」打击，又不打断操作。
//
// 用法:
//   JuiceManager.Instance.HitStop(0.06f);
//   JuiceManager.Instance.Shake(0.4f, 0.15f);

using System.Collections;
using UnityEngine;

namespace JamKit
{
    [DisallowMultipleComponent]
    public class JuiceManager : MonoBehaviour
    {
        public static JuiceManager Instance { get; private set; }

        [Header("Camera shake defaults")]
        [SerializeField] private Camera shakeCamera;
        [SerializeField, Min(0f)] private float defaultShakeAmplitude = 0.3f;
        [SerializeField, Min(0f)] private float defaultShakeDuration = 0.12f;

        [Header("HitStop")]
        [SerializeField, Min(0f)] private float defaultHitStop = 0.05f;

        // runtime
        private Vector3 _camOrigLocalPos;
        private Coroutine _shakeCo;
        private Coroutine _hitStopCo;
        // Captured once on the first HitStop so re-entrant calls don't freeze timeScale at 0.
        private float _baselineTimeScale = 1f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (shakeCamera == null) shakeCamera = Camera.main;
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        // -----------------------------------------------------------------
        // HitStop
        // -----------------------------------------------------------------

        /// <summary>命停帧：临时把 Time.timeScale 归零，时长到达后恢复原值。</summary>
        public void HitStop(float seconds = -1f)
        {
            float dur = seconds < 0f ? defaultHitStop : seconds;
            // Re-entrancy: if a hitstop is already running, just extend its duration
            // (restart the coroutine with the longer of the two). Critically, do NOT
            // re-capture the baseline here — timeScale is already 0, and capturing it
            // would lock the game at timeScale=0 when the coroutine ends.
            if (_hitStopCo == null)
            {
                _baselineTimeScale = Time.timeScale; // capture once
            }
            else
            {
                StopCoroutine(_hitStopCo);
            }
            _hitStopCo = StartCoroutine(HitStopCo(dur));
        }

        private IEnumerator HitStopCo(float seconds)
        {
            Time.timeScale = 0f;
            // 用 unscaled 计时，不受 timeScale 影响
            yield return new WaitForSecondsRealtime(seconds);
            Time.timeScale = _baselineTimeScale;
            _hitStopCo = null;
        }

        // -----------------------------------------------------------------
        // Shake
        // -----------------------------------------------------------------

        /// <summary>主相机震屏（基于 Perlin 噪声）。</summary>
        public void Shake(float amplitude = -1f, float duration = -1f)
        {
            if (shakeCamera == null)
            {
                shakeCamera = Camera.main;
                if (shakeCamera == null) return;
            }
            float amp = amplitude > -1f ? amplitude : defaultShakeAmplitude;
            float dur = duration > -1f ? duration : defaultShakeDuration;
            if (_shakeCo != null) StopCoroutine(_shakeCo);
            _shakeCo = StartCoroutine(ShakeCo(amp, dur));
        }

        private IEnumerator ShakeCo(float amplitude, float duration)
        {
            _camOrigLocalPos = shakeCamera.transform.localPosition;
            float t = 0f;
            while (t < duration)
            {
                t += Time.unscaledDeltaTime;
                float k = 1f - Mathf.Clamp01(t / duration); // fade out
                float ox = (Mathf.PerlinNoise(Time.unscaledTime * 35f, 0f) - 0.5f) * 2f * amplitude * k;
                float oy = (Mathf.PerlinNoise(0f, Time.unscaledTime * 35f) - 0.5f) * 2f * amplitude * k;
                shakeCamera.transform.localPosition = _camOrigLocalPos + new Vector3(ox, oy, 0f);
                yield return null;
            }
            shakeCamera.transform.localPosition = _camOrigLocalPos;
            _shakeCo = null;
        }
    }
}