
Imports System.IO
Imports System.IO.Compression
Imports System.Text.Json
Imports FlashCardGenerator.Data
Imports SixLabors.ImageSharp

Public Class FileManager
    Implements IDisposable

    Private Const DbName As String = "collection.anki2"

    Public ReadOnly DbPath As String
    Public ReadOnly Media As New Dictionary(Of Integer, String)

    Private ReadOnly fPackagePath, fTempDir As String

    Public Sub New(PackagePath As String)
        fPackagePath = PackagePath
        fTempDir = Path.Combine(Path.GetTempPath, "FlashCardGenerator")
        If Directory.Exists(fTempDir) Then
            Console.WriteLine("Deleting " & fTempDir)
            Directory.Delete(fTempDir, True)
        End If
        Directory.CreateDirectory(fTempDir)
        Console.WriteLine("Extracting existing content")
        DbPath = Path.Combine(fTempDir, DbName)
        Using ZA As ZipArchive = ZipFile.OpenRead(PackagePath)
            ZA.GetEntry(DbName).ExtractToFile(DbPath)
        End Using
    End Sub

    Public Function AddImage(E As Employee) As String
        Dim ID As Integer = Media.Count
        Dim Ret As String = KillInvalidFileNameChars(KillDiacritics(E.Name)).Trim.Replace(" "c, "-"c) & ".jpg"

        'FIXME: Resize 800x800

        E.Picture.SaveAsJpeg(Path.Combine(fTempDir, ID.ToString))
        Media.Add(ID, Ret)
        Return Ret
    End Function

    Public Sub SaveChanges()
        Dim NewPath As String = fPackagePath & ".new"
        Console.WriteLine("Creating new package")
        If File.Exists(NewPath) Then File.Delete(NewPath)
        File.WriteAllText(Path.Combine(fTempDir, "media"), JsonSerializer.Serialize(Media))
        ZipFile.CreateFromDirectory(fTempDir, NewPath)
        Console.WriteLine("Overriding the original file " & fPackagePath)
        File.Move(NewPath, fPackagePath, True)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Try
            Console.WriteLine("Cleanup: Deleting " & fTempDir)
            Directory.Delete(fTempDir, True)
        Catch ex As Exception
            Console.WriteLine("ERROR: Failed to delete temp dir: " & ex.Message)
        End Try
    End Sub

End Class