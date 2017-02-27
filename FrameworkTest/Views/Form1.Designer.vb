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
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn2 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewComboBoxColumn3 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.HynrTextBox3 = New HynrFramework.HynrTextBox()
        Me.HynrTextBox2 = New HynrFramework.HynrTextBox()
        Me.HynrTextBox1 = New HynrFramework.HynrTextBox()
        Me.HynrLabelStrip1 = New HynrFramework.HynrLabelStrip()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.PersonsGrid = New FrameworkTest.PersonsGrid()
        Me.NameCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AgeCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryCol = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 34)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Add new"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(105, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(96, 34)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "delete selected"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(207, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(96, 34)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "save"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(309, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(96, 34)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "open"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(59, 130)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(96, 34)
        Me.Button5.TabIndex = 8
        Me.Button5.Text = "Apply filter"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(59, 170)
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
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Remarks"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Remarks"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Age"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Age"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'DataGridViewComboBoxColumn2
        '
        Me.DataGridViewComboBoxColumn2.DataPropertyName = "CountryID"
        Me.DataGridViewComboBoxColumn2.HeaderText = "Country"
        Me.DataGridViewComboBoxColumn2.Name = "DataGridViewComboBoxColumn2"
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Remarks"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Remarks"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Age"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Age"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewComboBoxColumn3
        '
        Me.DataGridViewComboBoxColumn3.DataPropertyName = "CountryID"
        Me.DataGridViewComboBoxColumn3.HeaderText = "Country"
        Me.DataGridViewComboBoxColumn3.Name = "DataGridViewComboBoxColumn3"
        '
        'HynrTextBox3
        '
        Me.HynrTextBox3.Location = New System.Drawing.Point(3, 104)
        Me.HynrTextBox3.Name = "HynrTextBox3"
        Me.HynrTextBox3.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox3.TabIndex = 14
        '
        'HynrTextBox2
        '
        Me.HynrTextBox2.Location = New System.Drawing.Point(3, 78)
        Me.HynrTextBox2.Name = "HynrTextBox2"
        Me.HynrTextBox2.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox2.TabIndex = 13
        '
        'HynrTextBox1
        '
        Me.HynrTextBox1.Location = New System.Drawing.Point(3, 52)
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
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(162, 322)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(75, 27)
        Me.Button7.TabIndex = 15
        Me.Button7.Text = "Button7"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'PersonsGrid
        '
        Me.PersonsGrid.AllowUserToAddRows = False
        Me.PersonsGrid.AllowUserToDeleteRows = False
        Me.PersonsGrid.AutoGenerateColumns = False
        Me.PersonsGrid.BindingSourceDataSource = Nothing
        Me.PersonsGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PersonsGrid.CancellationSource = Nothing
        Me.PersonsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PersonsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameCol, Me.RemarksCol, Me.AgeCol, Me.CountryCol})
        Me.PersonsGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PersonsGrid.HynrSettings = Nothing
        Me.PersonsGrid.IsBusy = False
        Me.PersonsGrid.Location = New System.Drawing.Point(0, 0)
        Me.PersonsGrid.Name = "PersonsGrid"
        Me.PersonsGrid.RowHeadersVisible = False
        Me.PersonsGrid.SelectedItem = Nothing
        Me.PersonsGrid.SelectedItems = Nothing
        Me.PersonsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PersonsGrid.Size = New System.Drawing.Size(423, 492)
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
        Me.CountryCol.HeaderText = "Country"
        Me.CountryCol.Name = "CountryCol"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.PersonsGrid)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.HynrTextBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.HynrTextBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.HynrTextBox3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button5)
        Me.SplitContainer1.Size = New System.Drawing.Size(894, 492)
        Me.SplitContainer1.SplitterDistance = 423
        Me.SplitContainer1.TabIndex = 16
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.HynrLabelStrip1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
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
    Friend WithEvents HynrTextBox1 As HynrFramework.HynrTextBox
    Friend WithEvents HynrTextBox2 As HynrFramework.HynrTextBox
    Friend WithEvents HynrTextBox3 As HynrFramework.HynrTextBox
    Friend WithEvents DataGridViewTextBoxColumn1 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn1 As DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn2 As DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn3 As DataGridViewComboBoxColumn
    Friend WithEvents NameCol As DataGridViewTextBoxColumn
    Friend WithEvents RemarksCol As DataGridViewTextBoxColumn
    Friend WithEvents AgeCol As DataGridViewTextBoxColumn
    Friend WithEvents CountryCol As DataGridViewComboBoxColumn
    Friend WithEvents Button7 As Button
    Friend WithEvents SplitContainer1 As SplitContainer
End Class
