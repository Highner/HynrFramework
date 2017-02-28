﻿Public Interface IListViewModel(Of viewmodelitem)
    Property SelectedItem As viewmodelitem
    Property SelectedItems As List(Of viewmodelitem)
    Property CanSave As Boolean

    Event SelectedItemChanged()
    Event LoadingCompleted()
End Interface
