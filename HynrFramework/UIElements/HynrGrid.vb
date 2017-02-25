Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Windows.Forms
Imports HynrFramework

Public Class HynrGrid(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem, dbcontextclass), dbcontextclass As DbContext)
    Inherits DataGridView
    Implements IBindableListControl(Of dataitem, viewmodelitem, dbcontextclass)
    Implements INotifyPropertyChanged
    Implements IHasHynrSettings

#Region "PROPERTIES"
    Private Property _BindingSource As New BindingSource
    Public Property BindingSourceDataSource As ObservableListSource(Of viewmodelitem)
        Get
            If Not IsNothing(_BindingSource) Then
                Return _BindingSource.DataSource
            Else
                Return Nothing
            End If
        End Get
        Set(value As ObservableListSource(Of viewmodelitem))
            If Not IsNothing(value) Then
                _BindingSource.DataSource = value
            End If
        End Set
    End Property
    Private Property _SelectedItem As viewmodelitem
    Public Property SelectedItem As viewmodelitem Implements IBindableListControl(Of dataitem, viewmodelitem, dbcontextclass).SelectedItem
        Get
            Return _SelectedItem
        End Get
        Set(value As viewmodelitem)
            _SelectedItem = value
            OnPropertyChanged("SelectedItem")
        End Set
    End Property
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
#End Region

#Region "METHODS"
    Public Sub New()
        DataSource = _BindingSource
        AddHandler _BindingSource.CurrentItemChanged, AddressOf SelectedItemChanged
    End Sub
    Public Sub BindToListListViewModel(ByRef listviewmodel As IListViewModel(Of viewmodelitem))
        DataBindings.Add("BindingSourceDataSource", listviewmodel, "ItemList", True, DataSourceUpdateMode.OnPropertyChanged)
        DataBindings.Add("SelectedItem", listviewmodel, "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub
    Private Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        DefaultCellStyle.SelectionBackColor = HynrSettings.SelectedBackColor
        DefaultCellStyle.SelectionForeColor = HynrSettings.SelectedForecolor
        GridColor = HynrSettings.GridColor
        BackgroundColor = HynrSettings.GridBackcolor
        RowHeadersVisible = HynrSettings.RowHeadersVisible
        BorderStyle = HynrSettings.GridBorderStyle
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New System.ComponentModel.PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub SelectedItemChanged()
        SelectedItem = _BindingSource.Current
    End Sub
    Private Sub ItemDoubleClicked() Handles Me.MouseDoubleClick
        RaiseEvent ItemDoubleClick(SelectedItem)
    End Sub
#End Region

#Region "EVENTS"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ItemDoubleClick(ByRef item As viewmodelitem)
#End Region
End Class
