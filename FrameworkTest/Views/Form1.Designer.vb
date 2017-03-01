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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.CountriesGrid = New FrameworkTest.CountriesGrid()
        Me.HynrTextBox1 = New HynrFramework.HynrTextBox()
        Me.HynrTextBox2 = New HynrFramework.HynrTextBox()
        Me.StarshipsGrid = New FrameworkTest.StarshipsGrid()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.PersonsGrid = New FrameworkTest.PersonsGrid()
        Me.HynrLabelStrip1 = New HynrFramework.HynrLabelStrip()
        Me.NameCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RemarksCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CountryCol = New System.Windows.Forms.DataGridViewComboBoxColumn()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.CountriesGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StarshipsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(6, 0)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 34)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Add new"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(95, 0)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(83, 34)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "delete selected"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(184, 0)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(83, 34)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "save"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(273, 0)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(83, 34)
        Me.Button4.TabIndex = 6
        Me.Button4.Text = "open"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.CountriesGrid)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Button4)
        Me.SplitContainer1.Panel1.Controls.Add(Me.HynrTextBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.HynrTextBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.StarshipsGrid)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button10)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button9)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button8)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PersonsGrid)
        Me.SplitContainer1.Size = New System.Drawing.Size(709, 492)
        Me.SplitContainer1.SplitterDistance = 376
        Me.SplitContainer1.TabIndex = 16
        '
        'CountriesGrid
        '
        Me.CountriesGrid.AllowUserToAddRows = False
        Me.CountriesGrid.AllowUserToDeleteRows = False
        Me.CountriesGrid.AutoGenerateColumns = False
        Me.CountriesGrid.BindingSourceDataSource = Nothing
        Me.CountriesGrid.CancellationSource = Nothing
        Me.CountriesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.CountriesGrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.CountriesGrid.HynrSettings = Nothing
        Me.CountriesGrid.IsBusy = False
        Me.CountriesGrid.Location = New System.Drawing.Point(0, 66)
        Me.CountriesGrid.Name = "CountriesGrid"
        Me.CountriesGrid.SelectedItem = Nothing
        Me.CountriesGrid.SelectedItems = Nothing
        Me.CountriesGrid.Size = New System.Drawing.Size(376, 426)
        Me.CountriesGrid.TabIndex = 0
        '
        'HynrTextBox1
        '
        Me.HynrTextBox1.Location = New System.Drawing.Point(6, 40)
        Me.HynrTextBox1.Name = "HynrTextBox1"
        Me.HynrTextBox1.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox1.TabIndex = 12
        '
        'HynrTextBox2
        '
        Me.HynrTextBox2.Location = New System.Drawing.Point(0, 40)
        Me.HynrTextBox2.Name = "HynrTextBox2"
        Me.HynrTextBox2.Size = New System.Drawing.Size(229, 20)
        Me.HynrTextBox2.TabIndex = 22
        '
        'StarshipsGrid
        '
        Me.StarshipsGrid.AllowUserToAddRows = False
        Me.StarshipsGrid.AllowUserToDeleteRows = False
        Me.StarshipsGrid.AutoGenerateColumns = False
        Me.StarshipsGrid.BindingSourceDataSource = Nothing
        Me.StarshipsGrid.CancellationSource = Nothing
        Me.StarshipsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.StarshipsGrid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StarshipsGrid.HynrSettings = Nothing
        Me.StarshipsGrid.IsBusy = False
        Me.StarshipsGrid.Location = New System.Drawing.Point(0, 233)
        Me.StarshipsGrid.Name = "StarshipsGrid"
        Me.StarshipsGrid.SelectedItem = Nothing
        Me.StarshipsGrid.SelectedItems = Nothing
        Me.StarshipsGrid.Size = New System.Drawing.Size(329, 259)
        Me.StarshipsGrid.TabIndex = 21
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(165, 3)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(75, 23)
        Me.Button10.TabIndex = 20
        Me.Button10.Text = "save person"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(84, 3)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(75, 23)
        Me.Button9.TabIndex = 19
        Me.Button9.Text = "delete person"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(3, 3)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(75, 23)
        Me.Button8.TabIndex = 18
        Me.Button8.Text = "add person"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'PersonsGrid
        '
        Me.PersonsGrid.AllowUserToAddRows = False
        Me.PersonsGrid.AllowUserToDeleteRows = False
        Me.PersonsGrid.AutoGenerateColumns = False
        Me.PersonsGrid.BindingSourceDataSource = Nothing
        Me.PersonsGrid.CancellationSource = Nothing
        Me.PersonsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PersonsGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameCol, Me.RemarksCol, Me.CountryCol})
        Me.PersonsGrid.HynrSettings = Nothing
        Me.PersonsGrid.IsBusy = False
        Me.PersonsGrid.Location = New System.Drawing.Point(0, 66)
        Me.PersonsGrid.Name = "PersonsGrid"
        Me.PersonsGrid.SelectedItem = Nothing
        Me.PersonsGrid.SelectedItems = Nothing
        Me.PersonsGrid.Size = New System.Drawing.Size(329, 166)
        Me.PersonsGrid.TabIndex = 17
        '
        'HynrLabelStrip1
        '
        Me.HynrLabelStrip1.HynrSettings = Nothing
        Me.HynrLabelStrip1.LabelText = Nothing
        Me.HynrLabelStrip1.Location = New System.Drawing.Point(0, 0)
        Me.HynrLabelStrip1.Name = "HynrLabelStrip1"
        Me.HynrLabelStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.HynrLabelStrip1.Size = New System.Drawing.Size(709, 25)
        Me.HynrLabelStrip1.TabIndex = 7
        Me.HynrLabelStrip1.Text = "HynrLabelStrip1"
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
        'CountryCol
        '
        Me.CountryCol.HeaderText = "Country"
        Me.CountryCol.Name = "CountryCol"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(709, 517)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.HynrLabelStrip1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.CountriesGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StarshipsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PersonsGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents HynrLabelStrip1 As HynrFramework.HynrLabelStrip
    Friend WithEvents HynrTextBox1 As HynrFramework.HynrTextBox
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents CountriesGrid As CountriesGrid
    Friend WithEvents PersonsGrid As PersonsGrid
    Friend WithEvents Button10 As Button
    Friend WithEvents Button9 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents StarshipsGrid As StarshipsGrid
    Friend WithEvents HynrTextBox2 As HynrFramework.HynrTextBox
    Friend WithEvents NameCol As DataGridViewTextBoxColumn
    Friend WithEvents RemarksCol As DataGridViewTextBoxColumn
    Friend WithEvents CountryCol As DataGridViewComboBoxColumn
End Class
