Public Class Form1
    Property Settings As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings
    Property ViewModel As PersonsListViewModel
    Property DataContext As New FrameworkTestDBEntities
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewModel = New PersonsListViewModel(New PersonsDataController(New PersonsDataContext(DataContext)))
        Settings.ApplyToControlContainer(Me)
        ViewModel.GetData()
        PersonsGrid.AutoGenerateColumns = True
        PersonsGrid.BindToListListViewModel(ViewModel)
        TextBox1.DataBindings.Add("Text", ViewModel, "NameFilter", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox2.DataBindings.Add("Text", ViewModel, "RemarksFilter", True, DataSourceUpdateMode.OnPropertyChanged)
        TextBox3.DataBindings.Add("Text", ViewModel, "AgeFilter", True, DataSourceUpdateMode.OnPropertyChanged)
        HynrLabelStrip1.BindToListViewModel(ViewModel, "Header")
        Dim countries As New BindingSource
        countries.DataSource = DataContext.Countries.ToList
        HynrFramework.HynrHelpers.BindGridCombobox(PersonsGrid.Columns("CountryCol"), countries, "CountryID", "ID", "Name")
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ViewModel.ApplyFilterCommand.Execute(Nothing)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ViewModel.DeleteSelectedItemsCommand.Execute(Nothing)
    End Sub
End Class
