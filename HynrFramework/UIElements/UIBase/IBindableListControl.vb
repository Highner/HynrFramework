Imports System.Data.Entity

Public Interface IBindableListControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem, dbcontextclass), dbcontextclass As DbContext)
    Property SelectedItem As viewmodelitem
    Property IsBusy As Boolean
    Property CancellationSource As Threading.CancellationTokenSource
End Interface

