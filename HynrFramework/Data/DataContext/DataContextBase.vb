Imports System.Data.Entity
Imports HynrFramework
Imports System.Linq.Dynamic

Public Class DataContextBase(Of entityclass, dbcontextclass As DbContext) 'T1 = entity, T5 = DbContext
    Implements IDataContext(Of entityclass, dbcontextclass)

    Public Property DBContext As dbcontextclass Implements IDataContext(Of entityclass, dbcontextclass).DBContext


    Public Sub New(ByRef context As dbcontextclass)
        DBContext = context
    End Sub
    Public Sub Save() Implements IDataContext(Of entityclass, dbcontextclass).Save
        DBContext.SaveChanges()
    End Sub

    Public Overridable Function AddObject(ByRef entityobject As entityclass) As Boolean Implements IDataContext(Of entityclass, dbcontextclass).AddObject
        Try
            DBContext.Set(GetType(entityclass)).Add(entityobject)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function DeleteObject(id As Integer) As Boolean Implements IDataContext(Of entityclass, dbcontextclass).DeleteObject
        Try
            DBContext.Set(GetType(entityclass)).Remove(GetObject(id))
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function GetAllObjects() As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass, dbcontextclass).GetAllObjects
        Return DBContext.Set(GetType(entityclass))
    End Function

    Public Overridable Function GetObject(id As Integer) As entityclass Implements IDataContext(Of entityclass, dbcontextclass).GetObject
        Return DBContext.Set(GetType(entityclass)).Find(id)
    End Function


End Class
