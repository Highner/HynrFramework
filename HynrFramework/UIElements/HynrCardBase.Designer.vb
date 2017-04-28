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
        Me.LabelStrip = New HynrFramework.HynrLabelStrip()
        Me.CollapseButton = New HynrFramework.HynrToolStripButton()
        Me.LabelStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelStrip
        '
        Me.LabelStrip.HynrSettings = Nothing
        Me.LabelStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CollapseButton})
        Me.LabelStrip.LabelText = ""
        Me.LabelStrip.Location = New System.Drawing.Point(0, 0)
        Me.LabelStrip.Name = "LabelStrip"
        Me.LabelStrip.Size = New System.Drawing.Size(544, 25)
        Me.LabelStrip.TabIndex = 0
        Me.LabelStrip.Text = "HynrLabelStrip1"
        '
        'CollapseButton
        '
        Me.CollapseButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.CollapseButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.CollapseButton.Image = Global.HynrFramework.My.Resources.Resources.chevron
        Me.CollapseButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CollapseButton.IsBusy = False
        Me.CollapseButton.Name = "CollapseButton"
        Me.CollapseButton.Size = New System.Drawing.Size(23, 22)
        Me.CollapseButton.Text = "Collapse"
        '
        'HynrCardBase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelStrip)
        Me.Name = "HynrCardBase"
        Me.Size = New System.Drawing.Size(544, 316)
        Me.LabelStrip.ResumeLayout(False)
        Me.LabelStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CollapseButton As HynrToolStripButton
    Public WithEvents LabelStrip As HynrLabelStrip
End Class
