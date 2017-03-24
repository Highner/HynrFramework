Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Windows.Forms
Imports HynrFramework

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
                If Not IsNothing(value) Then
                    _BindingSource.Position = _BindingSource.IndexOf(value)
                    _SelectedItem = value
                    OnPropertyChanged("SelectedItem")
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
        If Not IsNothing(SelectedItem) Then
            SelectedItem.DoubleClickCommand.Execute(Nothing)
        End If
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
    Public Sub BindGridCombobox(ByRef columnname As String, ByRef datasource As Object, ByVal datapropertyname As String, ByVal valuemember As String, ByVal displaymember As String)
        Dim col As DataGridViewComboBoxColumn = Columns(columnname)
        col.DataPropertyName = datapropertyname
        col.ValueMember = valuemember
        col.DisplayMember = displaymember
        col.DataSource = New BindingSource(datasource, String.Empty)
    End Sub
    Private Sub OnBindingComplete() Handles Me.DataBindingComplete
        If ColumnCount = 0 Then AutoGenerateColumns = True
    End Sub
    'Private Sub ColumnAdd(sender As Object, e As DataGridViewColumnEventArgs) Handles Me.ColumnAdded
    '    Dim coltype As Type = e.Column.ValueType.GetType
    '    If ReferenceEquals(coltype, GetType(Decimal)) Then
    '        e.Column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
    '    End If
    'End Sub

#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ItemDoubleClick(ByRef item As viewmodelitem)
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
#End Region
End Class
