Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Windows.Input
Imports HynrFramework

<Serializable>
Public MustInherit Class ItemViewModelBase(Of dataclass As IHasID) ', dbcontextclass As DbContext
    Inherits ViewModelBase
    Implements IItemViewModel(Of dataclass)

#Region "Commands"
    <Browsable(False)>
    Public Property DeleteCommand As ICommand = New Command(AddressOf RaiseDeletedEvent) Implements IItemViewModel(Of dataclass).DeleteCommand
    <Browsable(False)>
    Public Property UpdateCommand As ICommand = New Command(AddressOf RaiseUpdatedEvent) Implements IItemViewModel(Of dataclass).UpdateCommand
#End Region

#Region "Properties"
    Private Property _Data As dataclass
    <Browsable(False)>
    Public Property Data As dataclass Implements IItemViewModel(Of dataclass).Data
        Get
            Return _Data
        End Get
        Set(value As dataclass)
            _Data = value
            _OriginalData = GetInstance(GetType(dataclass))
            MapProperties(_Data, _OriginalData)
            If CanSave Then
                CanSave = False
                RaiseEvent CanSaveChanged(Me, Nothing)
            End If
        End Set
    End Property
    Private Property _OriginalData As dataclass
    <Browsable(False)>
    Public Property ID As Integer Implements IHasID.ID
        Get
            Return Data.ID
        End Get
        Set(value As Integer)
            Data.ID = value
        End Set
    End Property
    'Private Property DataContext As dbcontextclass
    <Browsable(False)>
    Public Property CanSave As Boolean = False Implements IItemViewModel(Of dataclass).CanSave
#End Region

#Region "Constructor"
    ''' <summary>
    ''' no parameter allowed! set GetDataOnLoad in inheriting class
    ''' </summary>
    Public Sub New()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' in case child lists need to be updated. insert every child listviewmodels getdata() here.
    ''' </summary>
    Protected Overridable Sub _GetData()
    End Sub
    Public Async Sub GetData()
        If Not IsBusy Then
            IsBusy = True
        End If
        Await Task.Run(Sub() _GetData())
        IsBusy = False
    End Sub
    Private Sub RaiseDeletedEvent()
        RaiseEvent Deleted(Me, Nothing)
    End Sub
    Private Sub RaiseUpdatedEvent()
        RaiseEvent Updated(Me, Nothing)
    End Sub
    Private Sub ValueChanged() Handles Me.PropertyChanged
        If Not IsBusy Then
            Dim can As Boolean = CanSave
            CanSave = Not EqualValues(_OriginalData, Data)
            If Not can = CanSave Then RaiseEvent CanSaveChanged(Me, Nothing)
        End If
    End Sub
#End Region

#Region "Events"
    Public Event Deleted As IItemViewModel(Of dataclass).DeletedEventHandler Implements IItemViewModel(Of dataclass).Deleted
    Public Event Updated As IItemViewModel(Of dataclass).UpdatedEventHandler Implements IItemViewModel(Of dataclass).Updated
    Public Event CanSaveChanged As IItemViewModel(Of dataclass).CanSaveChangedEventHandler Implements IItemViewModel(Of dataclass).CanSaveChanged
#End Region
End Class