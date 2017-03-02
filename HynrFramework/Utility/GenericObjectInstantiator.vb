Public Module GenericObjectInstantiator
    Public Function GetInstance(ByVal t As Type) As Object
        Return Activator.CreateInstance(t)
    End Function
    Public Function GetInstanceLazy(Of T As Entity.DbContext)()
        Return New Lazy(Of T)(Function() TryCast(Activator.CreateInstance(GetType(T), True), T))
    End Function
End Module