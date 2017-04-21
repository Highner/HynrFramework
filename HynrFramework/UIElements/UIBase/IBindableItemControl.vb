Public Interface IBindableItemControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits IViewModelBase

    Property DataContext As viewmodelitem

End Interface
