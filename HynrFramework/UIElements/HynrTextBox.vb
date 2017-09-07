Imports System.ComponentModel
Imports System.Reflection
Imports System.Windows.Forms

Public Class HynrTextBox
    Inherits TextBox

#Region "Properties"
    Private LazyBindingViewModel As IViewModelBase
    Private LazyBindingDisplayProperty As String
    Private LazyBindingDataMember As String
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
                If Not IsNothing(Me.Parent) Then
                    If Not Parent.Controls.Contains(_Label) Then Parent.Controls.Add(_Label)
                End If
            Else
                If Not IsNothing(Me.Parent) Then
                    If Parent.Controls.Contains(_Label) Then Parent.Controls.Add(_Label)
                End If
            End If
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        _Label.Text = "labeltext"
    End Sub
    ''' <summary>
    ''' use this to bind to filter text box
    ''' </summary>
    ''' <param name="viewmodel"></param>
    ''' <param name="displayproperty"></param>
    ''' <param name="labeltext"></param>
    Public Sub BindToViewModel(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String, Optional ByVal labeltext As String = "")
        _Label.Text = labeltext
        If labeltext <> "" Then IncludeLabel = True
        PrepareBinding(viewmodel, displayproperty)
        DataBindings.Add("Text", LazyBindingViewModel, LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
    End Sub
    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase, ByVal displayproperty As String, Optional ByVal labeltext As String = "")
        _Label.Text = labeltext
        If labeltext <> "" Then IncludeLabel = True
        PrepareBinding(listviewmodel, displayproperty)
        LazyBindingDataMember = "SelectedItem."
        AddHandler listviewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        If Not IsNothing(DataBindings.Control) Then
            Dim item = DataBindings.Item("Text")
            Try
                If Not IsNothing(item) Then DataBindings.Remove(item)
                DataBindings.Add("Text", LazyBindingViewModel, LazyBindingDataMember & LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
            Catch
                'i really dont get it
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
        _Label.Location = New Drawing.Point(Me.Location.X - _Label.Size.Width + 15, Me.Location.Y)
    End Sub
    Private Sub AddLabel() Handles Me.ParentChanged
        _Label.Parent = Me.Parent
    End Sub
    Public Sub SelectText()
        SelectionStart = 0
        SelectionLength = Text.Length
    End Sub
#End Region

#Region "Events"
#End Region
End Class