Imports HynrFramework

Partial Class Country
    Implements HynrFramework.IHasID

    Private Property IHasID_ID As Integer Implements IHasID.ID
        Get
            Return ID
        End Get
        Set(value As Integer)
            ID = value
        End Set
    End Property
End Class
