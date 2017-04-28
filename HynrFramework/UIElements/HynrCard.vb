Public Class HynrCard(Of dataitem As IHasID)
    Inherits HynrCardBase

    Protected Property ItemViewModel As ItemViewModelBase(Of dataitem)

    Public Sub New(ByRef viewmodelitem As ItemViewModelBase(Of dataitem))
        MyBase.New
        ItemViewModel = viewmodelitem
        CreateView()
    End Sub

    ''' <summary>
    ''' Override in order to create viewmodel bindings
    ''' </summary>
    Overridable Sub CreateView()
    End Sub
End Class
