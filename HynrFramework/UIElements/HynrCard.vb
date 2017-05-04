Public Class HynrCard(Of dataitem As IHasID)
    Inherits HynrCardBase
    Private components As ComponentModel.IContainer
    Protected Property ItemViewModel As ItemViewModelBase(Of dataitem)

    Public Sub New(ByRef viewmodelitem As ItemViewModelBase(Of dataitem))
        MyBase.New
        ItemViewModel = viewmodelitem
        CreateView()
    End Sub

    ''' <summary>
    ''' Override in order to create viewmodel bindings
    ''' </summary>
    Overridable Sub CreateView()
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'HynrCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Name = "HynrCard"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
