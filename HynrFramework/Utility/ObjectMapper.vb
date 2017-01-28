Imports System.Reflection

Module ObjectMapper
    Public Sub MapProperties(ByVal source As Object, ByRef destination As Object)
        Dim destProperties = destination.[GetType]().GetProperties()
        For Each sourceProperty As PropertyInfo In source.[GetType]().GetProperties()
            For Each destProperty As PropertyInfo In destProperties
                If destProperty.Name = sourceProperty.Name AndAlso destProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType) Then
                    destProperty.SetValue(destination, sourceProperty.GetValue(source, New Object() {}), New Object() {})
                    Exit For
                End If
            Next
        Next
    End Sub
End Module
