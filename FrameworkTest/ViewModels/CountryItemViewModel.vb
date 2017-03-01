Imports System.ComponentModel

<Serializable>
Public Class CountryItemViewModel
    Inherits HynrFramework.ItemViewModelBase(Of CountryData)

    Public Property Name As String
        Get
            Return Data.Name
        End Get
        Set(value As String)
            Data.Name = value
        End Set
    End Property

End Class
