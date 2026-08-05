namespace Sistema.DTOs;

public class AulaDto
{
    public int IdAula { get; set; }
    public string TituloAula { get; set; } = string.Empty;
    public string ConteudoTexto { get; set; } = string.Empty;
    public int Ordem { get; set; }

    public int CursoId { get; set; }
    public string NomeCurso { get; set; } = string.Empty;
}