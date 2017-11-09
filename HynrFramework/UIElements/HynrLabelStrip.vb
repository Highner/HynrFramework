Imports System.Windows.Forms


Public Class HynrLabelStrip
    Inherits ToolStrip
    Implements IHasHynrSettings

#Region "PROPERTIES"
    Private Property _Label As New ToolStripLabel
    Public ReadOnly Property Label As ToolStripLabel
        Get
            Return _Label
        End Get
    End Property
    Public Property LabelText As String
        Get
            Return _Label.Text
        End Get
        Set(value As String)
            _Label.Text = value
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
    Private _IsSubHeader As Boolean = False
    Public Property IsSubHeader() As Boolean
        Get
            Return _IsSubHeader
        End Get
        Set(ByVal value As Boolean)
            _IsSubHeader = value
            If Not IsNothing(HynrSettings) Then ApplyHynrSettings()
        End Set
    End Property
    Private LazyBindingViewModel As IViewModelBase
    Private LazyBindingDataMember As String
    Private LazyBindingDisplayProperty As String
#End Region

#Region "METHODS"
    Public Sub New()
        MyBase.New()
        Items.Add(_Label)
    End Sub
    Public Sub ApplyHynrSettings() Implements IHasHynrSettings.ApplyHynrSettings
        If IsSubHeader Then
            BackColor = HynrSettings.SubHeaderColor
            _Label.ForeColor = Drawing.Color.Black
        Else
            BackColor = HynrSettings.CompanyColor
            _Label.ForeColor = HynrSettings.LabelstripColor
        End If

        RenderMode = ToolStripRenderMode.System
        GripStyle = ToolStripGripStyle.Hidden
    End Sub
    ''' <summary>
    ''' create string property in viewmodel as displayproperty
    ''' </summary>
    Public Sub BindToListViewModel(ByRef viewmodel As IViewModelBase, ByVal displayproperty As String)
        If viewmodel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = displayproperty).Any Then
            DataBindings.Add("LabelText", viewmodel, displayproperty, True, DataSourceUpdateMode.Never, String.Empty)
        End If
    End Sub
    Public Sub BindToListViewModel(ByRef viewmodel As IViewModelBase, ByVal datamember As String, ByVal displayproperty As String)
        LazyBindingDisplayProperty = displayproperty
        LazyBindingDataMember = datamember
        LazyBindingViewModel = viewmodel
        AddHandler viewmodel.LoadingCompleted, AddressOf CompleteBinding
    End Sub
    Private Sub CompleteBinding()
        DataBindings.Clear()
        If LazyBindingViewModel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = LazyBindingDataMember).Any Then
            DataBindings.Add("LabelText", LazyBindingViewModel, LazyBindingDataMember & "." & LazyBindingDisplayProperty, True, DataSourceUpdateMode.Never, String.Empty)
        End If
    End Sub
#End Region

#Region "EVENTS"

#End Region
End Class
