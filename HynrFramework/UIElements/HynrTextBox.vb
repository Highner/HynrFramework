Imports System.Windows.Forms

Public Class HynrTextBox
    Inherits TextBox
#Region "Properties"
    Private LazyBindingViewModel As IViewModelBase
    Private LazyBindingDisplayProperty As String
    Private LazyBindingDataMember As String
#End Region

#Region "Methods"
    Public Sub BindToProperty(ByRef viewmodel As IViewModelBase, ByVal propertyname As String)
        If viewmodel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = propertyname).Any Then
            DataBindings.Add("Text", viewmodel, propertyname, True, DataSourceUpdateMode.OnPropertyChanged, String.Empty)
        End If
    End Sub
    Public Sub BindToProperty(ByRef viewmodel As IViewModelBase, ByVal datamember As String, ByVal displayproperty As String)
        LazyBindingDisplayProperty = displayproperty
        LazyBindingViewModel = viewmodel
        LazyBindingDataMember = datamember
        AddHandler viewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        Dim propinfo = LazyBindingViewModel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = LazyBindingDataMember)
        If propinfo.Any Then
            Dim updatemode As DataSourceUpdateMode
            If propinfo.FirstOrDefault.CanWrite Then
                updatemode = DataSourceUpdateMode.OnPropertyChanged
            Else
                updatemode = DataSourceUpdateMode.Never
            End If
            DataBindings.Add("Text", LazyBindingViewModel, LazyBindingDataMember & "." & LazyBindingDisplayProperty, True, updatemode, String.Empty)
        End If
    End Sub
#End Region
End Class