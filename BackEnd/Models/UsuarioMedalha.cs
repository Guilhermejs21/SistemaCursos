using System.ComponentModel.DataAnnotations;
namespace Sistema.Models;

public class UsuarioMedalha
{
    [Key]
    public int IdConquista { get; set; }

    public int AlunoId { get; set; }

    public Aluno Aluno { get; set; } = null!;

    public int MedalhaId { get; set; }

    public Medalha Medalha { get; set; } = null!;

    public DateTime DataGanho { get; set; }

    public UsuarioMedalha()
    {
    }

    public UsuarioMedalha(
        int idConquista,
        int alunoId,
        int medalhaId,
        DateTime dataGanho)
    {
        IdConquista = idConquista;
        AlunoId = alunoId;
        MedalhaId = medalhaId;
        DataGanho = dataGanho;
    }
}