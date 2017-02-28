Public Class Form1
    Private Settings As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings
    Dim countrybindingsource As New BindingSource
    Private WithEvents CountryVM As CountryViewModel
    Private DataContext As New FrameworkTestDBEntities
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Settings.ApplyToControlContainer(Me)
        CountryVM = New CountryViewModel
        CountryVM.GetData()
        CountriesGrid.AutoGenerateColumns = True
        StarshipsGrid.AutoGenerateColumns = True
        HynrLabelStrip1.BindToListViewModel(CountryVM, "StarshipsVM", "Header")

        CountriesGrid.BindToListListViewModel(CountryVM.CountriesVM)

        PersonsGrid.BindToListListViewModel(CountryVM, "PersonsVM")

        StarshipsGrid.BindToListListViewModel(CountryVM, "StarshipsVM")



        HynrTextBox1.BindToProperty(CountryVM.CountriesVM, "NameFilter")
        'HynrTextBox2.BindToProperty(CountryVM, "PersonsVM", "NameFilter")
        HynrTextBox2.BindToProperty(CountryVM, "PersonsVM", "NameFilter", DataSourceUpdateMode.OnPropertyChanged)

        PersonsGrid.BindGridCombobox("CountryCol", (New FrameworkTestDBEntities).Countries.ToList, "CountryID", "ID", "Name")


    End Sub
    Private Sub CountryLoadingComplete() Handles CountryVM.LoadingCompleted

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
        CountryVM.CountriesVM.OpenNewFormCommand.Execute(Nothing)
    End Sub
    Private Sub itemdoubleclicked(ByRef item As PersonItemViewModel)
        CountryVM.CountriesVM.OpenNewFormCommand.Execute(Nothing)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        CountryVM.CountriesVM.ApplyFilterCommand.Execute(Nothing)
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        CountryVM.CountriesVM.DeleteSelectedItemsCommand.Execute(Nothing)
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs)
        If CountryVM.CountriesVM.IsBusy Then
            CountryVM.CountriesVM.IsBusy = False
        Else
            CountryVM.CountriesVM.IsBusy = True
        End If
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
End Class
