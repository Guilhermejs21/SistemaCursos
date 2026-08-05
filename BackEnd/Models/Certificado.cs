using System.ComponentModel.DataAnnotations;
namespace Sistema.Models;

public class Certificado
{
    [Key]
    public int IdCertificado { get; set; }

    public DateTime DataEmissao { get; set; }

    public int CargaHoraria { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public int AlunoId { get; set; }

    public Aluno Aluno { get; set; } = null!;

    public int CursoId { get; set; }

    public Curso Curso { get; set; } = null!;

    public Certificado()
    {
    }

    public Certificado(
        int idCertificado,
        DateTime dataEmissao,
        int cargaHoraria,
        string codigo,
        int alunoId,
        int cursoId)
    {
        IdCertificado = idCertificado;
        DataEmissao = dataEmissao;
        CargaHoraria = cargaHoraria;
        Codigo = codigo;
        AlunoId = alunoId;
        CursoId = cursoId;
    }
}