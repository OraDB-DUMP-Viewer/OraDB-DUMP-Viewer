''' <summary>
''' ダークテーマ用の ToolStrip/MenuStrip/StatusStrip レンダラー。
''' VS Code風の配色でメニューバー・ツールバー・ステータスバーを描画する。
''' </summary>
Public Class DarkToolStripRenderer
    Inherits ToolStripProfessionalRenderer

    Private ReadOnly _menuBackColor As Color = ThemeManager.DarkMenuBackColor
    Private ReadOnly _foreColor As Color = ThemeManager.DarkForeColor
    Private ReadOnly _accentColor As Color = ThemeManager.DarkAccentColor
    Private ReadOnly _borderColor As Color = ThemeManager.DarkBorderColor
    Private ReadOnly _statusBarBackColor As Color = ThemeManager.DarkStatusBarBackColor

    Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)
        If TypeOf e.ToolStrip Is StatusStrip Then
            Using brush As New SolidBrush(_statusBarBackColor)
                e.Graphics.FillRectangle(brush, e.AffectedBounds)
            End Using
        Else
            Using brush As New SolidBrush(_menuBackColor)
                e.Graphics.FillRectangle(brush, e.AffectedBounds)
            End Using
        End If
    End Sub

    Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
        If e.Item.Selected OrElse e.Item.Pressed Then
            Using brush As New SolidBrush(_accentColor)
                e.Graphics.FillRectangle(brush, New Rectangle(Point.Empty, e.Item.Size))
            End Using
        End If
    End Sub

    Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)
        Dim btn = TryCast(e.Item, ToolStripButton)
        If btn IsNot Nothing AndAlso (btn.Selected OrElse btn.Checked) Then
            Using brush As New SolidBrush(_accentColor)
                e.Graphics.FillRectangle(brush, New Rectangle(Point.Empty, e.Item.Size))
            End Using
        End If
    End Sub

    Protected Overrides Sub OnRenderDropDownButtonBackground(e As ToolStripItemRenderEventArgs)
        If e.Item.Selected OrElse e.Item.Pressed Then
            Using brush As New SolidBrush(_accentColor)
                e.Graphics.FillRectangle(brush, New Rectangle(Point.Empty, e.Item.Size))
            End Using
        End If
    End Sub

    Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)
        e.TextColor = If(TypeOf e.ToolStrip Is StatusStrip, Color.White, _foreColor)
        MyBase.OnRenderItemText(e)
    End Sub

    Protected Overrides Sub OnRenderSeparator(e As ToolStripSeparatorRenderEventArgs)
        Dim bounds = New Rectangle(Point.Empty, e.Item.Size)
        Using pen As New Pen(_borderColor)
            If e.Vertical Then
                Dim x = bounds.Width \ 2
                e.Graphics.DrawLine(pen, x, bounds.Top + 4, x, bounds.Bottom - 4)
            Else
                Dim y = bounds.Height \ 2
                e.Graphics.DrawLine(pen, bounds.Left + 2, y, bounds.Right - 2, y)
            End If
        End Using
    End Sub

    Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)
        ' ダークテーマではボーダーを描画しない（VS Code風）
    End Sub

    Protected Overrides Sub OnRenderImageMargin(e As ToolStripRenderEventArgs)
        Using brush As New SolidBrush(_menuBackColor)
            e.Graphics.FillRectangle(brush, e.AffectedBounds)
        End Using
    End Sub

    Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)
        e.ArrowColor = _foreColor
        MyBase.OnRenderArrow(e)
    End Sub

    Protected Overrides Sub OnRenderItemCheck(e As ToolStripItemImageRenderEventArgs)
        Using brush As New SolidBrush(_accentColor)
            e.Graphics.FillRectangle(brush, e.ImageRectangle)
        End Using
        ' チェックマークを白で描画
        Using pen As New Pen(Color.White, 2)
            Dim r = e.ImageRectangle
            Dim cx = r.X + r.Width \ 2
            Dim cy = r.Y + r.Height \ 2
            e.Graphics.DrawLines(pen, {
                New Point(cx - 4, cy),
                New Point(cx - 1, cy + 3),
                New Point(cx + 4, cy - 3)
            })
        End Using
    End Sub
End Class
