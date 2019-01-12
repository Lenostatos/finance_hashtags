Imports MicroLite
Imports MicroLite.Configuration

Module Module_Main

    Sub Main()

        Dim sessionFactory = New Configure
        .Fluently()
        .ForSQLiteConnection("SQLiteData")
        .CreateSessionFactory();

    End Sub

End Module
