Imports MicroLite
Imports MicroLite.Configuration

Module Module_Main

    Sub Main()

        Dim custom_mapping_convention_settings As New Mapping.ConventionMappingSettings
        With custom_mapping_convention_settings
            .UsePluralClassNameForTableName = False
            .IsIdentifier = Function(ByVal prop_info As Reflection.PropertyInfo) As Boolean
                                Return prop_info.Name = "id"
                            End Function
        End With
        Configure.Extensions().WithConventionBasedMapping(custom_mapping_convention_settings)

        Module_DB_Connection.SessionFactory = Configure.Fluently().ForSQLiteConnection("SQLiteData").CreateSessionFactory()

    End Sub

End Module
