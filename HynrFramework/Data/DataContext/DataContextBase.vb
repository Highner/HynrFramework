Imports System.Data.Entity
Imports System.Linq.Dynamic

Public MustInherit Class DataContextBase(Of entityclass, dbcontextclass As DbContext)
    Implements IDataContext(Of entityclass, dbcontextclass)

#Region "Properties"
    Private _ErrorLog As New List(Of String)
    Public Property DBContext As dbcontextclass
    Private ReadOnly Property ErrorLog As List(Of String) Implements IDataContext(Of entityclass, dbcontextclass).ErrorLog
        Get
            Return _ErrorLog
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        DBContext = GetInstance(GetType(dbcontextclass))
    End Sub
    Public Sub New(ByRef context As dbcontextclass)
        DBContext = context
    End Sub
#End Region

#Region "Crud"
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
#End Region
End Class
