Imports System.Windows.Forms

Public Class HynrToolStrip
    Inherits ToolStrip
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
    Public Sub New()
        MyBase.New
    End Sub

    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        RenderMode = ToolStripRenderMode.System
        GripStyle = ToolStripGripStyle.Hidden
    End Sub
#End Region
End Class
