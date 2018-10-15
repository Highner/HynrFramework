Imports HynrFramework
Imports System.ComponentModel
Imports System.Windows.Forms
Imports LiveCharts
Imports System.Collections.Specialized
Imports LiveCharts.Wpf

Public Class HynrLvChartPanel
    Implements IHasHynrSettings
    Implements INotifyPropertyChanged

#Region "Property"
    Private _HynrSettings As HynrUISettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            If Not IsNothing(value) Then
                _HynrSettings = value
                ApplyHynrSettings()
            End If
        End Set
    End Property
    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            ChartVisibility = Not value
            ToggleBusyIndicator(_IsBusy)
            OnPropertyChanged("IsBusy")
            OnPropertyChanged("IsNotBusy")
        End Set
    End Property
    Public ReadOnly Property IsNotBusy As Boolean
        Get
            Return Not IsBusy
        End Get
    End Property
    Public Property ChartVisibility As Boolean
        Get
            Select Case ChartMode
                Case ChartMode.Pie
                    Return PieChart.Visible
                Case ChartMode.Cartesian
                    Return CartesianChart.Visible
                Case Else
                    Return Nothing
            End Select
        End Get
        Set(value As Boolean)
            Select Case ChartMode
                Case ChartMode.Pie
                    PieChart.Visible = value
                Case ChartMode.Cartesian
                    CartesianChart.Visible = value
            End Select
            RaiseEvent ChartVisibilityChanged()
        End Set
    End Property
    Public Property LabelStripVisible() As Boolean
        Get
            Return LabelStrip.Visible
        End Get
        Set(ByVal value As Boolean)
            LabelStrip.Visible = value
        End Set
    End Property
    Public Property LabelStripText As String
        Get
            Return LabelStrip.LabelText
        End Get
        Set(value As String)
            LabelStrip.LabelText = value
        End Set
    End Property
    Private BusyIndicator As New MatrixCircularProgressControl
    Protected LazyBindingViewModel As IViewModelBase
    Public Property CreateSeries As Action
    Protected _CurrentLevel As String
    Public Property CurrentLevel() As String
        Get
            Return _CurrentLevel
        End Get
        Set(ByVal value As String)
            _CurrentLevel = value
            OnLevelChanged()
            OnPropertyChanged("CurrentLevel")
        End Set
    End Property
    Public Property CurrentItem As ChartPoint
    Public Property Levels As New List(Of Object)
    Private _ChartMode As ChartMode
    Public Property ChartMode() As ChartMode
        Get
            Return _ChartMode
        End Get
        Set(ByVal value As ChartMode)
            ChartVisibility = False
            _ChartMode = value
            Select Case value
                Case ChartMode.Pie
                    Series = PieChart.Series
                    ButtonSeries.Enabled = False
                Case ChartMode.Cartesian
                    Series = CartesianChart.Series
                    ButtonSeries.Enabled = True
            End Select
            ChartVisibility = True
        End Set
    End Property
    Public Property IsSubHeader() As Boolean
        Get
            Return LabelStrip.IsSubHeader
        End Get
        Set(ByVal value As Boolean)
            LabelStrip.IsSubHeader = value
        End Set
    End Property
    Private _ChartSeriesNames As List(Of String)
    Public Property ChartSeriesNames() As List(Of String)
        Get
            Return _ChartSeriesNames
        End Get
        Set(ByVal value As List(Of String))
            _ChartSeriesNames = value
            OnPropertyChanged("ChartSeriesNames")
        End Set
    End Property
    Private Property Series As SeriesCollection


#End Region

#Region "Methods"
    Public Sub New()
        InitializeComponent()
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        Controls.Add(BusyIndicator)
        BusyIndicator.BringToFront()
        ChartMode = ChartMode.Cartesian

    End Sub
    Public Sub LevelUp(ByVal levelname As String)
        Levels.Add(New With {.Item = CurrentItem, .Level = CurrentLevel})
        CurrentLevel = levelname
    End Sub
    Public Sub LevelDown()
        If Levels.Any Then
            CurrentItem = Levels.Last.Item
            CurrentLevel = Levels.Last.Level
            Levels.RemoveAt(Levels.IndexOf(Levels.Last))
        End If
    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        HynrSettings.ApplyToControlContainer(Me)
    End Sub
    Private Sub ToggleBusyIndicator(ByVal busy As Boolean)
        Enabled = Not busy
        If busy Then
            BusyIndicator.StartAngle = 30
            BusyIndicator.Show()
            BusyIndicator.Start()
        Else
            BusyIndicator.Stop()
            BusyIndicator.Hide()
        End If
    End Sub
    Private Sub AdjustBusyIndicatorLocation() Handles Me.SizeChanged
        Dim x As Integer = Width / 2 - BusyIndicator.Width / 2
        Dim y As Integer = Height / 2 - BusyIndicator.Height / 2
        BusyIndicator.Location = New Drawing.Point(x, y)
    End Sub

    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Public Sub Clear()
        CartesianChart.AxisX.Clear()
        CartesianChart.AxisY.Clear()
        CartesianChart.Series.Clear()
        CartesianChart.Series = New SeriesCollection
        PieChart.AxisX.Clear()
        PieChart.AxisY.Clear()
        PieChart.Series.Clear()
        PieChart.Series = New SeriesCollection
        ButtonSeries.DropDownItems.Clear()
        'NoDataLabel.Show()
    End Sub
    Private Sub HideCharts()
        CartesianChart.Hide()
        PieChart.Hide()
    End Sub
    Private Sub Loaded() Handles Me.Load
        Clear()
        HideCharts()
    End Sub
    Private Sub PointClicked(sender As Object, p As ChartPoint) Handles CartesianChart.DataClick
        CurrentItem = p
        RaiseEvent DataClick()
    End Sub
    Private Sub MouseClickRight(sender As Object, e As MouseEventArgs) Handles CartesianChart.MouseClick, PieChart.MouseClick
        If e.Button = MouseButtons.Right Then LevelDown()
    End Sub
    Public Sub AddSeries(ByRef series As Definitions.Series.ISeriesView)
        Select Case ChartMode
            Case ChartMode.Cartesian
                CartesianChart.Series.Add(series)
            Case ChartMode.Pie
                PieChart.Series.Add(series)
        End Select
        Dim butt As New HynrToolStripDropDownItem With {.Name = series.Title, .CheckOnClick = True, .Checked = series.IsSeriesVisible, .Text = series.Title}
        AddHandler butt.CheckedChanged, AddressOf SeriesButtonClicked
        ButtonSeries.DropDownItems.Add(butt)
    End Sub
    Protected Function CreateArea(ByVal height As Decimal) As AxisSection
        Dim section = New AxisSection() With {
                        .Value = 0,
                        .SectionWidth = height,
                        .Fill = New System.Windows.Media.SolidColorBrush With
                            {
                            .Color = System.Windows.Media.Color.FromRgb(204, 204, 204),
                            .Opacity = 0.4
                            }
                        }
        Return section
    End Function
    Private Sub SeriesButtonClicked(sender As Object, e As EventArgs)
        Dim butt As HynrToolStripDropDownItem = sender
        If Not IsNothing(Series) AndAlso ChartMode = ChartMode.Cartesian Then
            Dim selser = CartesianChart.Series.Where(Function(x) x.Title = butt.Name).FirstOrDefault
            If Not IsNothing(selser) Then
                If TypeOf (selser) Is LineSeries Then
                    Dim lineser As LineSeries = selser
                    If butt.Checked Then lineser.Visibility = System.Windows.Visibility.Visible Else lineser.Visibility = System.Windows.Visibility.Hidden
                End If
                If TypeOf (selser) Is ColumnSeries Then
                    Dim lineser As ColumnSeries = selser
                    If butt.Checked Then lineser.Visibility = System.Windows.Visibility.Visible Else lineser.Visibility = System.Windows.Visibility.Hidden
                End If
            End If
        End If
    End Sub
    Protected Overridable Sub OnLevelChanged()

    End Sub
#End Region

#Region "Binding"
    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase)
        LazyBindingViewModel = listviewmodel
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        AddHandler listviewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        If Not IsNothing(CreateSeries) Then CreateSeries.Invoke
    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event DataClick()
    Public Event RightClick()
    Public Event ChartVisibilityChanged()

    Private Sub SeriesChanged()

    End Sub
#End Region
End Class

Public Enum ChartMode
    Pie
    Cartesian
End Enum
