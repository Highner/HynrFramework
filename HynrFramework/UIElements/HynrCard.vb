Public Class HynrCard(Of dataitem As IHasID)
    Inherits HynrCardBase
    Private components As ComponentModel.IContainer
    Protected WithEvents ItemViewModel As ItemViewModelBase(Of dataitem)

    Public Sub New()
    End Sub

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

    Protected Sub OnItemClick() Handles Me.Click
        RaiseEvent ItemClick(ItemViewModel)
    End Sub
    Protected Sub OnItemDoubleClick() Handles Me.DoubleClick
        RaiseEvent ItemDoubleClick(ItemViewModel)
    End Sub

    Public Event ItemClick(ByRef itemviewmodel As ItemViewModelBase(Of dataitem))
    Public Event ItemDoubleClick(ByRef itemviewmodel As ItemViewModelBase(Of dataitem))

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'HynrCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Name = "HynrCard"
        Me.Size = New System.Drawing.Size(544, 132)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
