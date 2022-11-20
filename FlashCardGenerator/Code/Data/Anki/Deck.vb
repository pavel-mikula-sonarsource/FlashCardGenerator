
Namespace Data.Anki

    Public Class Deck

        Public Property newToday As Integer() = {0, 0}
        Public Property revToday As Integer() = {0, 0}
        Public Property lrnToday As Integer() = {0, 0}
        Public Property timeToday As Integer() = {0, 0}
        Public Property conf As Long = 1
        Public Property usn As Long = -1
        Public Property desc As String
        Public Property dyn As Long
        Public Property collapsed As Boolean
        Public Property extendNew As Long = 10
        Public Property extendRev As Long = 50
        Public Property id As Long
        Public Property name As String
        Public Property [mod] As Long
        Public Property browserCollapsed As Boolean

        Public Sub New()
        End Sub

        Public Sub New(Name As String)
            id = NowEpochMilisID()
            Me.name = Name
            [mod] = NowEpochSeconds()
        End Sub

    End Class

End Namespace