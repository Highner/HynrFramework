Public Class HynrCard(Of dataitem As IHasID)
    Inherits HynrCardBase

    Protected Property ItemViewModel As ItemViewModelBase(Of dataitem)
    Public Sub New(ByRef viewmodelitem As ItemViewModelBase(Of dataitem))
        MyBase.New
        ItemViewModel = viewmodelitem
    End Sub
End Class
