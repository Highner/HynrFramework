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
    Private _DefaultLabelstripForecolor As Color = Color.White



    Private _SelectedBackcolor As Color
    Public Property SelectedBackColor() As Color
        Get
            If (_SelectedBackcolor.Name = 0) Then
                Return _DefaultSelectedBackcolor
            Else
                Return _SelectedBackcolor
            End If
        End Get
        Set(ByVal value As Color)
            _SelectedBackcolor = value
        End Set
    End Property
    Private _SelectedForecolor As Color
    Public Property SelectedForecolor() As Color
        Get
            If (_SelectedForecolor.Name = 0) Then
                Return _DefaultSelectedForecolor
            Else
                Return _SelectedForecolor
            End If
        End Get
        Set(ByVal value As Color)
            _SelectedForecolor = value
        End Set
    End Property
    Private _GridColor As Color
    Public Property GridColor() As Color
        Get
            If (_GridColor.Name = 0) Then
                Return _DefaultGridColor
            Else
                Return _GridColor
            End If
        End Get
        Set(ByVal value As Color)
            _GridColor = value
        End Set
    End Property
    Private _GridBackcolor As Color
    Public Property GridBackcolor() As Color
        Get
            If (_GridBackcolor.Name = 0) Then
                Return _DefaultGridBackcolor
            Else
                Return _GridBackcolor
            End If
        End Get
        Set(ByVal value As Color)
            _GridBackcolor = value
        End Set
    End Property
    Private _CompanyColor As Color
    Public Property CompanyColor() As Color
        Get
            If (_CompanyColor.Name = 0) Then
                Return _DefaultCompanyColor
            Else
                Return _CompanyColor
            End If
        End Get
        Set(ByVal value As Color)
            _CompanyColor = value
        End Set
    End Property
    Private _LabelstripForecolor As Color
    Public Property LabelstripColor() As Color
        Get
            If (_LabelstripForecolor.Name = 0) Then
                Return _DefaultLabelstripForecolor
            Else
                Return _LabelstripForecolor
            End If
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
    Sub ApplyToControlContainer(ByRef container As ContainerControl)
        For Each control In container.Controls
            If TypeOf (control) Is IHasHynrSettings Then
                Dim hynrcontrol As IHasHynrSettings = control
                hynrcontrol.HynrSettings = Me
            End If
            If TypeOf (control) Is ContainerControl Then ApplyToControlContainer(control)
        Next
    End Sub
#End Region
End Class
