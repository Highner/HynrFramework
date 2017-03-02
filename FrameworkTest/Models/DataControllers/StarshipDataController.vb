Public Class StarshipDataController
    Inherits HynrFramework.DataControllerBase(Of Starship, StarshipData, StarshipDataContext, FrameworkTestDBEntities)

    Public Sub New(ByVal parentid As Integer)
        Me.ParentID = parentid
    End Sub

    Public Overrides Function GetAllItems() As IEnumerable(Of StarshipData)
        Dim items = GetItems("PersonID = " & ParentID)
        Return items
    End Function

    Public Property ParentID As Integer
End Class
