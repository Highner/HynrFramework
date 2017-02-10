Imports System.Data.Entity
Imports System.Windows.Forms

Public Interface IBindableListControl(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem, dbcontextclass), dbcontextclass As DbContext)
    Property SelectedItem As viewmodelitem
    Property BindingSourceDataSource As Object
    ReadOnly Property ControlDataBindings As ControlBindingsCollection
End Interface

