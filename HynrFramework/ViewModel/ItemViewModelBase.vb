Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Windows.Input
Imports HynrFramework

<Serializable>
Public Class ItemViewModelBase(Of dataclass As IHasID)
    Inherits ViewModelBase
    Implements IItemViewModel(Of dataclass)

#Region "Commands"
    <Browsable(False)>
    Public Property DeleteCommand As ICommand = New Command(AddressOf RaiseDeletedEvent) Implements IItemViewModel(Of dataclass).DeleteCommand
    <Browsable(False)>
    Public Property UpdateCommand As ICommand = New Command(AddressOf RaiseUpdatedEvent) Implements IItemViewModel(Of dataclass).UpdateCommand
    <Browsable(False)>
    Public Property DoubleClickCommand As ICommand = New Command(AddressOf RaiseDoubleClickedEvent) Implements IItemViewModel(Of dataclass).DoubleClickCommand
    <Browsable(False)>
    Public Property ClickCommand As ICommand = New Command(AddressOf RaiseClickedEvent) Implements IItemViewModel(Of dataclass).ClickCommand
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
    Public Property ID As Object Implements IItemViewModel(Of dataclass).ID
        Get
            Return Data.ID
        End Get
        Set(value As Object)
            Data.ID = value
        End Set
    End Property
    <Browsable(False)>
    Public Property CanSave As Boolean = False Implements IItemViewModel(Of dataclass).CanSave
    <Browsable(False)>
    Property _IsSelected As Boolean = False
    <Browsable(False)>
    Public Property IsSelected As Boolean
        Get
            Return _IsSelected
        End Get
        Set(value As Boolean)
            If Not value = _IsSelected Then
                _IsSelected = value
                OnPropertyChanged("IsSelected")
                RaiseEvent IsSelectedChanged(Me, Nothing)
            End If
        End Set
    End Property
    <Browsable(False)>
    Public Property SelectedCellIndex As Integer
#End Region

#Region "Constructor"
    ''' <summary>
    ''' no parameter allowed!
    ''' </summary>
    Public Sub New()
    End Sub
#End Region

#Region "Methods"
    Private Sub RaiseDeletedEvent()
        RaiseEvent Deleted(Me, Nothing)
    End Sub
    Private Sub RaiseUpdatedEvent()
        RaiseEvent Updated(Me, Nothing)
    End Sub
    Private Sub RaiseDoubleClickedEvent()
        RaiseEvent DoubleClicked(Me, SelectedCellIndex)
    End Sub
    Private Sub RaiseClickedEvent()
        RaiseEvent Clicked(Me, Nothing)
    End Sub
    Protected Sub RaiseCheckedChanged()
        RaiseEvent CheckedChanged(Me, Nothing)
    End Sub
    Protected Sub RaiseColorChanged()
        RaiseEvent ColorChanged(Me, Nothing)
    End Sub
    Private Sub ValueChanged() Handles Me.PropertyChanged
        If Not IsBusy Then
            Dim can As Boolean = CanSave
            CanSave = Not EqualValues(_OriginalData, Data)
            If Not can = CanSave Then RaiseEvent CanSaveChanged(Me, Nothing)
        End If
    End Sub
    Public Overridable Function Description() As String Implements IItemViewModel(Of dataclass).Description
        Return Data.ID.ToString
    End Function
#End Region

#Region "Events"
    Public Event Deleted As IItemViewModel(Of dataclass).DeletedEventHandler Implements IItemViewModel(Of dataclass).Deleted
    Public Event Updated As IItemViewModel(Of dataclass).UpdatedEventHandler Implements IItemViewModel(Of dataclass).Updated
    Public Event DoubleClicked As IItemViewModel(Of dataclass).DoubleClickedEventHandler Implements IItemViewModel(Of dataclass).DoubleClicked
    Public Event Clicked As IItemViewModel(Of dataclass).ClickedEventHandler Implements IItemViewModel(Of dataclass).Clicked
    Public Event CanSaveChanged As IItemViewModel(Of dataclass).CanSaveChangedEventHandler Implements IItemViewModel(Of dataclass).CanSaveChanged
    Public Event IsSelectedChanged As IItemViewModel(Of dataclass).IsSelectedChangedEventHandler Implements IItemViewModel(Of dataclass).IsSelectedChanged
    Public Event CheckedChanged As IItemViewModel(Of dataclass).CheckedChangedEventHandler Implements IItemViewModel(Of dataclass).CheckedChanged
    Public Event ColorChanged As IItemViewModel(Of dataclass).ColorChangedEventHandler Implements IItemViewModel(Of dataclass).ColorChanged
#End Region
End Class