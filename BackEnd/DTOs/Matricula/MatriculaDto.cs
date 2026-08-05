namespace Sistema.DTOs;

public class MatriculaDto
{
    public int IdMatricula { get; set; }

    public int AlunoId { get; set; }
    public string NomeAluno { get; set; } = string.Empty;

    public int CursoId { get; set; }
    public string NomeCurso { get; set; } = string.Empty;
}