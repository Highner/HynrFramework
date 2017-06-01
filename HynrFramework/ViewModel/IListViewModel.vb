Public Interface IListViewModel(Of viewmodelitem)
    Property SelectedItem As viewmodelitem
    Property SelectedItems As List(Of viewmodelitem)
    ReadOnly Property SelectedItemID As Integer
    Property CanSave As Boolean

    Event SelectedItemChanged()
    Event ItemListChanged()
    Event CreateCommandExecuted(ByVal item As viewmodelitem)
    Event UpdateAllCommandExecuted()
    Event UpdateItemCommandExecuted(ByVal item As viewmodelitem)
    Event DeleteSelectedItemCommandExecuted(ByVal item As viewmodelitem)
    Event DeleteSelectedItemsCommandExecuted()
End Interface
