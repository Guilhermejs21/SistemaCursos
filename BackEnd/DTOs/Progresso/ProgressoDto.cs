using Sistema.Enums;

namespace Sistema.DTOs;

public class ProgressoDto
{
    public int IdProgresso { get; set; }

    public int AlunoId { get; set; }
    public string NomeAluno { get; set; } = string.Empty;

    public int AulaId { get; set; }
    public string TituloAula { get; set; } = string.Empty;

    public decimal? NotaObtida { get; set; }
    public DateTime DataTentativa { get; set; }
    public StatusAula StatusAula { get; set; }
}