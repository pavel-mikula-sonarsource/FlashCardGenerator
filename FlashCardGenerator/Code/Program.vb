
Imports FlashCardGenerator.Data

'https://github.com/ankidroid/Anki-Android/wiki/Database-Structure

Module Program

    Sub Main(args As String())
        Dim E, Es() As Employee
        Try
            If args.Length <> 1 Then
                Console.WriteLine("Argument error: Expected 1 argument with BambooHR token")
                Return
            End If
            Es = DownloadEmployees(args.Single)
            'FIXME: Read Anki data
            'FIXME: Update Anki data
            'FIXME: Store data to DB
            'FIXME: Save DB to disk
            'FIXME: Save media to disk
            'FIXME: Save pictures to disk
            'FIXME: Zip result
        Catch ex As Exception
            Console.WriteLine(ex.GetType.Name & ": " & ex.Message)
            Console.WriteLine()
            Console.WriteLine(ex.ToString)
        End Try
    End Sub

    Private Function DownloadEmployees(Token As String) As Employee()
        'FIXME: Download data from BambooHR
        Return {
            New Employee(1, "Abass Sabra", "Languages Team", "Geneva", Drawing.Image.FromFile("d:\_temp\Anki\Abbas Sabra.jpg")),
            New Employee(2, "Alban Auzeill", "Languages Team", "Geneva", Drawing.Image.FromFile("d:\_temp\Anki\Alban Auzeill.jpg")),
            New Employee(3, "Pavel Mikula", "Languages Team", "Geneva", Drawing.Image.FromFile("d:\_temp\Anki\Pavel Mikula.jpg")),
            New Employee(4, "Alicia Savoia", "P&C Team", "Geneva", Drawing.Image.FromFile("d:\_temp\Anki\Alicia Savoia.jpg"))
        }
    End Function

End Module
