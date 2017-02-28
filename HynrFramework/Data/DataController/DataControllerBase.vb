Imports System.Data.Entity

Public Class DataControllerBase(Of entityclass As IHasID, dataclass As IHasID, datacontextclass As IDataContext(Of entityclass, dbcontextclass), dbcontextclass As DbContext)
    Implements IDataController(Of entityclass, dataclass, dbcontextclass)
#Region "PROPERTIES"
    Public Property DataContext As IDataContext(Of entityclass, dbcontextclass) Implements IDataController(Of entityclass, dataclass, dbcontextclass).DataContext
#End Region

#Region "METHODS"
    Public Sub New()
        DataContext = GetInstance(GetType(datacontextclass))
    End Sub
    Public Sub New(ByRef context As datacontextclass)
        DataContext = context
    End Sub
#Region "CRUD"
    Public Function CreateNewItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass, dbcontextclass).CreateNewItem
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
    ''' override this with a call to GetItems(parameters as string) if not ALL items are to be loaded
    ''' </summary>
    ''' <returns></returns>
    Public Overridable Function GetAllItems() As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass, dbcontextclass).GetAllItems
        Dim list As New List(Of dataclass)
        For Each entityitem In DataContext.GetAllObjects()
            Dim newdataitem As dataclass = ToData(entityitem)
            list.Add(newdataitem)
        Next
        Return list
    End Function
    Public Function GetItems(parameters As String) As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass, dbcontextclass).GetItems
        Dim list As New List(Of dataclass)
        For Each entityitem In DataContext.GetObjects(parameters)
            Dim newdataitem As dataclass = ToData(entityitem)
            list.Add(newdataitem)
        Next
        Return list
    End Function
    Public Function GetItem(id As Integer) As dataclass Implements IDataController(Of entityclass, dataclass, dbcontextclass).GetItem
        Dim newdataitem As dataclass = ToData(DataContext.GetObject(id))
        Return newdataitem
    End Function
    Public Function UpdateItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass, dbcontextclass).UpdateItem
        Dim entityitem = DataContext.GetObject(dataitem.ID)
        Dim newdataitem = ToEntity(dataitem, entityitem)
        If DataContext.Save() Then
            Return newdataitem
        Else
            Return Nothing
        End If
    End Function
    Public Function DeleteItem(dataitem As dataclass) As Boolean Implements IDataController(Of entityclass, dataclass, dbcontextclass).DeleteItem
        If DataContext.DeleteObject(dataitem.ID) = True Then
            Return DataContext.Save()
        End If
        Return False
    End Function
#End Region

#Region "DATA MAPPING"
    ''' <summary>
    ''' override this to fill custom properties etc., but include call to mybase.ToData to map the base properties.
    ''' in case of performance issues, possibly better to map manually and exclude call to MapProperties!
    ''' </summary>
    Public Overridable Function ToData(entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass, dbcontextclass).ToData
        Dim newdataitem As Object
        newdataitem = GetInstance(GetType(dataclass))
        MapProperties(entityitem, newdataitem)
        Return newdataitem
    End Function
    ''' <summary>
    ''' override this to fill custom properties in child entities etc., but include call to mybase.ToEntity to map the base properties.
    ''' in case of performance issues, possibly better to map manually and exclude call to MapProperties!
    ''' </summary>
    Public Function ToEntity(dataitem As dataclass, ByRef entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass, dbcontextclass).ToEntity
        MapProperties(dataitem, entityitem)
        Return dataitem
    End Function
#End Region
#End Region
End Class
