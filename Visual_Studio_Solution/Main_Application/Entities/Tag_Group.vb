Imports System.Collections.Generic
Imports Main_Application
Imports MicroLite

''' <summary>
''' Represents a tag group.
''' Allows for querying and manipulation of the associated tags via database queries.
''' </summary>
''' <TODO>Make sure that the model only exposes valid tag groups from the database because there can be tag groups that contain no tags.</TODO>
Public Class Tag_Group

    Implements IEquatable(Of Tag_Group)

    Private _id As Integer
    Private _name As String

    Private Shared ReadOnly exception_duplicate_tags As String = "A tag group must not contain any duplicate tags."

    Public Sub New()
    End Sub

    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.id = id
        Me.name = name
    End Sub

    Public Overrides Function ToString() As String
        Return Me.name
    End Function

    Public Function Equals(other As Tag_Group) As Boolean Implements IEquatable(Of Tag_Group).Equals
        If other Is Nothing Then Throw New ArgumentNullException()
        Return Me.id = other.id AndAlso Me.name = other.name
    End Function

    ''' <summary>
    ''' Indicates whether there is a record corresponding to this instance in the database.
    ''' </summary>
    ''' <returns><c>True</c> if there is a corresponding record in the database; otherwise <c>False</c>.</returns>
    Public Function is_valid() As Boolean

        Dim tag_group As Tag_Group

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                tag_group = session.Single(Of Tag_Group)(Me.id)

                transaction.Commit()

            End Using
        End Using

        Return Me.Equals(tag_group)

    End Function

    ' Note: There can't be any other properties except for those that map to the corresponding database table.
    ' TODO: find out whether this can be circumvented (possibly with custom mapping).
    ''' <summary>
    ''' Returns the tags of this group.
    ''' </summary>
    ''' <returns>A list of tags.</returns>
    Public Function get_tags() As HashSet(Of Tag)

        Dim tag_list As IList(Of Tag)

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT " & DB_Names.table_tag & "." & DB_Names.column_id & ", " & DB_Names.table_tag & "." & DB_Names.column_name &
                                          " FROM " & DB_Names.table_tag &
                                          " JOIN " & DB_Names.table_tag_grouping & " ON " & DB_Names.table_tag & "." & DB_Names.column_id & " = " & DB_Names.table_tag_grouping & "." & DB_Names.column_id_tag &
                                          " WHERE " & DB_Names.table_tag_grouping & "." & DB_Names.column_id_tag_group & " = " & Me.id)

                tag_list = session.Fetch(Of Tag)(query)

                transaction.Commit()

            End Using
        End Using

        Return New HashSet(Of Tag)(tag_list)

    End Function

    ''' <summary>
    ''' Sets the tags of this group to those contained in <paramref name="value"/>, replacing any previously contained tags.
    ''' </summary>
    ''' <param name="value">The tags to be inserted into the group.</param>
    ''' <exception cref="ArgumentException">Thrown when there are no tags to be inserted, i.e. <paramref name="value"/> is empty or <c>Nothing</c>.</exception>
    Public Sub set_tags(value As HashSet(Of Tag))

        If value Is Nothing OrElse value.Count = 0 Then Throw New ArgumentException(Exception_Messages.empty_tag_group)

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("DELETE FROM " & DB_Names.table_tag_grouping &
                                              " WHERE " & DB_Names.column_id_tag_group & " = @p0", Me.id)

                session.Advanced.Execute(query)

                ' TODO: find out whether this can be optimized:
                ' idea for that: compare with first creating a tag_grouping object and use session.Insert
                For Each tag In value
                    query = New SqlQuery("INSERT INTO " & DB_Names.table_tag_grouping & " (" & DB_Names.column_id_tag & ", " & DB_Names.column_id_tag_group &
                                             ") VALUES (@p0, " & Me.id & ")", tag.id)
                    session.Advanced.Execute(query)
                Next

                transaction.Commit()

            End Using
        End Using

    End Sub

    ''' <summary>
    ''' Indicates whether a certain tag is part of the tag_group.
    ''' </summary>
    ''' <param name="tag">The tag that the group is to be checked for.</param>
    ''' <returns><c>True</c> if the tag is contained in the group; otherwise <c>False</c>.</returns>
    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag"/> is <c>Nothing</c>.</exception>
    Public Function has_tag(tag As Tag) As Boolean

        If tag Is Nothing Then Throw New ArgumentNullException()

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag_grouping &
                                          " WHERE " & DB_Names.table_tag_grouping & "." & DB_Names.column_id_tag_group & " = " & Me.id &
                                          " AND " & DB_Names.table_tag_grouping & "." & DB_Names.column_id_tag & " = " & tag.id)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res = 1

    End Function

    ''' <summary>
    ''' Returns the number of tags that are part of this tag group.
    ''' </summary>
    ''' <returns>The number of tags that are part of this tag group.</returns>
    Public Function count() As Integer

        Dim res As Integer

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("SELECT COUNT(*) FROM " & DB_Names.table_tag_grouping &
                                          " WHERE " & DB_Names.table_tag_grouping & "." & DB_Names.column_id_tag_group & " = " & Me.id)

                res = session.Single(Of Integer)(query)

                transaction.Commit()

            End Using
        End Using

        Return res

    End Function

    ''' <summary>
    ''' Adds a tag to the group.
    ''' </summary>
    ''' <param name="tag">The tag to be added.</param>
    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag"/> is <c>Nothing</c>.</exception>
    ''' <exception cref="ArgumentException">Thrown when <paramref name="tag"/> is already a part of the group.</exception>
    Public Sub add_tag(tag As Tag)

        If tag Is Nothing Then Throw New ArgumentNullException()

        If Me.has_tag(tag) Then Throw New ArgumentException(exception_duplicate_tags)

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("INSERT INTO " & DB_Names.table_tag_grouping & " (" & DB_Names.column_id_tag & ", " & DB_Names.column_id_tag_group &
                                          ") VALUES (" & tag.id & ", " & Me.id & ")")

                session.Advanced.Execute(query)

                transaction.Commit()

            End Using
        End Using

    End Sub

    ''' <summary>
    ''' Removes <paramref name="tag"/> from the group. If <paramref name="tag"/> is not a part of the group, nothing happens.
    ''' </summary>
    ''' <param name="tag">The tag to be removed.</param>
    ''' <exception cref="ArgumentNullException">Thrown if <paramref name="tag"/> is <c>Nothing</c>.</exception>
    ''' <exception cref="InvalidOperationException">Thrown if removing the tag would lead to the group not having any tags.</exception>
    Public Sub remove_tag(tag As Tag)

        If tag Is Nothing Then Throw New ArgumentNullException()

        If Me.count() = 1 Then Throw New InvalidOperationException(Exception_Messages.empty_tag_group)

        Using session As ISession = SessionFactory.OpenSession()
            Using transaction As ITransaction = session.BeginTransaction()

                Dim query As New SqlQuery("DELETE FROM " & DB_Names.table_tag_grouping &
                                          " WHERE " & DB_Names.column_id_tag & " = " & tag.id &
                                          " AND " & DB_Names.column_id_tag_group & " = " & Me.id)

                session.Advanced.Execute(query)

                transaction.Commit()

            End Using
        End Using

    End Sub

    Public Property id As Integer
        Get
            Return _id
        End Get
        Set(value As Integer)
            _id = value
        End Set
    End Property

    Public Property name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

End Class
