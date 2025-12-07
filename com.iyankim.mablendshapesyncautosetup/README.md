# Blendshape Sync Auto Setup Tool

---

## 📖 English

### Overview

An automated Unity Editor tool for setting up Modular Avatar's Blendshape Sync component across multiple objects simultaneously. This tool simplifies the process of synchronizing blendshapes between a source mesh and multiple target meshes.

### Features

-   🎯 **Automatic Blendshape Detection**: Automatically finds common blendshapes between source and target meshes
-   📦 **Multi-Target Support**: Configure multiple target objects in a single operation
-   🖱️ **Drag & Drop Interface**: Easy-to-use drag and drop functionality with multi-selection support
-   🌳 **Recursive Child Processing**: Optionally include all child objects with SkinnedMeshRenderer components
-   🌍 **Multi-Language Support**: English, Korean, and Japanese UI languages
-   👁️ **Real-time Preview**: See which blendshapes will be configured before applying
-   ↩️ **Undo Support**: Full Unity Undo system integration

### Requirements

-   Unity 2022.3.22f1 or later
-   Modular Avatar package installed
-   NDMF (Non-Destructive Modular Framework)

### Installation

1. Download the `BlendshapeSyncAutoSetup.cs` file
2. Place it in an `Editor` folder in your Unity project
3. The tool will appear in the menu: `Iyan-Kim > Tools > Modular Avatar Blendshape Sync Auto Setup`

### How to Use

#### Basic Usage

1. Open the tool from `Iyan-Kim > Tools > Modular Avatar Blendshape Sync Auto Setup`

2. Assign a **Source Object** (the mesh with reference blendshapes)

3. Add

    Target Objects

    using one of these methods:

    - Drag and drop GameObjects into the drop area
    - Use the "Add Target Object" button
    - Multi-select objects in Hierarchy and drag them together

4. Enable/disable **Include Child Objects** toggle as needed

5. Review the preview of common blendshapes found

6. Click **Setup Blendshape Sync** button

7. Done! The Modular Avatar Blendshape Sync component is now configured on all targets

#### Include Child Objects Feature

-   **Enabled (Default)**: Automatically includes all child objects with SkinnedMeshRenderer components
-   **Disabled**: Only adds the explicitly selected objects

#### Language Selection

Choose your preferred language from the dropdown at the bottom of the window:

-   English (Default)
-   한국어 (Korean)
-   日本語 (Japanese)

### Tips

-   The tool only processes objects with SkinnedMeshRenderer components
-   Duplicate objects are automatically filtered out
-   You can remove individual targets using the "X" button
-   Use "Clear All" to remove all targets at once
-   The preview shows exactly which blendshapes will be configured for each target

### Troubleshooting

-   **No common blendshapes found**: Ensure both source and target meshes have blendshapes with identical names
-   **Setup button is disabled**: Make sure you have selected both a source object and at least one valid target object
-   **Component not working after setup**: Verify that the source object is within the same avatar hierarchy

---

## 📖 한국어

### 개요

Modular Avatar의 Blendshape Sync 컴포넌트를 여러 오브젝트에 동시에 설정할 수 있는 Unity 에디터 자동화 툴입니다. 소스 메시와 여러 타겟 메시 간의 블렌드쉐이프 동기화 과정을 간소화합니다.

### 기능

-   🎯 **자동 블렌드쉐이프 감지**: 소스와 타겟 메시 간의 공통 블렌드쉐이프를 자동으로 찾습니다
-   📦 **다중 타겟 지원**: 한 번의 작업으로 여러 타겟 오브젝트를 설정할 수 있습니다
-   🖱️ **드래그 앤 드롭 인터페이스**: 다중 선택을 지원하는 사용하기 쉬운 드래그 앤 드롭 기능
-   🌳 **재귀적 하위 처리**: SkinnedMeshRenderer 컴포넌트가 있는 모든 하위 오브젝트를 선택적으로 포함
-   🌍 **다국어 지원**: 영어, 한국어, 일본어 UI 언어 지원
-   👁️ **실시간 미리보기**: 적용 전에 어떤 블렌드쉐이프가 설정될지 확인 가능
-   ↩️ **Undo 지원**: Unity Undo 시스템과 완전히 통합

### 요구 사항

-   Unity 2022.3.22f1 이상
-   Modular Avatar 패키지 설치 필요
-   NDMF (Non-Destructive Modular Framework)

### 설치 방법

1. `BlendshapeSyncAutoSetup.cs` 파일을 다운로드합니다
2. Unity 프로젝트의 `Editor` 폴더에 배치합니다
3. 메뉴에서 툴을 찾을 수 있습니다: `Iyan-Kim > Tools > Modular Avatar Blendshape Sync Auto Setup`

### 사용 방법

#### 기본 사용법

1. `Iyan-Kim > Tools > Modular Avatar Blendshape Sync Auto Setup`에서 툴을 엽니다

2. **소스 오브젝트**를 할당합니다 (참조할 블렌드쉐이프가 있는 메시)

3. 다음 방법 중 하나로

    타겟 오브젝트

    를 추가합니다:

    - 드롭 영역에 GameObject를 드래그 앤 드롭
    - "타겟 오브젝트 추가" 버튼 사용
    - Hierarchy에서 여러 오브젝트를 선택하여 함께 드래그

4. 필요에 따라 **하위 오브젝트 포함** 토글을 활성화/비활성화합니다

5. 발견된 공통 블렌드쉐이프의 미리보기를 확인합니다

6. **블렌드쉐이프 싱크 설정** 버튼을 클릭합니다

7. 완료! 모든 타겟에 Modular Avatar Blendshape Sync 컴포넌트가 설정되었습니다

#### 하위 오브젝트 포함 기능

-   **활성화 (기본값)**: SkinnedMeshRenderer 컴포넌트가 있는 모든 하위 오브젝트를 자동으로 포함합니다
-   **비활성화**: 명시적으로 선택한 오브젝트만 추가합니다

#### 언어 선택

윈도우 하단의 드롭다운에서 원하는 언어를 선택하세요:

-   English (기본값)
-   한국어
-   日本語 (일본어)

### 팁

-   툴은 SkinnedMeshRenderer 컴포넌트가 있는 오브젝트만 처리합니다
-   중복된 오브젝트는 자동으로 필터링됩니다
-   "X" 버튼을 사용하여 개별 타겟을 제거할 수 있습니다
-   "전체 삭제"를 사용하여 모든 타겟을 한 번에 제거할 수 있습니다
-   미리보기는 각 타겟에 대해 정확히 어떤 블렌드쉐이프가 설정될지 보여줍니다

### 문제 해결

-   **공통 블렌드쉐이프를 찾을 수 없음**: 소스와 타겟 메시 모두 동일한 이름의 블렌드쉐이프를 가지고 있는지 확인하세요
-   **설정 버튼이 비활성화됨**: 소스 오브젝트와 최소 하나의 유효한 타겟 오브젝트를 선택했는지 확인하세요
-   **설정 후 컴포넌트가 작동하지 않음**: 소스 오브젝트가 동일한 아바타 계층 구조 내에 있는지 확인하세요

---

## 📖 日本語

### 概要

Modular Avatar の Blendshape Sync コンポーネントを複数のオブジェクトに同時に設定できる Unity エディター自動化ツールです。ソースメッシュと複数のターゲットメッシュ間のブレンドシェイプ同期プロセスを簡素化します。

### 機能

-   🎯 **自動ブレンドシェイプ検出**: ソースとターゲットメッシュ間の共通ブレンドシェイプを自動的に検出
-   📦 **マルチターゲット対応**: 1 回の操作で複数のターゲットオブジェクトを設定可能
-   🖱️ **ドラッグ&ドロップインターフェース**: 複数選択対応の使いやすいドラッグ&ドロップ機能
-   🌳 **再帰的な子処理**: SkinnedMeshRenderer コンポーネントを持つすべての子オブジェクトをオプションで含める
-   🌍 **多言語対応**: 英語、韓国語、日本語の UI 言語サポート
-   👁️ **リアルタイムプレビュー**: 適用前にどのブレンドシェイプが設定されるか確認可能
-   ↩️ **Undo サポート**: Unity Undo システムと完全に統合

### 必要環境

-   Unity 2022.3.22f1 以降
-   Modular Avatar パッケージのインストールが必要
-   NDMF (Non-Destructive Modular Framework)

### インストール方法

1. `BlendshapeSyncAutoSetup.cs`ファイルをダウンロード
2. Unity プロジェクトの`Editor`フォルダに配置
3. メニューからツールを開けます: `Iyan-Kim > Tools > Modular Avatar Blendshape Sync Auto Setup`

### 使用方法

#### 基本的な使い方

1. `Iyan-Kim > Tools > Modular Avatar Blendshape Sync Auto Setup`からツールを開く

2. **ソースオブジェクト**を割り当て（参照するブレンドシェイプを持つメッシュ）

3. 次のいずれかの方法で

    ターゲットオブジェクト

    を追加:

    - ドロップエリアに GameObject をドラッグ&ドロップ
    - "ターゲットを追加"ボタンを使用
    - Hierarchy で複数のオブジェクトを選択して一緒にドラッグ

4. 必要に応じて**子オブジェクトを含む**トグルを有効/無効化

5. 検出された共通ブレンドシェイプのプレビューを確認

6. **ブレンドシェイプ同期を設定**ボタンをクリック

7. 完了！すべてのターゲットに Modular Avatar Blendshape Sync コンポーネントが設定されました

#### 子オブジェクトを含む機能

-   **有効（デフォルト）**: SkinnedMeshRenderer コンポーネントを持つすべての子オブジェクトを自動的に含める
-   **無効**: 明示的に選択されたオブジェクトのみを追加

#### 言語選択

ウィンドウ下部のドロップダウンから好みの言語を選択:

-   English（デフォルト）
-   한국어（韓国語）
-   日本語

### ヒント

-   ツールは SkinnedMeshRenderer コンポーネントを持つオブジェクトのみを処理します
-   重複したオブジェクトは自動的にフィルタリングされます
-   "X"ボタンで個別のターゲットを削除できます
-   "すべてクリア"で全てのターゲットを一度に削除できます
-   プレビューは各ターゲットに対してどのブレンドシェイプが設定されるかを正確に表示します

### トラブルシューティング

-   **共通ブレンドシェイプが見つからない**: ソースとターゲットメッシュの両方が同一名のブレンドシェイプを持っているか確認してください
-   **設定ボタンが無効**: ソースオブジェクトと少なくとも 1 つの有効なターゲットオブジェクトを選択したか確認してください
-   **設定後にコンポーネントが動作しない**: ソースオブジェクトが同じアバター階層内にあるか確認してください

---

## 📝 License

This tool is designed to work with Modular Avatar by bd\_ (https://github.com/bdunderscore/modular-avatar)

## 🤝 Contributing

Issues and pull requests are welcome!

## 📧 Support

If you encounter any problems or have questions, please open an issue on the repository.

---

**Created with ❤️ for the VRChat and Unity community**
