Imports System.Reflection

Public Module ObjectMapper
    Public Sub MapProperties(ByVal source As Object, ByRef destination As Object)
        If Not IsNothing(source) AndAlso Not IsNothing(destination) Then

            Dim destProperties As PropertyInfo() = Nothing
            destProperties = destination.[GetType]().GetProperties()

            Dim sourceProperties As PropertyInfo() = Nothing
            sourceProperties = source.[GetType]().GetProperties()

            For Each sourceProperty As PropertyInfo In sourceProperties
                Dim destprop As PropertyInfo = Nothing
                destprop = (From p In destProperties Where p.Name = sourceProperty.Name AndAlso p.PropertyType.IsAssignableFrom(sourceProperty.PropertyType) AndAlso p.CanWrite = True).FirstOrDefault

                If Not IsNothing(destprop) Then
                    Dim val = Nothing
                    val = sourceProperty.GetValue(source, New Object() {})
                    If TypeOf (val) Is String AndAlso IsNothing(val) Then val = ""
                    destprop.SetValue(destination, val, New Object() {})
                End If

            Next
        End If
    End Sub
    Public Sub TrimStrings(ByRef obj As Object)
        If Not IsNothing(obj) Then
            Dim sourceProperties = obj.[GetType]().GetProperties()
            For Each sourceProperty As PropertyInfo In sourceProperties
                If sourceProperty.CanWrite Then
                    Dim val = sourceProperty.GetValue(obj, Nothing)
                    If TypeOf (val) Is String Then
                        sourceProperty.SetValue(obj, val.ToString.Trim)
                    End If
                End If
            Next
        End If
    End Sub
    Public Function GetDifferingProperties(ByVal firstitem As Object, seconditem As Object) As IEnumerable(Of PropertyInfo)
        Dim props As New List(Of PropertyInfo)
        If Not IsNothing(firstitem) AndAlso Not IsNothing(seconditem) Then

            Dim firstproperties As PropertyInfo() = Nothing
            firstproperties = firstitem.[GetType]().GetProperties()

            Dim secondproperties As PropertyInfo() = Nothing
            secondproperties = seconditem.[GetType]().GetProperties()

            For Each firstproperty As PropertyInfo In firstproperties
                Dim sharedprop As PropertyInfo = Nothing
                sharedprop = (From p In secondproperties Where p.Name = firstproperty.Name AndAlso p.PropertyType.IsAssignableFrom(firstproperty.PropertyType)).FirstOrDefault

                If Not IsNothing(sharedprop) Then

                    If Not firstproperty.GetValue(firstitem) = sharedprop.GetValue(seconditem) Then
                        props.Add(firstproperty)
                    End If
                End If
            Next
        End If
        Return props
    End Function
End Module
