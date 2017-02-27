Imports System.Drawing
Imports System.Windows.Forms

Public Class MatrixCircularProgressControl
    Inherits UserControl
#Region "Constants"

    Private Const DEFAULT_INTERVAL As Integer = 60
    Private ReadOnly DEFAULT_TICK_COLOR As Color = Color.FromArgb(58, 58, 58)
    Private Const DEFAULT_TICK_WIDTH As Integer = 2
    Private Const MINIMUM_INNER_RADIUS As Integer = 4
    Private Const MINIMUM_OUTER_RADIUS As Integer = 8
    Private MINIMUM_CONTROL_SIZE As New Size(28, 28)
    Private Const MINIMUM_PEN_WIDTH As Integer = 2
    Private Const INNER_RADIUS_FACTOR As Single = 0.175F
    Private Const OUTER_RADIUS_FACTOR As Single = 0.3125F

#End Region

#Region "Enums"

    Public Enum Direction
        CLOCKWISE
        ANTICLOCKWISE
    End Enum

#End Region

#Region "Structs"

    Private Structure Spoke
        Public StartPoint As PointF
        Public EndPoint As PointF

        Public Sub New(pt1 As PointF, pt2 As PointF)
            StartPoint = pt1
            EndPoint = pt2
        End Sub
    End Structure

#End Region

#Region "Members"

    Private m_Interval As Integer
    Private m_Pen As Pen = Nothing
    Private m_CentrePt As New PointF()
    Private m_InnerRadius As Integer = 0
    Private m_OuterRadius As Integer = 0
    Private m_SpokesCount As Integer = 0
    Private m_AlphaChange As Integer = 0
    Private m_AlphaLowerLimit As Integer = 0
    Private m_StartAngle As Single = 0
    Private m_AngleIncrement As Single = 0
    Private m_Rotation As Direction
    Private m_Timer As System.Timers.Timer = Nothing
    Private m_Spokes As List(Of Spoke) = Nothing

#End Region

#Region "Properties"

    ''' <summary>
    ''' Time interval for each tick.
    ''' </summary>
    Public Property Interval() As Integer
        Get
            Return m_Interval
        End Get
        Set
            If Value > 0 Then
                m_Interval = Value
            Else
                m_Interval = DEFAULT_INTERVAL
            End If
        End Set
    End Property

    ''' <summary>
    ''' Color of the tick
    ''' </summary>
    Public Property TickColor() As Color
        Get
            Return m_TickColor
        End Get
        Set
            m_TickColor = Value
        End Set
    End Property
    Private m_TickColor As Color

    ''' <summary>
    ''' Direction of rotation - CLOCKWISE/ANTICLOCKWISE
    ''' </summary>
    Public Property Rotation() As Direction
        Get
            Return m_Rotation
        End Get
        Set
            m_Rotation = Value
            CalculateSpokesPoints()
        End Set
    End Property

    ''' <summary>
    ''' Angle at which the tick should start
    ''' </summary>
    Public Property StartAngle() As Single
        Get
            Return m_StartAngle
        End Get
        Set
            m_StartAngle = Value
        End Set
    End Property

#End Region

#Region "Construction/Initialization"

    ''' <summary>
    ''' Ctor
    ''' </summary>
    Public Sub New()
        Me.DoubleBuffered = True

        InitializeComponent()

        ' Set Default Values
        Me.BackColor = Color.Transparent
        Me.TickColor = DEFAULT_TICK_COLOR
        Me.MinimumSize = MINIMUM_CONTROL_SIZE
        Me.Interval = DEFAULT_INTERVAL
        ' Default starting angle is 12 o'clock
        Me.StartAngle = 270
        ' Default number of Spokes in this control is 12
        m_SpokesCount = 12
        ' Set the Lower limit of the Alpha value (The spokes will be shown in 
        ' alpha values ranging from 255 to m_AlphaLowerLimit)
        m_AlphaLowerLimit = 15

        ' Create the Pen
        m_Pen = New Pen(TickColor, DEFAULT_TICK_WIDTH)
        m_Pen.EndCap = System.Drawing.Drawing2D.LineCap.Round
        m_Pen.StartCap = System.Drawing.Drawing2D.LineCap.Round

        ' Default rotation direction is clockwise
        Me.Rotation = Direction.CLOCKWISE

        ' Calculate the Spoke Points
        CalculateSpokesPoints()

        m_Timer = New System.Timers.Timer(Me.Interval)
        AddHandler m_Timer.Elapsed, AddressOf OnTimerElapsed
    End Sub

    ''' <summary>
    ''' Calculate the Spoke Points and store them
    ''' </summary>
    Private Sub CalculateSpokesPoints()
        m_Spokes = New List(Of Spoke)()

        ' Calculate the angle between adjacent spokes
        m_AngleIncrement = (360 / CSng(m_SpokesCount))
        ' Calculate the change in alpha between adjacent spokes
        m_AlphaChange = CInt((255 - m_AlphaLowerLimit) / m_SpokesCount)

        ' Calculate the location around which the spokes will be drawn
        Dim width As Integer = If((Me.Width < Me.Height), Me.Width, Me.Height)
        m_CentrePt = New PointF(Me.Width / 2, Me.Height / 2)
        ' Calculate the width of the pen which will be used to draw the spokes
        m_Pen.Width = CInt(width / 15)
        If m_Pen.Width < MINIMUM_PEN_WIDTH Then
            m_Pen.Width = MINIMUM_PEN_WIDTH
        End If
        ' Calculate the inner and outer radii of the control. The radii should not be less than the
        ' Minimum values
        m_InnerRadius = CInt(width * INNER_RADIUS_FACTOR)
        If m_InnerRadius < MINIMUM_INNER_RADIUS Then
            m_InnerRadius = MINIMUM_INNER_RADIUS
        End If
        m_OuterRadius = CInt(width * OUTER_RADIUS_FACTOR)
        If m_OuterRadius < MINIMUM_OUTER_RADIUS Then
            m_OuterRadius = MINIMUM_OUTER_RADIUS
        End If

        Dim angle As Single = 0

        For i As Integer = 0 To m_SpokesCount - 1
            Dim pt1 As New PointF(m_InnerRadius * CSng(Math.Cos(ConvertDegreesToRadians(angle))), m_InnerRadius * CSng(Math.Sin(ConvertDegreesToRadians(angle))))
            Dim pt2 As New PointF(m_OuterRadius * CSng(Math.Cos(ConvertDegreesToRadians(angle))), m_OuterRadius * CSng(Math.Sin(ConvertDegreesToRadians(angle))))

            ' Create a spoke based on the points generated
            Dim spoke As New Spoke(pt1, pt2)
            ' Add the spoke to the List
            m_Spokes.Add(spoke)

            If Rotation = Direction.CLOCKWISE Then
                angle -= m_AngleIncrement
            ElseIf Rotation = Direction.ANTICLOCKWISE Then
                angle += m_AngleIncrement
            End If
        Next
    End Sub

#End Region

#Region "EventHandlers"

    ''' <summary>
    ''' Handler for the event when the size of the control changes
    ''' </summary>
    ''' <param name="e">EventArgs</param>
    Protected Overrides Sub OnClientSizeChanged(e As EventArgs)
        CalculateSpokesPoints()
    End Sub

    ''' <summary>
    ''' Handle the Tick event
    ''' </summary>
    ''' <param name="sender">Timer</param>
    ''' <param name="e">ElapsedEventArgs</param>
    Private Sub OnTimerElapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        If Rotation = Direction.CLOCKWISE Then
            m_StartAngle += m_AngleIncrement

            If m_StartAngle >= 360 Then
                m_StartAngle = 0
            End If
        ElseIf Rotation = Direction.ANTICLOCKWISE Then
            m_StartAngle -= m_AngleIncrement

            If m_StartAngle <= -360 Then
                m_StartAngle = 0
            End If
        End If

        Invalidate()
    End Sub


    ''' <summary>
    ''' Handles the Paint Event of the control
    ''' </summary>
    ''' <param name="e">PaintEventArgs</param>
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality

        ' Perform a Translation so that the centre of the control is the centre of Rotation
        e.Graphics.TranslateTransform(m_CentrePt.X, m_CentrePt.Y, System.Drawing.Drawing2D.MatrixOrder.Prepend)
        ' Perform a Rotation about the control's centre
        e.Graphics.RotateTransform(m_StartAngle, System.Drawing.Drawing2D.MatrixOrder.Prepend)

        Dim alpha As Integer = 255

        ' Render the spokes
        For i As Integer = 0 To m_SpokesCount - 1
            m_Pen.Color = Color.FromArgb(alpha, Me.TickColor)
            e.Graphics.DrawLine(m_Pen, m_Spokes(i).StartPoint, m_Spokes(i).EndPoint)

            alpha -= m_AlphaChange
            If alpha < m_AlphaLowerLimit Then
                alpha = 255 - m_AlphaChange
            End If
        Next

        ' Perform a reverse Rotation and Translation to obtain the original Transformation
        e.Graphics.RotateTransform(-m_StartAngle, System.Drawing.Drawing2D.MatrixOrder.Append)
        e.Graphics.TranslateTransform(-m_CentrePt.X, -m_CentrePt.Y, System.Drawing.Drawing2D.MatrixOrder.Append)
    End Sub

#End Region

#Region "Helpers"

    ''' <summary>
    ''' Converts Degrees to Radians
    ''' </summary>
    ''' <param name="degrees">Degrees</param>
    ''' <returns></returns>
    Private Function ConvertDegreesToRadians(degrees As Single) As Double
        Return ((Math.PI / CDbl(180)) * degrees)
    End Function

#End Region

#Region "APIs"

    ''' <summary>
    ''' Start the Tick Control rotation
    ''' </summary>
    Public Sub Start()
        If m_Timer IsNot Nothing Then
            m_Timer.Interval = Me.Interval
            m_Timer.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Stop the Tick Control rotation
    ''' </summary>
    Public Sub [Stop]()
        If m_Timer IsNot Nothing Then
            m_Timer.Enabled = False
        End If
    End Sub

#End Region
End Class

