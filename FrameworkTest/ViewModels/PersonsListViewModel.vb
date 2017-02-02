Imports FrameworkTest

Public Class PersonsListViewModel
    Inherits HynrFramework.ListViewModelBase(Of Person, PersonData, PersonsDataController, PersonItemViewModel)

    Public Sub New(ByRef datacontroller As PersonsDataController, ByRef Optional bindingsource As BindingSource = Nothing)
        MyBase.New(datacontroller, New WindowFactory)
    End Sub

    Public Overrides Sub CreateNewItem()
        _DataController.CreateNewItem(DataFactory.CreateNewPersonDataItem)
        GetData()
    End Sub
End Class
