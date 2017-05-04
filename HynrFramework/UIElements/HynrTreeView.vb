Imports System.ComponentModel
Imports System.Windows.Forms

Public Class HynrTreeView(Of dataitem As IHasID, viewmodelitem As ItemViewModelBase(Of dataitem))
    Inherits TreeView
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
    Private WithEvents _BindingSource As New BindingSource
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
    Private _GroupHeader As String
    Public Property EnableDelete As Boolean = False
#End Region

#Region "Methods"
    Public Sub New()
        AddHandler _BindingSource.CurrentItemChanged, AddressOf SelectedItemChanged
        BusyIndicator.Height = 50
        BusyIndicator.Width = 50
        Controls.Add(BusyIndicator)
    End Sub
    Private Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        BackColor = HynrSettings.GridBackcolor
        BorderStyle = HynrSettings.GridBorderStyle
    End Sub
    Protected Sub OnPropertyChanged(ByVal strPropertyName As String)
        If PropertyChangedEvent IsNot Nothing Then
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(strPropertyName))
        End If
    End Sub
    Private Sub SelectedItemChanged()
        SelectedItem = _BindingSource.Current
    End Sub
    Private Sub ItemDoubleClicked() Handles Me.NodeMouseDoubleClick
        If Not IsNothing(SelectedItem) Then
            If FireItemDoubleClick = True Then
                SelectedItem.DoubleClickCommand.Execute(Nothing)
                RaiseEvent ItemDoubleClick(SelectedItem)
            End If
        End If
    End Sub
    Private Sub TreeviewClick(sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        SelectedNode = GetNodeAt(e.X, e.Y)
        Dim list As New List(Of viewmodelitem)
        If Not IsNothing(SelectedNode) Then
            If Not IsNothing(SelectedNode.Tag) Then
                list.AddRange(SelectedNode.Tag)
                Dim sellist As List(Of viewmodelitem) = SelectedNode.Tag
                _BindingSource.Position = _BindingSource.IndexOf(sellist.FirstOrDefault)
            End If
        End If
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
    Private Sub PopulateTreeView()
        If Not IsNothing(BindingSourceDataSource) Then
            Nodes.Clear()
            Dim ListOfObjectsSorted = (From g In BindingSourceDataSource Order By g.GetType().GetProperty(_GroupHeader).GetValue(g) Select g.GetType().GetProperty(_GroupHeader).GetValue(g)).Distinct
            For Each rootnodeName As String In ListOfObjectsSorted
                Dim list As List(Of viewmodelitem) = (From g In BindingSourceDataSource Where g.GetType().GetProperty(_GroupHeader).GetValue(g) = rootnodeName Select g).ToList
                Dim rootnode As New TreeNode(rootnodeName & " (" & list.Count & ")")
                rootnode.Tag = list
                Nodes.Add(rootnode)
                For Each childnodeItem In list
                    Dim childnode As New TreeNode(childnodeItem.Description)
                    Dim childlist As New List(Of viewmodelitem)
                    childlist.Add(childnodeItem)
                    childnode.Tag = childlist
                    rootnode.Nodes.Add(childnode)
                Next
            Next
        End If
    End Sub
    Private Sub DataSourceChanged() Handles _BindingSource.DataSourceChanged
        PopulateTreeView()
    End Sub
#End Region

#Region "Binding"
    Public TargetList As IEnumerable(Of viewmodelitem)
    Public Sub BindToList(ByRef targetlist As IEnumerable(Of viewmodelitem), ByVal groupheader As String)
        'Me.TargetList = targetlist
        BindingSourceDataSource = New ObservableListSource(Of viewmodelitem)
        'DataBindings.Add("BindingSourceDataSource", Me, "TargetList", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        _GroupHeader = groupheader
    End Sub

    Public Sub BindToListViewModel(ByRef listviewmodel As IViewModelBase, ByVal groupheader As String) ' IListViewModel(Of viewmodelitem))
        LazyBindingViewModel = listviewmodel
        _GroupHeader = groupheader
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
        AddHandler listviewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        DataBindings.Add("BindingSourceDataSource", LazyBindingViewModel, "ItemList", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItem", LazyBindingViewModel, "SelectedItem", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("SelectedItems", LazyBindingViewModel, "SelectedItems", True, DataSourceUpdateMode.OnPropertyChanged, Nothing)
        DataBindings.Add("IsBusy", LazyBindingViewModel, "IsBusy", True, DataSourceUpdateMode.Never, True)
    End Sub
#End Region

#Region "DragDrop/Interaction"
    Private Sub Treeviewtarget_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If EnableDelete = True Then
            If e.KeyCode = Keys.Delete Then
                Dim obj As List(Of viewmodelitem) = SelectedNode.Tag
                For Each item In obj
                    BindingSourceDataSource.Remove(item)
                Next
            End If
        End If
    End Sub
    Private Sub TreeViewTarget_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragEnter
        If e.Data.GetDataPresent(GetType(TreeNode)) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub
    Private Sub Treeview_ItemDrag(ByVal sender As Object, ByVal e As ItemDragEventArgs) Handles Me.ItemDrag
        DoDragDrop(SelectedNode, DragDropEffects.Move)
    End Sub
    Private Sub TreeViewTarget_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles Me.DragDrop
        If e.Data.GetDataPresent(GetType(TreeNode)) Then
            Dim item As TreeNode = e.Data.GetData(GetType(TreeNode))
            For Each newitem In TransformData(item)
                If Not BindingSourceDataSource.Contains(newitem) Then BindingSourceDataSource.Add(newitem)
            Next
            PopulateTreeView()
        End If
    End Sub
    ''' <summary>
    ''' overide this in order to transform the treenode into a list of this control's generic tag object (viewmodelitem)
    ''' </summary>
    ''' <param name="incomingobject"></param>
    ''' <returns></returns>
    Protected Overridable Function TransformData(ByRef incomingobject As TreeNode) As List(Of viewmodelitem)
        Return incomingobject.Tag
    End Function
#End Region

#Region "Events"
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
    Public Event ItemDoubleClick(ByRef item As viewmodelitem)
    Public Event LoadingCompleted() Implements IViewModelBase.LoadingCompleted
#End Region
End Class
