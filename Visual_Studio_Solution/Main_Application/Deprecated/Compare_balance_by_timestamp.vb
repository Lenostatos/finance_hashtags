Imports System.Collections.Generic

Public Class Compare_balance_by_timestamp

    Implements IComparer(Of Balance)

    Public Function Compare(ByVal x As Balance, ByVal y As Balance) As Integer Implements IComparer(Of Balance).Compare
        Return x.date_time.CompareTo(y.date_time)
    End Function

End Class
