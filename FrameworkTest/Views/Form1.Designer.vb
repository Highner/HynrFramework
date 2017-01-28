<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
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
        Me.components = New System.ComponentModel.Container()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.PersonsGrid = New FrameworkTest.PersonsGrid()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.HynrLabelStrip1 = New HynrFramework.HynrLabelStrip()
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(287, 56)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 34)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Add new"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(424, 61)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(121, 28)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "delete selected"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(596, 32)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(181, 20)
        Me.TextBox1.TabIndex = 4
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(596, 61)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "save"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(255, 455)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 23)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "Button4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'PersonsGrid
        '
        Me.PersonsGrid.AllowUserToAddRows = False
        Me.PersonsGrid.AllowUserToDeleteRows = False
        Me.PersonsGrid.AutoGenerateColumns = False
        Me.PersonsGrid.BindingSourceDataSource = Nothing
        Me.PersonsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PersonsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PersonsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1})
        Me.PersonsGrid.HynrSettings = Nothing
        Me.PersonsGrid.Location = New System.Drawing.Point(150, 108)
        Me.PersonsGrid.Name = "PersonsGrid"
        Me.PersonsGrid.RowHeadersVisible = False
        Me.PersonsGrid.SelectedItem = Nothing
        Me.PersonsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PersonsGrid.Size = New System.Drawing.Size(588, 309)
        Me.PersonsGrid.TabIndex = 3
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "Name"
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        '
        'HynrLabelStrip1
        '
        Me.HynrLabelStrip1.HynrSettings = Nothing
        Me.HynrLabelStrip1.LabelText = Nothing
        Me.HynrLabelStrip1.Location = New System.Drawing.Point(324, 13)
        Me.HynrLabelStrip1.Name = "HynrLabelStrip1"
        Me.HynrLabelStrip1.Size = New System.Drawing.Size(111, 25)
        Me.HynrLabelStrip1.TabIndex = 7
        Me.HynrLabelStrip1.Text = "HynrLabelStrip1"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(854, 517)
        Me.Controls.Add(Me.HynrLabelStrip1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.PersonsGrid)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents PersonsGrid As PersonsGrid
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents HynrLabelStrip1 As HynrFramework.HynrLabelStrip
End Class
