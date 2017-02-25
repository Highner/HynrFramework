Imports System.Data.Entity

Public Interface IDataContext(Of T1, dbcontextclass As DbContext)
    Function Save() As Boolean
    Function AddObject(ByRef entityobject As T1) As Boolean
    Function DeleteObject(ByVal id As Integer) As Boolean
    Function GetObject(ByVal id As Integer) As T1
    Function GetAllObjects() As IEnumerable(Of T1)
    Function GetObjects(parameters As String) As IEnumerable(Of T1)

    Property DBContext As dbcontextclass
End Interface
