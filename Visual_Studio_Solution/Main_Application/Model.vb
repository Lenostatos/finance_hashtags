Imports System.Collections.Generic

Public Class Model

    Public ReadOnly _accounts As List(Of Account)
    Public ReadOnly _account_groups As List(Of Criterion_Group(Of Integer))
    Public ReadOnly _partners As List(Of Partner)
    Public ReadOnly _partner_groups As List(Of Criterion_Group(Of Integer))
    Public ReadOnly _tags As List(Of Tag)
    Public ReadOnly _tag_groups As List(Of Criterion_Group(Of Integer))

    ' TODO all the database stuff (dataAdapter, dataSet and so on)

    Public Sub New()
        ' Load database and initialize model with it.
    End Sub

    Public Sub add_transaction(ByVal for_account_id As Integer,
                               ByVal new_transaction As Transaction)

        If new_transaction Is Nothing Then Throw New ArgumentNullException

    End Sub

    Public Sub add_transaction_element(ByVal for_account_id As Integer,
                                       ByVal for_transaction_id As Integer,
                                       ByVal new_transaction_element As Transaction_Element,
                                       Optional ByVal for_transaction_element_id As List(Of Integer) = Nothing)

        If new_transaction_element Is Nothing Then Throw New ArgumentNullException

    End Sub

End Class
