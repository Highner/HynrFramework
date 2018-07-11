Imports System.Windows.Forms

Public Class HynrObjectPickerDialogList
    Inherits HynrObjectPickerDialogBase

#Region "Methods"
    Public Function GetItems(source As List(Of String), header As String, Optional selecteditems As String() = Nothing) As List(Of String)

        'create grid from listviewmodel and add it to panel
        Dim Grid = New HynrFilterGrid(Of HynrCheckboxFilterItem, CheckboxFilterDataItemViewModel(Of HynrCheckboxFilterItem))
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
        Dim sourcelist As New List(Of CheckboxFilterDataItemViewModel(Of HynrCheckboxFilterItem))
        If Not IsNothing(source) Then
            For Each item In source
                Dim checked As Boolean
                If (Not IsNothing(selecteditems) AndAlso selecteditems.Any AndAlso selecteditems.Contains(item)) Then checked = True Else checked = False
                sourcelist.Add(New CheckboxFilterDataItemViewModel(Of HynrCheckboxFilterItem) With {.Data = New HynrCheckboxFilterItem With {.Checked = checked, .DisplayName = item}})
            Next
        End If
        Grid.DataSource = sourcelist

        'fill headertext
        LabelStrip.LabelText = header

        'create return object and fill in case OK clicked
        Dim list As New List(Of String)
        If ShowDialog() = DialogResult.OK Then
            For Each item As DataGridViewRow In Grid.Rows
                Dim dataitem As CheckboxFilterDataItemViewModel(Of HynrCheckboxFilterItem) = item.DataBoundItem
                If dataitem.Checked Then list.Add(dataitem.DisplayName)
            Next
        Else
            If Not IsNothing(selecteditems) Then Return selecteditems.ToList
        End If
        Return list
    End Function
#End Region
End Class
