
Imports System.Text.Json
Imports FlashCardGenerator.Data
Imports FlashCardGenerator.Data.Anki

Public Class DataManager

    Private ReadOnly fFiles As FileManager
    Private ReadOnly fDB As DbContext
    Private ReadOnly fColRow As Col
    Private ReadOnly fDecks As IDictionary(Of Integer, Deck) 'ID => Deck
    Private ReadOnly fDeckNames As IDictionary(Of String, Integer) 'ID => Deck

    Public Sub New(Files As FileManager, DB As DbContext)
        fFiles = Files : fDB = DB
        fColRow = DB.Cols.Single
        fDecks = JsonSerializer.Deserialize(Of IDictionary(Of String, Deck))(fColRow.decks)
        fDeckNames = fDecks.Values.ToDictionary(Function(x) x.name, Function(x) x.id)
    End Sub

    Public Sub Process(E As Employee)
        'FIXM

        'FIXME: Update Anki data
        'FIXME: Save media to disk
        'FIXME: Save pictures to disk
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
        fDB.SaveChanges()
        Microsoft.Data.Sqlite.SqliteConnection.ClearAllPools()  'Release file locks to be able to ZIP and delete the EF file
        fFiles.SaveChanges()
    End Sub

End Class