Public Class PersonsDataController
    Inherits HynrFramework.DataControllerBase(Of Person, PersonData, PersonsDataContext)

    Public Sub New(ByRef context As PersonsDataContext)
        MyBase.New(context)
    End Sub
End Class
