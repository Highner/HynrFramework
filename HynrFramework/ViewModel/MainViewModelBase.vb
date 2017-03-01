Public Class MainViewModelBase
    Inherits ViewModelBase


    Public Sub GetData()
        _GetData()
    End Sub

    Protected Overridable Sub _GetData()
    End Sub
End Class