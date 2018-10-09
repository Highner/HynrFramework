Imports System.ComponentModel

Public MustInherit Class ListViewModelBase(Of dataitem)
    Inherits ViewModelBase

#Region "Commands"
    <Browsable(False)>
    Public Property ClearListCommand As ICommand = New Command(AddressOf ClearList)
    <Browsable(False)>
    Public Property CancelLoadingCommand As ICommand = New Command(AddressOf CancelLoading)
#End Region

#Region "Properties"
    Private WithEvents _ItemList As New List(Of dataitem)
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property ItemList() As List(Of dataitem)
        Get
            Return _ItemList
        End Get
        Set(ByVal value As List(Of dataitem))
            _ItemList = value
            If _ItemList.Count = 0 Then
                RaiseEvent SelectedItemChanged()
            End If
            OnPropertyChanged("ItemList")
        End Set
    End Property
    Private _SelectedItem As dataitem
    Public Property SelectedItem() As dataitem
        Get
            If ItemList.Any Then
                Return _SelectedItem
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As dataitem)
            If Not IsNothing(value) AndAlso Not value.Equals(_SelectedItem) Then
                _SelectedItem = value
                RaiseEvent SelectedItemChanged()
                OnPropertyChanged("SelectedItem")
            End If
        End Set
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
    End Sub
    Protected Overrides Sub Finalize()
        CancelLoading()
    End Sub
#End Region

#Region "Crud"
    Protected Overrides Sub GetData()
        GetDataAsync()
    End Sub
    Private Async Sub GetDataAsync()
        DataLoaded = False
        IsBusy = True
        CancelLoading()
        ClearListLight()
        CancellationSource = New Threading.CancellationTokenSource
        Try
            ItemList = Await Task.Run(Function() GetItems(), CancellationSource.Token)
        Catch ex As Exception
        End Try
        IsBusy = False
        DataLoaded = True
        RaiseLoadingCompleted()
    End Sub
    Public Overridable Function GetItems() As List(Of dataitem)
        Return New List(Of dataitem)
    End Function
    Private Sub CancelLoading() 'not working (i think)
        If Not IsNothing(CancellationSource) Then
            CancellationSource.Cancel()
        End If
    End Sub
    Protected Sub ClearListLight()
        ItemList.Clear()
    End Sub
    Private Sub ClearList()
        ItemList.Clear()
        SelectedItem = Nothing
        OnPropertyChanged("SelectedItems")
    End Sub
#End Region

#Region "Events"
    Public Event SelectedItemChanged()
#End Region
End Class
