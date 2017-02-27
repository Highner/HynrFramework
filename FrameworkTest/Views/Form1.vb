Public Class Form1
    Private Settings As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings
    Private ViewModel As PersonsListViewModel
    Private DataContext As New FrameworkTestDBEntities
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ViewModel = New PersonsListViewModel(New PersonsDataController(New PersonsDataContext(DataContext)))
        Settings.ApplyToControlContainer(Me)
        ViewModel.GetData()
        PersonsGrid.BindToListListViewModel(ViewModel)
        HynrTextBox1.BindToProperty(ViewModel, "NameFilter")
        HynrTextBox2.BindToProperty(ViewModel, "RemarksFilter")
        HynrTextBox3.BindToProperty(ViewModel, "AgeFilter")
        HynrLabelStrip1.BindToListViewModel(ViewModel, "Header")
        PersonsGrid.BindGridCombobox("CountryCol", (New FrameworkTestDBEntities).Countries.ToList, "CountryID", "ID", "Name")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ViewModel.CreateCommand.Execute(Nothing)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ViewModel.DeleteSelectedItemCommand.Execute(Nothing)
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

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If ViewModel.IsBusy Then
            ViewModel.IsBusy = False
        Else
            ViewModel.IsBusy = True
        End If
    End Sub
End Class
