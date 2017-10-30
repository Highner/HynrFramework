Imports System.Windows.Forms

Public Module HynrHelpers
    Public Function EqualValues(ByVal original As Object, ByVal changed As Object) As Boolean
        If Not IsNothing(original) AndAlso Not IsNothing(changed) Then
            Dim destProperties = changed.[GetType]().GetProperties()
            Dim originalProperties = original.[GetType]().GetProperties()
            For Each prop In originalProperties
                Dim destprop = (From p In destProperties Where p.Name = prop.Name).FirstOrDefault
                If Not (destprop.GetValue(changed) Is prop.GetValue(original)) Then Return False
            Next
        End If
        Return True
    End Function
    Public Function DataGridViewToDataTable(ByVal dgv As DataGridView) As DataTable
        Dim table As New DataTable
        Try
            Dim col As DataColumn
            For Each dgvCol As DataGridViewColumn In dgv.Columns
                col = New DataColumn(dgvCol.HeaderText)
                table.Columns.Add(col)
            Next
            'Add Rows from the datagridview
            Dim row As DataRow
            Dim colcount As Integer = dgv.Columns.Count - 1
            For i As Integer = 0 To dgv.Rows.Count - 1
                row = table.Rows.Add
                For Each column As DataGridViewColumn In dgv.Columns
                    row.Item(column.Index) = dgv.Rows.Item(i).Cells(column.Index).FormattedValue
                Next
            Next
            Return table
        Catch ex As Exception
            MsgBox("CRITICAL ERROR : Exception caught while converting dataGridView to DataSet... " & Chr(10) & ex.Message)
            Return Nothing
        End Try
    End Function
End Module