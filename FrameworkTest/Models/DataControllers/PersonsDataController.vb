Public Class PersonsDataController
    Inherits HynrFramework.DataControllerBase(Of Person, PersonData, PersonsDataContext)

    Public Sub New(ByVal parentid As Integer)
        Me.ParentID = parentid
    End Sub

    Public Overrides Function GetAllItems() As IEnumerable(Of PersonData)
        Dim items = GetItems("CountryID = " & ParentID)
        Return items
    End Function

    Public Property ParentID As Integer
End Class
