#!/usr/bin/env bash
# setup.sh — 初始化 Unity / 团结 引擎工程仓库
#
# 用途:
#   - git init
#   - 安装/启用 Git LFS（美术+音频）
#   - 写入 .gitignore + .gitattributes
#   - 设置默认 branch = main
#   - 第一次提交（无实际 Unity 工程，仅元数据）
#
# 用法:
#   ./setup.sh                    # 在当前目录初始化
#   ./setup.sh /path/to/project   # 在指定目录初始化

set -euo pipefail

TARGET="${1:-$PWD}"

if [[ ! -d "$TARGET" ]]; then
  echo "❌ Target directory does not exist: $TARGET"
  exit 1
fi

cd "$TARGET"

echo "══════════════════════════════════════════════"
echo " Initializing Unity/团结 project at: $TARGET"
echo "══════════════════════════════════════════════"

# 1. git init
if [[ ! -d .git ]]; then
  git init -b main
  echo "✅ git init (branch=main)"
else
  echo "ℹ️  .git already exists, skipping init"
fi

# 2. Git LFS
if ! command -v git-lfs >/dev/null 2>&1; then
  echo "⚠️  git-lfs not installed. Install: sudo pacman -S git-lfs"
else
  git lfs install --local
  # Pull lfs patterns from .gitattributes if it exists
  if [[ -f .gitattributes ]]; then
    git lfs pull || true
    echo "✅ Git LFS enabled + .gitattributes pulled"
  else
    echo "⚠️  .gitattributes not found, LFS enabled but no patterns"
  fi
fi

# 3. git config (defaults if not set globally)
git config core.autocrlf false
git config core.symlinks true
git config pull.rebase false
git config init.defaultBranch main
git config advice.detachedHead false

# 4. Initial commit (only if no commits exist)
if ! git rev-parse --verify HEAD >/dev/null 2>&1; then
  git add .gitignore .gitattributes 2>/dev/null || true
  if [[ -n "$(git status --porcelain)" ]]; then
    git -c user.name="hackathon-bot" \
        -c user.email="bot@local" \
        commit -m "chore: scaffold Unity/团结 git + LFS (pre-jam)" \
        --quiet
    echo "✅ Initial commit"
  else
    echo "ℹ️  Nothing to commit (working tree clean)"
  fi
else
  echo "ℹ️  HEAD already exists, skipping initial commit"
fi

echo
echo "══════════════════════════════════════════════"
echo " ✅ Setup complete"
echo
echo " Next steps:"
echo "   1. Open 团结 Hub and create project (target = 2022.3.62t16)"
echo "   2. 从本工具包把 scripts/*.cs 拷到 <project>/Assets/Scripts/JamKit/"
echo "   3. 把本工具包 repo/packages-manifest-deps.md 的依赖项"
echo "      合并进 <project>/Packages/manifest.json"
echo "   4. git add . && git commit"
echo "══════════════════════════════════════════════"