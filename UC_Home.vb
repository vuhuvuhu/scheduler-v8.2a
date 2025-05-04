' 🧩 UC_Home.vb – საწყისი გვერდის UserControl
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Services
Imports Google.Apis.Auth.OAuth2
Imports System.IO

Public Class UC_Home

    ' ✅ საჭირო ცვლადები
    Private scheduleList As New List(Of ScheduleRow)()
    Private currentPage As Integer = 0
    Private Const pageSize As Integer = 20
    Private ReadOnly spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"
    Private userEmail As String = ""

    Public Sub RefreshSchedule()
        LoadScheduleData()
    End Sub
    ' ✅ კონსტრუქტორი – ელფოსტის მიღება Form1-დან
    Public Sub New(email As String)
        InitializeComponent()
        userEmail = email
        LoadScheduleData()
    End Sub

    ' ✅ მონაცემების მოდელი
    Private Class ScheduleRow
        Public Property RowId As String
        Public Property FullName As String
        Public Property DateTime As Date
        Public Property Duration As String
        Public Property Therapist As String
        Public Property Therapy As String
        Public Property Space As String
        Public Property Price As String
        Public Property Program As String
        Public Property Group As Boolean ' დავამატოთ ეს თვისება
        Public Property Comment As String ' ახალი თვისება კომენტარისთვის
    End Class

    ' ✅ Google Sheets API-სთან კავშირი
    Private Function GetSheetsService() As SheetsService
        Try
            Debug.WriteLine("Attempting to create GoogleCredential...")
            Dim credential As GoogleCredential
            Dim jsonPath As String = Path.Combine(Application.StartupPath, "sinuous-pact-454212-m3-ce5ff9f96bc8.json")
            Debug.WriteLine("JSON Path: " & jsonPath)
            Using stream = New FileStream(jsonPath, FileMode.Open, FileAccess.Read)
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.Scope.Spreadsheets)
                Debug.WriteLine("GoogleCredential created successfully.")
            End Using
            Dim service = New SheetsService(New BaseClientService.Initializer() With {
            .HttpClientInitializer = credential,
            .ApplicationName = "Scheduler VB"
        })
            Debug.WriteLine("SheetsService created successfully.")
            Return service
        Catch ex As Exception
            Debug.WriteLine("GetSheetsService Error: " & ex.Message & vbCrLf & ex.StackTrace)
            MessageBox.Show("GetSheetsService Error: " & ex.Message & vbCrLf & ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    ' ✅ ყველა დაგეგმილი და უკვე გასული ჩანაწერის წამოღება Google Sheets-დან
    Public Sub LoadScheduleData()
        Dim service = GetSheetsService()
        Dim request = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Schedule!A2:N")
        Dim response = request.Execute()

        scheduleList.Clear()

        If response.Values IsNot Nothing Then
            For Each row In response.Values
                If row.Count >= 14 Then
                    Dim rowId As String = row(0).ToString()
                    Dim status = row(12).ToString()
                    Dim dateTimeStr = row(5).ToString()
                    Dim sessionDate As DateTime

                    If status = "დაგეგმილი" AndAlso DateTime.TryParseExact(dateTimeStr, "dd.MM.yyyy HH:mm", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, sessionDate) Then
                        If sessionDate < DateTime.Now Then
                            scheduleList.Add(New ScheduleRow With {
                            .RowId = rowId,
                            .FullName = row(3).ToString() & " " & row(4).ToString(),
                            .DateTime = sessionDate,
                            .Duration = row(6).ToString(),
                            .Therapist = row(8).ToString(),
                            .Therapy = row(9).ToString(),
                            .Space = row(10).ToString(),
                            .Price = row(11).ToString(),
                            .Program = row(13).ToString(),
                            .Group = row(7).ToString().Trim().ToUpper().Equals("TRUE", StringComparison.OrdinalIgnoreCase),
                            .Comment = If(row.Count > 14, row(14).ToString().Trim(), "")
                        })
                        End If
                    End If
                End If
            Next
        End If

        scheduleList = scheduleList.OrderByDescending(Function(s) s.DateTime).ToList()
        currentPage = 0
        ShowCurrentPage()
    End Sub
    ' ✅ აჩვენებს მიმდინარე გვერდზე ჩანაწერებს
    Private Sub ShowCurrentPage()
        DgvDagegmili.Columns.Clear()
        DgvDagegmili.Rows.Clear()
        DgvDagegmili.ReadOnly = True
        DgvDagegmili.AllowUserToAddRows = False
        DgvDagegmili.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        DgvDagegmili.Columns.Add("N", "N")
        DgvDagegmili.Columns.Add("FullName", "სახელი გვარი")
        DgvDagegmili.Columns.Add("DateTime", "თარიღი")
        DgvDagegmili.Columns.Add("Duration", "ხანგძლ.")
        DgvDagegmili.Columns.Add("Therapist", "თერაპევტი")
        DgvDagegmili.Columns.Add("Therapy", "თერაპია")
        DgvDagegmili.Columns.Add("Space", "სივრცე")
        DgvDagegmili.Columns.Add("Price", "თანხა")
        DgvDagegmili.Columns.Add("Program", "დაფინანსება")

        If Not String.IsNullOrWhiteSpace(userEmail) Then
            Dim editButton As New DataGridViewButtonColumn()
            editButton.Name = "Edit"
            editButton.HeaderText = ""
            editButton.Text = "🖉"
            editButton.UseColumnTextForButtonValue = True
            DgvDagegmili.Columns.Add(editButton)
        End If

        DgvDagegmili.Columns("N").Width = 40
        DgvDagegmili.Columns("FullName").Width = 150
        DgvDagegmili.Columns("DateTime").Width = 110
        DgvDagegmili.Columns("Duration").Width = 30
        DgvDagegmili.Columns("Therapist").Width = 150
        DgvDagegmili.Columns("Therapy").Width = 150
        DgvDagegmili.Columns("Space").Width = 80
        DgvDagegmili.Columns("Price").Width = 80
        DgvDagegmili.Columns("Program").Width = 100
        If DgvDagegmili.Columns.Contains("Edit") Then
            DgvDagegmili.Columns("Edit").Width = 40
        End If

        Dim start = currentPage * pageSize
        Dim pageItems = scheduleList.Skip(start).Take(pageSize).ToList()

        For Each item In pageItems
            Dim formattedPrice As String = ""
            Dim priceNum As Decimal
            If Decimal.TryParse(item.Price, priceNum) Then
                formattedPrice = priceNum.ToString("N2")
            Else
                formattedPrice = item.Price
            End If

            If Not String.IsNullOrWhiteSpace(userEmail) Then
                DgvDagegmili.Rows.Add(
                    item.RowId,
                    item.FullName,
                    item.DateTime.ToString("dd.MM.yyyy HH:mm"),
                    item.Duration,
                    item.Therapist,
                    item.Therapy,
                    item.Space,
                    formattedPrice,
                    item.Program,
                    "🖉")
            Else
                DgvDagegmili.Rows.Add(
                    item.RowId,
                    item.FullName,
                    item.DateTime.ToString("dd.MM.yyyy HH:mm"),
                    item.Duration,
                    item.Therapist,
                    item.Therapy,
                    item.Space,
                    formattedPrice,
                    item.Program)
            End If
        Next

        BtnPrev.Enabled = currentPage > 0
        BtnNext.Enabled = (currentPage + 1) * pageSize < scheduleList.Count
    End Sub
    ' ✅ რედაქტირების ღილაკები
    Private Sub DgvDagegmili_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvDagegmili.CellContentClick
        If e.RowIndex >= 0 AndAlso DgvDagegmili.Columns(e.ColumnIndex).Name = "Edit" Then
            Dim row As DataGridViewRow = DgvDagegmili.Rows(e.RowIndex)

            Dim rowNum As String = row.Cells("N").Value.ToString()
            Dim fullName As String = row.Cells("FullName").Value.ToString()
            Dim dt As DateTime = DateTime.ParseExact(row.Cells("DateTime").Value.ToString(), "dd.MM.yyyy HH:mm", Globalization.CultureInfo.InvariantCulture)
            Dim duration As String = row.Cells("Duration").Value.ToString()
            Dim therapist As String = row.Cells("Therapist").Value.ToString()
            Dim therapy As String = row.Cells("Therapy").Value.ToString()
            Dim space As String = row.Cells("Space").Value.ToString()
            Dim price As String = row.Cells("Price").Value.ToString()
            Dim program As String = row.Cells("Program").Value.ToString()

            Dim scheduleRow = scheduleList.FirstOrDefault(Function(s) s.RowId = rowNum)

            Dim nameParts = fullName.Split(" "c)
            Dim beneName As String = nameParts(0)
            Dim beneSurname As String = If(nameParts.Length > 1, nameParts(1), "")

            Dim f2 As New Form2()
            f2.IsEditMode = True

            f2.PrefillLN = rowNum
            f2.PrefillBeneName = beneName
            f2.PrefillBeneSurname = beneSurname
            f2.PrefillTherapist = therapist
            f2.PrefillTherapy = therapy
            f2.PrefillProgram = program
            f2.PrefillIsGroup = If(scheduleRow IsNot Nothing, scheduleRow.Group, False)
            f2.PrefillComment = If(scheduleRow IsNot Nothing, scheduleRow.Comment, "")
            f2.PrefillDateTime = dt.ToString("dd.MM.yyyy HH:mm")
            f2.PrefillPrice = price
            f2.PrefillSpace = space

            f2.ShowFromHome(Me)

            f2.LN.Text = rowNum
            f2.LNow.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
            f2.LAutor.Text = userEmail
            f2.CBBeneName.Enabled = False
            f2.CBBeneSurname.Enabled = False
            f2.DTP1.Value = dt.Date
            f2.THour.Text = dt.Hour.ToString()
            f2.TMin.Text = dt.Minute.ToString()
            f2.TDur.Text = duration
            f2.TCost.Text = price

            For Each btn As Button In {f2.BTNS1, f2.BTNS2, f2.BTNS3, f2.BTNS4, f2.BTNS5, f2.BTNS6, f2.BTNS7, f2.BTNS8, f2.BTNS9, f2.BTNS10, f2.BTNS11, f2.BTNS12, f2.BTNS13, f2.BTNS14}
                If btn.Text = space Then
                    f2.SelectedSpaceButton = btn
                    btn.BackColor = Color.DeepSkyBlue
                End If
            Next

            If dt < DateTime.Now Then
                f2.LPlan.Visible = False
                For Each rb In {f2.RB1, f2.RB2, f2.RB3, f2.RB4, f2.RB5, f2.RB6}
                    rb.Visible = True
                Next
            Else
                f2.LPlan.Visible = True
                For Each rb In {f2.RB1, f2.RB2, f2.RB3, f2.RB4, f2.RB5, f2.RB6}
                    rb.Visible = False
                Next
            End If

            f2.CheckAvailability()
            f2.ValidateForm()

            f2.BtnAdd.Text = "შეცვლა"
            f2.BtnClear.Visible = False

            Debug.WriteLine("DgvDagegmili_CellContentClick: PrefillLN = " & f2.PrefillLN)
            Debug.WriteLine("DgvDagegmili_CellContentClick: LN.Text = " & f2.LN.Text)
            Debug.WriteLine("DgvDagegmili_CellContentClick: PrefillIsGroup = " & f2.PrefillIsGroup)
            Debug.WriteLine("DgvDagegmili_CellContentClick: PrefillComment = " & f2.PrefillComment)

            f2.ShowDialog()
        End If
    End Sub
    ' ✅ გვერდებზე გადართვა
    Private Sub BtnPrev_Click(sender As Object, e As EventArgs) Handles BtnPrev.Click
        If currentPage > 0 Then
            currentPage -= 1
            ShowCurrentPage()
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click
        If (currentPage + 1) * pageSize < scheduleList.Count Then
            currentPage += 1
            ShowCurrentPage()
        End If
    End Sub

    Private Sub BtnAddSchedule_Click(sender As Object, e As EventArgs) Handles BtnAddSchedule.Click
        Dim f2 As New Form2()
        f2.ShowFromHome(Me) ' გადავცეთ UC_Home როგორც გამომძახებელი
        Form2.BringToFront()
        Form2.Focus()
    End Sub

    Private Sub BtnRef_Click(sender As Object, e As EventArgs) Handles BtnRef.Click
        LoadScheduleData()
    End Sub

End Class
