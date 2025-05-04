' 📌 სქეჯულერის სრული VB.NET კოდი (ფორმა2) – Google Sheets API-ით მუშაობა
' Გამოიყენე ფაილი: sinuous-pact-454212-m3-d64e059c8663.json
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports System.IO
Imports System.Drawing
Imports System.Threading
Public Class Form2
    ' ✅ Google Sheets API-სთან კავშირის ცვლადები
    Public PrefillLN As String = ""
    Public PrefillBeneName As String = ""
    Public PrefillBeneSurname As String = ""
    Public PrefillTherapist As String = ""
    Public PrefillTherapy As String = ""
    Public PrefillProgram As String = ""
    Public PrefillDateTime As String = ""
    Public PrefillPrice As String = ""
    Public PrefillSpace As String = ""
    Public PrefillIsGroup As Boolean = False
    Public PrefillStatus As String = ""
    Public IsEditMode As Boolean = False
    Public PrefillComment As String = ""

    ' • ახალი თვისებები გამოძახების წყაროსთვის
    Public CallerControl As Object        ' იმ კონტროლერის ნამდვილი Instance
    Public CallerType As String           ' "UC_Home", "UC_Schedule", "UC_CalendarDay" და ა.შ.
    ' ✅ გლობალური ცვლადები
    Private spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"
    Public SelectedSpaceButton As Button
    ' ✅ თარიღის, საათის ან წუთის შეცვლისას – ვამოწმებთ სტატუსს და დატვირთვას
    Private isChangingDateTime As Boolean = False
    ' ✅ ფორმის ჩატვირთვისას საჭიროა პირველადი ინფორმაციის წამოღება Google Sheets-დან
    Public AutorEmail As String = ""
    ' • სასარგებლო მეთოდები გამოძახებისთვის
    Public Sub ShowFromHome(homeCtrl As UC_Home)
        CallerControl = homeCtrl
        CallerType = "UC_Home"
        Me.Show()
    End Sub

    Public Sub ShowFromSchedule(scheduleCtrl As UC_Schedule)
        CallerControl = scheduleCtrl
        CallerType = "UC_Schedule"
        Me.Show()
    End Sub

    Public Sub ShowFromCalendarDay(calDayCtrl As UC_Calendar_day)
        CallerControl = calDayCtrl
        CallerType = "UC_CalendarDay"
        Show()
    End Sub

    ' ✅ Google Sheets API-სთან კავშირის ფუნქცია
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
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' კომბობოქსების რედაქტირების შეზღუდვა
        CBBeneName.FlatStyle = FlatStyle.Flat
        CBBeneSurname.FlatStyle = FlatStyle.Flat
        CBPer.FlatStyle = FlatStyle.Flat
        CBTer.FlatStyle = FlatStyle.Flat
        CBDaf.FlatStyle = FlatStyle.Flat
        CBBeneName.DropDownStyle = ComboBoxStyle.DropDownList
        CBBeneSurname.DropDownStyle = ComboBoxStyle.DropDownList
        CBPer.DropDownStyle = ComboBoxStyle.DropDownList
        CBTer.DropDownStyle = ComboBoxStyle.DropDownList
        CBDaf.DropDownStyle = ComboBoxStyle.DropDownList
        CBGroup.Checked = PrefillIsGroup
        TCom.Text = PrefillComment
        ' ✅ Google Sheets API-ს ინიციალიზაცია
        Dim service = GetSheetsService()
        ' DTP1 ფორმატირება – მხოლოდ თარიღი
        DTP1.Format = DateTimePickerFormat.Custom
        DTP1.CustomFormat = "dd.MM.yyyy"
        ' ჩანაწერის ნომერი – A სვეტის მაქსიმუმი +1 ან რედაქტირების ღეჭიმში წამოღებული მნიშვნელობა
        If Not IsEditMode Then
            Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Schedule!A2:A")
            Dim res = req.Execute()
            Dim maxRow = If(res.Values IsNot Nothing, res.Values.Count + 1, 1)
            LN.Text = maxRow.ToString()
        Else
            ' 📌 რედაქტირების რეჟიმში პირდაპირ ვიყენებთ გადმოცემულს
            LN.Text = PrefillLN
        End If
        ' ავტორის ველის შევსება
        LAutor.Text = Form1.LUser.Text
        ' ბენეფიციარის სახელების ჩასმა DB-Bene ფურცლიდან, ანბანის მიხედვით დალაგებული
        Dim nameReq = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Bene!B2:B")
        Dim nameRes = nameReq.Execute()
        If nameRes.Values IsNot Nothing Then
            Dim names = nameRes.Values.
        Select(Function(x) x(0).ToString()).
        Where(Function(s) Not String.IsNullOrWhiteSpace(s)).
        Distinct().
        OrderBy(Function(s) s).
        ToArray()
            CBBeneName.Items.AddRange(names)
        End If

        ' ✅ თერაპევტების ჩასმა DB-Personal ფურცლიდან (ანბანის მიხედვით დალაგებული)
        Dim perReq = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Personal!B2:C")
        Dim perRes = perReq.Execute()

        If perRes.Values IsNot Nothing Then
            Dim therapists = perRes.Values.
        Where(Function(row) row.Count >= 2).
        Select(Function(row) row(0).ToString() & " " & row(1).ToString()).
        OrderBy(Function(name) name).
        ToArray()

            CBPer.Items.AddRange(therapists)
        End If
        ' თერაპიების ჩამატება DB-Therapy ფურცლიდან
        Dim terReq = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Therapy!B2:B")
        Dim terRes = terReq.Execute()
        If terRes.Values IsNot Nothing Then
            For Each row In terRes.Values
                CBTer.Items.Add(row(0).ToString())
            Next
        End If
        ' დაფინანსების ჩამატება DB-Program ფურცლიდან
        Dim dafReq = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Program!B2:B")
        Dim dafRes = dafReq.Execute()
        If dafRes.Values IsNot Nothing Then
            For Each row In dafRes.Values
                CBDaf.Items.Add(row(0).ToString())
            Next
        End If
        ' სივრცის ღილაკებზე დაჭერის მექანიზმი – ყველას ვუკავშირებთ საერთო მოვლენას
        For Each btn As Button In {BTNS1, BTNS2, BTNS3, BTNS4, BTNS5, BTNS6, BTNS7, BTNS8, BTNS9, BTNS10, BTNS11, BTNS12, BTNS13, BTNS14}
            AddHandler btn.Click, AddressOf SpaceButton_Click
        Next
        ' ვრთავთ დროის ტაიმერს – გამოიყენება ჩანაწერის თარიღისთვის
        Timer1.Start()
        ' რეგისტრაცია თარიღის ცვლილებისათვის
        ' რეგისტრაცია საათისა და წუთის ცვლილებისათვის
        AddHandler THour.TextChanged, AddressOf THourOrMin_Changed
        AddHandler TMin.TextChanged, AddressOf THourOrMin_Changed
        ' რადიობუტონების შემოწმებისთვის
        RegisterStatusHandlers()
        'ეს გვჭირდება, რომ ფორმა1-დან წამოვიღოთ კომბობოქსების ინფო
        If Not String.IsNullOrWhiteSpace(PrefillBeneName) Then
            CBBeneName.Text = PrefillBeneName
        End If
        If Not String.IsNullOrWhiteSpace(PrefillBeneSurname) Then
            CBBeneSurname.Text = PrefillBeneSurname
        End If
        If Not String.IsNullOrWhiteSpace(PrefillTherapist) Then
            CBPer.Text = PrefillTherapist
        End If
        If Not String.IsNullOrWhiteSpace(PrefillTherapy) Then
            CBTer.Text = PrefillTherapy
        End If
        If Not String.IsNullOrWhiteSpace(PrefillProgram) Then
            CBDaf.Text = PrefillProgram
        End If
        If Not String.IsNullOrWhiteSpace(PrefillPrice) Then
            TCost.Text = PrefillPrice
        End If
        ' ✅ თუ რედაქტირების რეჟიმია, ღილაკის ტექსტი გახდეს "შეცვლა"
        If IsEditMode Then
            BtnAdd.Text = "შეცვლა"
        End If

        ' ✅ თარიღის და დროის გადმოწერა PrefillDateTime-იდან
        If Not String.IsNullOrWhiteSpace(PrefillDateTime) Then
            Dim dt As DateTime
            If DateTime.TryParseExact(PrefillDateTime.Trim(), "dd.MM.yyyy HH:mm", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, dt) Then
                DTP1.Value = dt
                THour.Text = dt.Hour.ToString()
                TMin.Text = dt.Minute.ToString()
            End If
        End If

        ' ✅ სივრცის ღილაკის არჩევა და შეღებვა
        If Not String.IsNullOrWhiteSpace(PrefillSpace) Then
            For Each btn As Button In {BTNS1, BTNS2, BTNS3, BTNS4, BTNS5, BTNS6, BTNS7, BTNS8, BTNS9, BTNS10, BTNS11, BTNS12, BTNS13, BTNS14}
                If btn.Text = PrefillSpace Then
                    SelectedSpaceButton = btn
                    btn.BackColor = System.Drawing.Color.DeepSkyBlue
                    Exit For
                End If
            Next
            ' 🟩 სივრცე არჩეულია, საჭიროა ფორმის ვალიდაცია
            CheckAvailability()
            ValidateForm()
        End If
        If Not String.IsNullOrWhiteSpace(PrefillStatus) Then
            Dim sessionTime As DateTime
            If DateTime.TryParseExact(PrefillDateTime, "dd.MM.yyyy HH:mm", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, sessionTime) Then
                If sessionTime > DateTime.Now Then
                    ' მომავალი თარიღია → ვთქვათ დაგეგმილია
                    LPlan.Visible = True
                    For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6} : rb.Visible = False : rb.Checked = False : Next
                    LPlan.Text = "დაგეგმილი"
                Else
                    ' წარსული დროა → საჭიროა სტატუსის მონიშვნა
                    LPlan.Visible = False
                    For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6} : rb.Visible = True : Next

                    If PrefillStatus <> "დაგეგმილი" Then
                        ' მონიშნოს შესაბამისი რადიო
                        For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6}
                            If rb.Text = PrefillStatus Then
                                rb.Checked = True
                                Exit For
                            End If
                        Next
                    End If
                End If
            End If
        End If
        CheckAvailability()
        ValidateForm()
    End Sub

    ' ✅ ხელახლა ჩატვირთავს ბენეფიციარების სიას
    Public Sub ReloadBeneComboBoxes()
        Dim service = GetSheetsService()

        ' სახელები
        CBBeneName.Items.Clear()
        Dim nameReq = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Bene!B2:B")
        Dim nameRes = nameReq.Execute()
        If nameRes.Values IsNot Nothing Then
            Dim names = nameRes.Values.Select(Function(x) x(0).ToString()).Distinct().OrderBy(Function(x) x).ToArray()
            CBBeneName.Items.AddRange(names)
        End If

        ' გვარები
        CBBeneSurname.Items.Clear()
        Dim surnameReq = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Bene!C2:C")
        Dim surnameRes = surnameReq.Execute()
        If surnameRes.Values IsNot Nothing Then
            Dim surnames = surnameRes.Values.Select(Function(x) x(0).ToString()).Distinct().OrderBy(Function(x) x).ToArray()
            CBBeneSurname.Items.AddRange(surnames)
        End If
    End Sub
    ' ✅ დროის განახლება Label-ში (LNow)
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LNow.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
    End Sub
    ' ✅ სახელის არჩევისას – შესაბამისი გვარები შეგვყავს გვარის ComboBox-ში (ანბანის მიხედვით დალაგებული)
    Private Sub CBBeneName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBBeneName.SelectedIndexChanged
        CBBeneSurname.Items.Clear()

        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Bene!B2:C")
        Dim res = req.Execute()

        If res.Values IsNot Nothing Then
            Dim surnames = res.Values.
            Where(Function(row) row.Count >= 2 AndAlso row(0).ToString() = CBBeneName.Text).
            Select(Function(row) row(1).ToString()).
            Where(Function(s) Not String.IsNullOrWhiteSpace(s)).
            Distinct().
            OrderBy(Function(s) s).
            ToArray()

            CBBeneSurname.Items.AddRange(surnames)

        End If
    End Sub
    ' ✅ სივრცის ღილაკზე დაჭერის ფუნქცია – არჩეული ღილაკი იღებს ცისფერ ფერს, დანარჩენი უბრუნდება ნაგულისხმევს
    Private Sub SpaceButton_Click(sender As Object, e As EventArgs)
        Dim clickedButton = CType(sender, Button)
        SelectedSpaceButton = clickedButton

        ' არჩეული ღილაკი იღებს ცისფერს, დანარჩენი ღილაკებს არ ვეხებით – CheckAvailability განსაზღვრავს მათ ფერს
        For Each btn As Button In {BTNS1, BTNS2, BTNS3, BTNS4, BTNS5, BTNS6, BTNS7, BTNS8, BTNS9, BTNS10, BTNS11, BTNS12, BTNS13, BTNS14}
            If btn Is clickedButton Then
                btn.BackColor = System.Drawing.Color.DeepSkyBlue
            End If
        Next

        ' ხელახლა ვამოწმებთ სივრცისა და დროის დაკავებულობას
        CheckAvailability()
        ValidateForm()
    End Sub
    ' ✅ დროის გამოთვლა THour და TMin ველებიდან
    Private Function GetSessionDateTime() As DateTime
        Dim h As Integer = Val(THour.Text)
        Dim m As Integer = Val(TMin.Text)
        Return DTP1.Value.Date.AddHours(h).AddMinutes(m)
    End Function
    ' ✅ სტატუსის კონტროლი – თარიღის მიხედვით
    Private Sub UpdateStatusVisibility()
        If GetSessionDateTime() > DateTime.Now Then
            ' მომავალი – დაგეგმილი
            LPlan.Visible = True
            For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6} : rb.Visible = False : Next
            LPlan.Text = "დაგეგმილი"
        Else
            ' წარსული – საჭირო სტატუსი
            LPlan.Visible = False
            For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6} : rb.Visible = True : Next

            ' თუ არცერთი რადიო არ არის მონიშნული → BtnAdd არ გამოჩნდეს
            If Not {RB1, RB2, RB3, RB4, RB5, RB6}.Any(Function(r) r.Checked) Then
                BtnAdd.Visible = False
                LWarning.Text = "მიუთითეთ შესრულების სტატუსი"
                LWarning.BackColor = System.Drawing.Color.LightCoral
            End If
        End If
    End Sub
    ' ✅ არჩეული სტატუსის დაბრუნება
    Private Function GetSelectedStatus() As String
        If LPlan.Visible Then Return LPlan.Text
        For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6}
            If rb.Checked Then Return rb.Text
        Next
        Return ""
    End Function
    ' ✅ ვალიდაცია – ცარიელი ველების შემოწმება და გაფრთხილება
    Public Sub ValidateForm()
        Dim msg As String = ""

        If CBBeneName.Text = "" OrElse CBBeneSurname.Text = "" Then
            msg &= "შეავსეთ ბენეფიციარის სახელი და გვარი" & vbCrLf
        End If
        If CBPer.Text = "" Then
            msg &= "აირჩიეთ თერაპევტი" & vbCrLf
        End If
        If CBTer.Text = "" Then
            msg &= "აირჩიეთ თერაპია" & vbCrLf
        End If
        If CBDaf.Text = "" Then
            msg &= "აირჩიეთ დაფინანსება" & vbCrLf
        End If
        If TCost.Text = "" Then
            msg &= "მიუთითეთ ფასი" & vbCrLf
        End If
        If SelectedSpaceButton Is Nothing Then
            msg &= "აირჩიეთ სივრცე" & vbCrLf
        End If
        If Not LPlan.Visible AndAlso Not {RB1, RB2, RB3, RB4, RB5, RB6}.Any(Function(r) r.Checked) Then
            msg &= "მიუთითეთ შესრულების სტატუსი" & vbCrLf
        End If

        LWarning.Text = msg
        LWarning.BackColor = If(msg = "", System.Drawing.Color.LightGreen, System.Drawing.Color.LightCoral)
        BtnAdd.Visible = (msg = "")
    End Sub
    ' ✅ BtnAdd ღილაკის ლოგიკა - განსხვავებული ქცევა: დამატება vs რედაქტირება
    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        ValidateForm()
        If Not BtnAdd.Visible Then Exit Sub

        Dim service = GetSheetsService()

        ' ✅ ახალი ჩანაწერის მონაცემები - საერთო ორივე რეჟიმისთვის
        Dim newRow As New List(Of Object) From {
        LN.Text,
        LNow.Text,
        LAutor.Text,
        CBBeneName.Text,
        CBBeneSurname.Text,
        GetSessionDateTime().ToString("dd.MM.yyyy HH:mm"),
        TDur.Text,
        CBGroup.Checked.ToString().ToUpper(),
        CBPer.Text,
        CBTer.Text,
        If(SelectedSpaceButton IsNot Nothing, SelectedSpaceButton.Text, ""),
        TCost.Text,
        GetSelectedStatus(),
        CBDaf.Text,
        TCom.Text
    }

        If BtnAdd.Text = "შეცვლა" Then
            ' ✅ რედაქტირების რეჟიმი: ჩანაწერის განახლება LN.Text-ის მიხედვით
            Dim rowIndex As Integer = Val(LN.Text)
            Dim valueRange = New ValueRange() With {
            .Range = $"DB-Schedule!A{rowIndex + 1}:O{rowIndex + 1}",
            .Values = New List(Of IList(Of Object)) From {newRow}
        }

            Dim updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, valueRange.Range)
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED
            updateRequest.Execute()

            MessageBox.Show("ჩანაწერი წარმატებით განახლდა!", "✔ განახლება", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            ' ✅ დამატების რეჟიმი: ჩანაწერის დამატება ცხრილის ბოლოში
            Dim valueRange = New ValueRange()
            valueRange.Values = New List(Of IList(Of Object)) From {newRow}

            Dim appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, "DB-Schedule!A1")
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED
            appendRequest.Execute()

            MessageBox.Show("ჩანაწერი წარმატებით დაემატა ცხრილში!", "✔ წარმატება", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        ' ✅ განაახლეთ გამომძახებელი კონტროლი
        Select Case CallerType
            Case "UC_Home"
                If TypeOf CallerControl Is UC_Home Then
                    Dim homeCtrl As UC_Home = DirectCast(CallerControl, UC_Home)
                    homeCtrl.LoadScheduleData()
                End If
            Case "UC_Schedule"
                If TypeOf CallerControl Is UC_Schedule Then
                    Dim scheduleCtrl As UC_Schedule = DirectCast(CallerControl, UC_Schedule)
                    scheduleCtrl.LoadFilteredSchedule()
                End If
            Case "UC_CalendarDay"
                If TypeOf CallerControl Is UC_Calendar_day Then
                    Dim calDayCtrl As UC_Calendar_day = DirectCast(CallerControl, UC_Calendar_day)
                    calDayCtrl.CreateGrid()
                    calDayCtrl.PlaceCards()
                End If
        End Select

        Me.Close()
    End Sub
    ' ✅ ფორმის დახურვის ლოგიკა - გამგზავნთან დაბრუნება
    Private Sub Form2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' განაახლეთ გამომძახებელი, თუ ცვლილებები შეინახა (BtnAdd-ით)
        ' თუ განახლება საჭიროა მხოლოდ BtnAdd-ზე, ეს მოვლენა შეიძლება გამოტოვოთ
        Select Case CallerType
            Case "UC_Home"
                If TypeOf CallerControl Is UC_Home Then
                    Dim homeCtrl As UC_Home = DirectCast(CallerControl, UC_Home)
                    homeCtrl.LoadScheduleData()
                End If
            Case "UC_Schedule"
                If TypeOf CallerControl Is UC_Schedule Then
                    Dim scheduleCtrl As UC_Schedule = DirectCast(CallerControl, UC_Schedule)
                    scheduleCtrl.LoadFilteredSchedule()
                End If
            Case "UC_CalendarDay"
                If TypeOf CallerControl Is UC_Calendar_day Then
                    Dim calDayCtrl As UC_Calendar_day = DirectCast(CallerControl, UC_Calendar_day)
                    ' TODO: დაამატეთ UC_Calendar_day-ის განახლების მეთოდი
                    ' calDayCtrl.LoadDayData()
                End If
        End Select
    End Sub
    ' ✅ ბენეფიციარის, თერაპევტის და სივრცის დაკავებულობის შემოწმება
    Public Sub CheckAvailability()
        Dim sessionStart = GetSessionDateTime()
        Dim sessionEnd = sessionStart.AddMinutes(Val(TDur.Text))

        Dim service = GetSheetsService()
        Dim req = service.Spreadsheets.Values.Get(spreadsheetId, "DB-Schedule!A2:N")
        Dim res = req.Execute()

        Dim beneBusy As Boolean = False
        Dim perBusy As Boolean = False
        Dim beneMessage As String = ""
        Dim perMessage As String = ""
        Dim spaceMessage As String = ""

        ' ყველა ღილაკს ვუბრუნებთ ნაგულისხმევ ფერს, ხოლო არჩეულს ვტოვებთ ცისფრად
        For Each btn As Button In {BTNS1, BTNS2, BTNS3, BTNS4, BTNS5, BTNS6, BTNS7, BTNS8, BTNS9, BTNS10, BTNS11, BTNS12, BTNS13, BTNS14}
            btn.BackColor = If(btn Is SelectedSpaceButton, System.Drawing.Color.DeepSkyBlue, System.Drawing.Color.LightGreen)
        Next

        For Each row In res.Values
            If row.Count < 12 Then Continue For

            Dim rowStart As DateTime
            Dim rowStartStr = row(5).ToString()
            If Not DateTime.TryParseExact(rowStartStr, "dd.MM.yyyy HH:mm", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, rowStart) Then Continue For
            Dim rowDur As Integer = Val(row(6).ToString())
            Dim rowEnd = rowStart.AddMinutes(rowDur)

            Dim overlap = sessionStart < rowEnd AndAlso sessionEnd > rowStart

            ' ✅ ბენეფიციარის დაკავებულობა
            If overlap AndAlso row(3).ToString() = CBBeneName.Text AndAlso row(4).ToString() = CBBeneSurname.Text Then
                beneBusy = True
                beneMessage = "ბენეფიციარი მოცემულ დროს დაკავებულია: " & row(9) & ", " & row(8) & ", " & row(10)
            End If

            ' ✅ თერაპევტის დაკავებულობა
            If overlap AndAlso row(8).ToString() = CBPer.Text Then
                perBusy = True
                Dim isRowGroupSpace As Boolean = row(7).ToString().ToUpper() = "TRUE"
                If isRowGroupSpace Then
                    perMessage = "თერაპევტი მოცემულ დროს ჯგუფურ სეანსშია: " & row(3) & " " & row(4) & ", " & row(9) & ", " & row(10)
                Else
                    perMessage = "თერაპევტი მოცემულ დროს დაკავებულია: " & row(3) & " " & row(4) & ", " & row(9) & ", " & row(10)
                End If
            End If

            ' ✅ სივრცის დაკავებულობა – დროის გადაკვეთის საფუძველზე (დამოუკიდებლად ბენეფიციარის ან თერაპევტის ვინაობისა)
            If overlap Then
                For Each btn As Button In {BTNS1, BTNS2, BTNS3, BTNS4, BTNS5, BTNS6, BTNS7, BTNS8, BTNS9, BTNS10, BTNS11, BTNS12, BTNS13, BTNS14}
                    If btn.Text = row(10).ToString() Then
                        Dim isRowGroup As Boolean = row(7).ToString().ToUpper() = "TRUE"
                        If btn Is SelectedSpaceButton Then
                            spaceMessage = "სივრცეში მოცემულ დროს ტარდება სეანსი: " & row(3) & " " & row(4) & ", " & row(9) & ", " & row(8)
                        Else
                            btn.BackColor = If(isRowGroup, System.Drawing.Color.Khaki, System.Drawing.Color.LightCoral)
                        End If
                    End If
                Next
            End If
        Next

        ' ✅ ფერები ბენეფიციარზე
        If CBBeneName.SelectedIndex >= 0 AndAlso CBBeneSurname.SelectedIndex >= 0 Then
            Dim beneColor = If(beneBusy, System.Drawing.Color.LightCoral, System.Drawing.Color.LightGreen)
            CBBeneName.BackColor = beneColor
            CBBeneSurname.BackColor = beneColor
        Else
            CBBeneName.BackColor = System.Drawing.Color.White
            CBBeneSurname.BackColor = System.Drawing.Color.White
        End If
        ' ✅ ფერები თერაპევტზე
        If CBPer.SelectedIndex >= 0 Then
            Dim perColor = If(perBusy, If(perMessage.Contains("ჯგუფურ"), System.Drawing.Color.Khaki, System.Drawing.Color.LightCoral), System.Drawing.Color.LightGreen)
            CBPer.BackColor = perColor
        Else
            CBPer.BackColor = System.Drawing.Color.White
        End If

        ' ✅ არჩეული სივრცის ფერი – ცისფერი ან დარჩეს სტანდარტული
        If SelectedSpaceButton IsNot Nothing Then
            If spaceMessage = "" Then
                SelectedSpaceButton.BackColor = System.Drawing.Color.DeepSkyBlue
            End If
        End If

        ' ✅ შეტყობინება
        ' ✅ შეტყობინებები ბენეფიციარისთვის
        If CBBeneName.SelectedIndex >= 0 AndAlso CBBeneSurname.SelectedIndex >= 0 Then
            If beneBusy Then
                LMsgBene.Text = beneMessage
                LMsgBene.BackColor = System.Drawing.Color.LightCoral
            Else
                LMsgBene.Text = "მოცემულ დროს ბენეფიციარი თავისუფალია"
                LMsgBene.BackColor = System.Drawing.Color.LightGreen
            End If
        Else
            LMsgBene.Text = ""
        End If

        ' ✅ შეტყობინებები თერაპევტისთვის
        If CBPer.SelectedIndex >= 0 Then
            If perBusy Then
                LMsgPer.Text = perMessage
                LMsgPer.BackColor = If(perMessage.Contains("ჯგუფურ"), System.Drawing.Color.Khaki, System.Drawing.Color.LightCoral)
            Else
                LMsgPer.Text = "მოცემულ დროს თერაპევტი თავისუფალია"
                LMsgPer.BackColor = System.Drawing.Color.LightGreen
            End If
        Else
            LMsgPer.Text = ""
        End If

        ' ✅ შეტყობინებები სივრცისთვის
        If SelectedSpaceButton IsNot Nothing Then
            If spaceMessage <> "" Then
                LMsgSpace.Text = spaceMessage
                LMsgSpace.BackColor = System.Drawing.Color.LightCoral
            Else
                LMsgSpace.Text = "მოცემულ დროს სივრცე თავისუფალია"
                LMsgSpace.BackColor = System.Drawing.Color.LightGreen
            End If
        Else
            LMsgSpace.Text = ""
        End If

    End Sub
    Private Sub THourOrMin_Changed(sender As Object, e As EventArgs) Handles THour.TextChanged, TMin.TextChanged
        If isChangingDateTime Then Exit Sub
        Try
            isChangingDateTime = True
            UpdateStatusVisibility()
            CheckAvailability()
        Finally
            isChangingDateTime = False
        End Try
    End Sub
    ' ✅ თარიღის არჩევისას (CloseUp ივენთი, თავიდან იცილებს ციკლს)
    Private Sub DTP1_CloseUp(sender As Object, e As EventArgs) Handles DTP1.CloseUp
        If isChangingDateTime Then Return
        Try
            isChangingDateTime = True
            ' მხოლოდ აქ ვიძახებთ სინქრ- და ვალიდაციას
            CheckAvailability()
            ValidateForm()
        Finally
            isChangingDateTime = False
        End Try
    End Sub
    'თანხაზე მხოლოდ ციფრების შეყვანა
    Private Sub TCost_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TCost.KeyPress
        ' დაშვებულია მხოლოდ ციფრები, წერტილი და backspace
        If Not (Char.IsDigit(e.KeyChar) OrElse e.KeyChar = "."c OrElse e.KeyChar = ControlChars.Back) Then
            e.Handled = True
        End If

        ' მხოლოდ ერთი წერტილის დაშვება
        If e.KeyChar = "."c AndAlso TCost.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

    Private Sub BTNHourUp_Click(sender As Object, e As EventArgs) Handles BTNHourUp.Click
        THour.Text += 1
        If THour.Text = 24 Then THour.Text = 0
    End Sub
    Private Sub BTNHourDown_Click(sender As Object, e As EventArgs) Handles BTNHourDown.Click
        THour.Text -= 1
        If THour.Text < 0 Then THour.Text = 23
    End Sub
    Private Sub BTNMinUp_Click(sender As Object, e As EventArgs) Handles BTNMinUp.Click
        TMin.Text += 5
        If TMin.Text >= 60 Then TMin.Text = 0
    End Sub
    Private Sub BTNMinDown_Click(sender As Object, e As EventArgs) Handles BTNMinDown.Click
        TMin.Text -= 5
        If TMin.Text < 0 Then TMin.Text = 55
    End Sub
    Private Sub BTNDurUp_Click(sender As Object, e As EventArgs) Handles BTNDurUp.Click
        TDur.Text += 5
    End Sub
    Private Sub BTNDurDown_Click(sender As Object, e As EventArgs) Handles BTNDurDown.Click
        TDur.Text -= 5
        If TDur.Text < 0 Then TDur.Text = 0
    End Sub

    Private Sub CBBeneSurname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBBeneSurname.SelectedIndexChanged
        ValidateForm()
        CheckAvailability()
    End Sub
    Private Sub CBPer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBPer.SelectedIndexChanged
        ValidateForm()
        CheckAvailability()
    End Sub
    Private Sub CBTer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBTer.SelectedIndexChanged
        ValidateForm()
    End Sub
    Private Sub CBDaf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBDaf.SelectedIndexChanged
        ValidateForm()
    End Sub
    Private Sub TCost_TextChanged(sender As Object, e As EventArgs) Handles TCost.TextChanged
        ValidateForm()
    End Sub
    Private Sub RegisterStatusHandlers()
        For Each rb In {RB1, RB2, RB3, RB4, RB5, RB6}
            AddHandler rb.CheckedChanged, AddressOf StatusRadio_CheckedChanged
        Next
    End Sub
    Private Sub StatusRadio_CheckedChanged(sender As Object, e As EventArgs)
        ValidateForm()
    End Sub

    Private Sub BtnAddBene_Click(sender As Object, e As EventArgs) Handles BtnAddBene.Click
        FBene.ShowDialog()
    End Sub
    Private Sub BtnAddPer_Click(sender As Object, e As EventArgs) Handles BtnAddPer.Click
        FPer.ShowDialog()
    End Sub
    Private Sub BtnAddTer_Click(sender As Object, e As EventArgs) Handles BtnAddTer.Click
        FTer.ShowDialog()
    End Sub
    Private Sub BtnAddDaf_Click(sender As Object, e As EventArgs) Handles BtnAddDaf.Click
        FDaf.ShowDialog()
    End Sub

    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        Dim newForm As New Form2()
        newForm.Show()
        Me.Close()
    End Sub

    Private Sub TDur_TextChanged(sender As Object, e As EventArgs) Handles TDur.TextChanged
        CheckAvailability()
        ValidateForm()
    End Sub
End Class
