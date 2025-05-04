Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.IO
Imports System.Text.RegularExpressions

Public Class FPer

    Private spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"

    ' ✅ Google Sheets API-ის კავშირი
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

    ' ✅ ფორმის ჩატვირთვა – ითვლის ახალ ID-ს
    Private Sub FPer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Personal!A2:A")
        Dim res = req.Execute()
        Dim nextID = If(res.Values IsNot Nothing, res.Values.Count + 1, 1)
        LN.Text = nextID.ToString()
        BtnAdd.Visible = False
    End Sub

    ' ✅ ველების ცვლილებისას ვალიდაცია
    Private Sub FieldChanged(sender As Object, e As EventArgs) Handles TName.TextChanged, TSurname.TextChanged
        ValidateFields()
    End Sub

    ' ✅ ვალიდაციის ფუნქცია
    Private Sub ValidateFields()
        Dim name = TName.Text.Trim()
        Dim surname = TSurname.Text.Trim()
        Dim valid = False

        If name <> "" AndAlso surname <> "" Then
            Dim service = GetSheetsService()
            Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Personal!B2:C")
            Dim res = req.Execute()

            Dim exists = res.Values.Any(Function(r) r(0).ToString().Trim() = name AndAlso r(1).ToString().Trim() = surname)
            If exists Then
                TName.BackColor = System.Drawing.Color.LightCoral
                TSurname.BackColor = System.Drawing.Color.LightCoral
                BtnAdd.Visible = False
            Else
                TName.BackColor = System.Drawing.Color.LightGreen
                TSurname.BackColor = System.Drawing.Color.LightGreen
                valid = True
            End If
        Else
            TName.BackColor = System.Drawing.Color.White
            TSurname.BackColor = System.Drawing.Color.White
        End If

        BtnAdd.Visible = valid
    End Sub

    ' ✅ პირადი ნომერი – მხოლოდ 11 ციფრი
    Private Sub TPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TPN.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TPN_TextChanged(sender As Object, e As EventArgs) Handles TPN.TextChanged
        If TPN.Text.Length > 11 Then
            TPN.Text = TPN.Text.Substring(0, 11)
            TPN.SelectionStart = TPN.Text.Length
        End If
    End Sub

    ' ✅ ტელეფონი – ნებადართულია მხოლოდ ციფრები, +, ( ), და space
    Private Sub TTel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TTel.KeyPress
        Dim ch = e.KeyChar
        If Not (Char.IsDigit(ch) Or ch = "+"c Or ch = "("c Or ch = ")"c Or ch = " "c Or Char.IsControl(ch)) Then
            e.Handled = True
        End If
    End Sub

    ' ✅ დამატება
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim newRow As New List(Of Object) From {
            LN.Text,
            TName.Text.Trim(),
            TSurname.Text.Trim(),
            TPN.Text.Trim(),
            DTPBene.Value.ToString("dd.MM.yyyy"),
            TTel.Text.Trim(),
            TMail.Text.Trim()
        }

        Dim service = GetSheetsService()
        Dim vr = New ValueRange()
        vr.Values = New List(Of IList(Of Object)) From {newRow}

        Dim appendRequest = service.Spreadsheets.Values.Append(vr, spreadsheetId, "DB-Personal!A1")
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
        appendRequest.Execute()

        MessageBox.Show("თერაპევტი წარმატებით დაემატა!", "✔ წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Form2-ში ჩამატება
        If Application.OpenForms().OfType(Of Form2).Any() Then
            Dim f2 = Application.OpenForms().OfType(Of Form2).First()
            Dim fullName = TName.Text.Trim() & " " & TSurname.Text.Trim()
            f2.CBPer.Items.Add(fullName)
        End If

        Me.Close()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        Me.Close()
    End Sub
End Class
