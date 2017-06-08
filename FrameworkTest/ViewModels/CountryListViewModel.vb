Imports HynrFramework

Public Class CountryListViewModel
    Inherits HynrFramework.ListViewModelBase(Of Country, CountryData, CountriesDataController, CountryItemViewModel)


    Public Overrides Function CreateNewItem() As CountryData
        Dim cntry As New CountryData
        cntry.Name = "New Country"
        _DataController.CreateNewItem(cntry)
        GetData()
    End Function

    Public Property Header As String
        Get
            If Not IsNothing(SelectedItem) Then
                Return "Selected Country: " + SelectedItem.Name
            Else
                Return "No person selected"
            End If
        End Get
        Set(value As String)
        End Set
    End Property

    Private Property _NameFilter As String
    <ListViewModelFilterAttribute("Name", "string", False)>
    Property NameFilter As String
        Get
            Return _NameFilter
        End Get
        Set(value As String)
            _NameFilter = value
            ApplyFilter()
        End Set
    End Property
End Class
