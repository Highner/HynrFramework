Imports System.Data.Entity

Public Interface IDataContext(Of T1)
    Sub Save()
    Function AddObject(ByRef entityobject As T1) As Boolean
    Function DeleteObject(ByVal id As Integer) As Boolean
    Function GetObject(ByVal id As Integer) As T1
    Function GetAllObjects() As IEnumerable(Of T1)
End Interface
