Imports HynrFramework

Public Class WindowFactory(Of dataitem As IHasID)
    Implements HynrFramework.IWindowFactory(Of dataitem)

    Public Function OpenEditForm(ByRef datacontext As dataitem) As dataitem Implements IWindowFactory(Of dataitem).OpenEditForm
        Return Nothing
    End Function

    Public Function OpenNewForm(ByRef datacontext As dataitem) As dataitem Implements IWindowFactory(Of dataitem).OpenNewForm
        'Dim form As New PersonView(New HynrUISettings, datacontext)
        'Form.Show()
    End Function

End Class
