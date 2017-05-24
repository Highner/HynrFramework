Imports System.ComponentModel
Imports System.Windows.Forms
Imports HynrFramework

Public Class HynrPanel
    Inherits System.Windows.Forms.Panel
    Implements INotifyPropertyChanged
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
    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            ToggleBusyIndicator(_IsBusy)
            OnPropertyChanged("IsBusy")
        End Set
    End Property
    Private BusyIndicator As New MatrixCircularProgressControl
    Private LazyBindingViewModel As IViewModelBase



    Public Sub New()
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        ' Controls.Add(BusyIndicator)
    End Sub

    Private Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings

        BorderStyle = HynrSettings.GridBorderStyle
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub ToggleBusyIndicator(ByVal busy As Boolean)
        Enabled = Not busy
        If busy Then
            Controls.Clear()
            Controls.Add(BusyIndicator)
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

#Region "Binding"
    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase)
        LazyBindingViewModel = listviewmodel
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        AddHandler listviewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        DataBindings.Add("BindingSourceDataSource", LazyBindingViewModel, "ItemList", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItem", LazyBindingViewModel, "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItems", LazyBindingViewModel, "SelectedItems", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
    End Sub
#End Region


    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


End Class
