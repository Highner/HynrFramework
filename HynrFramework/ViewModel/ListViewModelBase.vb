Imports System.Data.Entity
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Input
Imports System.Linq.Dynamic
Imports System.ComponentModel
Imports System.Text
Imports HynrFramework
Imports System.Data.SqlClient

''' <summary>
''' only CreateNewItem needs to be specified in inherited class
''' </summary>
Public Class ListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
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
    Public Overrides Property RefreshAllAsyncCommand As ICommand = New Command(AddressOf GetDataAsync) Implements ICrudObject.RefreshAllAsyncCommand
    <Browsable(False)>
    Public Property ClearListCommand As ICommand = New Command(AddressOf ClearList)
#End Region

#Region "Properties"
    Public Property _DataController As datacontrollerclass
    Protected WithEvents _WindowFactory As IListViewWindowFactory(Of dataitem)
    Protected WithEvents _OriginalItemList As New ObservableListSource(Of viewmodelitem)
    Private WithEvents _ItemList As New ObservableListSource(Of viewmodelitem)
    Private WithEvents _AutoRefreshWrapper As AutoRefreshWrapper(Of entityitme)
    Protected _Refresh As Boolean = True
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
            If Not IsNothing(value) AndAlso Not value.Equals(_SelectedItem) Then
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
    Public ReadOnly Property HasSelectedItem As Boolean
        Get
            Return Not IsNothing(SelectedItem)
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
    Protected Overrides Sub Finalize()
        CancelLoading()
    End Sub
#End Region

#Region "Methods"
    Public Sub SetItemList(list As IEnumerable(Of viewmodelitem))
        _OriginalItemList = list
        ApplyFilter()
        RaiseLoadingCompleted()
    End Sub
    Public Sub AddToItemList(list As IEnumerable(Of viewmodelitem))
        For Each item In list
            _OriginalItemList.Add(item)
        Next
        ApplyFilter()
        RaiseLoadingCompleted()
    End Sub
    Private Sub ToggleCanSave()
        CanSave = (From c In _OriginalItemList Where c.CanSave = True).Any
    End Sub
    Public Sub AddItemToList(ByRef viewmodelitem As viewmodelitem)
        _OriginalItemList.Insert(0, viewmodelitem)
        ApplyFilter()
        RaiseEvent ItemAdded(viewmodelitem)
    End Sub
    Private Sub RemoveItemFromList(ByVal viewmodelitem As viewmodelitem)
        RemoveItemFromList(viewmodelitem.Data.ID)
    End Sub
    Public Sub RemoveItemFromList(ByVal id As Object)
        Dim item = (From i In _OriginalItemList Where i.Data.ID = id Select i).FirstOrDefault
        If Not IsNothing(item) Then
            _OriginalItemList.Remove(item)
            ApplyFilter()
            RaiseEvent ItemDeleted()
        End If
    End Sub
    Protected Sub ReplaceItemInList(ByVal viewmodelitem As viewmodelitem)
        Dim item = (From i In _OriginalItemList Where i.Data.ID = viewmodelitem.Data.ID Select i).FirstOrDefault
        If Not IsNothing(item) Then
            Dim index = _OriginalItemList.IndexOf(item)
            _OriginalItemList.Remove(item)
            _OriginalItemList.Insert(index, viewmodelitem)
            ApplyFilter()
        End If
    End Sub
    Public Function GetItemFromList(id As Object) As viewmodelitem
        Dim item = (From i In _OriginalItemList Where i.Data.ID = id Select i).FirstOrDefault
        If Not IsNothing(item) Then
            Return item
        End If
        Return Nothing
    End Function
    Protected Sub DataItemChanged(ByVal dataitem As IHasID) Handles _WindowFactory.FormClosed
        Dim id As Integer = dataitem.ID
        Dim changedlistitem = (From i In ItemList Where i.ID = id Select i).Single
        changedlistitem.Data = _DataController.GetItem(id)
        changedlistitem.AllPropertiesChanged()
        RaiseEvent ItemChanged()
    End Sub
    Public Sub RefreshItem(item As viewmodelitem)
        DataItemChanged(item.Data)
    End Sub
    Public Sub RaiseFileDropped(item As Object, data As Object) Implements IListViewModel(Of viewmodelitem).RaiseFileDropped
        RaiseEvent FileDropped(item, data)
    End Sub
    Public Sub RaiseItemAdded(item As Object)
        RaiseEvent ItemAdded(item)
    End Sub
    Public Sub RaiseItemClicked(item As Object, index As Integer) Implements IListViewModel(Of viewmodelitem).RaiseItemClicked
        RaiseEvent ItemClicked(item, index)
    End Sub
    Public Sub RaiseItemDoubleClicked(item As Object, index As Integer) Implements IListViewModel(Of viewmodelitem).RaiseItemDoubleClicked
        RaiseEvent ItemDoubleClicked(item, index)
    End Sub
    ''' <summary>
    ''' override in order to define action for certain columns clicked in datagridview
    ''' </summary>
    ''' <returns></returns>
    Protected Overridable Function AlternativeDoubleClickAction() As Boolean
        Return False
    End Function
#End Region

#Region "Filter"
    ''' <summary>
    ''' add bound properties with ListViewModelFilterAttribute to inherited listviewmodelclass for every filter parameter
    ''' </summary>
    Protected Async Sub ApplyFilter()
        Try
            If Not IsNothing(CancellationSource) Then CancellationSource.Cancel()
            CancellationSource = New Threading.CancellationTokenSource
            Dim filterparameters As String = GenerateFilterParameters(Me)
            If Not filterparameters = "" Then
                Dim newlist As New ObservableListSource(Of viewmodelitem)
                Dim filteredlist = Await Task.Run(Function() FilterFunction(_OriginalItemList.ToList, filterparameters), CancellationSource.Token)
                For Each item In filteredlist
                    newlist.Add(item)
                Next
                ItemList = newlist
            Else
                ItemList = _OriginalItemList
            End If
        Catch
        End Try
    End Sub
    ''' <summary>
    ''' override in case custom filter is required
    ''' </summary>
    ''' <param name="itemlist"></param>
    ''' <param name="filterparameters"></param>
    ''' <returns></returns>
    Protected Overridable Function FilterFunction(itemlist As IEnumerable(Of viewmodelitem), filterparameters As String) As List(Of viewmodelitem)
        Return itemlist.Where(filterparameters).ToList
    End Function

    Public Function GetUniqueItemsForFilter(ByVal propertyname As String) As String() Implements IListViewModel(Of viewmodelitem).GetUniqueItemsForFilter
        If _OriginalItemList.Any Then
            Dim ar As New List(Of String)
            Dim propinfo As PropertyInfo = GetType(viewmodelitem).GetProperty(propertyname)
            If Not IsNothing(propinfo) Then
                For Each item In _OriginalItemList.OrderBy(propertyname)
                    Dim val = propinfo.GetValue(item, Nothing)
                    If Not IsNothing(val) AndAlso Not ar.Contains(val) Then ar.Add(val)
                Next
                Return ar.Distinct.ToArray
            End If
        End If
        Return New String() {}
    End Function
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
            _Refresh = False
            dataitem = _DataController.CreateNewItem(dataitem)
            Dim item = DataToItem(dataitem)
            AddItemToList(item)
            _Refresh = True
        End If
        IsBusy = False
    End Sub
    Private Sub ExecuteOpenEditForm()
        If Not AlternativeDoubleClickAction() Then
            Dim dataitem As dataitem = OpenEditForm()
            If Not IsNothing(dataitem) Then
                _DataController.UpdateItem(dataitem)
                DataItemChanged(dataitem)
            End If
        End If
    End Sub
    Private Sub ExecuteCreateNewItem()
        Dim item = CreateNewItem()
        If Not IsNothing(item) Then
            _Refresh = False
            _DataController.CreateNewItem(item)
            RaiseEvent CreateCommandExecuted(DataToItem(item))
            _Refresh = True
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
            _Refresh = False
            Dim vmitem As viewmodelitem = sender
            If _DataController.DeleteItem(vmitem.Data) = True Then
                RemoveItemFromList(vmitem)
                RaiseEvent DeleteSelectedItemCommandExecuted(vmitem)
            End If
            _Refresh = True
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
    Protected Overridable Sub ClearListLight()
        ItemList.Clear()
    End Sub
    Protected Overrides Sub GetData()
        GetDataAsync()
    End Sub
    Private Async Sub GetDataAsync()
        DataLoaded = False
        IsBusy = True
        CancelLoading()
        ClearListLight()
        Dim dataitemlist As IEnumerable(Of dataitem) = Nothing
        CancellationSource = New Threading.CancellationTokenSource
        Try
            dataitemlist = Await Task.Run(Function() GetItems(), CancellationSource.Token)
        Catch ex As Exception
            Debug.Print(ex.Message)
            _DataController.DataContext.AddError(ex, "ListViewModel GetData")
        End Try
        _AutoRefreshWrapper = _DataController.AutoRefreshWrapper
        If IsNothing(dataitemlist) Then dataitemlist = New List(Of dataitem)
        DataToList(dataitemlist)
        ToggleCanSave()
        IsBusy = False
        DataLoaded = True
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
        If ItemList.Any Then
            SelectedItem = ItemList(0)
            SelectedItems.Add(SelectedItem)
        End If
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
    Private Sub CancelLoading() 'not working (i think)
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

#Region "Auto Refresh"
    Protected Overridable Sub OnCollectionChanged(e As SqlNotificationEventArgs) Handles _AutoRefreshWrapper.CollectionChanged
        If _Refresh Then RefreshAllCommand.Execute()
    End Sub
#End Region

#Region "Grid"
    Public Overridable Function CreateGrid(databound As Boolean) As HynrGrid(Of dataitem, viewmodelitem)
        Dim grid = New HynrGrid(Of dataitem, viewmodelitem)
        If databound Then grid.BindToListViewModel(Me)
        Return grid
    End Function
#End Region

#Region "Events"
    Public Event SelectedItemChanged() Implements IListViewModel(Of viewmodelitem).SelectedItemChanged
    Public Event ItemListChanged() Implements IListViewModel(Of viewmodelitem).ItemListChanged
    Public Event CreateCommandExecuted(ByVal item As viewmodelitem) Implements IListViewModel(Of viewmodelitem).CreateCommandExecuted
    Public Event UpdateAllCommandExecuted() Implements IListViewModel(Of viewmodelitem).UpdateAllCommandExecuted
    Public Event UpdateItemCommandExecuted(ByVal item As viewmodelitem) Implements IListViewModel(Of viewmodelitem).UpdateItemCommandExecuted
    Public Event DeleteSelectedItemCommandExecuted(ByVal item As viewmodelitem) Implements IListViewModel(Of viewmodelitem).DeleteSelectedItemCommandExecuted
    Public Event DeleteSelectedItemsCommandExecuted() Implements IListViewModel(Of viewmodelitem).DeleteSelectedItemsCommandExecuted
    Public Event ItemAdded(item As Object) Implements IListViewModelBase.ItemAdded
    Public Event ItemDeleted() Implements IListViewModelBase.ItemDeleted
    Public Event ItemChanged() Implements IListViewModelBase.ItemChanged
    Public Event FileDropped(item As Object, data As Object) Implements IListViewModel(Of viewmodelitem).FileDropped
    Public Event ItemClicked(item As viewmodelitem, index As Integer) Implements IListViewModel(Of viewmodelitem).ItemClicked
    Public Event ItemDoubleClicked(item As viewmodelitem, index As Integer) Implements IListViewModel(Of viewmodelitem).ItemDoubleClicked
#End Region
End Class