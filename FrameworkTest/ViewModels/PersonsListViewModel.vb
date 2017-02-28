﻿Imports System.ComponentModel
Imports HynrFramework

Public Class PersonsListViewModel
    Inherits ListViewModelBase(Of Person, PersonData, PersonsDataController, PersonItemViewModel, FrameworkTestDBEntities)

    Public Sub New(ByVal parentid As Integer)
        MyBase.New(New PersonsDataController(parentid), New WindowFactory)
        Me.ParentID = parentid
    End Sub

    Public Overrides Sub CreateNewItem()
        _DataController.CreateNewItem(CreateNewPersonDataItem(ParentID))
        GetData()
    End Sub

    Private ParentID As Integer
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
    Public ReadOnly Property Header As String
        Get
            If Not IsNothing(SelectedItem) Then
                Return "Selected Person: " + SelectedItem.Name
            Else
                Return "No person selected"
            End If
        End Get
    End Property
End Class
