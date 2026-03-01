Imports System.Net.Http
Imports System.Text
Imports System.Text.Json

''' <summary>
''' エラー報告の送信ロジック
''' Cloudflare Worker (report.odv.dev) へ HTTP POST でエラー報告を送信する
''' </summary>
Public Class ErrorReportLogic

    Private Const ReportEndpoint As String = "https://report.odv.dev/"

    ''' <summary>
    ''' エラー報告の送信結果
    ''' </summary>
    Public Class ReportResult
        Public Property Success As Boolean
        Public Property IssueUrl As String
        Public Property IssueNumber As Integer
        Public Property ErrorMessage As String
    End Class

    ''' <summary>
    ''' エラー報告を Cloudflare Worker に送信する
    ''' </summary>
    Public Shared Async Function SubmitReportAsync(
        title As String,
        description As String,
        contact As String,
        appVersion As String,
        osVersion As String,
        dotnetVersion As String
    ) As Task(Of ReportResult)

        Using client As New HttpClient()
            client.DefaultRequestHeaders.UserAgent.ParseAdd("OraDB-DUMP-Viewer")
            client.Timeout = TimeSpan.FromSeconds(15)

            Dim payload As New Dictionary(Of String, String) From {
                {"title", title},
                {"description", description},
                {"contact", contact},
                {"app_version", appVersion},
                {"os_version", osVersion},
                {"dotnet_version", dotnetVersion}
            }

            Dim jsonContent As New StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            )

            Dim response = Await client.PostAsync(ReportEndpoint, jsonContent)
            Dim responseBody = Await response.Content.ReadAsStringAsync()

            Using doc = JsonDocument.Parse(responseBody)
                Dim root = doc.RootElement
                Dim result As New ReportResult()

                Dim successProp As JsonElement
                If root.TryGetProperty("success", successProp) Then
                    result.Success = successProp.GetBoolean()
                End If

                If result.Success Then
                    Dim urlProp As JsonElement
                    If root.TryGetProperty("issue_url", urlProp) Then
                        result.IssueUrl = urlProp.GetString()
                    End If

                    Dim numProp As JsonElement
                    If root.TryGetProperty("issue_number", numProp) Then
                        result.IssueNumber = numProp.GetInt32()
                    End If
                Else
                    Dim errProp As JsonElement
                    If root.TryGetProperty("error", errProp) Then
                        result.ErrorMessage = TranslateError(errProp.GetString(), response.StatusCode)
                    Else
                        result.ErrorMessage = $"サーバーエラー (HTTP {CInt(response.StatusCode)})"
                    End If
                End If

                Return result
            End Using
        End Using
    End Function

    ''' <summary>
    ''' エラーコードを日本語メッセージに変換する
    ''' </summary>
    Private Shared Function TranslateError(errorCode As String, statusCode As Net.HttpStatusCode) As String
        Select Case errorCode
            Case "rate_limit_exceeded"
                Return "送信回数の制限に達しました。しばらく時間をおいてからお試しください。"
            Case "invalid_json"
                Return "リクエスト形式が不正です。"
            Case "internal_error"
                Return "サーバー内部エラーが発生しました。しばらく時間をおいてからお試しください。"
            Case Else
                If errorCode IsNot Nothing AndAlso errorCode.Length > 0 Then
                    Return errorCode
                End If
                Return $"不明なエラーが発生しました (HTTP {CInt(statusCode)})"
        End Select
    End Function

End Class
