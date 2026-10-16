// GameFlowManager.cs — JamKit
// 用途: 游戏状态机（StartMenu → InGame → GameOver → Victory）+ 场景重载。
//       R 键硬重启当前场景（调试用）；提供 OnStateChanged 事件供 UI 监听。
//
// 兼容: 团结引擎 2022.3 / Unity 2022.3，纯 C#，使用 UnityEngine.SceneManagement。
// 注意: 本类不依赖 Input System（旧版 Input 即可）；如要升级到 Input System，
//       把 Update() 里的 Input.GetKeyDown 换为 Keyboard.current.rKey.wasPressedThisFrame 即可。

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamKit
{
    public enum GameState
    {
        StartMenu,
        InGame,
        GameOver,
        Victory,
        Paused
    }

    [DisallowMultipleComponent]
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Instance { get; private set; }

        public GameState State { get; private set; } = GameState.StartMenu;

        /// <summary>UI 监听这个事件刷新界面。</summary>
        public event Action<GameState> OnStateChanged;

        [Header("Behavior")]
        [SerializeField] private bool allowRestartKey = true;
        [SerializeField] private KeyCode restartKey = KeyCode.R;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }

        private void Update()
        {
            if (allowRestartKey && Input.GetKeyDown(restartKey))
            {
                ReloadCurrentScene();
            }
        }

        // -----------------------------------------------------------------
        // State transitions
        // -----------------------------------------------------------------

        public void StartGame()
        {
            ChangeState(GameState.InGame);
            // Time scale may be left at 0 from a previous pause screen
            Time.timeScale = 1f;
        }

        public void GameOver()
        {
            ChangeState(GameState.GameOver);
        }

        public void Victory()
        {
            ChangeState(GameState.Victory);
        }

        public void Pause()
        {
            if (State != GameState.InGame) return;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
        }

        public void Resume()
        {
            if (State != GameState.Paused) return;
            ChangeState(GameState.InGame);
            Time.timeScale = 1f;
        }

        public void BackToMenu()
        {
            ChangeState(GameState.StartMenu);
            Time.timeScale = 1f;
        }

        private void ChangeState(GameState newState)
        {
            if (State == newState) return;
            State = newState;
            OnStateChanged?.Invoke(newState);
        }

        // -----------------------------------------------------------------
        // Scene helpers
        // -----------------------------------------------------------------

        public void ReloadCurrentScene()
        {
            Time.timeScale = 1f;
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }

        public void LoadScene(string name)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(name);
        }

        public void LoadScene(int buildIndex)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(buildIndex);
        }
    }
}