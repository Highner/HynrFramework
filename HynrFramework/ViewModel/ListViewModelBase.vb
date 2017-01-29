Imports System.Windows.Forms
Imports System.Windows.Input

'only contructor and CreateNewItem need to be specified in inherited class
Public Class ListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ViewModelBase

    Protected Property DataController As datacontrollerclass
    Public Property CreateCommand As ICommand = New Command(AddressOf CreateNewItem)
    Public Property UpdateAllCommand As ICommand = New Command(AddressOf UpdateAll)
    Public Property DeleteSelectedItemCommand As ICommand = New Command(AddressOf DeleteSelectedItem)
    Public Property OpenNewFormCommand As ICommand = New Command(AddressOf OpenNewForm)

    Protected ReadOnly Property _WindowFactory As IWindowFactory
    Private _Parameters As String
    Private _ParentObject As IViewModelItem(Of IHasID)

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
    Public Property SelectedItem() As viewmodelitem
        Get
            Return _SelectedItem
        End Get
        Set(ByVal value As viewmodelitem)
            If Not IsNothing(value) Then
                _SelectedItem = value
                _SelectedItem.GetData()
                OnPropertyChanged("SelectedItem")
            End If
        End Set
    End Property

    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef bindingsource As BindingSource = Nothing, Optional ByRef windowfactory As IWindowFactory = Nothing, Optional ByRef parentobject As IViewModelItem(Of IHasID) = Nothing)
        _WindowFactory = windowfactory
        Me.DataController = datacontroller
        If Not IsNothing(bindingsource) Then bindingsource.DataSource = ItemList
        _ParentObject = parentobject
    End Sub
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
        If DataController.DeleteItem(vmitem.Data) = True Then
            ItemList.Remove(vmitem)
            GetData()
        End If
    End Sub
    Protected Overridable Sub DeleteSelectedItem()
        If Not IsNothing(_SelectedItem) Then DeleteItem(_SelectedItem, Nothing)
    End Sub
    Protected Overridable Sub UpdateItem(sender As Object, e As EventArgs)
        Dim vmitem As viewmodelitem = sender
        DataController.UpdateItem(vmitem.Data)
    End Sub
    Public Overridable Sub GetData()
        Dim selectedindex As Integer = ItemList.IndexOf(SelectedItem)
        Dim list = New ObservableListSource(Of viewmodelitem)
        For Each dataitem In DataController.GetAllItems(_Parameters)
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
    Protected Overridable Sub OpenNewForm()
        If (Not IsNothing(_WindowFactory) And Not IsNothing(SelectedItem)) Then _WindowFactory.OpenNewForm(SelectedItem)
    End Sub
End Class
