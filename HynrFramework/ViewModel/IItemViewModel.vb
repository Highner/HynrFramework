Imports System.ComponentModel
Imports System.Windows.Input

Public Interface IItemViewModel(Of dataitem As IHasID)
    Inherits IHasID

    Property Data As dataitem
    Property DeleteCommand As ICommand
    Property UpdateCommand As ICommand
    Property DoubleClickCommand As ICommand
    Property ClickCommand As ICommand
    Property CanSave As Boolean

    Function Description() As String

    Event Deleted(sender As Object, e As EventArgs)
    Event Updated(sender As Object, e As EventArgs)
    Event DoubleClicked(sender As Object, cellindex As Integer)
    Event Clicked(sender As Object, e As EventArgs)
    Event CanSaveChanged(sender As Object, e As EventArgs)
    Event IsSelectedChanged(sender As Object, e As EventArgs)
    Event CheckedChanged(sender As Object, e As EventArgs)
    Event ColorChanged(sender As Object, e As EventArgs)
End Interface
