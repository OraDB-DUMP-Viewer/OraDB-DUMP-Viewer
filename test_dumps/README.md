# test_dumps

OraDB DUMP Viewer のパーサーテスト用ディレクトリです。

## テストハーネス

| ファイル | 内容 |
|---|---|
| `test_parser.c` | DLL の全機能テスト (テーブル一覧・行データ・パーティション・フィルタ) |
| `test_export.c` | CSV / SQL エクスポートテスト |

## ダンプファイルの入手

テスト用 `.dmp` ファイルはリポジトリサイズ削減のため git 管理から除外しています。  
以下のリポジトリから生成してください:

👉 **[OraDB-DUMP-Viewer/odv-testdump](https://github.com/OraDB-DUMP-Viewer/odv-testdump)**

```bash
# 1. テストダンプ生成環境をクローン
git clone https://github.com/OraDB-DUMP-Viewer/odv-testdump.git
cd odv-testdump

# 2. Docker で Oracle Database + ダンプファイルを自動生成
./run.sh

# 3. 生成された .dmp ファイルをこのディレクトリにコピー
cp output/*.dmp ../OraDB-DUMP-Viewer/test_dumps/11g/
```

## テストの実行

```bash
# DLL をビルド (test_dumps/ にも自動コピーされます)
cd Logics/DumpParser
build_dll.bat

# テストハーネスをビルド・実行
cd ../../test_dumps
cl /nologo /utf-8 /MT test_parser.c /Fe:test_parser.exe
test_parser.exe .
```

## 必要な環境

- Visual Studio 2026 (MSVC C++ toolchain)
- Docker (ダンプファイル生成時のみ)
