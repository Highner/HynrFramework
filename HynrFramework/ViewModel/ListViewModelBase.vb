Imports System.Windows.Forms
Imports System.Windows.Input

'only contructor and CreateNewItem and possibly _Parameters need to be specified in inherited class
Public Class ListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ViewModelBase
    Implements IListViewModel

#Region "COMMANDS"
    Public Property CreateCommand As ICommand = New Command(AddressOf CreateNewItem)
    Public Property UpdateAllCommand As ICommand = New Command(AddressOf UpdateAll)
    Public Property DeleteSelectedItemCommand As ICommand = New Command(AddressOf DeleteSelectedItem)
    Public Property OpenNewFormCommand As ICommand = New Command(AddressOf OpenNewForm)
#End Region

#Region "PROPERTIES"
    Protected Property _DataController As datacontrollerclass
    Protected ReadOnly Property _WindowFactory As IWindowFactory
    Protected _ParentIDColumn As String
    Protected _ParentIDValue As String
    Protected _Parameters As String ' e.g.: "Name = " & Chr(34) & "Donald" & Chr(34) or use _SelectedParent.ID for child objects of _Parentobject --- set when initializing after calling mybase.new
    Protected ReadOnly Property Parameters As String
        Get
            If _Parameters = "" Then
                If _ParentIDColumn <> "" And _ParentIDValue <> "" Then
                    Return _ParentIDColumn + " = " + _ParentIDValue
                End If
            End If
            Return _Parameters
        End Get
    End Property
    'Protected _ParentListViewModel As IListViewModel
    'Private _SelectedParent As IItemViewModel(Of IHasID)
    'Protected Property SelectedParent As IItemViewModel(Of IHasID)
    '    Get
    '        Return _SelectedParent
    '    End Get
    '    Set(value As IItemViewModel(Of IHasID))
    '        _SelectedParent = value
    '        _Parameters = _ParentIDColumn + "=" + SelectedParent.ID
    '        GetData()
    '    End Set
    'End Property
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
    Public Property SelectedItem() As Object Implements IListViewModel.SelectedItem
        Get
            Return _SelectedItem
        End Get
        Set(ByVal value As Object)
            If Not IsNothing(value) Then
                _SelectedItem = value
                If _SelectedItem.GetDataOnSelected Then _SelectedItem.GetData()
                OnPropertyChanged("SelectedItem")
            End If
        End Set
    End Property
#End Region

#Region "METHODS"
    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef windowfactory As IWindowFactory = Nothing)
        _WindowFactory = windowfactory
        _DataController = datacontroller
    End Sub
    Protected Overridable Sub OpenNewForm()
        If (Not IsNothing(_WindowFactory) And Not IsNothing(SelectedItem)) Then _WindowFactory.OpenNewForm(SelectedItem)
    End Sub
    Public Sub BindToListControl(ByRef control As IBindableListControl(Of dataitem, viewmodelitem))
        control.ControlDataBindings.Add("BindingSourceDataSource", Me, "ItemList", True, DataSourceUpdateMode.OnPropertyChanged)
        control.ControlDataBindings.Add("SelectedItem", Me, "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub
#End Region

#Region "CRUD"
    'override in order to use the specific data context of EntityItem to create a filled instance of DataClass and then use the datacontrollers CreateNewItem (Of DataClass)
    Public Overridable Sub CreateNewItem()
    End Sub
    Private Sub UpdateAll()
        For Each item As viewmodelitem In ItemList
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
    Public Overridable Sub GetData()
        Dim selectedindex As Integer = ItemList.IndexOf(SelectedItem)
        Dim list = New ObservableListSource(Of viewmodelitem)
        For Each dataitem In _DataController.GetAllItems(Parameters)
            Dim newvmitem As viewmodelitem = GetInstance(GetType(viewmodelitem))
            newvmitem.Data = dataitem
            AddHandler newvmitem.Deleted, AddressOf DeleteItem
            AddHandler newvmitem.Updated, AddressOf UpdateItem
            list.Add(newvmitem)
        Next
        ItemList = list
        If selectedindex >= 0 Then
            If (ItemList.Count >= selectedindex - 1) Then SelectedItem = ItemList(selectedindex) Else SelectedItem = ItemList(0)
        End If
        If ItemList.Count > 0 Then SelectedItem = ItemList(0)
    End Sub
    Public Sub GetData(ByVal parameters As String)
        _Parameters = parameters
        GetData()
        _Parameters = ""
    End Sub
#End Region
End Class
