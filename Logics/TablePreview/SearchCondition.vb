''' <summary>
''' 検索条件に関するすべてのクラスを含むモジュール
''' </summary>
Public Class SearchCondition

    ''' <summary>
    ''' 検索演算子の種類（12種類）
    ''' </summary>
    Public Enum OperatorType
        Contains
        NotContains
        Equals
        NotEquals
        GreaterThan
        LessThan
        GreaterThanOrEqual
        LessThanOrEqual
        StartsWith
        EndsWith
        IsNull
        IsNotNull
    End Enum

    ''' <summary>
    ''' 複合条件の論理演算子（AND/OR）
    ''' </summary>
    Public Enum LogicalOperatorType
        [And]
        [Or]
    End Enum

    ''' <summary>
    ''' 単一の検索条件を表現するクラス
    ''' </summary>
    Public Class SearchConditionItem
        ''' <summary>
        ''' 検索対象の列名
        ''' </summary>
        Public Property ColumnName As String

        ''' <summary>
        ''' 演算子タイプ
        ''' </summary>
        Public Property OperatorType As SearchCondition.OperatorType

        ''' <summary>
        ''' 検索値
        ''' </summary>
        Public Property Value As Object

        ''' <summary>
        ''' 大文字小文字区別
        ''' </summary>
        Public Property CaseSensitive As Boolean = False

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
        End Sub

        ''' <summary>
        ''' パラメータ付きコンストラクタ
        ''' </summary>
        ''' <param name="columnName">列名</param>
        ''' <param name="operatorType">演算子</param>
        ''' <param name="value">検索値</param>
        ''' <param name="caseSensitive">大文字小文字区別</param>
        Public Sub New(columnName As String, operatorType As SearchCondition.OperatorType, value As Object, Optional caseSensitive As Boolean = False)
            Me.ColumnName = columnName
            Me.OperatorType = operatorType
            Me.Value = value
            Me.CaseSensitive = caseSensitive
        End Sub

        ''' <summary>
        ''' 条件を評価する
        ''' </summary>
        ''' <param name="cellValue">セル値</param>
        ''' <returns>条件成立ならTrue</returns>
        Public Function Evaluate(cellValue As Object) As Boolean
            '--- Null値判定（IsNull/IsNotNull演算子）---
            If OperatorType = SearchCondition.OperatorType.IsNull Then
                ' セル値がNothingまたは空文字の場合True
                Return cellValue Is Nothing OrElse String.IsNullOrEmpty(cellValue.ToString())
            End If

            If OperatorType = SearchCondition.OperatorType.IsNotNull Then
                ' セル値がNothingでも空文字でもない場合True
                Return cellValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellValue.ToString())
            End If

            ' NULL値は文字列比較演算子ではFalseを返す
            If cellValue Is Nothing Then
                Return False
            End If

            '--- 文字列変換（数値比較や部分一致のため）---
            Dim cellStr = cellValue.ToString() ' セル値を文字列化
            Dim searchStr = If(Value Is Nothing, "", Value.ToString()) ' 検索値を文字列化（Null安全）

            '--- 大文字小文字区別なしの場合は小文字化 ---
            If Not CaseSensitive Then
                cellStr = cellStr.ToLower()
                searchStr = searchStr.ToLower()
            End If

            Select Case OperatorType
                ' 部分一致判定
                Case SearchCondition.OperatorType.Contains
                    ' cellStrがsearchStrを含む場合
                    Return cellStr.Contains(searchStr)
                ' 部分一致否定
                Case SearchCondition.OperatorType.NotContains
                    ' cellStrがsearchStrを含まない場合
                    Return Not cellStr.Contains(searchStr)
                ' 完全一致判定
                Case SearchCondition.OperatorType.Equals
                    ' cellStrとsearchStrが等しい場合
                    Return cellStr = searchStr
                ' 完全一致否定
                Case SearchCondition.OperatorType.NotEquals
                    ' cellStrとsearchStrが異なる場合
                    Return cellStr <> searchStr
                ' 前方一致判定
                Case SearchCondition.OperatorType.StartsWith
                    ' cellStrがsearchStrで始まる場合
                    Return cellStr.StartsWith(searchStr)
                ' 後方一致判定
                Case SearchCondition.OperatorType.EndsWith
                    ' cellStrがsearchStrで終わる場合
                    Return cellStr.EndsWith(searchStr)
                ' 数値比較: より大きい
                Case SearchCondition.OperatorType.GreaterThan
                    ' cellStr > searchStr
                    Dim cellNum As Decimal, searchNum As Decimal
                    If Decimal.TryParse(cellStr, cellNum) AndAlso Decimal.TryParse(searchStr, searchNum) Then
                        Return cellNum > searchNum
                    End If
                    ' 数値変換失敗時はFalse
                    Return False
                ' 数値比較: より小さい
                Case SearchCondition.OperatorType.LessThan
                    ' cellStr < searchStr
                    Dim cellNum As Decimal, searchNum As Decimal
                    If Decimal.TryParse(cellStr, cellNum) AndAlso Decimal.TryParse(searchStr, searchNum) Then
                        Return cellNum < searchNum
                    End If
                    Return False
                ' 数値比較: 以上
                Case SearchCondition.OperatorType.GreaterThanOrEqual
                    ' cellStr >= searchStr
                    Dim cellNum As Decimal, searchNum As Decimal
                    If Decimal.TryParse(cellStr, cellNum) AndAlso Decimal.TryParse(searchStr, searchNum) Then
                        Return cellNum >= searchNum
                    End If
                    Return False
                ' 数値比較: 以下
                Case SearchCondition.OperatorType.LessThanOrEqual
                    ' cellStr <= searchStr
                    Dim cellNum As Decimal, searchNum As Decimal
                    If Decimal.TryParse(cellStr, cellNum) AndAlso Decimal.TryParse(searchStr, searchNum) Then
                        Return cellNum <= searchNum
                    End If
                    Return False
                ' 未定義演算子
                Case Else
                    Return False
            End Select
        End Function
    End Class

    ''' <summary>
    ''' 複数の検索条件を組み合わせるクラス
    ''' </summary>
    Public Class ComplexSearchCondition
        ''' <summary>
        ''' 検索条件リスト
        ''' </summary>
        Public Property Conditions As List(Of SearchCondition.SearchConditionItem)

        ''' <summary>
        ''' 条件間の論理演算子リスト（AND/OR）
        ''' </summary>
        Public Property LogicalOperators As List(Of SearchCondition.LogicalOperatorType)

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            Conditions = New List(Of SearchCondition.SearchConditionItem)()
            LogicalOperators = New List(Of SearchCondition.LogicalOperatorType)()
        End Sub

        ''' <summary>
        ''' 行データが複合条件を満たすかチェックする
        ''' 条件数が1の場合はその条件だけ評価
        ''' 複数の場合はLogicalOperatorsに従って評価（最初の条件はスキップ）
        ''' </summary>
        ''' <param name="row">行データ（String配列、列インデックスで格納）</param>
        ''' <param name="columnIndexMap">列名→インデックスの逆引きマップ</param>
        ''' <returns>条件成立ならTrue</returns>
        Public Function Evaluate(row As String(), columnIndexMap As Dictionary(Of String, Integer)) As Boolean
            If Conditions.Count = 0 Then
                Return True
            End If

            If Conditions.Count = 1 Then
                Dim condition = Conditions(0)
                Dim colIndex As Integer = -1
                If columnIndexMap.TryGetValue(condition.ColumnName, colIndex) AndAlso colIndex >= 0 AndAlso colIndex < row.Length Then
                    Return condition.Evaluate(row(colIndex))
                End If
                Return False
            End If

            ' 最初の条件を評価
            Dim result = False
            Dim firstCondition = Conditions(0)
            Dim firstColIndex As Integer = -1
            If columnIndexMap.TryGetValue(firstCondition.ColumnName, firstColIndex) AndAlso firstColIndex >= 0 AndAlso firstColIndex < row.Length Then
                result = firstCondition.Evaluate(row(firstColIndex))
            End If

            ' 残りの条件をLogicalOperatorsに従って評価
            For i = 1 To Conditions.Count - 1
                Dim condition = Conditions(i)
                Dim colIndex As Integer = -1
                Dim cellValue As Object = Nothing
                If columnIndexMap.TryGetValue(condition.ColumnName, colIndex) AndAlso colIndex >= 0 AndAlso colIndex < row.Length Then
                    cellValue = row(colIndex)
                End If
                Dim conditionResult = condition.Evaluate(cellValue)

                Dim logicalOp = LogicalOperators(i - 1)
                If logicalOp = SearchCondition.LogicalOperatorType.And Then
                    result = result AndAlso conditionResult
                Else ' Or
                    result = result OrElse conditionResult
                End If
            Next

            Return result
        End Function
    End Class

End Class
