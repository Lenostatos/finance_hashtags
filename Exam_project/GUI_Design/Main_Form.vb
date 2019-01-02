Public Class Main_Form

    'create a data table as a common data source for daigram and table
    Private data As New DataTable("coordinates")
    Private data_binding As BindingSource

    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss"
        DateTimePicker2.CustomFormat = "yyyy-MM-dd HH:mm:ss"

        data_binding = New BindingSource()
        data_binding.DataSource = data

        'create dummy data
        Dim x_points() As Integer = {1, 2, 3, 4, 2, 4, 5, 6, 6, 6, 9}
        Dim y_points() As Integer = {1, 3, 5, 3, 3, 3, 1, 1, 5, 1, 1}
        Dim points_tool_tips() As String = {"1", "3", "5", "3", "3", "3", "1", "1", "5", "1", "1"}

        'create a list object just for testing
        Dim x_value_list As New List(Of Integer)(x_points)

        'create columns
        data.Columns.Add("x_values", Type.GetType("System.Int16"))
        data.Columns.Add("y_values", Type.GetType("System.Int16"))
        data.Columns.Add("y_tool_tips", Type.GetType("System.String"))

        'create and fill rows for the columns
        For i As Short = 0 To x_points.Length - 1
            data.Rows.Add(New Object() {x_points(i), y_points(i), points_tool_tips(i)})
        Next

        'bind the chart data series to the data table
        Main_Chart.DataSource = data_binding
        Main_Chart.Series.Item("Series_Line").Points.DataBind(data_binding, "x_values", "y_values", "Label=y_tool_tips") 'if one would like to use the last argument here the arguments would look like this: (myReader, "Name", "Sales", "Tooltip=Year, Label=Commissions{C2}") with the possible properties being AxisLabel, Tooltip, Label, LegendText, LegendTooltip and CustomPropertyName
        Main_Chart.Series.Item("Series_Points").Points.DataBind(data_binding, "x_values", "y_values", "Tooltip=y_tool_tips")

        'bind the table data to the data table
        Main_DataGridView.DataSource = data_binding

    End Sub

    'TODO: maybe make this more performant
    Private Sub Main_DataGridView_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Main_DataGridView.CellValueChanged
        For Each point_series In Main_Chart.Series
            point_series.Points.Clear()
            point_series.Points.DataBind(data_binding, "x_values", "y_values", "Tooltip=y_tool_tips")
        Next
        'data_binding.ResetBindings(False) The ResetBindings method would usually be used
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Transaction_Adding_Form.Show()
    End Sub
End Class