﻿Imports System.ComponentModel
Imports System.Windows.Input

Public Interface IViewModelItem(Of dataitem As IHasID)
    Inherits IHasID

    Property Data As dataitem
    Property DeleteCommand As ICommand
    Property UpdateCommand As ICommand

    Sub GetData()

    Event Deleted(sender As Object, e As EventArgs)
    Event Updated(sender As Object, e As EventArgs)
End Interface
