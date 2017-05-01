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
    Implements IListViewModel(Of viewmodelitem)

#Region "Commands"
    <Browsable(False)>
    Public Property CreateCommand As ICommand = New Command(AddressOf CreateNewItem)
    <Browsable(False)>
    Public Property UpdateAllCommand As ICommand = New Command(AddressOf UpdateAll)
    <Browsable(False)>
    Public Property DeleteSelectedItemCommand As ICommand = New Command(AddressOf DeleteSelectedItem)
    <Browsable(False)>
    Public Property DeleteSelectedItemsCommand As ICommand = New Command(AddressOf DeleteSelectedItems)
    <Browsable(False)>
    Public Property OpenNewFormCommand As ICommand = New Command(AddressOf OpenNewForm)
    <Browsable(False)>
    Public Property ApplyFilterCommand As ICommand = New Command(AddressOf ApplyFilter)
    <Browsable(False)>
    Public Property CancelLoadCommand As ICommand = New Command(AddressOf CancelLoading)
    <Browsable(False)>
    Public Property RefreshAllCommand As ICommand = New Command(AddressOf GetData)
#End Region

#Region "Properties"
    Protected Property _DataController As datacontrollerclass
    Protected ReadOnly Property _WindowFactory As IWindowFactory
    Private _OriginalItemList As New ObservableListSource(Of viewmodelitem)
    Private _ItemList As New ObservableListSource(Of viewmodelitem)
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
            _SelectedItem = value
            RaiseEvent SelectedItemChanged()
            OnPropertyChanged("SelectedItem")
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
#End Region

#Region "Constructor"
    Public Sub New()
        _DataController = GetInstance(GetType(datacontrollerclass))
    End Sub
    Public Sub New(ByRef windowfactory As IWindowFactory)
        _WindowFactory = windowfactory
    End Sub
    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef windowfactory As IWindowFactory = Nothing)
        _WindowFactory = windowfactory
        _DataController = datacontroller
    End Sub
#End Region

#Region "Methods"
    Protected Overridable Sub OpenNewForm()
        If (Not IsNothing(_WindowFactory) And Not IsNothing(SelectedItem)) Then _WindowFactory.OpenNewForm(SelectedItem)
    End Sub
    Private Sub ToggleCanSave()
        CanSave = (From c In _OriginalItemList Where c.CanSave = True).Any
    End Sub
#End Region

#Region "Filter"
    ''' <summary>
    ''' add bound properties to inherited listviewmodelclass for every filter parameter with ListViewModelFilterAttribute
    ''' </summary>
    Protected Async Sub ApplyFilter()
        Dim filterparameters As String = GenerateFilterParameters(Me)
        If Not filterparameters = "" Then
            Dim newlist As New ObservableListSource(Of viewmodelitem)
            'IsBusy = True
            Dim filteredlist = Await Task.Run(Function() _OriginalItemList.ToList.Where(filterparameters).ToList)
            For Each item In filteredlist
                newlist.Add(item)
            Next
            ItemList = newlist
            'IsBusy = False
        Else
            ItemList = _OriginalItemList
        End If
    End Sub
#End Region

#Region "Crud"
    ''' <summary>
    ''' override in order to use the specific data context of EntityItem to create a filled instance of DataClass and then use the datacontrollers CreateNewItem (Of DataClass)
    ''' </summary>
    Public Overridable Sub CreateNewItem()
    End Sub
    Private Sub UpdateAll()
        If Not IsBusy Then
            For Each item As viewmodelitem In _OriginalItemList
                UpdateItem(item, Nothing)
            Next
            ToggleCanSave()
        End If
    End Sub
    Protected Overridable Sub UpdateItem(sender As Object, e As EventArgs)
        If Not IsBusy Then
            Dim vmitem As viewmodelitem = sender
            If vmitem.CanSave Then
                Dim data = _DataController.UpdateItem(vmitem.Data)
                If Not IsNothing(data) Then vmitem.Data = data
            End If
        End If
    End Sub
    Protected Overridable Sub DeleteItem(sender As Object, e As EventArgs)
        If Not IsBusy Then
            Dim vmitem As viewmodelitem = sender
            If _DataController.DeleteItem(vmitem.Data) = True Then
                ItemList.Remove(vmitem)
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
    Public Async Sub GetData()
        IsBusy = True
        Dim dataitemlist As IEnumerable(Of dataitem) = Nothing
        CancellationSource = New Threading.CancellationTokenSource
        Try
            dataitemlist = Await Task.Run(Function() _DataController.GetAllItems(), CancellationSource.Token)
        Catch ex As Exception
            _DataController.DataContext.AddError(ex, "ListViewModel GetData")
        End Try
        If IsNothing(dataitemlist) Then dataitemlist = New List(Of dataitem)
        DataToList(dataitemlist)
        ToggleCanSave()
        IsBusy = False
        RaiseLoadingCompleted()
    End Sub
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
        AddHandler newvmitem.DoubleClicked, AddressOf OpenNewForm
        AddHandler newvmitem.CanSaveChanged, AddressOf ToggleCanSave
        Return newvmitem
    End Function
    Private Sub CancelLoading() 'not working
        If Not IsNothing(CancellationSource) Then
            CancellationSource.Cancel()
        End If
    End Sub
#End Region

#Region "Events"
    Public Event SelectedItemChanged() Implements IListViewModel(Of viewmodelitem).SelectedItemChanged
#End Region
End Class