<Serializable>
Public Class StarshipItemViewModel
    Inherits HynrFramework.ItemViewModelBase(Of StarshipData, FrameworkTestDBEntities)

    Public Property Name As String
        Get
            Return Data.Name
        End Get
        Set(value As String)
            Data.Name = value
        End Set
    End Property
End Class
