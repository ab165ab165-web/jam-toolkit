---
name: jam-kit-toolkit
description: 24h 游戏 Jam 通用工具包（与题材无关）：预建工程资产（素材/脚本/配置）、24h 设计铁律、团结 Codely 与 TJGenerators 用法、比赛规则/提交/现场/平台。开题后只填「核心机制 + 特定美术」。
---

# 24h 游戏 Jam 工具包（通用 · 可公开）

> 目的：把一切**与具体题材无关的重复劳动**压到零。开题后只填两个变量：**① 核心机制代码　② 特定美术**。
> 引擎约束：**团结引擎**（Unity 中国 fork）；Codely+ 赛道核心模块须 **Codely 实质性参与**。

## 工作守则
1. **一个动词**：机制围绕一个核心动作；循环 **≤2 分钟**且闭环（标题→指引→挑战→胜/负→重开 `R`）。
2. **先灰盒**：前 6h 用几何体跑通，好玩再上美术。
3. **统一 > 精细**：怕的是**风格割裂**，不是极简。
4. **Juice = 上限**：顿帧 1–3 帧 / 震屏 / 粒子 / 即时音效。
5. **同机位只改一处**。
6. **溯源**：每步留 prompt / 工具调用 / diff / 截图 / 运行结果；素材标来源。

## 导航

| 想干嘛 | 看哪 |
|---|---|
| 24h 极限赛怎么定范围、怎么做 | `references/03-jam-rules-24h.md` |
| 团结 Codely / Hub 下载·装法·Token | `references/04-codely-setup.md` |
| 官方 AI 生成器（TJGenerators）13 类工具 | `references/05-tuanjie-ai-generator.md` |
| 比赛规则 / 赛道 / 提交材料 / API 要求 | `references/06-competition-rules.md` |
| 选赛道（Codely+ vs AI+） | `references/07-track-selection.md` |
| 「正常基准线」怎么立 | `references/08-normalcy-baseline.md` |
| 提交物料套件（AI 日志 / 视频分镜 / PDF / 封面） | `references/12-submission-kit.md` |
| 现场执行（checklist / Token 用量 / 录屏 / 断网预案） | `references/13-onsite-ops.md` |
| 团队分工 + git 协作 + 平台/构建 | `references/14-team-and-platform.md` |
| 美术制作管线（多方位图 → 拆件 → Blender → 绑骨） | `references/15-asset-pipeline.md` |

## 随包资产（`asset-pack/`）

去题材化**预建工程资产**，拷进团结工程即可：

| 目录 | 内容 |
|---|---|
| `asset-pack/assets/` | Kenney CC0（UI / 九宫格底框 / 输入图标 / 粒子）、字库（Roboto / Inter / Press Start 2P / Bebas Neue / **Noto Sans SC / LXGW WenKai**）、音效（ui/action/pickup/win/lose）、BGM（calm/tense/ambient） |
| `asset-pack/scripts/` | 6 个通用 C#：`AudioManager` / `JuiceManager` / `GameFlowManager` / `ObjectPool` / `TopDownMover` / `PlatformerMover` |
| `asset-pack/repo/` | `.gitignore` / `.gitattributes` / `build.sh` / `setup.sh` / `packages-manifest-deps.md` |
| `asset-pack/open-assets/` | 补件：**LitMotion**(MIT 补间) / InputSystem 预设 / URP+Built-in 后处理 / Cinemachine 成品 / 粒子工厂(双管线+成品 prefab) |
| `asset-pack/templates/` | `PROVENANCE.md` / `SUBMISSION.md` |

> 素材均 **CC0 / MIT / OFL / Apache / Unity Companion License**（后者仅随 Unity 工程使用）。逐件来源见 `asset-pack/assets/LICENSES.md` 与 `OPEN-ASSETS.md`。
