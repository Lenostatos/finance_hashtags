Imports System.Collections.Generic

Public Class Transaction

    Inherits Transaction_Element

    Public ReadOnly date_time As Date
    Public partner_id As Integer

    Public Sub New(ByVal id As Integer,
                   ByVal amount As Double,
                   ByVal date_time As Date,
                   Optional ByVal description As String = "",
                   Optional ByVal elements As List(Of Transaction_Element) = Nothing,
                   Optional ByRef tags_id As SortedSet(Of Integer) = Nothing,
                   Optional ByRef partner_id As Integer = -1)

        MyBase.New(id, amount, description, elements, tags_id)

        Me.date_time = date_time
        Me.partner_id = partner_id

    End Sub

End Class
