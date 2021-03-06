﻿Public Class HynrFilterListDialog(Of dataitem As ICheckboxFilterItem)
    Inherits HynrItemViewFormDialog
    Friend WithEvents FilterGridPanel As HynrFilterGridPanel

    Public Property ItemList As IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of dataitem))

    Public Sub New(ByRef datacontext As IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of dataitem)), ByVal header As String)
        MyBase.New(New HynrUISettings, datacontext)
        InitializeComponent()
        LabelStrip.LabelText = header
        ItemList = datacontext

        FilterGridPanel.AddGrid(Of dataitem)(ItemList, "Item", True)

    End Sub
    Public Sub New(ByRef datacontext As IFilterListViewModel(Of CheckboxFilterDataItemViewModel(Of dataitem)), ByVal header As String, ByVal enablecancel As Boolean)
        Me.New(datacontext, header)
        MyBase.CancelVisible = enablecancel
    End Sub

    Public Function GetDialogResult(ByVal resultproperty As String) As String()
        Dim viewmodel As ViewModelBase = ItemList
        viewmodel.RefreshAllCommand.Execute()
        ShowDialog()
        If DialogResult = System.Windows.Forms.DialogResult.OK AndAlso ItemList.CheckedItems.Any Then Return ItemList.CheckedItems.Select(Function(x) x.Data.GetType().GetProperty(resultproperty).GetValue(x.Data).ToString).ToArray Else Return Nothing
    End Function

    Public Function GetDialogResult() As IEnumerable(Of dataitem)
        Dim list As New List(Of dataitem)
        Dim viewmodel As ViewModelBase = ItemList
        viewmodel.RefreshAllCommand.Execute()
        ShowDialog()
        If DialogResult = System.Windows.Forms.DialogResult.OK AndAlso ItemList.CheckedItems.Any Then Return ItemList.CheckedItems.Select(Function(x) x.Data).ToList Else Return list
    End Function

    Private Sub InitializeComponent()
        Me.FilterGridPanel = New HynrFramework.HynrFilterGridPanel()
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
        'HynrFilterListDialog
        '
        Me.CancelVisible = True
        Me.ClientSize = New System.Drawing.Size(451, 340)
        Me.Controls.Add(Me.FilterGridPanel)
        Me.LabelStripVisible = True
        Me.Name = "HynrFilterListDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Controls.SetChildIndex(Me.ButtonCancel, 0)
        Me.Controls.SetChildIndex(Me.ButtonOK, 0)
        Me.Controls.SetChildIndex(Me.FilterGridPanel, 0)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class
