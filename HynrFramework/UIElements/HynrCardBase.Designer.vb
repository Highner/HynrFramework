<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HynrCardBase
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(HynrCardBase))
        Me.ToolStrip = New HynrFramework.HynrToolStrip()
        Me.CollapseButton = New HynrFramework.HynrToolStripButton()
        Me.ToolStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip
        '
        Me.ToolStrip.HynrSettings = Nothing
        Me.ToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CollapseButton})
        Me.ToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(544, 25)
        Me.ToolStrip.TabIndex = 0
        Me.ToolStrip.Text = "HynrToolStrip1"
        '
        'CollapseButton
        '
        Me.CollapseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.CollapseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CollapseButton.Image = CType(resources.GetObject("CollapseButton.Image"), System.Drawing.Image)
        Me.CollapseButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CollapseButton.IsBusy = False
        Me.CollapseButton.Name = "CollapseButton"
        Me.CollapseButton.Size = New System.Drawing.Size(23, 22)
        Me.CollapseButton.Text = "HynrToolStripButton1"
        '
        'HynrCardBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ToolStrip)
        Me.Name = "HynrCardBase"
        Me.Size = New System.Drawing.Size(544, 316)
        Me.ToolStrip.ResumeLayout(False)
        Me.ToolStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ToolStrip As HynrToolStrip
    Friend WithEvents CollapseButton As HynrToolStripButton
End Class
