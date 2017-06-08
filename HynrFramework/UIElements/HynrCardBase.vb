Imports System.Windows.Forms
Imports HynrFramework

Public Class HynrCardBase
    Implements IHasHynrSettings

#Region "Properties"
    Private _OldHeight As Integer
    Private _Collapsed As Boolean
    Public Property Collapsed As Boolean
        Get
            Return _Collapsed
        End Get
        Set(value As Boolean)
            _Collapsed = value
        End Set
    End Property
    Public Property CollapseButtonVisible() As Boolean
        Get
            Return CollapseButton.Visible
        End Get
        Set(ByVal value As Boolean)
            CollapseButton.Visible = value
        End Set
    End Property

    Private _HynrSettings As IHasHynrSettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            _HynrSettings = value
            ApplyHynrSettings()
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        InitializeComponent()

    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        LabelStrip.HynrSettings = HynrSettings
    End Sub

    Private Sub ToggleCollapsed()
        If Collapsed Then
            Height = _OldHeight
            Collapsed = False
            CollapseButton.Text = "Collapse"
            CollapseButton.Image = My.Resources.chevron
        Else
            Dim remHeight As Integer = Height
            Height = LabelStrip.Height
            _OldHeight = remHeight
            Collapsed = True
            CollapseButton.Text = "Expand"
            CollapseButton.Image = My.Resources.chevron_expand
        End If
        RaiseEvent CollapsedChanged(Me)
    End Sub
    Private Sub CollapseButton_Click(sender As Object, e As EventArgs) Handles CollapseButton.Click
        ToggleCollapsed()
    End Sub
    Private Sub HeightChanged() Handles Me.SizeChanged
        If Not Collapsed Then _OldHeight = Height
    End Sub
    Public Sub SetWidth(ByVal width As Integer)
        Me.Width = width
    End Sub

#End Region

#Region "Events"
    Public Event CollapsedChanged(ByRef card As HynrCardBase)
#End Region
End Class
