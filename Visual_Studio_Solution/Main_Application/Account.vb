Public Class Account

    Public ReadOnly id As Integer
    Public ReadOnly name As String
    Private _balances As Collections.Generic.List(Of Balance)

    Public Sub New(ByVal id As Integer, ByVal name As String, ByVal balances As Collections.Generic.List(Of Balance))
        Me.id = id
        Me.name = name
        _balances = balances
    End Sub

End Class
