Imports System.Collections.Generic
Imports MicroLite

Public Class Tag_Group

    Public id As Integer
    Public name As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.id = id
        Me.name = name
    End Sub

    Public ReadOnly Property tags() As IList(Of Tag)
        Get

            Dim tag_list As IList(Of Tag)

            Using session As ISession = SessionFactory.OpenSession
                Using transaction As ITransaction = session.BeginTransaction()

                    Dim query As SqlQuery = Builder.SqlBuilder.
                        Select(Module_DB_Names.column_id,
                               Module_DB_Names.column_name).
                        From(Module_DB_Names.table_tag).
                        Where(Module_DB_Names.table_tag_group & "." & Module_DB_Names.column_id).
                        IsEqualTo(Me.id).
                        ToSqlQuery()

                    Console.WriteLine(query.ToString())

                    tag_list = session.Fetch(Of Tag)(query)

                    transaction.Commit()

                End Using
            End Using

            Return tag_list

        End Get
    End Property

End Class
