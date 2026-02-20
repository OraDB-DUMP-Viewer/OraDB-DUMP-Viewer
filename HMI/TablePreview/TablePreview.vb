Imports System.IO
Imports System.Collections.Generic

Public Class TablePreview

#Region "フィールド・コンストラクタ"
    Private _tableData As List(Of Dictionary(Of String, Object))
    Private _filteredData As List(Of Dictionary(Of String, Object))
    Private _columnNames As List(Of String)
    Private _currentPage As Integer = 1
    Private _pageSize As Integer = 100
    Private _totalRows As Integer = 0
    Private _isInitializing As Boolean = True
    Private _currentSearchCondition As SearchCondition.ComplexSearchCondition
    Private _lastSearchCondition As SearchCondition.ComplexSearchCondition

    Public Sub New(tableData As List(Of Dictionary(Of String, Object)), columnNames As List(Of String), tableName As String)
        InitializeComponent()
        _tableData = tableData
        _columnNames = columnNames
        _filteredData = New List(Of Dictionary(Of String, Object))(_tableData)
        _totalRows = _tableData.Count
        Me.Text = $"テーブルデータプレビュー - {tableName}"
        _currentSearchCondition = Nothing
        _lastSearchCondition = Nothing
    End Sub
#End Region

#Region "イベント処理"
    Private Sub TablePreview_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _isInitializing = True

        ' 列名をコンボボックスに設定
        For Each colName In _columnNames
            comboBoxColumns.Items.Add(colName)
        Next
        If comboBoxColumns.Items.Count > 0 Then
            comboBoxColumns.SelectedIndex = 0
        End If

        ' ページサイズの初期値を設定
        numericUpDownPageSize.Value = _pageSize

        ' イベントハンドラーを設定
        AddHandler buttonAdvancedSearch.Click, AddressOf ButtonAdvancedSearch_Click

        ' DataGridViewをセットアップ
        SetupDataGridView()

        ' 初期データを表示
        UpdateDataDisplay()

        _isInitializing = False
    End Sub

    Private Sub buttonSearch_Click(sender As Object, e As EventArgs) Handles buttonSearch.Click
        _currentPage = 1
        _currentSearchCondition = Nothing
        FilterData()
        UpdateDataDisplay()
    End Sub

    Private Sub ButtonAdvancedSearch_Click(sender As Object, e As EventArgs)
        OpenAdvancedSearchForm()
    End Sub

    Private Sub OpenAdvancedSearchForm()
        Dim advancedForm As New AdvancedSearchForm(_columnNames)

        ' 前回の検索条件があれば復元
        If _lastSearchCondition IsNot Nothing Then
            advancedForm.SetSearchCondition(_lastSearchCondition)
        End If

        If advancedForm.ShowDialog(Me) = DialogResult.OK Then
            _currentPage = 1
            _currentSearchCondition = advancedForm.SearchConditionResult

            ' 検索条件を保持
            _lastSearchCondition = CopySearchCondition(_currentSearchCondition)

            FilterData()
            UpdateDataDisplay()
        End If
    End Sub

    ''' <summary>
    ''' 検索条件をディープコピーする
    ''' </summary>
    Private Function CopySearchCondition(original As SearchCondition.ComplexSearchCondition) As SearchCondition.ComplexSearchCondition
        If original Is Nothing Then
            Return Nothing
        End If

        Dim copied As New SearchCondition.ComplexSearchCondition()

        For Each condition In original.Conditions
            Dim newCondition As New SearchCondition.SearchConditionItem(
                condition.ColumnName,
                condition.OperatorType,
                condition.Value,
                condition.CaseSensitive
            )
            copied.Conditions.Add(newCondition)
        Next

        For Each logicalOp In original.LogicalOperators
            copied.LogicalOperators.Add(logicalOp)
        Next

        Return copied
    End Function

    Private Sub buttonReset_Click(sender As Object, e As EventArgs) Handles buttonReset.Click
        _currentPage = 1
        _filteredData = New List(Of Dictionary(Of String, Object))(_tableData)
        textBoxSearchValue.Clear()
        comboBoxColumns.SelectedIndex = 0
        _currentSearchCondition = Nothing
        UpdateDataDisplay()
    End Sub

    Private Sub buttonPrev_Click(sender As Object, e As EventArgs) Handles buttonPrev.Click
        If _currentPage > 1 Then
            _currentPage -= 1
            UpdateDataDisplay()
        End If
    End Sub

    Private Sub buttonNext_Click(sender As Object, e As EventArgs) Handles buttonNext.Click
        Dim totalPages As Integer = Math.Ceiling(_filteredData.Count / _pageSize)
        If _currentPage < totalPages Then
            _currentPage += 1
            UpdateDataDisplay()
        End If
    End Sub

    Private Sub numericUpDownPageSize_ValueChanged(sender As Object, e As EventArgs) Handles numericUpDownPageSize.ValueChanged
        If Not _isInitializing Then
            _pageSize = CInt(numericUpDownPageSize.Value)
            _currentPage = 1
            UpdateDataDisplay()
        End If
    End Sub

    Private Sub textBoxSearchValue_KeyDown(sender As Object, e As KeyEventArgs) Handles textBoxSearchValue.KeyDown
        If e.KeyCode = Keys.Return Then
            buttonSearch.PerformClick()
            e.Handled = True
        End If
    End Sub
#End Region

#Region "データ処理"
    Private Sub SetupDataGridView()
        dataGridViewData.Columns.Clear()

        ' 列を追加
        For Each colName In _columnNames
            dataGridViewData.Columns.Add(colName, colName)
        Next

        ' 列の幅を自動調整
        For Each col As DataGridViewColumn In dataGridViewData.Columns
            col.Width = Math.Max(100, Math.Min(200, col.Width))
        Next
    End Sub

    Private Sub FilterData()
        ' 高度な検索が有効な場合
        If _currentSearchCondition IsNot Nothing Then
            _filteredData = _tableData.Where(Function(row)
                                               Return _currentSearchCondition.Evaluate(row)
                                           End Function).ToList()
            Return
        End If

        ' シンプル検索
        Dim columnName As String = comboBoxColumns.SelectedItem?.ToString()
        Dim searchValue As String = textBoxSearchValue.Text.Trim()

        If String.IsNullOrEmpty(columnName) OrElse String.IsNullOrEmpty(searchValue) Then
            _filteredData = New List(Of Dictionary(Of String, Object))(_tableData)
            Return
        End If

        ' フィルタリング処理
        _filteredData = _tableData.Where(Function(row)
                                           If row.ContainsKey(columnName) Then
                                               Dim cellValue = row(columnName)
                                               If cellValue IsNot Nothing Then
                                                   Return cellValue.ToString().Contains(searchValue, StringComparison.OrdinalIgnoreCase)
                                               End If
                                           End If
                                           Return False
                                       End Function).ToList()
    End Sub

    Private Sub UpdateDataDisplay()
        Dim totalPages As Integer = Math.Ceiling(If(_filteredData.Count = 0, 1, _filteredData.Count / _pageSize))
        Dim startRow As Integer = (_currentPage - 1) * _pageSize
        Dim endRow As Integer = Math.Min(startRow + _pageSize, _filteredData.Count)

        ' DataGridViewをクリア
        dataGridViewData.Rows.Clear()

        ' 現在のページのデータをDataGridViewに追加
        For i As Integer = startRow To endRow - 1
            Dim row = _filteredData(i)
            Dim cells As New List(Of Object)

            For Each colName In _columnNames
                If row.ContainsKey(colName) Then
                    cells.Add(If(row(colName), String.Empty))
                Else
                    cells.Add(String.Empty)
                End If
            Next

            dataGridViewData.Rows.Add(cells.ToArray())
        Next

        ' UI要素を更新
        labelPageInfo.Text = $"ページ: {_currentPage}/{totalPages}"
        labelRowCount.Text = $"合計行数: {_tableData.Count} (表示行数: {_filteredData.Count})"
        buttonPrev.Enabled = _currentPage > 1
        buttonNext.Enabled = _currentPage < totalPages
    End Sub
#End Region

End Class