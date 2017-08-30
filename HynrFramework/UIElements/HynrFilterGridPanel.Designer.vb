<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class HynrFilterGridPanel
    Inherits System.Windows.Forms.UserControl

    'UserControl überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TextBoxFilter = New System.Windows.Forms.TextBox()
        Me.ButtonSelect = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TextBoxFilter
        '
        Me.TextBoxFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxFilter.Location = New System.Drawing.Point(35, 0)
        Me.TextBoxFilter.Name = "TextBoxFilter"
        Me.TextBoxFilter.Size = New System.Drawing.Size(226, 20)
        Me.TextBoxFilter.TabIndex = 0
        '
        'ButtonSelect
        '
        Me.ButtonSelect.Location = New System.Drawing.Point(-1, -1)
        Me.ButtonSelect.Name = "ButtonSelect"
        Me.ButtonSelect.Size = New System.Drawing.Size(36, 22)
        Me.ButtonSelect.TabIndex = 1
        Me.ButtonSelect.UseVisualStyleBackColor = True
        '
        'HynrFilterGridPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ButtonSelect)
        Me.Controls.Add(Me.TextBoxFilter)
        Me.Name = "HynrFilterGridPanel"
        Me.Size = New System.Drawing.Size(261, 324)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents TextBoxFilter As System.Windows.Forms.TextBox
    Public WithEvents ButtonSelect As System.Windows.Forms.Button
End Class
