Imports Main_Application
Imports MicroLite

Public Class Tag

    Implements IEquatable(Of Tag)

    Private _id As Integer
    Private _name As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal id As Integer, ByVal name As String)
        Me.id = id
        Me.name = name
    End Sub

    Public Function is_valid() As Boolean

        Dim tag As Tag

        Using session As ISession = SessionFactory.OpenSession
            Using transaction As ITransaction = session.BeginTransaction()

                tag = session.Single(Of Tag)(Me.id)

                transaction.Commit()

            End Using
        End Using

        Return Me.Equals(tag)

    End Function

    Public Function Equals(other As Tag) As Boolean Implements IEquatable(Of Tag).Equals
        If other Is Nothing Then Throw New ArgumentNullException()
        Return Me.id = other.id AndAlso Me.name = other.name
    End Function

    ' Note: The properties are only here because the MicroLite framework needs them.
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
