Public Module GenericObjectInstantiator
    Public Function GetInstance(ByVal t As Type) As Object
        Return Activator.CreateInstance(t)
    End Function
End Module
