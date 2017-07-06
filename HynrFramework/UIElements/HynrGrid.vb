Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms
Imports HynrFramework
Imports Microsoft.Office.Interop

Public Class HynrGrid(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits DataHelper.DataGridViewSummary.DataGridViewSummary
    Implements IBindableListControl(Of dataitem, viewmodelitem)
    Implements INotifyPropertyChanged
    Implements IHasHynrSettings

#Region "Properties"
    Private Property _SelectedItems As New List(Of viewmodelitem)
    Property SelectedItems As List(Of viewmodelitem)
        Get
            Return _SelectedItems
        End Get
        Set(value As List(Of viewmodelitem))
            _SelectedItems = value
            OnPropertyChanged("SelectedItems")
        End Set
    End Property
    Private Property _BindingSource As New BindingSource
    Public Property BindingSourceDataSource As ObservableListSource(Of viewmodelitem)
        Get
            Return _BindingSource.DataSource
        End Get
        Set(value As ObservableListSource(Of viewmodelitem))
            If Not IsNothing(value) Then
                _BindingSource.DataSource = value
            End If
        End Set
    End Property
    Private Property _SelectedItem As viewmodelitem
    Public Property SelectedItem As viewmodelitem Implements IBindableListControl(Of dataitem, viewmodelitem).SelectedItem
        Get
            Return _SelectedItem
        End Get
        Set(value As viewmodelitem)
            If _BindingSource.Count > 0 Then
                If Not IsNothing(value) Then
                    _BindingSource.Position = _BindingSource.IndexOf(value)
                    _SelectedItem = value
                    OnPropertyChanged("SelectedItem")
                End If
            End If
        End Set
    End Property
    Private _HynrSettings As HynrUISettings
    Public Property HynrSettings As HynrUISettings Implements IHasHynrSettings.HynrSettings
        Get
            Return _HynrSettings
        End Get
        Set(value As HynrUISettings)
            If Not IsNothing(value) Then
                _HynrSettings = value
                ApplyHynrSettings()
            End If
        End Set
    End Property
    Private _IsBusy As Boolean
    Public Property IsBusy() As Boolean Implements IBindableListControl(Of dataitem, viewmodelitem).IsBusy
        Get
            Return _IsBusy
        End Get
        Set(ByVal value As Boolean)
            _IsBusy = value
            ToggleBusyIndicator(_IsBusy)
            OnPropertyChanged("IsBusy")
        End Set
    End Property
    Property FireItemDoubleClick As Boolean = False
    Property CancellationSource As Threading.CancellationTokenSource Implements IBindableListControl(Of dataitem, viewmodelitem).CancellationSource
    Private BusyIndicator As New MatrixCircularProgressControl
    Private LazyBindingViewModel As IViewModelBase
    Public Property FileDropToItemOnly As Boolean = True
    Private _BackColor As Color
    Private _SelectionColor As Color
#End Region

#Region "Methods"
    Public Sub New()
        DataSource = _BindingSource
        AddHandler _BindingSource.CurrentItemChanged, AddressOf SelectedItemChanged
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        Controls.Add(BusyIndicator)
    End Sub
    Private Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        DefaultCellStyle.SelectionBackColor = HynrSettings.SelectedBackColor
        DefaultCellStyle.SelectionForeColor = HynrSettings.SelectedForecolor
        GridColor = HynrSettings.GridColor
        BackgroundColor = HynrSettings.GridBackcolor
        RowHeadersVisible = HynrSettings.RowHeadersVisible
        BorderStyle = HynrSettings.GridBorderStyle
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If Me.PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub SelectedItemChanged()
        SelectedItem = _BindingSource.Current
    End Sub
    Private Sub ItemDoubleClicked() Handles Me.MouseDoubleClick
        If Not IsNothing(SelectedItem) Then
            SelectedItem.DoubleClickCommand.Execute(Nothing)
        End If
        RaiseEvent ItemDoubleClick(SelectedItem)
    End Sub
    Private Sub SelectedItemsChanged() Handles Me.SelectionChanged
        Dim list As New List(Of viewmodelitem)
        For Each row As DataGridViewRow In Me.SelectedRows
            list.Add(row.DataBoundItem)
        Next
        SelectedItems = list
    End Sub
    Private Sub ToggleBusyIndicator(ByVal busy As Boolean)
        Enabled = Not busy
        If busy Then
            BusyIndicator.StartAngle = 30
            BusyIndicator.Show()
            BusyIndicator.Start()
        Else
            BusyIndicator.Stop()
            BusyIndicator.Hide()
        End If
    End Sub
    Private Sub AdjustBusyIndicatorLocation() Handles Me.SizeChanged
        Dim x As Integer = Me.Width / 2 - BusyIndicator.Width / 2
        Dim y As Integer = Me.Height / 2 - BusyIndicator.Height / 2
        BusyIndicator.Location = New Drawing.Point(x, y)
    End Sub
    Private Sub Grid_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles Me.CurrentCellDirtyStateChanged
        If TypeOf (CurrentCell) Is DataGridViewCheckBoxCell Then
            If IsCurrentCellDirty Then
                CommitEdit(DataGridViewDataErrorContexts.Commit)
            End If
        End If
    End Sub
    Private Sub EnterComboboxNullvalue(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If Not IsNothing(CurrentCell) Then
            Dim items = {"UltraDateTimeEditorColumn", "DataGridViewComboBoxColumn", "UltraDataGridViewCell"}
            If e.KeyCode = Keys.Delete And CurrentCell.ReadOnly = False And items.Contains(CurrentCell.OwningColumn.CellType.Name) Then
                CurrentCell.Value = Nothing
            End If
        End If
    End Sub
#End Region

#Region "FileDrop"
    Private Sub FileDragDrop(sender As System.Object, e As System.Windows.Forms.DragEventArgs) 'Handles Me.DragDrop
        Dim files() As Object = e.Data.GetData(DataFormats.FileDrop)
        If files.Length = 1 Then
            Dim cursorLocation As Point = Me.PointToClient(New Point(e.X, e.Y))
            Dim hittest As System.Windows.Forms.DataGridView.HitTestInfo = Me.HitTest(cursorLocation.X, cursorLocation.Y)

            If hittest.ColumnIndex <> -1 AndAlso hittest.RowIndex <> -1 Then
                RaiseEvent FileDropped(Me.Rows(hittest.RowIndex).DataBoundItem, files(0))
            End If


        End If
    End Sub
    Private Sub FileDrop_DragOver(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver
        If Me.AllowDrop Then
            Dim clientPoint As Point = Me.PointToClient(New Point(e.X, e.Y))
            Dim aktRow As Integer = Me.HitTest(clientPoint.X, clientPoint.Y).RowIndex
            For Each dRow As DataGridViewRow In Me.Rows
                dRow.DefaultCellStyle.BackColor = Color.White
            Next
            If aktRow <> -1 Then
                Me.Rows(aktRow).DefaultCellStyle.BackColor = Color.LightGray
            End If
        End If
    End Sub
    Private Sub FileDrop_DragLeave() Handles Me.DragLeave
        Me.DefaultCellStyle.BackColor = _BackColor
        Me.DefaultCellStyle.SelectionBackColor = _SelectionColor
    End Sub

    Private Sub FileDragEnter(sender As System.Object, e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter
        _BackColor = Me.DefaultCellStyle.BackColor
        _SelectionColor = Me.DefaultCellStyle.SelectionBackColor
        If e.Data.GetDataPresent(DataFormats.FileDrop) Or e.Data.GetDataPresent("FileGroupDescriptor") Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub HandleFileDrops(sender As System.Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop
        Dim cursorLocation As Point = Me.PointToClient(New Point(e.X, e.Y))
        Dim hittest As System.Windows.Forms.DataGridView.HitTestInfo = Me.HitTest(cursorLocation.X, cursorLocation.Y)
        Dim item As viewmodelitem = Nothing
        If FileDropToItemOnly Then
            If hittest.ColumnIndex <> -1 AndAlso hittest.RowIndex <> -1 Then
                item = Me.Rows(hittest.RowIndex).DataBoundItem
            Else
                Exit Sub
            End If
        End If

        Try
            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                ' We have a file so lets pass it to the calling form
                Dim Filename As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
                'HandleFileDrops = Filename(0)
                RaiseEvent FileDropped(item, Filename(0))
            ElseIf e.Data.GetDataPresent("FileGroupDescriptor") Then
                ' We have a embedded file. First lets try to get the file name out of memory
                Dim theStream As Stream = CType(e.Data.GetData("FileGroupDescriptor"), Stream)
                Dim fileGroupDescriptor(512) As Byte
                theStream.Read(fileGroupDescriptor, 0, 512)
                Dim fileName As System.Text.StringBuilder = New System.Text.StringBuilder("")
                Dim i As Integer = 76
                While Not (fileGroupDescriptor(i) = 0)
                    fileName.Append(Convert.ToChar(fileGroupDescriptor(i)))
                    System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
                End While
                theStream.Close()
                ' We should have the file name or if its a email the subject line. Create our temp file based on the temp path and this info
                Dim myTempFile As String = Path.GetTempPath & fileName.ToString
                ' Look to see if this is a email message. If so save that temporarily and get the temp file.
                If InStr(myTempFile, ".msg") > 0 Then
                    Dim objOL As New Microsoft.Office.Interop.Outlook.Application
                    Dim objMI As Microsoft.Office.Interop.Outlook.MailItem
                    If objOL.ActiveExplorer.Selection.Count > 1 Then
                        MsgBox("You can only drag and drop one item at a time into this screen. The first item you selected will be used.", "One Item At A Time")
                    End If
                    For Each objMI In objOL.ActiveExplorer.Selection()
                        objMI.SaveAs(myTempFile)
                        Exit For
                    Next
                    objOL = Nothing
                    objMI = Nothing
                Else
                    ' If its a attachment we need to pull the file itself out of memory
                    Dim ms As MemoryStream = CType(e.Data.GetData("FileContents", True), MemoryStream)
                    Dim FileBytes(CInt(ms.Length)) As Byte
                    ' read the raw data into our variable
                    ms.Position = 0
                    ms.Read(FileBytes, 0, CInt(ms.Length))
                    ms.Close()
                    ' save the raw data into our temp file
                    Dim fs As FileStream = New FileStream(myTempFile, FileMode.OpenOrCreate, FileAccess.Write)
                    fs.Write(FileBytes, 0, FileBytes.Length)
                    fs.Close()
                End If
                ' Make sure we have a actual file and also if we do make sure we erase it when done
                If File.Exists(myTempFile) Then
                    ' Assign the file name to the add dialog
                    'HandleFileDrops = myTempFile
                    RaiseEvent FileDropped(item, myTempFile)
                    'Call AddTempFileToArray(myTempFile)
                End If
            Else
                Throw New System.Exception("An exception has occurred.")
            End If
        Catch ex As Exception
            MsgBox("Could not copy file from memory. Please save the file to your hard drive first and then retry your drag and drop.", "Drag and Drop Failed")
        End Try
        FileDrop_DragLeave()
    End Sub


#End Region

#Region "Binding"
    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase)
        LazyBindingViewModel = listviewmodel
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        AddHandler listviewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        DataBindings.Add("BindingSourceDataSource", LazyBindingViewModel, "ItemList", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItem", LazyBindingViewModel, "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItems", LazyBindingViewModel, "SelectedItems", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        'necessary for the summary grid
        MyBase.OnColumnAdded(New DataGridViewColumnEventArgs(Me.Columns(Me.Columns.Count - 1)))
    End Sub
    Public Sub BindGridCombobox(ByRef columnname As String, ByRef datasource As Object, ByVal datapropertyname As String, ByVal valuemember As String, ByVal displaymember As String)
        Dim col As DataGridViewComboBoxColumn = Columns(columnname)
        col.DataSource = New BindingSource(datasource, String.Empty)
        col.DataPropertyName = datapropertyname
        col.ValueMember = valuemember
        col.DisplayMember = displaymember
    End Sub
    Private Sub OnBindingComplete() Handles Me.DataBindingComplete
        If ColumnCount = 0 Then AutoGenerateColumns = True
    End Sub
    Private Sub view_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles Me.DataError
        If e.Exception.Message = "DataGridViewComboBoxCell value is not valid." Then
            Dim value As Object = Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            If Not DirectCast(Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Contains(value) Then
                DirectCast(Columns(e.ColumnIndex), DataGridViewComboBoxColumn).Items.Add(value)
                e.ThrowException = False
            End If
        End If

    End Sub
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ItemDoubleClick(ByRef item As viewmodelitem)
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
    Public Event FileDropped(ByRef item As viewmodelitem, ByRef data As Object)
#End Region
End Class