using Microsoft.EntityFrameworkCore;
using Sistema.Models;

namespace Sistema.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }

    public DbSet<Professor> Professores { get; set; }

    public DbSet<Administrador> Administradores { get; set; }

    public DbSet<Curso> Cursos { get; set; }

    public DbSet<Matricula> Matriculas { get; set; }

    public DbSet<Aula> Aulas { get; set; }

    public DbSet<MaterialApoio> MateriaisApoio { get; set; }

    public DbSet<Avaliacao> Avaliacoes { get; set; }

    public DbSet<Progresso> Progressos { get; set; }

    public DbSet<Certificado> Certificados { get; set; }

    public DbSet<Medalha> Medalhas { get; set; }

    public DbSet<UsuarioMedalha> UsuarioMedalhas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Matricula>()
            .HasOne(m => m.Aluno)
            .WithMany(a => a.Matriculas)
            .HasForeignKey(m => m.AlunoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Matricula>()
            .HasOne(m => m.Curso)
            .WithMany(c => c.Matriculas)
            .HasForeignKey(m => m.CursoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Matricula>()
            .HasIndex(m => new { m.AlunoId, m.CursoId })
            .IsUnique();

        modelBuilder.Entity<Progresso>()
            .Property(p => p.NotaObtida)
            .HasPrecision(5, 2);
    }
}