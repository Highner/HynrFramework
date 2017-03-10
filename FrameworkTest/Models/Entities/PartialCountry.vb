Imports HynrFramework

Partial Class Country
    Implements IHasID

    Private Property IHasID_ID As Object Implements IHasID.ID
        Get
            Return ID
        End Get
        Set(value As Object)
            ID = value
        End Set
    End Property
End Class
