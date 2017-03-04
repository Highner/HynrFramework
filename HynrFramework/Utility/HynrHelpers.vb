Imports System.Windows.Forms

Public Module HynrHelpers
    Public Function EqualValues(ByVal original As Object, ByVal changed As Object) As Boolean
        If Not IsNothing(original) AndAlso Not IsNothing(changed) Then
            Dim destProperties = changed.[GetType]().GetProperties()
            Dim originalProperties = original.[GetType]().GetProperties()
            For Each prop In originalProperties
                Dim destprop = (From p In destProperties Where p.Name = prop.Name).FirstOrDefault
                If Not (destprop.GetValue(changed) Is prop.GetValue(original)) Then Return False
            Next
        End If
        Return True
    End Function
End Module