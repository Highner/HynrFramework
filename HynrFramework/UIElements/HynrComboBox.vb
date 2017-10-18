Imports System.Windows.Forms

Public Class HynrComboBox
    Inherits ComboBox

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
            Dim item = DataBindings.Item("SelectedItem")
            Try
                If Not IsNothing(item) Then DataBindings.Remove(item)
                If Not LazyBindingSourceValuesProperty = "" Then
                    Items.AddRange(LazyBindingViewModel.GetUniqueItemsForFilter(LazyBindingSourceValuesProperty))
                    If Not Items.Contains("") Then Items.Insert(0, "")
                End If
                DataBindings.Add("SelectedItem", LazyBindingViewModel, LazyBindingDisplayProperty, True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
            Catch
            End Try
        End If
    End Sub
    Protected Overrides Sub OnSelectionChangeCommitted(e As EventArgs)
        MyBase.OnSelectionChangeCommitted(e)

        Dim bindings = Me.DataBindings.Cast(Of Binding)().Where(Function(x) x.PropertyName = "SelectedItem" AndAlso x.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged)
        For Each binding As Binding In bindings
            binding.WriteValue()
            binding.ReadValue()
        Next
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
