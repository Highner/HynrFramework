
Public Module DataFactory
    Public Function CreateNewPersonDataItem() As PersonData
        Dim personitem As New PersonData
        personitem.Name = "New Person"
        personitem.Remarks = "Enter remarks"
        personitem.Birthday = Date.Today
        Return personitem
    End Function
End Module
