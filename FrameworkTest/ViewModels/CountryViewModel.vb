Imports System.ComponentModel

Public Class CountryViewModel
    Inherits HynrFramework.ViewModelBase

    Public WithEvents CountriesVM As New CountryListViewModel
    Public WithEvents PersonsVM As New PersonsListViewModel(0)
    Public WithEvents StarshipsVM As New StarshipListViewModel(0)

    Public Sub GetData()
        CountriesVM.GetData()
    End Sub

    Private Sub LoadPersons() Handles CountriesVM.SelectedItemChanged
        PersonsVM.ParentID = CountriesVM.SelectedItemID
        PersonsVM.GetData()

    End Sub

    Private Sub LoadStarships() Handles PersonsVM.SelectedItemChanged
        StarshipsVM.ParentID = PersonsVM.SelectedItemID
        StarshipsVM.GetData()
        'Name = PersonsVM.SelectedItem.Name
        ' RaiseLoadingCompleted()
    End Sub



    Private Sub test() Handles PersonsVM.LoadingCompleted
        ' MsgBox("completed")

    End Sub
End Class