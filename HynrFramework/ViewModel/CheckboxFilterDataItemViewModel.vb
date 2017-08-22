Imports System.ComponentModel

<Serializable>
Public Class CheckboxFilterDataItemViewModel(Of dataitem As ICheckboxFilterItem)
    Inherits ItemViewModelBase(Of dataitem)

    <DisplayName("Check")>
    Public Property Checked As Boolean
        Get
            Return Data.Checked
        End Get
        Set(value As Boolean)
            Data.Checked = value
            OnPropertyChanged("Checked")
            RaiseCheckedChanged()
        End Set
    End Property

    <DisplayName("Item")>
    Public ReadOnly Property DisplayName As String
        Get
            If Not IsNothing(Data.DisplayName) Then Return Data.DisplayName Else Return "no data"
        End Get
    End Property
End Class