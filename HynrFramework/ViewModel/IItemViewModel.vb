Imports System.ComponentModel
Imports System.Windows.Input

Public Interface IItemViewModel(Of dataitem As IHasID)
    Inherits IHasID

    Property Data As dataitem
    Property DeleteCommand As ICommand
    Property UpdateCommand As ICommand
    Property GetDataOnSelected As Boolean

    Event Deleted(sender As Object, e As EventArgs)
    Event Updated(sender As Object, e As EventArgs)
End Interface
