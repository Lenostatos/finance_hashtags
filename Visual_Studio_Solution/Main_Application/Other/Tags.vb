Imports System.Collections.Generic
Imports MicroLite

''' <summary>
''' Provides an interface to the tags stored in the database.
''' </summary>
Public Class Tags

    ''' <summary>
    ''' Returns the number of tags in the database.
    ''' </summary>
    ''' <returns>The number of tags in the database.</returns>
    Public Shared Function count() As Integer

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res

    End Function

    ''' <summary>
    ''' Returns a set containing <c>Tag</c> objects that represent all tags stored in the database.
    ''' </summary>
    ''' <returns>A set containing <c>Tag</c> objects that represent all tags stored in the database.</returns>
    Public Shared Function get_them() As HashSet(Of Tag)

        Dim tags As IList(Of Tag)

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT * FROM " & DB_Names.table_tag)

                tags = session.Fetch(Of Tag)(query)

                transaction.Commit()

            End Using
        End Using

        Return New HashSet(Of Tag)(tags)

    End Function

    ''' <summary>
    ''' Returns a <c>Tag</c> object corresponding to the database record with the id <paramref name="id"/> or <c>Nothing</c> if such a record doesn't exist.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>A <c>Tag</c> object corresponding to the database record with the id <paramref name="id"/> or <c>Nothing</c> if such a record doesn't exist.</returns>
    ''' <exception cref="ArgumentException">Thrown if <paramref name="id"/> is smaller than 1.</exception>
    Public Shared Function get_by_id(ByVal id As Integer) As Tag

        If id < 1 Then Throw New ArgumentException(Exception_Messages.invalid_id)

        Dim tag As Tag

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                tag = session.Single(Of Tag)(id)

                transaction.Commit()

            End Using
        End Using

        Return tag

    End Function

    ''' <summary>
    ''' Returns a <c>Tag</c> object corresponding to the database record with the name <paramref name="name"/> or <c>Nothing</c> if such a record doesn't exist.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>A <c>Tag</c> object corresponding to the database record with the name <paramref name="name"/> or <c>Nothing</c> if such a record doesn't exist.</returns>
    ''' <exception cref="ArgumentException">Thrown if <paramref name="name"/> is an empty string.</exception>
    Public Shared Function get_by_name(ByRef name As String) As Tag

        If name = "" Then Throw New ArgumentException(Exception_Messages.invalid_name)

        Dim tag As Tag

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT * FROM " & DB_Names.table_tag &
                                          " WHERE " & DB_Names.column_name & " = " & name)

                tag = session.Single(Of Tag)(query)

                transaction.Commit()

            End Using
        End Using

        Return tag

    End Function

    Public Shared Function contains(ByVal with_id As Integer) As Boolean

        If with_id < 1 Then Throw New ArgumentException(Exception_Messages.invalid_id)

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag &
                                          " WHERE " & DB_Names.column_id & " = " & with_id)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res = 1

    End Function

    Public Shared Function contains(ByRef with_name As String) As Boolean

        If with_name = "" Then Throw New ArgumentException(Exception_Messages.invalid_name)

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag &
                                          " WHERE " & DB_Names.column_name & " = " & with_name)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res = 1

    End Function

    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag"/> is <c>Nothing</c>.</exception>
    Public Shared Function contains(ByRef tag As Tag) As Boolean

        If tag Is Nothing Then Throw New ArgumentNullException()

        Return tag.is_valid()

    End Function

    ''' <summary>
    ''' Note: This function is only available temporarily for testing purposes (TODO).
    ''' Creates a new tag and stores it in the database.
    ''' </summary>
    ''' <param name="tag_name"></param>
    Public Shared Function add(ByRef tag_name As String) As Tag

        If Tags.contains(tag_name) Then Throw New ArgumentException(exception_assigned_tag_name)

        Dim new_tag As New Tag(0, tag_name)

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                session.Insert(new_tag)

                transaction.Commit()

            End Using
        End Using

        Return new_tag

    End Function

    ''' <summary>
    ''' Trys to delete <paramref name="tag"/>'s corresponding entry from the database and set's it to <c>Nothing</c> if the deletion was successful.
    ''' </summary>
    ''' <param name="tag"></param>
    ''' <returns><c>True</c> if the deletion was successful; otherwise <c>False</c>.</returns>
    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag"/> is <c>Nothing</c>.</exception>
    ''' <exception cref="ArgumentException">Thrown if <c>Not </c><paramref name="tag"/><c>.is_valid()</c>.</exception>
    Public Shared Function remove(ByRef tag As Tag) As Boolean

        If tag Is Nothing Then Throw New ArgumentNullException()
        If Not tag.is_valid() Then Throw New ArgumentException(exception_invalid_tag)

        Dim res As Boolean

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                res = session.Delete(tag)

                transaction.Commit()

            End Using
        End Using

        If res Then tag = Nothing

        Return res

    End Function

    Private Shared ReadOnly exception_assigned_tag_name As String = "There already exists a tag with the same name."
    Private Shared ReadOnly exception_invalid_tag As String = "Attempted to use an invalid tag."

End Class
