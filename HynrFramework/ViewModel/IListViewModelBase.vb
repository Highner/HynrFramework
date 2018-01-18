Public Interface IListViewModelBase
    Inherits IViewModelBase
    Function GetUniqueItemsForFilter(ByVal propertyname As String) As String()
    Sub RaiseFileDropped(item As Object, data As Object)
    Event ItemListChanged()
    Event ItemAdded(item As Object)
    Event ItemDeleted()
    Event ItemChanged()
End Interface
