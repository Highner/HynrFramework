Imports System.ComponentModel
Imports System.Data.Entity
Imports System.Data.OleDb
Imports System.Drawing
Imports System.Windows.Forms

Public Module HynrHelpers
    Public Function EqualValues(ByVal original As Object, ByVal changed As Object) As Boolean
        If Not IsNothing(original) AndAlso Not IsNothing(changed) Then
            Dim destProperties = changed.[GetType]().GetProperties()
            Dim originalProperties = original.[GetType]().GetProperties()
            For Each prop In originalProperties
                Dim destprop = (From p In destProperties Where p.Name = prop.Name).FirstOrDefault
                If Not (destprop.GetValue(changed) Is prop.GetValue(original)) Then Return False
            Next
        End If
        Return True
    End Function
    Public Function DataGridViewToDataTable(ByVal dgv As DataGridView) As DataTable
        Dim table As New DataTable
        Try
            Dim col As DataColumn
            For Each dgvCol As DataGridViewColumn In dgv.Columns
                col = New DataColumn(dgvCol.HeaderText)
                table.Columns.Add(col)
            Next
            'Add Rows from the datagridview
            Dim row As DataRow
            Dim colcount As Integer = dgv.Columns.Count - 1
            For i As Integer = 0 To dgv.Rows.Count - 1
                row = table.Rows.Add
                For Each column As DataGridViewColumn In dgv.Columns
                    row.Item(column.Index) = dgv.Rows.Item(i).Cells(column.Index).FormattedValue
                Next
            Next
            Return table
        Catch ex As Exception
            MsgBox("CRITICAL ERROR : Exception caught while converting dataGridView to DataSet... " & Chr(10) & ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function ExcelFileToDatatable(filePath As String) As DataTable
        Dim dtexcel As New DataTable()
        Try
            Dim hasHeaders As Boolean = False
            Dim HDR As String = If(hasHeaders, "Yes", "No")
            Dim strConn As String
            If filePath.Substring(filePath.LastIndexOf("."c)).ToLower() = ".xlsx" Then
                strConn = (Convert.ToString((Convert.ToString("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=") & filePath) + ";Extended Properties=""Excel 12.0;HDR=") & HDR) + ";IMEX=0"""
            Else
                strConn = (Convert.ToString((Convert.ToString("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=") & filePath) + ";Extended Properties=""Excel 8.0;HDR=") & HDR) + ";IMEX=0"""
            End If
            Dim conn As New OleDbConnection(strConn)
            conn.Open()
            Dim schemaTable As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            Dim schemaRow As DataRow = schemaTable.Rows(0)
            Dim sheet As String = schemaRow("TABLE_NAME").ToString()
            If Not sheet.EndsWith("_") Then
                Dim query As String = "SELECT  * FROM [" & sheet & "]"
                Dim daexcel As New OleDbDataAdapter(query, conn)
                dtexcel.Locale = Globalization.CultureInfo.CurrentCulture
                daexcel.Fill(dtexcel)
            End If

            conn.Close()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dtexcel
    End Function
    Public Function GetFileFullPath(ByRef path As String) As Boolean
        Dim fd As OpenFileDialog = New OpenFileDialog()
        fd.Title = "Choose Excel File"
        fd.InitialDirectory = "C:\"
        fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True
        If fd.ShowDialog() = DialogResult.OK Then
            path = fd.FileName
            Return True
        Else
            Return False
        End If
    End Function
    Public Function GetGenericTypes(obj As Type) As Type()
        Dim generic As Type = obj.GetGenericTypeDefinition()
        Dim typeArguments As Type() = generic.GenericTypeArguments()
        Return typeArguments
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function ListToDataTable(Of T)(data As IList(Of T)) As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim dt As New DataTable()
        Dim indexlist As New List(Of Integer)
        For i As Integer = 0 To properties.Count - 1
            Dim [property] As PropertyDescriptor = properties(i)
            If [property].IsBrowsable = True Then
                ' Dim col As New DataColumn(,,, MappingType.Hidden)
                dt.Columns.Add([property].DisplayName, If(Nullable.GetUnderlyingType([property].PropertyType), [property].PropertyType))
                indexlist.Add(i)
            End If
        Next
        Dim values As Object() = New Object(indexlist.Count - 1) {}
        For Each item As T In data
            For i As Integer = 0 To values.Length - 1
                Try
                    values(i) = properties(indexlist(i)).GetValue(item)
                Catch
                    values(i) = Nothing
                End Try
            Next
            dt.Rows.Add(values)
        Next
        Return dt
    End Function
    <System.Runtime.CompilerServices.Extension>
    Public Function ListToDataTable(Of T)(data As IList(Of T), ByVal headerlist As List(Of String())) As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim dt As New DataTable()
        Dim indexlist As New List(Of Integer)
        Dim headerlistnames As New List(Of String)
        Dim headerlistcaptions As New List(Of String)
        For Each s In headerlist
            headerlistnames.Add(s(0))
            headerlistcaptions.Add(s(1))
        Next
        For i As Integer = 0 To properties.Count - 1
            Dim [property] As PropertyDescriptor = properties(i)
            If headerlistnames.Contains([property].Name) Then
                dt.Columns.Add(headerlistcaptions(headerlistnames.IndexOf([property].Name)), [property].PropertyType)
                indexlist.Add(i)
            End If
        Next
        Dim values As Object() = New Object(headerlist.Count - 1) {}
        For Each item As T In data
            For i As Integer = 0 To values.Length - 1
                Try
                    values(i) = properties(indexlist(i)).GetValue(item)
                Catch
                    values(i) = Nothing
                End Try
            Next
            dt.Rows.Add(values)
        Next
        Return dt
    End Function

    Public Function ConvertTextToImage(ByVal txt As String, ByVal fontname As String, ByVal fontsize As Integer, ByVal bgcolor As Color, ByVal fcolor As Color) As Bitmap
        Dim bmp As Bitmap = New Bitmap(1, 1)
        Dim graphics As Graphics = Graphics.FromImage(bmp)
        Dim font As Font = New Font(fontname, fontsize)
        Dim stringSize As SizeF = graphics.MeasureString(txt, font)
        bmp = New Bitmap(bmp, CInt(stringSize.Width + 2), CInt(stringSize.Height))
        graphics = Graphics.FromImage(bmp)
        Dim backcolor As Color
        If Not IsNothing(bgcolor) Then
            backcolor = bgcolor
        Else
            backcolor = Color.White
        End If
        graphics.FillRectangle(New SolidBrush(backcolor), 0, 0, bmp.Width, bmp.Height)
        graphics.DrawString(txt, font, New SolidBrush(fcolor), 1, 0)
        font.Dispose()
        graphics.Flush()
        graphics.Dispose()
        Return bmp
    End Function

    Public Function ImagesToSingleImage(images As Bitmap()) As Bitmap

        Dim outputImageHeight As Integer = images.Max(Function(x) x.Height)
        Dim outputImageWidth As Integer = images.Sum(Function(x) x.Width) + (images.Count - 1) * 2
        Dim outputImage As Bitmap = New Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        Dim pnt As New PointF(0, 0)

        Using g As Graphics = Graphics.FromImage(outputImage)
            For Each i As Bitmap In images
                g.DrawImage(i, pnt)
                pnt.X += i.Width + 2
            Next
        End Using

        Return outputImage
    End Function

    Public Function StringsToImage(strings As String(), colors As String()) As Bitmap
        Dim bitmaps As New List(Of Bitmap)
        If strings.Any Then
            For Each s In strings
                bitmaps.Add(ConvertTextToImage(s, "Arial", 12, Color.FromName(colors(Array.IndexOf(strings, s))), Color.Black))
            Next
        Else
            bitmaps.Add(New Bitmap(1, 1))
        End If
        Return ImagesToSingleImage(bitmaps.ToArray)
    End Function

    Public Function CheckConnection(context As DbContext) As Boolean
        Try
            context.Database.Connection.Open()
            context.Database.Connection.Close()
        Catch __unusedSqlException1__ As SqlClient.SqlException
            Return False
        End Try
        Return True
    End Function
End Module