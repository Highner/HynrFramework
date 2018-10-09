<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HynrExcelImporterDialogBase
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim HynrUISettings1 As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim HynrUISettings2 As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim HynrUISettings3 As HynrFramework.HynrUISettings = New HynrFramework.HynrUISettings()
        Me.TargetGrid = New HynrFramework.HynrGridBase()
        Me.SourceGrid = New HynrFramework.HynrGridBase()
        Me.PropertyGrid = New HynrFramework.HynrGridBase()
        Me.SourceColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TargetColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        CType(Me.TargetGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SourceGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PropertyGrid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(889, 410)
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(970, 410)
        '
        'TargetGrid
        '
        Me.TargetGrid.AllowUserToAddRows = False
        Me.TargetGrid.AllowUserToDeleteRows = False
        Me.TargetGrid.AllowUserToResizeRows = False
        Me.TargetGrid.BackgroundColor = System.Drawing.Color.White
        Me.TargetGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TargetGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TargetGrid.DefaultCellStyle = DataGridViewCellStyle1
        Me.TargetGrid.DisplaySumRowHeader = False
        Me.TargetGrid.EnableDirectGridExport = False
        Me.TargetGrid.GridColor = System.Drawing.Color.LightGray
        HynrUISettings1.CompanyColor = System.Drawing.Color.IndianRed
        HynrUISettings1.GridBackcolor = System.Drawing.Color.White
        HynrUISettings1.GridColor = System.Drawing.Color.LightGray
        HynrUISettings1.LabelstripColor = System.Drawing.Color.White
        HynrUISettings1.SelectedBackColor = System.Drawing.Color.AliceBlue
        HynrUISettings1.SelectedForecolor = System.Drawing.Color.Black
        HynrUISettings1.SubHeaderColor = System.Drawing.Color.LightGray
        Me.TargetGrid.HynrSettings = HynrUISettings1
        Me.TargetGrid.IsBusy = False
        Me.TargetGrid.Location = New System.Drawing.Point(650, 12)
        Me.TargetGrid.Name = "TargetGrid"
        Me.TargetGrid.ReadOnly = True
        Me.TargetGrid.RowHeadersVisible = False
        Me.TargetGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.TargetGrid.Size = New System.Drawing.Size(395, 392)
        Me.TargetGrid.SummaryColumns = Nothing
        Me.TargetGrid.SummaryRowBackColor = System.Drawing.Color.Empty
        Me.TargetGrid.SummaryRowSpace = 0
        Me.TargetGrid.SummaryRowVisible = False
        Me.TargetGrid.SumRowHeaderText = Nothing
        Me.TargetGrid.SumRowHeaderTextBold = False
        Me.TargetGrid.TabIndex = 2
        '
        'SourceGrid
        '
        Me.SourceGrid.AllowUserToAddRows = False
        Me.SourceGrid.AllowUserToDeleteRows = False
        Me.SourceGrid.AllowUserToResizeRows = False
        Me.SourceGrid.BackgroundColor = System.Drawing.Color.White
        Me.SourceGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.SourceGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.SourceGrid.DefaultCellStyle = DataGridViewCellStyle2
        Me.SourceGrid.DisplaySumRowHeader = False
        Me.SourceGrid.EnableDirectGridExport = False
        Me.SourceGrid.GridColor = System.Drawing.Color.LightGray
        HynrUISettings2.CompanyColor = System.Drawing.Color.IndianRed
        HynrUISettings2.GridBackcolor = System.Drawing.Color.White
        HynrUISettings2.GridColor = System.Drawing.Color.LightGray
        HynrUISettings2.LabelstripColor = System.Drawing.Color.White
        HynrUISettings2.SelectedBackColor = System.Drawing.Color.AliceBlue
        HynrUISettings2.SelectedForecolor = System.Drawing.Color.Black
        HynrUISettings2.SubHeaderColor = System.Drawing.Color.LightGray
        Me.SourceGrid.HynrSettings = HynrUISettings2
        Me.SourceGrid.IsBusy = False
        Me.SourceGrid.Location = New System.Drawing.Point(12, 12)
        Me.SourceGrid.Name = "SourceGrid"
        Me.SourceGrid.ReadOnly = True
        Me.SourceGrid.RowHeadersVisible = False
        Me.SourceGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SourceGrid.Size = New System.Drawing.Size(395, 392)
        Me.SourceGrid.SummaryColumns = Nothing
        Me.SourceGrid.SummaryRowBackColor = System.Drawing.Color.Empty
        Me.SourceGrid.SummaryRowSpace = 0
        Me.SourceGrid.SummaryRowVisible = False
        Me.SourceGrid.SumRowHeaderText = Nothing
        Me.SourceGrid.SumRowHeaderTextBold = False
        Me.SourceGrid.TabIndex = 3
        '
        'PropertyGrid
        '
        Me.PropertyGrid.AllowUserToAddRows = False
        Me.PropertyGrid.AllowUserToDeleteRows = False
        Me.PropertyGrid.AllowUserToResizeRows = False
        Me.PropertyGrid.BackgroundColor = System.Drawing.Color.White
        Me.PropertyGrid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.PropertyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.PropertyGrid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SourceColumn, Me.TargetColumn})
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.AliceBlue
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.PropertyGrid.DefaultCellStyle = DataGridViewCellStyle3
        Me.PropertyGrid.DisplaySumRowHeader = False
        Me.PropertyGrid.EnableDirectGridExport = False
        Me.PropertyGrid.GridColor = System.Drawing.Color.LightGray
        HynrUISettings3.CompanyColor = System.Drawing.Color.IndianRed
        HynrUISettings3.GridBackcolor = System.Drawing.Color.White
        HynrUISettings3.GridColor = System.Drawing.Color.LightGray
        HynrUISettings3.LabelstripColor = System.Drawing.Color.White
        HynrUISettings3.SelectedBackColor = System.Drawing.Color.AliceBlue
        HynrUISettings3.SelectedForecolor = System.Drawing.Color.Black
        HynrUISettings3.SubHeaderColor = System.Drawing.Color.LightGray
        Me.PropertyGrid.HynrSettings = HynrUISettings3
        Me.PropertyGrid.IsBusy = False
        Me.PropertyGrid.Location = New System.Drawing.Point(413, 12)
        Me.PropertyGrid.Name = "PropertyGrid"
        Me.PropertyGrid.ReadOnly = True
        Me.PropertyGrid.RowHeadersVisible = False
        Me.PropertyGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.PropertyGrid.Size = New System.Drawing.Size(231, 344)
        Me.PropertyGrid.SummaryColumns = Nothing
        Me.PropertyGrid.SummaryRowBackColor = System.Drawing.Color.Empty
        Me.PropertyGrid.SummaryRowSpace = 0
        Me.PropertyGrid.SummaryRowVisible = False
        Me.PropertyGrid.SumRowHeaderText = Nothing
        Me.PropertyGrid.SumRowHeaderTextBold = False
        Me.PropertyGrid.TabIndex = 4
        '
        'SourceColumn
        '
        Me.SourceColumn.DataPropertyName = "Source"
        Me.SourceColumn.HeaderText = "Source"
        Me.SourceColumn.Name = "SourceColumn"
        Me.SourceColumn.ReadOnly = True
        '
        'TargetColumn
        '
        Me.TargetColumn.DataPropertyName = "Target"
        Me.TargetColumn.HeaderText = "Target"
        Me.TargetColumn.Name = "TargetColumn"
        Me.TargetColumn.ReadOnly = True
        Me.TargetColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TargetColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'HynrExcelImporterDialog
        '
        Me.CancelVisible = True
        Me.ClientSize = New System.Drawing.Size(1057, 445)
        Me.Controls.Add(Me.PropertyGrid)
        Me.Controls.Add(Me.SourceGrid)
        Me.Controls.Add(Me.TargetGrid)
        Me.Name = "HynrExcelImporterDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Controls.SetChildIndex(Me.ButtonCancel, 0)
        Me.Controls.SetChildIndex(Me.ButtonOK, 0)
        Me.Controls.SetChildIndex(Me.TargetGrid, 0)
        Me.Controls.SetChildIndex(Me.SourceGrid, 0)
        Me.Controls.SetChildIndex(Me.PropertyGrid, 0)
        CType(Me.TargetGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SourceGrid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PropertyGrid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public WithEvents TargetGrid As HynrGridBase
    Protected WithEvents SourceGrid As HynrGridBase
    Public WithEvents PropertyGrid As HynrGridBase
    Friend WithEvents SourceColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TargetColumn As System.Windows.Forms.DataGridViewComboBoxColumn
End Class
