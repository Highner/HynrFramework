Imports System.Windows.Forms
Imports System.ComponentModel

Public Class HynrCardContainer(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem), card As HynrCard(Of dataitem))
    Inherits FlowLayoutPanel
    Implements IBindableListControl(Of dataitem, viewmodelitem)
    Implements INotifyPropertyChanged
    Implements IHasHynrSettings

#Region "Properties"
    Private Property _SelectedItems As New List(Of viewmodelitem)
    Property SelectedItems As List(Of viewmodelitem)
        Get
            Return _SelectedItems
        End Get
        Set(value As List(Of viewmodelitem))
            _SelectedItems = value
            OnPropertyChanged("SelectedItems")
        End Set
    End Property
    Private WithEvents _BindingSource As New BindingSource
    Public Property BindingSourceDataSource As ObservableListSource(Of viewmodelitem)
        Get
            Return _BindingSource.DataSource
        End Get
        Set(value As ObservableListSource(Of viewmodelitem))
            If Not IsNothing(value) Then
                _BindingSource.DataSource = value
            End If
        End Set
    End Property
    Private Property _SelectedItem As viewmodelitem
    Public Property SelectedItem As viewmodelitem Implements IBindableListControl(Of dataitem, viewmodelitem).SelectedItem
        Get
            Return _SelectedItem
        End Get
        Set(value As viewmodelitem)
            If _BindingSource.Count > 0 Then
                If Not IsNothing(value) Then
                    If Not value.Equals(_SelectedItem) Then
                        _BindingSource.Position = _BindingSource.IndexOf(value)
                        _SelectedItem = value
                        OnPropertyChanged("SelectedItem")
                    End If
                End If
            End If
        End Set
    End Property
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
    Public Property IsBusy() As Boolean Implements IBindableListControl(Of dataitem, viewmodelitem).IsBusy
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            ToggleBusyIndicator(_IsBusy)
            OnPropertyChanged("IsBusy")
        End Set
    End Property

    Property FireItemDoubleClick As Boolean = False
    Property CancellationSource As Threading.CancellationTokenSource Implements IBindableListControl(Of dataitem, viewmodelitem).CancellationSource
    Private BusyIndicator As New MatrixCircularProgressControl
    Private LazyBindingViewModel As IViewModelBase
#End Region

#Region "Methods"
    Public Sub New()
        AddHandler _BindingSource.CurrentItemChanged, AddressOf SelectedItemChanged
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        Controls.Add(BusyIndicator)
    End Sub
    Private Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        BorderStyle = HynrSettings.GridBorderStyle
        BackColor = HynrSettings.GridBackcolor
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub SelectedItemChanged()
        SelectedItem = _BindingSource.Current
    End Sub
    Private Sub ItemDoubleClicked(ByRef item As viewmodelitem)
        SelectedItem = item
        If Not IsNothing(SelectedItem) Then
            SelectedItem.DoubleClickCommand.Execute(Nothing)
        End If
        RaiseEvent ItemDoubleClick(SelectedItem)
    End Sub

    Private Sub SelectItem(ByRef item As viewmodelitem)
        SelectedItem = item
        If Not IsNothing(SelectedItem) Then
            SelectedItem.ClickCommand.Execute(Nothing)
        End If
        RaiseEvent ItemClick(SelectedItem)
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
    Private Function CountItems() As Integer
        Return (From c In Me.Controls Where c.GetType Is GetType(card)).Count
    End Function
#End Region

#Region "Binding"
    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase) ' IListViewModel(Of viewmodelitem))
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
    Private Sub DataSourceChanged() Handles _BindingSource.ListChanged, _BindingSource.DataSourceChanged
        If Not CountItems() = _BindingSource.Count Then
            Controls.Clear()
            For Each item In BindingSourceDataSource
                Dim newitem As card = DirectCast(Activator.CreateInstance(GetType(card), item), card)
                AddHandler newitem.ItemClick, AddressOf SelectItem
                AddHandler newitem.ItemDoubleClick, AddressOf ItemDoubleClicked
                Controls.Add(newitem)
            Next
            AdjustCardWidth()
        End If
    End Sub
    Private Sub AdjustCardWidth() Handles Me.SizeChanged
        For Each cont In Me.Controls
            If TypeOf (cont) Is card Then
                Dim card As card = cont
                card.Width = ClientSize.Width - card.Margin.Left - card.Margin.Right
            End If
        Next
    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ItemDoubleClick(ByRef item As viewmodelitem)
    Public Event ItemClick(ByRef item As viewmodelitem)
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
#End Region

End Class
