
Imports System.Text.Json
Imports FlashCardGenerator.Data
Imports FlashCardGenerator.Data.Anki

Public Class DataManager

    Private ReadOnly fFiles As FileManager
    Private ReadOnly fDB As DbContext
    Private ReadOnly fColRow As Col
    Private ReadOnly fDecks As IDictionary(Of Long, Deck)       'ID => Deck
    Private ReadOnly fDeckNames As IDictionary(Of String, Deck) 'Name => Deck
    Private ReadOnly fItems As IDictionary(Of String, Item)

    Public Sub New(Files As FileManager, DB As DbContext)
        fFiles = Files : fDB = DB
        fColRow = DB.Cols.Single
        fDecks = JsonSerializer.Deserialize(Of IDictionary(Of Long, Deck))(fColRow.decks)
        fDeckNames = fDecks.Values.ToDictionary(Function(x) x.name, Function(x) x)
        fItems = DB.Cards.Join(DB.Notes, Function(X) X.nid, Function(X) X.id, Function(C, N) New Item(C, N)).ToDictionary(Function(X) X.Note.flds, Function(X) X)
    End Sub

    Public Sub Process(E As Employee)
        Dim ImageFN As String = fFiles.AddImage(E)
        Dim Flds As String = SerializeFields($"<img src=""{ImageFN}"" />", $"<div>{E.Name}</div><div>{E.Team}</div><div>{E.Office}</div>")
        If fItems.ContainsKey(Flds) Then
            fItems(Flds).Used = True
        Else
            Console.WriteLine("Adding: " & Flds)
            Dim N As New Note(Flds)
            fDB.Notes.Add(N)
            fDB.Cards.Add(New Card(N, EnsureDeck("SonarSourcers::" & E.Office)))
        End If
    End Sub

    Public Sub CleanUp()
        Dim Unused As New HashSet(Of Long)(fDecks.Keys)
        For Each I As Item In fItems.Values.Where(Function(X) Not X.Used)
            Console.WriteLine("Removing: " & I.Note.flds)
            fDB.Cards.Remove(I.Card)
            fDB.Notes.Remove(I.Note)
        Next
        fDB.SaveChanges()
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

    Private Function EnsureDeck(Name As String) As Deck
        If fDeckNames.ContainsKey(Name) Then
            Return fDeckNames(Name)
        Else
            Dim D As New Deck(Name)
            fDecks.Add(D.id, D)
            fDeckNames.Add(D.name, D)
            Return D
        End If
    End Function

    Private Shared Function SerializeFields(ParamArray Fields As String()) As String
        Return String.Join(Chr(31), Fields)
    End Function

    Private Class Item

        Public ReadOnly Card As Card
        Public ReadOnly Note As Note
        Public Used As Boolean

        Public Sub New(Card As Card, Note As Note)
            Me.Card = Card : Me.Note = Note
        End Sub

    End Class

End Class