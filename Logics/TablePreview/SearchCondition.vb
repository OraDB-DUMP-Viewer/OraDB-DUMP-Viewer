''' <summary>
''' 検索条件に関するすべてのクラスを含むモジュール
''' </summary>
''' <summary>
''' 検索条件に関するすべてのクラスを含むモジュール
''' </summary>
Public Class SearchCondition

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

    Public Enum LogicalOperatorType
        [And]
        [Or]
    ''' <summary>
    ''' 検索演算子の種類（12種類）
    ''' </summary>

    ''' <summary>
    ''' 単一の検索条件を表現するクラス
    ''' </summary>
    Public Class SearchConditionItem
        Public Property ColumnName As String
        Public Property OperatorType As SearchCondition.OperatorType
        Public Property Value As Object
        Public Property CaseSensitive As Boolean = False

        Public Sub New()
        End Sub

        Public Sub New(columnName As String, operatorType As SearchCondition.OperatorType, value As Object, Optional caseSensitive As Boolean = False)
            Me.ColumnName = columnName
    ''' <summary>
    ''' 複合条件の論理演算子（AND/OR）
    ''' </summary>
            Me.Value = value
            Me.CaseSensitive = caseSensitive
        End Sub

        ''' <summary>
        ''' 条件を評価する
        ''' </summary>
        Public Function Evaluate(cellValue As Object) As Boolean
            ' Null値のチェック
        ''' <summary>
        ''' 検索対象の列名
        ''' </summary>
            If OperatorType = SearchCondition.OperatorType.IsNull Then
        ''' <summary>
        ''' 演算子タイプ
        ''' </summary>
                Return cellValue Is Nothing OrElse String.IsNullOrEmpty(cellValue.ToString())
        ''' <summary>
        ''' 検索値
        ''' </summary>
            End If
        ''' <summary>
        ''' 大文字小文字区別
        ''' </summary>

            If OperatorType = SearchCondition.OperatorType.IsNotNull Then
        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            End If

        ''' <summary>
        ''' パラメータ付きコンストラクタ
        ''' </summary>
        ''' <param name="columnName">列名</param>
        ''' <param name="operatorType">演算子</param>
        ''' <param name="value">検索値</param>
        ''' <param name="caseSensitive">大文字小文字区別</param>
        Public Sub New(columnName As String, operatorType As SearchCondition.OperatorType, value As Object, Optional caseSensitive As Boolean = False)
                Return False
            End If

            Dim cellStr = cellValue.ToString()
            Dim searchStr = If(Value Is Nothing, "", Value.ToString())

            If Not CaseSensitive Then
                cellStr = cellStr.ToLower()
                searchStr = searchStr.ToLower()
        ''' <param name="cellValue">セル値</param>
        ''' <returns>条件成立ならTrue</returns>
        Public Function Evaluate(cellValue As Object) As Boolean

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
        Public Property Conditions As List(Of SearchCondition.SearchConditionItem)
        Public Property LogicalOperators As List(Of SearchCondition.LogicalOperatorType)

        Public Sub New()
            Conditions = New List(Of SearchCondition.SearchConditionItem)()
            LogicalOperators = New List(Of SearchCondition.LogicalOperatorType)()
        End Sub

        ''' <summary>
        ''' 行データが複合条件を満たすかチェックする
        ''' 条件数が1の場合はその条件だけ評価
        ''' 複数の場合はLogicalOperatorsに従って評価（最初の条件はスキップ）
        ''' </summary>
        Public Function Evaluate(row As Dictionary(Of String, Object)) As Boolean
            If Conditions.Count = 0 Then
                Return True
            End If

            If Conditions.Count = 1 Then
                Dim condition = Conditions(0)
        ''' <summary>
        ''' 検索条件リスト
        ''' </summary>
                If row.ContainsKey(condition.ColumnName) Then
        ''' <summary>
        ''' 条件間の論理演算子リスト（AND/OR）
        ''' </summary>
                    Return condition.Evaluate(row(condition.ColumnName))
                End If
        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
            End If

            ' 最初の条件を評価
            Dim result = False
            Dim firstCondition = Conditions(0)
            If row.ContainsKey(firstCondition.ColumnName) Then
                result = firstCondition.Evaluate(row(firstCondition.ColumnName))
            End If

            ' 残りの条件をLogicalOperatorsに従って評価
        ''' <param name="row">行データ（列名→値）</param>
        ''' <returns>条件成立ならTrue</returns>
        Public Function Evaluate(row As Dictionary(Of String, Object)) As Boolean
                Dim condition = Conditions(i)
                Dim cellValue = If(row.ContainsKey(condition.ColumnName), row(condition.ColumnName), Nothing)
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
