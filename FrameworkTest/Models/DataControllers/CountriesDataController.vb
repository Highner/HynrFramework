Imports FrameworkTest

Public Class CountriesDataController
    Inherits HynrFramework.DataControllerBase(Of Country, CountryData, CountriesDataContext, FrameworkTestDBEntities)

    Public Overrides Function GetAllItems() As IEnumerable(Of CountryData)
        Threading.Thread.Sleep(5000)
        Return MyBase.GetAllItems()
    End Function
End Class
