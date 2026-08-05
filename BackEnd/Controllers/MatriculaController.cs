using Microsoft.AspNetCore.Mvc;
using Sistema.DTOs;
using Sistema.Data;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculasController : ControllerBase
{
    private readonly AppDbContext _context;

    public MatriculasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var matriculas = _context.Matriculas
            .Select(m => new
            {
                m.IdMatricula,
                m.AlunoId,
                NomeAluno = m.Aluno.NomeCompleto,
                m.CursoId,
                NomeCurso = m.Curso.NomeCurso
            })
            .ToList();

        return Ok(matriculas);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var matricula = _context.Matriculas
            .Where(m => m.IdMatricula == id)
            .Select(m => new
            {
                m.IdMatricula,
                m.AlunoId,
                NomeAluno = m.Aluno.NomeCompleto,
                m.CursoId,
                NomeCurso = m.Curso.NomeCurso
            })
            .FirstOrDefault();

        if (matricula is null)
        {
            return NotFound("Matrícula não encontrada");
        }

        return Ok(matricula);
    }

    [HttpPost]
    public IActionResult Criar(CriarMatriculaDto dados)
    {
        var aluno = _context.Alunos.Find(dados.AlunoId);
        if (aluno is null) { return BadRequest("Aluno não encontrado"); }

        var curso = _context.Cursos.Find(dados.CursoId);
        if (curso is null) { return BadRequest("Curso não encontrado"); }

        var matriculaExiste = _context.Matriculas.Any(m =>
            m.AlunoId == dados.AlunoId &&
            m.CursoId == dados.CursoId);
        if (matriculaExiste) { return BadRequest("Matrícula já existe"); }

        var matricula = new Matricula
        {
            AlunoId = dados.AlunoId,
            CursoId = dados.CursoId,
            DataMatricula = DateTime.Now,
            Ativa = true
        };

        _context.Matriculas.Add(matricula);
        _context.SaveChanges();

        var resposta = new MatriculaDto
        {
            IdMatricula = matricula.IdMatricula,
            AlunoId = matricula.AlunoId,
            NomeAluno = aluno.NomeCompleto,
            CursoId = matricula.CursoId,
            NomeCurso = curso.NomeCurso
        };

        return CreatedAtAction(
            nameof(Buscar),
            new { id = resposta.IdMatricula },
            resposta
        );
    }

    [HttpDelete("{id}")]
    public IActionResult Cancelar(int id)
    {
        var matricula = _context.Matriculas.Find(id);

        if (matricula is null)
        {
            return NotFound("Matrícula não encontrada.");
        }

        _context.Matriculas.Remove(matricula);
        _context.SaveChanges();

        return Ok("Matrícula cancelada com sucesso.");
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, AtualizarMatriculaDto dados)
    {
        var matricula = _context.Matriculas.Find(id);

        if (matricula is null)
        {
            return NotFound("Matrícula não encontrada.");
        }

        var aluno = _context.Alunos.Find(dados.AlunoId);
        if (aluno is null) { return BadRequest("Aluno não encontrado"); }

        var curso = _context.Cursos.Find(dados.CursoId);
        if (curso is null) { return BadRequest("Curso não encontrado"); }

        matricula.AlunoId = dados.AlunoId;
        matricula.CursoId = dados.CursoId;
        matricula.DataMatricula = DateTime.Now;
        matricula.Ativa = dados.Ativa;

        _context.SaveChanges();

        var resposta = new MatriculaDto
        {
            IdMatricula = matricula.IdMatricula,
            AlunoId = matricula.AlunoId,
            NomeAluno = aluno.NomeCompleto,
            CursoId = matricula.CursoId,
            NomeCurso = curso.NomeCurso
        };

        return Ok(resposta);
    }
}