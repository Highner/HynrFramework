
Imports System.Collections.Specialized
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity
Imports HynrFramework
Imports System.Data.Entity.Core.Objects.DataClasses

Public Class AutoRefreshWrapper(Of T)
    Implements IEnumerable(Of T)
    Implements INotifyRefresh

    Private objectQuery As IEnumerable(Of T)

    Public Sub New(ByVal objectQuery As ObjectQuery(Of T), ByVal refreshMode As RefreshMode)
        Me.objectQuery = objectQuery
        objectQuery.Context.AutoRefresh(refreshMode, Me)
    End Sub

    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Return objectQuery.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return GetEnumerator()
    End Function

    Public Sub OnRefresh() Implements INotifyRefresh.OnRefresh
        Try

            RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
        Catch ex As Exception
            System.Diagnostics.Debug.Print("Error in OnRefresh: {0}", ex.Message)
        End Try
    End Sub

    Public Event CollectionChanged As NotifyCollectionChangedEventHandler Implements INotifyCollectionChanged.CollectionChanged
End Class