'------------------------------------------------------------------------------
' <auto-generated>
'     Der Code wurde von einer Vorlage generiert.
'
'     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
'     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class Person
    Public Property ID As Integer
    Public Property Name As String
    Public Property Remarks As String
    Public Property Birthday As Nullable(Of Date)
    Public Property CountryID As Nullable(Of Integer)

    Public Overridable Property Country As Country
    Public Overridable Property Starships As ICollection(Of Starship) = New HashSet(Of Starship)

End Class
