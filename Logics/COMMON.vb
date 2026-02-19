Public Class COMMON
    ''' <summary>
    ''' ToolStripStatusLabelにテキストを設定する
    ''' </summary>
    ''' <param name="text"></param>
    Public Shared Sub Set_StatusLavel(text As String)
        'ステータスラベルのテキストを更新する
        Oracle_DUMP_Viewer.ToolStripStatusLabel.Text = text

        Application.DoEvents() ' UIの更新を強制
    End Sub

    ''' <summary>
    ''' ToolStripStatusLabelのテキストをリセットする
    ''' </summary>
    Public Shared Sub ReSet_StatusLavel()
        'ライセンス認証状態に応じてステータスラベルを更新
        Dim holder As String = LICENSE.GetLicenseHolder()
        If String.IsNullOrEmpty(holder) Then
            Oracle_DUMP_Viewer.ToolStripStatusLabel.Text = "Oracle DUMP Viewer - ライセンス未認証"
        Else
            Oracle_DUMP_Viewer.ToolStripStatusLabel.Text = $"Oracle DUMP Viewer - {holder}"
        End If

        Application.DoEvents() ' UIの更新を強制
    End Sub

    ''' <summary>
    ''' プログレスバー更新処理
    ''' </summary>
    ''' <param name="currentLine"></param>
    ''' <param name="totalLines"></param>
    Public Shared Sub UpdateProgress(current As Long, total As Long, startTime As DateTime)
        Try
            ' 現在の時間と経過時間を取得
            Dim currentTime As DateTime = DateTime.Now
            Dim elapsedTime As TimeSpan = currentTime - startTime

            ' 完了率を計算
            Dim completionPercentage As Double = current / total

            ' 残り時間を計算
            Dim estimatedRemainingTime As TimeSpan = TimeSpan.Zero
            Dim estimatedCompletionTime As DateTime = currentTime

            If completionPercentage > 0 Then
                ' 総予想時間 = 経過時間 / 完了率
                Dim estimatedTotalTime As TimeSpan = TimeSpan.FromTicks(CLng(elapsedTime.Ticks / completionPercentage))
                estimatedRemainingTime = estimatedTotalTime - elapsedTime
                estimatedCompletionTime = currentTime.Add(estimatedRemainingTime)
            End If

            ' 処理速度を計算
            Dim linesPerSecond As Double = If(elapsedTime.TotalSeconds > 0, current / elapsedTime.TotalSeconds, 0)

            ' ステータスラベルに詳細情報を表示
            Dim statusMessage As String = BuildStatusMessage(current, total, elapsedTime,
                                                           estimatedRemainingTime, linesPerSecond,
                                                           estimatedCompletionTime, completionPercentage)

            COMMON.Set_StatusLavel(statusMessage)

            Oracle_DUMP_Viewer.ToolStripProgressBar.Value = current


        Catch ex As Exception
            Console.WriteLine($"プログレス更新エラー: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' ステータスメッセージを構築
    ''' </summary>
    Private Shared Function BuildStatusMessage(currentLine As Long, totalLines As Long,
                                             elapsedTime As TimeSpan, estimatedRemainingTime As TimeSpan,
                                             linesPerSecond As Double, estimatedCompletionTime As DateTime,
                                             completionPercentage As Double) As String
        Try
            Dim elapsedStr As String = FormatTimeSpan(elapsedTime)
            Dim remainingStr As String = FormatTimeSpan(estimatedRemainingTime)

            ' ステータスメッセージを組み立て
            Dim message As String = $"処理中... {currentLine:N0}/{totalLines:N0}行 ({completionPercentage:P1}) | " &
                                   $"経過: {elapsedStr} | 残り: {remainingStr} | " &
                                   $"速度: {linesPerSecond:N0}行/秒 | 完了予定: {estimatedCompletionTime:HH:mm:ss}"

            Return message

        Catch ex As Exception
            Return $"処理中... {currentLine:N0}行目"
        End Try
    End Function

    ''' <summary>
    ''' TimeSpanを読みやすい形式にフォーマット
    ''' </summary>
    Private Shared Function FormatTimeSpan(timeSpan As TimeSpan) As String
        Try
            If timeSpan.TotalDays >= 1 Then
                Return $"{timeSpan.Days}日{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}"
            ElseIf timeSpan.TotalHours >= 1 Then
                Return $"{timeSpan.Hours}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}"
            ElseIf timeSpan.TotalMinutes >= 1 Then
                Return $"{timeSpan.Minutes}:{timeSpan.Seconds:D2}"
            Else
                Return $"{timeSpan.Seconds}秒"
            End If
        Catch
            Return "計算中..."
        End Try
    End Function


    Public Shared Sub setProgressBarMax(total As Integer)
        Oracle_DUMP_Viewer.ToolStripProgressBar.Maximum = total
        Oracle_DUMP_Viewer.ToolStripProgressBar.Value = 0
        Oracle_DUMP_Viewer.ToolStripProgressBar.Visible = True
    End Sub
    ''' <summary>
    ''' プログレスバーをリセットする
    ''' </summary>
    Public Shared Sub ResetProgressBar()
        Oracle_DUMP_Viewer.ToolStripProgressBar.Value = 0
        Oracle_DUMP_Viewer.ToolStripProgressBar.Visible = False
    End Sub

End Class
