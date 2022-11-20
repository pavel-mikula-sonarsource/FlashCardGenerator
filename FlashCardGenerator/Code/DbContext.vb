
Imports FlashCardGenerator.Data.Anki
Imports Microsoft.EntityFrameworkCore

Public Class DbContext
    Inherits Microsoft.EntityFrameworkCore.DbContext

    Public Property Cards As DbSet(Of Card)
    Public Property Cols As DbSet(Of Col)
    Public Property Notes As DbSet(Of Note)

    Private ReadOnly fPath As String

    Public Sub New(Path As String)
        fPath = Path
    End Sub

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlite($"DataSource={fPath};")
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
        modelBuilder.Entity(Of Card)(Sub(X)
                                         X.ToTable("cards")
                                         X.HasIndex(Function(e) e.nid, "ix_cards_nid")
                                         X.HasIndex(Function(e) New With {e.did, e.queue, e.due}, "ix_cards_sched")
                                         X.HasIndex(Function(e) e.usn, "ix_cards_usn")
                                         X.Property(Function(e) e.id).ValueGeneratedNever()
                                     End Sub)
        modelBuilder.Entity(Of Col)(Sub(x)
                                        x.ToTable("col")
                                        x.Property(Function(e) e.id).ValueGeneratedNever()
                                    End Sub)
        modelBuilder.Entity(Of Note)(Sub(x)
                                         x.ToTable("notes")
                                         x.HasIndex(Function(e) e.csum, "ix_notes_csum")
                                         x.HasIndex(Function(e) e.usn, "ix_notes_usn")
                                         x.Property(Function(e) e.id).ValueGeneratedNever()
                                     End Sub)
    End Sub

End Class