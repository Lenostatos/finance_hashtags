Imports System.Collections.Generic

Public Class Account

    'Public ReadOnly id As Integer
    'Public name As String
    'Private _balances As Balances
    'Private _transactions As SortedSet(Of Transaction)

    'Private Shared ReadOnly _exception_transaction_earlier_than_balance As String = "The earliest transaction of an account must not be earlier than the account's earliest balance."
    'Private Shared ReadOnly _exception_no_balances As String = "There must be at least one balance."

    'Public Sub New(ByVal id As Integer,
    '               ByVal name As String,
    '               ByVal balances As Balances,
    '               Optional ByVal transactions As SortedSet(Of Transaction) = Nothing)

    '    Me.id = id
    '    Me.name = name
    '    Me.balances = balances
    '    Me.transactions = transactions

    'End Sub

    'Public Property balances As Balances
    '    Get
    '        Return _balances
    '    End Get
    '    Set(ByVal value As Balances)
    '        If value Is Nothing OrElse value.Count = 0 Then
    '            Throw New ArgumentException(_exception_no_balances)
    '        Else
    '            _balances = value
    '        End If
    '    End Set
    'End Property

    'Public Property transactions As SortedSet(Of Transaction)
    '    Get
    '        Return _transactions
    '    End Get
    '    Set(ByVal value As SortedSet(Of Transaction))
    '        If value IsNot Nothing AndAlso value.Count > 0 Then
    '            _transactions = value
    '            _transactions.Sort(New Compare_transaction_by_timestamp)
    '            If _transactions(0).date_time < Me.balances.Min.date_time Then
    '                _transactions = Nothing
    '                Throw New ArgumentException(_exception_transaction_earlier_than_balance)
    '            End If
    '        Else
    '            _transactions = Nothing
    '        End If
    '    End Set
    'End Property

End Class
