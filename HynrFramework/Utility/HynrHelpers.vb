Imports System.Windows.Forms

Public Module HynrHelpers
    Public Sub BindGridCombobox(ByRef column As DataGridViewColumn, ByRef datasource As BindingSource, ByVal datapropertyname As String, ByVal valuemember As String, ByVal displaymember As String)
        Dim col As DataGridViewComboBoxColumn = column
        col.DataPropertyName = datapropertyname
        col.ValueMember = valuemember
        col.DisplayMember = displaymember
        col.DataSource = datasource
    End Sub
End Module
