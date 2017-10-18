<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HynrUICard
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
        Me.ToolStrip = New HynrFramework.HynrToolStrip()
        Me.SuspendLayout()
        '
        'LabelStrip
        '
        Me.LabelStrip.HynrSettings = Nothing
        Me.LabelStrip.LabelText = ""
        Me.LabelStrip.Location = New System.Drawing.Point(0, 0)
        Me.LabelStrip.Name = "LabelStrip"
        Me.LabelStrip.Size = New System.Drawing.Size(465, 25)
        Me.LabelStrip.TabIndex = 0
        Me.LabelStrip.Text = "HynrLabelStrip1"
        '
        'ToolStrip
        '
        Me.ToolStrip.HynrSettings = Nothing
        Me.ToolStrip.Location = New System.Drawing.Point(0, 25)
        Me.ToolStrip.Name = "ToolStrip"
        Me.ToolStrip.Size = New System.Drawing.Size(465, 25)
        Me.ToolStrip.TabIndex = 1
        Me.ToolStrip.Text = "HynrToolStrip1"
        '
        'HynrUICard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ToolStrip)
        Me.Controls.Add(Me.LabelStrip)
        Me.Name = "HynrUICard"
        Me.Size = New System.Drawing.Size(465, 386)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LabelStrip As HynrLabelStrip
    Friend WithEvents ToolStrip As HynrToolStrip
End Class
