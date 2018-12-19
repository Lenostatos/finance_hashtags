'Maybe just using a System.Data.DataSet as a database interface is better!
'In that case I don't know yet whether I should define an extra interface or just use the DataSet object directly.

'Other than that the plan is to create a sql-database on my p-drive and connect to that one.
Public Interface I_database

    ReadOnly Property table_names() As String()
    ReadOnly Property num_tables() As Integer

    Function get_keys(table_name As String) As VariantType()
    Function get_col_names(table_name As String) As String()

    Function get_row(table_name As String, key As VariantType) As VariantType()
    Function get_column(table_name As String, col_name As String) As VariantType()

    Function get_data(table_name As String, key As VariantType, col_name As String) As VariantType
    Function get_data(table_name As String, keys As VariantType(), col_names As String()) As VariantType(,)
    Function get_data(table_name As String, key_from As VariantType, key_to As VariantType, col_name_from As String, col_name_to As String) As VariantType(,)

    Function write_data(table_name As String, col_names As String(), data As VariantType(,), Optional overwrite As Boolean = False)

End Interface
