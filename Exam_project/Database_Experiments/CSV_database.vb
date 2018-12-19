Imports Exam_project

Public Class CSV_database

    'In order to make this app work for the exam I have to use local files as a database. E.g. csv-files.
    'https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualbasic.fileio.textfieldparser?view=netframework-4.7
    'https://stackoverflow.com/questions/28855994/oledb-import-of-csv-to-vb-net-datatable-reading-as-0/28856448#
    'https://docs.microsoft.com/en-gb/sql/odbc/microsoft/schema-ini-file-text-file-driver?view=sql-server-2017

    Implements I_database

    Private data As Data.DataSet

    'Creates a database using a CSV-file from a fixed location relative to the executable.
    'If no file with the name /a file_name could be found, a new one is created with that name.
    Public Sub New(file_name As String)

    End Sub

    Public ReadOnly Property table_names As String() Implements I_database.table_names
        Get
            Dim names = New String() {}
            ReDim names(data.Tables.Count)

            Dim i As Integer
            For i = 0 To names.Length
                names(i) = data.Tables(i).TableName
            Next

            Return names
        End Get
    End Property

    Public ReadOnly Property num_tables As Integer Implements I_database.num_tables
        Get
            Return data.Tables.Count
        End Get
    End Property

    Function get_keys(table_name As String) As VariantType() Implements I_database.get_keys
        Throw New NotImplementedException()
    End Function
    Function get_col_names(table_name As String) As String() Implements I_database.get_col_names
        Throw New NotImplementedException()
    End Function

    Function get_row(table_name As String, key As VariantType) As VariantType() Implements I_database.get_row
        Throw New NotImplementedException()
    End Function
    Function get_column(table_name As String, col_name As String) As VariantType() Implements I_database.get_column
        Throw New NotImplementedException()
    End Function

    Function get_data(table_name As String, key As VariantType, col_name As String) As VariantType Implements I_database.get_data
        Throw New NotImplementedException()
    End Function
    Function get_data(table_name As String, keys As VariantType(), col_names As String()) As VariantType(,) Implements I_database.get_data
        Throw New NotImplementedException()
    End Function
    Function get_data(table_name As String, key_from As VariantType, key_to As VariantType, col_name_from As String, col_name_to As String) As VariantType(,) Implements I_database.get_data
        Throw New NotImplementedException()
    End Function

    Function write_data(table_name As String, col_names As String(), data As VariantType(,), Optional overwrite As Boolean = False) Implements I_database.write_data
        Throw New NotImplementedException()
    End Function
End Class
