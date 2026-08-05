using System.ComponentModel.DataAnnotations;

namespace Sistema.Models;

public class Avaliacao
{
    [Key]
    public int IdAvaliacao { get; set; }

    public string Pergunta { get; set; } = string.Empty;

    public string OpcaoA { get; set; } = string.Empty;

    public string OpcaoB { get; set; } = string.Empty;

    public string OpcaoC { get; set; } = string.Empty;

    public string OpcaoD { get; set; } = string.Empty;

    public string RespostaCorreta { get; set; } = string.Empty;

    public int AulaId { get; set; }

    public Aula Aula { get; set; } = null!;

    public Avaliacao()
    {
    }

    public Avaliacao(
        int idAvaliacao,
        string pergunta,
        string opcaoA,
        string opcaoB,
        string opcaoC,
        string opcaoD,
        string respostaCorreta,
        int aulaId)
    {
        IdAvaliacao = idAvaliacao;
        Pergunta = pergunta;
        OpcaoA = opcaoA;
        OpcaoB = opcaoB;
        OpcaoC = opcaoC;
        OpcaoD = opcaoD;
        RespostaCorreta = respostaCorreta;
        AulaId = aulaId;
    }
}