Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports HynrFramework
Imports System.Data.SqlClient
Imports System.Data.Entity.Core.Objects

Public Class DataControllerBase(Of entityclass As IHasID, dataclass As IHasID, datacontextclass As IDataContext(Of entityclass))
    Implements IDataController(Of entityclass, dataclass)

#Region "Properties"
    Public Property DataContext As IDataContext(Of entityclass) Implements IDataController(Of entityclass, dataclass).DataContext
#End Region

#Region "Constructor"
    Public Sub New()
        InitializeConnection()
    End Sub
    Public Sub New(ByRef context As datacontextclass)
        DataContext = context
    End Sub
#End Region

#Region "Crud"
    Public Overridable Function CreateNewItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass).CreateNewItem
        InitializeConnection()
        Dim newentityitem As entityclass = GetInstance(GetType(entityclass))
        ToEntity(dataitem, newentityitem)
        If DataContext.AddObject(newentityitem) = True Then
            If DataContext.Save() Then
                dataitem.ID = newentityitem.ID
                Return GetItem(dataitem.ID)
            End If
        End If
        Return Nothing
    End Function
    ''' <summary>
    ''' override this with a call to GetItems(parameters as string) or create custom call to datacontext and insert here if not ALL items are to be loaded
    ''' </summary>
    Public Overridable Function GetAllItems() As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass).GetAllItems
        InitializeConnection()
        Return EntitiesToItems(DataContext.GetAllObjects)
    End Function
    Public Overridable Function GetItems(parameters As Object) As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass).GetItems
        InitializeConnection()
        Return EntitiesToItems(DataContext.GetObjects(parameters))
    End Function
    Protected Function EntitiesToItems(ByRef entities As IEnumerable(Of entityclass)) As IEnumerable(Of dataclass)
        Dim list As New List(Of dataclass)
        For Each entityitem In entities
            Dim newdataitem As dataclass = ToData(entityitem)
            If Not IsNothing(newdataitem) Then list.Add(newdataitem)
        Next
        Return list
    End Function
    Public Overridable Function GetItem(id As Object) As dataclass Implements IDataController(Of entityclass, dataclass).GetItem
        InitializeConnection()
        Dim newdataitem As dataclass = ToData(DataContext.GetObject(id))
        Return newdataitem
    End Function
    Public Overridable Function UpdateItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass).UpdateItem
        InitializeConnection()
        Dim entityitem = DataContext.GetObject(dataitem.ID)
        If Not IsNothing(entityitem) Then
            Dim newdataitem = ToEntity(dataitem, entityitem)
            If DataContext.Save() Then
                Return newdataitem
            End If
        End If
        Return Nothing
    End Function
    Public Overridable Function DeleteItem(dataitem As dataclass) As Boolean Implements IDataController(Of entityclass, dataclass).DeleteItem
        InitializeConnection()
        If DataContext.DeleteObject(dataitem.ID) = True Then
            Return DataContext.Save()
        End If
        Return False
    End Function
    Public Overridable Function DeleteItems(list As IEnumerable(Of dataclass)) As Boolean Implements IDataController(Of entityclass, dataclass).DeleteItems
        For Each item In list
            If Not DeleteItem(item) Then Return False
        Next
        Return True
    End Function
    Protected Overridable Sub InitializeConnection()
        DataContext = Nothing
        DataContext = GetInstance(GetType(datacontextclass))
    End Sub
#End Region

#Region "Data Mapping"
    ''' <summary>
    ''' override this to fill custom properties etc., but include call to mybase.ToData to map the base properties.
    ''' in case of performance issues, possibly better to map manually and exclude call to MapProperties!
    ''' </summary>
    Public Overridable Function ToData(entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass).ToData
        Dim newdataitem As Object
        newdataitem = GetInstance(GetType(dataclass))
        MapProperties(entityitem, newdataitem)
        Return newdataitem
    End Function

    ''' <summary>
    ''' override this to fill custom properties in child entities etc., but include call to mybase.ToEntity to map the base properties.
    ''' in case of performance issues, possibly better to map manually and exclude call to MapProperties!
    ''' </summary>
    Public Overridable Function ToEntity(dataitem As dataclass, ByRef entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass).ToEntity
        TrimStrings(dataitem)
        MapProperties(dataitem, entityitem)
        Return dataitem
    End Function
#End Region

End Class