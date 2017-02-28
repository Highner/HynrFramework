Imports System.Windows.Forms

Public Class HynrTextBox
    Inherits TextBox
#Region "Properties"
    Private LazyBindingViewModel As IViewModelBase
    Private LazyBindingUpdateMode As DataSourceUpdateMode
    Private LazyBindingDisplayProperty As String
    Private LazyBindingDataMember As String
#End Region

#Region "Methods"
    Public Sub BindToProperty(ByRef viewmodel As IViewModelBase, ByVal propertyname As String, Optional ByVal updatemode As DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged)
        If viewmodel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = propertyname).Any Then
            DataBindings.Add("Text", viewmodel, propertyname, True, updatemode, String.Empty)
        End If
    End Sub
    Public Sub BindToProperty(ByRef viewmodel As IViewModelBase, ByVal datamember As String, ByVal displayproperty As String, Optional ByVal updatemode As DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged)
        LazyBindingDisplayProperty = displayproperty
        LazyBindingUpdateMode = updatemode
        LazyBindingViewModel = viewmodel
        LazyBindingDataMember = datamember
        AddHandler viewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        If LazyBindingViewModel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = LazyBindingDataMember).Any Then
            DataBindings.Add("Text", LazyBindingViewModel, LazyBindingDataMember & "." & LazyBindingDisplayProperty, True, LazyBindingUpdateMode, String.Empty)
        End If
    End Sub
#End Region
End Class