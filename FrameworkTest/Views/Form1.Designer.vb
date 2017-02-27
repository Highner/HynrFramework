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
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.HynrTextBox3 = New HynrFramework.HynrTextBox()
        Me.HynrTextBox2 = New HynrFramework.HynrTextBox()
        Me.HynrTextBox1 = New HynrFramework.HynrTextBox()
        Me.HynrLabelStrip1 = New HynrFramework.HynrLabelStrip()
        Me.PersonsGrid = New FrameworkTest.PersonsGrid()
        Me.NameCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AgeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryCol = New System.Windows.Forms.DataGridViewComboBoxColumn()
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(12, 28)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 34)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Add new"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(114, 28)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 34)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "delete selected"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(216, 28)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(96, 34)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "save"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(318, 28)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(96, 34)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "open"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(548, 164)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(96, 34)
        Me.Button5.TabIndex = 8
        Me.Button5.Text = "Apply filter"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(495, 262)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(96, 34)
        Me.Button6.TabIndex = 11
        Me.Button6.Text = "Delete Selected"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Remarks"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Remarks"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Age"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Age"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.DataPropertyName = "CountryID"
        Me.DataGridViewComboBoxColumn1.HeaderText = "Country"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        '
        'HynrTextBox3
        '
        Me.HynrTextBox3.Location = New System.Drawing.Point(463, 120)
        Me.HynrTextBox3.Name = "HynrTextBox3"
        Me.HynrTextBox3.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox3.TabIndex = 14
        '
        'HynrTextBox2
        '
        Me.HynrTextBox2.Location = New System.Drawing.Point(463, 94)
        Me.HynrTextBox2.Name = "HynrTextBox2"
        Me.HynrTextBox2.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox2.TabIndex = 13
        '
        'HynrTextBox1
        '
        Me.HynrTextBox1.Location = New System.Drawing.Point(463, 68)
        Me.HynrTextBox1.Name = "HynrTextBox1"
        Me.HynrTextBox1.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox1.TabIndex = 12
        '
        'HynrLabelStrip1
        '
        Me.HynrLabelStrip1.HynrSettings = Nothing
        Me.HynrLabelStrip1.LabelText = Nothing
        Me.HynrLabelStrip1.Location = New System.Drawing.Point(0, 0)
        Me.HynrLabelStrip1.Name = "HynrLabelStrip1"
        Me.HynrLabelStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.HynrLabelStrip1.Size = New System.Drawing.Size(894, 25)
        Me.HynrLabelStrip1.TabIndex = 7
        Me.HynrLabelStrip1.Text = "HynrLabelStrip1"
        '
        'PersonsGrid
        '
        Me.PersonsGrid.AllowUserToAddRows = False
        Me.PersonsGrid.AllowUserToDeleteRows = False
        Me.PersonsGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PersonsGrid.AutoGenerateColumns = False
        Me.PersonsGrid.BindingSourceDataSource = Nothing
        Me.PersonsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PersonsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PersonsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameCol, Me.RemarksCol, Me.AgeCol, Me.CountryCol})
        Me.PersonsGrid.HynrSettings = Nothing
        Me.PersonsGrid.Location = New System.Drawing.Point(12, 68)
        Me.PersonsGrid.Name = "PersonsGrid"
        Me.PersonsGrid.RowHeadersVisible = False
        Me.PersonsGrid.SelectedItem = Nothing
        Me.PersonsGrid.SelectedItems = Nothing
        Me.PersonsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PersonsGrid.Size = New System.Drawing.Size(402, 228)
        Me.PersonsGrid.TabIndex = 3
        '
        'NameCol
        '
        Me.NameCol.DataPropertyName = "Name"
        Me.NameCol.HeaderText = "Name"
        Me.NameCol.Name = "NameCol"
        '
        'RemarksCol
        '
        Me.RemarksCol.DataPropertyName = "Remarks"
        Me.RemarksCol.HeaderText = "Remarks"
        Me.RemarksCol.Name = "RemarksCol"
        '
        'AgeCol
        '
        Me.AgeCol.DataPropertyName = "Age"
        Me.AgeCol.HeaderText = "Age"
        Me.AgeCol.Name = "AgeCol"
        '
        'CountryCol
        '
        Me.CountryCol.DataPropertyName = "CountryID"
        Me.CountryCol.HeaderText = "Country"
        Me.CountryCol.Name = "CountryCol"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 517)
        Me.Controls.Add(Me.HynrTextBox3)
        Me.Controls.Add(Me.HynrTextBox2)
        Me.Controls.Add(Me.HynrTextBox1)
        Me.Controls.Add(Me.Button6)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.HynrLabelStrip1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button3)
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
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents HynrLabelStrip1 As HynrFramework.HynrLabelStrip
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents NameCol As DataGridViewTextBoxColumn
    Friend WithEvents RemarksCol As DataGridViewTextBoxColumn
    Friend WithEvents AgeCol As DataGridViewTextBoxColumn
    Friend WithEvents CountryCol As DataGridViewComboBoxColumn
    Friend WithEvents HynrTextBox1 As HynrFramework.HynrTextBox
    Friend WithEvents HynrTextBox2 As HynrFramework.HynrTextBox
    Friend WithEvents HynrTextBox3 As HynrFramework.HynrTextBox
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn1 As DataGridViewComboBoxColumn
End Class
