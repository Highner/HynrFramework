Public Class PersonView
    Inherits HynrFramework.HynrItemViewForm

    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents TextBox1 As TextBox

    Public Sub New(ByRef settings As HynrFramework.HynrUISettings, ByRef datacontext As PersonItemViewModel)
        MyBase.New(settings, datacontext)
        InitializeComponent()
    End Sub

    Private Sub LoadMe() Handles Me.Load
        TextBox1.DataBindings.Add("Text", DataContext, "Name", True, DataSourceUpdateMode.OnPropertyChanged)
        CheckBox1.DataBindings.Add("Checked", DataContext, "CanSave", True, DataSourceUpdateMode.Never)
    End Sub

    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(75, 50)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(143, 20)
        Me.TextBox1.TabIndex = 0
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(101, 134)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(81, 17)
        Me.CheckBox1.TabIndex = 1
        Me.CheckBox1.Text = "CheckBox1"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'PersonView
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "PersonView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


End Class
