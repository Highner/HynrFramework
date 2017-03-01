Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Windows.Forms

Public Class HynrGrid(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits DataGridView
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
    Private Property _BindingSource As New BindingSource
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
                _SelectedItem = value
                _BindingSource.Position = _BindingSource.IndexOf(value)
                OnPropertyChanged("SelectedItem")
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
    Property CancellationSource As Threading.CancellationTokenSource Implements IBindableListControl(Of dataitem, viewmodelitem).CancellationSource
    Private BusyIndicator As New MatrixCircularProgressControl
    Private LazyBindingViewModel As IViewModelBase
    Private LazyBindingDataMember As String
#End Region

#Region "Methods"
    Public Sub New()
        DataSource = _BindingSource
        AddHandler _BindingSource.CurrentItemChanged, AddressOf SelectedItemChanged
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        Controls.Add(BusyIndicator)
    End Sub
    Private Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        DefaultCellStyle.SelectionBackColor = HynrSettings.SelectedBackColor
        DefaultCellStyle.SelectionForeColor = HynrSettings.SelectedForecolor
        GridColor = HynrSettings.GridColor
        BackgroundColor = HynrSettings.GridBackcolor
        RowHeadersVisible = HynrSettings.RowHeadersVisible
        BorderStyle = HynrSettings.GridBorderStyle
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub SelectedItemChanged()
        SelectedItem = _BindingSource.Current
    End Sub
    Private Sub ItemDoubleClicked() Handles Me.MouseDoubleClick
        RaiseEvent ItemDoubleClick(SelectedItem)
    End Sub
    Private Sub SelectedItemsChanged() Handles Me.SelectionChanged
        Dim list As New List(Of viewmodelitem)
        For Each row As DataGridViewRow In Me.SelectedRows
            list.Add(row.DataBoundItem)
        Next
        SelectedItems = list
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

#Region "Binding"
    Public Sub BindToListViewModel(ByRef listviewmodel As IListViewModel(Of viewmodelitem))
        DataBindings.Clear()
        LazyBindingViewModel = listviewmodel
        CompleteBinding(String.Empty)
    End Sub
    Public Sub BindToListViewModel(ByRef viewmodel As IViewModelBase, ByVal datamemberlistviewmodel As String)
        LazyBindingDataMember = datamemberlistviewmodel
        LazyBindingViewModel = viewmodel
        AddHandler viewmodel.LoadingCompleted, AddressOf OnCompleteBinding
    End Sub
    Private Sub OnCompleteBinding()
        CompleteBinding(LazyBindingDataMember & ".")
    End Sub
    Private Sub CompleteBinding(ByVal datamember As String)
        DataBindings.Clear()
        DataBindings.Add("BindingSourceDataSource", LazyBindingViewModel, datamember & "ItemList", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItem", LazyBindingViewModel, datamember & "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItems", LazyBindingViewModel, datamember & "SelectedItems", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("IsBusy", LazyBindingViewModel, datamember & "IsBusy", True, DataSourceUpdateMode.Never, True)
        DataBindings.Add("CancellationSource", LazyBindingViewModel, datamember & "CancellationSource", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
    End Sub
    Public Sub BindGridCombobox(ByRef columnname As String, ByRef datasource As Object, ByVal datapropertyname As String, ByVal valuemember As String, ByVal displaymember As String)
        Dim col As DataGridViewComboBoxColumn = Columns(columnname)
        col.DataPropertyName = datapropertyname
        col.ValueMember = valuemember
        col.DisplayMember = displaymember
        col.DataSource = New BindingSource(datasource, "")
    End Sub
    Private Sub OnBindingComplete() Handles Me.DataBindingComplete
        If Me.ColumnCount = 0 Then Me.AutoGenerateColumns = True
    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ItemDoubleClick(ByRef item As viewmodelitem)
#End Region
End Class
