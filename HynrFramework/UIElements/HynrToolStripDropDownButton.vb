Imports System.Windows.Forms

Public Class HynrToolStripDropDownButton(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ToolStripDropDownButton
    Implements IBindableComponent

#Region "Properties"
    Private _dataBindings As ControlBindingsCollection
    Private _bindingContext As BindingContext
    Public ReadOnly Property DataBindings() As ControlBindingsCollection Implements IBindableComponent.DataBindings
        Get
            If _dataBindings Is Nothing Then
                _dataBindings = New ControlBindingsCollection(Me)
            End If
            Return _dataBindings
        End Get
    End Property
    Public Property BindingContext() As BindingContext Implements IBindableComponent.BindingContext
        Get
            If _bindingContext Is Nothing Then
                _bindingContext = New BindingContext()
            End If
            Return _bindingContext
        End Get
        Set
            _bindingContext = Value
        End Set
    End Property
    Public Property ItemList As ObservableListSource(Of viewmodelitem)
    Private Property ItemTextProperty As String
    Private _IsBusy As Boolean
    Public Property IsBusy As Boolean
        Get
            Return _IsBusy
        End Get
        Set(value As Boolean)
            _IsBusy = value
            ToggleIsBusy(value)
        End Set
    End Property
    Private ViewModel As IViewModelBase
#End Region

#Region "Methods"
    Public Sub BindToViewModel(ByRef listviewmodel As IViewModelBase, Optional ByVal text As String = "", Optional ByVal itemtextpropertyname As String = "", Optional ByVal imagepropertyname As String = "")
        Me.ViewModel = listviewmodel
        Me.Text = text
        Me.ToolTipText = text
        Me.ItemTextProperty = itemtextpropertyname
        DataBindings.Add("IsBusy", Me.ViewModel, "IsBusy", True, DataSourceUpdateMode.Never)
        If imagepropertyname <> "" Then DataBindings.Add("Image", Me.ViewModel, imagepropertyname, True, DataSourceUpdateMode.OnPropertyChanged)
        AddHandler ViewModel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        DataBindings.Add("ItemList", ViewModel, "ItemList", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("IsBusy", ViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        GenerateChildButtons()
    End Sub
    Private Sub GenerateChildButtons()
        Me.DropDownItems.Clear()
        For Each item In ItemList
            Dim button As New ToolStripMenuItem
            button.Text = "lala"
            Me.DropDownItems.Add(button)
        Next
    End Sub
    Private Sub ToggleIsBusy(ByVal busy As Boolean)
        If busy Then
            Me.Enabled = False
        Else
            Me.Enabled = True
        End If
    End Sub
#End Region
End Class
