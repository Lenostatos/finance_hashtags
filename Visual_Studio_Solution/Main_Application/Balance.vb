Public Class Balance

    Public ReadOnly id As Integer
    Public amount As Double
    Public date_time As Date

    Public Sub New(ByVal id As Integer, ByVal amount As Double, ByVal date_time As Date)
        Me.id = id
        Me.amount = amount
        Me.date_time = date_time
    End Sub

End Class
