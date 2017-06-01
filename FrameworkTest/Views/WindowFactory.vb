Imports HynrFramework

Public Class WindowFactory
    Implements HynrFramework.IWindowFactory

    Public Function OpenNewForm(ByRef datacontext As Object) As IHasID Implements IWindowFactory.OpenNewForm
        Dim form As New PersonView(New HynrUISettings, datacontext)
        form.Show()
    End Function

End Class
