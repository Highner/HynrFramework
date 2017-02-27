Imports System.Data.Entity
Imports FrameworkTest
Imports HynrFramework

Public Class PersonsDataContext
    Inherits DataContextBase(Of Person, FrameworkTestDBEntities)

    Public Sub New(ByRef context As FrameworkTestDBEntities)
        MyBase.New(context)
    End Sub
End Class
