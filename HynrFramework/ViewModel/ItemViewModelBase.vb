Imports System.ComponentModel
Imports System.Windows.Input

Public Class ItemViewModelBase(Of dataclass As IHasID)
    Inherits ViewModelBase
    Implements IViewModelItem(Of dataclass)

#Region "PROPERTIES"
    <Browsable(False)>
    Public Property Data As dataclass Implements IViewModelItem(Of dataclass).Data

    <Browsable(False)>
    Public Property DeleteCommand As ICommand Implements IViewModelItem(Of dataclass).DeleteCommand

    <Browsable(False)>
    Public Property UpdateCommand As ICommand Implements IViewModelItem(Of dataclass).UpdateCommand

    <Browsable(False)>
    Public Property ID As Integer Implements IHasID.ID
        Get
            Return Data.ID
        End Get
        Set(value As Integer)
            Data.ID = value
        End Set
    End Property
#End Region

#Region "METHODS"
    'no parameter allowed!
    Public Sub New()
        DeleteCommand = New Command(AddressOf RaiseDeletedEvent)
        UpdateCommand = New Command(AddressOf RaiseUpdatedEvent)
    End Sub

    Private Sub RaiseDeletedEvent()
        RaiseEvent Deleted(Me, Nothing)
    End Sub
    Private Sub RaiseUpdatedEvent()
        RaiseEvent Updated(Me, Nothing)
    End Sub

    'override to get data for each child list when also using listviewmodels as properties for child items of this
    Public Overridable Sub GetData() Implements IViewModelItem(Of dataclass).GetData
    End Sub
#End Region

#Region "EVENTS"
    Public Event Deleted As IViewModelItem(Of dataclass).DeletedEventHandler Implements IViewModelItem(Of dataclass).Deleted
    Public Event Updated As IViewModelItem(Of dataclass).UpdatedEventHandler Implements IViewModelItem(Of dataclass).Updated
#End Region
End Class
