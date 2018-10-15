Imports System.ComponentModel
Imports System.Windows.Forms

Public Class HynrCheckedFilterButton
    Implements INotifyPropertyChanged

#Region "Properties"
    Private _LazyBindingViewModel As IListViewModelBase
    Private _LazyBindingDisplayProperty As String
    Private _LazyBindingSourceValuesProperty As String
    Private _EmptyText As String
    Private _Items As New List(Of String)

    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            If Not value Then
                Enabled = True
            Else
                Enabled = False
            End If
            OnPropertyChanged("IsBusy")
        End Set
    End Property
    Private _Values As String()
    Public Property Values() As String()
        Get
            Return _Values
        End Get
        Set(ByVal value As String())
            _Values = value
            OnPropertyChanged("Values")
            OnPropertyChanged("HasFilter")
            OnPropertyChanged("ButtonText")
        End Set
    End Property
    Public ReadOnly Property HasFilter() As Boolean
        Get
            If Not IsNothing(Values) Then Return Values.Any Else Return False
        End Get
    End Property
    Public ReadOnly Property ButtonText As String
        Get
            If Not HasFilter Then
                Return "set filter"
            Else
                If Values.Count = 1 Then
                    Return Values(0)
                Else
                    Return Values.Count
                End If
            End If
        End Get
    End Property
#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New
        InitializeComponent()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' use this to bind to filter combo box
    ''' </summary>
    ''' <param name="viewmodel"></param>
    ''' <param name="displayproperty"></param>
    Public Sub BindToListViewModelFilterProperty(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String, ByVal sourcevaluesproperty As String, labeltext As String)
        Label.Text = labeltext
        _LazyBindingSourceValuesProperty = sourcevaluesproperty
        PrepareBinding(viewmodel, displayproperty)
        AddHandler viewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        _Items.Clear()
        If Not IsNothing(DataBindings.Control) Then
            Dim item = DataBindings.Item("Values")
            Try
                If Not IsNothing(item) Then DataBindings.Remove(item)
                If Not _LazyBindingSourceValuesProperty = "" Then
                    _Items.AddRange(_LazyBindingViewModel.GetUniqueItemsForFilter(_LazyBindingSourceValuesProperty))
                End If
                DataBindings.Add("Values", _LazyBindingViewModel, _LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
            Catch
            End Try
        End If
    End Sub
    Private Sub PrepareBinding(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String)
        _LazyBindingDisplayProperty = displayproperty
        _LazyBindingViewModel = viewmodel
        If IsNothing(DataBindings.Item("IsBusy")) Then DataBindings.Add("IsBusy", _LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        SetDatabindings()
    End Sub

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub SetDatabindings()
        ButtonSetFilter.DataBindings.Add("Text", Me, "ButtonText")
    End Sub
#End Region

#Region "Controls"
    Private Sub ButtonSetFilter_Click(sender As Object, e As EventArgs) Handles ButtonSetFilter.Click
        Values = (New HynrObjectPickerDialogList).GetItems(_Items, "Choose filter items", Values).ToArray
    End Sub

    Private Sub ButtonClearFilter_Click(sender As Object, e As EventArgs) Handles ButtonClearFilter.Click
        Values = (New List(Of String)).ToArray
    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#End Region
End Class
