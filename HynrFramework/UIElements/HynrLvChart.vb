﻿Imports System.ComponentModel
Imports HynrFramework

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
    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            ToggleBusyIndicator(_IsBusy)
            OnPropertyChanged("IsBusy", value)
        End Set
    End Property
    Private BusyIndicator As New MatrixCircularProgressControl
#End Region


#Region "Methods"
    Public Sub New()
        MyBase.New
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
    End Sub
    Private Sub ToggleBusyIndicator(ByVal busy As Boolean)
        Enabled = Not busy
        If busy Then
            Me.Visible = False
            BusyIndicator.StartAngle = 30
            BusyIndicator.Show()
            BusyIndicator.Start()

        Else
            BusyIndicator.Stop()
            BusyIndicator.Hide()
            Me.Visible = True
        End If
    End Sub
    Private Sub AdjustBusyIndicatorLocation() Handles Me.SizeChanged
        Dim x As Integer = Me.Width / 2 - BusyIndicator.Width / 2
        Dim y As Integer = Me.Height / 2 - BusyIndicator.Height / 2
        BusyIndicator.Location = New Drawing.Point(x, y)
    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        BackColor = HynrSettings.GridColor
    End Sub
#End Region

#Region "Events"
#End Region
End Class