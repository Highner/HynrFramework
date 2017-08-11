Public Class HynrItemViewFormDialog
    Inherits HynrItemViewForm

    Public WithEvents ButtonCancel As System.Windows.Forms.Button

    Public WithEvents ButtonOK As System.Windows.Forms.Button

    Public Sub New()
        MyBase.New
        InitializeComponent()
    End Sub
    Public Sub New(ByRef settings As HynrUISettings, ByRef datacontext As Object)
        MyBase.New(settings, datacontext)
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(364, 282)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 0
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(283, 282)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'HynrItemViewFormDialog
        '
        Me.AcceptButton = Me.ButtonOK
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(451, 317)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Name = "HynrItemViewFormDialog"
        Me.ResumeLayout(False)

    End Sub

    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub
End Class
