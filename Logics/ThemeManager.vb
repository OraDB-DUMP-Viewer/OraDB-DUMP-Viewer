Imports System.Drawing
Imports System.Windows.Forms

''' <summary>
''' アプリケーション全体のテーマ管理モジュール
''' ダークモード/ライトモードの切り替えと適用を行う
''' </summary>
Public Module ThemeManager

#Region "列挙型・状態"

    Public Enum ThemeMode
        Light = 0
        Dark = 1
    End Enum

    Public Property CurrentTheme As ThemeMode = ThemeMode.Light

    Public ReadOnly Property IsDarkMode As Boolean
        Get
            Return CurrentTheme = ThemeMode.Dark
        End Get
    End Property

#End Region

#Region "カラーパレット"

    Public Property FormBackColor As Color
    Public Property ControlBackColor As Color
    Public Property ForeColor As Color
    Public Property InputBackColor As Color
    Public Property InputForeColor As Color
    Public Property SecondaryForeColor As Color
    Public Property MenuBackColor As Color
    Public Property MenuForeColor As Color
    Public Property StatusBackColor As Color
    Public Property GridBackColor As Color
    Public Property GridHeaderBackColor As Color
    Public Property GridHeaderForeColor As Color
    Public Property GridAlternateRowColor As Color
    Public Property GridCellForeColor As Color
    Public Property GridLineColor As Color
    Public Property ListBackColor As Color
    Public Property ListForeColor As Color
    Public Property SplitterColor As Color
    Public Property BorderColor As Color
    Public Property AccentBackColor As Color
    Public Property AccentForeColor As Color
    Public Property LinkColor As Color
    Public Property NeutralStatusColor As Color
    Public Property SuccessColor As Color
    Public Property ErrorColor As Color
    Public Property UpdateColor As Color
    Public Property TabBackColor As Color

    Public Sub UpdatePalette()
        If IsDarkMode Then
            FormBackColor = Color.FromArgb(30, 30, 30)
            ControlBackColor = Color.FromArgb(37, 37, 38)
            ForeColor = Color.FromArgb(220, 220, 220)
            InputBackColor = Color.FromArgb(45, 45, 48)
            InputForeColor = Color.FromArgb(220, 220, 220)
            SecondaryForeColor = Color.FromArgb(150, 150, 150)
            MenuBackColor = Color.FromArgb(37, 37, 38)
            MenuForeColor = Color.FromArgb(220, 220, 220)
            StatusBackColor = Color.FromArgb(37, 37, 38)
            GridBackColor = Color.FromArgb(30, 30, 30)
            GridHeaderBackColor = Color.FromArgb(45, 45, 48)
            GridHeaderForeColor = Color.FromArgb(220, 220, 220)
            GridAlternateRowColor = Color.FromArgb(37, 37, 38)
            GridCellForeColor = Color.FromArgb(220, 220, 220)
            GridLineColor = Color.FromArgb(60, 60, 60)
            ListBackColor = Color.FromArgb(30, 30, 30)
            ListForeColor = Color.FromArgb(220, 220, 220)
            SplitterColor = Color.FromArgb(60, 60, 60)
            BorderColor = Color.FromArgb(60, 60, 60)
            AccentBackColor = Color.FromArgb(0, 122, 204)
            AccentForeColor = Color.White
            LinkColor = Color.FromArgb(86, 156, 214)
            NeutralStatusColor = Color.FromArgb(220, 220, 220)
            SuccessColor = Color.FromArgb(78, 201, 176)
            ErrorColor = Color.FromArgb(244, 135, 113)
            UpdateColor = Color.FromArgb(255, 165, 0)
            TabBackColor = Color.FromArgb(37, 37, 38)
        Else
            FormBackColor = SystemColors.Control
            ControlBackColor = SystemColors.Control
            ForeColor = SystemColors.ControlText
            InputBackColor = Color.White
            InputForeColor = SystemColors.WindowText
            SecondaryForeColor = Color.Gray
            MenuBackColor = SystemColors.MenuBar
            MenuForeColor = SystemColors.MenuText
            StatusBackColor = SystemColors.Control
            GridBackColor = Color.White
            GridHeaderBackColor = SystemColors.Control
            GridHeaderForeColor = SystemColors.ControlText
            GridAlternateRowColor = Color.FromArgb(245, 245, 245)
            GridCellForeColor = SystemColors.WindowText
            GridLineColor = SystemColors.ControlDark
            ListBackColor = SystemColors.Window
            ListForeColor = SystemColors.WindowText
            SplitterColor = SystemColors.ButtonShadow
            BorderColor = SystemColors.ControlDark
            AccentBackColor = SystemColors.Highlight
            AccentForeColor = Color.White
            LinkColor = Color.Blue
            NeutralStatusColor = SystemColors.ControlText
            SuccessColor = Color.Green
            ErrorColor = Color.Red
            UpdateColor = Color.OrangeRed
            TabBackColor = SystemColors.Window
        End If
    End Sub

#End Region

#Region "テーマ適用"

    ''' <summary>
    ''' フォーム全体にテーマを適用する
    ''' </summary>
    Public Sub ApplyTheme(form As Form)
        form.BackColor = FormBackColor
        form.ForeColor = ForeColor

        ' MDI クライアント領域
        If form.IsMdiContainer Then
            For Each ctrl As Control In form.Controls
                If TypeOf ctrl Is MdiClient Then
                    ctrl.BackColor = FormBackColor
                    Exit For
                End If
            Next
        End If

        ApplyThemeToControls(form.Controls)
    End Sub

    ''' <summary>
    ''' UserControl にテーマを適用する
    ''' </summary>
    Public Sub ApplyTheme(uc As UserControl)
        uc.BackColor = ControlBackColor
        uc.ForeColor = ForeColor
        ApplyThemeToControls(uc.Controls)
    End Sub

    ''' <summary>
    ''' 個別コントロールにテーマを適用する (外部呼出し用)
    ''' </summary>
    Public Sub ApplyThemeToSingleControl(ctrl As Control)
        ApplyThemeToControl(ctrl)
        If ctrl.HasChildren Then
            ApplyThemeToControls(ctrl.Controls)
        End If
    End Sub

    Private Sub ApplyThemeToControls(controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            ApplyThemeToControl(ctrl)
            If ctrl.HasChildren Then
                ApplyThemeToControls(ctrl.Controls)
            End If
        Next
    End Sub

    Private Sub ApplyThemeToControl(ctrl As Control)
        If TypeOf ctrl Is MenuStrip Then
            ApplyMenuStrip(DirectCast(ctrl, MenuStrip))

        ElseIf TypeOf ctrl Is ToolStrip Then
            ApplyToolStrip(DirectCast(ctrl, ToolStrip))

        ElseIf TypeOf ctrl Is StatusStrip Then
            ApplyStatusStrip(DirectCast(ctrl, StatusStrip))

        ElseIf TypeOf ctrl Is DataGridView Then
            ApplyDataGridView(DirectCast(ctrl, DataGridView))

        ElseIf TypeOf ctrl Is TreeView Then
            Dim tv = DirectCast(ctrl, TreeView)
            tv.BackColor = ListBackColor
            tv.ForeColor = ListForeColor
            tv.LineColor = BorderColor

        ElseIf TypeOf ctrl Is ListView Then
            Dim lv = DirectCast(ctrl, ListView)
            lv.BackColor = ListBackColor
            lv.ForeColor = ListForeColor

        ElseIf TypeOf ctrl Is TextBox Then
            ctrl.BackColor = InputBackColor
            ctrl.ForeColor = InputForeColor

        ElseIf TypeOf ctrl Is ComboBox Then
            Dim cbo = DirectCast(ctrl, ComboBox)
            cbo.BackColor = InputBackColor
            cbo.ForeColor = InputForeColor
            cbo.FlatStyle = If(IsDarkMode, FlatStyle.Flat, FlatStyle.Standard)

        ElseIf TypeOf ctrl Is NumericUpDown Then
            ctrl.BackColor = InputBackColor
            ctrl.ForeColor = InputForeColor

        ElseIf TypeOf ctrl Is Button Then
            ApplyButton(DirectCast(ctrl, Button))

        ElseIf TypeOf ctrl Is LinkLabel Then
            Dim ll = DirectCast(ctrl, LinkLabel)
            ll.LinkColor = LinkColor
            ll.VisitedLinkColor = LinkColor
            ll.ActiveLinkColor = AccentBackColor
            ll.ForeColor = ForeColor

        ElseIf TypeOf ctrl Is Label Then
            Dim lbl = DirectCast(ctrl, Label)
            If IsSecondaryLabel(lbl) Then
                lbl.ForeColor = SecondaryForeColor
            Else
                lbl.ForeColor = ForeColor
            End If

        ElseIf TypeOf ctrl Is CheckBox OrElse TypeOf ctrl Is RadioButton Then
            ctrl.ForeColor = ForeColor
            ctrl.BackColor = ControlBackColor

        ElseIf TypeOf ctrl Is GroupBox Then
            ctrl.BackColor = ControlBackColor
            ctrl.ForeColor = ForeColor

        ElseIf TypeOf ctrl Is TabPage Then
            ctrl.BackColor = TabBackColor
            ctrl.ForeColor = ForeColor

        ElseIf TypeOf ctrl Is TabControl Then
            ' TabControl 自体は子の TabPage で色を設定

        ElseIf TypeOf ctrl Is Splitter Then
            ctrl.BackColor = SplitterColor

        ElseIf TypeOf ctrl Is Panel OrElse
               TypeOf ctrl Is FlowLayoutPanel OrElse
               TypeOf ctrl Is TableLayoutPanel Then
            ctrl.BackColor = ControlBackColor
            ctrl.ForeColor = ForeColor

        ElseIf TypeOf ctrl Is ProgressBar Then
            ' ProgressBar は OS テーマに委ねる

        End If
    End Sub

#End Region

#Region "コントロール別テーマ適用"

    Private Sub ApplyMenuStrip(ms As MenuStrip)
        ms.BackColor = MenuBackColor
        ms.ForeColor = MenuForeColor
        If IsDarkMode Then
            ms.Renderer = New DarkToolStripRenderer()
        Else
            ms.RenderMode = ToolStripRenderMode.Professional
        End If
        ApplyThemeToToolStripItems(ms.Items)
    End Sub

    Private Sub ApplyToolStrip(ts As ToolStrip)
        ts.BackColor = MenuBackColor
        ts.ForeColor = MenuForeColor
        If IsDarkMode Then
            ts.Renderer = New DarkToolStripRenderer()
        Else
            ts.RenderMode = ToolStripRenderMode.Professional
        End If
        ApplyThemeToToolStripItems(ts.Items)
    End Sub

    Private Sub ApplyStatusStrip(ss As StatusStrip)
        ss.BackColor = StatusBackColor
        ss.ForeColor = ForeColor
        If IsDarkMode Then
            ss.Renderer = New DarkToolStripRenderer()
        Else
            ss.RenderMode = ToolStripRenderMode.Professional
        End If
        For Each item As ToolStripItem In ss.Items
            item.ForeColor = ForeColor
            item.BackColor = StatusBackColor
        Next
    End Sub

    Private Sub ApplyThemeToToolStripItems(items As ToolStripItemCollection)
        For Each item As ToolStripItem In items
            item.ForeColor = MenuForeColor
            item.BackColor = MenuBackColor
            If TypeOf item Is ToolStripDropDownItem Then
                Dim ddi = DirectCast(item, ToolStripDropDownItem)
                If ddi.HasDropDownItems Then
                    If IsDarkMode Then
                        ddi.DropDown.BackColor = MenuBackColor
                        ddi.DropDown.ForeColor = MenuForeColor
                    End If
                    ApplyThemeToToolStripItems(ddi.DropDownItems)
                End If
            End If
        Next
    End Sub

    Private Sub ApplyDataGridView(dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BackgroundColor = GridBackColor
        dgv.DefaultCellStyle.BackColor = GridBackColor
        dgv.DefaultCellStyle.ForeColor = GridCellForeColor
        dgv.DefaultCellStyle.SelectionBackColor = AccentBackColor
        dgv.DefaultCellStyle.SelectionForeColor = AccentForeColor
        dgv.AlternatingRowsDefaultCellStyle.BackColor = GridAlternateRowColor
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = GridCellForeColor
        dgv.ColumnHeadersDefaultCellStyle.BackColor = GridHeaderBackColor
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderForeColor
        dgv.RowHeadersDefaultCellStyle.BackColor = GridHeaderBackColor
        dgv.RowHeadersDefaultCellStyle.ForeColor = GridHeaderForeColor
        dgv.GridColor = GridLineColor
    End Sub

    Private Sub ApplyButton(btn As Button)
        ' アクセントボタン (検索ボタン等)
        If btn.Name = "buttonSearch" OrElse btn.Name = "btnSubmit" Then
            btn.BackColor = AccentBackColor
            btn.ForeColor = AccentForeColor
            btn.FlatStyle = FlatStyle.Flat
            btn.FlatAppearance.BorderColor = AccentBackColor
        Else
            If IsDarkMode Then
                btn.BackColor = ControlBackColor
                btn.ForeColor = ForeColor
                btn.FlatStyle = FlatStyle.Flat
                btn.FlatAppearance.BorderColor = BorderColor
            Else
                btn.BackColor = SystemColors.Control
                btn.ForeColor = SystemColors.ControlText
                btn.FlatStyle = FlatStyle.Standard
                btn.UseVisualStyleBackColor = True
            End If
        End If
    End Sub

#End Region

#Region "セカンダリラベル判定"

    ''' <summary>
    ''' ErrorReportDialog のシステム情報ラベル群を判定
    ''' </summary>
    Private Function IsSecondaryLabel(lbl As Label) As Boolean
        Dim name = lbl.Name
        If String.IsNullOrEmpty(name) Then Return False
        Return name = "lblContactHint" OrElse
               name = "lblSysInfoHeader" OrElse
               name.StartsWith("lblVersion") OrElse
               name.StartsWith("lblDllVersion") OrElse
               name.StartsWith("lblOS") OrElse
               name.StartsWith("lblDotNet") OrElse
               name.StartsWith("lblArch") OrElse
               name.StartsWith("lblLocale") OrElse
               name.StartsWith("lblDpi") OrElse
               name.StartsWith("lblMemory") OrElse
               name.StartsWith("lblScreen") OrElse
               name.StartsWith("lblDumpInfo")
    End Function

#End Region

#Region "DarkToolStripRenderer"

    Friend Class DarkToolStripRenderer
        Inherits ToolStripProfessionalRenderer

        Public Sub New()
            MyBase.New(New DarkColorTable())
        End Sub

        Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
            e.TextColor = ThemeManager.MenuForeColor
            MyBase.OnRenderItemText(e)
        End Sub

        Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
            Using brush As New SolidBrush(ThemeManager.MenuBackColor)
                e.Graphics.FillRectangle(brush, e.AffectedBounds)
            End Using
        End Sub

        Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
            If e.Item.Selected OrElse e.Item.Pressed Then
                Using brush As New SolidBrush(ThemeManager.AccentBackColor)
                    e.Graphics.FillRectangle(brush, New Rectangle(Point.Empty, e.Item.Size))
                End Using
            Else
                Using brush As New SolidBrush(ThemeManager.MenuBackColor)
                    e.Graphics.FillRectangle(brush, New Rectangle(Point.Empty, e.Item.Size))
                End Using
            End If
        End Sub

        Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
            Using pen As New Pen(ThemeManager.BorderColor)
                Dim y = e.Item.Height \ 2
                e.Graphics.DrawLine(pen, 0, y, e.Item.Width, y)
            End Using
        End Sub

        Protected Overrides Sub OnRenderImageMargin(e As ToolStripRenderEventArgs)
            Using brush As New SolidBrush(ThemeManager.MenuBackColor)
                e.Graphics.FillRectangle(brush, e.AffectedBounds)
            End Using
        End Sub
    End Class

    Friend Class DarkColorTable
        Inherits ProfessionalColorTable

        Public Overrides ReadOnly Property MenuStripGradientBegin As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuStripGradientEnd As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripGradientBegin As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripGradientEnd As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ToolStripGradientMiddle As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelected As Color
            Get
                Return ThemeManager.AccentBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemBorder As Color
            Get
                Return ThemeManager.AccentBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuBorder As Color
            Get
                Return ThemeManager.BorderColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
            Get
                Return ThemeManager.AccentBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
            Get
                Return ThemeManager.AccentBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color
            Get
                Return ThemeManager.AccentBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color
            Get
                Return ThemeManager.AccentBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property SeparatorDark As Color
            Get
                Return ThemeManager.BorderColor
            End Get
        End Property

        Public Overrides ReadOnly Property SeparatorLight As Color
            Get
                Return ThemeManager.MenuBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property StatusStripGradientBegin As Color
            Get
                Return ThemeManager.StatusBackColor
            End Get
        End Property

        Public Overrides ReadOnly Property StatusStripGradientEnd As Color
            Get
                Return ThemeManager.StatusBackColor
            End Get
        End Property
    End Class

#End Region

#Region "テーマ切り替え・設定"

    Public Sub ToggleTheme()
        If CurrentTheme = ThemeMode.Light Then
            CurrentTheme = ThemeMode.Dark
        Else
            CurrentTheme = ThemeMode.Light
        End If
        UpdatePalette()
        SaveThemeToSettings()
    End Sub

    Public Sub LoadThemeFromSettings()
        If My.Settings.DarkMode Then
            CurrentTheme = ThemeMode.Dark
        Else
            CurrentTheme = ThemeMode.Light
        End If
        UpdatePalette()
    End Sub

    Public Sub SaveThemeToSettings()
        My.Settings.DarkMode = (CurrentTheme = ThemeMode.Dark)
        My.Settings.Save()
    End Sub

    Public Sub ApplyThemeToAllForms()
        For Each frm As Form In Application.OpenForms
            ApplyTheme(frm)
        Next
    End Sub

#End Region

End Module
