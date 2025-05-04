' 📄 Form1.vb – სუფთა ვერსია UserControl მოდელისთვის
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Util.Store
Imports Google.Apis.Services
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Newtonsoft.Json.Linq
Imports System.Net.Http.Headers
Imports System.Net.Http
Imports System.IO
Imports System.Threading

Public Class Form1

    ' 🔐 გლობალური ცვლადები ავტორიზაციისთვის და Sheets-ისთვის
    Dim userEmail As String = ""
    Public userRoleID As Integer = 6
    Dim userAcount As String = ""
    Dim tokenPath As String = "token.json"

    ' 🌐 Google Sheets-ის გლობალური ცვლადები, რომლებსაც ყველა ფორმიდან მივწვდებით
    Public Shared service As SheetsService
    Public Shared spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"

    Public UC_Schedule1 As UC_Schedule

    Private UC_Calendar1 As UC_Calendar

    Private UC_Calendar_day1 As UC_Calendar_day

    ' ფორმის ჩატვირთვისას საწყისი სტატუსი
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LUser.Text = "გთხოვთ გაიაროთ ავტორიზაცია"
        BtnLogin.Text = "ავტორიზაცია"
        ShowControl(New UC_Home(userEmail))

        UpdateMenuAccess()
    End Sub
    Private Sub UpdateMenuAccess()
        განრიგიToolStripMenuItem.Enabled = Not (String.IsNullOrEmpty(userEmail) OrElse userRoleID = 6)
        კალენდარიToolStripMenuItem.Enabled = Not (String.IsNullOrEmpty(userEmail) OrElse userRoleID = 6)
    End Sub
    ' ფუნქცია რომელიც Main პანელში ცვლის UserControl-ს
    Public Sub ShowControl(newControl As UserControl)
        PnlMain.Controls.Clear()
        newControl.Dock = DockStyle.Fill
        PnlMain.Controls.Add(newControl)
        'Control.BringToFront()
    End Sub

    ' ✅ ავტორიზაცია / გამოსვლა
    Private Async Sub BtnLogin_Click(sender As Object, e As EventArgs) Handles BtnLogin.Click
        If BtnLogin.Text = "ავტორიზაცია" Then
            Try
                ' მომხმარებლის ავტორიზაცია Google OAuth-ით
                Dim cred = Await AuthorizeUserAsync()

                If cred IsNot Nothing AndAlso cred.Token IsNot Nothing Then
                    Dim token = cred.Token.AccessToken
                    Try
                        userEmail = Await GetUserEmailAsync(token)
                    Catch ex As Exception
                        ' თუ მივიღეთ Invalid Credentials ან მსგავსი შეცდომა
                        If ex.Message.Contains("Invalid Credentials") OrElse ex.Message.Contains("invalid_request") Then
                            If Directory.Exists(tokenPath) Then
                                Directory.Delete(tokenPath, True)
                            End If
                            ' ხელახალი ავტორიზაცია
                            BtnLogin_Click(sender, e)
                            Return
                        Else
                            Throw ' სხვა შეცდომის შემთხვევაში გადააგდოს უკან
                        End If
                    End Try
                    ' Google Sheets API-ს ინიციალიზაცია
                    service = New SheetsService(New BaseClientService.Initializer() With {
                        .HttpClientInitializer = cred,
                        .ApplicationName = "Scheduler"
                    })

                    ' მომხმარებლის დამატება ან განახლება DB-Users ცხრილში
                    CheckOrInsertUser(service)

                    ' ეკრანზე ელფოსტის გამოჩენა
                    LUser.Text = userEmail
                    BtnLogin.Text = "გასვლა"
                    ShowControl(New UC_Home(userEmail))
                Else
                    MessageBox.Show("ავტორიზაცია ვერ შესრულდა.")
                End If

            Catch ex As Exception
                Clipboard.SetText(ex.Message)
                MessageBox.Show("შეცდომა ავტორიზაციისას: " & ex.Message)
            End Try

        Else
            ' გამოსვლა - token.json წაშლა და სტატუსის განულება
            If Directory.Exists(tokenPath) Then
                Directory.Delete(tokenPath, True)
            End If
            userEmail = ""
            LUser.Text = "გთხოვთ გაიაროთ ავტორიზაცია"
            BtnLogin.Text = "ავტორიზაცია"
            ShowControl(New UC_Home(userEmail))
        End If
        UpdateMenuAccess()
    End Sub

    ' ზედა მენიუს ფანჯრები
    Private Sub საწყისიToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles საწყისიToolStripMenuItem.Click
        ShowControl(New UC_Home(userEmail))
    End Sub
    Private Sub განრიგიToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles განრიგიToolStripMenuItem1.Click
        If UC_Schedule1 Is Nothing Then
            UC_Schedule1 = New UC_Schedule()
        End If
        ShowControl(UC_Schedule1)
    End Sub
    ' 🔧 ავტორიზაციის ფუნქცია - იღებს მომხმარებლის უფლებას
    Private Async Function AuthorizeUserAsync() As Task(Of UserCredential)
        Dim secrets = New ClientSecrets With {
            .ClientId = "1009555018809-q11pafnnb4qbckuesgkkhlli7ik6t3jm.apps.googleusercontent.com",
            .ClientSecret = "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"
        }

        Dim scopes = {
            "openid",
            "email",
            "profile",
            "https://www.googleapis.com/auth/spreadsheets"
        }

        If Directory.Exists(tokenPath) = False Then
            Directory.CreateDirectory(tokenPath)
        End If

        Return Await GoogleWebAuthorizationBroker.AuthorizeAsync(
            secrets, scopes, "user", CancellationToken.None,
            New FileDataStore(tokenPath, True))
    End Function

    ' 📧 ელფოსტის გამოთხოვა Access Token-ის საფუძველზე
    Private Async Function GetUserEmailAsync(token As String) As Task(Of String)
        Dim http = New HttpClient()
        http.DefaultRequestHeaders.Authorization = New AuthenticationHeaderValue("Bearer", token)

        Dim response = Await http.GetAsync("https://openidconnect.googleapis.com/v1/userinfo")
        Dim responseText = Await response.Content.ReadAsStringAsync()

        If response.IsSuccessStatusCode Then
            Return JObject.Parse(responseText)("email").ToString()
        Else
            Throw New Exception("ელფოსტის მიღება ვერ მოხერხდა: " & responseText)
        End If
        UpdateMenuAccess()
    End Function

    ' 📄 მომხმარებლის შემოწმება და საჭიროების შემთხვევაში დამატება DB-Users-ში
    Private Sub CheckOrInsertUser(service As SheetsService)
        Dim range As String = "DB-Users!A2:F"
        Dim request = service.Spreadsheets.Values.Get(spreadsheetId, range)
        Dim response = request.Execute()
        Dim users = response.Values

        Dim found As Boolean = False
        Dim now As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")

        If users IsNot Nothing Then
            For i = 1 To users.Count - 1 ' i = 1 → გამოტოვებს სათაურს
                If users(i)(1).ToString().Trim().ToLower() = userEmail.Trim().ToLower() Then
                    ' მომხმარებელი უკვე არსებობს – განვაახლოთ Last_login
                    Dim updateRange = $"DB-Users!F{i + 1}" ' +1 რადგან A1 სათაურია
                    Dim updateValue As New ValueRange()
                    updateValue.Values = New List(Of IList(Of Object)) From {
                New List(Of Object) From {now}
            }
                    Dim updateReq = service.Spreadsheets.Values.Update(updateValue, spreadsheetId, updateRange)
                    updateReq.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW
                    updateReq.Execute()

                    ' ამოვიღოთ მისი როლი და Acount
                    userRoleID = CInt(users(i)(2).ToString())
                    If users(i).Count > 3 Then userAcount = users(i)(3).ToString()

                    found = True
                    Exit For
                End If
            Next
        End If


        If users IsNot Nothing Then
            For i = 0 To users.Count - 1
                If users(i)(1).ToString().Trim().ToLower() = userEmail.Trim().ToLower() Then
                    ' მომხმარებელი არსებობს → განვაახლოთ ბოლო შესვლა
                    Dim updateRange = $"DB-Users!F{i + 2}"
                    Dim updateValue As New ValueRange()
                    updateValue.Values = New List(Of IList(Of Object)) From {
                        New List(Of Object) From {now}
                    }
                    Dim updateReq = service.Spreadsheets.Values.Update(updateValue, spreadsheetId, updateRange)
                    updateReq.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW
                    updateReq.Execute()

                    ' ამოვიღოთ RoleID და Acount
                    userRoleID = CInt(users(i)(2).ToString())
                    If users(i).Count > 3 Then userAcount = users(i)(3).ToString()

                    found = True
                    Exit For
                End If
            Next
        End If

        If Not found Then
            ' ახალი მომხმარებლის დამატება (RoleID = 6 by default)
            Dim newID As Integer = If(users IsNot Nothing, users.Count + 1, 1)
            Dim insertData As New ValueRange()
            insertData.Values = New List(Of IList(Of Object)) From {
                New List(Of Object) From {
                    newID, userEmail, 6, "", now, now
                }
            }
            Dim appendReq = service.Spreadsheets.Values.Append(insertData, spreadsheetId, range)
            appendReq.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.RAW
            appendReq.Execute()

            userRoleID = 6
            userAcount = ""
        End If
    End Sub

    Private Sub კვირაToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles კვირაToolStripMenuItem.Click
        If UC_Calendar1 Is Nothing Then
            UC_Calendar1 = New UC_Calendar()
        End If
        ShowControl(UC_Calendar1)
    End Sub

    Private Sub დღეToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles დღეToolStripMenuItem.Click
        If UC_Calendar_day1 Is Nothing Then
            UC_Calendar_day1 = New UC_Calendar_day()
        End If
        ShowControl(UC_Calendar_day1)
    End Sub
End Class
