Imports System.ComponentModel
Imports System.Threading
Imports System.Windows.Input
Imports HynrFramework

<Serializable>
Public MustInherit Class ViewModelBase
    Implements INotifyPropertyChanged
    Implements IViewModelBase

#Region "Commands"
    <Browsable(False)>
    Public Property RefreshAllCommand As ICommand = New Command(AddressOf GetData)
    <Browsable(False)>
    Public Overridable Property RefreshAllAsyncCommand As ICommand = New Command(AddressOf ExecuteGetData)
#End Region

#Region "Properties"
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
    Private _DataLoaded As Boolean = False
    <Browsable(False)>
    Public Property DataLoaded() As Boolean
        Get
            Return _DataLoaded
        End Get
        Set(ByVal value As Boolean)
            _DataLoaded = value
            OnPropertyChanged("DataLoaded")
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' override to get base data. fires when RefreshAllCommand is executed.
    ''' </summary>
    Protected Overridable Sub GetData()
    End Sub
    Private Async Sub ExecuteGetData()
        DataLoaded = False
        IsBusy = True
        CancellationSource = New Threading.CancellationTokenSource
        Await Task.Run(Sub() GetData(), CancellationSource.Token)
        IsBusy = False
        DataLoaded = True
    End Sub
    Public Sub RaiseLoadingCompleted()
        RaiseEvent LoadingCompleted()
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Public Sub AllPropertiesChanged()
        For Each prop In Me.[GetType]().GetProperties()
            OnPropertyChanged(prop.Name)
        Next
    End Sub
    Protected Async Sub ExecuteAsyncSub(methods As Action(), raiseloadingcompleted As Boolean)
        IsBusy = True
        For Each method In methods
            Await Task.Run(Sub() method())
        Next
        If raiseloadingcompleted Then Me.RaiseLoadingCompleted()
        IsBusy = False
    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
#End Region
End Class
