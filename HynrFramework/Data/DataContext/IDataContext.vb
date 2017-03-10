Imports System.Data.Entity

Public Interface IDataContext(Of T1)
    Function Save() As Boolean
    Function AddObject(ByRef entityobject As T1) As Boolean
    Function DeleteObject(ByVal id As Object) As Boolean
    Function GetObject(ByVal id As Object) As T1
    Function GetAllObjects() As IEnumerable(Of T1)
    Function GetObjects(parameters As String) As IEnumerable(Of T1)

    Sub AddError(ByVal ex As Exception, ByVal errortype As String)

    'Property DBContext As dbcontextclass
    ReadOnly Property ErrorLog As List(Of String)
End Interface
