namespace Sistema.DTOs;

public class AtualizarAulaDto
{
    public string TituloAula { get; set; } = string.Empty;
    public string ConteudoTexto { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public int CursoId { get; set; }
}