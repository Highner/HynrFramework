﻿Imports System.Windows.Forms
Imports HynrFramework

Public Class HynrCardBase
    Implements IHasHynrSettings

#Region "Properties"
    Private _OldHeight As Integer
    Private _Collapsed As Boolean
    Public Property Collapsed As Boolean
        Get
            Return _Collapsed
        End Get
        Set(value As Boolean)
            _Collapsed = value
        End Set
    End Property
    Public Property CollapseButtonVisible() As Boolean
        Get
            Return CollapseButton.Visible
        End Get
        Set(ByVal value As Boolean)
            CollapseButton.Visible = value
            ToggleCollapseButtonIcon()
        End Set
    End Property

    Private _HynrSettings As IHasHynrSettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            _HynrSettings = value
            ApplyHynrSettings()
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        InitializeComponent()

    End Sub
    Overridable Sub SetBindings()

    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings

    End Sub
    Private Sub ToggleCollapseButtonIcon()

    End Sub
    Private Sub ToggleCollapsed()
        If Collapsed Then
            Height = _OldHeight
            Collapsed = False
        Else
            Dim remHeight As Integer = Height
            Height = ToolStrip.Height
            _OldHeight = remHeight
            Collapsed = True
        End If
        RaiseEvent CollapsedChanged(Me)
    End Sub
    Private Sub CollapseButton_Click(sender As Object, e As EventArgs) Handles CollapseButton.Click
        ToggleCollapsed()
    End Sub
    Private Sub HeightChanged() Handles Me.SizeChanged
        _OldHeight = Height
    End Sub

#End Region

#Region "Events"
    Public Event CollapsedChanged(ByRef card As HynrCardBase)
#End Region
End Class
