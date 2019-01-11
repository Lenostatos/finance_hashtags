Imports System.Collections
Imports System.Collections.Generic

Public Class Balances

    Implements ISorted_Set(Of Balance)

    Private _sorted_set As SortedSet(Of Balance)

    Private Shared ReadOnly _exception_no_balances As String = "There must be at least one balance in a set of balances."
    Private Shared ReadOnly _exception_wrong_comparer As String = "A set of balances must be initialized with a set that compares balances with their default comparison mechanism."
    Private Shared ReadOnly _exception_no_parent_account As String = "A set of balances must have a parent account."
    Private Shared ReadOnly _exception_wrong_parent_account As String = "Attempted to add balance with different parent account."
    Private Shared ReadOnly _exception_same_timestamp As String = "No two balances can have the same timestamp."

    Public Sub New(ByVal balances As SortedSet(Of Balance))

        'If parent_account Is Nothing Then
        '    Throw New ArgumentNullException(_exception_no_parent_account)
        'Else
        '    Me.parent_account = parent_account
        'End If

        If balances Is Nothing OrElse balances.Count = 0 Then
            Throw New ArgumentException(_exception_no_balances)
        ElseIf TypeOf balances.Comparer IsNot Comparer(Of Balance) Then
            Throw New ArgumentException(_exception_wrong_comparer)
        Else
            _sorted_set = balances
        End If

    End Sub

    Public ReadOnly Property Count As Integer Implements ICollection(Of Balance).Count
        Get
            Return _sorted_set.Count
        End Get
    End Property

    Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Balance).IsReadOnly
        Get
            Return False
        End Get
    End Property

    Public ReadOnly Property Min As Balance Implements ISorted_Set(Of Balance).Min
        Get
            Return _sorted_set.Min
        End Get
    End Property

    Public ReadOnly Property Max As Balance Implements ISorted_Set(Of Balance).Max
        Get
            Return _sorted_set.Max
        End Get
    End Property

    Public ReadOnly Property Comparer As IComparer(Of Balance) Implements ISorted_Set(Of Balance).Comparer
        Get
            Return _sorted_set.Comparer
        End Get
    End Property

    Public Sub UnionWith(other As IEnumerable(Of Balance)) Implements ISet(Of Balance).UnionWith
        _sorted_set.UnionWith(other)
    End Sub

    Public Sub IntersectWith(other As IEnumerable(Of Balance)) Implements ISet(Of Balance).IntersectWith
        _sorted_set.IntersectWith(other)
    End Sub

    Public Sub ExceptWith(other As IEnumerable(Of Balance)) Implements ISet(Of Balance).ExceptWith
        _sorted_set.ExceptWith(other)
    End Sub

    Public Sub SymmetricExceptWith(other As IEnumerable(Of Balance)) Implements ISet(Of Balance).SymmetricExceptWith
        _sorted_set.SymmetricExceptWith(other)
    End Sub

    Public Sub Clear() Implements ICollection(Of Balance).Clear
        Throw New InvalidOperationException(_exception_no_balances)
    End Sub

    Public Sub CopyTo(array() As Balance, arrayIndex As Integer) Implements ICollection(Of Balance).CopyTo
        _sorted_set.CopyTo(array, arrayIndex)
    End Sub

    Private Sub ICollection_Add(item As Balance) Implements ICollection(Of Balance).Add
        Throw New NotSupportedException
    End Sub

    Public Function Add(item As Balance) As Boolean Implements ISet(Of Balance).Add
        Return _sorted_set.Add(item)
    End Function

    Public Function IsSubsetOf(other As IEnumerable(Of Balance)) As Boolean Implements ISet(Of Balance).IsSubsetOf
        Return _sorted_set.IsSubsetOf(other)
    End Function

    Public Function IsSupersetOf(other As IEnumerable(Of Balance)) As Boolean Implements ISet(Of Balance).IsSupersetOf
        Return _sorted_set.IsSupersetOf(other)
    End Function

    Public Function IsProperSupersetOf(other As IEnumerable(Of Balance)) As Boolean Implements ISet(Of Balance).IsProperSupersetOf
        Return _sorted_set.IsProperSupersetOf(other)
    End Function

    Public Function IsProperSubsetOf(other As IEnumerable(Of Balance)) As Boolean Implements ISet(Of Balance).IsProperSubsetOf
        Return _sorted_set.IsProperSubsetOf(other)
    End Function

    Public Function Overlaps(other As IEnumerable(Of Balance)) As Boolean Implements ISet(Of Balance).Overlaps
        Return _sorted_set.Overlaps(other)
    End Function

    Public Function SetEquals(other As IEnumerable(Of Balance)) As Boolean Implements ISet(Of Balance).SetEquals
        Return _sorted_set.SetEquals(other)
    End Function

    Public Function Contains(item As Balance) As Boolean Implements ICollection(Of Balance).Contains
        Return _sorted_set.Contains(item)
    End Function

    Public Function Remove(item As Balance) As Boolean Implements ICollection(Of Balance).Remove
        If _sorted_set.Count = 1 Then
            Throw New InvalidOperationException(_exception_no_balances)
        Else
            Return _sorted_set.Remove(item)
        End If
    End Function

    Public Function GetEnumerator() As IEnumerator(Of Balance) Implements IEnumerable(Of Balance).GetEnumerator
        Return _sorted_set.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Throw New NotImplementedException()
    End Function

    Public Function GetViewBetween(lowerValue As Balance, upperValue As Balance) As SortedSet(Of Balance) Implements ISorted_Set(Of Balance).GetViewBetween
        Return _sorted_set.GetViewBetween(lowerValue, upperValue)
    End Function

    Public Function RemoveWhere(match As Predicate(Of Balance)) As Integer Implements ISorted_Set(Of Balance).RemoveWhere
        Return _sorted_set.RemoveWhere(match)
    End Function

    Public Function Reverse() As IEnumerable(Of Balance) Implements ISorted_Set(Of Balance).Reverse
        Return _sorted_set.Reverse()
    End Function
End Class
