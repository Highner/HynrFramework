Public Class HynrLvPieChart
    Inherits LiveCharts.WinForms.PieChart
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

#End Region


#Region "Methods"
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
    End Sub
#End Region

#Region "Events"
#End Region
End Class
