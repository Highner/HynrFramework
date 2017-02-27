Imports System.ComponentModel
Imports System.Threading

Public Class ViewModelBase
    Implements INotifyPropertyChanged

    Private _Cts As CancellationTokenSource
    Public Property CancellationSource As CancellationTokenSource
        Get
            Return _Cts
        End Get
        Set(value As CancellationTokenSource)
            _Cts = value
            OnPropertyChanged("CancellationSource")
        End Set
    End Property
    Private Property _IsBusy As Boolean
    Public Property IsBusy As Boolean
        Get
            Return _IsBusy
        End Get
        Set(value As Boolean)
            _IsBusy = value
            OnPropertyChanged("IsBusy")
        End Set
    End Property

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
End Class
