'this is the reference for all the database connectivity stuff:
'https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/

Imports System.Data
Imports System.Data.Odbc

Public Class access_database

    Private m_db_adapter As OdbcDataAdapter
    Private m_data As DataSet

    Public Sub New()

        'first create a connection string
        Dim cnn_string As String
        Dim cnn_string_builder As New OdbcConnectionStringBuilder()

        cnn_string_builder.Driver = "Microsoft Access Driver (*.mdb, *.accdb)"

        cnn_string_builder.Add("Dbq", ".\..\..\..\data\Database.accdb")
        cnn_string_builder.Add("Uid", "Admin")
        cnn_string_builder.Add("Pwd", "")

        cnn_string = cnn_string_builder.ToString()

        'then create a connection object from the connection string
        Dim db_connection As New OdbcConnection(cnn_string)

        'the connection has to be opened and closed manually!
        db_connection.Open()

        'now we have to get the names of the tables contained within the database
        Dim schema As DataTable 'the schema contains tables with metadata on the database
        schema = db_connection.GetSchema("Tables") 'the "Tables" table contains metadata on the DB's tables, a.o. their names

        'we are interested in the two columns containing the table types and table names
        Dim col_ind_tbl_type As Integer
        Dim col_ind_tbl_name As Integer

        col_ind_tbl_type = schema.Columns.IndexOf("TABLE_TYPE") 'there are not only the "self made" tables but also system tables which are not relevant for the application
        col_ind_tbl_name = schema.Columns.IndexOf("TABLE_NAME")

        Dim table_names As New Collection

        For row_ind As Integer = 0 To schema.Rows.Count - 1
            If schema.Rows(row_ind)(col_ind_tbl_type) = "TABLE" Then '"self made" tables are of the type "TABLE"
                table_names.Add(schema.Rows(row_ind)(col_ind_tbl_name))
            End If
        Next

        'now let's look at the field names
        Dim query_string As New OdbcCommand("SELECT * FROM " + table_names(1), db_connection)

        m_db_adapter = New OdbcDataAdapter(query_string.CommandText, db_connection)

        'I might need the command builder to use the update method of the dbAdapter
        Dim query_builder As New OdbcCommandBuilder(m_db_adapter)

        m_data = New DataSet

        m_db_adapter.FillSchema(m_data, SchemaType.Source)

        Dim col As DataColumn
        For Each col In m_data.Tables(0).Columns
            Console.WriteLine(col.ColumnName)
        Next

        Console.WriteLine("===================================")

        db_connection.Close()

    End Sub

End Class
