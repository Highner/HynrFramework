Imports FrameworkTest

Public Class PersonsDataController
    Inherits HynrFramework.DataControllerBase(Of Person, PersonData, PersonsDataContext, FrameworkTestDBEntities)

    Public Sub New(ByRef context As PersonsDataContext)
        MyBase.New(context)
    End Sub

    Public Overrides Function ToData(entityitem As Person) As PersonData
        Dim dataitem = MyBase.ToData(entityitem)
        'dataitem.Country = entityitem.Country.Name
        dataitem.Birthday = entityitem.Birthday
        Return dataitem
    End Function
End Class
