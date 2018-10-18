Public Class HynrFormDialog
    Inherits HynrForm

    Protected Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Protected Friend WithEvents ButtonCancel As System.Windows.Forms.Button

#Region "Properties"
    Public Property CancelVisible() As Boolean
        Get
            Return ButtonCancel.Visible
        End Get
        Set(ByVal value As Boolean)
            ButtonCancel.Visible = value
            If value = False Then
                ButtonOK.Location = ButtonCancel.Location
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New(New HynrUISettings)
        InitializeComponent()
    End Sub
    Public Sub New(cancelvisible As Boolean)
        Me.New
        Me.CancelVisible = cancelvisible
    End Sub
#End Region

#Region "Controls"
    Private Sub ButtonOK_Click(sender As Object, e As EventArgs) Handles ButtonOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Close()
    End Sub
    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Close()
    End Sub
#End Region

#Region "Base Methods"
    Private Sub InitializeComponent()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(211, 263)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 0
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(130, 263)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'HynrFormDialog
        '
        Me.AcceptButton = Me.ButtonOK
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(298, 298)
        Me.ControlBox = False
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "HynrFormDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class
