Imports System.Collections.Generic
Imports MicroLite

''' <summary>
''' Provides an interface to the tag groups stored in the database.
''' </summary>
Public Class Tag_Groups

    ''' <summary>
    ''' Returns the number of tag groups in the database.
    ''' </summary>
    ''' <returns>The number of tag groups in the database.</returns>
    Public Shared Function count() As Integer

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag_group)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res

    End Function

    ''' <summary>
    ''' Returns a set containing <c>Tag_Group</c> objects that represent all tag groups stored in the database.
    ''' </summary>
    ''' <returns>A set containing <c>Tag_Group</c> objects that represent all tag groups stored in the database.</returns>
    Public Shared Function get_them() As HashSet(Of Tag_Group)

        Dim tag_groups As IList(Of Tag_Group)

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT * FROM " & DB_Names.table_tag_group)

                tag_groups = session.Fetch(Of Tag_Group)(query)

                transaction.Commit()

            End Using
        End Using

        Return New HashSet(Of Tag_Group)(tag_groups)

    End Function

    ''' <summary>
    ''' Returns a <c>Tag_Group</c> object corresponding to the database record with the id <paramref name="id"/> or <c>Nothing</c> if such a record doesn't exist.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>A <c>Tag_Group</c> object corresponding to the database record with the id <paramref name="id"/> or <c>Nothing</c> if such a record doesn't exist.</returns>
    ''' <exception cref="ArgumentException">Thrown if <paramref name="id"/> is smaller than 1.</exception>
    Public Shared Function get_by_id(ByVal id As Integer) As Tag_Group

        If id < 1 Then Throw New ArgumentException(Exception_Messages.invalid_id)

        Dim tag_group As Tag_Group

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                tag_group = session.Single(Of Tag_Group)(id)

                transaction.Commit()

            End Using
        End Using

        Return tag_group

    End Function

    ''' <summary>
    ''' Returns a <c>Tag_Group</c> object corresponding to the database record with the name <paramref name="name"/> or <c>Nothing</c> if such a record doesn't exist.
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>A <c>Tag_Group</c> object corresponding to the database record with the name <paramref name="name"/> or <c>Nothing</c> if such a record doesn't exist.</returns>
    ''' <exception cref="ArgumentException">Thrown if <paramref name="name"/> is an empty string.</exception>
    Public Shared Function get_by_name(ByRef name As String) As Tag_Group

        If name = "" Then Throw New ArgumentException(Exception_Messages.invalid_name)

        Dim tag_group As Tag_Group

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT * FROM " & DB_Names.table_tag_group &
                                          " WHERE " & DB_Names.column_name & " = " & name)

                tag_group = session.Single(Of Tag_Group)(query)

                transaction.Commit()

            End Using
        End Using

        Return tag_group

    End Function

    Public Shared Function contains(ByVal with_id As Integer) As Boolean

        If with_id < 1 Then Throw New ArgumentException(Exception_Messages.invalid_id)

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag_group &
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

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag_group &
                                          " WHERE " & DB_Names.column_name & " = " & with_name)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res = 1

    End Function

    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag_group"/> is <c>Nothing</c>.</exception>
    Public Shared Function contains(ByRef tag_group As Tag_Group) As Boolean

        If tag_group Is Nothing Then Throw New ArgumentNullException()

        Return tag_group.is_valid()

    End Function

    ''' <summary>
    ''' Creates a new tag group and stores it in the database.
    ''' </summary>
    ''' <param name="group_name"></param>
    ''' <param name="with_tags"></param>
    Public Shared Function add(ByRef group_name As String, ByRef with_tags As HashSet(Of Tag)) As Tag_Group

        If with_tags Is Nothing OrElse with_tags.Count = 0 Then Throw New ArgumentException(Exception_Messages.empty_tag_group)
        If Tag_Groups.Contains(group_name) Then Throw New ArgumentException(exception_assigned_group_name)

        ' TODO: this is not very elegant. Maybe create a container that contains only valid stuff?
        For Each tag In with_tags
            If Not tag.is_valid() Then Throw New ArgumentException(exception_invalid_tag)
        Next

        Dim new_group As New Tag_Group(0, group_name)

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                session.Insert(new_group)

                transaction.Commit()

            End Using
        End Using

        Return new_group

    End Function

    ''' <summary>
    ''' Trys to delete <paramref name="tag_group"/>'s corresponding entry from the database and set's it to <c>Nothing</c> if the deletion was successful.
    ''' </summary>
    ''' <param name="tag_group"></param>
    ''' <returns><c>True</c> if the deletion was successful; otherwise <c>False</c>.</returns>
    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag_group"/> is <c>Nothing</c>.</exception>
    ''' <exception cref="ArgumentException">Thrown if <c>Not </c><paramref name="tag_group"/><c>.is_valid()</c>.</exception>
    Public Shared Function remove(ByRef tag_group As Tag_Group) As Boolean

        If tag_group Is Nothing Then Throw New ArgumentNullException()
        If Not tag_group.is_valid() Then Throw New ArgumentException(exception_invalid_tag_group)

        Dim res As Boolean

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                res = session.Delete(tag_group)

                transaction.Commit()

            End Using
        End Using

        If res Then tag_group = Nothing

        Return res

    End Function

    Private Shared ReadOnly exception_invalid_tag_group As String = "The tag group is not valid."
    Private Shared ReadOnly exception_assigned_group_name As String = "There already exists a tag group with the same name."
    Private Shared ReadOnly exception_invalid_tag As String = "Attempted to use an invalid tag."

End Class
