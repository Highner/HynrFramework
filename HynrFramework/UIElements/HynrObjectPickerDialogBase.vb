Imports System.Windows.Forms

Public MustInherit Class HynrObjectPickerDialogBase
    Inherits HynrForm

#Region "Properties"
    Public WithEvents ButtonCancel As System.Windows.Forms.Button
    Public WithEvents LabelStrip As HynrLabelStrip
    Public WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents GridPanel As Panel

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

    Public Property LabelStripVisible As Boolean
        Get
            Return LabelStrip.Visible
        End Get
        Set(value As Boolean)
            LabelStrip.Visible = value
        End Set
    End Property

    Public Property LabelText As String
        Get
            Return LabelStrip.LabelText
        End Get
        Set(value As String)
            LabelStrip.LabelText = value
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New(New HynrUISettings)
        InitializeComponent()
    End Sub
    Public Sub New(ByVal settings As HynrUISettings)
        MyBase.New(settings)
        InitializeComponent()
    End Sub
    Public Sub New(ByVal settings As HynrUISettings, ByVal cancelvisible As Boolean)
        MyBase.New(settings)
        InitializeComponent()
        Me.CancelVisible = cancelvisible
    End Sub
#End Region

#Region "Base Methods"
    Public Overrides Sub ApplyHynrSettings()
        MyBase.ApplyHynrSettings()
        If Not IsNothing(LabelStrip) Then LabelStrip.HynrSettings = HynrSettings
    End Sub
#End Region

#Region "Methods"

#End Region

    Private Sub InitializeComponent()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.LabelStrip = New HynrFramework.HynrLabelStrip()
        Me.GridPanel = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(364, 288)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 0
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(283, 288)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'LabelStrip
        '
        Me.LabelStrip.Highlighted = False
        Me.LabelStrip.HynrSettings = Nothing
        Me.LabelStrip.IsSubHeader = False
        Me.LabelStrip.LabelText = ""
        Me.LabelStrip.Location = New System.Drawing.Point(0, 0)
        Me.LabelStrip.Name = "LabelStrip"
        Me.LabelStrip.Size = New System.Drawing.Size(451, 25)
        Me.LabelStrip.TabIndex = 2
        Me.LabelStrip.Text = "LabelStrip"
        '
        'GridPanel
        '
        Me.GridPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridPanel.Location = New System.Drawing.Point(12, 28)
        Me.GridPanel.Name = "GridPanel"
        Me.GridPanel.Size = New System.Drawing.Size(427, 254)
        Me.GridPanel.TabIndex = 3
        '
        'HynrObjectPickerDialog
        '
        Me.AcceptButton = Me.ButtonOK
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(451, 323)
        Me.ControlBox = False
        Me.Controls.Add(Me.GridPanel)
        Me.Controls.Add(Me.LabelStrip)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "HynrObjectPickerDialog"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)
        Me.PerformLayout()

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
