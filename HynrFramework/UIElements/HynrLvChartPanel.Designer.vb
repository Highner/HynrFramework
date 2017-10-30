<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class HynrLvChartPanel
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
        Me.PieChart = New HynrFramework.HynrLvPieChart()
        Me.CartesianChart = New HynrFramework.HynrLvChart()
        Me.LabelStrip = New HynrFramework.HynrLabelStrip()
        Me.SuspendLayout()
        '
        'PieChart
        '
        Me.PieChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PieChart.HynrSettings = Nothing
        Me.PieChart.Location = New System.Drawing.Point(0, 25)
        Me.PieChart.Name = "PieChart"
        Me.PieChart.Size = New System.Drawing.Size(466, 372)
        Me.PieChart.TabIndex = 2
        Me.PieChart.Text = "PieChart"
        Me.PieChart.Visible = False
        '
        'CartesianChart
        '
        Me.CartesianChart.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CartesianChart.HynrSettings = Nothing
        Me.CartesianChart.Location = New System.Drawing.Point(0, 25)
        Me.CartesianChart.Margin = New System.Windows.Forms.Padding(15)
        Me.CartesianChart.Name = "CartesianChart"
        Me.CartesianChart.Size = New System.Drawing.Size(466, 372)
        Me.CartesianChart.TabIndex = 0
        Me.CartesianChart.Text = "CartesianChart"
        '
        'LabelStrip
        '
        Me.LabelStrip.HynrSettings = Nothing
        Me.LabelStrip.LabelText = "Chart"
        Me.LabelStrip.Location = New System.Drawing.Point(0, 0)
        Me.LabelStrip.Name = "LabelStrip"
        Me.LabelStrip.Size = New System.Drawing.Size(466, 25)
        Me.LabelStrip.TabIndex = 1
        Me.LabelStrip.Text = "HynrLabelStrip1"
        '
        'HynrLvChartPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PieChart)
        Me.Controls.Add(Me.CartesianChart)
        Me.Controls.Add(Me.LabelStrip)
        Me.Name = "HynrLvChartPanel"
        Me.Size = New System.Drawing.Size(466, 397)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents CartesianChart As HynrLvChart
    Friend WithEvents LabelStrip As HynrLabelStrip
    Public WithEvents PieChart As HynrLvPieChart
End Class
