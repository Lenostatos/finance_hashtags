<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main_Form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage_Diagram = New System.Windows.Forms.TabPage()
        Me.Main_Chart = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.TabPage_Table = New System.Windows.Forms.TabPage()
        Me.Main_DataGridView = New System.Windows.Forms.DataGridView()
        Me.TabControl1.SuspendLayout()
        Me.TabPage_Diagram.SuspendLayout()
        CType(Me.Main_Chart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage_Table.SuspendLayout()
        CType(Me.Main_DataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage_Diagram)
        Me.TabControl1.Controls.Add(Me.TabPage_Table)
        Me.TabControl1.Location = New System.Drawing.Point(178, 55)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(408, 356)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage_Diagram
        '
        Me.TabPage_Diagram.Controls.Add(Me.Main_Chart)
        Me.TabPage_Diagram.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_Diagram.Name = "TabPage_Diagram"
        Me.TabPage_Diagram.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Diagram.Size = New System.Drawing.Size(400, 327)
        Me.TabPage_Diagram.TabIndex = 0
        Me.TabPage_Diagram.Text = "Diagram"
        Me.TabPage_Diagram.UseVisualStyleBackColor = True
        '
        'Main_Chart
        '
        ChartArea1.Name = "ChartArea1"
        Me.Main_Chart.ChartAreas.Add(ChartArea1)
        Me.Main_Chart.Location = New System.Drawing.Point(6, 6)
        Me.Main_Chart.Name = "Main_Chart"
        Series1.ChartArea = "ChartArea1"
        Series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series1.Legend = "Legend1"
        Series1.Name = "Series_Line"
        Series2.ChartArea = "ChartArea1"
        Series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point
        Series2.Name = "Series_Points"
        Me.Main_Chart.Series.Add(Series1)
        Me.Main_Chart.Series.Add(Series2)
        Me.Main_Chart.Size = New System.Drawing.Size(388, 315)
        Me.Main_Chart.TabIndex = 0
        Me.Main_Chart.Text = "Chart1"
        '
        'TabPage_Table
        '
        Me.TabPage_Table.Controls.Add(Me.Main_DataGridView)
        Me.TabPage_Table.Location = New System.Drawing.Point(4, 25)
        Me.TabPage_Table.Name = "TabPage_Table"
        Me.TabPage_Table.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage_Table.Size = New System.Drawing.Size(400, 327)
        Me.TabPage_Table.TabIndex = 1
        Me.TabPage_Table.Text = "Table"
        Me.TabPage_Table.UseVisualStyleBackColor = True
        '
        'Main_DataGridView
        '
        Me.Main_DataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Main_DataGridView.Location = New System.Drawing.Point(6, 6)
        Me.Main_DataGridView.Name = "Main_DataGridView"
        Me.Main_DataGridView.RowTemplate.Height = 24
        Me.Main_DataGridView.Size = New System.Drawing.Size(388, 315)
        Me.Main_DataGridView.TabIndex = 0
        '
        'Main_Form
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Main_Form"
        Me.Text = "Main_Form"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage_Diagram.ResumeLayout(False)
        CType(Me.Main_Chart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage_Table.ResumeLayout(False)
        CType(Me.Main_DataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage_Diagram As TabPage
    Friend WithEvents TabPage_Table As TabPage
    Friend WithEvents Main_Chart As DataVisualization.Charting.Chart
    Friend WithEvents Main_DataGridView As DataGridView
End Class
