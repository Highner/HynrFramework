Public Class HynrListGroupDialog(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits HynrForm

#Region "Properties"

#End Region

#Region "Methods"
    Public Sub New()
        InitializeComponent()
        Text = "Group items"

    End Sub


    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'HynrListGroupDialog
        '
        Me.ClientSize = New System.Drawing.Size(758, 261)
        Me.Name = "HynrListGroupDialog"
        Me.ResumeLayout(False)

    End Sub
#End Region
End Class