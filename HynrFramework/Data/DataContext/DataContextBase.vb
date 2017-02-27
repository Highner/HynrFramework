Imports System.Data.Entity
Imports HynrFramework
Imports System.Linq.Dynamic

Public Class DataContextBase(Of entityclass, dbcontextclass As DbContext) 'T1 = entity, T5 = DbContext
    Implements IDataContext(Of entityclass, dbcontextclass)

    Private _ErrorLog As New List(Of String)
    Public Property DBContext As dbcontextclass Implements IDataContext(Of entityclass, dbcontextclass).DBContext

    Private ReadOnly Property ErrorLog As List(Of String) Implements IDataContext(Of entityclass, dbcontextclass).ErrorLog
        Get
            Return _ErrorLog
        End Get
    End Property

    Public Sub New(ByRef context As dbcontextclass)
        DBContext = context
    End Sub
    Public Function Save() As Boolean Implements IDataContext(Of entityclass, dbcontextclass).Save
        Try
            DBContext.SaveChanges()
        Catch ex As Exception
            ErrorLog.Add("DB Save Error: " + ex.InnerException.ToString)
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function AddObject(ByRef entityobject As entityclass) As Boolean Implements IDataContext(Of entityclass, dbcontextclass).AddObject
        Try
            DBContext.Set(GetType(entityclass)).Add(entityobject)
        Catch ex As Exception
            ErrorLog.Add("DB AddObject Error: " + ex.InnerException.ToString)
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function DeleteObject(id As Integer) As Boolean Implements IDataContext(Of entityclass, dbcontextclass).DeleteObject
        Try
            DBContext.Set(GetType(entityclass)).Remove(GetObject(id))
        Catch ex As Exception
            ErrorLog.Add("DB DeleteObject Error: " + ex.InnerException.ToString)
        End Try
        Return True
    End Function

    Public Overridable Function GetAllObjects() As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass, dbcontextclass).GetAllObjects
        Return DBContext.Set(GetType(entityclass))
    End Function

    Public Overridable Function GetObject(id As Integer) As entityclass Implements IDataContext(Of entityclass, dbcontextclass).GetObject
        Return DBContext.Set(GetType(entityclass)).Find(id)
    End Function

    Public Function GetObjects(parameters As String) As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass, dbcontextclass).GetObjects
        Return DBContext.Set(GetType(entityclass)).Where(parameters)
    End Function

End Class
