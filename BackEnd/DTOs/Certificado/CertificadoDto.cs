namespace Sistema.DTOs;

public class CertificadoDto
{
    public int IdCertificado { get; set; }

    public int AlunoId { get; set; }

    public string NomeAluno { get; set; } = string.Empty;

    public int CursoId { get; set; }

    public string NomeCurso { get; set; } = string.Empty;

    public DateTime DataEmissao { get; set; }

    public int CargaHoraria { get; set; }

    public string Codigo { get; set; } = string.Empty;
}