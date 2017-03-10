Imports System.Data.Entity

Public MustInherit Class DataControllerBase(Of entityclass As IHasID, dataclass As IHasID, datacontextclass As IDataContext(Of entityclass))
    Implements IDataController(Of entityclass, dataclass)

#Region "Properties"
    Public Property DataContext As IDataContext(Of entityclass) Implements IDataController(Of entityclass, dataclass).DataContext
#End Region

#Region "Constructor"
    Public Sub New()
        DataContext = GetInstance(GetType(datacontextclass))
    End Sub
    Public Sub New(ByRef context As datacontextclass)
        DataContext = context
    End Sub
#End Region

#Region "Crud"
    Public Function CreateNewItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass).CreateNewItem
        DataContext = GetInstance(GetType(datacontextclass))
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
    ''' <returns></returns>
    Public Overridable Function GetAllItems() As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass).GetAllItems
        DataContext = GetInstance(GetType(datacontextclass))
        Return EntitiesToItems(DataContext.GetAllObjects)
    End Function
    Public Function GetItems(parameters As String) As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass).GetItems
        DataContext = GetInstance(GetType(datacontextclass))
        Return EntitiesToItems(DataContext.GetObjects(parameters))
    End Function
    Protected Function EntitiesToItems(ByRef entities As IEnumerable(Of entityclass)) As IEnumerable(Of dataclass)
        Dim list As New List(Of dataclass)
        For Each entityitem In entities
            Dim newdataitem As dataclass = ToData(entityitem)
            list.Add(newdataitem)
        Next
        Return list
    End Function
    Public Function GetItem(id As Integer) As dataclass Implements IDataController(Of entityclass, dataclass).GetItem
        DataContext = GetInstance(GetType(datacontextclass))
        Dim newdataitem As dataclass = ToData(DataContext.GetObject(id))
        Return newdataitem
    End Function
    Public Function UpdateItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass).UpdateItem
        DataContext = GetInstance(GetType(datacontextclass))
        Dim entityitem = DataContext.GetObject(dataitem.ID)
        Dim newdataitem = ToEntity(dataitem, entityitem)
        If DataContext.Save() Then
            Return newdataitem
        Else
            Return Nothing
        End If
    End Function
    Public Function DeleteItem(dataitem As dataclass) As Boolean Implements IDataController(Of entityclass, dataclass).DeleteItem
        DataContext = GetInstance(GetType(datacontextclass))
        If DataContext.DeleteObject(dataitem.ID) = True Then
            Return DataContext.Save()
        End If
        Return False
    End Function
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
    Public Function ToEntity(dataitem As dataclass, ByRef entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass).ToEntity
        MapProperties(dataitem, entityitem)
        Return dataitem
    End Function
#End Region
End Class