
Namespace Data.Anki

    Public Class Note

        Public Property id As Long      'Primary Key, date created, epoch miliseconds
        Public Property guid As String  'For sync
        Public Property mid As Long     'Const model ID
        Public Property [mod] As Long     'Modified, epoch seconds
        Public Property usn As Long     'Universal Serial Number for sync, not important
        Public Property tags As String
        Public Property flds As String  'Fields (Question 0x1F Answer)
        Public Property sfld As Long
        Public Property csum As Long    'Checksum for duplicate check: integer representation of first 8 digits of sha1 hash of the first field
        Public Property flags As Long
        Public Property data As String

    End Class

End Namespace