Imports System.ComponentModel
Imports System.Threading
Imports System.Windows.Input
Imports HynrFramework

<Serializable>
Public MustInherit Class ViewModelBase
    Implements INotifyPropertyChanged
    Implements IViewModelBase

    <Browsable(False)>
    Public Property RefreshAllCommand As ICommand = New Command(AddressOf GetData)

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
                OnPropertyChanged("IsNotBusy")
            End If
        End Set
    End Property
    <Browsable(False)>
    Public ReadOnly Property IsNotBusy As Boolean
        Get
            Return Not _IsBusy
        End Get
    End Property
    ''' <summary>
    ''' override to get base data. fires when RefreshAllCommand is called.
    ''' </summary>
    Protected Overridable Sub GetData()
    End Sub
    Protected Sub RaiseLoadingCompleted()
        RaiseEvent LoadingCompleted()
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Public Sub AllPropertiesChanged()
        For Each prop In Me.[GetType]().GetProperties()
            OnPropertyChanged(prop.Name)
        Next
    End Sub

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
End Class
