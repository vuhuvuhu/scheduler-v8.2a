Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.IO
Imports System.Text.RegularExpressions

Public Class FDaf

    ' ✅ Google Sheets API-ს კავშირი
    Private Function GetSheetsService() As SheetsService
        Dim credential As GoogleCredential
        Using stream = New FileStream("sinuous-pact-454212-m3-d64e059c8663.json", FileMode.Open, FileAccess.Read)
            credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets)
        End Using
        Return New SheetsService(New BaseClientService.Initializer() With {
            .HttpClientInitializer = credential,
            .ApplicationName = "Scheduler VB"
        })
    End Function

    Private spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"

    ' ✅ ფორმის ჩატვირთვა – გვითვლის ახალ ნომერს
    Private Sub FDaf_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Program!A2:A")
        Dim res = req.Execute()
        Dim maxRow = If(res.Values IsNot Nothing, res.Values.Count + 1, 1)
        LN1.Text = maxRow.ToString()
        TName.Text = ""
    End Sub

    ' ✅ სახელის ცვლილებისას ვამოწმებთ გამეორებას
    Private Sub TName_TextChanged(sender As Object, e As EventArgs) Handles TName.TextChanged
        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Program!B2:B")
        Dim res = req.Execute()
        Dim exists = res.Values.Any(Function(r) r(0).ToString().Trim().ToLower() = TName.Text.Trim().ToLower())
        TName.BackColor = If(exists, System.Drawing.Color.LightCoral, System.Drawing.Color.LightGreen)
        BtnAdd.Visible = Not exists AndAlso TName.Text.Trim() <> ""
    End Sub

    ' ✅ ღილაკზე დაჭერისას ჩანაწერის დამატება
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim ln As Integer = Val(LN1.Text)
        Dim codeC As String = "611" & (ln - 1).ToString()

        Dim newRow As New List(Of Object) From {
            ln.ToString(),
            TName.Text.Trim(),
            codeC
        }

        Dim valueRange As New ValueRange()
        valueRange.Values = New List(Of IList(Of Object)) From {newRow}

        Dim service = GetSheetsService()
        Dim appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, "DB-Program!A1")
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
        appendRequest.Execute()

        ' ჩამატება Form2-ში ComboBox-ში
        If Application.OpenForms().OfType(Of Form2).Any() Then
            Dim f2 = Application.OpenForms().OfType(Of Form2).First()
            f2.CBDaf.Items.Add(TName.Text.Trim())
            f2.CBDaf.Sorted = True
        End If

        MessageBox.Show("დაფინანსება დამატებულია!", "✔", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        Me.Close()
    End Sub
End Class
