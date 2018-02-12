
Imports System.Data.SqlClient

Interface INotifyRefresh
    'Inherits Specialized.INotifyCollectionChanged

    Sub OnRefresh(e As SqlNotificationEventArgs)
End Interface