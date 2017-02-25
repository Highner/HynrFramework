Imports System.Reflection

Module FilteredList
    Function GenerateFilterParameters(ByRef listviewmodel As Object) As String
        Dim parameters As String = ""
        Dim props = listviewmodel.[GetType]().GetProperties().Where(Function(prprt) Attribute.IsDefined(prprt, GetType(ListViewModelFilterAttribute)))
        Dim prevparamexists As Boolean = False
        For Each prop As PropertyInfo In props
            If Not IsNothing(prop.GetValue(listviewmodel)) Then
                Dim att As ListViewModelFilterAttribute = prop.GetCustomAttribute(GetType(ListViewModelFilterAttribute))
                Select Case att.ValueType.ToLower
                    Case "string"
                        If Not IsDBNull(prop.GetValue(listviewmodel)) And Not (prop.GetValue(listviewmodel) = "") Then
                            If prevparamexists Then parameters = parameters + " and "
                            If att.ExactMatch Then
                                parameters = parameters + att.FilteredField + " = " + Chr(34) + prop.GetValue(listviewmodel).ToString + Chr(34)
                            Else
                                parameters = parameters + att.FilteredField + ".ToLower().Contains(" + Chr(34) + prop.GetValue(listviewmodel).ToString.ToLower + Chr(34) + ")"
                            End If
                            prevparamexists = True
                        End If
                    Case "number"
                        If Not (IsNothing(prop.GetValue(listviewmodel))) And Not (prop.GetValue(listviewmodel) = "") Then
                            If prevparamexists Then parameters = parameters + " and "
                            Dim value As Decimal = Convert.ToDecimal(prop.GetValue(listviewmodel))
                            If att.ExactMatch Then
                                parameters = parameters + att.FilteredField + " = " & value & ""
                            Else
                                parameters = parameters + att.FilteredField + ".ToString().Contains(" + Chr(34) + value.ToString + Chr(34) + ")"
                            End If
                            prevparamexists = True
                        End If
                End Select
            End If
        Next
        Return parameters
    End Function
End Module
