Imports System.Windows.Forms

Public Class HynrCheckbox
    Inherits CheckBox

#Region "Properties"
    Private LazyBindingViewModel As IViewModelBase
    Private LazyBindingDisplayProperty As String
    Private LazyBindingDataMember As String

    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            If Not value Then
                Me.Enabled = True
            Else
                Me.Enabled = False
            End If
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
    End Sub
    Public Sub BindToViewModel(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String)
        PrepareBinding(viewmodel, displayproperty)
        DataBindings.Add("Checked", LazyBindingViewModel, LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
    End Sub
    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase, ByVal displayproperty As String)
        PrepareBinding(listviewmodel, displayproperty)
        LazyBindingDataMember = "SelectedItem."
        AddHandler listviewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        If Not IsNothing(DataBindings.Control) Then
            Dim item = DataBindings.Item("Checked")
            Try
                If Not IsNothing(item) Then DataBindings.Remove(item)
                DataBindings.Add("Checked", LazyBindingViewModel, LazyBindingDataMember & LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
            Catch
                'i really dont get it
            End Try
        End If
    End Sub
    Private Sub PrepareBinding(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String)
        LazyBindingDisplayProperty = displayproperty
        LazyBindingViewModel = viewmodel
        If IsNothing(DataBindings.Item("IsBusy")) Then DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
    End Sub
#End Region

#Region "Events"
#End Region

End Class
