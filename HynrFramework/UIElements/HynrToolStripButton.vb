﻿Imports System.ComponentModel
Imports System.Windows.Forms
Imports HynrFramework

Public Class HynrToolStripButton
    Inherits ToolStripButton
    Implements IBindableComponent

#Region "Properties"
    Private _dataBindings As ControlBindingsCollection
    Private _bindingContext As BindingContext
    Public ReadOnly Property DataBindings() As ControlBindingsCollection Implements IBindableComponent.DataBindings
        Get
            If _dataBindings Is Nothing Then
                _dataBindings = New ControlBindingsCollection(Me)
            End If
            Return _dataBindings
        End Get
    End Property
    Public Property BindingContext() As BindingContext Implements IBindableComponent.BindingContext
        Get
            If _bindingContext Is Nothing Then
                _bindingContext = New BindingContext()
            End If
            Return _bindingContext
        End Get
        Set
            _bindingContext = Value
        End Set
    End Property
    Private _IsBusy As Boolean
    Public Property IsBusy As Boolean
        Get
            Return _IsBusy
        End Get
        Set(value As Boolean)
            _IsBusy = value
            ToggleIsBusy(value)
        End Set
    End Property
    Private Command As Command
    Private ViewModel As IViewModelBase
#End Region

#Region "Methods"
    Public Sub BindToViewModel(viewmodel As IViewModelBase, command As ICommand, Optional ByVal text As String = "", Optional ByVal imagepropertyname As String = "")
        BindToViewModelCommand(viewmodel, command, text, imagepropertyname)
        DataBindings.Add("IsBusy", Me.ViewModel, "IsBusy", True, DataSourceUpdateMode.Never)
    End Sub
    Public Sub BindToViewModelCommand(viewmodel As IViewModelBase, command As ICommand, Optional ByVal text As String = "", Optional ByVal imagepropertyname As String = "")
        Me.Command = command
        Me.ViewModel = viewmodel
        Me.ToolTipText = text
        If imagepropertyname <> "" Then DataBindings.Add("Image", Me.ViewModel, imagepropertyname, True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub
    Private Sub ExecuteCommand() Handles Me.Click
        If Not IsNothing(Command) Then Command.Execute(Nothing)
    End Sub
    Private Sub ToggleIsBusy(ByVal busy As Boolean)
        If busy Then
            Me.Enabled = False
        Else
            Me.Enabled = True
        End If
    End Sub
#End Region
End Class
