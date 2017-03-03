Imports System.ComponentModel
Imports System.Threading
Imports HynrFramework

<Serializable>
Public MustInherit Class ViewModelBase
    Implements INotifyPropertyChanged
    Implements IViewModelBase

    Private _Cts As CancellationTokenSource
    <Browsable(False)>
    Public Property CancellationSource As CancellationTokenSource
        Get
            Return _Cts
        End Get
        Set(value As CancellationTokenSource)
            _Cts = value
            OnPropertyChanged("CancellationSource")
        End Set
    End Property
    Private Property _IsBusy As Boolean = False
    <Browsable(False)>
    Public Property IsBusy As Boolean Implements IViewModelBase.IsBusy
        Get
            Return _IsBusy
        End Get
        Set(value As Boolean)
            If Not (_IsBusy = value) Then
                _IsBusy = value
                OnPropertyChanged("IsBusy")
            End If
        End Set
    End Property

    Protected Sub RaiseLoadingCompleted()
        RaiseEvent LoadingCompleted()
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
End Class
