Imports System.Data.Entity

Public Class DataControllerBase(Of entityclass As IHasID, dataclass As IHasID, datacontextclass As IDataContext(Of entityclass))
    Implements IDataController(Of entityclass, dataclass)
#Region "PROPERTIES"
    Protected DataContext As datacontextclass
    Private KeepEntityProperties As Boolean = False
#End Region

#Region "METHODS"
    Public Sub New(ByRef context As datacontextclass)
        DataContext = context
    End Sub

#Region "CRUD"
    Public Function CreateNewItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass).CreateNewItem
        Dim newentityitem As entityclass = GetInstance(GetType(entityclass))
        ToEntity(dataitem, newentityitem)
        If DataContext.AddObject(newentityitem) = True Then
            DataContext.Save()
            dataitem.ID = newentityitem.ID
        Else
            Throw New Exception
        End If
        Return GetItem(dataitem.ID)
    End Function
    Public Function GetAllItems() As IEnumerable(Of dataclass) Implements IDataController(Of entityclass, dataclass).GetAllItems
        Dim list As New List(Of dataclass)
        For Each entityitem In DataContext.GetAllObjects
            Dim newdataitem As dataclass = ToData(entityitem)
            list.Add(newdataitem)
        Next
        Return list
    End Function
    Public Function GetItem(id As Integer) As dataclass Implements IDataController(Of entityclass, dataclass).GetItem
        Dim newdataitem As dataclass = ToData(DataContext.GetObject(id))
        Return newdataitem
    End Function
    Public Function UpdateItem(dataitem As dataclass) As dataclass Implements IDataController(Of entityclass, dataclass).UpdateItem
        Dim entityitem = DataContext.GetObject(dataitem.ID)
        Dim newdataitem = ToEntity(dataitem, entityitem)
        DataContext.Save()
        Return newdataitem
    End Function
    Public Function DeleteItem(dataitem As dataclass) As Boolean Implements IDataController(Of entityclass, dataclass).DeleteItem
        If DataContext.DeleteObject(dataitem.ID) = True Then
            DataContext.Save()
            Return True
        End If
        Return False
    End Function
#End Region

#Region "DATA MAPPING"
    'override this to fill custom properties etc., but include call to mybase.ToData to map the base properties. set KeepEntityProperties to false in that case
    Public Overridable Function ToData(entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass).ToData
        Dim newdataitem As Object
        'If KeepEntityProperties = True Then
        'TODO: extend base dataclass with entity properties
        ' newdataitem = New DynamicExtension(Of DataCarriersBase)().ExtendWith(Of entity)
        'Else
        newdataitem = GetInstance(GetType(dataclass))
        'End If
        MapProperties(entityitem, newdataitem)
        Return newdataitem
    End Function
    'override this to fill custom properties in child entities etc., but include call to mybase.ToEntity to map the base properties
    Public Function ToEntity(dataitem As dataclass, ByRef entityitem As entityclass) As dataclass Implements IDataController(Of entityclass, dataclass).ToEntity
        MapProperties(dataitem, entityitem)
        Return dataitem
    End Function
#End Region
#End Region
End Class
