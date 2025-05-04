' 📄 UC_Schedule.vb – განრიგის კონტროლი ფილტრებით და გვერდებად
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports System.Globalization

Public Class UC_Schedule

    Private currentPage As Integer = 1 ' მიმდინარე გვერდი ცხრილში

    Private Sub UC_Schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' საწყისი და საბოლოო თარიღის დაყენება
        DtpDan.Value = New Date(Date.Today.Year, 1, 1)
        DtpMde.Value = New Date(Date.Today.Year, 12, 31)

        ' აკრძალვა ცხრილში ხელით სტრიქონების დამატებაზე და რედაქტირებაზე
        DgvSchedule.AllowUserToAddRows = False
        DgvSchedule.ReadOnly = True

        ' ფილტრების შევსება
        LoadComboBoxes()

        ' ცხრილის შევსება
        LoadFilteredSchedule()
    End Sub

    ' 📌 ფილტრების ComboBox-ების შევსება Google Sheets-იდან უნიკალური მნიშვნელობებით
    Private Sub LoadComboBoxes()
        ' ყველა ComboBox-ზე ჩაწერა უნდა იყოს შეზღუდული მხოლოდ სიიდან არჩევით
        CBBeneName.DropDownStyle = ComboBoxStyle.DropDownList
        CBBeneSurname.DropDownStyle = ComboBoxStyle.DropDownList
        CBPer.DropDownStyle = ComboBoxStyle.DropDownList
        CBTer.DropDownStyle = ComboBoxStyle.DropDownList
        CBDaf.DropDownStyle = ComboBoxStyle.DropDownList
        CBSpace.DropDownStyle = ComboBoxStyle.DropDownList
        CBShes.DropDownStyle = ComboBoxStyle.DropDownList
        CBBeneName.Items.Clear()
        Dim nameList = GoogleSheetsHelper.GetUniqueColumnValues(Form1.service, Form1.spreadsheetId, "DB-Bene", 1)
        Dim sortedList = nameList.Distinct().OrderBy(Function(n) n).ToList()
        CBBeneName.Items.Add("ყველა")
        CBBeneName.Items.AddRange(sortedList.ToArray())
        AddHandler CBBeneName.SelectedIndexChanged, AddressOf CBBeneName_SelectedIndexChanged
        CBBeneName.SelectedIndex = 0

        CBBeneSurname.Items.Clear()
        CBBeneSurname.Items.Add("ყველა")
        CBBeneSurname.Items.AddRange(GoogleSheetsHelper.GetUniqueColumnValues(Form1.service, Form1.spreadsheetId, "DB-Bene", 2).ToArray())
        CBBeneSurname.SelectedIndex = 0

        CBPer.Items.Clear()
        CBPer.Items.Add("ყველა")
        Dim allTherapists = GoogleSheetsHelper.GetSheetData(Form1.service, Form1.spreadsheetId, "DB-Personal")
        Dim fullNames = allTherapists.
    Where(Function(r) r.Count >= 3).
    Select(Function(r) r(1).ToString().Trim() & " " & r(2).ToString().Trim()).
    Distinct().
    OrderBy(Function(n) n).
    ToArray()
        CBPer.Items.AddRange(fullNames)
        CBPer.SelectedIndex = 0

        CBTer.Items.Clear()
        CBTer.Items.Add("ყველა")
        CBTer.Items.AddRange(GoogleSheetsHelper.GetTherapyNames(Form1.service, Form1.spreadsheetId).ToArray())
        CBTer.SelectedIndex = 0

        CBDaf.Items.Clear()
        CBDaf.Items.Add("ყველა")
        CBDaf.Items.AddRange(GoogleSheetsHelper.GetFundingNames(Form1.service, Form1.spreadsheetId).ToArray())
        CBDaf.SelectedIndex = 0

        CBSpace.Items.Clear()
        CBSpace.Items.Add("ყველა")
        CBSpace.Items.AddRange(GoogleSheetsHelper.GetUniqueColumnValues(Form1.service, Form1.spreadsheetId, "DB-Space", 1).ToArray())
        CBSpace.SelectedIndex = 0

        CBShes.Items.Clear()
        CBShes.Items.AddRange(New String() {"ყველა", "დაგეგმილი", "შესრულებული", "გაცდენა არასაპატიო", "გაცდენა საპატიო", "პროგრამით გატარება", "გაუქმება"})
        CBShes.SelectedIndex = 0
    End Sub

    ' 📊 ცხრილის შევსება ფილტრების მიხედვით
    Public Sub LoadFilteredSchedule()
        DgvSchedule.Rows.Clear()

        ' თუ ჯერ სვეტები არ დამატებულა, ვამატებთ და ვუსვამთ ზომებს
        If DgvSchedule.Columns.Count = 0 Then
            With DgvSchedule.Columns
                .Add("N", "N")
                .Add("Tarigi", "თარიღი")
                .Add("Duri", "ხანგძლიობა")
                .Add("Bene", "ბენეფიციარი")
                .Add("Per", "თერაპევტი")
                .Add("Ter", "თერაპია")
                .Add("Group", "ჯგუფური")
                .Add("Space", "სივრცე")
                .Add("Price", "თანხა")
                .Add("Status", "სტატუსი")
                .Add("Program", "პროგრამა")
                .Add("Coment", "კომენტარი")
                If Form1.userRoleID = 1 Then
                    .Add("EditDate", "რედ. თარიღი")
                    .Add("Author", "ავტორი")
                End If
                Dim editBtn As New DataGridViewButtonColumn()
                editBtn.Name = "Edit"
                editBtn.HeaderText = ""
                editBtn.Text = "✎"
                editBtn.UseColumnTextForButtonValue = True
                .Add(editBtn)
            End With

            ' სვეტების ზომების დაყენება
            DgvSchedule.Columns("N").Width = 40
            DgvSchedule.Columns("Tarigi").Width = 110
            DgvSchedule.Columns("Duri").Width = 40
            DgvSchedule.Columns("Bene").Width = 180
            DgvSchedule.Columns("Per").Width = 185
            DgvSchedule.Columns("Ter").Width = 185
            DgvSchedule.Columns("Group").Width = 50
            DgvSchedule.Columns("Price").Width = 60
            DgvSchedule.Columns("Status").Width = 130
            DgvSchedule.Columns("Program").Width = 130
            DgvSchedule.Columns("Coment").DefaultCellStyle.WrapMode = DataGridViewTriState.False
            DgvSchedule.Columns("Coment").ToolTipText = "სრული კომენტარი გამოჩნდება მაუსის მიტანისას"
            If Form1.userRoleID = 1 Then
                DgvSchedule.Columns("Author").Width = 120
            End If
            DgvSchedule.Columns("Edit").Width = 24
        End If

        ' თარიღის დიაპაზონი ფილტრიდან
        Dim dateFrom As Date = DtpDan.Value
        Dim dateTo As Date = DtpMde.Value

        ' Google Sheets-დან ყველა ჩანაწერის წამოღება
        Dim allRows As List(Of IList(Of Object)) = GoogleSheetsHelper.GetSheetData(Form1.service, Form1.spreadsheetId, "DB-Schedule")
        Dim filtered As New List(Of IList(Of Object))

        ' ჩანაწერების გაფილტვრა
        For Each row As IList(Of Object) In allRows
            ' აუცილებელი ველები: N (0), თარიღი (5), ხანგძლიობა (6), სახელი (3), გვარი (4)
            If row.Count < 7 Then Continue For ' მინიმუმ უნდა არსებობდეს N-დან ხანგძლიობამდე
            If String.IsNullOrWhiteSpace(row(0).ToString()) Then Continue For ' N ცარიელი
            If String.IsNullOrWhiteSpace(row(5).ToString()) Then Continue For ' თარიღი ცარიელი
            If String.IsNullOrWhiteSpace(row(3).ToString()) OrElse String.IsNullOrWhiteSpace(row(4).ToString()) Then Continue For ' სახელი ან გვარი ცარიელი
            Dim dt As DateTime
            Dim rawDate As String = row(5).ToString().Trim()
            Dim formats As String() = {"dd.MM.yyyy HH:mm", "dd.MM.yyyy", "dd.MM.yy HH:mm"}
            If Not DateTime.TryParseExact(rawDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, dt) Then
                Debug.WriteLine("⛔ ვერ მოხდა თარიღის წაკითხვა: " & rawDate)
                Continue For
            End If
            If dt.Date < dateFrom.Date OrElse dt.Date > dateTo.Date Then Continue For
            If Not String.IsNullOrWhiteSpace(CBShes.Text) AndAlso CBShes.Text <> "ყველა" AndAlso Not row(12).ToString().Trim() = CBShes.Text Then Continue For
            If Not String.IsNullOrWhiteSpace(CBBeneName.Text) AndAlso CBBeneName.Text <> "ყველა" AndAlso Not row(3).ToString().Trim() = CBBeneName.Text Then Continue For
            If Not String.IsNullOrWhiteSpace(CBBeneSurname.Text) AndAlso CBBeneSurname.Text <> "ყველა" AndAlso Not row(4).ToString().Trim() = CBBeneSurname.Text Then Continue For
            If Not String.IsNullOrWhiteSpace(CBPer.Text) AndAlso CBPer.Text <> "ყველა" AndAlso Not row(8).ToString().Trim() = CBPer.Text Then Continue For
            If Not String.IsNullOrWhiteSpace(CBTer.Text) AndAlso CBTer.Text <> "ყველა" AndAlso Not row(9).ToString().Trim() = CBTer.Text Then Continue For
            If Not String.IsNullOrWhiteSpace(CBSpace.Text) AndAlso CBSpace.Text <> "ყველა" AndAlso Not row(10).ToString().Trim() = CBSpace.Text Then Continue For
            If Not String.IsNullOrWhiteSpace(CBDaf.Text) AndAlso CBDaf.Text <> "ყველა" AndAlso Not row(13).ToString().Trim() = CBDaf.Text Then Continue For
            filtered.Add(row)
        Next

        ' დალაგება თარიღის მიხედვით კლებადობით
        filtered.Sort(Function(a, b)
                          Dim dta, dtb As DateTime
                          Dim formats As String() = {"dd.MM.yyyy HH:mm", "dd.MM.yyyy", "dd.MM.yy HH:mm"}

                          If Not DateTime.TryParseExact(b(5).ToString().Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, dtb) Then dtb = Date.MinValue
                          If Not DateTime.TryParseExact(a(5).ToString().Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, dta) Then dta = Date.MinValue

                          If dtb = Date.MinValue AndAlso dta = Date.MinValue Then
                              Return 0
                          ElseIf dtb = Date.MinValue Then
                              Return 1
                          ElseIf dta = Date.MinValue Then
                              Return -1
                          Else
                              Return dtb.CompareTo(dta)
                          End If
                      End Function)

        ' გვერდების ლოგიკა: 20 ჩანაწერი თითო გვერდზე
        Dim pageSize As Integer = 20
        Dim totalPages As Integer = Math.Ceiling(filtered.Count / pageSize)
        If currentPage > totalPages Then currentPage = 1
        Dim pageRows = filtered.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()

        ' თითოეული ჩანაწერის დამატება ცხრილში
        For Each row In pageRows
            Dim beneFullName As String = row(3).ToString().Trim() & " " & row(4).ToString().Trim()
            Dim dgvRow As New List(Of Object) From {
                row(0), row(5), row(6), beneFullName,
                row(8), row(9), row(7), row(10), row(11),
                row(12), row(13), If(row.Count > 14, row(14).ToString(), "")
            }
            If Form1.userRoleID = 1 Then
                dgvRow.Add(row(1)) ' რედაქტირების თარიღი
                dgvRow.Add(row(2)) ' ავტორი
            End If
            dgvRow.Add("✎") ' რედაქტირება
            Dim rowIndex As Integer = DgvSchedule.Rows.Add(dgvRow.ToArray())
            ' თუ სტატუსი არის "შესრულებული" → მწვანე ფონი მთელ სტრიქონზე
            Dim statusText As String = row(12).ToString().Trim()
            Dim nowTime As DateTime = DateTime.Now
            Dim sessionTime As DateTime
            Dim formats As String() = {"dd.MM.yyyy HH:mm", "dd.MM.yyyy", "dd.MM.yy HH:mm"}
            DateTime.TryParseExact(row(5).ToString().Trim(), formats, CultureInfo.InvariantCulture, DateTimeStyles.None, sessionTime)

            Select Case statusText
                Case "შესრულებული"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen
                Case "აღდგენა"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew
                Case "გაცდენა არასაპატიო"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Plum
                Case "გაცდენა საპატიო"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow
                Case "პროგრამით გატარება"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.LightGray
                Case "გაუქმება"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                Case "შესრულებული"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen
                Case "აღდგენა"
                    DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew
                Case "დაგეგმილი"
                    If sessionTime < nowTime Then
                        DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral ' ღია წითელი
                    Else
                        DgvSchedule.Rows(rowIndex).DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue
                    End If
            End Select
        Next

        ' გვერდის ნომრის ჩვენება
        LPageN.Text = $"გვერდი {currentPage} / {totalPages}"
    End Sub

    ' 📍 წინა გვერდზე გადასვლა
    Private Sub BtnPrev_Click(sender As Object, e As EventArgs) Handles BtnPrev.Click
        If currentPage > 1 Then
            currentPage -= 1
            LoadFilteredSchedule()
        End If
    End Sub

    ' 📍 შემდეგ გვერდზე გადასვლა
    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        currentPage += 1
        LoadFilteredSchedule()
    End Sub

    ' 🔄 განახლება (დაბრუნება პირველ გვერდზე)
    Private Sub BtnRef_Click(sender As Object, e As EventArgs) Handles BtnRef.Click
        currentPage = 1
        LoadFilteredSchedule()
    End Sub

    ' 📌 სახელის არჩევისას იფილტრება მხოლოდ შესაბამისი გვარები
    Private Sub CBBeneName_SelectedIndexChanged(sender As Object, e As EventArgs)
        If CBBeneName.SelectedIndex <= 0 Then
            ' თუ არჩეულია "ყველა" – გვარების სია დარჩეს მხოლოდ "ყველა"
            CBBeneSurname.Items.Clear()
            CBBeneSurname.Items.Add("ყველა")
            CBBeneSurname.SelectedIndex = 0
            Exit Sub
        End If

        ' წამოიღე ყველა ჩანაწერი DB-Bene-დან
        Dim allRows = GoogleSheetsHelper.GetSheetData(Form1.service, Form1.spreadsheetId, "DB-Bene")
        Dim surnameSet As New HashSet(Of String)
        Dim selectedName As String = CBBeneName.Text.Trim()

        For Each row In allRows
            If row.Count > 2 AndAlso row(1).ToString().Trim() = selectedName Then
                Dim surname = row(2).ToString().Trim()
                If Not String.IsNullOrWhiteSpace(surname) Then
                    surnameSet.Add(surname)
                End If
            End If
        Next

        ' შევსება ComboBox-ში
        CBBeneSurname.Items.Clear()
        CBBeneSurname.Items.Add("ყველა")
        CBBeneSurname.Items.AddRange(surnameSet.ToArray())
        CBBeneSurname.SelectedIndex = 0
    End Sub

    ' 📌 ცხრილში ✎ ღილაკზე დაჭერისას Form2-ში არსებული ჩანაწერის რედაქტირება
    Private Sub DgvSchedule_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvSchedule.CellContentClick
        If e.RowIndex < 0 OrElse DgvSchedule.Columns(e.ColumnIndex).Name <> "Edit" Then Exit Sub

        Dim row = DgvSchedule.Rows(e.RowIndex)
        Dim fullName As String = row.Cells("Bene").Value.ToString().Trim()
        Dim nameParts = fullName.Split(" "c)

        ' გადაცემა Form2-ში
        Dim f2 As New Form2()
        With f2
            .PrefillLN = row.Cells("N").Value.ToString()
            .PrefillBeneName = If(nameParts.Length > 0, nameParts(0), "")
            .PrefillBeneSurname = If(nameParts.Length > 1, nameParts(1), "")
            .PrefillTherapist = row.Cells("Per").Value.ToString()
            .PrefillTherapy = row.Cells("Ter").Value.ToString()
            .PrefillProgram = row.Cells("Program").Value.ToString()
            .PrefillPrice = row.Cells("Price").Value.ToString()
            .PrefillIsGroup = row.Cells("Group").Value.ToString().Trim().ToUpper().Equals("TRUE", StringComparison.OrdinalIgnoreCase)
            .PrefillComment = row.Cells("Coment").Value.ToString() ' კომენტარის გადაცემა
            .IsEditMode = True
            .PrefillDateTime = row.Cells("Tarigi").Value.ToString()
            .PrefillSpace = row.Cells("Space").Value.ToString()
            .PrefillStatus = row.Cells("Status").Value.ToString().Trim()
            .ShowFromSchedule(Me) ' გადავცეთ UC_Schedule როგორც გამომძახებელი
        End With
    End Sub

    Private Sub BtnAddSchedule_Click(sender As Object, e As EventArgs) Handles BtnAddSchedule.Click
        Dim f2 As New Form2()
        f2.ShowFromSchedule(Me) ' გადავცეთ UC_Schedule როგორც გამომძახებელი
        f2.BringToFront()
        f2.Focus()
    End Sub
End Class
