using System.ComponentModel.DataAnnotations;
namespace Sistema.Models;
using Sistema.Enums;

public class Progresso
{
    [Key]
    public int IdProgresso { get; set; }

    public int AlunoId { get; set; }

    public Aluno Aluno { get; set; } = null!;

    public int AulaId { get; set; }

    public Aula Aula { get; set; } = null!;

    public decimal? NotaObtida { get; set; }

    public DateTime DataTentativa { get; set; }

    public StatusAula StatusAula { get; set; }

    public Progresso()
    {
    }

    public Progresso(
        int idProgresso,
        int alunoId,
        int aulaId,
        decimal notaObtida,
        DateTime dataTentativa,
        StatusAula statusAula)
    {
        IdProgresso = idProgresso;
        AlunoId = alunoId;
        AulaId = aulaId;
        NotaObtida = notaObtida;
        DataTentativa = dataTentativa;
        StatusAula = statusAula;
    }
}