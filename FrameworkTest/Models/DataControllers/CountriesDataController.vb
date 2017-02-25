Imports FrameworkTest

Public Class CountriesDataController
    Inherits HynrFramework.DataControllerBase(Of Country, CountryData, CountriesDataContext, FrameworkTestDBEntities)

    Public Sub New(ByRef context As CountriesDataContext)
        MyBase.New(context)
    End Sub

End Class
