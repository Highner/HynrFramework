Imports System.Windows.Forms

Public Class HynrObjectEditorDialog
    Inherits HynrObjectPickerDialogBase

#Region "Methods"
    Public Function GetItems(ByRef source As IEnumerable(Of IHasID), header As String) As DialogResult

        'create grid from listviewmodel and add it to panel
        Dim Grid = New HynrGridBase
        Grid.Location = New Drawing.Point(0, 21)
        Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        Grid.AllowUserToAddRows = False
        Grid.AllowUserToDeleteRows = False
        Grid.AllowUserToResizeColumns = False
        Grid.AllowUserToResizeRows = False
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        Grid.MultiSelect = False
        GridPanel.Controls.Add(Grid)
        Grid.Dock = DockStyle.Fill
        Grid.DataSource = source

        'fill headertext
        LabelStrip.LabelText = header

        Return ShowDialog()

    End Function
#End Region
End Class
