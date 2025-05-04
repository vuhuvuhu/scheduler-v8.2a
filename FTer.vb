Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.IO

Public Class FTer

    ' Google Sheets კავშირი
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

    ' SpreadSheet ID
    Private spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"

    ' ფორმის ჩატვირთვისას დავსვათ ახალი ნომერი
    Private Sub FTer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Therapy!A2:A")
        Dim res = req.Execute()
        Dim nextId = If(res.Values IsNot Nothing, res.Values.Count + 1, 1)
        LN.Text = nextId.ToString()
    End Sub

    ' მხოლოდ როდესაც ჩაწერილია სახელი, გამოჩნდება BtnAdd
    Private Sub TName_TextChanged(sender As Object, e As EventArgs) Handles TName.TextChanged
        BtnAdd.Visible = TName.Text.Trim() <> ""
    End Sub

    ' ახალი თერაპიის დამატება
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim id As String = LN.Text.PadLeft(3, "0"c)
        Dim name As String = TName.Text.Trim()
        If name = "" Then
            MessageBox.Show("გთხოვთ შეიყვანოთ თერაპიის დასახელება!", "შეცდომა", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' ცხრილში დამატება
        Dim row As New List(Of Object) From {
            LN.Text,
            name,
            "6110-" & id,
            "6111-" & id,
            "6112-" & id
        }

        Dim vr As New ValueRange()
        vr.Values = New List(Of IList(Of Object)) From {row}

        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Append(vr, spreadsheetId, "DB-Therapy!A1")
        req.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
        req.Execute()

        MessageBox.Show("თერაპია წარმატებით დაემატა!", "✔ დამატება", MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Form2-ზე CBTer ComboBox-ში ჩასმა
        If Application.OpenForms().OfType(Of Form2).Any() Then
            Dim f2 = Application.OpenForms().OfType(Of Form2).First()
            f2.CBTer.Items.Add(name)
        End If

        Me.Close()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        Me.Close()
    End Sub
End Class
