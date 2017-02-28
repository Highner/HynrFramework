Public Class PersonsDataController
    Inherits HynrFramework.DataControllerBase(Of Person, PersonData, PersonsDataContext, FrameworkTestDBEntities)

    Public Sub New(ByVal parentid As Integer)
        Me.ParentID = parentid
    End Sub

    Public Overrides Function GetAllItems() As IEnumerable(Of PersonData)
        Dim items = GetItems("CountryID = " & ParentID)
        Return items
    End Function

    Private Property ParentID As Integer
End Class
