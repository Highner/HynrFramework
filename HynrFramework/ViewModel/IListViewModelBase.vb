Public Interface IListViewModelBase
    Inherits IViewModelBase
    Function GetUniqueItemsForFilter(ByVal propertyname As String) As String()
    Sub RaiseFileDropped(item As Object, data As Object)
    Sub RaiseItemClicked(item As Object, index As Integer)
    Sub RaiseItemDoubleClicked(item As Object, index As Integer)
    Event ItemListChanged()
    Event ItemAdded(item As Object)
    Event ItemDeleted()
    Event ItemChanged()
End Interface
