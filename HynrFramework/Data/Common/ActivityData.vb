Imports System.ComponentModel

Public Class ActivityData
    Implements IHasID

    Public Property ID As Object Implements IHasID.ID
        Get
            Return ActivityID
        End Get
        Set(value As Object)
            ActivityID = value
        End Set
    End Property

    Property ActivityID As Integer
    Property ObjectID As Integer?
    Property UserID As Integer?
    Property ValueDate As Date
    Property ActivityMessage As String
    Property IsPublic As Boolean
    Property ObjectType As String
    Property ModuleID As Integer?
End Class
