# 提交物料清单 — Submission Checklist

> 比赛结束前 2h 必须全部勾掉。文件路径填好，最终构建/截图/GIF/描述就绪。
>
> **本赛硬要求（与 06-competition-rules §4 对齐）**：
> - 演示视频 **3–5 分钟、必交**，**必须含 AI 辅助开发过程录屏段**（Codely+ 赛道含 Codely 录屏段）
> - 封面 **16:9**，推荐 **1920×1080**
> - **Codely+ 赛道**：额外提交 **Token 用量截图**（详见 13-onsite-ops §2）
> - 5 份材料在同一网盘文件夹，命名 `赛道-类型-队名-作品名`，提取码 `UCDC`，有效期 ≥ 1 年

## 基础信息

- [ ] 项目名:
- [ ] 主题:
- [ ] 小队名:
- [ ] 队长 + 队员名单:
- [ ] **赛道**（Codely+ / AI+）: ⚠️ T+2h 前必确定（详见 07-track-selection）
- [ ] **目标平台**（小游戏 / PC / 移动端）: ⚠️ T+2h 前必确定
- [ ] 比赛平台: 团结 Codely — 官方报名/赛事页 `https://unity.cn/ucdc2026`，编辑器内嵌入口走 `https://codely.tuanjie.cn`；现场提交通道**以主办当日公告为准**。

## 1. 封面 / 截图

- [ ] **封面图** (**1920×1080, 16:9**, PNG/JPG, <1MB): `docs/cover.png`
  - **封面要一句话讲清玩什么**（主角 + 动作 + 输赢信号）
  - 字体用 Bebas Neue 或 Press Start 2P 或 思源黑体
- [ ] **游戏截图 1**: `docs/shot-01.png`  — 主要玩法场景
- [ ] **游戏截图 2**: `docs/shot-02.png`  — 次要玩法/状态
- [ ] **游戏截图 3**: `docs/shot-03.png`  — UI / 菜单
- [ ] **游戏截图 4** (可选): `docs/shot-04.png`

## 2. GIF / 视频

- [ ] **核心循环 GIF** (15-30s, <5MB): `docs/gameplay.gif`
  - 用 ffmpeg / 屏幕录制 → gifski / ffmpeg palette 压缩
  - 展示: 主菜单 → 玩法 → 胜利/失败
- [ ] **演示视频** (**必交, 3–5min, 含 AI 辅助开发录屏段**): `docs/demo.mp4`
  - 1920×1080 / 30fps / H.264 MP4 / 8–12 Mbps / ≤ 500MB
  - **必含 AI 录屏段（30–60s）**：Codely+ 赛道要 Codely 对话+Editor 联动；AI+ 赛道任意 AI 工具
  - 分镜表与脚本见 12-submission-kit §2

## 3. 文字描述

### 一句话介绍（提交表单第一行）
> 一句话讲清：什么游戏 + 怎么玩 + 为什么有意思。

<!-- placeholder: -->

### 详细描述（200-400 字）
- 玩法核心
- 美术风格
- 技术亮点
- 灵感来源

### 操作说明
| 输入 | 动作 |
| --- | --- |
| WASD / 方向键 | 移动 |
| 鼠标左键 | 攻击 |
| 空格 | 跳跃 |
| R | 重开 |
| Esc | 暂停/菜单 |

### 技术栈
- 引擎: 团结引擎 2022.3.62t16 (Unity 2022.3 fork)
- 渲染管线: URP 14.0.9（Unity 2022.3 LTS 默认；UPM 升 ≥ 14.0.9）
- 输入: Input System 1.13.0 / 旧版 Input（`Active Input Handling = Both`）
- 后处理: Post-Processing v2 **3.4.x**（Built-in 最后支持版）/ URP Volume；URP 项目走 `cinemachine-urp/VolumeProfiles/JamKit_URP_PostProcessing_Profile.asset`
- 音频: Unity Audio + 自研 AudioManager (PlaySFX / PlayMusic / SFX 池)
- 其他: Cinemachine 3.1.1（CM3+ API；团结 2022.3 自带 **2.9.7** — 若现场仅能用自带版本，以现场为准；赛包默认走 `packages-manifest-deps.md` 声明的 UPM 升级路径）

### 资源来源
> 完整表格见 `../assets/LICENSES.md`；这里只列实际用到的。

| 素材 | 来源 | License | 路径 |
| --- | --- | --- | --- |
|  |  |  |  |

## 4. 构建产物

- [ ] **Windows 构建**: `dist/win/JamGame.exe`
- [ ] **macOS 构建** (可选): `dist/mac/JamGame.app`
- [ ] **Linux 构建** (可选): `dist/linux/JamGame`
- [ ] **WebGL 构建** (可选, 适合浏览器试玩): `dist/webgl/`
- [ ] **Android APK** (可选): `dist/android/JamGame.apk`

## 5. 演示脚本骨架（拍摄/录制前定稿）

> **3–5 分钟讲解视频话术**；按秒切场景。与 §0「演示视频 3–5 分钟、必交」一致。

```
[0:00-0:30]  开场：项目名 + 主题 + 队名 + 一句话玩法
[0:30-1:00]  玩法介绍（对着封面/截图讲）
[1:00-3:30]  实机演示（约 2.5 min）：
  - 主菜单 → 开始游戏
  - 教学关（10-15s）
  - 普通关（30-60s）
  - 难点 / Boss / 高潮（45-60s）
  - 胜利 / 失败状态
[3:30-4:30]  技术亮点（含 AI 辅助开发录屏段 30-60s — Codely+ 赛道要 Codely 对话+Editor 联动；AI+ 赛道任意 AI 工具）
[4:30-5:00]  收尾：致谢 + 邀请评委试玩
```

## 6. 提交前最后一遍自检

- [ ] 游戏的「教学关」能在 30 秒内教会一个陌生人
- [ ] 主菜单可点击且 Quit 工作
- [ ] 失败/胜利状态有视觉反馈 + 音频
- [ ] R 键能重开（即使没绑 UI 按钮）
- [ ] 全屏分辨率自适配（至少 16:9 不变形）
- [ ] 音量可调（最好有滑条）
- [ ] 构建产物可启动（在自己机器上至少跑过一遍）
- [ ] 所有可见文字字体已打包进工程
- [ ] 所有使用过的 CC0 / OFL 资源已写入 LICENSES.md
- [ ] GitHub README 有项目说明 + 控制截图 + 演示链接

## 提交平台 (团结 Codely)

提交入口: **待现场确认** — 候选 (按现场公告二选一)：
- **官方报名/赛事页**: `https://unity.cn/ucdc2026`（UCDC 2026 上海站官方入口）
- **Codely 后台**: `https://codely.tuanjie.cn`（`我的` → `账户与用量` 附近有提交入口）

> ⚠️ `https://codely.tuanjieengine.com` (旧提交入口) 已停服 / NXDOMAIN — 以官方报名页**最新公告**为准。

- [ ] 登录小队账号
- [ ] 上传构建（或填 WebGL URL）
- [ ] 上传封面 + GIF
- [ ] 填文字描述 + 操作说明
- [ ] 上传 LICENSES.md
- [ ] 上传 PROVENANCE.md（可选，展示认真程度）
- [ ] 点击提交，截图保存「提交成功」页面

## 提交后

- [ ] 在小队群里发提交确认截图
- [ ] 把 GitHub 仓库设为 public，附 README 链接到提交说明
- [ ] 复盘（用 PROVENANCE.md 的「24h 后复盘」段）

## 7. 本赛特别补充（与默认清单差异）

| 项 | 本赛要求 | 与模板默认差异 |
|---|---|---|
| **演示视频** | 3–5min **必交** + 含 AI 录屏段 | 默认「60s 可选」 → 改为「3–5min 必交」 |
| **封面** | 1920×1080 / 16:9 | 默认 1280×720 → 改为 1920×1080（统一 16:9） |
| **AI 工具说明** | 必交（详见 06-competition-rules §4.3） | 模板默认未列 → 新增必填 |
| **AI 工具使用日志** | 必交（详见 06-competition-rules §4.4） | 模板默认未列 → 新增必填 |
| **项目工程截图 / 核心代码片段** | 必交（验证团结引擎使用） | 模板默认未列 → 新增必填 |
| **Codely+ 赛道 Token 用量截图** | 必交（详见 13-onsite-ops §2） | 模板默认未列 → Codely+ 队伍新增必填 |
| **百度网盘提取码** | `UCDC`，**有效期 ≥ 1 年** | 默认未指定 → 补齐 |

## 8. 相关 references 索引（提交当日随身备查）

- `12-submission-kit.md` — 提交模板全集（AI 日志模板 / 视频分镜 / PDF 骨架 / 封面口径）
- `13-onsite-ops.md` — 现场一页清单 + Token 截图 + 录屏 + 应急
- `14-team-and-platform.md` — 2 人分工 + git + 平台/构建/URP 2D/触控
- `06-competition-rules.md` — 比赛规则（提交材料 §4）
- `07-track-selection.md` — 赛道选择（Codely+ vs AI+）