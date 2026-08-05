using System.ComponentModel.DataAnnotations;
namespace Sistema.Models;

public class Medalha
{
    [Key]
    public int IdMedalha { get; set; }

    public string NomeMedalha { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string IconeCaminho { get; set; } = string.Empty;

    public Medalha()
    {
    }

    public Medalha(
        int idMedalha,
        string nomeMedalha,
        string descricao,
        string iconeCaminho)
    {
        IdMedalha = idMedalha;
        NomeMedalha = nomeMedalha;
        Descricao = descricao;
        IconeCaminho = iconeCaminho;
    }
}