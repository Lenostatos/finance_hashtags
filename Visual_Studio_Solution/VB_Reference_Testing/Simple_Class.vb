Public Class Simple_Class

    Public member As Member_Class
    Public ReadOnly readonly_member As Member_Class

    Public word As String
    Public ReadOnly readonly_word As String

    Public Sub New()
        Me.member = New Member_Class
        Me.readonly_member = New Member_Class

        Me.word = "'Hello'"
        Me.readonly_word = "'ReadOnly Hello'"
    End Sub

    Public Overrides Function ToString() As String

        Dim str As New Text.StringBuilder

        str.AppendLine("Direct member values:")
        str.AppendLine("Word: " & vbTab & vbTab & word)
        str.AppendLine("ReadOnly Word: " & vbTab & readonly_word)
        str.AppendLine()
        str.AppendLine("Normal member values:")
        str.AppendLine("Number: " & vbTab & vbTab & member.number)
        str.AppendLine("ReadOnly Number: " & vbTab & member.readonly_number)
        str.AppendLine("Word: " & vbTab & vbTab & vbTab & member.word)
        str.AppendLine("ReadOnly Word: " & vbTab & vbTab & member.readonly_word)
        str.AppendLine()
        str.AppendLine("ReadOnly member values:")
        str.AppendLine("Number: " & vbTab & vbTab & readonly_member.number)
        str.AppendLine("ReadOnly Number: " & vbTab & readonly_member.readonly_number)
        str.AppendLine("Word: " & vbTab & vbTab & vbTab & readonly_member.word)
        str.AppendLine("ReadOnly Word: " & vbTab & vbTab & readonly_member.readonly_word)
        str.AppendLine()

        Return str.ToString()

    End Function

End Class
