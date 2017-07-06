Imports System.Windows.Input

Public Interface ICrudObject
    Property DeleteSelectedItemsCommand As ICommand
    Property OpenNewFormCommand As ICommand
    Property OpenEditFormCommand As ICommand
    Property RefreshAllAsyncCommand As ICommand
    Property UpdateAllCommand As ICommand
End Interface
