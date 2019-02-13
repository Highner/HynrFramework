Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports HynrFramework
Imports System.Data.SqlClient
Imports System.Data.Entity.Core.Objects
Imports System.Reflection

Public Class DataControllerBaseLight(Of dataclass As IHasID)
    Implements IDataControllerLight(Of dataclass)

#Region "Properties"

#End Region

#Region "Constructor"
    Public Sub New()
    End Sub
#End Region

#Region "Crud"
    Public Overridable Function CreateNewItem(dataitem As dataclass) As dataclass Implements IDataControllerLight(Of dataclass).CreateNewItem
    End Function
    ''' <summary>
    ''' override this with a call to GetItems(parameters as string) or create custom call to datacontext and insert here if not ALL items are to be loaded
    ''' </summary>
    Public Overridable Function GetAllItems() As IEnumerable(Of dataclass) Implements IDataControllerLight(Of dataclass).GetAllItems
    End Function
    Public Overridable Function GetItems(parameters As Object) As IEnumerable(Of dataclass) Implements IDataControllerLight(Of dataclass).GetItems
    End Function
    Public Overridable Function GetItem(id As Object) As dataclass Implements IDataControllerLight(Of dataclass).GetItem
    End Function
    Public Overridable Function UpdateItem(dataitem As dataclass) As dataclass Implements IDataControllerLight(Of dataclass).UpdateItem
    End Function
    Public Overridable Function DeleteItem(dataitem As dataclass) As Boolean Implements IDataControllerLight(Of dataclass).DeleteItem
    End Function
    Public Overridable Function DeleteItems(list As IEnumerable(Of dataclass)) As Boolean Implements IDataControllerLight(Of dataclass).DeleteItems
        For Each item In list
            If Not DeleteItem(item) Then Return False
        Next
        Return True
    End Function
#End Region


#Region "Events"
    Public Event CollectionChanged As IDataControllerLight(Of dataclass).CollectionChangedEventHandler Implements IDataControllerLight(Of dataclass).CollectionChanged
#End Region
End Class