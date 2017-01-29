Public Class Form1
    Property Settings As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings
    Property ViewModel As PersonsListViewModel = New PersonsListViewModel(New PersonsDataController(New PersonsDataContext(New FrameworkTestDBEntities)))
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Settings.ApplyToControlContainer(Me)
        ViewModel.GetData()
        PersonsGrid.AutoGenerateColumns = True

        ViewModel.BindToListControl(PersonsGrid)
        TextBox1.DataBindings.Add("Text", ViewModel, "SelectedItem.Name", True, DataSourceUpdateMode.OnPropertyChanged)
        HynrLabelStrip1.DataBindings.Add("LabelText", ViewModel, "SelectedItem.Header", True, DataSourceUpdateMode.Never)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ViewModel.CreateCommand.Execute(Nothing)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ViewModel.SelectedItem.DeleteCommand.Execute(Nothing)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ViewModel.UpdateAllCommand.Execute(Nothing)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ViewModel.OpenNewFormCommand.Execute(Nothing)
    End Sub
    Private Sub itemdoubleclicked(ByRef item As PersonItemViewModel) Handles PersonsGrid.ItemDoubleClick
        ViewModel.OpenNewFormCommand.Execute(Nothing)
    End Sub
End Class
