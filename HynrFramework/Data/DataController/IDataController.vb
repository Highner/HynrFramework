Imports System.Data.Entity
Imports System.Data.SqlClient

Public Interface IDataController(Of T1 As IHasID, T2 As IHasID) 'T1 = entityitem, T2 = dataitem
    Inherits IDataControllerLight(Of T2)
    Function ToEntity(ByVal dataitem As T2, ByRef entityitem As T1) As T2 'returns dataitem
    Function ToData(ByVal entityitem As T1) As T2 'returns dataitem

    Property DataContext As IDataContext(Of T1)
    Property AutoRefresh As Boolean
    Property AutoRefreshWrapper As AutoRefreshWrapper(Of T1)

End Interface

Public Interface IDataControllerLight(Of T2 As IHasID) 'T2 = dataitem
    Function CreateNewItem(ByVal dataitem As T2) As T2 'returns dataitem
    Function CreateNewItems(ByVal dataitem As IEnumerable(Of T2)) As Boolean 'returns dataitem
    Function GetItem(ByVal id As Object) As T2 'returns dataitem
    Function GetAllItems() As IEnumerable(Of T2) 'returns dataitems
    Function GetItems(ByVal parameters As Object) As IEnumerable(Of T2) 'returns dataitems
    Function UpdateItem(ByVal dataitem As T2) As T2 'returns dataitem
    Function DeleteItem(ByVal dataitem As T2) As Boolean
    Function DeleteItems(ByVal list As IEnumerable(Of T2)) As Boolean

    Event CollectionChanged(e As SqlNotificationEventArgs)
End Interface