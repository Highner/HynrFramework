Imports HynrFramework

Public Class StarshipListViewModel
    Inherits ListViewModelBase(Of Starship, StarshipData, StarshipDataController, StarshipItemViewModel)

    Public Sub New(ByVal parentid As Integer)
        MyBase.New(New StarshipDataController(parentid), New WindowFactory(Of StarshipData))
        Me.ParentID = parentid
    End Sub

    Public Overrides Function CreateNewItem() As StarshipData
        Dim person = New StarshipData
        person.PersonID = ParentID
        person.Name = "New Vessel"
        person.Type = "New Vesseltype"
        _DataController.CreateNewItem(person)
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




    Public ReadOnly Property Header As String
        Get
            If Not IsNothing(SelectedItem) Then
                Return "Selected vessel: " + SelectedItem.Name
            Else
                Return "No vessel selected"
            End If
        End Get
    End Property

End Class
