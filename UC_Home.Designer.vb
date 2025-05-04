<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UC_Home
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
        Me.BtnAddSchedule = New System.Windows.Forms.Button()
        Me.BtnRef = New System.Windows.Forms.Button()
        Me.DgvDagegmili = New System.Windows.Forms.DataGridView()
        Me.BtnPrev = New System.Windows.Forms.Button()
        Me.BtnNext = New System.Windows.Forms.Button()
        CType(Me.DgvDagegmili, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnAddSchedule
        '
        Me.BtnAddSchedule.Location = New System.Drawing.Point(3, 3)
        Me.BtnAddSchedule.Name = "BtnAddSchedule"
        Me.BtnAddSchedule.Size = New System.Drawing.Size(169, 23)
        Me.BtnAddSchedule.TabIndex = 0
        Me.BtnAddSchedule.Text = "ახალი ჩანაწერის დამატება"
        Me.BtnAddSchedule.UseVisualStyleBackColor = True
        '
        'BtnRef
        '
        Me.BtnRef.Location = New System.Drawing.Point(178, 3)
        Me.BtnRef.Name = "BtnRef"
        Me.BtnRef.Size = New System.Drawing.Size(75, 23)
        Me.BtnRef.TabIndex = 6
        Me.BtnRef.Text = "განახლება"
        Me.BtnRef.UseVisualStyleBackColor = True
        '
        'DgvDagegmili
        '
        Me.DgvDagegmili.AllowUserToOrderColumns = True
        Me.DgvDagegmili.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DgvDagegmili.Location = New System.Drawing.Point(3, 32)
        Me.DgvDagegmili.Name = "DgvDagegmili"
        Me.DgvDagegmili.Size = New System.Drawing.Size(970, 480)
        Me.DgvDagegmili.TabIndex = 3
        '
        'BtnPrev
        '
        Me.BtnPrev.Location = New System.Drawing.Point(3, 518)
        Me.BtnPrev.Name = "BtnPrev"
        Me.BtnPrev.Size = New System.Drawing.Size(75, 23)
        Me.BtnPrev.TabIndex = 4
        Me.BtnPrev.Text = "წინა"
        Me.BtnPrev.UseVisualStyleBackColor = True
        '
        'BtnNext
        '
        Me.BtnNext.Location = New System.Drawing.Point(84, 518)
        Me.BtnNext.Name = "BtnNext"
        Me.BtnNext.Size = New System.Drawing.Size(75, 23)
        Me.BtnNext.TabIndex = 5
        Me.BtnNext.Text = "შემდეგი"
        Me.BtnNext.UseVisualStyleBackColor = True
        '
        'UC_Home
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.BtnNext)
        Me.Controls.Add(Me.BtnAddSchedule)
        Me.Controls.Add(Me.BtnPrev)
        Me.Controls.Add(Me.BtnRef)
        Me.Controls.Add(Me.DgvDagegmili)
        Me.Name = "UC_Home"
        Me.Size = New System.Drawing.Size(979, 549)
        CType(Me.DgvDagegmili, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents BtnAddSchedule As Button
    Friend WithEvents BtnRef As Button
    Friend WithEvents DgvDagegmili As DataGridView
    Friend WithEvents BtnPrev As Button
    Friend WithEvents BtnNext As Button
End Class
