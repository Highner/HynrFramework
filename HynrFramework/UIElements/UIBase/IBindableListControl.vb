Imports System.Data.Entity

Public Interface IBindableListControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Property SelectedItem As viewmodelitem
    Property IsBusy As Boolean
    Property CancellationSource As Threading.CancellationTokenSource
End Interface

