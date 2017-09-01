Imports System.Windows.Forms

Public Class HynrButton
    Inherits Button
    Implements IBindableComponent

#Region "Properties"
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
    Public Sub BindToViewModel(ByRef viewmodel As IViewModelBase, ByRef command As Command, Optional ByVal text As String = "")
        BindToViewModelCommandOnly(viewmodel, command, text)
        DataBindings.Add("IsBusy", Me.ViewModel, "IsBusy", True, DataSourceUpdateMode.Never)
    End Sub
    Public Sub BindToViewModelCommandOnly(ByRef viewmodel As IViewModelBase, ByRef command As Command, Optional ByVal text As String = "")
        Me.Command = command
        Me.ViewModel = viewmodel
        Me.Text = text
    End Sub
    Private Sub ExecuteCommand() Handles Me.Click
        If Not IsNothing(Command) Then Command.Execute(Nothing)
    End Sub
    Private Sub ToggleIsBusy(ByVal busy As Boolean)
        Enabled = Not busy
    End Sub
#End Region
End Class
