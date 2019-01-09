Module Module_DB_names

    Public Const table_transaction As String = "_transaction"
    Public Const table_transaction_element As String = "_transaction_element"
    Public Const table_tag As String = "_tag"
    Public Const table_transaction_tagging As String = "_transaction_tagging"
    Public Const table_tag_group As String = "_tag_group"
    Public Const table_tag_grouping As String = "_tag_grouping"
    Public Const table_partner As String = "_partner"
    Public Const table_partner_group As String = "_partner_group"
    Public Const table_partner_grouping As String = "_partner_grouping"
    Public Const table_account As String = "_account"
    Public Const table_account_group As String = "_account_group"
    Public Const table_account_grouping As String = "_account_grouping"
    Public Const table_balance As String = "_balance"

    Public Const column_id As String = "_id"
    Public Const column_name As String = "_name"
    Public Const column_date As String = "_date"
    Public Const column_amount As String = "_amount"
    Public Const column_description As String = "_description"
    Public Const column_details As String = "_details"

    Private Const name_connector As String = ""

    Public Const column_id_transaction As String = column_id & name_connector & table_transaction
    Public Const column_id_transaction_element As String = column_id & name_connector & table_transaction_element
    Public Const column_id_tag As String = column_id & name_connector & table_tag
    Public Const column_id_tag_group As String = column_id & name_connector & table_tag_group
    Public Const column_id_partner As String = column_id & name_connector & table_partner
    Public Const column_id_partner_group As String = column_id & name_connector & table_partner_group
    Public Const column_id_account As String = column_id & name_connector & table_account
    Public Const column_id_account_group As String = column_id & name_connector & table_account_group

End Module
