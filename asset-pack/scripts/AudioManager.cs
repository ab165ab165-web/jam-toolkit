// AudioManager.cs — JamKit
// 用途: 全局单例音频播放。PlaySFX 走短音池（一次性 AudioSource），
//       PlayMusic 走专用 AudioSource（淡入淡出切换）。
// 兼容: 团结引擎 2022.3 / Unity 2022.3 LTS，纯 C#，无第三方依赖。
//
// 用法:
//   AudioManager.Instance.PlaySFX(myClip);
//   AudioManager.Instance.PlaySFX(myClip, pitchRandomness: 0.2f);
//   AudioManager.Instance.PlayMusic(bgmClip, fadeDuration: 0.5f);

using System.Collections.Generic;
using UnityEngine;

namespace JamKit
{
    [DisallowMultipleComponent]
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Pool")]
        [SerializeField, Min(4)] private int initialSfxPoolSize = 16;

        private AudioSource _musicSourceA;
        private AudioSource _musicSourceB;
        private bool _musicAActive = true;
        private readonly List<AudioSource> _sfxPool = new List<AudioSource>();
        private int _nextSfxIndex;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Two music sources for crossfade
            _musicSourceA = gameObject.AddComponent<AudioSource>();
            _musicSourceB = gameObject.AddComponent<AudioSource>();
            ConfigureMusicSource(_musicSourceA);
            ConfigureMusicSource(_musicSourceB);

            // SFX pool
            for (int i = 0; i < initialSfxPoolSize; i++)
                _sfxPool.Add(CreateSfxSource());
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private static void ConfigureMusicSource(AudioSource src)
        {
            src.playOnAwake = false;
            src.loop = true;
            src.spatialBlend = 0f; // 2D music
            src.volume = 1f;
        }

        private AudioSource CreateSfxSource()
        {
            var src = gameObject.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.loop = false;
            src.spatialBlend = 0f; // 2D SFX by default
            return src;
        }

        // -----------------------------------------------------------------
        // SFX
        // -----------------------------------------------------------------

        /// <summary>
        /// 一次性播放短音效，自动从池里取下一个空闲 source。
        /// </summary>
        public void PlaySFX(AudioClip clip, float pitchRandomness = 0.1f, float volume = 1f)
        {
            if (clip == null) return;
            var src = GetNextSfxSource();
            src.clip = clip;
            src.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
            src.volume = Mathf.Clamp01(volume);
            // Pool reuse: a prior PlaySFXAt() may have left this source at 3D.
            src.spatialBlend = 0f; // 2D
            src.Play();
        }

        /// <summary>带 3D 位置的 SFX（俯视/横版场景需要声音从远处传来）</summary>
        public void PlaySFXAt(AudioClip clip, Vector3 worldPos, float pitchRandomness = 0.1f, float volume = 1f)
        {
            if (clip == null) return;
            var src = GetNextSfxSource();
            src.clip = clip;
            src.pitch = 1f + Random.Range(-pitchRandomness, pitchRandomness);
            src.volume = Mathf.Clamp01(volume);
            src.spatialBlend = 1f;
            src.transform.position = worldPos;
            src.Play();
        }

        private AudioSource GetNextSfxSource()
        {
            // Find a non-playing source, else round-robin
            for (int i = 0; i < _sfxPool.Count; i++)
            {
                int idx = (_nextSfxIndex + i) % _sfxPool.Count;
                if (!_sfxPool[idx].isPlaying)
                {
                    _nextSfxIndex = (idx + 1) % _sfxPool.Count;
                    return _sfxPool[idx];
                }
            }
            // All busy — add a new source on demand
            var newSrc = CreateSfxSource();
            _sfxPool.Add(newSrc);
            _nextSfxIndex = 0;
            return newSrc;
        }

        // -----------------------------------------------------------------
        // Music
        // -----------------------------------------------------------------

        /// <summary>淡入新音乐；如已在播放则交叉淡出旧音乐。</summary>
        public void PlayMusic(AudioClip clip, float fadeDuration = 0.5f, float volume = 1f)
        {
            if (clip == null) return;
            StopAllCoroutines();
            var from = _musicAActive ? _musicSourceA : _musicSourceB;
            var to = _musicAActive ? _musicSourceB : _musicSourceA;
            _musicAActive = !_musicAActive;

            to.clip = clip;
            to.volume = 0f;
            to.loop = true;
            to.Play();
            StartCoroutine(Crossfade(from, to, fadeDuration, volume));
        }

        public void StopMusic(float fadeDuration = 0.3f)
        {
            StopAllCoroutines();
            // After PlayMusic(), _musicAActive has been flipped; the currently-active
            // source is therefore the one PlayMusic wrote to (the OLD _musicAActive value).
            // See PlayMusic's `to = _musicAActive ? B : A` + assignment — symmetric here.
            var live = _musicAActive ? _musicSourceA : _musicSourceB;
            StartCoroutine(FadeOut(live, fadeDuration));
        }

        private System.Collections.IEnumerator Crossfade(AudioSource from, AudioSource to, float dur, float targetVol)
        {
            float t = 0f;
            float fromStartVol = from.volume;
            while (t < dur)
            {
                t += Time.unscaledDeltaTime;
                float k = Mathf.Clamp01(t / dur);
                from.volume = Mathf.Lerp(fromStartVol, 0f, k);
                to.volume = Mathf.Lerp(0f, targetVol, k);
                yield return null;
            }
            from.Stop();
            to.volume = targetVol;
        }

        private System.Collections.IEnumerator FadeOut(AudioSource src, float dur)
        {
            float t = 0f;
            float startVol = src.volume;
            while (t < dur)
            {
                t += Time.unscaledDeltaTime;
                src.volume = Mathf.Lerp(startVol, 0f, t / dur);
                yield return null;
            }
            src.Stop();
        }
    }
}