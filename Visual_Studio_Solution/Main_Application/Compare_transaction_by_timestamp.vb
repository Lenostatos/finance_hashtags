Imports System.Collections.Generic

Public Class Compare_transaction_by_timestamp

    Implements IComparer(Of Transaction)

    Public Function Compare(x As Transaction, y As Transaction) As Integer Implements IComparer(Of Transaction).Compare
        Return x.date_time.CompareTo(y.date_time)
    End Function

End Class