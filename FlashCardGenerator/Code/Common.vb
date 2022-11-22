
Imports System.Globalization
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports SixLabors.ImageSharp
Imports SixLabors.ImageSharp.Processing

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

    Public Function PrintifyHtml(Html As String) As String
        Return Web.HttpUtility.HtmlDecode(RegularExpressions.Regex.Replace(Html, "<.*?>", " "))
    End Function

    <Extension>
    Public Function Resize(Img As Image, Width As Integer, Height As Integer) As Image
        If Img IsNot Nothing Then
            Dim W, H As Integer
            If Img.Width = Img.Height Then
                W = Width : H = Height
            ElseIf Img.Width > Img.Height Then
                W = CInt(Width * Img.Width / Img.Height) : H = Height
            Else
                W = Width : H = CInt(Height * Img.Height / Img.Width)
            End If
            Img.Mutate(Function(X) X.Resize(W, H).Crop(Width, Height))
        End If
        Return Img
    End Function

End Module
