Imports System.Windows.Forms

Public Interface IWindowFactory
    Function OpenNewForm(ByRef datacontext As Object) As IHasID
End Interface
