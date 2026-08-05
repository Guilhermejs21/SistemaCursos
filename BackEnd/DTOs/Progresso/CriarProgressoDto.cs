using Sistema.Enums;

namespace Sistema.DTOs;

public class CriarProgressoDto
{
    public int AlunoId { get; set; }
    public int AulaId { get; set; }
    public decimal? NotaObtida { get; set; }
    public StatusAula StatusAula { get; set; }
}