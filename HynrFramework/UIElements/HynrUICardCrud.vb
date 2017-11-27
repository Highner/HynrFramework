Public Class HynrUICardCrud
    Inherits HynrUICard

    Public WithEvents ToolStrip As HynrGridCrudToolstrip

    Public Property ToolStripVisible As Boolean
        Get
            Return ToolStrip.Visible
        End Get
        Set(value As Boolean)
            ToolStrip.Visible = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()

    End Sub

    Public Overrides Sub ApplyHynrSettings()
        LabelStrip.HynrSettings = HynrSettings
        ToolStrip.HynrSettings = HynrSettings

        ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
    End Sub

    Private Sub InitializeComponent()
        Me.ToolStrip = New HynrFramework.HynrGridCrudToolstrip()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip._ListViewModel = Nothing
        Me.ToolStrip.CreateButtonEnabled = True
        Me.ToolStrip.CreateImage = Nothing
        Me.ToolStrip.DeleteButtonEnabled = True
        Me.ToolStrip.DeleteImage = Nothing
        Me.ToolStrip.HynrSettings = Nothing
        Me.ToolStrip.ListViewModel = Nothing
        Me.ToolStrip.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.RefreshButtonEnabled = True
        Me.ToolStrip.RefreshImage = Nothing
        Me.ToolStrip.SaveButtonEnabled = True
        Me.ToolStrip.SaveImage = Nothing
        Me.ToolStrip.Size = New System.Drawing.Size(465, 25)
        Me.ToolStrip.TabIndex = 2
        Me.ToolStrip.Text = "HynrGridCrudToolstrip1"
        '
        'HynrUICardCrud
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Controls.Add(Me.ToolStrip)
        Me.Name = "HynrUICardCrud"
        Me.Controls.SetChildIndex(Me.ToolStrip, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


End Class
