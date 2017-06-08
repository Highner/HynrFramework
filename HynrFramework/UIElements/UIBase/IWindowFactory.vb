Imports System.Windows.Forms

Public Interface IWindowFactory(Of dataitem As IHasID)
    Function OpenNewForm(ByRef datacontext As dataitem) As dataitem
    Function OpenEditForm(ByRef datacontext As dataitem) As dataitem
End Interface
