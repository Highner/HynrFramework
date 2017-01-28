Imports System.Windows.Forms


Public Class HynrLabelStrip
    Inherits ToolStrip
    Implements IHasHynrSettings

#Region "PROPERTIES"

    Private Property _Label As New ToolStripLabel
    Public Property LabelText As String
        Get
            Return _Label.Text
        End Get
        Set(value As String)
            _Label.Text = value
        End Set
    End Property
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

#Region "METHODS"
    Public Sub New()
        MyBase.New()
        Items.Add(_Label)
    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        BackColor = HynrSettings.CompanyColor
        _Label.ForeColor = HynrSettings.LabelstripColor
    End Sub
#End Region

#Region "EVENTS"

#End Region
End Class
