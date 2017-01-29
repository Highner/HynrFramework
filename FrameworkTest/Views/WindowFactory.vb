Imports HynrFramework

Public Class WindowFactory
    Implements HynrFramework.IWindowFactory

    Public Sub OpenNewForm(ByRef datacontext As Object) Implements IWindowFactory.OpenNewForm
        Dim form As New PersonView(New HynrUISettings, datacontext)
        form.Show()
    End Sub
End Class
