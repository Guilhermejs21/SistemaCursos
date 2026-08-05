using System.ComponentModel.DataAnnotations;

namespace Sistema.Models;

public class Matricula
{
    [Key]
    public int IdMatricula { get; set; }

    public int AlunoId { get; set; }

    public Aluno Aluno { get; set; } = null!;

    public int CursoId { get; set; }

    public Curso Curso { get; set; } = null!;

    public DateTime DataMatricula { get; set; }

    public bool Ativa { get; set; }

    public Matricula()
    {
    }

    public Matricula(
        int alunoId,
        int cursoId,
        DateTime dataMatricula,
        bool ativa)
    {
        AlunoId = alunoId;
        CursoId = cursoId;
        DataMatricula = dataMatricula;
        Ativa = ativa;
    }
}