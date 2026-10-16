# 团结 AI 生成器 TJGenerators — 技巧速查

> 抓取自团结官方文档站 https://codely-docs.tuanjie.cn/en/ai-generation-tools/  
> 抓取时间：2026-10-16；覆盖 19 个目标页（16 完整 + 3 内容偏薄；详见文末附录 D）  
> 包名（Package）：`cn.tuanjie.ai.generators` （TJGenerators 扩展）  
> 入口统一：Editor 菜单 `AI > Generate` / `AI > Search Generated Assets`，CLI 通过 Codely Bridge + MCP 调用  
> 默认产物目录：`Assets/TJGenerators/History/`（视频特例为 `Assets/Video/`）

---

## 目录

- [0. 安装与入口](#0-安装与入口)
- [1. 3D 模型 (Tripo P1 / Hunyuan 3.1)](#1-3d-模型-tripo-p1--hunyuan-31)
- [2. 人形动画角色 (Meshy AI)](#2-人形动画角色-meshy-ai)
- [3. 模型绑定 + 动画生成 (UniRig / HunyuanMotion)](#3-模型绑定--动画生成-unirig--hunyuanmotion)
- [4. 地形 Terrain (Frontier)](#4-地形-terrain-frontier)
- [5. 天空盒 Skybox (Rodin)](#5-天空盒-skybox-rodin)
- [6. 2D 精灵 Sprite (Frontier Game Design / SeeDream / Frontier Effect)](#6-2d-精灵-sprite-frontier-game-design--seedream--frontier-effect)
- [7. 表面材质 Material (SeeDream Material)](#7-表面材质-material-seedream-material)
- [8. 2D 动作序列帧 (sprite_sequence_v1)](#8-2d-动作序列帧-sprite_sequence_v1)
- [9. 2D 精灵表单列帧 (GUI-only, Frontier Game Design)](#9-2d-精灵表单列帧-gui-only-frontier-game-design)
- [10. 音频 BGM (Huoshan Music)](#10-音频-bgm-huoshan-music)
- [11. 音频 SFX (SFX AI)](#11-音频-sfx-sfx-ai)
- [12. TTS 语音合成 (MiniMax TTS)](#12-tts-语音合成-minimax-tts)
- [13. 通用图片 Image (Frontier Game Design / SeeDream Image / Frontier Effect)](#13-通用图片-image-frontier-game-design--seedream-image--frontier-effect)
- [14. 视频 Video (Huoshan SeeDance)](#14-视频-video-huoshan-seedance)
- [附录 A：13 工具总览表](#附录-a13-工具总览表)
- [附录 B：MCP 工具名清单 + 配置命令](#附录-bmcp-工具名清单--配置命令)
- [附录 C：可溯源提示（哪些步骤能留痕）](#附录-c可溯源提示哪些步骤能留痕)
- [附录 D：抓取索引](#附录-d抓取索引)

---

## 0. 安装与入口

**安装方式三选一：**

1. **Editor 内对话安装**：打开项目 → AI 对话框默认出现 → 点 `/` → Manage Extensions → 搜 `TJGenerators` → Install
2. **Tuanjie Cowork 安装**：在 Cowork 对话框点 `/` → 选 `Open Game Project` 打开项目 → 自动装 `cn.tuanjie.codely.bridge`（Console 显示成功日志，Codely Bridge 状态变 "available"）→ 同样 `/` → Manage Extensions 装 TJGenerators
3. **Package Manager**：菜单 `Window > Package Manager` → `+` → *Add package by name* → 输入 `cn.tuanjie.ai.generators` → Add

**使用入口：**
- 菜单：`AI > Generate`（全部 13 个工具的 GUI 入口）
- 菜单：`AI > Search Generated Assets`（在 Project 窗口中显示所有 AI 生成资产）
- 重画已生成资产：选中 Prefab → Inspector 顶部点 `AI Generate` 按钮 → 改 prompt/参数/参考图 → Generate → 「Apply to Current Prefab」覆盖
- CLI / Agent：所有工具在 Codely Bridge / MCP 中暴露（见 [附录 B](#附录-bmcp-工具名清单--配置命令)）

**Unity 版本要求：2020.3+，推荐 2022 LTS**

---

## 1. 3D 模型 (Tripo P1 / Hunyuan 3.1)

- **能做什么**：文字 / 图片 / 多视图 / 分离模型 生成 GLB 或 FBX。Props、建筑、小摆件首选。
- **入口**：
  - 菜单：`AI > Generate > Generate 3D Model`
  - MCP 工具：`generate_3d_model_by_tripo_p1`（默认）、`generate_3d_model_by_tencent_generation`（Hunyuan 3.1）
- **关键参数**：
  - `prompt` (≤1024 字符 Tripo / ≤1000 Hunyuan)
  - `image_path`：参考图 Assets/...
  - `multiview_image_paths`：多视图数组 `[front, left, back, right]`，至少 3 张，必须有 front（仅 Tripo P1）
  - `face_limit` (Tripo)：48–20000；`face_count` (Hunyuan)：1000–2000000，默认 500000
  - `texture` / `pbr` / `texture_quality` / `texture_seed`（Tripo）
  - `export_uv` (Tripo，默认 true，false 省时减小体积)
  - `texture_alignment`：original_image / geometry
  - `orientation`：default / align_image
  - `with_mesh_segmentation` (Tripo，默认 false)：true = 语义切分子网格 = 分离模型
  - `enable_pbr` (Hunyuan)
  - `result_format` (Hunyuan)："GLB" / "FBX"
- **提示词写法**（Play Guide 给的"跟 Agent 说"模板）：
  - "Generate a wooden crate with low polygon count, for mobile use"
  - "Create a magic sword, high-precision hero model, maximize polygon count"
  - "Generate a robot with PBR material, export in FBX format"
  - "Generate a cartoon dog without textures, just show me the untextured model first"
  - 分离版："Generate a small car, with separated parts—doors and wheels can be detached individually"（启用 `with_mesh_segmentation: true`）
- **输出格式**：GLB (Tripo 默认) / GLB 或 FBX (Hunyuan) → 自动落 Prefab
- **坑**：
  - ⚠️ P1-20260311 版本**禁用**参数：`quad` / `smart_low_poly` / `generate_parts` / `geometry_quality`，传了就 400
  - ⚠️ 生成耗时 3–15 分钟；**100% ≠ 完成**，模型文件还在下载导入，等结束
  - ⚠️ 生成期间**禁止写 .cs 到磁盘**（触发 Domain Reload），用 `execute_csharp_script`
  - ⚠️ 中断后恢复必须加 `force_overwrite=true`
  - ⚠️ 大场景地形用 `generate_terrain`，别用 3D 工具
  - ⚠️ 人形骨骼动画用 `generate_animated_character`，别用 3D

**面数优化配比：**
| 场景 | Tripo face_limit | Hunyuan face_count |
|---|---|---|
| 移动端 / 背景物 | 5000 | 50000 |
| 通用 | 10000 | 200000 |
| 主角 Hero | — | 500000+ |

---

## 2. 人形动画角色 (Meshy AI)

- **能做什么**：从文本描述生成人形（Humanoid）角色 + 骨骼动画，最多 4 个 FBX（角色 + 动画）+ 自动 Animator + AnimatorController（Idle/Walk/Run/Action 四个状态）。
- **入口**：
  - 菜单：同 3D 模型流程（通过对话框描述"动起来的人形"）
  - MCP 工具：`generate_animated_character`（模型 `meshy-ai`）
- **关键参数**：
  - `prompt` (≤600 字符，**必填**)
  - `prefab_output_path`：默认 `Assets/TJGenerators/History/` 同名自动去重
  - `action_id`（默认 452 = Backflip）—— 676 个动作，见下面部分
  - `target_polycount`（100–300000，启用 remesh 时有效）
  - `pose_mode`："t-pose" / "a-pose" / ""
  - `height_meters`（默认 1.7）
  - `enable_pbr`（默认 true）
  - `should_remesh`（默认 true）;`topology`："triangle"（默认） / "quad";`symmetry_mode`："off"/"auto"/"on"
  - `seed`（默认 0 随机）
  - `force_overwrite`（默认 false）
- **常用动作 ID**：

| ID | 动作 | 类别 |
|---|---|---|
| 0 | Idle | Daily |
| 42 | Casual_Walk | Walking |
| 26 | Run_02 | Running |
| 4 | Attack | Combat |
| 75 | Arm_Circle_Shuffle | Dance |
| 452 | Backflip（默认） | Stunts |

完整 676 动作见 **TJGenerators 官方包内** `animations.json`。

**AnimatorController 自动配置：**
- States：Idle（无动画时退回 T-pose）/ Walk（Speed>0.1）/ Run（Speed>0.5）/ Action（Trigger 触发）
- 默认状态优先级：Action（进 Play Mode 即播）→ Walk → Idle

- **提示词写法**：
  - "a medieval knight in full plate armor with a red cape and horned helmet"（注意装备细节）
  - "low-poly cartoony warrior" / "hyper-realistic sci-fi soldier"（指定风格）
- **输出格式**：4 个 FBX + 自动配置 Animator + AnimatorController → Prefab
- **坑**：
  - ⚠️ **只能 Humanoid**：动物 / 怪物 / 非人形骨架不支持
  - ⚠️ 耗时 3–10 分钟
  - ⚠️ **最多 3 个并发任务**（其他工具是 5）
  - ⚠️ 默认动作是 Backflip，进 Play Mode 不写代码就会翻跟头
  - ⚠️ `should_remesh: false` 时返回最高精度三角网，`target_polycount` 不生效
  - ⚠️ WASD 移动控制要写 MonoBehaviour 用 `execute_csharp_script`，别写 .cs 到磁盘
  - ⚠️ 通用 3D 物件（武器/家具/载具）请用 `generate_3d_model`

**面数建议：**
- 移动端/背景 NPC：5000–10000
- PC 主角：15000–30000
- Hero：50000+

---

## 3. 模型绑定 + 动画生成 (UniRig / HunyuanMotion)

- **能做什么**：给现有 3D 模型做骨骼绑定 (Rig)、加动作、整体一键 Rig+动画。
- **决策树**（直接抄自官方）：

```
What do you want to do with an existing model?
 ├── Rig only (no animation) → generate_rigged_model       (UniRig)
 ├── Already Humanoid, just animations → generate_model_motion  (HunyuanMotion)
 └── Rig + Animation in one step → generate_rigged_animated_model
```

- **入口**：
  - MCP 工具：`generate_rigged_model` / `generate_model_motion` / `generate_rigged_animated_model`
  - 用户说法："rig this model" / "make the model move" / "add running animation to character" / "rig and generate animation"
- **关键参数**：见各子工具文档（页面内容较简略）
- **坑**：⚠️ 此页内容在 readability 抓取下较薄（1315 字符），详细参数请直接查官方页 https://codely-docs.tuanjie.cn/en/ai-generation-tools/rigged-animated-model

---

## 4. 地形 Terrain (Frontier)

- **能做什么**：从文本或参考图生成大尺度 Unity Terrain（灰度 heightmap）→ 自动应用成 Terrain GameObject。
  - 适用：山谷、山脉、丘陵、平原、高原、岛屿、火山、沙丘、海岸悬崖
  - **不适用**：独立物件（石头、树、建筑）→ 用 §1
- **入口**：
  - 菜单：同 3D 流程，对话框描述地形
  - MCP 工具两步走：`generate_terrain` → `apply_terrain_heightmap`（默认自动完成）
- **关键参数**：
  - `generate_terrain`：
    - `prompt`：地形描述
    - `image_path`：参考图 (Assets/...) 引导地形形状
    - `resolution`："1K" / "2K"（默认） / "4K"
    - 至少 `prompt` 或 `image_path` 之一
  - `apply_terrain_heightmap`（后处理）：
    - `use_default_options`（默认 true = 推荐后处理：中值+双边+热侵蚀+百分位归一）
    - `height_gamma`（默认 1.0；<1 山更陡，>1 更平）
    - `remap_output_min/max`（默认 0.02/0.98；调高 min 抬升"海平面"，调低 max 削平山顶）
    - `thermal_erosion`（默认 true）+ `thermal_erosion_iterations`（默认 25）
    - `bilateral_filter`（默认 true，优于 Gaussian）
    - `terrain_go_name`（默认 "TJGenerators Terrain"）
- **地形提示词示例**：

| 地形 | 推荐 prompt |
|---|---|
| 山脉 | "rugged mountain range with steep peaks and deep valleys, gradual foothills" |
| 丘陵 | "gently rolling grassy plains with subtle elevation changes" |
| 岛屿 | "tropical island with central mountain peak, flat coastal area" |
| 峡谷 | "deep canyon with layered rock formations, narrow river valley at bottom" |
| 火山 | "dormant volcano with central caldera, smooth sloped sides" |
| 沙丘 | "sand dunes with smooth flowing curves, gradual ridges, no sharp edges" |

- **输出格式**：Unity Terrain GameObject + TerrainData asset + heightmap PNG → `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ 两步流程，默认自动
  - ⚠️ 2K 后处理耗时 60–180 秒，**别反复重提**
  - ⚠️ 长宽比固定 1:1，**无 `generator_id` 参数**
  - ⚠️ 别描述植被、建筑物、树 → 会产生不期望的高度凸起
  - ⚠️ 给参考图 (`image_path`) 形状控制显著提升精度

---

## 5. 天空盒 Skybox (Rodin)

- **能做什么**：生成场景背景天空（Cubemap）→ 自动创建 `.mat` 材质。
- **入口**：
  - 菜单：同 3D 流程，对话框描述天空
  - MCP 工具：`generate_skybox`（仅 Rodin Skybox 模型）
- **关键参数**：
  - `prompt`：天空描述（"sunset over ocean with dramatic clouds"）
  - `image_path`：参考图 (Assets/...)
  - `high_res`：高质量档开关（默认后台启用）
  - 至少 `prompt` 或 `image_path` 之一
- **输出格式**：PNG → TextureCube (Cubemap) → `.mat`，落 `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ **无 `resolution` 自定义**，后端固定分辨率档；想要 4K 类等距全景需后置调用 `upscale_image`（Real-ESRGAN 4×）上到 4096 宽
  - ⚠️ 耗时 60–180 秒
  - ⚠️ 别用 Skybox 工具去生成 3D 模型 / 精灵 / 角色
- **提示词优化**：
  - 强调氛围："golden hour sunset with volumetric light rays" 比 "sunset" 好
  - 给天气时段："starry night sky with northern lights" / "overcast grey sky with rolling clouds"
  - 给远景细节："distant mountain silhouettes" / "horizon glow"

---

## 6. 2D 精灵 Sprite (Frontier Game Design / SeeDream / Frontier Effect)

- **能做什么**：生成游戏内 2D 资产（图标、道具、头像、技能图标、UI 元素），PNG → 自动 Sprite (Texture2D)。
- **入口**：
  - 菜单：`AI > Generate > Generate Sprite`
  - MCP 工具：`generate_sprite`
- **模型三选一**（`generator_id`）：

### 模型 A：Frontier Game Design（默认）
- `generator_id: frontier-game-design`
- 用途：游戏图标 / 道具 / 概念型资产，自带游戏 prompt 模板
- 参数：
  - `prompt` / `image_path`
  - `imageSize`：square_hd（默认）/ square / portrait_4_3 / portrait_16_9 / landscape_4_3 / landscape_16_9
  - `outputFormat`：png（默认）/ jpeg
  - **不支持**：`type_id` / `style_id`（用描述词引导）

### 模型 B：Huoshan SeeDream
- `generator_id: huoshan_seedream`
- 用户说法："use Huoshan model" / "use SeeDream"
- 用途：通用，含内容类型 / 美术风格 / 自动抠图
- 参数：
  - `prompt` / `image_path`
  - `type_id`（见下 31 类）
  - `style_id`（见下 29 类）
  - `size`（默认 2048×2048，8 种预设，见下）
  - `is_segmentation`：自动抠图（默认 true）
  - `q_value`：抠图后压缩质量 1–100（默认 75）
  - `resize_width`：输出最大宽度（px），0 = 不缩放

### 模型 C：Frontier Effect
- `generator_id: frontier-effect`
- 用途：风格化、美术效果精灵
- 参数：`prompt` / `image_path` / `resolution` (0.5K/1K/2K/4K) / `aspect_ratio` (auto/16:9/9:16/1:1/...) / `output_format`

### 内容类型（SeeDream 31 类）
weapon_melee / weapon_ranged / weapon_magic / armor_head / armor_body / armor_shield / armor_accessory / consumable_hp / consumable_mp / consumable_buff / material_common / material_rare / material_epic / key_item / quest_item / tool_gathering / tool_crafting / mount / pet / character_hero / character_npc / monster / furniture / decoration / ui_button / ui_icon / ui_frame / skill_active / skill_passive / effect_buff / effect_debuff

### 美术风格（SeeDream 29 类）
pixel / pixel_8bit / pixel_16bit / cartoon / anime / chibi / realistic / semi_realistic / flat / vector / minimalist / watercolor / oil_painting / sketch / lineart / hand_drawn / cell_shading / low_poly / isometric / top_down / side_scroller / cyberpunk / steampunk / fantasy / sci_fi / horror / cute / gothic / medieval

### 输出尺寸（SeeDream 8 种）
2048×2048（默认 1:1）, 2304×1728 (4:3), 1728×2304 (3:4), 2560×1440 (16:9), 1440×2560 (9:16), 2496×1664 (3:2), 1664×2496 (2:3), 3024×1296 (21:9)

- **提示词写法**：
  - "a glowing blue crystal sword with intricate rune engravings" 比 "a sword" 好
  - Frontier Game Design 默认即适合游戏图标/道具，无须选类型和风格
  - SeeDream 类型+风格组合比纯文本更精准
- **输出格式**：PNG → 自动 Sprite (Texture2D) → `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ 默认模型是 Frontier Game Design；`type_id`/`style_id`/`size`/`is_segmentation` 仅对 SeeDream 生效
  - ⚠️ SeeDream 最小尺寸 ≈1920×1920，再小会触发 400
  - ⚠️ **精灵默认缩放异常大**（2048px / 100PPU ≈ 20.48 单位），场景中必须设 `localScale: 0.05f ≈ 1 单位宽`，不设就是 20 米大
  - ⚠️ 耗时 60–180 秒
  - ⚠️ 精灵 vs 图像：精灵=游戏中 2D 资产（透明背景/图标/头像）；图=概念/原画/背景
  - ⚠️ 最多 5 并发

---

## 7. 表面材质 Material (SeeDream Material)

- **能做什么**：生成 PBR 表面材质（金属/木材/石材/布料…），输出无缝 PNG (Default Texture) + 可直接用的 Unity `.mat` 材质。
- **入口**：
  - 菜单：`AI > Generate > Generate Surface Material`
  - MCP 工具：`generate_material`（唯一模型 `huoshan_seedream_material`）
- **关键参数**（至少传一个；组合 prompt = `preset.prompt + ", " + style.prompt + ", " + 你的 prompt`）：
  - `prompt`：附加描述
  - `image_path`：参考图（被 `pattern_id` 模板覆盖时失效）
  - `preset_id`：13 类材质预设（**自动设置 Metallic / Smoothness**）
  - `pattern_id`：12 类纹理图案（**需先在 Editor 通过菜单 `AI > Development > Generate Texture Direction Template` 预生成**）
  - `style_id`：表面状态（5 种）
  - `size`：1024×1024 / 2048×2048（默认）/ 4096×4096

### 材质预设（13 类 preset_id）
| ID | 名称 | Metallic | Smoothness |
|---|---|---|---|
| metal | Metal | 0.9 | 0.8 |
| wood | Wood | 0 | 0.3 |
| stone | Stone | 0 | 0.1 |
| fabric | Fabric | 0 | 0.3 |
| leather | Leather | 0 | 0.3 |
| concrete | Concrete | 0 | 0.1 |
| brick | Brick | 0 | 0.1 |
| tile | Tile | 0 | 0.1 |
| glass | Glass | 0 | 0.95 |
| ceramic | Ceramic | 0 | 0.5 |
| grass | Grass | 0 | 0.5 |
| sand | Sand | 0 | 0.5 |
| snow | Snow | 0 | 0.5 |

### 纹理图案（12 种 pattern_id）
uniform, horizontal_lines, vertical_lines, cross_hatch, diagonal, wave, noise, honeycomb, brick_layout, scales, cracks, woven

### 表面状态（5 种 style_id）
new（全新）, aged（老化）, dirty（脏污）, wet（湿润）, weathered（风化）

- **提示词写法**：
  - 组合范例：`preset_id: "metal" + style_id: "aged" + prompt: "rusted iron with orange patina"`
  - **描述表面，别描述物体**："seamless tileable" 加上去很值
- **输出格式**：无缝 PNG（Default Texture）+ `.mat` → `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ `pattern_id` 必须**先在 Editor 菜单 `AI > Development > Generate Texture Direction Template` 预生成模板**，否则忽略 fallback 到 `image_path`
  - ⚠️ 材质 ≠ 精灵：材质用于 3D 表面 PBR；精灵用于 2D 图标/头像
  - ⚠️ 类型预设自动设 Metallic/Smoothness，不用手调
  - ⚠️ 耗时 60–180 秒

---

## 8. 2D 动作序列帧 (sprite_sequence_v1)

- **能做什么**：从一张角色参考图生成 2D 动作序列帧（Idle / 前跑 / 后跑）+ 自动导出 AnimationClip (.anim)。
- **入口**：
  - 菜单：`AI > Generate > 2D Action Sequence`
  - MCP 工具：`generate_sprite_sequence`（`generator_id: sprite_sequence_v1`，仅此一个）
- **关键参数**：
  - `image_path`（**必填**，参考图）
  - `animation_type`：`idle`（默认） / `frontRun` / `backRun`（仅这三值，其他回退 idle）
  - `fps`（1–60，默认 12）
  - `loop`（默认 true）
- **动作类型**：

| animation_type | 描述 |
|---|---|
| idle | 站立 / 呼吸（默认） |
| frontRun | 朝前跑步循环 |
| backRun | 朝后跑步循环 |

- **提示词写法**：**本工具基于参考图，不靠 prompt 描述动作**；流程是先有角色图再生成动作。
- **输出格式**：多张 Sprite PNG + 一个 AnimationClip → `Assets/TJGenerators/History/Sequence_yyyyMMdd_HHmmss/`
- **坑**：
  - ⚠️ `image_path` 必须有，工具不预校验文件存在，路径错了直接 fail
  - ⚠️ 工具不会替你生成角色，无参考图时先用 `generate_sprite` 生成精灵（建议加 `is_segmentation: true`）再链
  - ⚠️ 输出是 **2D 帧动画**而非 3D 骨骼动画（要骨骼请用 §2）
  - ⚠️ AnimationClip 驱动 `SpriteRenderer.m_Sprite`（切换帧），不是骨骼变形；播放端 GameObject 必须挂 `SpriteRenderer` + `Animator`
  - ⚠️ 4×4 精灵表单列帧请用 §9
  - ⚠️ 耗时 60–180 秒

**FPS 选择**：8–12 像素风，12–18 普通动效，24 高质量，30+ 极速/粒子

---

## 9. 2D 精灵表单列帧 (GUI-only, Frontier Game Design)

- **能做什么**：生成 4×4 精灵表单（spritesheet）→ 自动绿幕抠图 + 网格切片 + 导出 AnimationClip。
- **入口**：
  - 菜单：`AI > Generate > 2D Spritesheet Sequence`（**GUI 唯一入口，无独立 CLI 工具**）
- **输入**：
  - 文本：角色 + 动作描述
  - 可选：上传角色参考图（定义身份/服装/颜色/体型/发型）
- **关键配置**（固定）：
  - 网格：4 列 × 4 行 = 16 帧
  - 长宽比：1:1
  - 背景：默认纯绿（用于自动抠图）
  - 切片参数：`sliceColumns=4`, `sliceRows=4`
  - 不出文字 / 数字 / 网格线 / 水印
- **优先级（多参考图）**：
  1. 用户上传参考图（决定身份/脸/发型/服装/主色）— **最高**
  2. 文本里角色 + 动作描述
  3. 布局参考图（仅影响行列/对齐/切片，**不影响**角色/风格/颜色/动作）
- **提示词写法**：专注"角色 + 动作"；不完整会自动补全一套连贯动作序列
- **输出格式**：单张 spritesheet PNG（4×4 16 帧）+ 自动 AnimationClip
- **坑**：
  - ⚠️ 仅 GUI
  - ⚠️ 固定 4×4 / 16 帧 / 1:1 / 绿幕
  - ⚠️ 角色一致性靠参考图，上传参考图能大幅减少跨帧漂移
  - ⚠️ 布局参考图只影响布局，别指望它影响角色外观

**与 §8 区别**：

| 维度 | 精灵表单列帧（§9） | 2D 动作序列帧（§8） |
|---|---|---|
| 入口 | GUI 精灵表单窗口 | `generate_sprite_sequence` CLI |
| 模型 | Frontier Game Design | sprite_sequence_v1 |
| 输出 | 单张 4×4 spritesheet → 自动切+AnimationClip | 逐帧 PNG + AnimationClip |
| 适用 | 想要精灵表单、一键导动画 | 从参考图生成特定动作 |

---

## 10. 音频 BGM (Huoshan Music)

- **能做什么**：生成背景音乐 / 环境氛围 / 循环配乐，30–120 秒 WAV，自动 AudioClip。
- **入口**：
  - 菜单：`AI > Generate > Generate Audio`
  - MCP 工具：`generate_audio_clip`（BGM 用这个；SFX 用 `generate_sound_effect`）
- **关键参数**：
  - `prompt`：风格/氛围描述（必填）
  - `duration`（秒，30–120，默认 60）
  - `enable_input_rewrite`（默认 true，让 AI 帮你润色 prompt）
  - `play_on_awake`（BGM 默认 true，进 Play Mode 自动播）
- **提示词写法**：
  - "epic orchestral battle theme with heavy drums and brass" 比 "battle music" 好
  - 指定乐器："piano and strings, melancholic" / "electronic synth with deep bass"
  - 开 `enable_input_rewrite` 一般效果更佳
- **输出格式**：WAV → AudioClip → `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ BGM ≠ SFX，别混；BGM 用 `generate_audio_clip`（循环），SFX 用 `generate_sound_effect`（一次性短）
  - ⚠️ AudioSource 默认 `loop: true` + `play_on_awake: true`
  - ⚠️ 耗时 60–180 秒

---

## 11. 音频 SFX (SFX AI)

- **能做什么**：生成一次性短音效（1–22 秒，多格式）。
- **入口**：
  - 菜单：同 BGM（菜单里切换模型）
  - MCP 工具：`generate_sound_effect`
- **关键参数**：
  - `prompt`：**⚠️ 必须英文**（中文 prompt 会出错）
  - `duration_seconds`（1–22，默认 5）
  - `prompt_influence`（0–1，默认 0.5）
  - `output_format`：服务器默认（默认）/ mp3_44100_128 / mp3_44100_192 / pcm_44100 / opus_48000_128
  - `loop`（默认 false = 一次性）
  - `play_on_awake`（默认 false）
- **提示词写法**：
  - 描述细节："sharp wooden door knock, three rapid knocks" 比 "door knock" 好
  - 含材质+动作："heavy metal sword slash through air, whoosh sound"
  - `prompt_influence`：0.3–0.5 自由发挥，0.7–1.0 严格遵守
- **输出格式**：MP3/PCM/Opus → AudioClip → `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ prompt **必须英文**！用户中文时需要先翻译
  - ⚠️ 耗时 10–60 秒

---

## 12. TTS 语音合成 (MiniMax TTS)

- **能做什么**：从文本生成语音（普通话 / 英语 / 日语），MP3 → AudioClip。
- **入口**：
  - 菜单：`AI > Generate > Generate Audio` → 在打开的 TJGenerators Audio 窗口 → 切 Model 到 *MiniMax Text-to-Speech*
  - MCP 工具：`generate_tts`（`generator_id: minimax-tts`）
- **关键参数**：
  - `prompt`（文本，**必填**）
  - `voice_id`（详见下表，支持自定义）
  - `output_path`（自动生成，`.mp3` 自动追加）
  - `play_on_awake`（默认 false）
- **预设 voice_id**：

| voice_id | 描述 |
|---|---|
| `Chinese (Mandarin)_Gentleman` | 中文男 - 绅士（默认） |
| `Chinese (Mandarin)_Humorous_Elder` | 中文男 - 幽默长者 |
| `Chinese (Mandarin)_Cute_Spirit` | 中文女 - 萌系少女 |
| `Chinese (Mandarin)_Warm_Bestie` | 中文女 - 暖心闺蜜 |
| `English_WiseScholar` | 英文男 - 睿智学者 |
| `English_captivating_female1` | 英文女 - 迷人女声 |
| `Japanese_LoyalKnight` | 日文男 - 忠诚骑士 |
| `moss_audio_f0c5494c-7c25-11f0-8d70-a2abf1fbea61` | 日文女 |

`voice_id` 支持自定义值（`allowCustom`），可以填 MiniMax 提供的其它 ID。

- **提示词写法**：写**待合成的文本**本身，不是描述词；中→中文 voice，英文→英文 voice
- **优化**：逗号句号控制停顿；长台词拆分多个独立生成
- **输出格式**：MP3 → AudioClip → `Assets/TJGenerators/History/`
- **坑**：
  - ⚠️ 文本与 voice 语言必须匹配，否则发音异常
  - ⚠️ 耗时 10–30 秒
  - ⚠️ 输出是 AudioClip，场景中挂 AudioSource 当作音效播
  - ⚠️ 一次性台词：`play_on_awake: false`；场景入场叙事：`play_on_awake: true`

---

## 13. 通用图片 Image (Frontier Game Design / SeeDream Image / Frontier Effect)

- **能做什么**：生成概念图 / 参考图 / UI 背景 / 海报 / Banner 等。PNG → 自动 Texture2D (Default)。
- **入口**：
  - 菜单：`AI > Generate > Generate Image`
  - MCP 工具：`generate_image`
- **三模型**：

### 模型 A：Frontier Game Design（默认）
- `generator_id: frontier-game-design`
- 用途：游戏风图片 / 概念图 / 含游戏 prompt 模板
- 参数：
  - `prompt`（**永远必填**）/ `image_path`
  - `imageSize`：square_hd（默认）/ square / portrait_4_3 / portrait_16_9 / landscape_4_3 / landscape_16_9
  - `outputFormat`：png（默认）/ jpeg
  - `prompt_template`：`game_icon`（游戏图标）/ `concept_art`（概念图）/ ""（默认无模板）

### 模型 B：Huoshan SeeDream
- `generator_id: huoshan_seedream_image`（用户说 "use Huoshan model" / "use SeeDream"）
- 用途：通用 T2I / I2I，支持像素级尺寸 + 自动抠图
- 参数：
  - `prompt` / `image_path`
  - `size`：默认 2048×2048，15 种预设（含 4K）
  - `is_segmentation`：自动抠图（**默认 true**；要背景/纹理传 false）
  - `q_value`：抠图压缩 1–100（默认 75）
  - `resize_width`：输出最大宽度（px），0 = 不缩

### 模型 C：Frontier Effect
- `generator_id: frontier-effect`
- 用途：对参考图做风格化效果 + 严格比例控制
- 参数：
  - `prompt` / `image_path`
  - `resolution`：0.5K / 1K（默认）/ 2K / 4K
  - `aspect_ratio`：auto（默认）/ 16:9 / 9:16 / 1:1 / 4:3 / 3:4 / 3:2 / 2:3 / 5:4 / 4:5 / 21:9
  - `output_format`：png（默认）/ jpeg

- **输出尺寸（SeeDream 2K 8 种 + 4K 7 种）**：
  - 2K: 2048×2048 (默认), 2304×1728, 1728×2304, 2560×1440, 1440×2560, 2496×1664, 1664×2496, 3024×1296
  - 4K: 4096×4096, 4704×3520, 3520×4704, 5504×3040, 3040×5504, 4992×3328, 3328×4992
- **提示词写法**：
  - "a fantasy castle on a cliff overlooking a stormy sea, dramatic lighting" 比 "a castle" 好
  - 指定风格："oil painting style" / "photorealistic" / "anime style"
  - Frontier Game Design 可用 `prompt_template` 自动加游戏风格前缀
  - SeeDream 支持中文 prompt，但**英文更稳**
- **输出格式**：PNG → Texture2D (Default) → `Assets/TJGenerators/History/`，**默认不放场景，按路径引用**
- **坑**：
  - ⚠️ `prompt` 永远必填，即使给了 `image_path`
  - ⚠️ SeeDream 最小尺寸 ≈1920×1920，再小会 400
  - ⚠️ 图像 ≠ 精灵：图像是 Texture2D（概念/背景/海报）；精灵是 Sprite（游戏图标/道具）
  - ⚠️ 默认不放场景，除非用户明确要求
  - ⚠️ 耗时 30–90 秒
  - ⚠️ 最多 5 并发

**模型选择速查**：

| 场景 | 选 |
|---|---|
| 通用图 / 概念 / 游戏图标（默认） | Frontier Game Design |
| 游戏特定模板（game_icon / concept_art） | Frontier Game Design + `prompt_template` |
| 需要像素级尺寸 / 自动抠图 | SeeDream |
| 要对参考图做风格化 / 特定比例 | Frontier Effect |
| 需要 JPEG 输出 | Frontier Effect 或 Game Design |

---

## 14. 视频 Video (Huoshan SeeDance)

- **能做什么**：生成概念视频 / 转场 / 宣传片 / 过场动画，30–120 秒完成，自动 VideoClip。
- **入口**：
  - 菜单：`AI > Generate > Generate Video`
  - MCP 工具：`generate_video`（`generator_id: huoshan-seedance`，仅此一个）
- **三种模式**：

| 模式 | 参数 | 描述 |
|---|---|---|
| Text-to-Video | `prompt + mode: "text_to_video"` | 纯文本 |
| Image-to-Video | `prompt + image_path`（mode 默认 `reference_image`） | 参考图 + 文本 |
| Scene-to-Video | 场景截图作为 `image_path` + `prompt` | 当前场景过渡/宣传片 |

- **关键参数**：
  - `prompt`（**必填 + 必须英文**，中文 prompt 会出错）
  - `mode`：`reference_image`（默认）/ `text_to_video`（纯文本时显式传）
  - `image_path`：参考图（提供时自动切 `reference_image`）
  - `resolution`：`"480p"` (854×480) / `"720p"` (1280×720，默认)
  - `ratio`：`"16:9"`（默认） / `"9:16"` / `"1:1"`
  - `duration`（整数 3–15 秒，默认 12）
  - `return_last_frame`（默认 true，返末帧作预览图）
- **提示词写法**（必须英文）：
  - 描述动态变化："a majestic dragon flying over a medieval castle, cinematic lighting, 4K quality"
  - 指定运镜："slow zoom into..." / "panning shot of..." / "aerial view of..."
  - 加视觉细节："cinematic lighting" / "dramatic shadows" / "volumetric fog"
- **输出格式**：视频文件 → VideoClip → **`Assets/Video/`**（**注意：与其他工具不同，不是 `Assets/TJGenerators/History/`**）
- **坑**：
  - ⚠️ prompt **必须英文**！中文翻译先
  - ⚠️ 耗时 30–120 秒
  - ⚠️ 时长限制 3–15 秒，不适合长视频
  - ⚠️ **最高 720p**，高分需求换工具链
  - ⚠️ Scene-to-Video 比例必须和场景截图一致
  - ⚠️ 完成后检查 VideoPlayer 的 source/clip/loop/play_on_awake 配置

---

## 附录 A：13 工具总览表

| # | 工具 | 入口（菜单） | MCP 工具（CLI） | 输入 | 输出 |
|---|---|---|---|---|---|
| 1 | 3D 模型 | `AI > Generate > Generate 3D Model` | `generate_3d_model_by_tripo_p1` / `generate_3d_model_by_tencent_generation` | prompt / image_path / multiview_image_paths | GLB 或 FBX → Prefab |
| 2 | 人形动画角色 | `AI > Generate > Generate 3D Model`（描述人形） | `generate_animated_character` | prompt + action_id | 4 FBX + Animator + Controller → Prefab |
| 3 | 模型绑定 + 动画 | 通过 MCP 调用 | `generate_rigged_model` / `generate_model_motion` / `generate_rigged_animated_model` | 已有模型 | Rigged + Animation FBX |
| 4 | 地形 Terrain | 对话框描述地形 | `generate_terrain` + `apply_terrain_heightmap` | prompt / image_path | Terrain GameObject + TerrainData + PNG |
| 5 | 天空盒 Skybox | 对话框描述天空 | `generate_skybox` | prompt / image_path | PNG → Cubemap → .mat |
| 6 | 2D 精灵 Sprite | `AI > Generate > Generate Sprite` | `generate_sprite` | prompt / image_path (+ generator_id) | PNG → Sprite (Texture2D) |
| 7 | 表面材质 Material | `AI > Generate > Generate Surface Material` | `generate_material` | prompt / image_path / preset_id / pattern_id / style_id | 无缝 PNG + .mat |
| 8 | 2D 动作序列帧 | `AI > Generate > 2D Action Sequence` | `generate_sprite_sequence` | image_path + animation_type + fps | 多 PNG 帧 + AnimationClip |
| 9 | 2D 精灵表单列帧 | `AI > Generate > 2D Spritesheet Sequence`（GUI 唯一） | 无独立 CLI | prompt + 参考图 | 4×4 PNG + AnimationClip |
| 10 | 音频 BGM | `AI > Generate > Generate Audio` | `generate_audio_clip` | prompt + duration | WAV → AudioClip |
| 11 | 音频 SFX | 同 BGM（切模型） | `generate_sound_effect` | prompt（英文）+ duration | MP3/PCM/Opus → AudioClip |
| 12 | TTS 语音合成 | 同音频窗口（切到 MiniMax TTS） | `generate_tts` | prompt（文本）+ voice_id | MP3 → AudioClip |
| 13 | 通用图片 Image | `AI > Generate > Generate Image` | `generate_image` | prompt / image_path (+ generator_id) | PNG → Texture2D |
| 14 | 视频 Video | `AI > Generate > Generate Video` | `generate_video` | prompt（英文）+ image_path / mode / resolution / ratio / duration | 视频 → VideoClip |

> 表中 13 工具 = 1–13；从 #3 模型绑定开始为游戏既有资产加工；#14 视频官方作为补充章节。  
> 资产库搜索（菜单 `AI > Search Generated Assets`）未计入"工具"。

---

## 附录 B：MCP 工具名清单 + 配置命令

### Cowork MCP 配置

URL：`https://ai-generator.tuanjie.cn/mcp`

命令行：
```bash
codely mcp add <name> <url>
# 示例
codely mcp add tjlocal https://ai-generator.tuanjie.cn/mcp
```

> 仅 Cowork + Codely CLI 下可用。**不建议与 Editor 内的 Generator Extension 同时启用**（模型判断可能冲突）。

### MCP 暴露的工具名（按官方 MCP 页面列出）

| 工具名 | 功能 |
|---|---|
| `generate_image` | 文/图生图 |
| `generate_3d_model` | 文/图/多视图生 3D 模型 |
| `generate_video` | 文/图/场景生视频 |
| `generate_skybox` | 文/图生天空盒 |
| `generate_sound_effect` | 文生 SFX |
| `generate_music` | 文生 BGM（官方 MCP 页面用 `generate_music`，与 §10 的 `generate_audio_clip` 对应同一类；具体实现差异以平台为准） |
| `generate_material` | 文/图/预设/模板/风格 → PBR 材质 |

> 此外，**Editor TJGenerators 内部还提供**（通过 Codely Bridge / Agent CLI，非 MCP）：
> `generate_animated_character`（人形角色）、`generate_terrain` + `apply_terrain_heightmap`（地形）、`generate_sprite_sequence`（2D 序列帧）、`generate_tts`（语音）、`generate_3d_model_by_tripo_p1` / `generate_3d_model_by_tencent_generation`（按模型分通道）、`execute_csharp_script`（运行 C# 用，避免 Domain Reload）。

### MCP 用法范式（直接告诉 Agent 即可）

```
"Use generate_image to draw a fantasy castle at sunset"
"Use generate_3d_model to create a low-poly wooden crate, FBX format"
"Use generate_video to make a 15s trailer from this scene screenshot"
```

可附参数：`"add `resolution: 2K`"`、`"add `ratio: 9:16`"`、`"add `size: 4096x4096`"` 等。

---

## 附录 C：可溯源提示（哪些步骤能留痕）

下游核对 / 复现 / 审计时，下面的痕迹会自动写盘，可直接取用：

| 步骤 | 留痕位置 |
|---|---|
| 所有生成产物的源文件 | `Assets/TJGenerators/History/`（视频特例：`Assets/Video/`）|
| 生成的 Prefab 及其 GUID | 同上目录，可看 .meta |
| `AnimationClip` | 序列帧目录（`.anim` 文本格式，可读）|
| `TerrainData` + heightmap PNG | `Assets/TJGenerators/History/` |
| `.mat`（材质球） | 同上 |
| 精灵表单（4×4 拼图） | 同上 |
| 编辑器 Console | 进度日志（生成 100% ≠ 完成，要等结束）|
| `force_overwrite=true` | 中断后恢复痕迹在同路径覆盖 |
| 历史记录面板 | 每个生成窗口右侧 history 区 → 可复用 |
| Agent 对话 / Copilot 输出 | MCP 调用时的完整 prompt 与参数（建议在外部笔记中存一份）|
| **TJGenerators 官方包内** `animations.json` | 676 个动作 ID 的真值表 |
| `force_overwrite` 触发记录 | 重生成前旧 Prefab 名 + 新覆盖 |

**最佳实践**：
1. 把每次生成用的 `prompt`、`image_path`、`generator_id`、`关键参数` 一并存到外部 commit message / Notion / 周报；
2. 优先用 `Assets/TJGenerators/History/` 默认路径，**别自定义**（除非用户明确要求）；
3. 中断后恢复必传 `force_overwrite=true`；
4. 跑长任务期间**不要写 .cs 文件到磁盘**（触发 Domain Reload），用 `execute_csharp_script` 临时执行。

---

## 附录 D：抓取索引

| # | 路径 | 状态 | 抓取长度 |
|---|---|---|---|
| 1 | /en/ai-generation-tools/intro | ✅ 完整 | 5694 字符 |
| 2 | /en/ai-generation-tools/quick-start | ✅ 完整 | 4500 字符 |
| 3 | /en/ai-generation-tools/play-guide | ⚠️ 截断在 "Generate 2D Action Sequence Frames" | 6503 字符 |
| 4 | /en/ai-generation-tools/3d-models | ✅ 完整 | 5559 字符 |
| 5 | /en/ai-generation-tools/animated-character | ✅ 完整 | 6388 字符 |
| 6 | /en/ai-generation-tools/rigged-animated-model | ⚠️ 内容薄（仅决策树 + 工具名） | 1315 字符 |
| 7 | /en/ai-generation-tools/terrain | ✅ 完整 | 5195 字符 |
| 8 | /en/ai-generation-tools/skybox | ✅ 完整 | 3487 字符 |
| 9 | /en/ai-generation-tools/2d-models | ✅ 完整 | 6726 字符 |
| 10 | /en/ai-generation-tools/surface-material | ✅ 完整 | 5647 字符 |
| 11 | /en/ai-generation-tools/2d-animation | ✅ 完整 | 5088 字符 |
| 12 | /en/ai-generation-tools/sequence-frames | ✅ 完整 | 5038 字符 |
| 13 | /en/ai-generation-tools/audio | ✅ 完整 | 4773 字符 |
| 14 | /en/ai-generation-tools/tts | ✅ 完整 | 4725 字符 |
| 15 | /en/ai-generation-tools/images | ✅ 完整 | 6465 字符 |
| 16 | /en/ai-generation-tools/video | ✅ 完整 | 4460 字符 |
| 17 | /en/ai-generation-tools/q-and-a | ✅ 完整 | 2837 字符 |
| 18 | /en/learn/cowork-generator-mcp | ✅ 完整（MCP 配置 + 7 个工具名） | 3343 字符 |
| 19 | /en/tutorials/codely-game-dev-beginner | ⚠️ 内容极薄（只显示了页面骨架/导航） | 1413 字符 |

**抓取覆盖：19 个目标页（含 q-and-a 和教程）  
完整抓到：16 个；内容偏薄但有结构：3 个（rigged-animated-model / play-guide 截断部分 / 教程页）  
未抓到：0**

> 中文站（`/zh/`）对应路径尝试：play-guide 显示"找不到页面"（中文站这个路径被替换为 `/en/` 共享），其余工具页中文版未单独尝试（与英文内容一致，只是语言切换）。
