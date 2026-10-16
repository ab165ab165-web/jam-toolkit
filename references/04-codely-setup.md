# 04 · Codely 与团结环境：下载 / 装法 / Token

> 现场硬要求：**必须用团结引擎核心开发**；Codely+ 赛道还要求 **Codely 实质性参与**。

## 1. 团结引擎 / 团结 Hub（下载 + 登录）

- **团结 Hub（Tuanjie Hub）** = 管安装团结引擎及组件的工具。**下载**：团结引擎官网 `unity.cn/tuanjie`（或 releases 页 `unity.cn/tuanjie/releases`）。
- **引擎版本**：本机已装 `2022.3.62t16`（团结）+ 国际版 Unity `6000.0.84f1`。
- **登录**：走 **Unity 中国账号 `id.unity.cn`**。登录页有**两个 Tab**：
  - **手机登录**：国家码默认 **+86**，但**可展开国家列表选其它**（非锁死 +86）；
  - **电子邮件登录**：可**完全绕开手机**。
  - → 所以 **+86 不是硬性前提**：能收码的手机或邮箱登录都行。

## 2. Codely（团结内嵌 AI 智能体 + CLI）

- **Codely 是什么**：团结引擎内嵌 AI 智能体——自然语言 → 写码 / 搭场景 / 跑测试 / 修 bug；配套 **Codely CLI**（在 Unity 工程目录内跑，内置 Unity Tools，通过 TCP Socket 直连正在运行的 Unity Editor）。
- **Codely CLI 安装**（前置：Node.js 20+；Linux/macOS 需 curl；推荐 ripgrep）：
  ```bash
  npm i @unity-china/codely-cli
  codely            # 启动
  ```
  Nightly（开发快照，未过完整 QA，需先移除公版）：
  ```bash
  npm remove -g @unity-china/codely-cli
  # 再按 nightly 指示安装
  ```
- **VSCode 扩展**：`UnityChina.codely`（+ `UnityChina.codely-vscode-ide-companion`）。
- **Token**：现场发 **Codely Token**。⚠️ 具体发放/注入方式**以上海站现场口径为准**——官方文档里 Codely CLI 走浏览器登录（`/auth`），`export CODELY_TOKEN="..."` 为本包按常见做法标注、**未在官方文档核到**。Codely+ 赛道提交需 **Token 用量截图**。

## 3. TJGenerators（团结 AI 生成器 / 官方出素材）

13 类能力（3D 模型 / 人形动画角色 / 绑定+动作 / 地形 / 天空盒 / 2D 精灵 / PBR 材质 / 2D 动作序列帧 / 4×4 精灵表 / BGM / SFX / TTS / 图像）+ **视频补充**（第 14 项）——**详见 `05-tuanjie-ai-generator.md`**。

**三种装法（择一）**：
1. **Editor 内**：打开项目 → AI 对话框点 `/` → Manage Extensions → 搜 `TJGenerators` → Install。
2. **Cowork**：`/` → Open Game Project 打开项目（自动装 `cn.tuanjie.codely.bridge`）→ `/` → Manage Extensions 装 TJGenerators。
3. **Package Manager**：`Window > Package Manager` → `+` → *Add package by name* → `cn.tuanjie.ai.generators`。

**入口**：菜单 `AI > Generate`（全部工具）；`AI > Search Generated Assets`（找产物）。

## 4. 捷径：MCP 通道（不依赖 Unity 工程）

```bash
codely mcp add tjlocal https://ai-generator.tuanjie.cn/mcp
```
- 仅 Cowork + Codely CLI 下可用；**不建议**与 Editor 内 Generator Extension 同时启用。
- 暴露工具（告诉 Agent 即用）：`generate_image` / `generate_3d_model` / `generate_video` / `generate_skybox` / `generate_sound_effect` / `generate_music` / `generate_material`。
- 意义：**不开 Unity 工程也能出素材**——队友的 agent 可直接调它。

## 5. 现场第一件事（顺序）

1. 装/登录团结 Hub（`id.unity.cn`，手机或邮箱）。
2. 新建工程（`3D/2D (URP)`，2022.3.62t16）→ **先跑一次 WebGL/PC 空包构建**（排掉着色器/压缩报错）。
3. `codely skills install`（cowork-skills）+ `/skills reload`；或按上装 TJGenerators。
4. 备好的素材/脚本/配置拷进工程（`LitMotion` UPM、Input System、Cinemachine、URP Post Profile、粒子工厂、UI/音频素材）。
