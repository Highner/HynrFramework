Imports System.Windows.Input

Public Class Command
    Implements ICommand

    Private _action As Action

    Sub New(action As Action)
        _action = action
    End Sub

    Public Function CanExecute(parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function

    Public Event CanExecuteChanged(sender As Object, e As EventArgs) Implements ICommand.CanExecuteChanged

    Public Sub Execute(parameter As Object) Implements ICommand.Execute
        _action()
    End Sub
End Class
