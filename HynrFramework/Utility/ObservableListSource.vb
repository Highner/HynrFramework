Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Data.Entity


Public Class ObservableListSource(Of T As Class)
        Inherits ObservableCollection(Of T)
        Implements IListSource
        Private _bindingList As IBindingList

        Private ReadOnly Property IListSource_ContainsListCollection() As Boolean Implements IListSource.ContainsListCollection
            Get
                Return False
            End Get
        End Property

        Private Function IListSource_GetList() As IList Implements IListSource.GetList
            Return If(_bindingList, (InlineAssignHelper(_bindingList, Me.ToBindingList())))
        End Function
        Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function
    End Class
