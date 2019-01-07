' Models a group of selection criteria for transactions like for example tags or partners.

Imports System.Collections.Generic

Public Class Criterion_Group(Of T)

    Public ReadOnly id As Integer
    Public ReadOnly name As String
    Private _criteria As SortedSet(Of T)

    Public Sub New(ByVal id As Integer, ByVal name As String, ByRef criteria As SortedSet(Of T))
        Me.id = id
        Me.name = name
        Me.criteria = criteria
    End Sub

    Public Property criteria As SortedSet(Of T)
        Get
            Return _criteria
        End Get
        Set(ByVal value As SortedSet(Of T))
            If value Is Nothing OrElse value.Count = 0 Then
                Throw New ArgumentException("A criterion group must contain at least one criterion.")
            Else
                _criteria = value
            End If
        End Set
    End Property

End Class
