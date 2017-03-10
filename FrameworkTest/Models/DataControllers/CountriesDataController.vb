Imports FrameworkTest

Public Class CountriesDataController
    Inherits HynrFramework.DataControllerBase(Of Country, CountryData, CountriesDataContext)

    Public Overrides Function GetAllItems() As IEnumerable(Of CountryData)
        Return MyBase.GetAllItems()
    End Function
End Class
