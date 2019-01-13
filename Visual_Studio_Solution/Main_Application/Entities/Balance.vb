Imports Main_Application

Public Class Balance

    Implements IComparable(Of Balance)

    Public ReadOnly id As Integer
    Public ReadOnly date_time As Date
    Public amount As Double

    Public Sub New(ByVal id As Integer,
                   ByVal date_time As Date,
                   ByVal amount As Double)

        Me.id = id
        Me.date_time = date_time
        Me.amount = amount

    End Sub

    Public Function CompareTo(other As Balance) As Integer Implements IComparable(Of Balance).CompareTo
        Return id.CompareTo(other.id)
    End Function
End Class
