﻿Imports System.Data.Entity

Public Interface IDataController(Of T1 As IHasID, T2 As IHasID) 'T1 = entityitem, T2 = dataitem
    Function CreateNewItem(ByVal dataitem As T2) As T2 'returns dataitem
    Function GetItem(ByVal id As Object) As T2 'returns dataitem
    Function GetAllItems() As IEnumerable(Of T2) 'returns dataitems
    Function GetItems(ByVal parameters As Object) As IEnumerable(Of T2) 'returns dataitems
    Function UpdateItem(ByVal dataitem As T2) As T2 'returns dataitem
    Function DeleteItem(ByVal dataitem As T2) As Boolean

    Function ToEntity(ByVal dataitem As T2, ByRef entityitem As T1) As T2 'returns dataitem
    Function ToData(ByVal entityitem As T1) As T2 'returns dataitem

    Property DataContext As IDataContext(Of T1)

End Interface
