' 📄 კალენდარი2.vb – კალენდარის UserControl და მონაცემთა მოდელი

' საჭირო Namespaces: კულტურის კონფიგურაცია, WinForms UI და Google Sheets API
Imports System.Globalization
Imports System.Windows.Forms
Imports Color = System.Drawing.Color  ' ალიასი System.Drawing.Color–ისთვის, რომ არ იყოს მკვეთრად აკროფატული
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.Sheets.v4

' მთავარი UserControl კლასი კალენდრისთვის
Public Class UC_Calendar_day
    Inherits UserControl

    ' 🌐 გახდება 30px თითო Row, 100px თითო Column
    Private currentRowHeight As Integer = 30
    Private currentColWidth As Integer = 100

    ' ⚙️ კონტროლერი იტვირთება Form-ზე გამოჩენისას
    Private Sub UC_Calendar_day_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Dock = DockStyle.Fill  ' UserControl ფარავს მშობელ ფორმას
        ConfigureDatePicker()      ' ქართული თარიღის კონფიგურაცია
        CreateGrid()               ' ცხრილის (grid) შექმნა
        PlaceCards()               ' ბარათების განთავსება ცხრილზე
    End Sub

    ' 🔄 თარიღის ცვლილებისას კალენდრის განახლება და ბარათების განთავსება
    Private Sub DTPCalendar_ValueChanged(sender As Object, e As EventArgs) Handles DTPCalendar.ValueChanged
        CreateGrid()
        PlaceCards()
    End Sub

    ' 📅 თარიღის ნავიგაციისთვის DatePicker ქართულ ენაზე
    Private Sub ConfigureDatePicker()
        Dim kaCulture As New CultureInfo("ka-GE")
        ' ქართული კვირის და თვის სახელები
        kaCulture.DateTimeFormat.DayNames = New String() {"კვირა", "ორშაბათი", "სამშაბათი", "ოთხშაბათი", "ხუთშაბათი", "პარასკევი", "შაბათი"}
        kaCulture.DateTimeFormat.MonthNames = New String() {"იანვარი", "თებერვალი", "მარტი", "აპრილი", "მაისი", "ივნისი", "ივლისი", "აგვისტო", "სექტემბერი", "ოქტომბერი", "ნოემბერი", "დეკემბერი", String.Empty}
        ' ეფექტი აქტიური ფაილში და UI-ში
        Threading.Thread.CurrentThread.CurrentCulture = kaCulture
        Threading.Thread.CurrentThread.CurrentUICulture = kaCulture
    End Sub

    ' ⌛ ძირითადად აქ ავაგებთ ცხრილს: საათების სვეტი და სივრცეების სვეტები
    Public Sub CreateGrid()
        ' exist–GridContainer წინა შექმნილისგან მოსაფრთხილებლად
        Me.Controls.RemoveByKey("gridContainer")

        ' სივრცეების სახელების მასივი
        Dim spaceNames As String() = {"მწვანე აბა", "ლურჯი აბა", "მუსიკა", "მეტყველება", "ფიზიკური", "სენსორი", "არტი", "თერაპევტი", "სხვა"}
        Dim timeColWidth As Integer = 40       ' დროის სვეტის ფიქსირებული სიგანე
        Dim headerHeight As Integer = 25       ' სათაურის (spaceNames) ზოლის სიმაღლე
        Dim rows As Integer = 22               ' 09:00-დან 19:30-მდე => 22 ინტერვალი (პირველი სათაური არ ითვლება)

        ' 🏗️ TableLayoutPanel კონფიგურაცია
        Dim grid As New TableLayoutPanel() With {
            .Name = "grid",
            .RowCount = rows + 1,                   ' ერთი პლუს სათაური
            .ColumnCount = spaceNames.Length + 1,   ' ერთი(time) + სივრცეCount
            .CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
            .GrowStyle = TableLayoutPanelGrowStyle.FixedSize,
            .Location = New Point(0, 0),
            .Size = New Size(timeColWidth + spaceNames.Length * currentColWidth, headerHeight + rows * currentRowHeight),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left
        }
        ' — სვეტი#0: დრო
        grid.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, timeColWidth))
        ' — ბენფიციარის სივრცეები
        For i As Integer = 1 To spaceNames.Length
            grid.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, currentColWidth))
        Next

        ' 📋 სათაურის მწკრივი: პირველი უჯრა ცარიელი (დრო), დანარჩენი spaceNames
        grid.RowStyles.Add(New RowStyle(SizeType.Absolute, headerHeight))
        Dim emptyLabel = New Label() With {
            .Text = String.Empty,
            .Dock = DockStyle.Fill,
            .BackColor = Color.MidnightBlue,
            .Margin = New System.Windows.Forms.Padding(0)
        }
        grid.Controls.Add(emptyLabel, 0, 0)
        For i As Integer = 0 To spaceNames.Length - 1
            Dim lbl As New Label() With {
                .Text = spaceNames(i),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleCenter,
                .BackColor = Color.MidnightBlue,
                .ForeColor = Color.White,
                .Font = New Font("Segoe UI", 9, FontStyle.Bold),
                .Margin = New System.Windows.Forms.Padding(0)
            }
            ' ვერტიკალური ზოლი (border imitation)
            Dim borderRight As New Panel() With {
                .Dock = DockStyle.Right,
                .Width = 1,
                .BackColor = Color.Black
            }
            lbl.Controls.Add(borderRight)
            grid.Controls.Add(lbl, i + 1, 0)
        Next

        ' ⏰ საათების და ფონის რიგები
        For r As Integer = 1 To rows
            grid.RowStyles.Add(New RowStyle(SizeType.Absolute, currentRowHeight))
            ' დროის Label
            Dim lblTime As New Label() With {
                .Text = DateTime.Today.AddHours(9).AddMinutes((r - 1) * 30).ToString("HH:mm"),
                .Dock = DockStyle.Fill,
                .TextAlign = ContentAlignment.MiddleRight,
                .Margin = New System.Windows.Forms.Padding(0)
            }
            grid.Controls.Add(lblTime, 0, r)

            ' საუბრები სივრცეების მიხედვით
            For c As Integer = 1 To spaceNames.Length
                Dim cell As New Panel() With {
                    .Dock = DockStyle.Fill,
                    .BackColor = If(r Mod 2 = 0, Color.White, Color.Gainsboro),
                    .Margin = New System.Windows.Forms.Padding(0)
                }
                Dim border As New Panel() With {
                    .Dock = DockStyle.Right,
                    .Width = 1,
                    .BackColor = Color.Black
                }
                cell.Controls.Add(border)
                grid.Controls.Add(cell, c, r)
            Next
        Next

        ' ↕️ გადავამოწმოთ: ჭირდება თუ არა სკროლი და დამატებითი სივრცე
        Dim needsVScroll = grid.Height > (Me.ClientSize.Height - 60)
        Dim needsHScroll = grid.Width > Me.ClientSize.Width
        Dim containerWidth = grid.Width + If(needsVScroll, SystemInformation.VerticalScrollBarWidth, 0)
        Dim containerHeight = grid.Height + If(needsHScroll, SystemInformation.HorizontalScrollBarHeight, 0)

        ' 📦 Scrollable Container
        Dim gridContainer As New Panel() With {
            .Name = "gridContainer",
            .AutoScroll = True,
            .Location = New Point(0, 60),  ' ოდნავ ქვემოთ, რომ DatePicker არ დაფაროს
            .Size = New Size(containerWidth, containerHeight),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
        }
        gridContainer.Controls.Add(grid)
        Me.Controls.Add(gridContainer)
    End Sub

    ' 🔼/🔽 Buttons გვეხმარება ცხრილის ზომის შემცირებას/გაფართოებას
    Private Sub BtnVUp_Click(sender As Object, e As EventArgs) Handles BtnVUp.Click
        currentRowHeight += 5  ' ზრდა ყველა RowHeight–ის
        CreateGrid() : PlaceCards()
    End Sub
    Private Sub BtnVDown_Click(sender As Object, e As EventArgs) Handles BtnVDown.Click
        If currentRowHeight > 15 Then currentRowHeight -= 5 ' მინიმუმ 15px
        CreateGrid() : PlaceCards()
    End Sub
    Private Sub BtnHUp_Click(sender As Object, e As EventArgs) Handles BtnHUp.Click
        currentColWidth += 10
        CreateGrid() : PlaceCards()
    End Sub
    Private Sub BtnHDown_Click(sender As Object, e As EventArgs) Handles BtnHDown.Click
        If currentColWidth > 50 Then currentColWidth -= 10 ' მინიმუმ 50px
        CreateGrid() : PlaceCards()
    End Sub

    ' 🎨 ბარათების განთავსება თარიღისა და სეანსის მიხედვით
    Public Sub PlaceCards()
        Dim records = LoadScheduleData()         ' საჭირო მონაცემები
        Dim selectedDate = DTPCalendar.Value.Date ' დღეს არჩეული თარიღი
        Dim spaceNames As String() = {"მწვანე აბა", "ლურჯი აბა", "მუსიკა", "მეტყველება", "ფიზიკური", "სენსორი", "არტი", "თერაპევტი", "სხვა"}
        Dim headerHeight As Integer = 25         ' სათაურის ზოლის სიმაღლე
        Dim startHour As Integer = 9             ' კალენდრის საწყისი საათი
        Dim timeColWidth As Integer = 40       ' დროის სვეტის ფიქსირებული სიგანე
        Dim nowTime As DateTime = DateTime.Now             ' კალენდრის საწყისი საათი
        'Dim nowTime As DateTime = DateTime.Now

        For Each rec In records
            If rec.SessionDate.Date = selectedDate Then
                ' Top პოზიციის გამოთვლა (headerHeight + ინტერვალების მიხედვით)
                Dim totalMinutes = (rec.SessionDate.Hour - startHour) * 60 + rec.SessionDate.Minute
                Dim minuteHeight As Double = currentRowHeight / 30.0 ' 1 წუთი რამდენი px-ია
                Dim offsetY As Integer = CInt(totalMinutes * minuteHeight)
                Dim topPos = headerHeight + offsetY  ' გადაადგილება ნახევარი საათით ზემოთ (ერთი სრული RowHeight)  ' გადაადგილება ნახევარი ხუთშაბათი ზოლისთვის (პოუსთის სწორობისთვის)

                ' Left პოზიცია სივრცესთან დაკავშირებით
                Dim spaceIndex = Array.IndexOf(spaceNames, rec.Space)
                If spaceIndex = -1 Then Continue For
                Dim leftPos = timeColWidth + spaceIndex * currentColWidth

                ' Card სიმაღლე
                Dim rawHeight As Integer = CInt(rec.Duration * minuteHeight)
                Dim cardH = Math.Max(rawHeight, currentRowHeight)

                ' სტატუსის მიხედვით ფერი
                Dim cardColor As Color
                Select Case rec.Status
                    Case "შესრულებული"
                        cardColor = Color.LightGreen
                    Case "აღდგენა"
                        cardColor = Color.Honeydew
                    Case "გაცდენა არასაპატიო"
                        cardColor = Color.Plum
                    Case "გაცდენა საპატიო"
                        cardColor = Color.LightYellow
                    Case "პროგრამით გატარება"
                        cardColor = Color.LightGray
                    Case "გაუქმება"
                        cardColor = Color.Red
                    Case "დაგეგმილი"
                        If rec.SessionDate < nowTime Then
                            cardColor = Color.LightCoral
                        Else
                            cardColor = Color.LightBlue
                        End If
                    Case Else
                        cardColor = Color.Gray
                End Select

                ' ბარათის შექმნა
                Dim card As New Panel() With {
                    .BackColor = cardColor,
                    .Location = New Point(leftPos, topPos),
                    .Size = New Size(currentColWidth - 2, cardH - 2),
                    .BorderStyle = BorderStyle.FixedSingle
                }
                ' ტექსტის დამატება
                Dim lbl As New Label() With {
                    .Text = rec.BeneName & " " & rec.BeneSurname & vbCrLf & rec.Therapist & vbCrLf & rec.Therapy,
                    .Dock = DockStyle.Fill,
                    .TextAlign = ContentAlignment.MiddleCenter,
                    .Font = New Font("Segoe UI", 8, FontStyle.Regular),
                    .ForeColor = Color.Black,
                    .Margin = New System.Windows.Forms.Padding(0)
                }
                card.Controls.Add(lbl)

                ' ✏️ რედაქტირების ღილაკი ბარათზე
                Dim btnEdit As New Button() With {
    .Text = "✎",
    .Font = New Font("Segoe UI", 7, FontStyle.Regular),
    .Size = New Size(20, 20),
    .FlatStyle = FlatStyle.Flat,
    .BackColor = Color.WhiteSmoke,
    .ForeColor = Color.Black,
    .Location = New Point(card.Width - 22, card.Height - 22),
    .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right,
    .Tag = rec
}
                AddHandler btnEdit.Click, Sub(s, ev)
                                              Dim r = CType(CType(s, Button).Tag, ScheduleRecord)
                                              Dim f2 As New Form2()
                                              With f2
                                                  .IsEditMode = True
                                                  .PrefillLN = r.RowId
                                                  .LNow.Text = r.EditDate
                                                  .PrefillBeneName = r.BeneName
                                                  .PrefillBeneSurname = r.BeneSurname
                                                  .PrefillTherapist = r.Therapist
                                                  .PrefillTherapy = r.Therapy
                                                  .PrefillProgram = r.Program
                                                  .PrefillPrice = r.Price
                                                  .PrefillIsGroup = r.Group
                                                  .PrefillComment = r.Coment ' კომენტარის გადაცემა
                                                  .PrefillDateTime = r.SessionDate.ToString("dd.MM.yyyy HH:mm")
                                                  .PrefillSpace = r.Space
                                                  .PrefillStatus = r.Status
                                                  .BtnAdd.Text = "შეცვლა"
                                                  .BtnClear.Visible = False
                                                  .ShowFromCalendarDay(Me) ' გამოვიყენოთ ShowFromCalendarDay
                                              End With
                                          End Sub
                card.Controls.Add(btnEdit)
                btnEdit.BringToFront()

                ' ბარათის დამატება GridContainer-ზე და BringToFront
                For Each ctl As Control In Me.Controls
                    If ctl.Name = "gridContainer" Then
                        ctl.Controls.Add(card)
                        card.BringToFront()
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    ' 📥 Google Sheets ცხრილიდან მონაცემების წამოღება ბარათებისთვის
    Private Function LoadScheduleData() As List(Of ScheduleRecord)
        Dim list As New List(Of ScheduleRecord)()
        Try
            Debug.WriteLine("Attempting to create GoogleCredential for LoadScheduleData...")
            Dim credential = GoogleCredential.FromFile("sinuous-pact-454212-m3-ce5ff9f96bc8.json").CreateScoped(SheetsService.Scope.SpreadsheetsReadonly)
            Debug.WriteLine("GoogleCredential created successfully.")
            Dim service = New SheetsService(New BaseClientService.Initializer() With {
            .HttpClientInitializer = credential,
            .ApplicationName = "SchedulerApp"
        })
            Debug.WriteLine("SheetsService created successfully.")
            Dim spreadsheetId As String = "1SrBc4vLKPui6467aNmF5Hw-WZEd7dfGhkeFjfcnUqog"
            Dim range As String = "DB-Schedule!A2:O"
            Dim request = service.Spreadsheets.Values.Get(spreadsheetId, range)
            Debug.WriteLine("Sending request to Google Sheets API...")
            Dim response = request.Execute()
            Debug.WriteLine("Response received from Google Sheets API.")
            Dim values = response.Values
            If values IsNot Nothing Then
                For Each row In values
                    If row.Count >= 14 Then
                        Dim rec As New ScheduleRecord() With {
                        .RowId = row(0)?.ToString().Trim(),
                        .EditDate = row(1)?.ToString().Trim(),
                        .Author = row(2)?.ToString().Trim(),
                        .BeneName = row(3)?.ToString().Trim(),
                        .BeneSurname = row(4)?.ToString().Trim(),
                        .Therapist = row(8)?.ToString().Trim(),
                        .Therapy = row(9)?.ToString().Trim(),
                        .Space = row(10)?.ToString().Trim(),
                        .Status = row(12)?.ToString().Trim(),
                        .Price = row(11)?.ToString().Trim(),
                        .Program = row(13)?.ToString().Trim(),
                        .Coment = If(row.Count > 14, row(14)?.ToString().Trim(), ""),
                        .Group = If(row.Count > 7, row(7)?.ToString().Trim().ToUpper() = "TRUE", False)
                    }
                        DateTime.TryParseExact(row(5)?.ToString().Trim(), "dd.MM.yyyy HH:mm", Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, rec.SessionDate)
                        Integer.TryParse(row(6)?.ToString().Trim(), rec.Duration)
                        list.Add(rec)
                    End If
                Next
            End If
        Catch ex As Exception
            Debug.WriteLine("LoadScheduleData Error: " & ex.Message & vbCrLf & ex.StackTrace)
            If ex.Message.Contains("NotFound") Then
                MessageBox.Show("შეცდომა მონაცემების წამოღებისას: ვერ მოიძებნა Google Spreadsheet.")
            Else
                MessageBox.Show("შეცდომა მონაცემების ჩატვირთვისას: " & ex.Message & vbCrLf & ex.StackTrace)
            End If
        End Try
        Return list
    End Function
    Private Sub BtnRef_Click(sender As Object, e As EventArgs) Handles BtnRef.Click
        CreateGrid()
        PlaceCards()
    End Sub

    Private Sub BtnAddSchedule_Click(sender As Object, e As EventArgs) Handles BtnAddSchedule.Click
        Dim f2 As New Form2()
        f2.ShowFromCalendarDay(Me) ' გადავცეთ UC_Calendar_day როგორც გამომძახებელი
        f2.BringToFront()
        f2.Focus()
    End Sub
End Class

' 📑 მონაცემთა მოდელი თითო ჩანაწერისთვის
Public Class ScheduleRecord
    Public Property EditDate As String
    Public Property Author As String
    Public Property BeneName As String
    Public Property BeneSurname As String
    Public Property Therapist As String
    Public Property Therapy As String
    Public Property SessionDate As DateTime
    Public Property Duration As Integer
    Public Property Space As String
    Public Property Status As String
    Public Property Price As String
    Public Property Program As String
    Public Property RowId As String
    Public Property Coment As String
    Public Property Group As Boolean
End Class
