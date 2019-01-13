Public Class Tag

    Public ReadOnly id As Integer
    Public ReadOnly name As String

    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.id = id
        Me.name = name
    End Sub

End Class
