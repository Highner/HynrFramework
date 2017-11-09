Imports System.Drawing
Imports System.Windows.Forms

Public Class HynrUISettings
#Region "COLORS"
    Private _DefaultBackground As Color = Color.White
    Private _DefaultSelectedBackcolor As Color = Color.AliceBlue
    Private _DefaultSelectedForecolor As Color = Color.Black
    Private _DefaultMarked As Color = Color.LightYellow
    Private _DefaultGridColor As Color = Color.LightGray
    Private _DefaultGridBackcolor As Color = Color.White
    Private _DefaultCompanyColor As Color = Color.IndianRed
    Private _DefaultSubHeaderColor As Color = Color.LightGray
    Private _DefaultLabelstripForecolor As Color = Color.White

    Private _SelectedBackcolor As Color = _DefaultSelectedBackcolor
    Public Property SelectedBackColor() As Color
        Get
            Return _SelectedBackcolor
        End Get
        Set(ByVal value As Color)
            _SelectedBackcolor = value
        End Set
    End Property
    Private _SelectedForecolor As Color = _DefaultSelectedForecolor
    Public Property SelectedForecolor() As Color
        Get
            Return _SelectedForecolor
        End Get
        Set(ByVal value As Color)
            _SelectedForecolor = value
        End Set
    End Property
    Private _GridColor As Color = _DefaultGridColor
    Public Property GridColor() As Color
        Get
            Return _GridColor
        End Get
        Set(ByVal value As Color)
            _GridColor = value
        End Set
    End Property
    Private _GridBackcolor As Color = _DefaultGridBackcolor
    Public Property GridBackcolor() As Color
        Get
            Return _GridBackcolor
        End Get
        Set(ByVal value As Color)
            _GridBackcolor = value
        End Set
    End Property
    Private _CompanyColor As Color = _DefaultCompanyColor
    Public Property CompanyColor() As Color
        Get
            Return _CompanyColor
        End Get
        Set(ByVal value As Color)
            _CompanyColor = value
        End Set
    End Property
    Private _SubHeaderColor As Color = _DefaultSubHeaderColor
    Public Property SubHeaderColor() As Color
        Get
            Return _SubHeaderColor
        End Get
        Set(ByVal value As Color)
            _SubHeaderColor = value
        End Set
    End Property
    Private _LabelstripForecolor As Color = _DefaultLabelstripForecolor
    Public Property LabelstripColor() As Color
        Get
            Return _LabelstripForecolor
        End Get
        Set(ByVal value As Color)
            _LabelstripForecolor = value
        End Set
    End Property
#End Region

#Region "FONT"
    Private _DefaultFontSize As Integer = 11
    Private _DefaultFontName As String = "Arial"
#End Region

#Region "CONTROLS"
#Region "HYNRGRID"
    Public RowHeadersVisible As Boolean = False
    Public GridBorderStyle As BorderStyle = BorderStyle.None
    Public GridColumHeaderBorderStyle As ColumnHeaderStyle = ColumnHeaderStyle.Nonclickable
#End Region

#End Region

#Region "METHODS"
    Public Sub ApplyToControlContainer(ByRef container As ContainerControl)
        For Each control In container.Controls
            If TypeOf (control) Is IHasHynrSettings Then
                Dim hynrcontrol As IHasHynrSettings = control
                hynrcontrol.HynrSettings = Me
            End If
            If TypeOf (control) Is ContainerControl Then ApplyToControlContainer(control)
            If TypeOf (control) Is SplitContainer Then
                Dim split As SplitContainer = control
                For Each splitcontrol In split.Panel1.Controls
                    If TypeOf (splitcontrol) Is IHasHynrSettings Then
                        Dim hynrcontrol As IHasHynrSettings = splitcontrol
                        hynrcontrol.HynrSettings = Me
                    End If
                Next
                For Each splitcontrol In split.Panel2.Controls
                    If TypeOf (splitcontrol) Is IHasHynrSettings Then
                        Dim hynrcontrol As IHasHynrSettings = splitcontrol
                        hynrcontrol.HynrSettings = Me
                    End If
                Next
            End If
        Next
    End Sub
#End Region
End Class
