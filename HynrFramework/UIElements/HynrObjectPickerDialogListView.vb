Imports System.Windows.Forms

Public Class HynrObjectPickerDialogListView

#Region "Constructor"
    Public Sub New(cancelvisible As Boolean)
        MyBase.New(New HynrUISettings, cancelvisible)
    End Sub
    Public Sub New(cancelvisible As Boolean, crud As Boolean, create As Boolean, delete As Boolean, save As Boolean, refresh As Boolean)
        MyBase.New(New HynrUISettings, cancelvisible, crud, create, delete, save, refresh)
    End Sub
#End Region

#Region "Methods"
    Public Function GetItems(ByRef lvm As Object, ByVal header As String, multiselect As Boolean) As List(Of Object)

        'create grid from listviewmodel and add it to panel
        Dim grid = CreateGrid(lvm, multiselect)

        'fill headertext
        LabelStrip.LabelText = header

        'bind to crudstrip
        AddDatabindings(lvm)

        'fetch data
        lvm.RefreshAllCommand.Execute()

        'create return object and fill in case OK clicked
        Dim list As New List(Of Object)
        If ShowDialog() = DialogResult.OK Then
            If Not multiselect Then
                list.Add(grid.SelectedRows(0).DataBoundItem)
            Else
                For Each item In grid.SelectedRows
                    list.Add(item.DataBoundItem)
                Next
            End If
        End If
        Return list
    End Function
    Public Function GetItems(Of t As ICheckboxFilterItem)(ByRef lvm As Object, ByVal header As String, multiselect As Boolean) As List(Of t)

        'create grid from listviewmodel and add it to panel
        CreateGrid(lvm, multiselect)

        'fill headertext
        LabelStrip.LabelText = header

        'bind to crudstrip
        AddDatabindings(lvm)

        'fetch data
        lvm.RefreshAllCommand.Execute()

        'create return object and fill in case OK clicked
        Dim list As New List(Of t)
        If ShowDialog() = DialogResult.OK Then
            Dim result As IEnumerable(Of t) = lvm.ItemList
            Return (result.Where(Function(x) x.Checked).ToList)
        End If

        Return list
    End Function
    Private Function CreateGrid(lvm As Object, multiselect As Boolean) As DataGridView
        Dim Grid = lvm.CreateGrid(True)
        Grid.Location = New Drawing.Point(0, 21)
        Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Grid.AllowUserToAddRows = False
        Grid.AllowUserToDeleteRows = False
        Grid.AllowUserToResizeColumns = False
        Grid.AllowUserToResizeRows = False
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Grid.MultiSelect = multiselect
        GridPanel.Controls.Add(Grid)
        Grid.Dock = DockStyle.Fill
        Return Grid
    End Function
    Private Sub AddDatabindings(ByRef listview As ICrudObject)
        CrudStrip.BindToListView(listview)
    End Sub
#End Region
End Class
