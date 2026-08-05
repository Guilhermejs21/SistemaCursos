namespace Sistema.DTOs;

public class AtualizarMaterialApoioDto
{
    public string Titulo { get; set; } = string.Empty;

    public bool Visivel { get; set; }

    public int CursoId { get; set; }
}