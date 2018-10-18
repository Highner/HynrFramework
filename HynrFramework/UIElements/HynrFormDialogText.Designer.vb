<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HynrFormDialogText
    Inherits HynrFramework.HynrFormDialog

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(572, 463)
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(653, 463)
        '
        'TextBox
        '
        Me.TextBox.Location = New System.Drawing.Point(12, 12)
        Me.TextBox.Multiline = True
        Me.TextBox.Name = "TextBox"
        Me.TextBox.Size = New System.Drawing.Size(716, 445)
        Me.TextBox.TabIndex = 2
        '
        'Form1
        '
        Me.CancelVisible = True
        Me.ClientSize = New System.Drawing.Size(740, 498)
        Me.Controls.Add(Me.TextBox)
        Me.Name = "Form1"
        Me.Controls.SetChildIndex(Me.ButtonCancel, 0)
        Me.Controls.SetChildIndex(Me.ButtonOK, 0)
        Me.Controls.SetChildIndex(Me.TextBox, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents TextBox As System.Windows.Forms.TextBox
End Class
