<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HynrCheckedFilterButton
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
        Me.ButtonSetFilter = New HynrFramework.HynrButton()
        Me.ButtonClearFilter = New HynrFramework.HynrButton()
        Me.Label = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'ButtonSetFilter
        '
        Me.ButtonSetFilter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonSetFilter.IsBusy = False
        Me.ButtonSetFilter.Location = New System.Drawing.Point(111, 0)
        Me.ButtonSetFilter.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonSetFilter.Name = "ButtonSetFilter"
        Me.ButtonSetFilter.Size = New System.Drawing.Size(90, 25)
        Me.ButtonSetFilter.TabIndex = 1
        Me.ButtonSetFilter.Text = "HynrButton1"
        Me.ButtonSetFilter.UseVisualStyleBackColor = True
        '
        'ButtonClearFilter
        '
        Me.ButtonClearFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonClearFilter.IsBusy = False
        Me.ButtonClearFilter.Location = New System.Drawing.Point(207, 0)
        Me.ButtonClearFilter.Margin = New System.Windows.Forms.Padding(0)
        Me.ButtonClearFilter.Name = "ButtonClearFilter"
        Me.ButtonClearFilter.Size = New System.Drawing.Size(36, 25)
        Me.ButtonClearFilter.TabIndex = 0
        Me.ButtonClearFilter.Text = "X"
        Me.ButtonClearFilter.UseVisualStyleBackColor = True
        '
        'Label
        '
        Me.Label.AutoSize = True
        Me.Label.Location = New System.Drawing.Point(3, 6)
        Me.Label.Name = "Label"
        Me.Label.Size = New System.Drawing.Size(50, 13)
        Me.Label.TabIndex = 2
        Me.Label.Text = "Labeltext"
        Me.Label.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'HynrCheckedFilterButton
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label)
        Me.Controls.Add(Me.ButtonSetFilter)
        Me.Controls.Add(Me.ButtonClearFilter)
        Me.Name = "HynrCheckedFilterButton"
        Me.Size = New System.Drawing.Size(243, 25)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents ButtonClearFilter As HynrButton
    Public WithEvents ButtonSetFilter As HynrButton
    Friend WithEvents Label As System.Windows.Forms.Label
End Class
