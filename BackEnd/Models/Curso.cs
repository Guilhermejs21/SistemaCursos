using System.ComponentModel.DataAnnotations;
using Sistema.Enums;

namespace Sistema.Models;

public class Curso
{
    [Key]
    public int IdCurso { get; set; }

    public string NomeCurso { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public NivelCurso Nivel { get; set; }

    public int CargaHoraria { get; set; }

    public int ProfessorId { get; set; }

    public Professor Professor { get; set; } = null!;

    public List<Matricula> Matriculas { get; set; } = new();

    public List<Aula> Aulas { get; set; } = new();

    public List<MaterialApoio> MateriaisApoio { get; set; } = new();

    public Curso()
    {
    }

    public Curso(
        int idCurso,
        string nomeCurso,
        string descricao,
        NivelCurso nivel,
        int cargaHoraria,
        int professorId)
    {
        IdCurso = idCurso;
        NomeCurso = nomeCurso;
        Descricao = descricao;
        Nivel = nivel;
        CargaHoraria = cargaHoraria;
        ProfessorId = professorId;
    }

    public void AdicionarAula(Aula aula)
    {
        Aulas.Add(aula);
    }

    public void AdicionarMaterial(MaterialApoio material)
    {
        MateriaisApoio.Add(material);
    }

    public void AdicionarMatricula(Matricula matricula)
    {
        Matriculas.Add(matricula);
    }
}