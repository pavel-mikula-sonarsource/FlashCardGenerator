
Imports SixLabors.ImageSharp

Namespace Data

    Public NotInheritable Class Employee
        Implements IDisposable

        Public ReadOnly ID As Integer
        Public ReadOnly Name, Team, Office As String
        Public ReadOnly Picture As Image

        Public Sub New(ID As Integer, Name As String, Team As String, Office As String, Picture As Image)
            Me.ID = ID : Me.Name = Name : Me.Team = Team : Me.Office = Office : Me.Picture = Picture.Resize(800, 800)
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Picture?.Dispose()
        End Sub

    End Class

End Namespace