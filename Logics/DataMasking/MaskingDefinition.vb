Imports System.IO
Imports System.Text.Json

''' <summary>
''' データマスキング定義をJSONファイル (.odmask) として永続化するためのデータクラス群
''' </summary>
Public Class MaskingDefinition
    Public Property Version As String = "1.0"
    Public Property Description As String = String.Empty
    Public Property DefaultMaskValue As String = "***"
    Public Property Tables As List(Of TableMaskingRule) = New List(Of TableMaskingRule)

    Private Shared ReadOnly _jsonOptions As New JsonSerializerOptions() With {
        .WriteIndented = True
    }

    ''' <summary>
    ''' 指定されたスキーマ・テーブル名に一致するマスクルールを取得する
    ''' </summary>
    Public Function FindTableRule(schema As String, tableName As String) As TableMaskingRule
        Return Tables.FirstOrDefault(
            Function(t) String.Equals(t.Schema, schema, StringComparison.OrdinalIgnoreCase) AndAlso
                        String.Equals(t.TableName, tableName, StringComparison.OrdinalIgnoreCase))
    End Function

    ''' <summary>
    ''' .odmask ファイルから定義を読み込む
    ''' </summary>
    Public Shared Function Load(path As String) As MaskingDefinition
        Dim json = File.ReadAllText(path, System.Text.Encoding.UTF8)
        Dim data = JsonSerializer.Deserialize(Of MaskingDefinition)(json, _jsonOptions)
        If data Is Nothing Then data = New MaskingDefinition()
        Return data
    End Function

    ''' <summary>
    ''' .odmask ファイルに定義を保存する
    ''' </summary>
    Public Sub Save(path As String)
        Dim json = JsonSerializer.Serialize(Me, _jsonOptions)
        File.WriteAllText(path, json, System.Text.Encoding.UTF8)
    End Sub
End Class

''' <summary>
''' テーブル単位のマスクルール
''' </summary>
Public Class TableMaskingRule
    Public Property Schema As String = String.Empty
    Public Property TableName As String = String.Empty
    Public Property Columns As List(Of ColumnMaskingRule) = New List(Of ColumnMaskingRule)
End Class

''' <summary>
''' 列単位のマスクルール
''' </summary>
Public Class ColumnMaskingRule
    Public Property ColumnName As String = String.Empty
    Public Property MaskValue As String = String.Empty
End Class
