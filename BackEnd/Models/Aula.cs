using System.ComponentModel.DataAnnotations;

namespace Sistema.Models;

public class Aula
{
    [Key]
    public int IdAula { get; set; }

    public string TituloAula { get; set; } = string.Empty;

    public string ConteudoTexto { get; set; } = string.Empty;

    public int Ordem { get; set; }

    public int CursoId { get; set; }

    public Curso Curso { get; set; } = null!;

    public List<Avaliacao> Avaliacoes { get; set; } = new();

    public List<Progresso> Progressos { get; set; } = new();

    public Aula()
    {
    }

    public Aula(
        int idAula,
        string tituloAula,
        string conteudoTexto,
        int ordem,
        int cursoId)
    {
        IdAula = idAula;
        TituloAula = tituloAula;
        ConteudoTexto = conteudoTexto;
        Ordem = ordem;
        CursoId = cursoId;
    }

    public void AdicionarAvaliacao(Avaliacao avaliacao)
    {
        Avaliacoes.Add(avaliacao);
    }

    public void AdicionarProgresso(Progresso progresso)
    {
        Progressos.Add(progresso);
    }
}