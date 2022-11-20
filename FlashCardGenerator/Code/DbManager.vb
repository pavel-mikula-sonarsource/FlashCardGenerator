
Imports System.Text.Json
Imports FlashCardGenerator.Data.Anki

Public Class DbManager

    Private ReadOnly fDB As DbContext
    Private ReadOnly fColRow As Col
    Private ReadOnly fDecks As IDictionary(Of Integer, Deck) 'ID => Deck
    Private ReadOnly fDeckNames As IDictionary(Of String, Integer) 'ID => Deck

    Public Sub New(DB As DbContext)
        fDB = DB
        fColRow = DB.Cols.Single
        fDecks = JsonSerializer.Deserialize(Of IDictionary(Of String, Deck))(fColRow.decks)
        fDeckNames = fDecks.Values.ToDictionary(Function(x) x.name, Function(x) x.id)
    End Sub

    Public Sub CleanUp()
        Dim Unused As New HashSet(Of Integer)(fDecks.Keys)
        For Each C As Card In fDB.Cards
            C.reps = 0
            C.left = 0
            Unused.Remove(C.did)
        Next
        For Each D As Deck In Unused.Select(Function(X) fDecks(X))
            fDeckNames.Remove(D.name)
            fDecks.Remove(D.id)
        Next
    End Sub

    Public Sub SaveChanges()
        fColRow.decks = JsonSerializer.Serialize(fDecks)
    End Sub

End Class