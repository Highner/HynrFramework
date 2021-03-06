﻿Imports System.ComponentModel
<Serializable>
Public Class PersonItemViewModel
    Inherits HynrFramework.ItemViewModelBase(Of PersonData)

    <DisplayName("lksdhfsdj")>
    Public Property Name As String
        Get
            Return Data.Name
        End Get
        Set(value As String)
            Data.Name = value
            OnPropertyChanged("Name")
        End Set
    End Property

    Public Property Remarks As String
        Get
            Return Data.Remarks
        End Get
        Set(value As String)
            Data.Remarks = value
            OnPropertyChanged("Remarks")
        End Set
    End Property

    Public Property Age As Integer
        Get
            Return Data.Age
        End Get
        Set(value As Integer)
            OnPropertyChanged("Age")
        End Set
    End Property
    Public ReadOnly Property Country As String
        Get
            Return Data.Country
        End Get
    End Property
    Public Property CountryID As Integer
        Get
            Return Data.CountryID
        End Get
        Set(value As Integer)
            Data.CountryID = value
            OnPropertyChanged("CountryID")
        End Set
    End Property

End Class
