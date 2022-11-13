
Namespace Data.Anki

    Public Class Note

        Public ReadOnly ID As Integer   'Primary Key, date created, epoch miliseconds
        Public ReadOnly Guid As Guid    'For sync
        Public ReadOnly mID As Integer  'Const model ID
        Public ReadOnly [Mod] As Integer 'Modified, epoch seconds
        Public Const Usn As Integer = -1    'Universal Serial Number for sync, not important
        Public ReadOnly Flds As String           'Fields (Question 0x1F Answer)
        Public ReadOnly CSum As Integer 'Checksum for duplicate check: integer representation of first 8 digits of sha1 hash of the first field

    End Class

End Namespace