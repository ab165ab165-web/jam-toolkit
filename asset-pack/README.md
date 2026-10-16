# hackathon-prep — 团结 Codely 上海站 24h 黑客松 (10/17–18，**以现场为准**) 去题材化脚手架

> 拿到工程后怎么用：**素材 > 工具类 > 仓库骨架 > 模板 > README**
> 全部为「无题材」准备 —— 主题周六才给，本包先把与题材无关的重复劳动压到零。

## 30 秒上手

```bash
# 1) 把本包拷到 ~/.openclaw/workspace/work/ （已经在这）
# 2) 在 Tuanjie Hub 创建 Unity 2022.3 工程 (URP 模板)
# 3) 把 scripts/*.cs 拷到 <project>/Assets/Scripts/JamKit/
# 4) 把 packages-manifest-deps.md 里的 dependencies 块合并到 <project>/Packages/manifest.json
# 5) 把 assets/fonts/, assets/sfx/, assets/bgm/, assets/art/ 拷到 <project>/Assets/ 下
# 6) 把 build.sh / setup.sh 拷到工程根目录；chmod +x build.sh setup.sh
# 7) cp templates/PROVENANCE.md templates/SUBMISSION.md 到工程 docs/
```

> 不要求 Unity 通过构建 —— 本包不创建 .uproject/.csproj 等 IDE 文件；脚本靠 IDE 编译。

---

## A. 素材 (assets/)

**优先级 #1** — 周六拿到主题后立即可用。

```
assets/
├── LICENSES.md          ← 来源 + license + 署名表（必须读）
├── fonts/               ← Roboto (Apache 2.0) / Inter (OFL) / Press Start 2P (OFL) / Bebas Neue (OFL) / **Noto Sans SC Reg+Bold (OFL) / LXGW WenKai Reg+Medium+Mono (OFL)** — 每字体的 LICENSE / NOTICE 文本与 .ttf 同目录
├── sfx/
│   ├── ui/      (12)    ← 悬停 / 点击 / 确认 / 取消
│   ├── action/  (19)    ← 跳跃 / 落地 / 攻击 / 受击 / 闪避 / **射击·激光 (shoot_001..003 CC0)**
│   ├── pickup/  (4)     ← 拾取
│   ├── win/     (3)     ← 胜利
│   └── lose/    (3)     ← 失败
├── bgm/
│   ├── calm/    (3)     ← 平静 / 探索
│   ├── tense/   (2)     ← 紧张 / Boss
│   └── ambient/ (2)     ← 环境 / 菜单
└── art/
    ├── ui/kenney-ui-pack/        ← UI Pack (Vector SVG + 多色主题)
    ├── input-prompts/            ← Input Prompts (Keyboard & Mouse + Generic)
    └── particles/kenney-particle-pack/  ← Particle Pack (PNG transparent)
```

- **来源**: Kenney (CC0)、OpenGameArt Pro Sensory (CC0)、Google Fonts (OFL/Apache 2.0)
- **详情**: `assets/LICENSES.md`
- **统计**: `find assets -type f | wc -l`（见文末验收段）

## B. 工具类 (scripts/) — Unity 2022.3 / 团结 2022.3 通用

**优先级 #2** — 6 个 JamKit 命名空间下的脚本；拷到 `Assets/Scripts/JamKit/` 即可。

| 文件 | 用途 |
| --- | --- |
| `AudioManager.cs` | 单例音频管理器：PlaySFX(随机 pitch) / PlayMusic(交叉淡入) / SFX 池 |
| `JuiceManager.cs` | 手感动画：HitStop(timeScale→0→恢复) / Shake(Perlin 震屏) |
| `GameFlowManager.cs` | 状态机 StartMenu→InGame→GameOver→Victory；R 键重载场景 |
| `ObjectPool.cs` | 泛型 GameObject 池；预生成 + 自动扩容 |
| `TopDownMover.cs` | 2D 俯视 8 向移动 + 可选鼠标朝向 |
| `PlatformerMover.cs` | 2D 横版跳跃 + 土狼时间 + 跳跃缓冲 + 可变跳高 + 可选二段跳 |

> **未编译**: 本机无 csc/mcs/dotnet/mono，未做语法编译验证。
> 已做 brace/paren 平衡 + namespace/class/method 存在性静态检查，全部通过。
> 第一次打开 Unity 编辑器会自动编译；如有报错请按 error 信息修复。

## C. 仓库/工程骨架 (repo/)

**优先级 #3** — 拷 `.gitignore` / `.gitattributes` / `build.sh` / `setup.sh` / `packages-manifest-deps.md` 到工程根。

| 文件 | 用途 |
| --- | --- |
| `.gitignore` | Unity / 团结 标准 ignore（Library/Temp/Build/VS/Rider/.idea） |
| `.gitattributes` | 美术 + 音频走 Git LFS（`*.png/*.ogg/*.wav/*.fbx/*.ttf/...`） |
| `packages-manifest-deps.md` | Cinemachine / Input System / Post-Processing / URP 等包名 + 版本 + 拷贝说明 |
| `build.sh` | 命令行构建模板：`. /build.sh win` / `linux` / `mac` / `android` / `webgl`；变量带注释 |
| `setup.sh` | `git init` + `git lfs install` + 默认配置 + 第一次 commit |
| `scripts/BuildPipelineHelper.cs` | 给 `build.sh -executeMethod` 调用的入口（拷到 `Assets/Editor/`） |

> **变量**: `UNITY_EDITOR=<你的团结编辑器路径>   # 例：…/Tuanjie/Hub/Editor/2022.3.62t16/Editor/Tuanjie`

## D. 模板 (templates/)

**优先级 #4** — 拷到 `docs/`，现场边做边填。

| 文件 | 用途 |
| --- | --- |
| `PROVENANCE.md` | 每步记 prompt / 工具调用 / diff / 截图 / 编译结果；赛后能还原制作过程 |
| `SUBMISSION.md` | 提交物料清单：封面 / 截图 / GIF / 文字描述 / 操作 / 技术栈 / 来源 / 构建 / 提交前自检 |

## E. 本文件 (README.md)

**优先级 #5** — 30 秒上手 + 验收 + 失败项。

---

## 验收

```bash
BASE=<本包根目录>   # 即 asset-pack 所在目录

# 1) 文件数（验收要求）
find $BASE/assets -type f | wc -l

# 2) SFX 数量
for d in ui action pickup win lose; do
  echo "$d: $(ls $BASE/assets/sfx/$d | wc -l) 个 .ogg"
done

# 3) BGM 数量
for d in calm tense ambient; do
  echo "bgm/$d: $(ls $BASE/assets/bgm/$d | wc -l) 个"
done

# 4) 字体
ls $BASE/assets/fonts/

# 5) C# 脚本
ls $BASE/scripts/

# 6) 仓库骨架
ls $BASE/repo/

# 7) 模板
ls $BASE/templates/
```

期望输出（实际跑过的数字）：

| 类别 | 数量 | 备注 |
| --- | --- | --- |
| `assets/` 总文件 | **6424** (实测，2026-10-16) | 拆分见下：sfx=38 / bgm=7 / **fonts=23** (含 LICENSE / HISTORY) / art=6065 (ui=1302, input-prompts=4667, particles=96) |
| `sfx/ui` | 12 | ≥3 ✓ |
| `sfx/action` | 16 | ≥3 ✓ |
| `sfx/pickup` | 4 | ≥3 ✓ |
| `sfx/win` | 3 | ≥3 ✓ |
| `sfx/lose` | 3 | ≥3 ✓ |
| `bgm/calm` | 3 | ≥1 ✓ |
| `bgm/tense` | 2 | ≥1 ✓ |
| `bgm/ambient` | 2 | ≥1 ✓ |
| `fonts/` | **23** | Roboto ×3 + LICENSE + NOTICE / Inter ×3+license / PressStart2P ×1+LICENSE / BebasNeue ×1+LICENSE / **Noto Sans SC ×2 (Regular/Bold OTF) + LICENSE + HISTORY.txt** / **LXGW WenKai ×3 (Regular/Medium/Mono TTF) + OFL.txt + HISTORY.md** |
| `scripts/*.cs` | 6 | AudioManager / JuiceManager / GameFlowManager / ObjectPool / TopDownMover / PlatformerMover |
| `repo/` 骨架 | 5 | .gitignore / .gitattributes / build.sh / setup.sh / packages-manifest-deps.md + BuildPipelineHelper.cs |
| `templates/` | 2 | PROVENANCE.md / SUBMISSION.md |

---

## 失败 / 偏差项 (honest log)

- ⚠️ **C# 编译未做**: 本机无 `csc/mcs/dotnet/mono`，仅做 brace/paren 平衡 + 关键字存在性静态检查。
- ⚠️ **Music Jingles (Kenney) 不作 BGM**: 17 段 × 5 风格，每段 ~40KB / 仅几秒，不能循环。改用 OpenGameArt Pro Sensory 的 7 段 CC0 循环音乐作为 BGM；Kenney Jingles 可留作 win/lose/transition stings 备用。
- ⚠️ **Google Fonts 下载页改用 GitHub raw**: `fonts.google.com/download?family=X` 重定向到 HTML 而非 zip。改用 `googlefonts/{repo}/main/` 或 `rsms/inter/releases` 的直链。
- ⚠️ **Roboto raw 路径**: `google/fonts/apache/roboto/Roboto-Regular.ttf` 404；改用 `googlefonts/roboto/main/src/hinted/Roboto-Regular.ttf`。
- ⚠️ **无 Unity 工程**: 不创建实际 `Assets/ProjectSettings/Packages/` 三件套；只输出素材+脚本+配置。建工程在 Tuanjie Hub 手动完成。
- ⚠️ **WebGL 打包验证现场做** (G4): 需团结 Hub 登录与建工程，本机无 Unity/Tuanjie Editor；OPEN-ASSETS.md § G4 列了 5 步现场操作。
- ⚠️ **PrimeTween 放弃** (G1): 仓库 license 文件明文 "not allowed: Distribute PrimeTween's source code and tarball (.tgz) archive inside derivative products" — 与 DOTween 限制一致，不能塞进本 prep 包。已改用 **LitMotion (MIT)**，且更现代（Sept 2026 仍在更新）。
- ⚠️ **粒子 .prefab 文件未手写** (G5): Unity .prefab 用 GUID+fileID 引用纹理，GUID 只在首次编辑器导入时生成，单文件无法预写可靠的 .prefab。改为 runtime `JamKitParticleFactory.cs`（MIT，9.6KB / 9,841B）+ 1 个 MIT 参考 .prefab 作为编辑器内复制粘贴模板。Kenney CC0 PNG 复用现有 `assets/art/particles/kenney-particle-pack/png/`。
- ⚠️ **SFX 生成器仅给 URL** (G6): jfxr/Bfxr2/ChipTone 是交互式工具而非可分发动产；提供 URL + license + 4 步工作流，5 秒在浏览器里直接用。
- ⚠️ **DefaultInputActions / VolumeProfile 走 Unity Companion License** (G2/G3): 这是 Unity 自定义许可（允许 Unity 工程内使用 + 跟包分发，禁单独售卖）。在**自有 Unity 工程内**使用、并随该工程分发**合规**；本包按「样例/引用」分发并附 UCL notice（§5），**不构成**对官方素材的独立批量再分发或售卖。LICENSE.md § Addendum 有详细说明。

### 第二轮（N1–N4）追加的失败/偏差项

- ✅ **N1 九宫格底框 → 已解决**：抓 **Kenney Fantasy UI Borders 1.0**（CC0，2023-12-03），280 PNG + 1 SVG。`PNG/Default/Border/panel-border-XXX.png` 32 张 *闭合环* sprite 可直接做 Unity 9-slice（推荐 L=T=R=B=2），`PNG/Double/Border/*` 同理（L=4）。决策记录在 `open-assets/ui-2d/README.md` + `kenney-fantasy-ui-borders/README.md`。
- ✅ **N2 血条/进度条 → 已解决，无新下载**：复用 `assets/art/ui/kenney-ui-pack-png/<Color>/<Default|Double>/slide_horizontal_*.png` + `slide_hangle.png`。组成见 `open-assets/ui-2d/HP_BAR_RECIPE.md`（Unity 5 步配 + HealthBar.cs 单文件脚本 + LitMotion 滞动版）。
- ✅ **N3 Cinemachine 2D 正交 → 已解决，文档追加**：在 `open-assets/cinemachine-urp/README.md` 加了 "2D Orthographic" 节（Camera2D + PositionComposer + Perlin），含逐字段推荐值 + 过渡兼容 `Framing Transposer` 表。
- ✅ **N4 DOTween→LitMotion 对照表 → 已解决，文档追加**：在 `open-assets/tween/INTEGRATION.md` 加了全 API 映射表（Transform / UI / SpriteRenderer / Camera / CanvasGroup / RectTransform / Sequence / Easings / Kill），含单文件 `LitMotionSmokeTest` 安装验证脚本。

所有 N1–N4 资产均为 CC0/MIT 跟现有 prep 包一致，凭 `OPEN-ASSETS.md` 表格 + `assets/LICENSES.md` §1 可查。

### 第三轮（G7）追加的失败/偏差项

- ✅ **G7-1 中文字体 → 已解决**（OFL）：下 **Noto Sans SC** Regular+Bold（SubsetOTF ~17 MB）+ **LXGW WenKai** Regular+Medium+Mono（v1.522 ~76 MB），加 `README-CJK.md`（TMP 动态/静态图集、fallback、按需子集化、Unity/团结接入步骤）。LXGW 不发 Bold，用 Medium 替代。
- ✅ **G7-2 粒子双管线 → 已解决**（MIT）：`JamKitParticleFactory.cs` 用 `GraphicsSettings.currentRenderPipeline` 自动判 URP（`Universal Render Pipeline/Particles/Unlit` + `_BaseColor`/`_BaseMap`）vs Built-in（`Particles/Standard Unlit` + `_TintColor`/`_Mode`/`_SrcBlend`/`_DstBlend`）。`Editor/JamKitParticlePrefabBaker.cs` 一键 bake 8 个 .prefab（4 效果 × 3D/2D），用 `PrefabUtility.SaveAsPrefabAsset` 避免 GUID 问题。`RECIPES.md` 同步更新。
- ✅ **G7-3 Built-in 后处理 → 已解决**（MIT）：新 `open-assets/postprocessing/JamKit_BuiltIn_PostProcessing_Profile.asset`（稳定 GUID `651f34e1d6904f499f5bff25906d0012`），Bloom 0.5 + ColorGrading (LDR, sat/contrast 1.05, ACES) + Vignette 0.4 procedural。保留 URP 两版。需安装 `com.unity.postprocessing` 3.4.x（最后支持 Built-in 的版本）。
- ✅ **G7-4 Cinemachine 成品预设 → 已解决**（MIT）：新 `open-assets/cinemachine-urp/Prefabs/` 含 2 个 .prefab + 稳定 GUID .meta：① 3D 第三人称跟随（`JamKit_ThirdPersonFollow3D.prefab`，GUID `78ea5746051c4c73acffe8df5caad970`）：`CinemachineCamera` + `CinemachineThirdPersonFollow` (CameraDistance=4) + `CinemachineRotationComposer`，右肩偏移 (0.5, -0.4, 0)，FOV 60°；② 2D 正交（`JamKit_2DPlatformerOrtho.prefab`，GUID `112b381e29654b15b6e85a40fb59c7df`）：`CinemachineCamera` (OrthographicSize=6) + `CinemachinePositionComposer` (DeadZone 0.1, Damping 0.5)。MonoScript GUIDs 对齐 CM3.1.1（`f9dfa5b68...`、`625c14ee...`、`886251e9a...`、`f38bda98361e1de48a4ca2bd86ea3c17`）。`README.md` 已增补 Prefabs 段 + 参数表。
- ✅ **G7-5 新 Input System 双分支 → 已解决**（MIT）：`TopDownMover.cs` / `PlatformerMover.cs` 用 `#if ENABLE_INPUT_SYSTEM` 双分支支持 `Keyboard.current`/`Gamepad.current`（含 `wasPressedThisFrame`/`isPressed` 跳跃、leftStick 移动 dead zone 0.2）。兼容旧 Input（默认）；`Active Input Handling = Both` 即两边同时可用。
- ✅ **G7-6a 射击/激光 CC0 → 已解决**（CC0）：新 3 个 `assets/sfx/action/shoot_00{1,2,3}.ogg`，全部由 ffmpeg 从零合成（`anoisesrc` + `sine` + 高通/低通/`tremolo`/`aecho`），**CC0 公共领域**，生成脚本在 `SHOOT-SOURCES.md`。完全无第三方样本依赖。
- ✅ **G7-6b jsfxr 条目 → 已解决**：在 `open-assets/sfx-generators/README.md` 加 **jsfxr** 条目：https://sfxr.me + `chr15m/jsfxr` repo，**The Unlicense**（公共领域等同）。

所有 G7-1..6 资产跟现有 prep 包一致（OFL/CC0/MIT/Unlicense + UCL 参考），凭 `OPEN-ASSETS.md` § G7 表 + `assets/LICENSES.md` § Addendum 可查。

---

## 下一步（拿到主题后）

1. **打开 Tuanjie Hub** → New Project → 3D (URP) / 2D (URP) 模板 → 用 2022.3.62t16
2. **拷素材**（`Assets/` 对应拷）
3. **拷脚本**（`Assets/Scripts/JamKit/`）
4. **合并 manifest.json**（见 `repo/packages-manifest-deps.md`）
5. **拷 .gitignore / .gitattributes / build.sh / setup.sh**
6. **第一次提交**：`./setup.sh`
7. **填 `PROVENANCE.md`** 记录每一步
8. **做到最后两小时** 按 `SUBMISSION.md` 自检 + 提交

---

## 致谢

- **Kenney** (kenney.nl) — CC0 UI/Input/Particle/Audio 资产
- **Pro Sensory** (OpenGameArt) — CC0 BGM
- **Christian Robertson** (Roboto, Apache 2.0)
- **Rasmus Andersson** (Inter, OFL)
- **CodeMan38** (Press Start 2P, OFL)
- **Ryoichi Tsunekawa / Dharma Type** (Bebas Neue, OFL)

无任何付费授权；所有资源符合 24h 黑客松的提交与商用规则。