Imports System.ComponentModel
Imports System.Windows.Input
Imports HynrFramework

Public Class ItemViewModelBase(Of dataclass As IHasID)
    Inherits ViewModelBase
    Implements IItemViewModel(Of dataclass)

#Region "PROPERTIES"
    <Browsable(False)>
    Public Property Data As dataclass Implements IItemViewModel(Of dataclass).Data

    <Browsable(False)>
    Public Property DeleteCommand As ICommand = New Command(AddressOf RaiseDeletedEvent) Implements IItemViewModel(Of dataclass).DeleteCommand

    <Browsable(False)>
    Public Property UpdateCommand As ICommand = New Command(AddressOf RaiseUpdatedEvent) Implements IItemViewModel(Of dataclass).UpdateCommand

    <Browsable(False)>
    Public Property ID As Integer Implements IHasID.ID
        Get
            Return Data.ID
        End Get
        Set(value As Integer)
            Data.ID = value
        End Set
    End Property

    <Browsable(False)>
    Public Property GetDataOnSelected As Boolean = False Implements IItemViewModel(Of dataclass).GetDataOnSelected
#End Region

#Region "METHODS"
    'no parameter allowed!
    Public Sub New()
    End Sub
    'in case child lists need to be updated. insert every child listviewmodels getdata() here.
    Public Overridable Sub GetData()
    End Sub
    Private Sub RaiseDeletedEvent()
        RaiseEvent Deleted(Me, Nothing)
    End Sub
    Private Sub RaiseUpdatedEvent()
        RaiseEvent Updated(Me, Nothing)
    End Sub
#End Region

#Region "EVENTS"
    Public Event Deleted As IItemViewModel(Of dataclass).DeletedEventHandler Implements IItemViewModel(Of dataclass).Deleted
    Public Event Updated As IItemViewModel(Of dataclass).UpdatedEventHandler Implements IItemViewModel(Of dataclass).Updated
#End Region
End Class
