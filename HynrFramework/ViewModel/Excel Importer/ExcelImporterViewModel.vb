Imports System.ComponentModel

Public Class ExcelImporterViewModel(Of t)
    Inherits ViewModelBase

#Region "Commands"
    <Browsable(False)>
    Public Property ImportDataCommand As ICommand = New Command(AddressOf ExecuteImportData)
#End Region

#Region "Properties"
    Property _Header As String = "Import Data from Excel"
    Public Property Header As String
        Get
            Return _Header
        End Get
        Set(value As String)
            _Header = value
            OnPropertyChanged("Header")
        End Set
    End Property
    Private _SourceTable As DataTable
    Public Property SourceTable() As DataTable
        Get
            Return _SourceTable
        End Get
        Set(ByVal value As DataTable)
            _SourceTable = value
            OnPropertyChanged("SourceTable")
        End Set
    End Property
    Private _TargetTable As DataTable
    Public Property TargetTable() As DataTable
        Get
            Return _TargetTable
        End Get
        Set(ByVal value As DataTable)
            _TargetTable = value
            OnPropertyChanged("TargetTable")
        End Set
    End Property
    Private _SourcePropertyList As New ObservableListSource(Of SourceTargetData)
    Public Property SourcePropertyList() As ObservableListSource(Of SourceTargetData)
        Get
            Return _SourcePropertyList
        End Get
        Set(ByVal value As ObservableListSource(Of SourceTargetData))
            _SourcePropertyList = value
            OnPropertyChanged("SourcePropertyList")
        End Set
    End Property
    Private _TargetPropertyList As New ObservableListSource(Of String)
    Public Property TargetPropertyList() As ObservableListSource(Of String)
        Get
            Return _TargetPropertyList
        End Get
        Set(ByVal value As ObservableListSource(Of String))
            _TargetPropertyList = value
            OnPropertyChanged("TargetPropertyList")
        End Set
    End Property
    Public ReadOnly Property HasResult() As Boolean
        Get
            Return Not IsNothing(Result)
        End Get
    End Property
    Private _Result As IEnumerable(Of t)
    Public Property Result() As IEnumerable(Of t)
        Get
            Return _Result
        End Get
        Set(ByVal value As IEnumerable(Of t))
            _Result = value
            OnPropertyChanged("HasResult")
        End Set
    End Property
#End Region

#Region "Contructor"
    Public Sub New()

    End Sub
#End Region

#Region "Methods"
    Public Sub ExecuteImportData()
        ImportFile()
        FillTargetPropertyList()
        FillSourcePropertyList()
        RaiseLoadingCompleted()
    End Sub
    Private Sub ImportFile()
        Dim filepath As String = ""
        If GetFileFullPath(filepath) Then
            SourceTable = ExcelFileToDatatable(filepath)
            TargetTable = (New List(Of t)).ListToDataTable
        End If
    End Sub
    Private Sub FillSourcePropertyList()
        SourcePropertyList.Clear()

        For Each col As DataColumn In SourceTable.Columns
            SourcePropertyList.Add(New SourceTargetData() With {.Source = col.ColumnName, .Target = "not used"})
        Next
        OnPropertyChanged("PropertyList")
    End Sub
    Private Sub FillTargetPropertyList()
        TargetPropertyList.Clear()

        For Each prop In GetType(t).GetProperties
            TargetPropertyList.Add(prop.Name)
        Next
        TargetPropertyList.Add("not used")
        OnPropertyChanged("TargetPropertyList")
    End Sub
#End Region
End Class
