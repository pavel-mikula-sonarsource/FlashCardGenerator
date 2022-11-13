
Namespace Data.Anki

    Public Class Card

        Public ReadOnly ID As Integer   'Primary Key, date created, epoch miliseconds
        Public ReadOnly nID As Integer  'Note.ID
        Public ReadOnly dID As Integer  'Deck.ID
        Public ReadOnly [Mod] As Integer 'Modified, epoch seconds
        'Due => nID pro nove karty

    End Class

End Namespace