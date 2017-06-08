Imports System.Windows.Forms

Public Interface IListViewWindowFactory(Of dataitem As IHasID)
    Function OpenNewForm(ByRef datacontext As dataitem) As dataitem
    Function OpenEditForm(ByRef datacontext As dataitem) As dataitem
    Function DeleteConfirmationForm(ByRef datacontext As dataitem) As Boolean
End Interface
