# Unity 2022.3 / 团结引擎 2022.3 packages-manifest.json 依赖参考

> 把下面这段 `dependencies` 块合并到 Unity 工程 `Packages/manifest.json`。
> 团结引擎对 UPM 包的支持与 Unity 一致；按需勾选即可。

```json
{
  "dependencies": {
    "com.unity.collab-proxy": "2.2.0",
    "com.unity.ide.rider": "3.0.27",
    "com.unity.ide.visualstudio": "2.0.22",
    "com.unity.test-framework": "1.1.33",
    "com.unity.timeline": "1.7.6",
    "com.unity.ugui": "1.0.0",
    "com.unity.modules.audio": "1.0.0",
    "com.unity.modules.imageconversion": "1.0.0",
    "com.unity.modules.imgui": "1.0.0",
    "com.unity.modules.jsonserialize": "1.0.0",
    "com.unity.modules.physics": "1.0.0",
    "com.unity.modules.physics2d": "1.0.0",
    "com.unity.modules.terrain": "1.0.0",
    "com.unity.modules.terrainphysics": "1.0.0",
    "com.unity.modules.tilemap": "1.0.0",
    "com.unity.modules.ui": "1.0.0",
    "com.unity.modules.uielements": "1.0.0",
    "com.unity.modules.unitywebrequest": "1.0.0",

    "com.unity.cinemachine": "6.6.0",
    "com.unity.inputsystem": "1.7.0",
    "com.unity.postprocessing": "3.2.2",
    "com.unity.render-pipelines.universal": "14.0.9",

    "com.unity.addressables": "1.21.14"
  }
}
```

> **Cinemachine 版本说明**：本 manifest 默认 `6.6.0` 是为了与本仓库
> prefab（`open-assets/cinemachine-urp/Prefabs/JamKit_*.prefab`，使用
> `CinemachineCamera` CM3+ API）对齐。**团结引擎 2022.3 自带 Cinemachine
> 2.9.7（CM2 API，类名不同）**，无法加载本仓库 prefab — 使用时请按现场
> 团结版本切换：自带版本能跑就用自带（但 prefab 会变粉/失效），否则
> UPM 升级到 ≥ 3.0.0。

## 包名 → 用途对照

| 包名 | 用途 | 备注 |
| --- | --- | --- |
| `com.unity.cinemachine` | 智能相机（跟随、过肩、震屏） | **6.6.0**（CM3+ API 与本仓库 prefab 对齐）；团结 2022.3 自带 2.9.7（CM2）需替换；震屏也可以用 JuiceManager.Shake |
| `com.unity.inputsystem` | 新版输入 | 如果用 Input System，把 TopDownMover/PlatformerMover 里的 `Input.GetAxisRaw` 换成 `Keyboard.current` |
| `com.unity.postprocessing` | 后处理 | Bloom/ChromaticAberration/Vignette；URP 也可走 `com.unity.render-pipelines.universal` 自带 |
| `com.unity.render-pipelines.universal` | URP | 默认渲染管线 |
| `com.unity.addressables` | 按需加载 | 24h 黑客松可选 — 简单项目不需要 |
| `com.unity.modules.tilemap` | 内置 2D TileMap 模块 | 平台调/俯视 2D 都常用（注意：是 `modules.tilemap`，**不是** `com.unity.tilemap` — 后者不是有效 UPM 包名） |
| `com.unity.ugui` | uGUI | UI Pack + Canvas 用 |
| `com.unity.test-framework` | 单元测试 | 可选 |

## 团结引擎特别说明

- 团结引擎（Unity China fork）对 UPM 包的兼容性 >95%，但部分 Unity Asset Store 包未在团结商店上架；
- 在「包管理器 → My Registries」里确认 `registry.tuanjieengine.com:8000` 已添加；
- `manifest.json` 直接复制上面这份即可；如果用旧 `input manager`，**移除** `com.unity.inputsystem`。

## 强制依赖检查清单

提交工程时务必保证：

- [ ] `com.unity.cinemachine` 在 `dependencies`
- [ ] `com.unity.inputsystem` 或显式声明用旧版 Input（决定 Mover 怎么写）
- [ ] `com.unity.render-pipelines.universal` 启用 URP
- [ ] `com.unity.postprocessing` 或 URP 自带的后处理 volume

> 提示：在「团结 Hub」创建工程时选 `3D (URP)` 或 `2D (URP)` 模板就自带这些包；模板外工程用本清单手动合并。