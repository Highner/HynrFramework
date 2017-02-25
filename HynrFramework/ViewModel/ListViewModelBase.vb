Imports System.Data.Entity
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Input
Imports System.Linq.Dynamic

'only contructor and CreateNewItem and possibly _Parameters need to be specified in inherited class
Public Class ListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem, dbcontextclass), viewmodelitem As ItemViewModelBase(Of dataitem, dbcontextclass), dbcontextclass As DbContext)
    Inherits ViewModelBase
    Implements IListViewModel(Of viewmodelitem)

#Region "COMMANDS"
    Public Property CreateCommand As ICommand = New Command(AddressOf CreateNewItem)
    Public Property UpdateAllCommand As ICommand = New Command(AddressOf UpdateAll)
    Public Property DeleteSelectedItemCommand As ICommand = New Command(AddressOf DeleteSelectedItem)
    Public Property OpenNewFormCommand As ICommand = New Command(AddressOf OpenNewForm)
    Public Property ApplyFilterCommand As ICommand = New Command(AddressOf ApplyFilter)
#End Region

#Region "PROPERTIES"
    Protected Property _DataController As datacontrollerclass
    Protected ReadOnly Property _WindowFactory As IWindowFactory
    Protected _ParentIDColumn As String
    Protected _ParentItem As IHasID
    Protected _Parameters As String ' e.g.: "Name = " & Chr(34) & "Donald" & Chr(34) or use _SelectedParent.ID for child objects of _Parentobject --- set when initializing after calling mybase.new
    Protected ReadOnly Property Parameters As String
        Get
            If _Parameters = "" Then
                If _ParentIDColumn <> "" And Not IsNothing(_ParentItem) Then
                    Return _ParentIDColumn + " = " + _ParentItem.ID
                End If
            End If
            Return _Parameters
        End Get
    End Property
    Private _OriginalItemList As New ObservableListSource(Of viewmodelitem)
    Private _ItemList As New ObservableListSource(Of viewmodelitem)
    Public Property ItemList() As ObservableListSource(Of viewmodelitem)
        Get
            Return _ItemList
        End Get
        Set(ByVal value As ObservableListSource(Of viewmodelitem))
            _ItemList = value
            OnPropertyChanged("ItemList")
        End Set
    End Property
    Private _SelectedItem As viewmodelitem
    Public Property SelectedItem() As viewmodelitem Implements IListViewModel(Of viewmodelitem).SelectedItem
        Get
            Return _SelectedItem
        End Get
        Set(ByVal value As viewmodelitem)
            If Not IsNothing(value) Then
                _SelectedItem = value
                If _SelectedItem.GetDataOnSelected Then _SelectedItem.GetData()
                OnPropertyChanged("SelectedItem")
            End If
        End Set
    End Property
#End Region

#Region "METHODS"
    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef windowfactory As IWindowFactory = Nothing, Optional ByRef parentidcomlun As String = "", Optional ByRef parentitem As IHasID = Nothing)
        _WindowFactory = windowfactory
        _DataController = datacontroller
        _ParentIDColumn = parentidcomlun
        _ParentItem = parentitem
    End Sub
    Protected Overridable Sub OpenNewForm()
        If (Not IsNothing(_WindowFactory) And Not IsNothing(SelectedItem)) Then _WindowFactory.OpenNewForm(SelectedItem)
    End Sub
    Public Sub SetParent(ByRef parentitemviewmodel As IHasID, ByVal parentidcolumn As String)
        _ParentItem = parentitemviewmodel
        _ParentIDColumn = parentidcolumn
    End Sub
#End Region

#Region "FILTER"
    'add bound properties for every filter parameter with ListViewModelFilterAttribute
    Protected Overridable Sub ApplyFilter()
        Dim filterparameters As String = GenerateFilterParameters(Me)
        If Not filterparameters = "" Then
            Dim filteredlist As List(Of viewmodelitem) = _OriginalItemList.ToList.Where(filterparameters).ToList
            Dim newlist As New ObservableListSource(Of viewmodelitem)
            For Each item In filteredlist
                newlist.Add(item)
            Next
            ItemList = newlist
        Else
            ItemList = _OriginalItemList
        End If
    End Sub
#End Region

#Region "CRUD"
    'override in order to use the specific data context of EntityItem to create a filled instance of DataClass and then use the datacontrollers CreateNewItem (Of DataClass)
    Public Overridable Sub CreateNewItem()
    End Sub
    Private Sub UpdateAll()
        For Each item As viewmodelitem In _OriginalItemList
            UpdateItem(item, Nothing)
        Next
    End Sub
    Protected Overridable Sub DeleteItem(sender As Object, e As EventArgs)
        Dim vmitem As viewmodelitem = sender
        If _DataController.DeleteItem(vmitem.Data) = True Then
            ItemList.Remove(vmitem)
            GetData()
        End If
    End Sub
    Protected Overridable Sub DeleteSelectedItem()
        If Not IsNothing(_SelectedItem) Then DeleteItem(_SelectedItem, Nothing)
    End Sub
    Protected Overridable Sub UpdateItem(sender As Object, e As EventArgs)
        Dim vmitem As viewmodelitem = sender
        _DataController.UpdateItem(vmitem.Data)
    End Sub
    Public Sub GetData()
        DataToList(_DataController.GetAllItems())
    End Sub
    Private Sub DataToList(ByRef dataitemlist As IEnumerable(Of dataitem))
        Dim selectedindex As Integer = ItemList.IndexOf(SelectedItem)
        Dim list = New ObservableListSource(Of viewmodelitem)
        For Each dataitem In dataitemlist
            Dim newvmitem As viewmodelitem = GetInstance(GetType(viewmodelitem))
            newvmitem.Data = dataitem
            newvmitem.DataContext = _DataController.DBContext
            AddHandler newvmitem.Deleted, AddressOf DeleteItem
            AddHandler newvmitem.Updated, AddressOf UpdateItem
            list.Add(newvmitem)
        Next
        _OriginalItemList = list
        ApplyFilter()
        If selectedindex >= 0 Then
            If (ItemList.Count >= selectedindex - 1) Then SelectedItem = ItemList(selectedindex) Else SelectedItem = ItemList(0)
        End If
        If ItemList.Any Then SelectedItem = ItemList(0)
    End Sub
#End Region
End Class
