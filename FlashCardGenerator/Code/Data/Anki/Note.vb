
Namespace Data.Anki

    Public Class Note

        Public Property id As Long          'Primary Key, date created, epoch miliseconds
        Public Property guid As String      'For sync, values are messed up. Note.guild and Card.ord are used for skipping existing card. Anki cannot update cards, only notes.
        Public Property mid As Long = 1561930677787    'Const model ID. Needs to be same for updating notes
        Public Property [mod] As Long       'Modified, epoch seconds
        Public Property usn As Long = -1    'Universal Serial Number for sync, not important
        Public Property tags As String = ""
        Public Property flds As String      'Fields (Question 0x1F Answer)
        Public Property sfld As Long
        Public Property csum As Long        'Checksum for duplicate check: integer representation of first 8 digits of sha1 hash of the first field
        Public Property flags As Long
        Public Property data As String = ""

        Public Sub New()
        End Sub

        Public Sub New(UniqueID As String, Flds As String)
            id = NowEpochMilisID()
            guid = UniqueID
            [mod] = NowEpochSeconds()
            Me.flds = Flds
            csum = Flds.GetHashCode
        End Sub

    End Class

End Namespace