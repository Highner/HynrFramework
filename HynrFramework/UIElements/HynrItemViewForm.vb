Imports System.ComponentModel
Imports System.Windows.Forms

Public Class HynrItemViewForm(Of dataitem, viewmodelitem As IViewModelItem(Of dataitem))
    Inherits HynrForm
    Implements INotifyPropertyChanged


    Private _ItemViewModel As IViewModelItem(Of dataitem)
    Public Property SelectedItem As IViewModelItem(Of dataitem)
        Get
            Return _ItemViewModel
        End Get
        Set(value As IViewModelItem(Of dataitem))
            _ItemViewModel = value
            OnPropertyChanged("SelectedItem")
        End Set
    End Property

    Public Sub New(ByRef settings As HynrUISettings, ByRef datacontext As Object)
        MyBase.New(settings)
        DataBindings.Add("SelectedItem", datacontext, "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
