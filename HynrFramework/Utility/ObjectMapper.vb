Imports System.Reflection

Module ObjectMapper
    Public Sub MapProperties(ByVal source As Object, ByRef destination As Object)
        Dim destProperties = destination.[GetType]().GetProperties()
        For Each sourceProperty As PropertyInfo In source.[GetType]().GetProperties()
            Dim destprop = (From p In destProperties Where p.Name = sourceProperty.Name AndAlso p.PropertyType.IsAssignableFrom(sourceProperty.PropertyType)).FirstOrDefault
            If Not IsNothing(destprop) Then destprop.SetValue(destination, sourceProperty.GetValue(source, New Object() {}), New Object() {})
        Next
    End Sub
End Module
