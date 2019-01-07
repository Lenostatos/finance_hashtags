Imports System.Collections.Generic

Public Class Account

    Public ReadOnly id As Integer
    Public ReadOnly name As String
    Private _balances As SortedSet(Of Balance)
    Private _transactions As List(Of Transaction)

    Public Sub New(ByVal id As Integer,
                   ByVal name As String,
                   ByVal balances As SortedSet(Of Balance),
                   ByVal transactions As List(Of Transaction))

        Me.id = id
        Me.name = name
        Me.balances = balances
        Me.transactions = transactions

    End Sub

    Public Property balances As SortedSet(Of Balance)
        Get
            Return _balances
        End Get
        Set(ByVal value As SortedSet(Of Balance))
            If value Is Nothing OrElse value.Count = 0 OrElse
                TypeOf value.Comparer IsNot Compare_balance_by_timestamp Then
                Throw New ArgumentException("There must be at least one balance and no two balances can have the same timestamp.")
            Else
                _balances = value
            End If
        End Set
    End Property

    Public Property transactions As List(Of Transaction)
        Get
            Return _transactions
        End Get
        Set(value As List(Of Transaction))
            If value IsNot Nothing AndAlso value.Count > 0 Then
                _transactions = value
                _transactions.Sort(New Compare_transaction_by_timestamp)
                If _transactions(0).date_time < Me.balances.Min.date_time Then
                    _transactions = Nothing
                    Throw New ArgumentException("The earliest transaction of an account must not be earlier than the account's earliest balance.")
                End If
            End If
        End Set
    End Property

End Class
