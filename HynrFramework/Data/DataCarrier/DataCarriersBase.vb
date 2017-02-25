Imports HynrFramework
'probably not necessary, as IHasID should suffice for the dataclasses
Public Class DataCarriersBase
    Implements IHasID

    Public Property ID As Integer Implements IHasID.ID

End Class
