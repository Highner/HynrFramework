Imports System.Windows.Forms

Public Interface IBindableListControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Property SelectedItem As viewmodelitem
End Interface

