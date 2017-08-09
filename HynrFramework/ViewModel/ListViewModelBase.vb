Imports System.Data.Entity
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Input
Imports System.Linq.Dynamic
Imports System.ComponentModel

''' <summary>
''' only contructor and CreateNewItem need to be specified in inherited class
''' </summary>
Public MustInherit Class ListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ViewModelBase
    Implements IListViewModel(Of viewmodelitem), ICrudObject

#Region "Commands"
    <Browsable(False)>
    Public Property CreateCommand As ICommand = New Command(AddressOf ExecuteCreateNewItem)
    <Browsable(False)>
    Public Property UpdateAllCommand As ICommand = New Command(AddressOf UpdateAll) Implements ICrudObject.UpdateAllCommand
    <Browsable(False)>
    Public Property DeleteSelectedItemCommand As ICommand = New Command(AddressOf ExecuteDeleteSelectedItem)
    <Browsable(False)>
    Public Property DeleteSelectedItemsCommand As ICommand = New Command(AddressOf ExecuteDeleteSelectedItems) Implements ICrudObject.DeleteSelectedItemsCommand
    <Browsable(False)>
    Public Property OpenNewFormCommand As ICommand = New Command(AddressOf ExecuteOpenNewForm) Implements ICrudObject.OpenNewFormCommand
    <Browsable(False)>
    Public Property OpenEditFormCommand As ICommand = New Command(AddressOf ExecuteOpenEditForm) Implements ICrudObject.OpenEditFormCommand
    <Browsable(False)>
    Public Property ApplyFilterCommand As ICommand = New Command(AddressOf ApplyFilter)
    <Browsable(False)>
    Public Property CancelLoadCommand As ICommand = New Command(AddressOf CancelLoading)
    <Browsable(False)>
    Public Property RefreshAllAsyncCommand As ICommand = New Command(AddressOf GetDataAsync) Implements ICrudObject.RefreshAllAsyncCommand
    <Browsable(False)>
    Public Property ClearListCommand As ICommand = New Command(AddressOf ClearList)
#End Region

#Region "Properties"
    Protected Property _DataController As datacontrollerclass
    Protected Property _WindowFactory As IListViewWindowFactory(Of dataitem)
    Private WithEvents _Timer As Timer
    Private WithEvents _OriginalItemList As New ObservableListSource(Of viewmodelitem)
    Private WithEvents _ItemList As New ObservableListSource(Of viewmodelitem)
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property ItemList() As ObservableListSource(Of viewmodelitem)
        Get
            Return _ItemList
        End Get
        Set(ByVal value As ObservableListSource(Of viewmodelitem))
            _ItemList = value
            If _ItemList.Count = 0 Then
                RaiseEvent SelectedItemChanged()
            End If
            OnPropertyChanged("ItemList")
        End Set
    End Property
    Private _SelectedItem As viewmodelitem
    Public Property SelectedItem() As viewmodelitem Implements IListViewModel(Of viewmodelitem).SelectedItem
        Get
            If ItemList.Any Then
                Return _SelectedItem
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As viewmodelitem)
            If Not value.Equals(_SelectedItem) Then
                If Not IsNothing(_SelectedItem) Then _SelectedItem.IsSelected = False
                _SelectedItem = value
                value.IsSelected = True
                RaiseEvent SelectedItemChanged()
                OnPropertyChanged("SelectedItem")
                OnPropertyChanged("SelectedItemID")
            End If
        End Set
    End Property
    Public ReadOnly Property SelectedItemID As Integer Implements IListViewModel(Of viewmodelitem).SelectedItemID
        Get
            If ItemList.Any Then
                Return SelectedItem.ID
            Else
                Return 0
            End If
        End Get
    End Property
    Public Property SelectedItems As New List(Of viewmodelitem) Implements IListViewModel(Of viewmodelitem).SelectedItems
    Private _CanSave As Boolean
    Public Property CanSave As Boolean Implements IListViewModel(Of viewmodelitem).CanSave
        Get
            Return _CanSave
        End Get
        Set(value As Boolean)
            _CanSave = value
            OnPropertyChanged("CanSave")
        End Set
    End Property
    Private _ListCount As Boolean
    Public Property ListCount() As Boolean
        Get
            Return _ListCount
        End Get
        Set(ByVal value As Boolean)
            _ListCount = value
            OnPropertyChanged("ListCount")
        End Set
    End Property
    Private _FilteredListCount As Boolean
    Public Property FilteredListCount() As Boolean
        Get
            Return _FilteredListCount
        End Get
        Set(ByVal value As Boolean)
            _FilteredListCount = value
            OnPropertyChanged("FilteredListCount")
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        _DataController = GetInstance(GetType(datacontrollerclass))
    End Sub
    Public Sub New(ByRef windowfactory As IListViewWindowFactory(Of dataitem))
        _WindowFactory = windowfactory
    End Sub
    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef windowfactory As IListViewWindowFactory(Of dataitem) = Nothing)
        _WindowFactory = windowfactory
        _DataController = datacontroller
    End Sub
#End Region

#Region "Methods"
    Private Sub ToggleCanSave()
        CanSave = (From c In _OriginalItemList Where c.CanSave = True).Any
    End Sub
    Private Sub AddItemToList(ByRef viewmodelitem As viewmodelitem)
        _OriginalItemList.Insert(0, viewmodelitem)
        ApplyFilter()
    End Sub
    Private Sub RemoveItemFromList(ByVal viewmodelitem As viewmodelitem)
        Dim item = (From i In _OriginalItemList Where i.ID = viewmodelitem.ID Select i).FirstOrDefault
        If Not IsNothing(item) Then
            _OriginalItemList.Remove(item)
            ApplyFilter()
        End If
    End Sub
    Protected Sub ReplaceItemInList(ByVal viewmodelitem As viewmodelitem)
        Dim item = (From i In _OriginalItemList Where i.ID = viewmodelitem.ID Select i).FirstOrDefault
        If Not IsNothing(item) Then
            Dim index = _OriginalItemList.IndexOf(item)
            _OriginalItemList.Remove(item)
            _OriginalItemList.Insert(index, viewmodelitem)
            ApplyFilter()
        End If
    End Sub
    Public Sub SetTimer(ByVal interval As Integer)
        If IsNothing(_Timer) Then
            _Timer = New Timer
        End If
        StopTimer()
        _Timer.Interval = interval
        StartTimer()
    End Sub
    Public Sub StopTimer()
        _Timer.Stop()
    End Sub
    Public Sub StartTimer()
        _Timer.Start()
    End Sub
    Private Sub TimerTick() Handles _Timer.Tick
        If Not IsBusy Then RefreshAllCommand.Execute(Nothing)
    End Sub
    Protected Sub DataItemChanged(ByRef dataitem As IHasID)
        Dim id As Integer = dataitem.ID
        Dim changedlistitem = (From i In ItemList Where i.ID = id Select i).Single
        changedlistitem.Data = dataitem
        changedlistitem.AllPropertiesChanged()
    End Sub
#End Region

#Region "Filter"
    ''' <summary>
    ''' add bound properties with ListViewModelFilterAttribute to inherited listviewmodelclass for every filter parameter
    ''' </summary>
    Protected Async Sub ApplyFilter()
        If Not IsNothing(CancellationSource) Then CancellationSource.Cancel()
        CancellationSource = New Threading.CancellationTokenSource
        Dim filterparameters As String = GenerateFilterParameters(Me)
        If Not filterparameters = "" Then
            Dim newlist As New ObservableListSource(Of viewmodelitem)
            Dim filteredlist = Await Task.Run(Function() _OriginalItemList.ToList.Where(filterparameters).ToList, CancellationSource.Token)
            For Each item In filteredlist
                newlist.Add(item)
            Next
            ItemList = newlist
        Else
            ItemList = _OriginalItemList
        End If
    End Sub
#End Region

#Region "Crud"
    ''' <summary>
    ''' override in order to use the specific data context of EntityItem to create a filled instance of DataClass and then use the datacontrollers CreateNewItem (Of DataClass).
    ''' </summary>
    Public Overridable Function CreateNewItem() As dataitem
    End Function
    Protected Overridable Function OpenNewForm() As dataitem
        Dim newdataitem As dataitem = CreateNewItem()
        If Not IsNothing(_WindowFactory) Then
            If Not IsNothing(newdataitem) Then Return _WindowFactory.OpenNewForm(newdataitem)
        End If
        Return newdataitem
    End Function
    Protected Overridable Function OpenEditForm() As dataitem
        If Not IsNothing(_WindowFactory) Then
            If Not IsNothing(SelectedItem) Then
                Return _WindowFactory.OpenEditForm(SelectedItem.Data)
            End If
        End If
        Return Nothing
    End Function
    Private Sub ExecuteOpenNewForm()
        IsBusy = True
        Dim dataitem As dataitem = OpenNewForm()
        If Not IsNothing(dataitem) Then
            _DataController.CreateNewItem(dataitem)
            Dim item = DataToItem(dataitem)
            AddItemToList(item)
        End If
        IsBusy = False
    End Sub
    Private Sub ExecuteOpenEditForm()
        Dim dataitem As dataitem = OpenEditForm()
        If Not IsNothing(dataitem) Then
            _DataController.UpdateItem(dataitem)
            DataItemChanged(dataitem)
        End If
    End Sub
    Private Sub ExecuteCreateNewItem()
        Dim item = CreateNewItem()
        If Not IsNothing(item) Then
            _DataController.CreateNewItem(item)
            RaiseEvent CreateCommandExecuted(DataToItem(item))
        End If
    End Sub
    Private Sub UpdateAll()
        If Not IsBusy Then
            For Each item As viewmodelitem In _OriginalItemList
                UpdateItem(item, Nothing)
            Next
            ToggleCanSave()
        End If
        RaiseEvent UpdateAllCommandExecuted()
    End Sub
    ''' <summary>
    ''' this one will (also) be fired after EditWindow is closed. override in order to save other stuff than just properties to db table (i.e. relationships to other tables)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Overridable Sub UpdateItem(sender As Object, e As EventArgs)
        Dim vmitem As viewmodelitem = sender
        If vmitem.CanSave Then
            Dim data = _DataController.UpdateItem(vmitem.Data)
            If Not IsNothing(data) Then
                vmitem.Data = data
                RaiseEvent UpdateItemCommandExecuted(vmitem)
            End If
        End If
    End Sub
    Private Function GetDeleteConfirmation() As Boolean
        If Not IsNothing(_WindowFactory) Then
            If Not IsNothing(SelectedItem) Then
                Return _WindowFactory.DeleteConfirmationForm(SelectedItem.Data)
            End If
        End If
        Return True
    End Function
    Private Sub ExecuteDeleteSelectedItem()
        If GetDeleteConfirmation() Then DeleteSelectedItem()
    End Sub
    Private Sub ExecuteDeleteSelectedItems()
        If GetDeleteConfirmation() Then
            DeleteSelectedItems()
            RaiseEvent DeleteSelectedItemsCommandExecuted()
        End If
    End Sub
    Protected Overridable Sub DeleteItem(sender As Object, e As EventArgs)
        If Not IsBusy Then
            Dim vmitem As viewmodelitem = sender
            If _DataController.DeleteItem(vmitem.Data) = True Then
                RemoveItemFromList(vmitem)
                RaiseEvent DeleteSelectedItemCommandExecuted(vmitem)
            End If
        End If
    End Sub
    Protected Overridable Sub DeleteSelectedItem()
        If Not IsBusy Then
            If Not IsNothing(_SelectedItem) Then
                DeleteItem(_SelectedItem, Nothing)
            End If
        End If
    End Sub
    Protected Overridable Sub DeleteSelectedItems()
        If Not IsBusy Then
            For Each item In SelectedItems
                DeleteItem(item, Nothing)
            Next
        End If
    End Sub
    Private Sub ClearList()
        ItemList.Clear()
        SelectedItem = Nothing
        SelectedItems = Nothing
        OnPropertyChanged("SelectedItems")
    End Sub
    Protected Overrides Sub GetData()
        GetDataAsync()
    End Sub
    Private Async Sub GetDataAsync()
        IsBusy = True
        CancelLoading()
        ItemList.Clear()
        Dim dataitemlist As IEnumerable(Of dataitem) = Nothing
        CancellationSource = New Threading.CancellationTokenSource
        Try
            dataitemlist = Await Task.Run(Function() GetItems(), CancellationSource.Token)
        Catch ex As Exception
            _DataController.DataContext.AddError(ex, "ListViewModel GetData")
        End Try
        If IsNothing(dataitemlist) Then dataitemlist = New List(Of dataitem)
        DataToList(dataitemlist)
        ToggleCanSave()
        IsBusy = False
        RaiseLoadingCompleted()
    End Sub
    ''' <summary>
    ''' override this in order to use different "get functions" from the datacontroller
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Function GetItems() As IEnumerable(Of dataitem)
        Return _DataController.GetAllItems()
    End Function
    Private Sub DataToList(ByRef dataitemlist As IEnumerable(Of dataitem))
        Dim selectedindex As Integer = ItemList.IndexOf(SelectedItem)
        Dim list = New ObservableListSource(Of viewmodelitem)
        For Each dataitem In dataitemlist
            list.Add(DataToItem(dataitem))
        Next
        _OriginalItemList = list
        ApplyFilter()
        'TODO: select correct item after loading
        'If selectedindex >= 0 Then
        '    If (ItemList.Count >= selectedindex - 1) Then SelectedItem = ItemList(selectedindex) Else SelectedItem = ItemList(0)
        'End If
        If ItemList.Any Then SelectedItem = ItemList(0)
    End Sub
    ''' <summary>
    ''' Override this in order to add additional event handlers to the item
    ''' </summary>
    ''' <param name="dataitem"></param>
    ''' <returns></returns>
    Public Overridable Function DataToItem(ByRef dataitem As dataitem) As viewmodelitem
        Dim newvmitem As viewmodelitem = GetInstance(GetType(viewmodelitem))
        newvmitem.Data = dataitem
        newvmitem.CancellationSource = CancellationSource
        AddHandler newvmitem.Deleted, AddressOf DeleteItem
        AddHandler newvmitem.Updated, AddressOf UpdateItem
        AddHandler newvmitem.DoubleClicked, AddressOf ExecuteOpenEditForm
        AddHandler newvmitem.CanSaveChanged, AddressOf ToggleCanSave
        Return newvmitem
    End Function
    Private Sub CancelLoading() 'not working
        If Not IsNothing(CancellationSource) Then
            CancellationSource.Cancel()
        End If
    End Sub
    Private Sub ListChanged() Handles _OriginalItemList.CollectionChanged
        ListCount = _OriginalItemList.Count
    End Sub
    Private Sub FilteredListChanged() Handles _ItemList.CollectionChanged
        FilteredListCount = ItemList.Count
    End Sub
#End Region

#Region "Events"
    Public Event SelectedItemChanged() Implements IListViewModel(Of viewmodelitem).SelectedItemChanged
    Public Event ItemListChanged() Implements IListViewModel(Of viewmodelitem).ItemListChanged
    Public Event CreateCommandExecuted(ByVal item As viewmodelitem) Implements IListViewModel(Of viewmodelitem).CreateCommandExecuted
    Public Event UpdateAllCommandExecuted() Implements IListViewModel(Of viewmodelitem).UpdateAllCommandExecuted
    Public Event UpdateItemCommandExecuted(ByVal item As viewmodelitem) Implements IListViewModel(Of viewmodelitem).UpdateItemCommandExecuted
    Public Event DeleteSelectedItemCommandExecuted(ByVal item As viewmodelitem) Implements IListViewModel(Of viewmodelitem).DeleteSelectedItemCommandExecuted
    Public Event DeleteSelectedItemsCommandExecuted() Implements IListViewModel(Of viewmodelitem).DeleteSelectedItemsCommandExecuted
#End Region
End Class