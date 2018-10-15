Imports HynrFramework

Public Class HynrCheckboxFilterItem
    Implements ICheckboxFilterItem

    Private _Checked As Boolean
    Public Property Checked As Boolean Implements IIsCheckable.Checked
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
        End Set
    End Property

    Public Property DisplayName As String Implements IHasDisplayName.DisplayName


    Public Property FilterString As String Implements ICheckboxFilterItem.FilterString
        Get
            Return DisplayName
        End Get
        Set(value As String)
        End Set
    End Property

    Public Property ID As Object Implements IHasID.ID
        Get
            Return DisplayName
        End Get
        Set(value As Object)
        End Set
    End Property
End Class
