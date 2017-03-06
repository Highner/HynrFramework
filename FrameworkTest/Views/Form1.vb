Public Class Form1
    Private Settings As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings
    Private WithEvents CountryVM As CountryViewModel

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Settings.ApplyToControlContainer(Me)
        CountryVM = New CountryViewModel
        HynrLabelStrip1.BindToListViewModel(CountryVM, "StarshipsVM", "Header")
        CountriesGrid.BindToListViewModel(CountryVM.CountriesVM)
        PersonsGrid.BindToListViewModel(CountryVM.PersonsVM)
        StarshipsGrid.BindToListViewModel(CountryVM.StarshipsVM)
        HynrTextBox4.BindToListViewModel(CountryVM.PersonsVM, "Name", "testing schmesting")
        HynrTextBox1.BindToViewModel(CountryVM.CountriesVM, "NameFilter")
        HynrTextBox2.BindToViewModel(CountryVM.PersonsVM, "NameFilter")
        PersonsGrid.BindGridCombobox("CountryCol", (New FrameworkTestDBEntities).Countries.ToList, "CountryID", "ID", "Name")
        HynrToolStripButton1.BindToViewModel(CountryVM.CountriesVM, CountryVM.CountriesVM.CreateCommand)
        HynrCheckboxWarp.BindToListViewModel(CountryVM.StarshipsVM, "WarpCapable")
        CountryVM.GetData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        CountryVM.CountriesVM.CreateCommand.Execute(Nothing)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        CountryVM.CountriesVM.DeleteSelectedItemCommand.Execute(Nothing)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        CountryVM.CountriesVM.UpdateAllCommand.Execute(Nothing)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        CountryVM.CountriesVM.CancelLoadCommand.Execute(Nothing)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        CountryVM.PersonsVM.CreateCommand.Execute(Nothing)
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        CountryVM.PersonsVM.DeleteSelectedItemsCommand.Execute(Nothing)
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        CountryVM.PersonsVM.UpdateAllCommand.Execute(Nothing)
    End Sub

    Private Sub HynrTextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub
End Class
