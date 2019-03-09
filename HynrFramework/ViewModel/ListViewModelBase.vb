Imports System.Data.Entity
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Windows.Input
Imports System.Linq.Dynamic
Imports System.ComponentModel
Imports System.Text
Imports HynrFramework
Imports System.Data.SqlClient

''' <summary>
''' only CreateNewItem needs to be specified in inherited class
''' </summary>
Public Class ListViewModelBase(Of entityitme As IHasID, dataitem As IHasID, datacontrollerclass As IDataController(Of entityitme, dataitem), viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits ListViewModelBaseLight(Of dataitem, datacontrollerclass, viewmodelitem)
    Implements IListViewModel(Of viewmodelitem), ICrudObject

#Region "Properties"
    Private WithEvents _AutoRefreshWrapper As AutoRefreshWrapper(Of entityitme)

#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New
    End Sub
    Public Sub New(ByRef windowfactory As IListViewWindowFactory(Of dataitem))
        MyBase.New(windowfactory)
    End Sub
    Public Sub New(ByRef datacontroller As datacontrollerclass, Optional ByRef windowfactory As IListViewWindowFactory(Of dataitem) = Nothing)
        MyBase.New(datacontroller, windowfactory)
    End Sub
#End Region

#Region "Auto Refresh"
    Protected Overridable Sub OnCollectionChanged(e As SqlNotificationEventArgs) Handles _AutoRefreshWrapper.CollectionChanged
        If _Refresh Then RefreshAllCommand.Execute()
    End Sub
#End Region

#Region "Events"
#End Region
End Class