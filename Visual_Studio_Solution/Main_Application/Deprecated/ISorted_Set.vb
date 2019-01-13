' This interface completes the ISet interface to match the interface of the SortedSet(Of T) class.

Imports System.Collections.Generic

Public Interface ISorted_Set(Of T)

    Inherits ISet(Of T)

    ReadOnly Property Comparer As IComparer(Of T)
    ReadOnly Property Min As T
    ReadOnly Property Max As T

    Function GetViewBetween(lowerValue As T, upperValue As T) As SortedSet(Of T)
    Function RemoveWhere(match As Predicate(Of T)) As Integer
    Function Reverse() As IEnumerable(Of T)

    ' I used this list to find out which properties and methods of the SortedSet are missing in the ISet interface.
    'a.Comparer
    ''a.Count
    'a.Min
    'a.Max

    ''a.Add()
    ''a.Clear()
    ''a.Contains()
    ''a.CopyTo()
    '''a.Equals()
    ''a.ExceptWith()
    ''a.GetEnumerator()
    '''a.GetHashCode()
    '''a.GetType()
    'a.GetViewBetween()
    ''a.IntersectWith()
    ''a.IsProperSubsetOf()
    ''a.IsProperSupersetOf()
    ''a.IsSubsetOf()
    ''a.IsSupersetOf()
    ''a.Overlaps()
    ''a.Remove()
    'a.RemoveWhere()
    'a.Reverse()
    ''a.SetEquals()
    ''a.SymmetricExceptWith()
    '''a.ToString()
    ''a.UnionWith()

End Interface
