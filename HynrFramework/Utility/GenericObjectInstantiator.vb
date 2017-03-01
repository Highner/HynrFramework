Public Module GenericObjectInstantiator
    Public Function GetInstance(ByVal t As Type) As Object
        Return Activator.CreateInstance(t)
    End Function
    Public Function GetInstanceLazy(Of T As Entity.DbContext)()
        Return New Lazy(Of T)(Function() TryCast(Activator.CreateInstance(GetType(T), True), T))
    End Function
End Module

Public MustInherit Class SingletonBase(Of T As Class)
#Region "Members"

    ''' <summary>
    ''' Static instance. Needs to use lambda expression
    ''' to construct an instance (since constructor is private).
    ''' </summary>
    Private Shared ReadOnly sInstance As New Lazy(Of T)(Function() CreateInstanceOfT())

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets the instance of this singleton.
    ''' </summary>
    Public Shared ReadOnly Property Instance() As T
        Get
            Return sInstance.Value
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Creates an instance of T via reflection since T's constructor is expected to be private.
    ''' </summary>
    ''' <returns></returns>
    Private Shared Function CreateInstanceOfT() As T
        Return TryCast(Activator.CreateInstance(GetType(T), True), T)
    End Function

#End Region
End Class