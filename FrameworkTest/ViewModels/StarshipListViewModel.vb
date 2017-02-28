Imports HynrFramework

Public Class StarshipListViewModel
    Inherits ListViewModelBase(Of Starship, StarshipData, StarshipDataController, StarshipItemViewModel, FrameworkTestDBEntities)

    Public Sub New(ByVal parentid As Integer)
        MyBase.New(New StarshipDataController(parentid), New WindowFactory)
        Me.ParentID = parentid
    End Sub

    Public Overrides Sub CreateNewItem()
        Dim person = New StarshipData
        person.PersonID = ParentID
        person.Name = "New Vessel"
        person.Type = "New Vesseltype"
        _DataController.CreateNewItem(person)
        GetData()
    End Sub


    Private ParentID As Integer




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
