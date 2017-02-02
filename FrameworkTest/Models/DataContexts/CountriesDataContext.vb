Public Class CountriesDataContext
    Inherits HynrFramework.DataContextBase(Of Country, FrameworkTestDBEntities)

    Public Sub New(ByRef context As FrameworkTestDBEntities)
        MyBase.New(context)
    End Sub
End Class
