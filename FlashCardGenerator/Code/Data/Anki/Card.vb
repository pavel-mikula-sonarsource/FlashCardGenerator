
Namespace Data.Anki

    Public Class Card

        Public Property id As Long  'Primary Key, date created, epoch miliseconds
        Public Property nid As Long 'Note.ID
        Public Property did As Long 'Deck.ID
        Public Property ord As Long
        Public Property [mod] As Long 'Modified, epoch seconds
        Public Property usn As Long
        Public Property type As Long
        Public Property queue As Long
        Public Property due As Long
        Public Property ivl As Long
        Public Property factor As Long
        Public Property reps As Long
        Public Property lapses As Long
        Public Property left As Long
        Public Property odue As Long
        Public Property odid As Long
        Public Property flags As Long
        Public Property data As String

    End Class

End Namespace