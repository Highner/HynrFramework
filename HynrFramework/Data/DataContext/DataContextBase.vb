Imports System.Data.Entity
Imports HynrFramework

Public Class DataContextBase(Of entityclass, dbcontextclass As DbContext) 'T1 = entity, T5 = DbContext
    Implements IDataContext(Of entityclass)

    Public Property DataContext As dbcontextclass

    Public Sub New(ByRef context As dbcontextclass)
        DataContext = context
    End Sub
    Public Sub Save() Implements IDataContext(Of entityclass).Save
        DataContext.SaveChanges()
    End Sub

    Public Function AddObject(ByRef entityobject As entityclass) As Boolean Implements IDataContext(Of entityclass).AddObject
        Try
            DataContext.Set(GetType(entityclass)).Add(entityobject)
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function DeleteObject(id As Integer) As Boolean Implements IDataContext(Of entityclass).DeleteObject
        Try
            DataContext.Set(GetType(entityclass)).Remove(GetObject(id))
        Catch
            Return False
        End Try
        Return True
    End Function

    Public Function GetAllObjects(ByVal parameters As String) As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass).GetAllObjects
        If parameters = "" Then
            Return DataContext.Set(GetType(entityclass))
        Else
            Try
                Dim objects As IEnumerable(Of entityclass) = DataContext.Set(GetType(entityclass))
                Return (From o In objects Where (parameters) Select o).ToList
            Catch
                Return New List(Of entityclass)
            End Try
        End If
    End Function
    Public Function GetAllObjects() As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass).GetAllObjects
        Return DataContext.Set(GetType(entityclass))
    End Function

    Public Function GetObject(id As Integer) As entityclass Implements IDataContext(Of entityclass).GetObject
        Return DataContext.Set(GetType(entityclass)).Find(id)
    End Function


End Class
