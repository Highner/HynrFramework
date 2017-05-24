Imports System.ComponentModel
Imports HynrFramework

Public Class HynrSplitContainer
    Inherits System.Windows.Forms.SplitContainer
    Implements IHasHynrSettings
    Implements INotifyPropertyChanged

    Private BusyIndicator As New MatrixCircularProgressControl
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
    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            'ToggleBusyIndicator(_IsBusy)
            OnPropertyChanged("IsBusy")
        End Set
    End Property

    Public Sub New()
        'BusyIndicator.Height = 50
        'BusyIndicator.Width = 50
        'Controls.Add(BusyIndicator)
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



    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        HynrSettings.ApplyToControlContainer(Me)
        Me.BackColor = HynrSettings.GridBackcolor
    End Sub

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
