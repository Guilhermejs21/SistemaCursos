namespace Sistema.DTOs;

public class MaterialApoioDto
{
    public int IdMaterial { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public DateTime DataPublicacao { get; set; }

    public bool Visivel { get; set; }

    public int CursoId { get; set; }

    public string NomeCurso { get; set; } = string.Empty;
}