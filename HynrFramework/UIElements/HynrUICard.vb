Public Class HynrUICard
    Implements IHasHynrSettings

#Region "Properties"
    Private _HynrSettings As HynrUISettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            _HynrSettings = value
            ApplyHynrSettings()
        End Set
    End Property
    Public Property ToolStripVisible As Boolean
        Get
            Return ToolStrip.Visible
        End Get
        Set(value As Boolean)
            ToolStrip.Visible = value
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
    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            ToggleBusyIndicator(_IsBusy)
        End Set
    End Property
    Private _Control As System.Windows.Forms.Control
    Public Property Control() As System.Windows.Forms.Control
        Get
            Return _Control
        End Get
        Set(ByVal value As System.Windows.Forms.Control)
            _Control = value
            If IsNothing(value) Then
                If Controls.Contains(value) Then Controls.Remove(value)
            Else
                If Not Control.Contains(value) Then
                    value.Dock = System.Windows.Forms.DockStyle.Fill
                    Controls.Add(value)
                    value.BringToFront()
                End If
            End If
        End Set
    End Property
    Private BusyIndicator As New MatrixCircularProgressControl
    Public Property IsSubHeader() As Boolean
        Get
            Return LabelStrip.IsSubHeader
        End Get
        Set(ByVal value As Boolean)
            LabelStrip.IsSubHeader = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        InitializeComponent()
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        Controls.Add(BusyIndicator)
    End Sub

    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        LabelStrip.HynrSettings = HynrSettings
    End Sub

    Private Sub ToggleBusyIndicator(ByVal busy As Boolean)
        Enabled = Not busy
        If busy Then
            BusyIndicator.StartAngle = 30
            BusyIndicator.Show()
            BusyIndicator.Start()
        Else
            BusyIndicator.Stop()
            BusyIndicator.Hide()
        End If
    End Sub
    Private Sub AdjustBusyIndicatorLocation() Handles Me.SizeChanged
        Dim x As Integer = Me.Width / 2 - BusyIndicator.Width / 2
        Dim y As Integer = Me.Height / 2 - BusyIndicator.Height / 2
        BusyIndicator.Location = New Drawing.Point(x, y)
    End Sub
#End Region
End Class
