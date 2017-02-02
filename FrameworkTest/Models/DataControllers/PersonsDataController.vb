Imports FrameworkTest

Public Class PersonsDataController
    Inherits HynrFramework.DataControllerBase(Of Person, PersonData, PersonsDataContext)

    Public Sub New(ByRef context As PersonsDataContext)
        MyBase.New(context)
    End Sub

    Public Overrides Function ToData(entityitem As Person) As PersonData
        Dim dataitem = MyBase.ToData(entityitem)
        'dataitem.Country = entityitem.Country.Name
        Return dataitem
    End Function
End Class
