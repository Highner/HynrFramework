<AttributeUsage(AttributeTargets.Property,
    AllowMultiple:=False, Inherited:=False)>
Public Class ListViewModelFilterAttribute
    Inherits Attribute
    Private _FilteredField As String
    Private _ValueType As String
    Private _ExactMatch As Boolean = False

    Public Sub New(ByVal filterfieldname As String, ByVal typestring As String, ByVal exactmatch As Boolean)
        _FilteredField = filterfieldname
        _ValueType = typestring
        _ExactMatch = exactmatch
    End Sub
    Public Property FilteredField() As String
        Get
            Return _FilteredField
        End Get
        Set(ByVal Value As String)
            _FilteredField = Value
        End Set
    End Property
    Public Property ValueType() As String
        Get
            Return _ValueType
        End Get
        Set(ByVal Value As String)
            _ValueType = Value
        End Set
    End Property
    Public Property ExactMatch As Boolean
        Get
            Return _ExactMatch
        End Get
        Set(value As Boolean)
            _ExactMatch = value
        End Set
    End Property
End Class