Imports System.Windows.Forms

Public Class HynrTextBox
    Inherits TextBox
#Region "Properties"

#End Region

#Region "Methods"
    Public Sub BindToProperty(ByRef viewmodel As Object, ByVal propertyname As String, Optional ByVal updatemode As DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged)
        If viewmodel.[GetType]().GetProperties().Where(Function(prprt) prprt.Name = propertyname).Any Then
            DataBindings.Add("Text", viewmodel, propertyname, True, updatemode)
        End If
    End Sub
#End Region
End Class