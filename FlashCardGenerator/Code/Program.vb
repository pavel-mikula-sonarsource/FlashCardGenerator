
Imports FlashCardGenerator.Data
Imports SixLabors.ImageSharp

'https://github.com/ankidroid/Anki-Android/wiki/Database-Structure

Module Program

    Sub Main(args As String())
        Dim E, Es() As Employee
        Try
            'FIXME: REMOVE DEBUG
            Console.WindowWidth = 200

            If args.Length <> 2 Then
                Console.WriteLine("Argument error: Expected 2 arguments: <BambooHR token> <Path to SonarSourcers.apkg>")
                Return
            End If
            Dim Token As String = args(0)
            Dim File As String = IO.Path.GetFullPath(args(1))
            IO.File.Copy(File, $"{File} backup {Now:yyyy-MM-dd HH-mm-ss}.apkg")
            Es = DownloadEmployees(Token)
            Try
                Using Files As New FileManager(File)
                    Console.WriteLine("Opening database")
                    Using DB As New DbContext(Files.DbPath)
                        Dim Data As New DataManager(Files, DB)
                        Console.WriteLine("Processing cards")
                        For Each E In Es
                            If E.Picture IsNot Nothing Then
                                Try
                                    Data.Process(E)
                                Catch ex As Exception
                                    Console.WriteLine($"ERROR processing {E.Name}: {ex.GetType.Name} {ex.Message}")
                                End Try
                            End If
                        Next
                        Console.WriteLine("Database cleanup")
                        Data.CleanUp()
                        Console.WriteLine("Saving changes")
                        Data.SaveChanges()
                    End Using
                End Using
                Console.WriteLine("Done")
                Console.WriteLine()
                For Each E In Es.Where(Function(X) X.Picture Is Nothing)
                    Console.WriteLine("Missing picture, skipped: " & E.Name)
                Next
            Finally
                For Each E In Es
                    E.Dispose()
                Next
            End Try
        Catch ex As Exception
            Console.WriteLine(ex.GetType.Name & ": " & ex.Message)
            Console.WriteLine()
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Function DownloadEmployees(Token As String) As Employee()
        'FIXME: Download data from BambooHR
        Return {
            New Employee(1, "Abass Sabra", "Languages Team", "Geneva", LoadImage("d:\_temp\Anki\Abbas Sabra.jpg")),
            New Employee(2, "Alban Auzeill", "Languages Team", "Geneva", LoadImage("d:\_temp\Anki\Alban Auzeill.jpg")),
            New Employee(3, "Pavel Mikula", "Languages Team", "Geneva", LoadImage("d:\_temp\Anki\Pavel Mikula.jpg")),
            New Employee(4, "Alicia Savoia", "P&C Team", "Geneva", LoadImage("d:\_temp\Anki\Alicia Savoia.jpg")),
            New Employee(5, "Tom Howlet", "PM Team", "Remote", LoadImage("d:\_temp\Anki\Tom Howlet.jpg")),
            New Employee(6, "New Joiner", "P&C Team", "Geneva", Nothing)
        }
    End Function

    Private Function LoadImage(fn As String) As Image
        Return Image.Load(fn)
    End Function

End Module
