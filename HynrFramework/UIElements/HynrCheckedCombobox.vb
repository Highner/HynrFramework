Imports System.ComponentModel
Imports System.Windows.Forms

Public Class HynrCheckedCombobox
    Inherits CheckComboBoxTest.CheckedComboBox
    Implements INotifyPropertyChanged

#Region "Properties"
    Private LazyBindingViewModel As IListViewModelBase
    Private LazyBindingDisplayProperty As String
    Private LazyBindingSourceValuesProperty As String
    Private _EmptyText As String
    Private WithEvents _Label As New Label

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
        End Set
    End Property

    Private _IncludeLabel As Boolean = False
    Public Property IncludeLabel() As Boolean
        Get
            Return _IncludeLabel
        End Get
        Set(ByVal value As Boolean)
            _IncludeLabel = value
            _Label.Visible = value
            If value Then
                If Not IsNothing(Parent) Then
                    If Not Parent.Controls.Contains(_Label) Then Parent.Controls.Add(_Label)
                End If
            Else
                If Not IsNothing(Parent) Then
                    If Parent.Controls.Contains(_Label) Then Parent.Controls.Add(_Label)
                End If
            End If
        End Set
    End Property

    Private _ValueArray As String()
    Public Property ValueArray() As String()
        Get
            Return _ValueArray
        End Get
        Set(ByVal value As String())
            _ValueArray = value
            OnPropertyChanged("ValueArray")
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        MyBase.New
        _Label.Text = "labeltext"
        ValueSeparator = ";"
    End Sub
    ''' <summary>
    ''' use this to bind to filter combo box
    ''' </summary>
    ''' <param name="viewmodel"></param>
    ''' <param name="displayproperty"></param>
    ''' <param name="labeltext"></param>
    Public Sub BindToListViewModelFilterProperty(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String, ByVal sourcevaluesproperty As String, Optional ByVal labeltext As String = "")
        _Label.Text = labeltext
        DropDownStyle = ComboBoxStyle.DropDownList
        If labeltext <> "" Then IncludeLabel = True
        LazyBindingSourceValuesProperty = sourcevaluesproperty
        PrepareBinding(viewmodel, displayproperty)
        AddHandler viewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        Items.Clear()
        If Not IsNothing(DataBindings.Control) Then
            Dim item = DataBindings.Item("ValueArray")
            Try
                If Not IsNothing(item) Then DataBindings.Remove(item)
                If Not LazyBindingSourceValuesProperty = "" Then
                    Items.AddRange(LazyBindingViewModel.GetUniqueItemsForFilter(LazyBindingSourceValuesProperty))
                End If
                DataBindings.Add("ValueArray", LazyBindingViewModel, LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
            Catch
            End Try
        End If
    End Sub
    Private Sub PrepareBinding(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String)
        LazyBindingDisplayProperty = displayproperty
        LazyBindingViewModel = viewmodel
        If IsNothing(DataBindings.Item("IsBusy")) Then DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
    End Sub

    Private Sub PlaceLabel() Handles Me.LocationChanged, _Label.SizeChanged
        _Label.TextAlign = Drawing.ContentAlignment.MiddleLeft
        _Label.Location = New Drawing.Point(Location.X - _Label.Size.Width + 15, Location.Y)
    End Sub
    Private Sub AddLabel() Handles Me.ParentChanged
        _Label.Parent = Parent
    End Sub
    Public Sub SelectText()
        SelectionStart = 0
        SelectionLength = Text.Length
    End Sub
    Protected Sub ArrayValueChanged() Handles Me.DropDownClosed
        Dim list As New List(Of String)
        For Each item In CheckedItems
            list.Add(item)
        Next
        If list.Count > 0 Then
            ValueArray = list.ToArray
            Me.Text = ValueArray.ToString
        Else
            ValueArray = Nothing
            Me.Text = ""
        End If
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
#End Region
End Class
