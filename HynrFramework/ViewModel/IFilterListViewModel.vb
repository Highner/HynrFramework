Public Interface IFilterListViewModel(Of viewmodelitem)
    Inherits IListViewModel(Of viewmodelitem)

    Property CheckedItems As List(Of viewmodelitem)

    Event ItemCheckedChanged(ByVal item As viewmodelitem)
End Interface
