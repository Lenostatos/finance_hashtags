' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/data-types/value-types-And-reference-types
' https://docs.microsoft.com/en-us/dotnet/visual-basic/programming-guide/language-features/procedures/differences-between-passing-an-argument-by-value-and-by-reference

' Conclusions from the documents above:
' + When passing instances of a reference type always just the pointer is passed or copied.
' + ReadOnly stuff is never modifiable when passed to somewhere else. Doesn't matter whether it's ByVal or ByRef.
' + Members of a passed instance are modifiable, regardless of whether they are passed ByVal or ByRef.
' -> Now to find out whether members of ReadOnly instances are modifiable.

Module Module1

    Sub Main()

        Dim first As New Simple_Class
        Dim second As Simple_Class = first
        Dim third As New Simple_Class

        'Console.Write("First: " & vbNewLine & first.ToString & "Second: " & vbNewLine & second.ToString)

        ' Second uses the same memory as first but third has it's own memory:
        'second.member.number = 2
        'first.member.number = 1
        'second.member.word = "Foo"

        'Console.Write("First: " & vbNewLine & first.ToString & "Second: " & vbNewLine & second.ToString & "Third: " & vbNewLine & third.ToString)

        ' ReadOnly members themselves are never modifiable,
        ' BUT members of ReadOnly members are modifiable:
        'first.readonly_member.number = 1
        'second.readonly_member.word = "Foo"

        'Console.Write("First: " & vbNewLine & first.ToString & "Second: " & vbNewLine & second.ToString)

        'first.member.number = 1
        'first.member.word = "Foo"

        ' The number is NOT of a reference type. Therefore this doesn't need to be tested:
        'first.member.number = third.member.readonly_number
        ' The word however is of a reference type...

        ' All of the following doesn't modify third's members.
        ' This means that a copy of the memory for word was made.
        'first.member.word = third.member.readonly_word
        'first.member.word = "Baa"

        'first.member.word = third.member.word
        'first.member.word = "Baa"

        'first.word = third.readonly_word
        'first.word = "Baa"

        'first.word = third.word
        'first.word = "Baa"

        'Console.Write("First: " & vbNewLine & first.ToString & "Third: " & vbNewLine & third.ToString)

        ' This causes first to reference third's member and then third's readonly member:
        'first.member = third.member
        'first.member.number = 1

        'first.member = third.readonly_member
        'first.member.number = 1

        'Console.Write("First: " & vbNewLine & first.ToString & "Third: " & vbNewLine & third.ToString)



        Console.Write("First: " & vbNewLine & first.ToString & "Third: " & vbNewLine & third.ToString)

        Console.ReadKey()

    End Sub

End Module
