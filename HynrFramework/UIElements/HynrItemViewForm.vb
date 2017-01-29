Imports System.ComponentModel
Imports System.Windows.Forms

Public Class HynrItemViewForm
    Inherits HynrForm
    Implements IBindableControl

    Private _DataContext As Object
    Public Property DataContext As Object Implements IBindableControl.DataContext
        Get
            Return _DataContext
        End Get
        Set(value As Object)
            _DataContext = value
            OnPropertyChanged("SelectedItem")
        End Set
    End Property

    Public Sub New(ByRef settings As HynrUISettings, ByRef datacontext As Object)
        MyBase.New(settings)
        _DataContext = datacontext
    End Sub

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
