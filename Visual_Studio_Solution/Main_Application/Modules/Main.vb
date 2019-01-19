Imports MicroLite
Imports MicroLite.Configuration

Module Main

    Sub Main()

        initialize_db()

        Console.WriteLine("Number of available tags: " & Tags.count())

        Dim tag_set As Collections.Generic.HashSet(Of Tag)
        tag_set = Tags.get_them()

        For Each tag In tag_set
            Console.WriteLine(tag.name)
        Next

        Dim tag_list As New Collections.Generic.List(Of Tag)(tag_set)
        Tags.remove(tag_list(0))

        Console.WriteLine("Number of available tags: " & Tags.count())

        tag_set = Tags.get_them()

        For Each tag In tag_set
            Console.WriteLine(tag.name)
        Next

        Console.ReadKey()

    End Sub

    Private Sub initialize_db()

        Dim custom_mapping_convention_settings As New Mapping.ConventionMappingSettings
        With custom_mapping_convention_settings
            .UsePluralClassNameForTableName = False
            .IsIdentifier = Function(ByVal prop_info As Reflection.PropertyInfo) As Boolean
                                Return prop_info.Name.Equals("id")
                            End Function
            ' TODO: I wanted to make the id property of the entities ReadOnly but I will refrain from that for now because
            ' I don't know whether I can actually use the .AllowInsert and .AllowUpdate functions for that.
            '.AllowInsert = Function(ByVal prop_info As Reflection.PropertyInfo) As Boolean
            '                   Return Not .IsIdentifier(prop_info)
            '               End Function
            '.AllowUpdate = .AllowInsert
        End With
        Configure.Extensions().WithConventionBasedMapping(custom_mapping_convention_settings)

        DB_Connection.SessionFactory = Configure.Fluently().ForSQLiteConnection("SQLiteData").CreateSessionFactory()

        ' TODO: check whether the "PRAGMA optimize;" and the "VACUUM" command can be executed automatically at some point.

    End Sub

End Module
