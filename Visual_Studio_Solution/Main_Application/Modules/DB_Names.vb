Module DB_Names

    Public Const table_transaction As String = """Transaction"""
    Public Const table_transaction_element As String = """Transaction_Element"""
    Public Const table_tag As String = """Tag"""
    Public Const table_transaction_tagging As String = """Transaction_Tagging"""
    Public Const table_tag_group As String = """Tag_Group"""
    Public Const table_tag_grouping As String = """Tag_Grouping"""
    Public Const table_partner As String = """Partner"""
    Public Const table_partner_group As String = """Partner_Group"""
    Public Const table_partner_grouping As String = """Partner_Grouping"""
    Public Const table_account As String = """Account"""
    Public Const table_account_group As String = """Account_Group"""
    Public Const table_account_grouping As String = """Account_Grouping"""
    Public Const table_balance As String = """Balance"""

    Public Const column_id As String = """id"""
    Public Const column_name As String = """name"""
    Public Const column_date As String = """date"""
    Public Const column_amount As String = """amount"""
    Public Const column_description As String = """description"""
    Public Const column_details As String = """details"""

    Public ReadOnly column_id_transaction As String = create_id_column_name(table_transaction)
    Public ReadOnly column_id_transaction_element As String = create_id_column_name(table_transaction_element)
    Public ReadOnly column_id_tag As String = create_id_column_name(table_tag)
    Public ReadOnly column_id_tag_group As String = create_id_column_name(table_tag_group)
    Public ReadOnly column_id_partner As String = create_id_column_name(table_partner)
    Public ReadOnly column_id_partner_group As String = create_id_column_name(table_partner_group)
    Public ReadOnly column_id_account As String = create_id_column_name(table_account)
    Public ReadOnly column_id_account_group As String = create_id_column_name(table_account_group)

    Private Function create_id_column_name(quoted_table_name As String) As String
        Return """id_" & quoted_table_name.Substring(1)
    End Function

End Module
