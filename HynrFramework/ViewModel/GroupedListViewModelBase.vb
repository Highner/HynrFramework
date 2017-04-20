Public Class GroupedListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ListViewModelBase(Of entityitme, dataitem, datacontrollerclass, viewmodelitem)

#Region "Properties"
    Public Property Groups As New Dictionary(Of Integer, String)
#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New
    End Sub
    Public Sub New(ByRef windowfactory As IWindowFactory)
        MyBase.New(windowfactory)
    End Sub
    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef windowfactory As IWindowFactory = Nothing)
        MyBase.New(datacontroller, windowfactory)
    End Sub
#End Region

#Region "Methods"

#End Region
End Class
