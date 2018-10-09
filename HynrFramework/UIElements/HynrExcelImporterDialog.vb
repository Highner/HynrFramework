Public Class HynrExcelImporterDialog(Of t)
    Inherits HynrExcelImporterDialogBase

#Region "Properties"
    Private WithEvents _ViewModel As ExcelImporterViewModel(Of t)
#End Region

    Public Sub New()
        _ViewModel = New ExcelImporterViewModel(Of t)
        AddDatabindings()
    End Sub

#Region "Methods"
    Public Function ImportFile() As IEnumerable(Of t)
        _ViewModel.ImportDataCommand.Execute()

        TargetColumn.DataSource = _ViewModel.TargetPropertyList.ToArray

        TargetColumn.ReadOnly = False

        If ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Return _ViewModel.Result
        Else
            Return New List(Of t)
        End If
    End Function
    Private Sub AddDatabindings()
        ButtonOK.DataBindings.Add("Enabled", _ViewModel, "HasResult")
        SourceGrid.DataBindings.Add("DataSource", _ViewModel, "SourceTable")
        TargetGrid.DataBindings.Add("DataSource", _ViewModel, "TargetTable")
        PropertyGrid.DataBindings.Add("DataSource", _ViewModel, "SourcePropertyList")
    End Sub
#End Region
End Class
