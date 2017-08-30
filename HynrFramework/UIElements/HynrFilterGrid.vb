Imports System.Windows.Forms

Public Class HynrFilterGrid(Of dataitem As ICheckboxFilterItem, viewmodelitem As CheckboxFilterDataItemViewModel(Of dataitem))
    Inherits HynrGrid(Of dataitem, viewmodelitem)

    Public Sub GenerateColumns(ByVal header As String)
        Dim checkcol As New DataGridViewCheckBoxColumn()
        checkcol.HeaderText = ""
        checkcol.DataPropertyName = "Checked"
        checkcol.Width = 35

        Dim displaycol As New DataGridViewTextBoxColumn
        displaycol.HeaderText = header
        displaycol.DataPropertyName = "DisplayName"
        displaycol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        Columns.Add(checkcol)
        Columns.Add(displaycol)
    End Sub
End Class
