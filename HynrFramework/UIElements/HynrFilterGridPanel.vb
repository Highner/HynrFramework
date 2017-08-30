Imports System.Windows.Forms

Public Class HynrFilterGridPanel

    Public WithEvents Grid As Object ' HynrFilterGrid(Of ICheckboxFilterItem, CheckboxFilterDataItemViewModel(Of ICheckboxFilterItem))
    Public WithEvents ListViewModel As Object ' IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of ICheckboxFilterItem))

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub AddGrid(Of dataitem As ICheckboxFilterItem)(ByRef lvm As Object, ByVal header As String)
        ListViewModel = lvm
        Me.Grid = New HynrFilterGrid(Of dataitem, CheckboxFilterDataItemViewModel(Of dataitem))
        Me.Grid.Location = New Drawing.Point(0, 21)
        Me.Grid.Anchor = CType(((AnchorStyles.Top Or AnchorStyles.Left) Or AnchorStyles.Right Or AnchorStyles.Bottom), AnchorStyles)
        Me.Grid.Size = New Drawing.Size(Me.Size.Width, Me.Size.Height - 21)

        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.AllowUserToAddRows = False
        Me.Grid.AllowUserToDeleteRows = False
        Me.Grid.AllowUserToResizeColumns = False
        Me.Grid.AllowUserToResizeRows = False

        Me.Grid.GenerateColumns(header)

        Me.Grid.BindToListViewModel(ListViewModel)
        Controls.Add(Me.Grid)

        TextBoxFilter.DataBindings.Add("Text", ListViewModel, "DisplayNameFilter", True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub

    Private Sub ButtonSelect_Click(sender As Object, e As EventArgs) Handles ButtonSelect.Click
        If Not IsNothing(ListViewModel) Then ListViewModel.ToggleCheckedItemsCommand.Execute(Nothing)
    End Sub
End Class
