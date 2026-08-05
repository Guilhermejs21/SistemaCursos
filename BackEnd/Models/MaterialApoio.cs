using System.ComponentModel.DataAnnotations;
namespace Sistema.Models;

public class MaterialApoio
{
    [Key]
    public int IdMaterial { get; set; }

    public string Titulo { get; set; } = string.Empty;

    public DateTime DataPublicacao { get; set; }

    public bool Visivel { get; set; }

    public int CursoId { get; set; }

    public Curso Curso { get; set; } = null!;

    public MaterialApoio()
    {
    }

    public MaterialApoio(
        int idMaterial,
        string titulo,
        DateTime dataPublicacao,
        bool visivel,
        int cursoId)
    {
        IdMaterial = idMaterial;
        Titulo = titulo;
        DataPublicacao = dataPublicacao;
        Visivel = visivel;
        CursoId = cursoId;
    }

    public void Publicar()
    {
        Visivel = true;
    }
}