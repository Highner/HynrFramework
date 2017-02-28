
Public Module DataFactory
    Public Function CreateNewPersonDataItem(ByVal countryid As Integer) As PersonData
        Dim personitem As New PersonData
        personitem.Name = "New Person"
        personitem.Remarks = "Enter remarks"
        personitem.Birthday = Date.Today
        personitem.CountryID = countryid
        Return personitem
    End Function
End Module
