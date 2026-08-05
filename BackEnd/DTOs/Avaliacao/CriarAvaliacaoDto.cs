namespace Sistema.DTOs;

public class CriarAvaliacaoDto
{
    public string Pergunta { get; set; } = string.Empty;

    public string OpcaoA { get; set; } = string.Empty;

    public string OpcaoB { get; set; } = string.Empty;

    public string OpcaoC { get; set; } = string.Empty;

    public string OpcaoD { get; set; } = string.Empty;

    public string RespostaCorreta { get; set; } = string.Empty;

    public int AulaId { get; set; }
}