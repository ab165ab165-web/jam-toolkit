# 14 · 团队分工 + 平台/构建/URP 2D（Team & Platform）

> 2 人 24h 极限赛**最容易翻车的不是技术，是工种反复撞**：美术在做玩法、机制在改 UI。
> 这一篇把**谁干什么、怎么协作、平台怎么选、URP 2D 怎么配、触控怎么适配**全部给具体值。

---

## 1. 2 人分工（机制 vs 美术，含边界与交接点）

### 1.1 角色定位

| 角色 | 主体职责 | 工具栈 | 不该碰什么 |
|---|---|---|---|
| **A 机制（= 主程 + Game Designer）** | 玩法循环、核心机制、状态机、构建、提交、**录屏/AI 工具日志** | Editor / Codely / Recorder / Git | 不直接画贴图；不写 UI 文案 |
| **B 美术（= 美术 + 音频 + UI + 文档）** | 概念图、场景、角色、UI、音效、文案、封面、视频剪辑 | TJGenerators / 概念工具 / DAW / Figma / 剪映 | 不改 `Assets/Scripts/`；不改构建配置 |

### 1.2 时间分配（24h 总览）

| 时段 | A 机制 | B 美术 |
|---|---|---|
| **0–2h**（赛前） | 建工程 + 空包构建 + 备料拷入 + 开录屏 | 概念图 + 主题情绪板（Mood Board）+ 字体/色板 |
| **2–8h**（灰盒） | 写核心机制（角色移动 / 判定 / 胜负 / 状态机）——**方块代替美术** | 概念图定稿 + 主场景背景（色块版） |
| **8–14h**（机制集成） | 接入美术资源 + 调 Juice（HitStop / Shake / SFX）+ URP 2D 灯光 | 主场景+角色：精修贴图 / 出 4 套异常态变体 |
| **14–20h**（打磨 + AI 重头戏） | 异常态切换逻辑 + 多遍机制 + 调试 | UI + 音效 + BGM + 4 张截图 + GIF |
| **20–22h**（最终构建 + 录屏） | 最终构建 + 录演示视频 + Token 截图 + 录 AI 段 | 视频剪辑 + 封面 + 文档 PDF |
| **22–24h**（提交） | 填提交表单 + 上传网盘 + 自检 | 备份工程 + 通知队群 |

### 1.3 边界与交接点

| 节点 | 谁交付 | 谁接收 | 交付内容 | 接收检查 |
|---|---|---|---|---|
| **T+2h** | A | B | 空包工程 + 目录结构 | B 能直接拖素材进 `Assets/Art/` |
| **T+8h** | A | B | 核心机制跑通的灰盒版本 + 命名空间 | B 能改美术而不破坏机制 |
| **T+8h** | B | A | 主场景背景（色块版）+ 概念定稿 | A 能直接换 SpriteRenderer 贴图 |
| **T+14h** | A | B | URP 2D 灯光可调 + 状态切换代码 | B 能调灯光触发笔触切换 |
| **T+14h** | B | A | 完整美术资源 + 音效 + UI | A 接入后不再改美术 |
| **T+20h** | A | B | 完整可玩版本（机制全通） | B 录视频 / 出截图 |
| **T+22h** | A | B | 演示视频（含 AI 录屏段） | B 套封面 + 剪尾 |

### 1.4 沟通规则（每 4h 一次同步）

| 时段 | 同步内容 | 时长 |
|---|---|---|
| T+2h | 工程跑通 + 主题确定 | 5min |
| T+6h | 灰盒机制是否成立（要不要换壳） | 10min |
| T+10h | 美术接入是否破坏机制 | 5min |
| T+14h | 完整可玩版本验收 | 10min |
| T+18h | 终局打磨优先级 | 10min |
| T+22h | 提交清单勾选 | 15min |

### 1.5 ⚠️ 决策规则

- **赛道选择**（Codely+ / AI+） → 队长拍板，**现场前 1h 必确定**。
- **主题换壳** → 队长拍板，**T+2h 灰盒前必定**，**T+4h 后不允许换壳**。
- **加班 / 通宵** → 双方同意才允许，**单人同意**单独考虑。
- **改工具 / 换赛道** → **T+12h 后禁止**（评分维度已固定）。

---

## 2. git 协作规范（2 人也能用）

### 2.1 分支模型（极简版）

| 分支 | 谁用 | 什么时候用 |
|---|---|---|
| `main` | 队长（A） | **始终可构建**；每 4h 接受合并 |
| `feat/<name>` | A 或 B | 新功能 / 新美术；命名 `feat/player-controller`、`feat/scene-bg` |
| `fix/<name>` | A 或 B | 修 bug；命名 `fix/jump-bug` |
| `wip/<name>` | 个人 | 半成品；**禁止直接合并** main |

### 2.2 分支节奏（实操）

```
main ─────────●──────●──────●──────●  (每 4h)
              ↑      ↑      ↑      ↑
              │      │      │      │
       feat/player  feat/  feat/   feat/
                    ui-bg  juice   art-final
```

- **A 机制**主要在 `main` + `feat/player-*` / `fix/*`。
- **B 美术**主要在 `feat/art-*` / `wip/*`（美术半成品多，wip/ 更安全）。
- **T+8h 前**：各自干各自的，**少合并**。
- **T+8h 后**：每 4h 必合并一次（不要攒到最后一次性合并，**冲突会爆**）。

### 2.3 提交信息（commit message）

格式：**`<类型>(<范围>): <中文说明> [时间戳] [AI工具]`**

| 类型 | 用途 | 示例 |
|---|---|---|
| `feat` | 新功能 | `feat(player): 加入可变跳高 [T+10h] [codely]` |
| `fix` | 修 bug | `fix(jump): 修土狼时间过长 [T+12h]` |
| `art` | 美术资源 | `art(scene): 主背景 v2 入池` |
| `docs` | 文档 | `docs(readme): 补 AI 工具说明` |
| `wip` | 半成品 | `wip(juice): hitstop 草稿，未测` |
| `build` | 构建产物 | `ci: 通过构建 [#T+18h]` |

> 提交信息里**必带 AI 工具**（用过的标），**不带 = 视为「未用 AI」**——和 `ai-log.md` 对得上。

### 2.4 LFS 注意

> `hackathon-prep/repo/.gitattributes` 已配好。

```
*.png  *.jpg  *.jpeg  *.webp  *.tga  *.psd   → lfs
*.wav  *.ogg  *.mp3  *.aiff                   → lfs
*.fbx  *.obj  *.blend                         → lfs
*.ttf  *.otf                                  → lfs
*.pdf  *.zip  *.tar.gz                        → lfs
```

- **现场不要 push 大文件**（网速慢 + LFS 配额问题）——先 commit 进 `.git/lfs`，**最后再 push**。
- **不要把 `Library/`、`Temp/`、`Build/`、`dist/` 推上去**——`.gitignore` 已配好。
- **不要把 `Assets/StreamingAssets/` 临时文件 commit**——会进构建包。

### 2.5 冲突处理（2 人常见场景）

| 场景 | 谁改 | 怎么避免 |
|---|---|---|
| **同一脚本两人改** | A 拍板 → 谁后改谁 merge | 改前**喊一声**；分工明确 |
| **美术资源同时改** | B 拍板 | A 接到通知再合并 |
| **`Packages/manifest.json`** | A 锁「**只 A 能改**」 | B 改完依赖**告诉 A** |
| **`ProjectSettings/`** | A 锁「**只 A 能改**」 | 同上 |
| **`docs/`** | 双方都可改 | 改完**立即 commit** |

### 2.6 兜底（如果现场完全不上 git）

- 每 4h 把工程 **zip** 一份到外接盘 / 网盘（**含 `Assets/`、`ProjectSettings/`、`Packages/manifest.json`**）。
- 命名：`JamGame_<队名>_<T+N>h.zip`。
- **不要 zip `Library/`**（占空间；编译时自动重建）。

---

## 3. 平台与构建：小游戏 vs PC 取舍 + 包体优化

### 3.1 怎么选平台（决策矩阵）

| 因素 | 小游戏（微信/抖音） | PC（Windows/Mac/Linux） | 移动端（iOS/Android） |
|---|---|---|---|
| **现场开发友好度** | ⭐⭐（转换工具额外装） | ⭐⭐⭐⭐⭐（Editor 默认） | ⭐⭐（SDK 装） |
| **评委机器配置** | 手机扫一扫 | 笔记本电脑 | 现场可能没 iOS 设备 |
| **包体约束** | **≤ 4MB 主包**（微信硬限制） | ≤ 1GB（推荐 ≤ 500MB） | ≤ 200MB（推荐 ≤ 100MB） |
| **即点即玩** | ✅ 优势大 | ❌ 要下载 | ⚠️ 要扫码 |
| **平台差异化权重(10%)** | 包体/即点即玩（10% 平台维度） | 画质/沉浸感（10%） | 操作适配/机型兼容（10%） |
| **24h 难度** | ⭐⭐⭐⭐ | ⭐⭐⭐ | ⭐⭐⭐⭐ |
| **适配难度** | 触控 + 屏幕尺寸 + 性能 | 键鼠 + 手柄 + 多分辨率 | 触控 + 多机型 |

### 3.2 24h 推荐选择（**默认 PC**）

- **默认 PC（Windows64）**：开发最快 / 评委最方便 / 提交最稳。
- **如果一定要做小游戏**：Editor → Switch Platform → WebGL → 微信小游戏转换工具 → 上传。
- **如果团队有移动端经验**：iOS / Android 可选，但**调试机型**现场可能缺。
- **如果主题明显是「碎片化体验」**：选小游戏。
- **如果主题明显是「沉浸/画质」**：选 PC。

### 3.3 包体优化要点（**全平台通用**）

| 类别 | 优化项 | 数值目标 |
|---|---|---|
| **纹理** | ASTC（移动/WebGL）/ DXT（PC）；分辨率 ≤ 1024×1024（场景）；主角 ≤ 512×512 | 主包内纹理 ≤ 30MB |
| **音频** | `ogg` / `.mp3`（不要 `.wav`）；Bgm 64kbps / SFX 96kbps；**不要 Resources** | 音频 ≤ 10MB |
| **模型** | 移动端 face_limit 5000 / PC 10000；Hunyuan face_count 50000 / 200000 | 模型 ≤ 20MB |
| **代码** | IL2CPP + Managed Strip Level = High；Strip Engine Code = on；删除未用 Package | code 包 ≤ 30MB（小游戏首包 ≤ 10MB） |
| **首场景** | 仅必要资源（不引整图集）；Addressables 加载其余 | 首场景 ≤ 5MB |
| **资源加载** | **不要 `Resources/`**；用 Addressables / AssetBundle | 单包 ≤ 2MB |
| **分包** | WebGL 用 Brotli；小游戏用代码分包（首包 ≤ 1/3 原始） | 总包 ≤ 30MB（小游戏首包 ≤ 4MB） |
| **Shader** | 关未用 shader 变体；URP `Strip 2D Unused Variants` **开**（减少包体；与 §4.2.E 统一） | 减少 10–30% 包体 |

### 3.4 构建脚本（沿用 `hackathon-prep/repo/build.sh`）

```bash
# 编辑器路径
export UNITY_EDITOR=/path/to/Tuanjie/Hub/Editor/2022.3.62t16/Editor/Tuanjie

# PC（最快，最稳）
./build.sh win

# 小游戏
./build.sh webgl
# 然后跑微信小游戏转换工具

# 移动端（可选）
./build.sh android
```

- `build.sh` 已配好 `Assets/Editor/BuildPipelineHelper.cs` 的 `-executeMethod` 入口。
- **现场先跑一次 PC 空包构建**（与 13 §1.1 第 6 步对齐）。

### 3.5 Graphics API 配置（小游戏专属）

- `Project Settings > Player > WechatMinigame > Other Settings` → 取消 `Auto Graphics API` → **只保留 WebGL 1 或 WebGL 2** → 减少 shader 变体。
- `Player > Publishing Settings` → Debug Symbols = `external`；异常选 `WASM Exception`。

---

## 4. URP 2D Renderer / Light2D 基本配置

### 4.1 工程模板选择

- 新建工程 → **`2D (URP)`** 模板（不是 `3D (URP)`）。
- 推荐 Unity/团结版本：**2022.3.62t16**（与本赛一致）。

### 4.2 关键配置（按场景给具体值）

#### A. **Renderer 资产配置**

| 项 | 路径 | 值 |
|---|---|---|
| **Renderer 资产** | `Project Settings > Graphics > Render Pipeline Asset` | 2D Renderer（不是 2D Render Pipeline Asset for 2D URP HD） |
| **2D Renderer Asset** | `Assets/Settings/2D Renderer.asset` | Blend Styles：保留 **Multiply** + **Additive** 两种（多了增包体 + 损性能） |

#### B. **2D Camera 配置**

| 项 | 值 |
|---|---|
| **Projection** | Orthographic（不要 Perspective，2D 必备） |
| **Size** | 5–8（看场景尺度） |
| **Background** | 单色（按调色板定；不要渐变，2D 渐变要后处理） |
| **Clear Flags** | Solid Color |
| **Culling Mask** | 仅 Default + UI 层 |
| **Sorting Layer** | 仅 Default |

#### C. **Light2D 配置**

| 项 | 值 | 说明 |
|---|---|---|
| **光源类型** | **Freeform**（首选）/ Spot / Sprite | Freeform 适合「自然形状」；Spot 适合「灯笼/灯」；Sprite 适合「火焰贴图光源」 |
| **Blend Style** | **Multiply**（默认，必有） / Additive（可选） | 多于 2 种 → 性能降 |
| **Target Sorting Layers** | **单层**（Default 或自定义一层） | 多层 → 性能降 |
| **Light Order** | -1 / 0 / 1 错开 | 小值先渲染 |
| **Intensity** | 0.6–1.2（场景基线）；异常态调到 0.2–0.4 | 异常态靠 **强度骤变** 触发恐怖 |
| **Color** | 暖白（#FFF5E0，正常） / 冷青（#B0D8E0,异常） | 异常态**只改色相**= Control 式恐怖 |
| **Shadow Strength** | 0–1 | 默认 0，启用后设 0.5–0.8 |
| **Volumetric** | **关**（默认） | 24h 不开体积光 |
| **掺 Target Sorting Layers** | 一个就够 | 性能 |

#### D. **Sprite / Tilemap 配置**

- Sprite 用 **Sprite-Lit-Default**（自动与 2D 光兼容）。
- Tilemap Mode：`Individual` 或 `SRP Batch`（**不要 Chunk**——与 SRP Batcher 不兼容）。
- Sprite 内有 Normal Map 时：Normal Map Quality = Accurate（光照细节好）；没有就 Disable。

#### E. **性能开关**

| 项 | 路径 | 值 |
|---|---|---|
| **Strip 2D Unused Variants** | `Project Settings > Graphics` | **开**（减少 shader 变体 → 减小包体） |
| **SRP Batcher** | `Project Settings > Graphics` | **开** |
| **Light Culling Mask** | 每盏 Light2D 上 | **单 Sorting Layer** |
| **Lighting Texture Size** | 2D Renderer Asset | `Medium (512)` 或 `Low (256)`（24h 不上 `High`） |

### 4.3 异常态触发（与「内核」结合）

> 配合 01-core §核心机制：异常态 = **同机位 + 只改灯光**。

```csharp
// LightingFlicker.cs —— 异常态灯光强度骤变
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightingFlicker : MonoBehaviour
{
    public Light2D mainLight;
    public float normalIntensity = 1.0f;
    public float abnormalIntensity = 0.25f;
    public float lerpDuration = 1.5f;

    public void TriggerAbnormal()
    {
        LeanTween.value(gameObject, normalIntensity, abnormalIntensity, lerpDuration)
            .setOnUpdate((float v) => mainLight.intensity = v);
    }

    public void RestoreNormal()
    {
        LeanTween.value(gameObject, abnormalIntensity, normalIntensity, lerpDuration)
            .setOnUpdate((float v) => mainLight.intensity = v);
    }
}
```

> `LeanTween` → 本赛用 **LitMotion**（MIT 替代，见 03-jam-rules §三）。

---

## 5. 触控适配要点（小游戏 / 移动端）

> 选 PC 方案可跳过；选小游戏 / 移动端**必须读**。

### 5.1 Input System 配触控（小游戏关键）

| 项 | 配置 |
|---|---|
| **Event System** | 把 `Standalone Input Module` 换为 `InputSystemUIInputModule` |
| **Touch 支持** | 用 `EnhancedTouch.Touch`（推荐） 或 `InputSystem.Touchscreen` |
| **虚拟控件** | On-Screen Stick / On-Screen Button（Input System 自带） |
| **多点触控** | 绑定 `<Touchscreen>/touch*/press`，Action Type = **Pass-Through** |
| **Input Field** | 用 `WXInputFieldAdapter`（微信小游戏 SDK 提供） |

### 5.2 屏幕适配

| 项 | 值 |
|---|---|
| **Canvas Scaler** | `Scale With Screen Size`；Reference Resolution = `1920×1080` |
| **Match** | 0.5（Width/Height 等比缩放） |
| **Safe Area** | 顶部刘海 / 底部 Home 条 → `Screen.safeArea` 留 padding |
| **Orientation** | 横屏锁定（小游戏 90% 横屏作品） |

### 5.3 触控体验细节（24h 内易忽略）

| 问题 | 解决 |
|---|---|
| **按钮太小点不到** | 最小 **88×88 px**（Apple HIG）；游戏内 ≥ **120×120 px** |
| **滑动 vs 点击冲突** | `InputAction` 区分 `<Pointer>/press`（点击） vs `<Pointer>/delta`（拖拽） |
| **屏幕旋转跳变** | 锁定方向（最稳） 或 监听 `Screen.orientation` 改 UI |
| **刘海遮挡** | `Screen.safeArea` 算 padding |
| **打字体验差** | 用 `WXInputFieldAdapter`，别用 Unity 自带 InputField |

### 5.4 微信小游戏转换步骤（**仅选小游戏时**）

1. **Build** → Platform = **WebGL** → 输出 `webgl/Build/`。
2. 装微信小游戏转换工具（`minigame-unity-webgl-transform`）。
3. 跑 `webgl/minigame/transform.js` → 输出 `minigame/` 目录。
4. 微信开发者工具导入 `minigame/` → 真机预览。
5. 包体优化（见 §3.3）→ 主包 ≤ 4MB。

---

## 6. ⚠️ 待现场确认项汇总

| 项 | 现场找谁问 |
|---|---|
| **赛道是否锁单**（能否中途 Codely+ ↔ AI+） | 主办 |
| **Codely Token 发放方式**（扫码/账号/前置） | 主办 / 团结 Hub |
| **Codely+ Token 截图具体 URL** | 现场点开「账户与用量」验证 |
| **录屏软件是否指定** | 主办 |
| **提交表单字段**（实时提交入口） | 主办 / 提交平台 |
| **网盘 / 提交平台是否锁定**（百度网盘 vs 其他） | 主办（默认按规则 = 百度网盘，提取码 UCDC） |
| **微信小游戏转换工具版本** | 选小游戏时才需要 |

---

## 7. 启动即用一份表（贴团队屏）

| 谁 | 现在做 | 下一件事 |
|---|---|---|
| **A 机制** | 建工程 + 空包构建 | 写核心机制 |
| **B 美术** | 概念图 + 情绪板 | 主场景色块版 |
| **共用** | 开录屏 + 写 `ai-log.md` 表头 + 建网盘文件夹 | T+2h 同步会议 |