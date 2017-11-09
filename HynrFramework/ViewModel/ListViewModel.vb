Public Class ListViewModel(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ListViewModelBase(Of entityitme, dataitem, datacontrollerclass, viewmodelitem)

    Public Property GetDataFunction As Func(Of IEnumerable(Of dataitem))

    Public Overrides Function GetItems() As IEnumerable(Of dataitem)
        If Not IsNothing(GetDataFunction) Then Return GetDataFunction.Invoke Else Return MyBase.GetItems
    End Function
End Class
