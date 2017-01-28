Public Class PersonView
    Inherits HynrFramework.HynrItemViewForm(Of PersonData, PersonItemViewModel)

    Friend WithEvents TextBox1 As TextBox

    Public Sub New(ByRef settings As HynrFramework.HynrUISettings, ByRef datacontext As PersonsListViewModel)
        MyBase.New(settings, datacontext)
        InitializeComponent()
    End Sub

    Private Sub LoadMe() Handles Me.Load
        TextBox1.DataBindings.Add("Text", SelectedItem, "Name", True, DataSourceUpdateMode.OnPropertyChanged)
    End Sub

    Private Sub InitializeComponent()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(75, 50)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(143, 20)
        Me.TextBox1.TabIndex = 0
        '
        'PersonView
        '
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "PersonView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub


End Class
