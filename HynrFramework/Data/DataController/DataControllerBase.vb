Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports HynrFramework
Imports System.Data.SqlClient
Imports System.Data.Entity.Core.Objects
Imports System.Reflection

Public Class DataControllerBase(Of entityclass As IHasID, dataclass As IHasID, datacontextclass As IDataContext(Of entityclass))
    Implements IDataController(Of entityclass, dataclass)

#Region "Properties"
    Public Property DataContext As IDataContext(Of entityclass) Implements IDataController(Of entityclass, dataclass).DataContext

    'autorefresh
    Private _ObjectContext As ObjectContext
    Public Property AutoRefreshWrapper As AutoRefreshWrapper(Of entityclass) Implements IDataController(Of entityclass, dataclass).AutoRefreshWrapper
    Public Property AutoRefresh As Boolean = False Implements IDataController(Of entityclass, dataclass).AutoRefresh
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
                If IsActivity() Then SaveActivity(GetActivityLogCreateData(dataitem))
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
            If IsActivity() Then SaveActivity(GetActivityLogChangeData(dataitem, entityitem))
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
            If DataContext.Save() Then
                If IsActivity() Then SaveActivity(GetActivityLogDeleteData(dataitem))
                Return True
            End If
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

#Region "Activity Log"
    Public Overridable Function GetActivityLogChangeData(dataitem As dataclass, entityitem As entityclass) As ActivityData
        Dim activitylogdataitem = CreateActivityLogData(dataitem)
        If HasChangeMessage() Then
            Dim messagedataitem As IHasActivityLogChangeMessage = dataitem
            If messagedataitem.ChangeMessage = "" Then Return Nothing
            activitylogdataitem.ActivityMessage = messagedataitem.ChangeMessage
            messagedataitem.ChangeMessage = ""
        Else
            Dim changemessageitems As New List(Of String)
            For Each item In GetDifferingProperties(dataitem, entityitem)
                Dim newvalue As Object = item.GetValue(dataitem)
                If Not IsNothing(newvalue) Then
                    changemessageitems.Add(item.Name & " to " & newvalue.ToString)
                End If
            Next
            If Not changemessageitems.Any Then Return Nothing
            activitylogdataitem.ActivityMessage = "changed " & CreateStringListforText(changemessageitems, True) & " in " & GetActivityLogEntryName(dataitem)
        End If
        Return activitylogdataitem
    End Function
    Protected Overridable Function GetActivityLogCreateData(ByVal dataitem As dataclass) As ActivityData
        Dim activitylogdataitem = CreateActivityLogData(dataitem)
        activitylogdataitem.ActivityMessage = "created " & GetActivityLogEntryName(dataitem)
        Return activitylogdataitem
    End Function
    Protected Overridable Function GetActivityLogDeleteData(ByVal dataitem As dataclass) As ActivityData
        Dim activitylogdataitem = CreateActivityLogData(dataitem)
        activitylogdataitem.ActivityMessage = "deleted " & GetActivityLogEntryName(dataitem)
        Return activitylogdataitem
    End Function


    Protected Sub SaveActivity(activitydataitem As ActivityData)
        If Not IsNothing(activitydataitem) Then
            If GetType(datacontextclass).GetInterfaces().Contains(GetType(IHasActivityLog)) Then
                Dim context As IHasActivityLog = DataContext
                context.SaveActivity(activitydataitem)
            End If
        End If
    End Sub
    Protected Function IsActivity() As Boolean
        If GetType(dataclass).GetInterfaces().Contains(GetType(IHasActivityLogName)) Then Return True Else Return False
    End Function
    Protected Function HasChangeMessage() As Boolean
        If GetType(dataclass).GetInterfaces().Contains(GetType(IHasActivityLogChangeMessage)) Then Return True Else Return False
    End Function
    Protected Function GetActivityLogEntryName(ByVal dataitem As dataclass) As String
        Dim entity As IHasActivityLogName = dataitem
        Return entity.ActivityLogName
    End Function
    Protected Function CreateActivityLogData(ByVal dataitem As dataclass) As ActivityData
        Dim activitydataitem As New ActivityData
        activitydataitem.IsPublic = True
        activitydataitem.ObjectType = dataitem.GetType().FullName
        activitydataitem.ValueDate = Date.Today
        activitydataitem.ObjectID = dataitem.ID
        Return activitydataitem
    End Function
#End Region

#Region "Methods"
    Protected Function ToAutoRefresh(query As IQueryable(Of entityclass)) As IQueryable(Of entityclass)
        InitializeAutoRefresh(query)
        Return query
    End Function
    Private Sub InitializeAutoRefresh(query As IQueryable(Of entityclass))

        If Not IsNothing(_ObjectContext) Then _ObjectContext.Dispose()
        AutoRefreshWrapper = Nothing

        Dim internalQueryField = query.[GetType]().GetFields(BindingFlags.NonPublic Or BindingFlags.Instance).FirstOrDefault(Function(f) f.Name.Equals("_internalQuery"))
        Dim internalQuery = internalQueryField.GetValue(query)
        Dim objectQueryField = internalQuery.[GetType]().GetFields(BindingFlags.NonPublic Or BindingFlags.Instance).FirstOrDefault(Function(f) f.Name.Equals("_objectQuery"))
        Dim objectQuery = TryCast(objectQueryField.GetValue(internalQuery), ObjectQuery(Of entityclass))

        _ObjectContext = (CType(DataContext.GetSQLDBContext, IObjectContextAdapter)).ObjectContext
        AutoRefreshWrapper = New AutoRefreshWrapper(Of entityclass)(objectQuery, RefreshMode.StoreWins)
        'AddHandler AutoRefreshWrapper.CollectionChanged, AddressOf OnCollectionChanged
    End Sub
    'Private Sub OnCollectionChanged(e As SqlNotificationEventArgs)
    '    RaiseEvent CollectionChanged(e)
    'End Sub
#End Region

#Region "Data Mapping"
    ''' <summary>
    ''' override this to fill custom properties etc., but include call to mybase.ToData to map the base properties.
    ''' in case of performance issues, possibly better to map manually and exclude call to MapProperties!
    ''' can return "nothing" in case item not to be added to list.
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

#Region "Events"
    Public Event CollectionChanged As IDataController(Of entityclass, dataclass).CollectionChangedEventHandler Implements IDataController(Of entityclass, dataclass).CollectionChanged
#End Region
End Class