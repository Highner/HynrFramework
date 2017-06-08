Imports HynrFramework

Public Class WindowFactory(Of dataitem As IHasID)
    Implements HynrFramework.IListViewWindowFactory(Of dataitem)

    Public Function DeleteConfirmationForm(ByRef datacontext As dataitem) As Boolean Implements IListViewWindowFactory(Of dataitem).DeleteConfirmationForm
        Return True
    End Function

    Public Function OpenEditForm(ByRef datacontext As dataitem) As dataitem Implements IListViewWindowFactory(Of dataitem).OpenEditForm
        Return Nothing
    End Function

    Public Function OpenNewForm(ByRef datacontext As dataitem) As dataitem Implements IListViewWindowFactory(Of dataitem).OpenNewForm
        'Dim form As New PersonView(New HynrUISettings, datacontext)
        'Form.Show()
    End Function

End Class
