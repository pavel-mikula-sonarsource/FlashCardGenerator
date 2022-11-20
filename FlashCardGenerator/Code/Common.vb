
Public Module Common

    Private fEpochMilisPrev As Long

    Public Function NowEpochMilisID() As Long    'Used as ID => must be unique
        Dim Ret As Long = DateTimeOffset.Now.ToUnixTimeMilliseconds
        If Ret = fEpochMilisPrev Then Ret += 1
        fEpochMilisPrev = Ret
        Return Ret
    End Function

    Public Function NowEpochSeconds() As Long
        Return DateTimeOffset.Now.ToUnixTimeSeconds
    End Function

End Module
