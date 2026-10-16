# CJK Fonts — Unity / 团结引擎 用法

> **TL;DR**：把 `NotoSansSC-Regular.otf` + `NotoSansSC-Bold.otf` 拖进
> `Assets/Fonts/`，用 TMP（TextMeshPro）导入一次（生成 `.asset` 字体图集），
> 然后在 UI Text/TextMeshPro 组件里直接选这个字体就能渲染。
> `LXGWWenKai-Regular.ttf` 用法相同，但用作 **游戏内美术字**（招牌 / 对话框
> / 「技」之类点睛字），不是主 UI。

## 文件清单（OFL 1.1，免费可商用、可改名打包、可嵌入）

| 文件 | 用途 | 备注 |
| --- | --- | --- |
| `NotoSansSC-Regular.otf` (~8.3 MB) | 主 UI 正文 | 思源黑体简中（Noto CJK 2.004 Subset） |
| `NotoSansSC-Bold.otf` (~8.5 MB) | 标题 / 按钮 | 同上，Bold 字重 |
| `NotoSansSC-LICENSE.txt` | OFL 1.1 全文 | Google / Adobe 官方 |
| `NotoSansSC-HISTORY.txt` | 版本说明 | 随包 HISTORY 节录上游 **Noto Sans CJK 2.003 / 2021-04-08** 发行说明；字体二进制本体 **name-table 2.004**（2.003 上游源 + 后续 2.004 重打包，见 `OS_2` / `name` 表）。**勿把 2.004 当作 2022-01-27 首发**。 |
| `LXGWWenKai-Regular.ttf` (~25.5 MB) | 美术字 / 标题装饰 | 霞鹜文楷 1.522（基于 Klee One）|
| `LXGWWenKai-Medium.ttf` (~25.4 MB) | 「粗体」替代 | LXGW 不发 Bold，用 Medium 替代 |
| `LXGWWenKaiMono-Regular.ttf` (~25.6 MB) | 等宽调试 / 中文密码条 | 等宽宽度，可用于 ASCII 对齐 |
| `LXGWWenKai-OFL.txt` | OFL 1.1 全文 + Reserved Font Name 说明 | LXGW 项目 |
| `LXGWWenKai-History.md` | 版本历史 | 1.522 = 2026-03-17 发布 |

**为什么是 OTF 而非 TTF**：Noto CJK 官方发行只有 `OTF/`、`SubsetOTF/`、
`Variable/` 三种（无 `SubsetTTF/`）。OTF 在 Unity / 团结 / TMP 里原生支持
（CFF 轮廓，导入选项比 TTF 略慢但功能等价）。如果你**真的**需要 TTF，可以从上游 `notofonts/noto-cjk` 仓库的
`Sans/OTC/` 或 `Sans/` 目录下用 `otf2ttf` / fonttools 自转
（不在本 prep 内；上游 Noto CJK 只发布 OTF/OTC，不发布原生 SubsetTTF）。

**为什么不只挑 3500 常用字**：CJK 子集化（3500 常用字 / GB2312）能砍掉
~80% 体积（~1.5MB），但 24h 黑客松里会撞"人名 / 招牌 / 拼音 / 表情符号"
Heck 正确性，反而 debug 时间不值。装全集，启动 TMP 后内存里 ~30 MB，
中端手机端运行无感（Unity 主存吃 600MB+）。

---

## Unity / 团结引擎 接入步骤

### 1. 一次性导入 + 生成 TMP 字体图集

```
1. 把 .otf / .ttf 文件拖进 <unity-project>/Assets/Fonts/CJK/
   （注意：不是当前 prep pack 的 fonts/，是你自己工程的 Assets/Fonts/）
2. Window → TextMeshPro → Import TMP Essential Resources
   （首次使用 TMP 必做，已做可跳）
3. 在 Project 窗口选中 NotoSansSC-Regular.otf
   → Inspector 右下角 Import Settings 选「TextMeshPro → SDF」
   → Material Type: 「SDF」, Padding: 9, Atlas Resolution: 1024×1024
   → Custom Characters List 留空（用全集）
   → Apply
   → Unity 会生成 NotoSansSC-Regular SDF.asset + 1 张 .png atlas
4. 同样处理 NotoSansSC-Bold.otf
6. LXGWWenKai-Regular.ttf / LXGWWenKai-Medium.ttf 同样导入
```

> **小提示**：如果只想在某种游戏用一种中文字体（例如只用 LXGW 文楷），
> 那只导入那 1 个文件，省去生成两个 atlas 的内存。

### 2. 静态图集 vs 动态图集（按需选）

| 模式 | 何时用 | 优缺 |
| --- | --- | --- |
| **静态图集**（默认） | **正式 release / 知道要显示的字** | 一次性烘焙到 1024×1024 PNG，运行期零 GC、临时分配小；缺点：图集写死，新加字要重烘 |
| **动态图集 / Fallback** | **开发期 / 临时显示玩家输入 / 用户名 / 聊天** | TMP 的「**Fallback Font Assets**」机制（`TMP_FontAsset.fallbackFontAssets`）：运行期没在主图集找到的字去 fallback 表里找，再没找到就 dynamic atlas（`multiAtlasTexturesEnabled=true` 或动态 SDF） |

**推荐组合**（2 字体总内存 ~60MB）：
- `NotoSansSC-Regular SDF`（静态 1024²）
- `NotoSansSC-Bold SDF`（静态 1024²）
- `NotoSansSC-Regular SDF` 标作自己的 fallback → 实现 dynamic overflow
- LXGW 同理

### 3. 在脚本 / UGUI / TextMeshPro 组件里使用

```csharp
// UGUI Text：用 TMP_Text.font 字段（不是 Text.font）
using TMPro;
public TextMeshProUGUI titleText;
public void Start() {
    // Resources.Load 路径需要把字体放到 Assets/Resources/Fonts/CJK/
    titleText.font = Resources.Load<TMP_FontAsset>("Fonts/CJK/NotoSansSC-Bold SDF");
    titleText.text = "欢迎来到游戏世界 / Welcome to the Game!";
}
```

```csharp
// 或者更简单：序列化字段，Inspector 拖
[SerializeField] private TMP_FontAsset notoBold;
[SerializeField] private TMP_FontAsset lxgwRegular;
// Inspector 里把生成的 NotoSansSC-Bold SDF.asset 拖到 notoBold
```

### 4. 按需子集化（可选 · 体积敏感时）

如果你的游戏一定要压字体（移动端 2 MB 包体限制）：

```bash
# 用 fonttools / pyftsubset 子集化：
pip install fonttools brotli
pyftsubset NotoSansSC-Regular.otf \
    --unicodes="U+4E00-9FFF,U+3000-303F,U+FF00-FFEF,U+0020-007E" \
    --flavor=none \
    --layout-features='*' \
    --no-hinting \
    --output-file=NotoSansSC-Simplified.otf
# 输出通常 ~2-3 MB（覆盖基本汉字 + ASCII + 全角标点）
```

或者用 Unity Editor 插件 **Font Asset Optimizer**（TMP 官方 / Unity Asset
Store 也有）勾 "Custom Character Set" → 输入所有要显示的字 → 重新生成 atlas。

### 5. Fallback（中日韩三地混排 / 缺字）

```csharp
// 把日文 / 韩文字体 fallback 到 Noto Sans JP / Noto Sans KR（OFL）
// 都加到主字体的 fallbackFontAssets 列表里：
var jpFont = Resources.Load<TMP_FontAsset>("Fonts/CJK/NotoSansJP-Regular SDF");
notoBold.fallbackFontAssets.Add(jpFont);
```

---

## 性能 / 包体 快速对照

| 字体 | 完整 .otf/.ttf 体积 | TMP SDF Atlas 1024² RGBA32 | TMP SDF Atlas 2048² | 备注 |
| --- | --- | --- | --- | --- |
| NotoSansSC Regular | 8.3 MB | ~5 MB runtime texture | ~20 MB | 默认 |
| NotoSansSC Bold | 8.5 MB | ~5 MB runtime texture | ~20 MB | 默认 |
| LXGWWenKai Regular | 25.5 MB | ~5 MB runtime texture | ~20 MB | 美术字 |
| LXGWWenKai Medium | 25.4 MB | ~5 MB runtime texture | ~20 MB | 粗体替代 |

**两条铁律**：
1. **不要混用 SDF 模式**：TMP 有 `SDFAA` / `SDF` / `Bitmap` 三种生成模式，
   同一字体在不同模式下生成的图集**不能互用**。改模式必须重新烘。
2. **不要把整本 OTF 打包进 build 但不生成 SDF**：TMP 在运行时不会
   自动从 .otf 生成 atlas，要么先烘，要么你会看见"□□□"豆腐块。

---

## License 出处

| 字体 | Repo | License | Author |
| --- | --- | --- | --- |
| Noto Sans SC | https://github.com/notofonts/noto-cjk | **OFL 1.1** | Google + Adobe CJK 团队（详见 `NotoSansSC-HISTORY.txt`） |
| LXGW WenKai | https://github.com/lxgw/LxgwWenKai | **OFL 1.1** + Reserved Font Names 限制 | LXGW (https://lxgw.github.io) 基于 Klee One |

OFL 1.1 全文见 `NotoSansSC-LICENSE.txt` 和 `LXGWWenKai-OFL.txt`。
两个 LICENSE 内容一致（OFL 1.1 公版），但 **LXGW 附加 Reserved Font Name
「霞鹜」「霞鶩」「落霞孤鹜」「落霞孤鶩」「LXGW」** —— 你的游戏**不能用
这 5 个字符串作为游戏名 / 字体名**，其它商用 / 改名 / 嵌入都自由。

---

## 24h 黑客松推荐配置（minimal）

```csharp
// 1. 把 NotoSansSC-Regular.otf + NotoSansSC-Bold.otf 拖进 Assets/Fonts/CJK/
// 2. TMP 导入一次 → 选 SDF → Atlas 1024² → Apply（生成 NotoSansSC-* SDF.asset）
// 3. UI 上所有 TextMeshProUGUI 组件 → Font → NotoSansSC-Regular SDF
// 4. 标题 / 按钮文本 → Font → NotoSansSC-Bold SDF
// 5. 需要美术字（招牌、剧情标题）→ LXGW WenKai Regular
//
// 包体影响：+24 MB（Noto 17 MB + LXGW ~25 MB 压缩 ~7 MB）、runtime 内存 +20 MB（atlas）
// 推荐：如果只用 Noto，省掉 LXGW 能省 25 MB 文件体积。
```