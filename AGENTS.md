# AGENTS.md - Oracle DUMP Viewer

## Project Overview

Oracle DUMP Viewer は、Oracle データベースのEXPORT形式（DUMPファイル）を解析し、スキーマ・テーブル・データを視覚的に確認できるWinFormsアプリケーションです。

**主な特徴:**
- Oracle DUMPファイルの解析と表示
- ライセンス認証機能（RSA署名検証）
- 大量データへの対応（メモリ効率的なページング表示）
- 列名による高速検索機能

## Architecture & Data Flow

### データ構造

すべてのテーブルデータは以下の階層構造で保持されます：

```csharp
Dictionary(Of String, Dictionary(Of String, List(Of Dictionary(Of String, Object))))
    ├─ Key: スキーマ名 (String)
    │   └─ Key: テーブル名 (String)
    │       └─ List: 各行のデータ
    │           └─ Key: 列名 (String)
    │               └─ Value: セルの値 (Object)
```

**利点:**
- スキーマ単位でのデータ管理
- 動的なテーブル構造への対応
- 任意のデータ型をサポート（型安全）

### データフロー

```
1. DUMPファイル選択
   ↓
2. AnalyzeLogic.AnalyzeDumpFile() 呼び出し
   ├─ [DEBUG] → TestDataGenerator.GenerateTestData()
   └─ [RELEASE] → 本番用解析ロジック（未実装）
   ↓
3. Workspace フォーム
   ├─ TreeView: スキーマ一覧表示
   ├─ ListView: 選択したスキーマのテーブル一覧
   └─ イベント: テーブルダブルクリック
   ↓
4. TablePreview フォーム
   ├─ DataGridView: ページング表示
   ├─ 検索機能: 列名で絞り込み
   └─ ナビゲーション: 前へ/次へ
```

## Project Structure

```
Oracle DUMP Viewer/
├─ HMI/
│  ├─ Oracle DUMP Viewer/
│  │  ├─ Oracle_DUMP_Viewer.vb          # メインフォーム
│  │  └─ Oracle_DUMP_Viewer.Designer.vb
│  ├─ Workspace/
│  │  ├─ Workspace.vb                   # DBスキーマ表示フォーム
│  │  └─ Workspace.Designer.vb
│  └─ TablePreview/
│     ├─ TablePreview.vb                # テーブルデータ表示フォーム
│     └─ TablePreview.Designer.vb
├─ Logics/
│  ├─ COMMON.vb                         # 共通ユーティリティ
│  ├─ LICENSE.vb                        # ライセンス検証
│  ├─ Oracle DUMP Viewer/
│  │  └─ MenuStripLogics.vb             # メニュー処理
│  ├─ Workspace/
│  │  └─ AnalyzeLogic.vb                # DUMPファイル解析（本番用）
│  ├─ TablePreview/
│  │  └─ TablePreviewLogic.vb           # テーブル表示制御ロジック
│  └─ TestData/
│     └─ TestDataGenerator.vb           # テスト用データ生成
```

## Development Mode vs Release Mode

### DEBUG ビルド

- **テストデータ自動使用**: `TestDataGenerator.GenerateTestData()` を呼び出し
- **本番ロジックを迂回**: `AnalyzeLogic.AnalyzeDumpFile()` は呼び出されない
- **利用場面**: 開発・テスト中のUI動作確認

```visualbasic
#If DEBUG Then
    _allTableData = TestDataGenerator.GenerateTestData()
#Else
    _allTableData = AnalyzeLogic.AnalyzeDumpFile(DumpFilePath)
#End If
```

### RELEASE ビルド

- **本番ロジック使用**: `AnalyzeLogic.AnalyzeDumpFile()` でDUMPファイルを解析
- **実際のファイル処理**: Oracle EXPORTフォーマットのパース実装必須

## Data Persistence

### 現在の状態

- **メモリベース**: すべてのデータはメモリ（Dictionary）に保持
- **セッション単位**: アプリケーション起動～終了時のみ保持
- **永続化なし**: ファイルシステムへの保存機能は未実装

### 将来の拡張

必要に応じて以下を検討：
- SQLite/キャッシュDBへの一時保存
- 解析結果のJSON/CSVエクスポート
- キャッシュ機構（頻繁にアクセスするDUMPファイル）

## Performance Considerations

### メモリ効率化

1. **ページング**: `TablePreview` で1ページ100行（設定可能）に制限
   - UI描画負荷軽減
   - 大量データ（数万～数百万行）にも対応

2. **フィルタリング**: In-memory キューリ
   - 検索結果も同じページング機構を適用
   - 結果が多い場合でも応答性を維持

3. **辞書構造の活用**: O(1) キー検索
   - スキーマ名・テーブル名での高速アクセス

### 制限値

- **1ページ最大行数**: 100行（デフォルト、ユーザーが変更可能）
- **予想対応データ**: 数百万行程度

## Code Style & Conventions

### VB.NET コーディング規約

- **命名規則**
  - クラス: PascalCase（`TablePreview`）
  - メソッド: PascalCase（`DisplayTableData`）
  - フィールド: `_camelCase`（プライベート）
  - ローカル変数: camelCase（`tableName`）

- **構文**
  - `End If`, `End Sub` 等を明示的に記述（VB.NETスタイル）
  - 1行80文字以内を推奨
  - `#Region` / `#End Region` で論理的にグループ化

- **コメント**
  - XML Documentation（`'''`）：public メソッド
  - `'` シングルコメント：ロジック説明
  - 複雑な処理には詳細なコメント追加

### エラーハンドリング

すべての public メソッドで try-catch を実装：

```visualbasic
Try
    ' 処理
Catch ex As Exception
    MessageBox.Show($"エラー: {ex.Message}", "エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error)
End Try
```

## Testing

### テスト用データ生成

`TestDataGenerator.vb` で以下のテーブルを生成：

| スキーマ | テーブル | 行数 | 説明 |
|---------|---------|------|------|
| SCHEMA1 | EMP_TABLE | 10 | 従業員データ |
| SCHEMA1 | DEPT_TABLE | 3 | 部署情報 |
| SCHEMA1 | SALARY_TABLE | 10 | 給与情報 |
| SCHEMA2 | USERS_TABLE | 5 | ユーザー情報 |
| SCHEMA2 | PRODUCTS_TABLE | 8 | 商品マスタ |
| SCHEMA2 | ORDERS_TABLE | 8 | 注文データ |

**テスト手順:**
1. Visual Studio でデバッグビルド実行
2. ダンプファイル選択ダイアログで任意のファイルを選択
3. Workspace が開き、テストデータが表示される
4. テーブルをダブルクリックして TablePreview を確認

## License & Authentication

### ライセンス仕様

- **方式**: RSA署名検証
- **ファイル形式**: JSON (`*.lic.json`)
- **保存場所**: `%APPDATA%\OracleDUMPViewer\license.status`
- **検証時期**: 
  - アプリ起動時
  - ライセンス認証時

**実装場所**: `Logics/LICENSE.vb`

## Build & Deployment

### ビルド

```powershell
# デバッグビルド（テストデータ使用）
msbuild "Oracle DUMP Viewer.csproj" /p:Configuration=Debug

# リリースビルド（本番ロジック使用）
msbuild "Oracle DUMP Viewer.csproj" /p:Configuration=Release
```

### 対象フレームワーク

- **.NET**: net10.0-windows7.0
- **言語**: Visual Basic .NET
- **UI**: Windows Forms (WinForms)

## Future Enhancements

1. **DUMPファイル解析実装** (`AnalyzeLogic.vb`)
   - Oracle EXPORTフォーマットのパース
   - テーブル構造の自動抽出

2. **パフォーマンス最適化**
   - ストリーミング解析（大規模ファイル対応）
   - インデックス機構

3. **UI 拡張**
   - データエクスポート（CSV/JSON）
   - テーブル統計情報の表示
   - 行フォーカス機能

4. **キャッシング機構**
   - 解析済みDUMPファイルのキャッシュ
   - 次回起動時の高速ロード

## Contact & Support

ライセンス認証、機能リクエスト、バグレポート等は GitHub Issues で報告してください。
