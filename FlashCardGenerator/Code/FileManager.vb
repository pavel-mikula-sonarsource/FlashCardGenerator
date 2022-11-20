
Imports System.IO
Imports System.IO.Compression
Imports System.Text.Json

Public Class FileManager
    Implements IDisposable

    Public ReadOnly DbPath As String
    Public ReadOnly Media As IDictionary(Of String, String)

    Private ReadOnly fPackagePath, fTempDir, fMediaPath As String

    Public Sub New(PackagePath As String)
        fPackagePath = PackagePath
        fTempDir = Path.Combine(Path.GetTempPath, "FlashCardGenerator")
        If Directory.Exists(fTempDir) Then
            Console.WriteLine("Deleting " & fTempDir)
            Directory.Delete(fTempDir, True)
        End If
        Directory.CreateDirectory(fTempDir)
        Console.WriteLine("Extracting existing content")
        ZipFile.ExtractToDirectory(PackagePath, fTempDir)
        DbPath = Path.Combine(fTempDir, "collection.anki2")
        fMediaPath = Path.Combine(fTempDir, "media")
        Media = JsonSerializer.Deserialize(Of IDictionary(Of String, String))(File.ReadAllText(fMediaPath))
    End Sub

    Public Sub SaveChanges()
        Dim NewPath As String = fPackagePath & ".new"
        Console.WriteLine("Creating new package")
        If File.Exists(NewPath) Then File.Delete(NewPath)
        File.WriteAllText(fMediaPath, JsonSerializer.Serialize(Media))
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