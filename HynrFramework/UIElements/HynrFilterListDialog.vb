Public Class HynrFilterListDialog(Of dataitem As ICheckboxFilterItem)
    Inherits HynrItemViewFormDialog

    Friend WithEvents LabelStrip As HynrLabelStrip
    Friend WithEvents FilterGridPanel As HynrFilterGridPanel

    Public Property ItemList As IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of dataitem))

    Public Sub New(ByRef datacontext As IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of dataitem)), ByVal header As String)
        MyBase.New(New HynrUISettings, datacontext)
        InitializeComponent()
        LabelStrip.LabelText = header
        ItemList = datacontext

        FilterGridPanel.AddGrid(Of dataitem)(ItemList, header, True)

    End Sub
    Public Sub New(ByRef datacontext As IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of dataitem)), ByVal header As String, ByVal enablecancel As Boolean)
        Me.New(datacontext, header)
        Me.ButtonCancel.Visible = enablecancel
    End Sub

    Private Sub InitializeComponent()
        Me.FilterGridPanel = New HynrFramework.HynrFilterGridPanel()
        Me.LabelStrip = New HynrFramework.HynrLabelStrip()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Location = New System.Drawing.Point(364, 305)
        '
        'ButtonOK
        '
        Me.ButtonOK.Location = New System.Drawing.Point(283, 305)
        '
        'FilterGridPanel
        '
        Me.FilterGridPanel.Grid = Nothing
        Me.FilterGridPanel.ListViewModel = Nothing
        Me.FilterGridPanel.Location = New System.Drawing.Point(12, 28)
        Me.FilterGridPanel.Name = "FilterGridPanel"
        Me.FilterGridPanel.Size = New System.Drawing.Size(427, 271)
        Me.FilterGridPanel.TabIndex = 2
        '
        'LabelStrip
        '
        Me.LabelStrip.HynrSettings = Nothing
        Me.LabelStrip.IsSubHeader = False
        Me.LabelStrip.LabelText = ""
        Me.LabelStrip.Location = New System.Drawing.Point(0, 0)
        Me.LabelStrip.Name = "LabelStrip"
        Me.LabelStrip.Size = New System.Drawing.Size(451, 25)
        Me.LabelStrip.TabIndex = 3
        Me.LabelStrip.Text = "HynrLabelStrip1"
        '
        'HynrFilterListDialog
        '
        Me.ClientSize = New System.Drawing.Size(451, 340)
        Me.Controls.Add(Me.LabelStrip)
        Me.Controls.Add(Me.FilterGridPanel)
        Me.Name = "HynrFilterListDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Controls.SetChildIndex(Me.ButtonCancel, 0)
        Me.Controls.SetChildIndex(Me.ButtonOK, 0)
        Me.Controls.SetChildIndex(Me.FilterGridPanel, 0)
        Me.Controls.SetChildIndex(Me.LabelStrip, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
