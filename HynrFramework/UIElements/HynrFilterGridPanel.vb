Public Class HynrFilterGridPanel

    Public WithEvents Grid As HynrGrid(Of ICheckboxFilterItem, CheckboxFilterDataItemViewModel(Of ICheckboxFilterItem))
    Public WithEvents ListViewModel As IListViewModel(Of CheckboxFilterDataItemViewModel(Of ICheckboxFilterItem))

    Public Sub New()
        InitializeComponent()
    End Sub
    Public Sub AddGrid(ByRef grid As HynrGrid(Of ICheckboxFilterItem, CheckboxFilterDataItemViewModel(Of ICheckboxFilterItem)), ByRef lvm As IListViewModel(Of CheckboxFilterDataItemViewModel(Of ICheckboxFilterItem)))
        ListViewModel = lvm
        Me.Grid = grid
        Me.Grid.AutoGenerateColumns = True
        Me.Grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.Grid.BindToListViewModel(ListViewModel)
        Controls.Add(Me.Grid)
        TextBoxFilter.BringToFront()
        ButtonSelect.BringToFront()
        TextBoxFilter.DataBindings.Add("Text", ListViewModel, "DisplayNameFilter", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged)
    End Sub

End Class
