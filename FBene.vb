' ✅ FBene ფორმის კოდი – ბენეფიციარის დამატება Google Sheets-ში

Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.IO

Public Class FBene

    Private spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"
    Private service As SheetsService

    Private Sub FBene_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        service = GetSheetsService()
        LoadNextBeneId()
    End Sub

    ' ✅ Google Sheets API კავშირი
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

    ' ✅ ჩასმისთვის მომდევნო ნომრის გამოთვლა
    Private Sub LoadNextBeneId()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Bene!A2:A")
        Dim res = req.Execute()
        Dim maxId = If(res.Values IsNot Nothing, res.Values.Count + 1, 1)
        LN.Text = maxId.ToString()
    End Sub

    ' ✅ ტექსტის ცვლილებაზე რეაგირება
    Private Sub TNameOrSurname_Changed(sender As Object, e As EventArgs) Handles TName.TextChanged, TSurname.TextChanged
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Bene!B2:C")
        Dim res = req.Execute()
        Dim exists = res.Values.Any(Function(r) r(0).ToString() = TName.Text.Trim() AndAlso r(1).ToString() = TSurname.Text.Trim())

        If exists Then
            TName.BackColor = System.Drawing.Color.LightCoral
            TSurname.BackColor = System.Drawing.Color.LightCoral
            BtnAdd.Visible = False
        ElseIf TName.Text.Trim() <> "" AndAlso TSurname.Text.Trim() <> "" Then
            TName.BackColor = System.Drawing.Color.LightGreen
            TSurname.BackColor = System.Drawing.Color.LightGreen
            BtnAdd.Visible = True
        Else
            TName.BackColor = System.Drawing.Color.White
            TSurname.BackColor = System.Drawing.Color.White
            BtnAdd.Visible = False
        End If
    End Sub

    ' ✅ შეყვანის შეზღუდვები – TPN და TCarmPN მხოლოდ 11 ციფრი
    Private Sub TPN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TPN.KeyPress, TCarmPN.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then e.Handled = True
    End Sub

    Private Sub TPN_TextChanged(sender As Object, e As EventArgs) Handles TPN.TextChanged, TCarmPN.TextChanged
        Dim box = CType(sender, TextBox)
        If box.Text.Length > 11 Then box.Text = box.Text.Substring(0, 11)
    End Sub

    ' ✅ შეყვანის შეზღუდვები – TTel (ციფრები, ფრჩხილები, პლიუსი, ცარიელი)
    Private Sub TTel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TTel.KeyPress
        Dim allowed = "0123456789 ()+"
        If Not Char.IsControl(e.KeyChar) AndAlso Not allowed.Contains(e.KeyChar) Then e.Handled = True
    End Sub

    ' ✅ BtnAdd_Click – ჩანაწერის დამატება DB-Bene-ში
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        Dim id = LN.Text.PadLeft(3, "0"c)
        Dim gender = If(RBM.Checked, "m", If(RBF.Checked, "f", ""))

        Dim newRow As New List(Of Object) From {
            LN.Text,
            TName.Text.Trim(),
            TSurname.Text.Trim(),
            "1410-" & id,
            "1412-" & id,
            TPN.Text.Trim(),
            DTPBene.Value.ToString("dd.MM.yyyy"),
            gender,
            TCarm.Text.Trim(),
            TCarmPN.Text.Trim(),
            "",
            TTel.Text.Trim(),
            TMail.Text.Trim()
        }

        Dim vr = New ValueRange()
        vr.Values = New List(Of IList(Of Object)) From {newRow}
        Dim req = service.Spreadsheets.Values.Append(vr, spreadsheetId, "DB-Bene!A1")
        req.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
        req.Execute()
        ' ჩანაწერის შემდეგ ჩაკეტვის წინ
        If Application.OpenForms().OfType(Of Form2).Any() Then
            Dim frm2 = CType(Application.OpenForms().OfType(Of Form2).First(), Form2)
            frm2.ReloadBeneComboBoxes()
        End If
        MessageBox.Show("ბენეფიციარი დამატებულია!", "✔ წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Me.Close()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        Me.Close()
    End Sub
End Class