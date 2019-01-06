Public Class Transaction_Element

    Protected _id As Integer
    Public amount As Double
    Public description As String
    Public elements As Collections.Generic.List(Of Transaction_Element)
    Public tags As Collections.Generic.SortedSet(Of Tag)

    Public Sub New(ByVal id As Integer,
                   ByVal amount As Double,
                   Optional ByVal description As String = "",
                   Optional ByVal elements As Collections.Generic.List(Of Transaction_Element) = Nothing,
                   Optional ByRef tags As Collections.Generic.SortedSet(Of Tag) = Nothing)

        _id = id
        Me.amount = amount
        Me.description = description
        Me.elements = elements
        Me.tags = tags

    End Sub

    Public ReadOnly Property id As Integer
        Get
            Return _id
        End Get
    End Property
End Class
