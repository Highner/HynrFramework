
Imports LiveCharts
Imports LiveCharts.Defaults
Imports LiveCharts.Wpf

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
        CountryVM.RefreshAllCommand.Execute(Nothing)




        Chart.Series = New SeriesCollection()

        Dim ser = New OhlcSeries()
        ser.Values = New ChartValues(Of OhlcPoint)

        Dim po As New OhlcPoint

        ''
        '    Dim map As New

        'ser.Values.Add(po)
        ser.Values.Add(New OhlcPoint(33, 38, 31, 37))
        ser.Values.Add(New OhlcPoint(35, 42, 30, 40))
        ser.Values.Add(New OhlcPoint(37, 40, 35, 38))
        ser.Values.Add(New OhlcPoint(35, 38, 32, 33))

        Chart.Series.Add(ser)

        'based on https://github.com/beto-rodriguez/Live-Charts/issues/166 
        'The Ohcl point X property is zero based indexed.
        'this means the first point is 0, second 1, third 2.... and so on
        'then you can use the Axis.Labels properties to map the chart X with a label in the array.
        'for more info see (mapped labels section) 
        'http://lvcharts.net/#/examples/v1/labels-wpf?path=WPF-Components-Labels

        '  Chart.AxisX.Add(New Axis())




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
