Imports HynrFramework

<Serializable>
Public Class StarshipData
    Implements HynrFramework.IHasID
    Public Property ID As Object Implements IHasID.ID
    Public Property Name As String
    Public Property PersonID As Integer
    Public Property Type As String
    Public Property WarpCapable As Boolean?
End Class
