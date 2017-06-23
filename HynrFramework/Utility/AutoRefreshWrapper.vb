Imports System.Collections.Specialized
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Core.Objects.DataClasses
Imports HynrFramework

Public Class AutoRefreshWrapper(Of T As EntityObject)
    Implements IEnumerable(Of T)
    Implements INotifyRefresh
    Private objectQuery As IEnumerable(Of T)
    Public Sub New(objectQuery As ObjectQuery(Of T), refreshMode As RefreshMode)
        Me.objectQuery = objectQuery
        objectQuery.Context.AutoRefresh(refreshMode, Me)
    End Sub

    Public Function GetEnumerator() As IEnumerator(Of T)
        Return objectQuery.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me.GetEnumerator()
    End Function

    Public Function OnRefresh() As Object Implements INotifyRefresh.OnRefresh
        Try
            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
        Catch ex As Exception
            System.Diagnostics.Debug.Print("Error in OnRefresh: {0}", ex.Message)
        End Try
    End Function

    Private Function IEnumerable_GetEnumerator1() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Throw New NotImplementedException()
    End Function

    Public Event CollectionChanged As NotifyCollectionChangedEventHandler Implements INotifyCollectionChanged.CollectionChanged
End Class
