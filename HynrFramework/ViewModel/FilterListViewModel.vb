Imports System.ComponentModel
Imports System.Windows.Input

Public Class FilterListViewModel(Of entityitme As IHasID, dataitem As ICheckboxFilterItem, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As CheckboxFilterDataItemViewModel(Of dataitem))
    Inherits ListViewModelBase(Of entityitme, dataitem, datacontrollerclass, viewmodelitem)
    Implements IFilterListViewModel(Of viewmodelitem)

#Region "Commands"
    <Browsable(False)>
    Public Property ToggleCheckedItemsCommand As ICommand = New Command(AddressOf ExecuteToggleCheckedItems) Implements IFilterListViewModel(Of viewmodelitem).ToggleCheckedItemsCommand
    <Browsable(False)>
    Public Property CheckAllItemsCommand As ICommand = New Command(AddressOf CheckAllItems)
    <Browsable(False)>
    Public Property UncheckAllItemsCommand As ICommand = New Command(AddressOf UncheckAllItems)
#End Region

#Region "Properties"
    Private Property _DisplayNameFilter As String
    <ListViewModelFilterAttribute("DisplayName", "string", False)>
    Public Property DisplayNameFilter As String
        Get
            Return _DisplayNameFilter
        End Get
        Set(value As String)
            _DisplayNameFilter = value
            ApplyFilter()
        End Set
    End Property

    Private _CheckedItems As New List(Of viewmodelitem)
    Public Property CheckedItems() As List(Of viewmodelitem) Implements IFilterListViewModel(Of viewmodelitem).CheckedItems
        Get
            Return _CheckedItems
        End Get
        Set(ByVal value As List(Of viewmodelitem))
            _CheckedItems = value
            OnPropertyChanged("CheckedItems")
        End Set
    End Property
#End Region

#Region "Methods"
    Public Overridable Function GetFilterString() As String
        Return CreateStringListforFilter((From i In CheckedItems Select i.DisplayName).ToList)
    End Function
    Public Overridable Function GetFilterArray() As Array
        Return (From c In CheckedItems Select c.Data.FilterString).ToArray
    End Function
    Private Sub ExecuteToggleCheckedItems()
        If CheckedItems.Any Then
            UncheckAllItems()
        Else
            CheckAllItems()
        End If
    End Sub
    Private Sub CheckAllItems()
        CheckedItems.Clear()
        For Each item In ItemList
            item.Checked = True
        Next
    End Sub
    Private Sub UncheckAllItems()
        For Each item In ItemList
            item.Checked = False
        Next
    End Sub
    Public Overrides Function DataToItem(ByRef dataitem As dataitem) As viewmodelitem
        Dim newvmitem = MyBase.DataToItem(dataitem)
        AddHandler newvmitem.CheckedChanged, AddressOf RaiseItemCheckedChanged
        Return newvmitem
    End Function
    Private Sub RaiseItemCheckedChanged(sender As Object, e As EventArgs)
        Dim item = sender
        If item.Checked Then
            CheckedItems.Add(item)
        Else
            If CheckedItems.Contains(item) Then CheckedItems.Remove(item)
        End If
        OnPropertyChanged("CheckedItems")
        RaiseEvent ItemCheckedChanged(item)
    End Sub
#End Region

#Region "Events"
    Public Event ItemCheckedChanged(ByVal item As viewmodelitem) Implements IFilterListViewModel(Of viewmodelitem).ItemCheckedChanged
#End Region
End Class