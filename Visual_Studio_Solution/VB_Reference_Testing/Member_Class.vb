Public Class Member_Class

    Public number As Integer
    Public ReadOnly readonly_number As Integer

    Public word As String
    Public ReadOnly readonly_word As String

    Public Sub New()
        Me.number = 13
        Me.readonly_number = -13

        Me.word = "'Hello'"
        Me.readonly_word = "'ReadOnly Hello'"
    End Sub

End Class
