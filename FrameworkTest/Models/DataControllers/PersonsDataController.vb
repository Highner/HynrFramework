Imports FrameworkTest

Public Class PersonsDataController
    Inherits HynrFramework.DataControllerBase(Of Person, PersonData, PersonsDataContext, FrameworkTestDBEntities)

    Public Sub New(ByRef context As PersonsDataContext)
        MyBase.New(context)
    End Sub

End Class
