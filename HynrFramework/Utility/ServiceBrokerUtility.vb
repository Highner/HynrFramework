Imports System.Data.Entity.Core.Objects
Imports System.Data.SqlClient
Imports System.Runtime.Remoting.Messaging
Imports System.Configuration

Public Module ServiceBrokerUtility
    'Private Sub New()
    'End Sub
    Private connectionStrings As New List(Of String)()
    Private Const sqlDependencyCookie As String = "MS.SqlDependencyCookie"
    Private ctx As ObjectContext
    Private refreshMode As RefreshMode
    Private ReadOnly collections As New Dictionary(Of String, IEnumerable)()

    <System.Runtime.CompilerServices.Extension>
    Public Sub AutoRefresh(ctx As ObjectContext, refreshMode As RefreshMode, collection As IEnumerable)
        Dim csInEF = ctx.Connection.ConnectionString
        Dim csName = csInEF.Replace("name=", "").Trim()
        Dim csForEF = ConfigurationManager.ConnectionStrings(csName).ConnectionString
        Dim newConnectionString = New System.Data.EntityClient.EntityConnectionStringBuilder(csForEF).ProviderConnectionString
        If Not connectionStrings.Contains(newConnectionString) Then
            connectionStrings.Add(newConnectionString)
            SqlDependency.Start(newConnectionString)
        End If
        ServiceBrokerUtility.ctx = ctx
        ServiceBrokerUtility.refreshMode = refreshMode
        AutoRefresh(collection)
    End Sub

    Public Sub AutoRefresh(collection As IEnumerable)
        Dim oldCookie = CallContext.GetData(sqlDependencyCookie)
        Try
            Dim dependency = New SqlDependency()
            collections.Add(dependency.Id, collection)
            CallContext.SetData(sqlDependencyCookie, dependency.Id)
            AddHandler dependency.OnChange, AddressOf dependency_OnChange
            ctx.Refresh(refreshMode, collection)
        Finally
            CallContext.SetData(sqlDependencyCookie, oldCookie)
        End Try
    End Sub

    Private Sub dependency_OnChange(sender As Object, e As SqlNotificationEventArgs)
        If e.Info = SqlNotificationInfo.Invalid Then
            Debug.Print("SqlNotification:  A statement was provided that cannot be notified.")
            Return
        End If
        Try
            Dim id = DirectCast(sender, SqlDependency).Id
            Dim collection As IEnumerable
            If collections.TryGetValue(id, collection) Then
                collections.Remove(id)
                AutoRefresh(collection)
                Dim notifyRefresh = TryCast(collection, INotifyRefresh)
                If notifyRefresh IsNot Nothing Then
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(DirectCast(notifyRefresh.OnRefresh, Action))
                End If
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.Print("Error in OnChange: {0}", ex.Message)
        End Try
    End Sub
End Module

