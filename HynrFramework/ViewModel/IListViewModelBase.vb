Public Interface IListViewModelBase
    Inherits IViewModelBase
    Function GetUniqueItemsForFilter(ByVal propertyname As String) As String()
    Event ItemListChanged()
    Event ItemAdded()
    Event ItemDeleted()
    Event ItemChanged()
End Interface
