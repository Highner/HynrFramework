Imports System.Collections.Specialized
Imports System.Data.Entity
Imports System.Data.SqlClient

Public Interface IDataContext(Of T1)

    Function Save() As Boolean
    Function AddObject(ByRef entityobject As T1) As Boolean
    Function DeleteObject(ByVal id As Object) As Boolean
    Function GetObject(ByVal id As Object) As T1
    Function GetAllObjects() As IEnumerable(Of T1)
    Function GetAllObjectsQuery() As IQueryable(Of T1)
    Function GetAllObjectsQuery(ByRef entities As DbContext) As IQueryable(Of T1)
    Function GetObjects(parameters As Object) As IEnumerable(Of T1)

    Sub AddError(ByVal ex As Exception, ByVal errortype As String)

    ReadOnly Property ErrorLog As List(Of String)
    Function GetSQLDBContext() As DbContext

End Interface
