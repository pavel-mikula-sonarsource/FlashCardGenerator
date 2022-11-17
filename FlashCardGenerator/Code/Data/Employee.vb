
Imports SixLabors.ImageSharp

Namespace Data

    Public Class Employee

        Public ReadOnly ID As Integer
        Public ReadOnly Name, Team, Office As String
        Public ReadOnly Picture As IImage

        Public Sub New(ID As Integer, Name As String, Team As String, Office As String, Picture As IImage)
            Me.ID = ID : Me.Name = Name : Me.Team = Team : Me.Office = Office : Me.Picture = Picture
        End Sub

    End Class

End Namespace