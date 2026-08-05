using Sistema.Enums;

namespace Sistema.DTOs;

public class AtualizarProgressoDto
{
    public decimal? NotaObtida { get; set; }

    public StatusAula StatusAula { get; set; }
}