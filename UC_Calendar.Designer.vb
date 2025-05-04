<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class UC_Calendar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.DTPCalendar = New System.Windows.Forms.DateTimePicker()
        Me.RBDay = New System.Windows.Forms.RadioButton()
        Me.RBWeek = New System.Windows.Forms.RadioButton()
        Me.RBMonth = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RBSpace = New System.Windows.Forms.RadioButton()
        Me.RBPer = New System.Windows.Forms.RadioButton()
        Me.RBBene = New System.Windows.Forms.RadioButton()
        Me.CBName = New System.Windows.Forms.ComboBox()
        Me.CBSurname = New System.Windows.Forms.ComboBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CheckBox2 = New System.Windows.Forms.CheckBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.CheckBox4 = New System.Windows.Forms.CheckBox()
        Me.CheckBox5 = New System.Windows.Forms.CheckBox()
        Me.CheckBox6 = New System.Windows.Forms.CheckBox()
        Me.CheckBox7 = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'DTPCalendar
        '
        Me.DTPCalendar.Location = New System.Drawing.Point(3, 3)
        Me.DTPCalendar.Name = "DTPCalendar"
        Me.DTPCalendar.Size = New System.Drawing.Size(200, 20)
        Me.DTPCalendar.TabIndex = 0
        '
        'RBDay
        '
        Me.RBDay.AutoSize = True
        Me.RBDay.Location = New System.Drawing.Point(6, 9)
        Me.RBDay.Name = "RBDay"
        Me.RBDay.Size = New System.Drawing.Size(49, 17)
        Me.RBDay.TabIndex = 1
        Me.RBDay.TabStop = True
        Me.RBDay.Text = "დღე"
        Me.RBDay.UseVisualStyleBackColor = True
        '
        'RBWeek
        '
        Me.RBWeek.AutoSize = True
        Me.RBWeek.Location = New System.Drawing.Point(61, 9)
        Me.RBWeek.Name = "RBWeek"
        Me.RBWeek.Size = New System.Drawing.Size(55, 17)
        Me.RBWeek.TabIndex = 2
        Me.RBWeek.TabStop = True
        Me.RBWeek.Text = "კვირა"
        Me.RBWeek.UseVisualStyleBackColor = True
        '
        'RBMonth
        '
        Me.RBMonth.AutoSize = True
        Me.RBMonth.Location = New System.Drawing.Point(122, 9)
        Me.RBMonth.Name = "RBMonth"
        Me.RBMonth.Size = New System.Drawing.Size(47, 17)
        Me.RBMonth.TabIndex = 3
        Me.RBMonth.TabStop = True
        Me.RBMonth.Text = "თვე"
        Me.RBMonth.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(209, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "პერიოდი:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(441, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "ჩვენება:"
        '
        'RBSpace
        '
        Me.RBSpace.AutoSize = True
        Me.RBSpace.Location = New System.Drawing.Point(6, 9)
        Me.RBSpace.Name = "RBSpace"
        Me.RBSpace.Size = New System.Drawing.Size(63, 17)
        Me.RBSpace.TabIndex = 7
        Me.RBSpace.TabStop = True
        Me.RBSpace.Text = "სივრცე"
        Me.RBSpace.UseVisualStyleBackColor = True
        '
        'RBPer
        '
        Me.RBPer.AutoSize = True
        Me.RBPer.Location = New System.Drawing.Point(75, 9)
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
        Me.RBBene.Location = New System.Drawing.Point(167, 11)
        Me.RBBene.Name = "RBBene"
        Me.RBBene.Size = New System.Drawing.Size(97, 17)
        Me.RBBene.TabIndex = 9
        Me.RBBene.TabStop = True
        Me.RBBene.Text = "ბენეფიციარი"
        Me.RBBene.UseVisualStyleBackColor = True
        '
        'CBName
        '
        Me.CBName.FormattingEnabled = True
        Me.CBName.Location = New System.Drawing.Point(763, 7)
        Me.CBName.Name = "CBName"
        Me.CBName.Size = New System.Drawing.Size(121, 21)
        Me.CBName.TabIndex = 10
        '
        'CBSurname
        '
        Me.CBSurname.FormattingEnabled = True
        Me.CBSurname.Location = New System.Drawing.Point(888, 7)
        Me.CBSurname.Name = "CBSurname"
        Me.CBSurname.Size = New System.Drawing.Size(121, 21)
        Me.CBSurname.TabIndex = 11
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(82, 34)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(90, 17)
        Me.CheckBox1.TabIndex = 12
        Me.CheckBox1.Text = "დაგეგმილი"
        Me.CheckBox1.UseVisualStyleBackColor = True
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
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(178, 34)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(110, 17)
        Me.CheckBox2.TabIndex = 14
        Me.CheckBox2.Text = "შესრულებული"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(294, 34)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(120, 17)
        Me.CheckBox3.TabIndex = 15
        Me.CheckBox3.Text = "გაცდენა საპატიო"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(420, 34)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(140, 17)
        Me.CheckBox4.TabIndex = 16
        Me.CheckBox4.Text = "გაცდენა არასაპატიო"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(566, 34)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(74, 17)
        Me.CheckBox5.TabIndex = 17
        Me.CheckBox5.Text = "აღდგენა"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(646, 34)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(144, 17)
        Me.CheckBox6.TabIndex = 18
        Me.CheckBox6.Text = "პროგრამით გატარება"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Location = New System.Drawing.Point(796, 34)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(94, 17)
        Me.CheckBox7.TabIndex = 19
        Me.CheckBox7.Text = "გაუქმებული"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RBMonth)
        Me.GroupBox1.Controls.Add(Me.RBDay)
        Me.GroupBox1.Controls.Add(Me.RBWeek)
        Me.GroupBox1.Location = New System.Drawing.Point(263, -4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(178, 33)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RBSpace)
        Me.GroupBox2.Controls.Add(Me.RBPer)
        Me.GroupBox2.Controls.Add(Me.RBBene)
        Me.GroupBox2.Location = New System.Drawing.Point(489, -4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(268, 33)
        Me.GroupBox2.TabIndex = 21
        Me.GroupBox2.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.DTPCalendar)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.CheckBox7)
        Me.Panel1.Controls.Add(Me.CBName)
        Me.Panel1.Controls.Add(Me.CheckBox6)
        Me.Panel1.Controls.Add(Me.CBSurname)
        Me.Panel1.Controls.Add(Me.CheckBox5)
        Me.Panel1.Controls.Add(Me.CheckBox1)
        Me.Panel1.Controls.Add(Me.CheckBox4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.CheckBox3)
        Me.Panel1.Controls.Add(Me.CheckBox2)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1025, 90)
        Me.Panel1.TabIndex = 22
        '
        'UC_Calendar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Panel1)
        Me.Name = "UC_Calendar"
        Me.Size = New System.Drawing.Size(1041, 557)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents DTPCalendar As DateTimePicker
    Friend WithEvents RBDay As RadioButton
    Friend WithEvents RBWeek As RadioButton
    Friend WithEvents RBMonth As RadioButton
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents RBSpace As RadioButton
    Friend WithEvents RBPer As RadioButton
    Friend WithEvents RBBene As RadioButton
    Friend WithEvents CBName As ComboBox
    Friend WithEvents CBSurname As ComboBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents CheckBox4 As CheckBox
    Friend WithEvents CheckBox5 As CheckBox
    Friend WithEvents CheckBox6 As CheckBox
    Friend WithEvents CheckBox7 As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Panel1 As Panel
End Class
