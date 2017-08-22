Imports System.Reflection

Public Module ObjectMapper
    Public Sub MapProperties(ByVal source As Object, ByRef destination As Object)
        If Not IsNothing(source) AndAlso Not IsNothing(destination) Then
            Dim destProperties = destination.[GetType]().GetProperties()
            Dim sourceProperties = source.[GetType]().GetProperties()
            For Each sourceProperty As PropertyInfo In sourceProperties
                Dim destprop = (From p In destProperties Where p.Name = sourceProperty.Name AndAlso p.PropertyType.IsAssignableFrom(sourceProperty.PropertyType) AndAlso p.CanWrite = True).FirstOrDefault
                If Not IsNothing(destprop) Then destprop.SetValue(destination, sourceProperty.GetValue(source, New Object() {}), New Object() {})
            Next
        End If
    End Sub
    Public Sub TrimStrings(ByRef obj As Object)
        If Not IsNothing(obj) Then
            Dim sourceProperties = obj.[GetType]().GetProperties()
            For Each sourceProperty As PropertyInfo In sourceProperties
                Dim val = sourceProperty.GetValue(obj, Nothing)
                If TypeOf (val) Is String Then
                    sourceProperty.SetValue(obj, val.ToString.Trim)
                End If
            Next
        End If
    End Sub
End Module
