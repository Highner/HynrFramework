<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class sdfsf
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.CheckedComboBox1 = New CheckComboBoxTest.CheckedComboBox()
        Me.SuspendLayout()
        '
        'CheckedComboBox1
        '
        Me.CheckedComboBox1.CheckOnClick = True
        Me.CheckedComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable
        Me.CheckedComboBox1.DropDownHeight = 1
        Me.CheckedComboBox1.FormattingEnabled = True
        Me.CheckedComboBox1.IntegralHeight = False
        Me.CheckedComboBox1.Location = New System.Drawing.Point(70, 76)
        Me.CheckedComboBox1.Name = "CheckedComboBox1"
        Me.CheckedComboBox1.Size = New System.Drawing.Size(121, 21)
        Me.CheckedComboBox1.TabIndex = 0
        Me.CheckedComboBox1.ValueSeparator = ", "
        '
        'sdfsf
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.CheckedComboBox1)
        Me.Name = "sdfsf"
        Me.Text = "sdfsf"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CheckedComboBox1 As CheckComboBoxTest.CheckedComboBox
End Class
