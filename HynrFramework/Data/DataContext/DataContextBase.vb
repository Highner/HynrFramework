Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Linq.Dynamic

Public MustInherit Class DataContextBase(Of entityclass, dbcontextclass As DbContext)
    Implements IDataContext(Of entityclass)

#Region "Properties"
    Private WithEvents _ErrorLog As New BindingList(Of String)
    Protected ShowError As Boolean = True
    Private _DBContext As dbcontextclass = GetInstance(GetType(dbcontextclass))
    Public Property DBContext As dbcontextclass
        Get
            Return _DBContext
        End Get
        Set(value As dbcontextclass)
            _DBContext = value
        End Set
    End Property
    Private ReadOnly Property ErrorLog As List(Of String) Implements IDataContext(Of entityclass).ErrorLog
        Get
            Return _ErrorLog.ToList
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
    End Sub
    Public Sub New(ByRef context As dbcontextclass)
        DBContext = context
    End Sub
#End Region

#Region "Methods"
#End Region

#Region "Error"
    Private Sub ShowNewError() Handles _ErrorLog.ListChanged
    End Sub
    Public Sub AddError(ByVal ex As Exception, ByVal errortype As String) Implements IDataContext(Of entityclass).AddError
        Dim errortext As String = errortype & " Error: "
        If Not IsNothing(ex.InnerException) Then
            _ErrorLog.Add(errortext + ex.InnerException.ToString)
        Else
            _ErrorLog.Add(errortext + ex.Message)
        End If
    End Sub
#End Region

#Region "Crud"
    Public Function Save() As Boolean Implements IDataContext(Of entityclass).Save
        Try
            DBContext.SaveChanges()
        Catch ex As Exception
            AddError(ex, "DB SaveChanges")
            Return False
        End Try
        Return True
    End Function
    Public Overridable Function AddObject(ByRef entityobject As entityclass) As Boolean Implements IDataContext(Of entityclass).AddObject
        Try
            DBContext.Set(GetType(entityclass)).Add(entityobject)
        Catch ex As Exception
            AddError(ex, "DB AddObject")
            Return False
        End Try
        Return True
    End Function
    Public Overridable Function DeleteObject(id As Integer) As Boolean Implements IDataContext(Of entityclass).DeleteObject
        Try
            DBContext.Set(GetType(entityclass)).Remove(GetObject(id))
        Catch ex As Exception
            AddError(ex, "DB DeleteObject")
            Return False
        End Try
        Return True
    End Function
    Public Overridable Function GetAllObjects() As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass).GetAllObjects
        Return DBContext.Set(GetType(entityclass))
    End Function
    Public Overridable Function GetObject(id As Integer) As entityclass Implements IDataContext(Of entityclass).GetObject
        Return DBContext.Set(GetType(entityclass)).Find(id)
    End Function
    Public Function GetObjects(parameters As String) As IEnumerable(Of entityclass) Implements IDataContext(Of entityclass).GetObjects
        Return DBContext.Set(GetType(entityclass)).Where(parameters)
    End Function
#End Region
End Class
