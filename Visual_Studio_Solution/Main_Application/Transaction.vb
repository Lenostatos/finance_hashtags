Public Class Transaction

    Inherits Transaction_Element

    Public date_time As Date
    Public partner As Partner

    Public Sub New(ByVal id As Integer,
                   ByVal amount As Double,
                   ByVal date_time As Date,
                   Optional ByVal description As String = "",
                   Optional ByVal elements As Collections.Generic.List(Of Transaction_Element) = Nothing,
                   Optional ByRef tags As Collections.Generic.SortedSet(Of Tag) = Nothing,
                   Optional ByRef partner As Partner = Nothing)

        MyBase.New(id, amount, description, elements, tags)

        Me.date_time = date_time
        Me.partner = partner

        ' If _partner IsNot Nothing Then
        ' End If

    End Sub

End Class
