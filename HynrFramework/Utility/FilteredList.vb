Imports System.Reflection

Public Module FilteredList
    Function GenerateFilterParameters(ByRef listviewmodel As Object) As String
        Dim parameters As String = ""
        Dim props = listviewmodel.[GetType]().GetProperties().Where(Function(prprt) Attribute.IsDefined(prprt, GetType(ListViewModelFilterAttribute)))
        Dim prevparamexists As Boolean = False
        For Each prop As PropertyInfo In props
            If Not IsNothing(prop.GetValue(listviewmodel)) Then
                Dim att As ListViewModelFilterAttribute = prop.GetCustomAttribute(GetType(ListViewModelFilterAttribute))
                Select Case att.ValueType.ToLower
                    Case "string()"
                        If Not IsDBNull(prop.GetValue(listviewmodel)) And Not IsNothing(prop.GetValue(listviewmodel)) Then
                            Dim arr As String() = prop.GetValue(listviewmodel)
                            If arr.Count > 0 Then
                                If prevparamexists Then parameters = parameters + " and "

                                parameters = parameters + "("
                                Dim values As String() = prop.GetValue(listviewmodel)
                                Dim secondprevparameterexists As Boolean = False


                                For Each filtervalue In values
                                    If secondprevparameterexists Then parameters = parameters + " or "
                                    If att.ExactMatch Then
                                        parameters = parameters + att.FilteredField + " = " + Chr(34) + filtervalue.ToString.Trim + Chr(34)
                                    Else
                                        parameters = parameters + att.FilteredField + ".ToLower().Contains(" + Chr(34) + filtervalue.ToString.ToLower.Trim + Chr(34) + ")"
                                    End If
                                    secondprevparameterexists = True
                                Next

                                parameters = parameters + ")"

                                prevparamexists = True
                            End If
                        End If
                    Case "string"
                        If Not IsDBNull(prop.GetValue(listviewmodel)) And Not (prop.GetValue(listviewmodel) = "") Then
                            If prevparamexists Then parameters = parameters + " and "
                            If att.ExactMatch Then
                                parameters = parameters + att.FilteredField + " = " + Chr(34) + prop.GetValue(listviewmodel).ToString.Trim + Chr(34)
                            Else
                                parameters = parameters + att.FilteredField + ".ToLower().Contains(" + Chr(34) + prop.GetValue(listviewmodel).ToString.ToLower.Trim + Chr(34) + ")"
                            End If
                            prevparamexists = True
                        End If
                    Case "number"
                        If Not (IsNothing(prop.GetValue(listviewmodel))) And Not (prop.GetValue(listviewmodel) = "") Then
                            If IsNumeric(prop.GetValue(listviewmodel)) Then
                                If prevparamexists Then parameters = parameters + " and "
                                Dim value As Decimal = Convert.ToDecimal(prop.GetValue(listviewmodel))
                                If att.ExactMatch Then
                                    parameters = parameters + att.FilteredField + " = " & value & ""
                                Else
                                    parameters = parameters + att.FilteredField + ".ToString().Contains(" + Chr(34) + value.ToString.Trim + Chr(34) + ")"
                                End If
                                prevparamexists = True
                            End If
                        End If
                    Case "date"
                        If Not (IsNothing(prop.GetValue(listviewmodel))) And Not (prop.GetValue(listviewmodel) = "") Then
                            If IsDate(prop.GetValue(listviewmodel)) Then
                                If prevparamexists Then parameters = parameters + " and "
                                Dim value As Date = Convert.ToDateTime(prop.GetValue(listviewmodel))
                                If att.ExactMatch Then
                                    parameters = parameters + att.FilteredField + " = " + Chr(34) & value & Chr(34)
                                Else
                                    parameters = parameters + att.FilteredField + ".ToString().Contains(" + Chr(34) + value.ToString + Chr(34) + ")"
                                End If
                                prevparamexists = True
                            End If
                        End If
                End Select
            End If
        Next
        Return parameters
    End Function
    Public Function CreateStringListforText(ByVal list As IEnumerable(Of String), Optional ByVal useand As Boolean = True) As String
        Dim text As String = ""
        Dim lastseparator As String
        If Not useand Then lastseparator = ", " Else lastseparator = " and "
        Dim counter As Integer = 1

        If Not IsNothing(list) Then
            If list.Count > 1 Then
                For Each s In list
                    If counter < list.Count - 1 Then
                        text = text & s & ", "
                    ElseIf counter = list.Count - 1 Then
                        text = text & s & lastseparator & list.Last
                    End If
                    counter = counter + 1
                Next
            ElseIf list.Count = 1 Then
                text = list(0)
            End If
        End If
        Return text
    End Function
    Public Function CreateStringListforFilter(ByVal list As IEnumerable(Of String)) As String
        Dim newlist As New List(Of String)
        For Each item In list
            newlist.Add(Chr(34) & item & Chr(34))
        Next
        Return "(" & CreateStringListforText(newlist, False) & ")"
    End Function
End Module
