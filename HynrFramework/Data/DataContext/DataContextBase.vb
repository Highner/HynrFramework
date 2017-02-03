Imports System.Data.Entity
Imports HynrFramework
Imports System.Linq.Dynamic

Public Class DataContextBase(Of entityclass, dbcontextclass As DbContext) 'T1 = entity, T5 = DbContext
    Implements IDataContext(Of entityclass)

    Public Property DataContext As dbcontextclass

    Public Sub New(ByRef context As dbcontextclass)
        DataContext = context
    End Sub
    Public Sub Save() Implements IDataContext(Of entityclass).Save
        DataContext.SaveChanges()
    End Sub

    Public Overridable Function AddObject(ByRef entityobject As entityclass) As Boolean Implements IDataContext(Of entityclass).AddObject
        Try
            DataContext.Set(GetType(entityclass)).Add(entityobject)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function DeleteObject(id As Integer) As Boolean Implements IDataContext(Of entityclass).DeleteObject
        Try
            DataContext.Set(GetType(entityclass)).Remove(GetObject(id))
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Overridable Function GetAllObjects() As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass).GetAllObjects
        Return DataContext.Set(GetType(entityclass))
    End Function

    Public Overridable Function GetObject(id As Integer) As entityclass Implements IDataContext(Of entityclass).GetObject
        Return DataContext.Set(GetType(entityclass)).Find(id)
    End Function


End Class
