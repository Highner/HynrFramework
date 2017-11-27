Imports System.Windows.Forms
Imports HynrFramework

Public Class HynrForm
    Inherits Form
    Implements IHasHynrSettings


    Private _HynrSettings As HynrUISettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            If Not IsNothing(value) Then
                _HynrSettings = value
                ApplyHynrSettings()
            End If
        End Set
    End Property

    Public Overridable Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        '  BackColor = HynrSettings.SelectedBackColor
    End Sub

    Public Sub New()
    End Sub
    Public Sub New(ByRef hynrsettigns As HynrUISettings)
        HynrSettings = hynrsettigns
    End Sub

    Private Sub FirstLoadToApplyHynrSettings(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not IsNothing(HynrSettings) Then HynrSettings.ApplyToControlContainer(Me)
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'HynrForm
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Name = "HynrForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub
End Class
