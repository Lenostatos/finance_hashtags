Imports MicroLite
Imports MicroLite.Configuration
Imports System.Runtime.InteropServices

Module Module_Main

    Sub Main()

        Dim custom_mapping_convention_settings As New Mapping.ConventionMappingSettings
        With custom_mapping_convention_settings
            .UsePluralClassNameForTableName = False
            .IsIdentifier = Function(ByVal prop_info As Reflection.PropertyInfo) As Boolean
                                Return prop_info.Name = "id"
                            End Function
            ' TODO: I wanted to make the id property of the entities ReadOnly but I will refrain from that for now bcause
            ' I don't know whether I can actually use the .AllowInsert and .AllowUpdate functions for that.
            '.AllowInsert = Function(ByVal prop_info As Reflection.PropertyInfo) As Boolean
            '                   Return Not .IsIdentifier(prop_info)
            '               End Function
            '.AllowUpdate = .AllowInsert
        End With
        Configure.Extensions().WithConventionBasedMapping(custom_mapping_convention_settings)

        Module_DB_Connection.SessionFactory = Configure.Fluently().ForSQLiteConnection("SQLiteData").CreateSessionFactory()

        Dim tags As Collections.Generic.IList(Of Tag)

        Using session As MicroLite.ISession = SessionFactory.OpenSession
            Using transaction As MicroLite.ITransaction = session.BeginTransaction()

                Dim query As SqlQuery = Builder.SqlBuilder.
                    Select(Module_DB_Names.column_id,
                           Module_DB_Names.column_name).
                    From(GetType(Tag)).
                    ToSqlQuery()

                Console.WriteLine(query.ToString())
                tags = session.Fetch(Of Tag)(query)

                transaction.Commit()

            End Using
        End Using

        Console.WriteLine(tags.Count)
        Console.ReadKey()

    End Sub

End Module
