Public Class Partner

    Public ReadOnly id As Integer
    Public ReadOnly name As String
    Public details As String

    Public Sub New(ByVal id As Integer, ByVal name As String, ByVal details As String)
        Me.id = id
        Me.name = name
        Me.details = details
    End Sub

End Class
