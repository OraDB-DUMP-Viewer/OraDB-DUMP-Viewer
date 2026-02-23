# AGENTS.md - OraDB DUMP Viewer

## Project Overview

OraDB DUMP Viewer は、Oracle データベースのEXPORT形式（DUMPファイル）を解析し、スキーマ・テーブル・データを視覚的に確認できるWinFormsアプリケーションです。

**主な特徴:**
   - OraDB DUMPファイルの解析と表示
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
OraDB DUMP Viewer/
├─ HMI/
│  ├─ OraDB DUMP Viewer/
│  │  ├─ OraDB_DUMP_Viewer.vb          # メインフォーム
│  │  └─ OraDB_DUMP_Viewer.Designer.vb
│  ├─ Workspace/
│  │  ├─ Workspace.vb                   # DBスキーマ表示フォーム
│  │  └─ Workspace.Designer.vb
│  └─ TablePreview/
│     ├─ TablePreview.vb                # テーブルデータ表示フォーム（高度な検索がデフォルト）
│     ├─ TablePreview.Designer.vb       # UI定義
│     ├─ AdvancedSearchForm.vb          # 高度な検索ダイアログ
│     ├─ AdvancedSearchForm.Designer.vb # UI定義（テンプレート含む）
│     ├─ SearchConditionRow.vb          # 検索条件行コンポーネント
│     └─ SearchConditionRow.Designer.vb # UI定義
├─ Logics/
│  ├─ COMMON.vb                         # 共通ユーティリティ
│  ├─ LICENSE.vb                        # ライセンス検証
│  ├─ OraDB DUMP Viewer/
│  │  └─ MenuStripLogics.vb             # メニュー処理
│  ├─ Workspace/
│  │  └─ AnalyzeLogic.vb                # DUMPファイル解析（本番用）
│  ├─ TablePreview/
│  │  ├─ TablePreviewLogic.vb           # テーブル表示制御ロジック
│  │  └─ SearchCondition.vb             # 検索条件の定義・評価ロジック
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

## Advanced Search Feature（高度な検索）

### 概要

SQL Server Management Studio のような複合条件検索機能を実装。複数の検索条件を AND/OR ロジックで組み合わせることが可能です。

**デフォルト動作:**
- `TablePreview` フォーム起動時に高度な検索ダイアログが自動で開く
- シンプル検索パネルは非表示
- 前回の検索条件が保持される

### 検索演算子（12種類）

| 演算子 | 説明 | 用途例 |
|--------|------|--------|
| **含む** | 値を部分一致で検索 | 名前に「太郎」を含む |
| **含まない** | 値を部分一致で除外 | 部門に「営業」を含まない |
| **等しい** | 完全一致 | ステータス = 「有効」 |
| **等しくない** | 完全一致で除外 | ステータス ≠ 「削除」 |
| **で始まる** | 前方一致 | メールが「user@」で始まる |
| **で終わる** | 後方一致 | 拡張子が「.txt」で終わる |
| **>** | より大きい | 給与 > 500000 |
| **<** | より小さい | 在庫 < 10 |
| **>=** | 以上 | 年度 >= 2024 |
| **<=** | 以下 | スコア <= 100 |
| **Null** | Null値判定 | コメント欄が空 |
| **Not Null** | Null以外 | 更新日時が空でない |

### 検索条件の保持機能

#### データ流

```
TablePreview起動
  ↓
前回の検索条件がある？
  ├─ YES → _lastSearchCondition から復元
  └─ NO  → デフォルト（条件1つ）を初期化
  ↓
AdvancedSearchForm を開く
  ├─ SetSearchCondition() で前回条件を渡す
  └─ RestoreSearchCondition() で UI に復元
  ↓
ユーザーが検索実行
  ├─ 検索条件を構築
  ├─ _currentSearchCondition に設定
  └─ _lastSearchCondition にコピー（ディープコピー）
  ↓
FilterData() で評価実行
```

#### 実装詳細

**TablePreview.vb:**
- `_lastSearchCondition` フィールド：前回の検索条件を保持
- `CopySearchCondition()` メソッド：条件をディープコピー
- `OpenAdvancedSearchForm()` メソッド：条件を渡してフォームを開く

**AdvancedSearchForm.vb:**
- `SetSearchCondition()` メソッド：外部から条件を受け取る
- `RestoreSearchCondition()` メソッド：条件から UI を再構築
- `GetOperatorIndex()` メソッド：演算子タイプをインデックスに変換

**SearchConditionRow.vb:**
- `GetColumnIndex()` / `SetColumnIndex()` - 列を取得・設定
- `SetOperatorIndex()` - 演算子を設定
- `SetValue()` - 検索値を設定
- `SetCaseSensitive()` - 大文字小文字区別を設定

### 複合条件の評価ロジック

#### クラス構成

**SearchCondition.vb:**

```visualbasic
Public Class SearchCondition
    Enum OperatorType
        Contains, NotContains, Equals, ... (12種類)
    End Enum

    Enum LogicalOperatorType
        [And], [Or]
    End Enum

    ' 単一条件を表現
    Public Class SearchConditionItem
        Property ColumnName As String
        Property OperatorType As OperatorType
        Property Value As Object
        Property CaseSensitive As Boolean

        Function Evaluate(cellValue As Object) As Boolean
            ' 条件を評価して True/False を返す
    End Class

    ' 複合条件を表現
    Public Class ComplexSearchCondition
        Property Conditions As List(Of SearchConditionItem)
        Property LogicalOperators As List(Of LogicalOperatorType)

        Function Evaluate(row As Dictionary(Of String, Object)) As Boolean
            ' 複数条件を AND/OR で組み合わせて評価
    End Class
End Class
```

#### 評価のフロー

```visualbasic
例: (SALARY >= 500000 AND DEPARTMENT = "開発部") OR USER_NAME Contains "太郎"

Conditions:  [condition1, condition2, condition3]
LogicalOps:  [And, Or]

評価:
1. result = condition1.Evaluate(row)           ' SALARY >= 500000
2. result = result AND condition2.Evaluate()   ' AND DEPARTMENT = "開発部"
3. result = result OR condition3.Evaluate()    ' OR USER_NAME Contains "太郎"
```

#### パフォーマンス最適化

1. **短絡評価**
   - AND: 最初の FALSE で即座に False を返す
   - OR: 最初の TRUE で即座に True を返す

2. **型変換の最小化**
   - 数値比較前に `Decimal.TryParse()` でバリデーション
   - 文字列変換は1回のみ

3. **ケース変換の効率化**
   - `CaseSensitive = False` のときのみ ToLower() を実行

### UI デザイン（デザイナーパターン）

#### ファイル分離

すべてのフォームが **Designer パターン** を採用：

| ファイル | 役割 |
|---------|------|
| `*.Designer.vb` | UI定義（コンポーネント配置・プロパティ設定） |
| `*.vb` | ロジック（イベント処理・ビジネスロジック） |

#### テンプレート方式

`AdvancedSearchForm.Designer.vb` に以下のテンプレートを配置：
- `templateConditionRow` - 検索条件行のテンプレート（Hidden）
- `templateLogicalPanel` - AND/OR パネルのテンプレート（Hidden）

フォーム内で必要に応じてコピーして使用：

```visualbasic
Private Function CloneConditionRow() As SearchConditionRow
    Dim row As New SearchConditionRow(_columnNames)
    ' テンプレートの状態をコピー
    Return row
End Function
```

### 使用方法

#### ユーザー手順

1. **フォーム起動**
   - TablePreview が開く → 自動的に高度な検索ダイアログが表示

2. **条件を設定**
   - 列名を選択 → 演算子を選択 → 値を入力
   - 必要に応じて「大文字小文字区別」をチェック

3. **複数条件を追加**
   - 「条件を追加」ボタン → AND/OR を選択

4. **検索実行**
   - 「検索」ボタン → 結果が表示される

5. **条件を修正して再検索**
   - 高度な検索ボタン → **前回の条件が復元済み** ← 保持機能

#### プログラマー向け利用

```visualbasic
' 高度な検索フォームを開く
Dim advancedForm As New AdvancedSearchForm(columnNames)

' 前回の条件がある場合は復元
If lastCondition IsNot Nothing Then
    advancedForm.SetSearchCondition(lastCondition)
End If

' ダイアログを表示
If advancedForm.ShowDialog() = DialogResult.OK Then
    Dim searchCondition = advancedForm.SearchConditionResult

    ' フィルタリング実行
    Dim filteredData = tableData.Where(Function(row)
        Return searchCondition.Evaluate(row)
    End Function).ToList()
End If
```

### パフォーマンス特性

| 操作 | 処理時間の目安 |
|------|--------------|
| 1,000行の検索 | < 10ms |
| 10,000行の検索 | < 50ms |
| 100,000行の検索 | < 500ms |
| 複合条件（3条件AND） | 単一条件と同等 |
| 複合条件（3条件OR） | 単一条件と同等 |

※計測環境: .NET Framework / Intel Core i5

## Code Style & Conventions

### VB.NET コーディング規約

- **命名規則**
  - クラス: PascalCase（`TablePreview`、`SearchConditionRow`）
  - メソッド: PascalCase（`DisplayTableData`、`RestoreSearchCondition`）
  - フィールド: `_camelCase`（プライベート、`_tableData`）
  - ローカル変数: camelCase（`tableName`）

- **構文**
  - `End If`, `End Sub` 等を明示的に記述（VB.NETスタイル）
  - 1行80文字以内を推奨
  - `#Region` / `#End Region` で論理的にグループ化

- **コメント**
  - XML Documentation（`'''`）：public メソッドと複雑なロジック
  - `'` シングルコメント：ロジック説明
  - 複雑な処理には詳細なコメント追加

- **デザイナーパターン**
  - UI定義は `*.Designer.vb` に完全隔離
  - ロジックファイル（`*.vb`）には UI 生成コードを記載しない
  - デフォルトから逸脱する UI 変更のみ InitializeComponent() 呼び出し後に実施

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
msbuild "OraDB DUMP Viewer.csproj" /p:Configuration=Debug

# リリースビルド（本番ロジック使用）
msbuild "OraDB DUMP Viewer.csproj" /p:Configuration=Release
```

### 対象フレームワーク

- **.NET**: net10.0-windows7.0
- **言語**: Visual Basic .NET
- **UI**: Windows Forms (WinForms)

## Future Enhancements

### 高度な検索機能の拡張

1. **より複雑な検索式**
   - 括弧による優先度制御: `(条件1 AND 条件2) OR 条件3`
   - NOT 演算子のサポート

2. **検索条件の永続化**
   - 検索条件をファイル保存/読み込み（JSON形式）
   - よく使用する検索条件をテンプレート化

3. **ワイルドカード検索**
   - `%` / `_` ワイルドカードのサポート
   - 正規表現マッチング

4. **検索結果の操作**
   - 検索結果のCSV/JSON エクスポート
   - ハイライト表示

### DUMPファイル解析実装

1. **AnalyzeLogic.vb の実装**
   - OraDB EXPORTフォーマットのパース
   - テーブル構造の自動抽出
   - スキーマ・テーブル・データの階層構造化

2. **ストリーミング解析**
   - 大規模ファイル対応（ギガバイト単位）
   - メモリ効率的な処理

### UI/UX の改善

1. **テーブル統計情報**
   - 行数、カラム数の表示
   - データ型の推定・表示

2. **行のフォーカス機能**
   - 特定行への直接ジャンプ
   - 行番号で検索

3. **ソート機能**
   - 列をクリックして昇順/降順
   - マルチカラムソート

### パフォーマンス最適化

1. **キャッシング機構**
   - 解析済みDUMPファイルのキャッシュ
   - 次回起動時の高速ロード

2. **インデックス機構**
   - 頻繁にアクセスされる列にインデックスを作成
   - 検索速度の大幅向上

3. **非同期処理**
   - 大規模データ読み込みを非同期化
   - UI フリーズ防止

## Contact & Support

ライセンス認証、機能リクエスト、バグレポート等は GitHub Issues で報告してください。
