Imports System.Collections.Specialized

Public Class HynrLvChart
    Inherits LiveCharts.WinForms.CartesianChart
    Implements IHasHynrSettings


#Region "Properties"
    Private _HynrSettings As HynrUISettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            If Not IsNothing(value) Then
                _HynrSettings = value
                ApplyHynrSettings()
            End If
        End Set
    End Property
    WithEvents ChartSeries As LiveCharts.SeriesCollection
    Public ReadOnly Property HasSeries As Boolean
        Get
            Return ChartSeries.Any
        End Get
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        ChartSeries = Series
    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
    End Sub
    Private Sub SeriesAdded(sender As Object, e As NotifyCollectionChangedEventArgs) Handles ChartSeries.CollectionChanged
        If e.Action = NotifyCollectionChangedAction.Add Or e.Action = NotifyCollectionChangedAction.Remove Then RaiseEvent Activated()
    End Sub
#End Region

#Region "Events"
    Public Event Activated()
#End Region
End Class
