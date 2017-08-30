Imports System.Windows.Input

Public Interface IFilterListViewModel(Of viewmodelitem)
    Inherits IListViewModel(Of viewmodelitem)

    Property ToggleCheckedItemsCommand As ICommand

    Property CheckedItems As List(Of viewmodelitem)

    Event ItemCheckedChanged(ByVal item As viewmodelitem)
End Interface
