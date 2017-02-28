Imports HynrFramework
<Serializable>
Public Class PersonData
    Implements IHasID

    Public Property ID As Integer Implements IHasID.ID
    Public Property Name As String
    Public Property Birthday As Date?
    Public Property Country As String
    Public Property CountryID As Integer?
    Public Property Remarks As String
    Public ReadOnly Property Age As Integer
        Get
            If Not IsNothing(Birthday) Then
                Dim bd As Date = Birthday
                Return Math.Round((Date.Today - bd).Days / 365, 0)
            Else
                Return 0
            End If
        End Get
    End Property
End Class