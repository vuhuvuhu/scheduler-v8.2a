' 📅 UC_Calendar.vb – კალენდრის ბადის მოდული (კვირის ხედვა)
Imports Google.Apis.Sheets.v4
Imports Google.Apis.Sheets.v4.Data
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Globalization

Public Class UC_Calendar
    Inherits UserControl

    ' 🔁 Scrollable Grid Panel
    Private PnlGrid As New Panel()
    Private PnlHeader As Panel

    ' 📋 სეანსების სია
    Private AllSessions As New List(Of SessionRecord)()

    ' 📅 პარამეტრები
    Private SpaceNames As String() = {"მწვანე აბა", "ლურჯი აბა", "სენსორი", "ფიზიკური", "მეტყველება", "მუსიკა", "საკლასო", "თერაპევტი", "სხვა"}
    Private DaysOfWeek As String() = {"ორშაბათი", "სამშაბათი", "ოთხშაბათი", "ხუთშაბათი", "პარასკევი", "შაბათი", "კვირა"}
    Private StartTime As TimeSpan = TimeSpan.FromHours(9)
    Private EndTime As TimeSpan = TimeSpan.FromHours(20)
    Private IntervalMinutes As Integer = 30

    Public Sub New()
        InitializeComponent()
        Me.Dock = DockStyle.Fill
        InitLayout()
        LoadFilterDefaults()
        LoadAllSessions()
    End Sub

    Private Sub InitLayout()
        PnlHeader = New Panel With {
            .Height = 90,
            .Dock = DockStyle.Top,
            .BackColor = System.Drawing.Color.White,
            .Name = "PnlHeader"
        }
        Me.Controls.Add(PnlHeader)

        PnlGrid.Dock = DockStyle.Fill
        PnlGrid.AutoScroll = True
        PnlGrid.BackColor = System.Drawing.Color.White
        Me.Controls.Add(PnlGrid)
    End Sub

    Private Sub LoadFilterDefaults()
        RBWeek.Checked = True
        RBDay.Checked = False
        RBMonth.Checked = False
        RBSpace.Checked = True
        RBPer.Checked = False
        CBName.Visible = False
        CBSurname.Visible = False
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        CheckBox3.Checked = True
        CheckBox4.Checked = True
        CheckBox5.Checked = True
        CheckBox6.Checked = False
        CheckBox7.Checked = False
    End Sub

    Private Sub LoadAllSessions()
        Try
            If Form1.service Is Nothing Then
                MessageBox.Show("Google Sheets სერვისი არაა ინიციალიზებული.")
                Exit Sub
            End If

            Dim range As String = "DB-Schedule!A2:Z"
            Dim request = Form1.service.Spreadsheets.Values.Get(Form1.spreadsheetId, range)
            Dim response = request.Execute()

            If response.Values Is Nothing Then Exit Sub
            AllSessions.Clear()

            For Each row In response.Values
                Dim session As New SessionRecord With {
    .N = GetValue(row, 0),
    .DateStr = GetValue(row, 5),
    .TimeStr = GetValue(row, 5),
    .DurationStr = GetValue(row, 6),
    .Group = GetValue(row, 7),
    .BeneName = GetValue(row, 3),
    .BeneSurname = GetValue(row, 4),
    .Therapist = GetValue(row, 8),
    .Therapy = GetValue(row, 9),
    .Space = GetValue(row, 10),
    .Cost = GetValue(row, 11),
    .Status = GetValue(row, 12),
    .Comment = GetValue(row, 14),
    .Program = GetValue(row, 13),
    .EditDate = GetValue(row, 1),
    .Author = GetValue(row, 2)
}
                AllSessions.Add(session)
            Next

            GenerateGrid()

        Catch ex As Exception
            MessageBox.Show("შეცდომა სეანსების ჩატვირთვისას: " & ex.Message)
        End Try
    End Sub

    Private Function GetValue(row As IList(Of Object), index As Integer) As String
        If row.Count > index Then
            Return row(index).ToString().Trim()
        Else
            Return ""
        End If
    End Function

    Private Sub GenerateGrid()
        PnlGrid.Controls.Clear()

        Dim cellWidth As Integer = 150
        Dim cellHeight As Integer = 30
        Dim labelWidth As Integer = 70 ' 🟢 გაზრდილია სიგანე
        Dim leftOffset As Integer = labelWidth
        Dim topOffset As Integer = PnlHeader.Bottom

        ' სივრცეების ლეიბლები
        For s = 0 To SpaceNames.Length - 1
            Dim lbl As New Label With {
                .Text = SpaceNames(s),
                .Width = cellWidth,
                .Height = cellHeight,
                .Left = leftOffset + s * cellWidth,
                .Top = 60,
                .TextAlign = ContentAlignment.MiddleCenter,
                .ForeColor = System.Drawing.Color.White,
                .Font = New Font("Segoe UI", 10, FontStyle.Bold),
                .BackColor = System.Drawing.Color.FromArgb(0, 51, 102),
                .BorderStyle = BorderStyle.None
            }
            Panel1.Controls.Add(lbl)
        Next

        Dim startOfWeek As Date = Date.Today.AddDays(-(CInt(Date.Today.DayOfWeek) + 6) Mod 7)
        Dim dayBlockHeight As Integer = ((EndTime - StartTime).TotalMinutes / IntervalMinutes) * cellHeight
        Dim currentTop As Integer = topOffset

        For i = 0 To DaysOfWeek.Length - 1
            Dim dayName As String = DaysOfWeek(i)
            Dim dayDate As Date = startOfWeek.AddDays(i)
            Dim labelText As String = dayName & " " & dayDate.ToString("dd.MM.yy")

            Dim dayLabel As New Label With {
                .Font = New Font("Segoe UI", 12, FontStyle.Bold),
                .Width = 30,
                .Height = dayBlockHeight,
                .Left = 0,
                .Top = currentTop,
                .BackColor = System.Drawing.Color.LightBlue,
                .BorderStyle = BorderStyle.FixedSingle
            }
            AddHandler dayLabel.Paint, Sub(sender2, e2)
                                           Dim g = e2.Graphics
                                           g.TranslateTransform(dayLabel.Width / 2, dayLabel.Height / 2)
                                           g.RotateTransform(-90)
                                           g.DrawString(labelText, dayLabel.Font, Brushes.Black, -40, -10)
                                           g.ResetTransform()
                                       End Sub
            PnlGrid.Controls.Add(dayLabel)

            Dim currentTime As TimeSpan = StartTime
            Dim innerTop As Integer = currentTop
            While currentTime < EndTime
                Dim timeLabel As New Label With {
                    .Text = currentTime.ToString("hh\:mm"),
                    .Width = labelWidth,
                    .Height = cellHeight,
                    .Left = 0,
                    .Top = innerTop,
                    .TextAlign = ContentAlignment.MiddleRight,
                    .BorderStyle = BorderStyle.FixedSingle,
                    .BackColor = System.Drawing.Color.White,
                    .Font = New Font("Segoe UI", 9, FontStyle.Regular)
                }
                PnlGrid.Controls.Add(timeLabel)

                For s = 0 To SpaceNames.Length - 1
                    Dim isEvenRow As Boolean = ((innerTop \ cellHeight) Mod 2 = 0)
                    Dim rowColor As System.Drawing.Color = If(isEvenRow, System.Drawing.Color.White, System.Drawing.Color.FromArgb(235, 235, 235))

                    Dim box As New Label With {
                        .Width = cellWidth,
                        .Height = cellHeight,
                        .Left = leftOffset + s * cellWidth,
                        .Top = innerTop,
                        .BackColor = rowColor,
                        .BorderStyle = BorderStyle.None
                    }
                    AddHandler box.Paint, Sub(senderBox, eBox)
                                              Dim g = eBox.Graphics
                                              Dim pen = New Pen(System.Drawing.Color.Black, 1)
                                              g.DrawLine(pen, 0, 0, 0, box.Height)
                                              g.DrawLine(pen, box.Width - 1, 0, box.Width - 1, box.Height)
                                          End Sub
                    PnlGrid.Controls.Add(box)
                Next

                innerTop += cellHeight
                currentTime = currentTime.Add(TimeSpan.FromMinutes(IntervalMinutes))
            End While

            AddSessionCards(currentTop, DaysOfWeek(i))
            currentTop += dayBlockHeight
        Next
    End Sub

    Private Sub AddSessionCards(dayTop As Integer, dayName As String)
        Dim cellHeight As Integer = 30
        Dim cellWidth As Integer = 150
        Dim labelWidth As Integer = 70
        Dim leftOffset As Integer = labelWidth

        ' 🐞 დეტალური შემოწმება თითოეული სეანსისთვის
        For Each session In AllSessions
            Debug.WriteLine("🔍 შემოწმება: " & session.EditDate & " " & session.TimeStr & " | " & session.Status)
            Dim parts = session.DateStr.Split(" "c)
            If parts.Length < 2 Then
                Debug.WriteLine("❌ თარიღი ვერ გაიშიფრა: " & session.DateStr)
                Continue For
            End If

            Dim dateOnly = parts(0)
            Dim timeOnly = parts(1)

            Dim sessionDate As DateTime
            If Not Date.TryParseExact(dateOnly, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, sessionDate) Then
                Debug.WriteLine("❌ თარიღის ნაწილი არასწორია: " & dateOnly)
                Continue For
            End If

            Dim startTime As TimeSpan
            If Not TimeSpan.TryParse(timeOnly, startTime) Then
                Debug.WriteLine("❌ დრო ვერ გაიშიფრა (Parse): " & timeOnly)
                Continue For
            End If
            Dim startOfWeek As Date = Date.Today.AddDays(-(CInt(Date.Today.DayOfWeek) + 6) Mod 7)
            Dim currentDate As Date = startOfWeek.AddDays(Array.IndexOf(DaysOfWeek, dayName))
            If sessionDate.Date <> currentDate.Date Then
                Debug.WriteLine("⛔ თარიღი არ ემთხვევა: " & sessionDate.ToString("dd.MM.yyyy") & " vs " & currentDate.ToString("dd.MM.yyyy"))
                Continue For
            End If

            ' ✅ სტატუსის გაფილტვაც
            Dim allowedStatuses As New List(Of String)
            If CheckBox1.Checked Then allowedStatuses.Add("დაგეგმილი")
            If CheckBox2.Checked Then allowedStatuses.Add("შესრულებული")
            If CheckBox3.Checked Then allowedStatuses.Add("გაცდენა საპატიო")
            If CheckBox4.Checked Then allowedStatuses.Add("გაცდენა არასაპატიო")
            If CheckBox5.Checked Then allowedStatuses.Add("აღდგენა")
            If CheckBox6.Checked Then allowedStatuses.Add("პროგრამით გატარება")
            If CheckBox7.Checked Then allowedStatuses.Add("გაუქმებული")
            If Not allowedStatuses.Contains(session.Status.Trim()) Then
                Debug.WriteLine("⚠️ სტატუსი გაფილტრულია: " & session.Status)
                Continue For
            End If

            ' ⛔ თავიდან ვეღარ ვაცხადებთ timeOnly და startTime, რადგან უკვე გამოცხადდა ზემოთ
            Dim duration As Integer
            If Not Integer.TryParse(session.DurationStr, duration) Then
                Debug.WriteLine("⚠️ ვერ იქნა აღქმული ხანგძლივობა: " & session.DurationStr)
                Continue For
            End If
            Dim startRow As Integer = (startTime.TotalMinutes - startTime.TotalMinutes) \ IntervalMinutes
            Dim topPos As Integer = dayTop + (startRow * cellHeight)
            Dim height As Integer = (duration \ IntervalMinutes) * cellHeight

            Dim spaceIndex As Integer = Array.IndexOf(SpaceNames, session.Space)
            If spaceIndex = -1 Then Continue For
            Dim leftPos As Integer = leftOffset + spaceIndex * cellWidth

            Dim card As New Label With {
                .Text = session.BeneName & " " & session.BeneSurname & vbCrLf & session.Therapist & vbCrLf & session.Therapy,
                .Width = cellWidth,
                .Height = height,
                .Left = leftPos,
                .Top = topPos,
                .Font = New Font("Segoe UI", 8, FontStyle.Regular),
                .BackColor = GetColorByStatus(session.Status),
                .ForeColor = System.Drawing.Color.Black,
                .TextAlign = ContentAlignment.TopLeft,
                .BorderStyle = BorderStyle.Fixed3D
            }
            Debug.WriteLine("✅ ბარათი დამატდა: " & session.BeneName & " " & session.DateStr & " - " & session.Status)
            Debug.WriteLine("📍 პოზიცია: Top=" & card.Top & ", Left=" & card.Left & ", Height=" & card.Height & ", Width=" & card.Width)
            card.BringToFront()
            PnlGrid.Controls.Add(card)
        Next
    End Sub

    Private Function GetColorByStatus(status As String) As System.Drawing.Color
        Select Case status.Trim().ToLower()
            Case "დაგეგმილი"
                Return System.Drawing.Color.LightGray
            Case "შესრულებული"
                Return System.Drawing.Color.LightGreen
            Case "გაცდენა არასაპატიო"
                Return System.Drawing.Color.LightCoral
            Case "გაცდენა საპატიო"
                Return System.Drawing.Color.LightYellow
            Case "აღდგენა"
                Return System.Drawing.Color.MediumPurple
            Case "პროგრამით გატარება"
                Return System.Drawing.Color.DarkOrange
            Case "გაუქმებული"
                Return System.Drawing.Color.LightSlateGray
            Case Else
                Return System.Drawing.Color.White
        End Select
    End Function

    Private Class SessionRecord
        Public Property N As String
        Public Property DateStr As String
        Public Property TimeStr As String
        Public Property DurationStr As String
        Public Property Group As String
        Public Property BeneName As String
        Public Property BeneSurname As String
        Public Property Therapist As String
        Public Property Therapy As String
        Public Property Space As String
        Public Property Cost As String
        Public Property Status As String
        Public Property Comment As String
        Public Property Program As String
        Public Property EditDate As String
        Public Property Author As String
    End Class

    Private Function GetDayOfWeekEnum(dayName As String) As DayOfWeek
        Select Case dayName.Trim().ToLower()
            Case "ორშაბათი" : Return DayOfWeek.Monday
            Case "სამშაბათი" : Return DayOfWeek.Tuesday
            Case "ოთხშაბათი" : Return DayOfWeek.Wednesday
            Case "ხუთშაბათი" : Return DayOfWeek.Thursday
            Case "პარასკევი" : Return DayOfWeek.Friday
            Case "შაბათი" : Return DayOfWeek.Saturday
            Case "კვირა" : Return DayOfWeek.Sunday
            Case Else : Return DayOfWeek.Monday
        End Select
    End Function

End Class
