<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Schedule
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
        Me.DgvSchedule = New System.Windows.Forms.DataGridView()
        Me.BtnNext = New System.Windows.Forms.Button()
        Me.BtnAddSchedule = New System.Windows.Forms.Button()
        Me.BtnPrev = New System.Windows.Forms.Button()
        Me.BtnRef = New System.Windows.Forms.Button()
        Me.LPageN = New System.Windows.Forms.Label()
        Me.DtpDan = New System.Windows.Forms.DateTimePicker()
        Me.DtpMde = New System.Windows.Forms.DateTimePicker()
        Me.CBBeneName = New System.Windows.Forms.ComboBox()
        Me.CBBeneSurname = New System.Windows.Forms.ComboBox()
        Me.CBPer = New System.Windows.Forms.ComboBox()
        Me.CBTer = New System.Windows.Forms.ComboBox()
        Me.CBShes = New System.Windows.Forms.ComboBox()
        Me.ChBGroup = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LName = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.CBSpace = New System.Windows.Forms.ComboBox()
        Me.CBDaf = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        CType(Me.DgvSchedule, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DgvSchedule
        '
        Me.DgvSchedule.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvSchedule.Location = New System.Drawing.Point(3, 54)
        Me.DgvSchedule.Name = "DgvSchedule"
        Me.DgvSchedule.Size = New System.Drawing.Size(1343, 498)
        Me.DgvSchedule.TabIndex = 0
        '
        'BtnNext
        '
        Me.BtnNext.Location = New System.Drawing.Point(189, 558)
        Me.BtnNext.Name = "BtnNext"
        Me.BtnNext.Size = New System.Drawing.Size(75, 23)
        Me.BtnNext.TabIndex = 9
        Me.BtnNext.Text = "შემდეგი"
        Me.BtnNext.UseVisualStyleBackColor = True
        '
        'BtnAddSchedule
        '
        Me.BtnAddSchedule.Location = New System.Drawing.Point(1209, 0)
        Me.BtnAddSchedule.Name = "BtnAddSchedule"
        Me.BtnAddSchedule.Size = New System.Drawing.Size(131, 23)
        Me.BtnAddSchedule.TabIndex = 7
        Me.BtnAddSchedule.Text = "ჩანაწერის დამატება"
        Me.BtnAddSchedule.UseVisualStyleBackColor = True
        '
        'BtnPrev
        '
        Me.BtnPrev.Location = New System.Drawing.Point(3, 558)
        Me.BtnPrev.Name = "BtnPrev"
        Me.BtnPrev.Size = New System.Drawing.Size(75, 23)
        Me.BtnPrev.TabIndex = 8
        Me.BtnPrev.Text = "წინა"
        Me.BtnPrev.UseVisualStyleBackColor = True
        '
        'BtnRef
        '
        Me.BtnRef.Location = New System.Drawing.Point(1209, 25)
        Me.BtnRef.Name = "BtnRef"
        Me.BtnRef.Size = New System.Drawing.Size(131, 21)
        Me.BtnRef.TabIndex = 10
        Me.BtnRef.Text = "განახლება"
        Me.BtnRef.UseVisualStyleBackColor = True
        '
        'LPageN
        '
        Me.LPageN.AutoSize = True
        Me.LPageN.Location = New System.Drawing.Point(84, 563)
        Me.LPageN.Name = "LPageN"
        Me.LPageN.Size = New System.Drawing.Size(39, 13)
        Me.LPageN.TabIndex = 11
        Me.LPageN.Text = "Label1"
        '
        'DtpDan
        '
        Me.DtpDan.Location = New System.Drawing.Point(3, 3)
        Me.DtpDan.Name = "DtpDan"
        Me.DtpDan.Size = New System.Drawing.Size(200, 20)
        Me.DtpDan.TabIndex = 12
        '
        'DtpMde
        '
        Me.DtpMde.Location = New System.Drawing.Point(3, 27)
        Me.DtpMde.Name = "DtpMde"
        Me.DtpMde.Size = New System.Drawing.Size(200, 20)
        Me.DtpMde.TabIndex = 13
        '
        'CBBeneName
        '
        Me.CBBeneName.FormattingEnabled = True
        Me.CBBeneName.Location = New System.Drawing.Point(317, 0)
        Me.CBBeneName.Name = "CBBeneName"
        Me.CBBeneName.Size = New System.Drawing.Size(121, 21)
        Me.CBBeneName.TabIndex = 14
        '
        'CBBeneSurname
        '
        Me.CBBeneSurname.FormattingEnabled = True
        Me.CBBeneSurname.Location = New System.Drawing.Point(317, 27)
        Me.CBBeneSurname.Name = "CBBeneSurname"
        Me.CBBeneSurname.Size = New System.Drawing.Size(121, 21)
        Me.CBBeneSurname.TabIndex = 15
        '
        'CBPer
        '
        Me.CBPer.FormattingEnabled = True
        Me.CBPer.Location = New System.Drawing.Point(518, 0)
        Me.CBPer.Name = "CBPer"
        Me.CBPer.Size = New System.Drawing.Size(182, 21)
        Me.CBPer.TabIndex = 16
        '
        'CBTer
        '
        Me.CBTer.FormattingEnabled = True
        Me.CBTer.Location = New System.Drawing.Point(518, 27)
        Me.CBTer.Name = "CBTer"
        Me.CBTer.Size = New System.Drawing.Size(182, 21)
        Me.CBTer.TabIndex = 17
        '
        'CBShes
        '
        Me.CBShes.FormattingEnabled = True
        Me.CBShes.Location = New System.Drawing.Point(1058, 1)
        Me.CBShes.Name = "CBShes"
        Me.CBShes.Size = New System.Drawing.Size(145, 21)
        Me.CBShes.TabIndex = 20
        '
        'ChBGroup
        '
        Me.ChBGroup.AutoSize = True
        Me.ChBGroup.Location = New System.Drawing.Point(1058, 25)
        Me.ChBGroup.Name = "ChBGroup"
        Me.ChBGroup.Size = New System.Drawing.Size(80, 17)
        Me.ChBGroup.TabIndex = 22
        Me.ChBGroup.Text = "ჯგუფური"
        Me.ChBGroup.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(209, 4)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(32, 13)
        Me.Label2.TabIndex = 23
        Me.Label2.Text = "-დან"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(209, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(32, 13)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "-მდე"
        '
        'LName
        '
        Me.LName.AutoSize = True
        Me.LName.Location = New System.Drawing.Point(262, 3)
        Me.LName.Name = "LName"
        Me.LName.Size = New System.Drawing.Size(49, 13)
        Me.LName.TabIndex = 25
        Me.LName.Text = "სახელი"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(262, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "გვარი"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(444, 3)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(68, 13)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "თერაპევტი"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(444, 30)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 28
        Me.Label6.Text = "თერაპია"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(706, 4)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(81, 13)
        Me.Label7.TabIndex = 29
        Me.Label7.Text = "დაფინანსება"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(706, 29)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(45, 13)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "სივრცე"
        '
        'CBSpace
        '
        Me.CBSpace.FormattingEnabled = True
        Me.CBSpace.Location = New System.Drawing.Point(793, 27)
        Me.CBSpace.Name = "CBSpace"
        Me.CBSpace.Size = New System.Drawing.Size(182, 21)
        Me.CBSpace.TabIndex = 32
        '
        'CBDaf
        '
        Me.CBDaf.FormattingEnabled = True
        Me.CBDaf.Location = New System.Drawing.Point(793, 0)
        Me.CBDaf.Name = "CBDaf"
        Me.CBDaf.Size = New System.Drawing.Size(182, 21)
        Me.CBDaf.TabIndex = 31
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(981, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(71, 13)
        Me.Label9.TabIndex = 33
        Me.Label9.Text = "შესრულება"
        '
        'UC_Schedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.CBSpace)
        Me.Controls.Add(Me.CBDaf)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.LName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ChBGroup)
        Me.Controls.Add(Me.CBShes)
        Me.Controls.Add(Me.CBTer)
        Me.Controls.Add(Me.CBPer)
        Me.Controls.Add(Me.CBBeneSurname)
        Me.Controls.Add(Me.CBBeneName)
        Me.Controls.Add(Me.DtpMde)
        Me.Controls.Add(Me.DtpDan)
        Me.Controls.Add(Me.LPageN)
        Me.Controls.Add(Me.BtnNext)
        Me.Controls.Add(Me.BtnAddSchedule)
        Me.Controls.Add(Me.BtnPrev)
        Me.Controls.Add(Me.BtnRef)
        Me.Controls.Add(Me.DgvSchedule)
        Me.Name = "UC_Schedule"
        Me.Size = New System.Drawing.Size(1353, 620)
        CType(Me.DgvSchedule, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DgvSchedule As DataGridView
    Friend WithEvents BtnNext As Button
    Friend WithEvents BtnAddSchedule As Button
    Friend WithEvents BtnPrev As Button
    Friend WithEvents BtnRef As Button
    Friend WithEvents LPageN As Label
    Friend WithEvents DtpDan As DateTimePicker
    Friend WithEvents DtpMde As DateTimePicker
    Friend WithEvents CBBeneName As ComboBox
    Friend WithEvents CBBeneSurname As ComboBox
    Friend WithEvents CBPer As ComboBox
    Friend WithEvents CBTer As ComboBox
    Friend WithEvents CBShes As ComboBox
    Friend WithEvents ChBGroup As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents LName As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label8 As Label
    Friend WithEvents CBSpace As ComboBox
    Friend WithEvents CBDaf As ComboBox
    Friend WithEvents Label9 As Label
End Class
