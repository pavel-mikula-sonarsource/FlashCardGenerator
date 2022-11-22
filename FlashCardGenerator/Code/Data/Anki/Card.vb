
Namespace Data.Anki

    Public Class Card

        Public Property id As Long      'Primary Key, date created, epoch miliseconds
        Public Property nid As Long     'Note.ID
        Public Property did As Long     'Deck.ID
        Public Property ord As Long     'Note.guild and Card.ord are used for skipping existing card. Anki cannot update cards, only notes.
        Public Property [mod] As Long   'Modified, epoch seconds
        Public Property usn As Long = -1
        Public Property type As Long    '0=new, 1=learning, 2=review, 3=relearning
        Public Property queue As Long
        Public Property due As Long     'For type=0: note id or random int
        Public Property ivl As Long
        Public Property factor As Long = 2500
        Public Property reps As Long
        Public Property lapses As Long
        Public Property left As Long
        Public Property odue As Long
        Public Property odid As Long
        Public Property flags As Long
        Public Property data As String = ""

        Public Sub New()
        End Sub

        Public Sub New(N As Note, D As Deck)
            id = NowEpochMilisID()
            nid = N.id
            due = N.id  'Note ID or Random ID
            Update(D)
        End Sub

        Public Function Update(D As Deck) As Boolean
            If D.id <> did Then
                did = D.id
                [mod] = NowEpochSeconds()
                Return True
            Else
                Return False
            End If
        End Function

    End Class

End Namespace