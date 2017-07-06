Imports System.Drawing

Public Class HynrGridCrudToolstrip
    Inherits HynrToolStrip

#Region "Properties"
    Property _ListViewModel
    Public Property ListViewModel As ICrudObject
        Get
            Return _ListViewModel
        End Get
        Set(value As ICrudObject)
            _ListViewModel = value
        End Set
    End Property

    Public Property CreateImage As Image
        Get
            Return CreateButton.Image
        End Get
        Set(value As Image)
            CreateButton.Image = value
        End Set
    End Property
    Public Property SaveImage As Image
        Get
            Return SaveButton.Image
        End Get
        Set(value As Image)
            SaveButton.Image = value
        End Set
    End Property
    Public Property DeleteImage As Image
        Get
            Return DeleteButton.Image
        End Get
        Set(value As Image)
            DeleteButton.Image = value
        End Set
    End Property
    Public Property RefreshImage As Image
        Get
            Return RefreshButton.Image
        End Get
        Set(value As Image)
            RefreshButton.Image = value
        End Set
    End Property
    Private Property CreateButton As New HynrToolStripButton
    Private Property SaveButton As New HynrToolStripButton
    Private Property DeleteButton As New HynrToolStripButton
    Private Property RefreshButton As New HynrToolStripButton
    Public Property CreateButtonEnabled As Boolean
        Get
            Return CreateButton.Enabled
        End Get
        Set(value As Boolean)
            CreateButton.Enabled = value
            CreateButton.Visible = value
        End Set
    End Property
    Public Property SaveButtonEnabled As Boolean
        Get
            Return SaveButton.Enabled
        End Get
        Set(value As Boolean)
            SaveButton.Enabled = value
            SaveButton.Visible = value
        End Set
    End Property
    Public Property DeleteButtonEnabled As Boolean
        Get
            Return DeleteButton.Enabled
        End Get
        Set(value As Boolean)
            DeleteButton.Enabled = value
            DeleteButton.Visible = value
        End Set
    End Property
    Public Property RefreshButtonEnabled As Boolean
        Get
            Return RefreshButton.Enabled
        End Get
        Set(value As Boolean)
            RefreshButton.Enabled = value
            RefreshButton.Visible = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New()
        Items.Add(CreateButton)
        Items.Add(DeleteButton)
        Items.Add(SaveButton)
        Items.Add(RefreshButton)
    End Sub
    Public Sub BindToListView(ByRef listview As ICrudObject)
        ListViewModel = listview
        BindButtons()
    End Sub
    Private Sub BindButtons()
        CreateButton.DataBindings.Clear()
        CreateButton.BindToViewModel(ListViewModel, ListViewModel.OpenNewFormCommand, "Create new item")
        SaveButton.DataBindings.Clear()
        SaveButton.BindToViewModel(ListViewModel, ListViewModel.UpdateAllCommand, "Save")
        DeleteButton.DataBindings.Clear()
        DeleteButton.BindToViewModel(ListViewModel, ListViewModel.DeleteSelectedItemsCommand, "Delete selected items")
        RefreshButton.DataBindings.Clear()
        RefreshButton.BindToViewModel(ListViewModel, ListViewModel.RefreshAllAsyncCommand, "Refresh")
    End Sub
#End Region
End Class
