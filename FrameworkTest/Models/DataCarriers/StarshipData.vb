Imports HynrFramework

<Serializable>
Public Class StarshipData
    Implements HynrFramework.IHasID
    Public Property ID As Integer Implements IHasID.ID
    Public Property Name As String
    Public Property PersonID As Integer
    Public Property Type As String
End Class
