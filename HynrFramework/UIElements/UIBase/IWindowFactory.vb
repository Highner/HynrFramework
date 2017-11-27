Imports System.Windows.Forms

Public Interface IListViewWindowFactory(Of dataitem As IHasID)
    Function OpenNewForm(ByVal datacontext As dataitem) As dataitem
    Function OpenEditForm(ByVal datacontext As dataitem) As dataitem
    Function DeleteConfirmationForm(ByVal datacontext As dataitem) As Boolean
    Event FormClosed(ByVal datacontext As dataitem)
End Interface
