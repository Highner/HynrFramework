Imports System.Data.Entity.Core.Objects
Imports System.Data.SqlClient
Imports System.Runtime.CompilerServices
Imports System.Runtime.Remoting.Messaging

Module ServiceBrokerUtility

    Private connectionStrings As List(Of String) = New List(Of String)()

    Private Const sqlDependencyCookie As String = "MS.SqlDependencyCookie"

    Private ctx As ObjectContext

    Private refreshMode As RefreshMode

    Private ReadOnly collections As Dictionary(Of String, IEnumerable) = New Dictionary(Of String, IEnumerable)()

    <Extension()>
    Sub AutoRefresh(ByVal ctx As ObjectContext, ByVal refreshMode As RefreshMode, ByVal collection As IEnumerable)
        'Dim csInEF = ctx.Connection.ConnectionString
        'Dim csName = csInEF.Replace("name=", "").Trim()
        'Dim csForEF = csInEF 'System.Configuration.ConfigurationManager.ConnectionStrings(csName).ConnectionString

        Dim newConnectionString = New EntityClient.EntityConnectionStringBuilder(ctx.Connection.ConnectionString).ProviderConnectionString
        If Not connectionStrings.Contains(newConnectionString) Then
            connectionStrings.Add(newConnectionString)

        End If
        SqlDependency.Start(newConnectionString)
        ServiceBrokerUtility.ctx = ctx
        ServiceBrokerUtility.refreshMode = refreshMode
        ApplyAutoRefresh(collection)
    End Sub

    Private Sub ApplyAutoRefresh(ByVal collection As IEnumerable)
        Dim oldCookie = CallContext.GetData(sqlDependencyCookie)
        Try
            Dim dependency = New SqlDependency()
            collections.Add(dependency.Id, collection)
            CallContext.SetData(sqlDependencyCookie, dependency.Id)
            AddHandler dependency.OnChange, AddressOf dependency_OnChange
            Try
                ctx.Refresh(refreshMode, collection)
            Catch ex As Exception
                Debug.Print(ex.Message)
            End Try
        Catch
            CallContext.SetData(sqlDependencyCookie, oldCookie)
        End Try
    End Sub

    Private Sub dependency_OnChange(ByVal sender As Object, ByVal e As SqlNotificationEventArgs)
        If e.Info = SqlNotificationInfo.Invalid Then
            Debug.Print("SqlNotification:  A statement was provided that cannot be notified.")
            Return
        End If
        Try
            Dim id = (CType(sender, SqlDependency)).Id
            Dim collection As IEnumerable
            If collections.TryGetValue(id, collection) Then
                collections.Remove(id)
                ApplyAutoRefresh(collection)
                Dim notifyRefresh = TryCast(collection, INotifyRefresh)
                'If notifyRefresh IsNot Nothing Then System.Windows.Application.Current.Dispatcher.BeginInvoke(notifyRefresh.OnRefresh)
                If notifyRefresh IsNot Nothing Then notifyRefresh.OnRefresh(e)
            End If
        Catch ex As Exception
            Debug.Print("Error in OnChange: {0}", ex.Message)
        End Try
    End Sub
End Module