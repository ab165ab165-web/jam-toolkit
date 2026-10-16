#!/usr/bin/env bash
# build.sh — Unity / 团结引擎 命令行构建模板
#
# 用法:
#   ./build.sh                       # 默认: Win64 Standalone
#   ./build.sh linux                 # Linux Standalone
#   ./build.sh mac                   # macOS Standalone
#   ./build.sh android               # Android (apk)
#   ./build.sh webgl                  # WebGL
#   ./build.sh win -c Release        # Win64 Release
#   ./build.sh win -o ./dist/win     # 自定义输出目录
#
# 变量 (可被环境变量覆盖):
#   UNITY_EDITOR       团结/Unity 编辑器可执行路径
#   PROJECT_PATH       Unity 工程根
#   BUILD_TARGET       Win64/Linux/macOS/Android/WebGL
#   OUTPUT_PATH        输出目录
#   BUILD_CONFIG       Debug / Release
#   LOG_PATH           日志路径
#
# 示例:
#   UNITY_EDITOR=<你的团结编辑器路径> \
#   PROJECT_PATH=./Builds/MyGame \
#   ./build.sh linux

set -euo pipefail

# ---------- 默认值 ----------
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
: "${PROJECT_PATH:=$SCRIPT_DIR}"
: "${UNITY_EDITOR:=<set-me>}"
: "${BUILD_TARGET:=win}"
: "${OUTPUT_PATH:=$SCRIPT_DIR/dist/$BUILD_TARGET}"
: "${BUILD_CONFIG:=Debug}"
: "${LOG_PATH:=$SCRIPT_DIR/build.log}"

# ---------- 平台映射 ----------
declare -A TARGET_TO_FLAG=(
  [win]="StandaloneWindows64"
  [win32]="StandaloneWindows"
  [linux]="StandaloneLinux64"
  [mac]="StandaloneOSX"
  [android]="Android"
  [webgl]="WebGL"
  [ios]="iOS"
)
declare -A TARGET_TO_EXT=(
  [win]=".exe"
  [win32]=".exe"
  [linux]=""
  [mac]=""
  [android]=".apk"
  [webgl]=""  # 输出整个 folder
  [ios]=""
)

# ---------- 参数解析 ----------
while [[ $# -gt 0 ]]; do
  case "$1" in
    win|win32|linux|mac|android|webgl|ios)
      BUILD_TARGET="$1"; shift ;;
    -c|--config)
      BUILD_CONFIG="$2"; shift 2 ;;
    -o|--output)
      OUTPUT_PATH="$2"; shift 2 ;;
    -p|--project)
      PROJECT_PATH="$2"; shift 2 ;;
    -u|--unity)
      UNITY_EDITOR="$2"; shift 2 ;;
    -h|--help)
      sed -n '2,28p' "$0"
      exit 0 ;;
    *)
      echo "Unknown arg: $1"; exit 2 ;;
  esac
done

TARGET_FLAG="${TARGET_TO_FLAG[$BUILD_TARGET]:-}"
TARGET_EXT="${TARGET_TO_EXT[$BUILD_TARGET]:-}"
if [[ -z "$TARGET_FLAG" ]]; then
  echo "Unknown target: $BUILD_TARGET"
  exit 2
fi

mkdir -p "$OUTPUT_PATH"

# ---------- 拼接产物名 ----------
PRODUCT_NAME="JamGame"
ARTIFACT="$OUTPUT_PATH/$PRODUCT_NAME$TARGET_EXT"

# ---------- 前置检查 ----------
if [[ "$UNITY_EDITOR" == "<set-me>" || -z "$UNITY_EDITOR" ]]; then
  echo "❌ UNITY_EDITOR 未设置"
  echo "   请在调用前显式设置，例如："
  echo "     UNITY_EDITOR=/path/to/Tuanjie ./build.sh linux"
  echo "   （Tuanjie Hub 一般在 <hub>/Editor/<版本>/Editor/Tuanjie）"
  exit 1
fi
if [[ ! -x "$UNITY_EDITOR" ]]; then
  echo "❌ UNITY_EDITOR not executable at: $UNITY_EDITOR"
  echo "   Set UNITY_EDITOR=/path/to/Tuanjie (or Unity)"
  exit 1
fi
if [[ ! -d "$PROJECT_PATH/Assets" ]]; then
  echo "❌ PROJECT_PATH missing Assets/: $PROJECT_PATH"
  exit 1
fi

# ---------- 执行构建 ----------
echo "══════════════════════════════════════════════"
echo " Build target : $BUILD_TARGET ($TARGET_FLAG)"
echo " Config       : $BUILD_CONFIG"
echo " Project      : $PROJECT_PATH"
echo " Output       : $ARTIFACT"
echo " Unity        : $UNITY_EDITOR"
echo " Log          : $LOG_PATH"
echo "══════════════════════════════════════════════"

set +e
"$UNITY_EDITOR" \
  -batchmode \
  -nographics \
  -quit \
  -projectPath "$PROJECT_PATH" \
  -buildTarget "$TARGET_FLAG" \
  -executeMethod JamKit.Editor.BuildPipelineHelper.BuildFromCLI \
  -logFile "$LOG_PATH" \
  -customArg:outputPath="$ARTIFACT" \
  -customArg:buildConfig="$BUILD_CONFIG"
EXIT=$?
set -e

if [[ $EXIT -ne 0 ]]; then
  echo "❌ Build failed (exit $EXIT). Tail of log:"
  tail -50 "$LOG_PATH" || true
  exit $EXIT
fi
echo "✅ Build OK → $ARTIFACT"
