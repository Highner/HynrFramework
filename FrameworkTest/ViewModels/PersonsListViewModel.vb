Imports HynrFramework

Public Class PersonsListViewModel
    Inherits ListViewModelBase(Of Person, PersonData, PersonsDataController, PersonItemViewModel, FrameworkTestDBEntities)

    Public Sub New(ByRef datacontroller As PersonsDataController, ByRef Optional bindingsource As BindingSource = Nothing)
        MyBase.New(datacontroller, New WindowFactory)
    End Sub

    Public Overrides Sub CreateNewItem()
        _DataController.CreateNewItem(CreateNewPersonDataItem)
        GetData()
    End Sub


    Private Property _NameFilter As String
    <ListViewModelFilterAttribute("Name", "string", False)>
    Property NameFilter As String
        Get
            Return _NameFilter
        End Get
        Set(value As String)
            _NameFilter = value
            ApplyFilter()
        End Set
    End Property

    Private Property _RemarksFilter As String
    <ListViewModelFilterAttribute("Remarks", "string", False)>
    Property RemarksFilter As String
        Get
            Return _RemarksFilter
        End Get
        Set(value As String)
            _RemarksFilter = value
            ApplyFilter()
        End Set
    End Property

    Private Property _AgeFilter As String
    <ListViewModelFilterAttribute("Age", "number", False)>
    Property AgeFilter As String
        Get
            Return _AgeFilter
        End Get
        Set(value As String)
            _AgeFilter = value
            ApplyFilter()
        End Set
    End Property


    Public Property Header As String
        Get
            If Not IsNothing(SelectedItem) Then
                Return "Selected Person: " + SelectedItem.Name
            Else
                Return "No person selected"
            End If
        End Get
        Set(value As String)
            ' OnPropertyChanged("Header")
        End Set
    End Property

End Class
