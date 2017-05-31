Imports System.ComponentModel
Imports HynrFramework

Public Class PersonsListViewModel
    Inherits ListViewModelBase(Of Person, PersonData, PersonsDataController, PersonItemViewModel)

    Public Sub New(ByVal parentid As Integer)
        MyBase.New(New PersonsDataController(parentid), New WindowFactory)
        Me.ParentID = parentid
    End Sub

    Public Overrides Function CreateNewItem() As PersonItemViewModel
        _DataController.CreateNewItem(CreateNewPersonDataItem(ParentID))
        GetData()
    End Function

    Private _ParentID As Integer
    Public Property ParentID As Integer
        Get
            Return _ParentID
        End Get
        Set(value As Integer)
            _ParentID = value
            _DataController.ParentID = value
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
