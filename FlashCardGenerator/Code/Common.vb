
Imports System.Globalization
Imports System.IO
Imports System.Text

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

    Public Function KillDiacritics(Value As String) As String
        Dim sb As New StringBuilder
        For Each Ch As Char In Value.Normalize(NormalizationForm.FormD)
            If CharUnicodeInfo.GetUnicodeCategory(Ch) <> UnicodeCategory.NonSpacingMark Then sb.Append(Ch)
        Next
        Return sb.ToString
    End Function

    Public Function KillInvalidFileNameChars(FileName As String) As String
        Dim sb As New StringBuilder, Skip As New HashSet(Of Char)(Path.GetInvalidFileNameChars)
        For Each C As Char In FileName
            If Not Skip.Contains(C) Then sb.Append(C)
        Next
        Return sb.ToString
    End Function

End Module
