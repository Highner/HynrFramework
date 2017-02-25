Imports System.Data.Entity
Imports System.Windows.Forms

Public Interface IBindableListControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem, dbcontextclass), dbcontextclass As DbContext)
    Property SelectedItem As viewmodelitem
End Interface

