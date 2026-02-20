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
    End Enum

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
            Me.OperatorType = operatorType
            Me.Value = value
            Me.CaseSensitive = caseSensitive
        End Sub

        ''' <summary>
        ''' 条件を評価する
        ''' </summary>
        Public Function Evaluate(cellValue As Object) As Boolean
            ' Null値のチェック
            If OperatorType = SearchCondition.OperatorType.IsNull Then
                Return cellValue Is Nothing OrElse String.IsNullOrEmpty(cellValue.ToString())
            End If

            If OperatorType = SearchCondition.OperatorType.IsNotNull Then
                Return cellValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(cellValue.ToString())
            End If

            If cellValue Is Nothing Then
                Return False
            End If

            Dim cellStr = cellValue.ToString()
            Dim searchStr = If(Value Is Nothing, "", Value.ToString())

            If Not CaseSensitive Then
                cellStr = cellStr.ToLower()
                searchStr = searchStr.ToLower()
            End If

            Select Case OperatorType
                Case SearchCondition.OperatorType.Contains
                    Return cellStr.Contains(searchStr)
                Case SearchCondition.OperatorType.NotContains
                    Return Not cellStr.Contains(searchStr)
                Case SearchCondition.OperatorType.Equals
                    Return cellStr = searchStr
                Case SearchCondition.OperatorType.NotEquals
                    Return cellStr <> searchStr
                Case SearchCondition.OperatorType.StartsWith
                    Return cellStr.StartsWith(searchStr)
                Case SearchCondition.OperatorType.EndsWith
                    Return cellStr.EndsWith(searchStr)
                Case SearchCondition.OperatorType.GreaterThan
                    If Decimal.TryParse(cellStr, Nothing) AndAlso Decimal.TryParse(searchStr, Nothing) Then
                        Return Decimal.Parse(cellStr) > Decimal.Parse(searchStr)
                    End If
                    Return False
                Case SearchCondition.OperatorType.LessThan
                    If Decimal.TryParse(cellStr, Nothing) AndAlso Decimal.TryParse(searchStr, Nothing) Then
                        Return Decimal.Parse(cellStr) < Decimal.Parse(searchStr)
                    End If
                    Return False
                Case SearchCondition.OperatorType.GreaterThanOrEqual
                    If Decimal.TryParse(cellStr, Nothing) AndAlso Decimal.TryParse(searchStr, Nothing) Then
                        Return Decimal.Parse(cellStr) >= Decimal.Parse(searchStr)
                    End If
                    Return False
                Case SearchCondition.OperatorType.LessThanOrEqual
                    If Decimal.TryParse(cellStr, Nothing) AndAlso Decimal.TryParse(searchStr, Nothing) Then
                        Return Decimal.Parse(cellStr) <= Decimal.Parse(searchStr)
                    End If
                    Return False
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
                If row.ContainsKey(condition.ColumnName) Then
                    Return condition.Evaluate(row(condition.ColumnName))
                End If
                Return False
            End If

            ' 最初の条件を評価
            Dim result = False
            Dim firstCondition = Conditions(0)
            If row.ContainsKey(firstCondition.ColumnName) Then
                result = firstCondition.Evaluate(row(firstCondition.ColumnName))
            End If

            ' 残りの条件をLogicalOperatorsに従って評価
            For i As Integer = 1 To Conditions.Count - 1
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
