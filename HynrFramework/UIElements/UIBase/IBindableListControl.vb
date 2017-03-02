Imports System.Data.Entity

Public Interface IBindableListControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits IViewModelBase

    Property SelectedItem As viewmodelitem
    Property CancellationSource As Threading.CancellationTokenSource
End Interface

