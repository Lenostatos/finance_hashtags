Imports System.Collections.Generic

Public Class Transaction_Element

    Implements IComparable(Of Transaction_Element)

    Public ReadOnly id As Integer
    Public amount As Double
    Public description As String
    Public elements As List(Of Transaction_Element)
    Public tags_id As SortedSet(Of Integer)

    Public Sub New(ByVal id As Integer,
                   ByVal amount As Double,
                   Optional ByVal description As String = "",
                   Optional ByVal elements As List(Of Transaction_Element) = Nothing,
                   Optional ByRef tags_id As SortedSet(Of Integer) = Nothing)

        Me.id = id
        Me.amount = amount
        Me.description = description
        Me.elements = elements
        Me.tags_id = tags_id

    End Sub

    Public Function CompareTo(other As Transaction_Element) As Integer Implements IComparable(Of Transaction_Element).CompareTo
        Return id.CompareTo(other.id)
    End Function
End Class
