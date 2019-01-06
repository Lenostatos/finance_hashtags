' Models a group of selection criteria for transactions like for example tags or partners.

Public Class Criterion_Group(Of T)

    Public ReadOnly id As Integer
    Public ReadOnly name As String
    Private _criteria As Collections.Generic.SortedSet(Of T)

    Public Sub New(ByVal id As Integer, ByVal name As String, ByRef criteria As Collections.Generic.SortedSet(Of T))
        Me.id = id
        Me.name = name
        Me.criteria = criteria
    End Sub

    Public Property criteria As Collections.Generic.SortedSet(Of T)
        Get
            Return _criteria
        End Get
        ' I can't declare param tags "ByRef"
        Set(ByVal criteria As Collections.Generic.SortedSet(Of T))
            If criteria Is Nothing OrElse criteria.Count = 0 Then
                Throw New ArgumentException
            Else
                _criteria = criteria
            End If
        End Set
    End Property

End Class
