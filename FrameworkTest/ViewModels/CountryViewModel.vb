﻿Imports System.ComponentModel

Public Class CountryViewModel
    Inherits HynrFramework.ViewModelBase

    Public WithEvents CountriesVM As New CountryListViewModel
    Public WithEvents PersonsVM As New PersonsListViewModel(0)
    Public WithEvents StarshipsVM As New StarshipListViewModel(0)

    Public Sub GetData()
        CountriesVM.GetData()
    End Sub

    Private Sub LoadPersons() Handles CountriesVM.SelectedItemChanged
        PersonsVM = New PersonsListViewModel(CountriesVM.SelectedItem.ID)
        PersonsVM.GetData()
        RaiseLoadingCompleted()
    End Sub

    Private Sub LoadStarships() Handles PersonsVM.SelectedItemChanged
        StarshipsVM = New StarshipListViewModel(PersonsVM.SelectedItem.ID)
        StarshipsVM.GetData()
        RaiseLoadingCompleted()
    End Sub
End Class