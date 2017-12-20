Public Interface IListViewModel(Of viewmodelitem)
    Inherits IListViewModelBase
    Property SelectedItem As viewmodelitem
    Property SelectedItems As List(Of viewmodelitem)

    ReadOnly Property SelectedItemID As Integer
    Property CanSave As Boolean

    Event SelectedItemChanged()

    Event CreateCommandExecuted(ByVal item As viewmodelitem)
    Event UpdateAllCommandExecuted()
    Event UpdateItemCommandExecuted(ByVal item As viewmodelitem)
    Event DeleteSelectedItemCommandExecuted(ByVal item As viewmodelitem)
    Event DeleteSelectedItemsCommandExecuted()
    Event FileDropped(item As Object, data As Object)
End Interface
