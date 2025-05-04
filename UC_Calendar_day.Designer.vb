<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Calendar_day
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.BtnHDown = New System.Windows.Forms.Button()
        Me.BtnHUp = New System.Windows.Forms.Button()
        Me.BtnVDown = New System.Windows.Forms.Button()
        Me.BtnVUp = New System.Windows.Forms.Button()
        Me.RBSpace = New System.Windows.Forms.RadioButton()
        Me.DTPCalendar = New System.Windows.Forms.DateTimePicker()
        Me.RBPer = New System.Windows.Forms.RadioButton()
        Me.RBBene = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.CBSurname = New System.Windows.Forms.ComboBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CBName = New System.Windows.Forms.ComboBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.BtnAddSchedule = New System.Windows.Forms.Button()
        Me.BtnRef = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.BtnAddSchedule)
        Me.Panel1.Controls.Add(Me.BtnRef)
        Me.Panel1.Controls.Add(Me.BtnHDown)
        Me.Panel1.Controls.Add(Me.BtnHUp)
        Me.Panel1.Controls.Add(Me.BtnVDown)
        Me.Panel1.Controls.Add(Me.BtnVUp)
        Me.Panel1.Controls.Add(Me.RBSpace)
        Me.Panel1.Controls.Add(Me.DTPCalendar)
        Me.Panel1.Controls.Add(Me.RBPer)
        Me.Panel1.Controls.Add(Me.RBBene)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.CheckBox7)
        Me.Panel1.Controls.Add(Me.CBSurname)
        Me.Panel1.Controls.Add(Me.CheckBox6)
        Me.Panel1.Controls.Add(Me.CBName)
        Me.Panel1.Controls.Add(Me.CheckBox5)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.CheckBox4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.CheckBox3)
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1088, 60)
        Me.Panel1.TabIndex = 23
        '
        'BtnHDown
        '
        Me.BtnHDown.Location = New System.Drawing.Point(889, 18)
        Me.BtnHDown.Name = "BtnHDown"
        Me.BtnHDown.Size = New System.Drawing.Size(20, 20)
        Me.BtnHDown.TabIndex = 23
        Me.BtnHDown.Text = "-"
        Me.BtnHDown.UseVisualStyleBackColor = True
        '
        'BtnHUp
        '
        Me.BtnHUp.Location = New System.Drawing.Point(941, 18)
        Me.BtnHUp.Name = "BtnHUp"
        Me.BtnHUp.Size = New System.Drawing.Size(20, 20)
        Me.BtnHUp.TabIndex = 22
        Me.BtnHUp.Text = "+"
        Me.BtnHUp.UseVisualStyleBackColor = True
        '
        'BtnVDown
        '
        Me.BtnVDown.Location = New System.Drawing.Point(915, 30)
        Me.BtnVDown.Name = "BtnVDown"
        Me.BtnVDown.Size = New System.Drawing.Size(20, 20)
        Me.BtnVDown.TabIndex = 21
        Me.BtnVDown.Text = "-"
        Me.BtnVDown.UseVisualStyleBackColor = True
        '
        'BtnVUp
        '
        Me.BtnVUp.Location = New System.Drawing.Point(915, 5)
        Me.BtnVUp.Name = "BtnVUp"
        Me.BtnVUp.Size = New System.Drawing.Size(20, 20)
        Me.BtnVUp.TabIndex = 20
        Me.BtnVUp.Text = "+"
        Me.BtnVUp.UseVisualStyleBackColor = True
        '
        'RBSpace
        '
        Me.RBSpace.AutoSize = True
        Me.RBSpace.Location = New System.Drawing.Point(361, 5)
        Me.RBSpace.Name = "RBSpace"
        Me.RBSpace.Size = New System.Drawing.Size(63, 17)
        Me.RBSpace.TabIndex = 7
        Me.RBSpace.TabStop = True
        Me.RBSpace.Text = "სივრცე"
        Me.RBSpace.UseVisualStyleBackColor = True
        '
        'DTPCalendar
        '
        Me.DTPCalendar.Location = New System.Drawing.Point(3, 3)
        Me.DTPCalendar.Name = "DTPCalendar"
        Me.DTPCalendar.Size = New System.Drawing.Size(294, 20)
        Me.DTPCalendar.TabIndex = 0
        '
        'RBPer
        '
        Me.RBPer.AutoSize = True
        Me.RBPer.Location = New System.Drawing.Point(430, 5)
        Me.RBPer.Name = "RBPer"
        Me.RBPer.Size = New System.Drawing.Size(86, 17)
        Me.RBPer.TabIndex = 8
        Me.RBPer.TabStop = True
        Me.RBPer.Text = "თერაპევტი"
        Me.RBPer.UseVisualStyleBackColor = True
        '
        'RBBene
        '
        Me.RBBene.AutoSize = True
        Me.RBBene.Location = New System.Drawing.Point(522, 5)
        Me.RBBene.Name = "RBBene"
        Me.RBBene.Size = New System.Drawing.Size(97, 17)
        Me.RBBene.TabIndex = 9
        Me.RBBene.TabStop = True
        Me.RBBene.Text = "ბენეფიციარი"
        Me.RBBene.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(303, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "ჩვენება:"
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Location = New System.Drawing.Point(786, 30)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(94, 17)
        Me.CheckBox7.TabIndex = 19
        Me.CheckBox7.Text = "გაუქმებული"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'CBSurname
        '
        Me.CBSurname.FormattingEnabled = True
        Me.CBSurname.Location = New System.Drawing.Point(754, 3)
        Me.CBSurname.Name = "CBSurname"
        Me.CBSurname.Size = New System.Drawing.Size(121, 21)
        Me.CBSurname.TabIndex = 11
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(636, 30)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(144, 17)
        Me.CheckBox6.TabIndex = 18
        Me.CheckBox6.Text = "პროგრამით გატარება"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CBName
        '
        Me.CBName.FormattingEnabled = True
        Me.CBName.Location = New System.Drawing.Point(629, 3)
        Me.CBName.Name = "CBName"
        Me.CBName.Size = New System.Drawing.Size(121, 21)
        Me.CBName.TabIndex = 10
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(556, 30)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox5.TabIndex = 17
        Me.CheckBox5.Text = "აღდგენა"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(72, 30)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(90, 17)
        Me.CheckBox1.TabIndex = 12
        Me.CheckBox1.Text = "დაგეგმილი"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(410, 30)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(140, 17)
        Me.CheckBox4.TabIndex = 16
        Me.CheckBox4.Text = "გაცდენა არასაპატიო"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(2, 35)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "შესრულება:"
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(284, 30)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(120, 17)
        Me.CheckBox3.TabIndex = 15
        Me.CheckBox3.Text = "გაცდენა საპატიო"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(168, 30)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(110, 17)
        Me.CheckBox2.TabIndex = 14
        Me.CheckBox2.Text = "შესრულებული"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'BtnAddSchedule
        '
        Me.BtnAddSchedule.Location = New System.Drawing.Point(967, 4)
        Me.BtnAddSchedule.Name = "BtnAddSchedule"
        Me.BtnAddSchedule.Size = New System.Drawing.Size(131, 23)
        Me.BtnAddSchedule.TabIndex = 24
        Me.BtnAddSchedule.Text = "ჩანაწერის დამატება"
        Me.BtnAddSchedule.UseVisualStyleBackColor = True
        '
        'BtnRef
        '
        Me.BtnRef.Location = New System.Drawing.Point(967, 29)
        Me.BtnRef.Name = "BtnRef"
        Me.BtnRef.Size = New System.Drawing.Size(131, 21)
        Me.BtnRef.TabIndex = 25
        Me.BtnRef.Text = "განახლება"
        Me.BtnRef.UseVisualStyleBackColor = True
        '
        'UC_Calendar_day
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UC_Calendar_day"
        Me.Size = New System.Drawing.Size(1094, 527)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents DTPCalendar As DateTimePicker
    Friend WithEvents RBSpace As RadioButton
    Friend WithEvents RBPer As RadioButton
    Friend WithEvents RBBene As RadioButton
    Friend WithEvents Label2 As Label
    Friend WithEvents CheckBox7 As CheckBox
    Friend WithEvents CBSurname As ComboBox
    Friend WithEvents CheckBox6 As CheckBox
    Friend WithEvents CBName As ComboBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents BtnHDown As Button
    Friend WithEvents BtnHUp As Button
    Friend WithEvents BtnVDown As Button
    Friend WithEvents BtnVUp As Button
    Friend WithEvents BtnAddSchedule As Button
    Friend WithEvents BtnRef As Button
End Class
