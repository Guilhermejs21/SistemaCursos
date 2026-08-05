using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursosController : ControllerBase
{
    private readonly AppDbContext _context;

    public CursosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var cursos = _context.Cursos.ToList();

        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var curso = _context.Cursos.Find(id);
        if (curso is null) { return NotFound("Curso não encontrado"); }

        return Ok(curso);
    }

    [HttpPost]
    public IActionResult Criar(Curso curso)
    {
        var prof = _context.Professores.Find(curso.ProfessorId);
        if (prof is null) { return BadRequest("Professor não encontrado"); }

        _context.Cursos.Add(curso);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Buscar), new { id = curso.IdCurso }, curso);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Curso curso)
    {
        var cursoExistente = _context.Cursos.Find(id);
        if (cursoExistente is null) { return NotFound("Curso não encontrado"); }

        cursoExistente.NomeCurso = curso.NomeCurso;
        cursoExistente.Descricao = curso.Descricao;
        cursoExistente.CargaHoraria = curso.CargaHoraria;
        cursoExistente.Nivel = curso.Nivel;
        cursoExistente.ProfessorId = curso.ProfessorId;

        _context.SaveChanges();
        return Ok(cursoExistente);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var curso = _context.Cursos.Find(id);
        if (curso is null) { return NotFound("Curso não encontrado"); }
        _context.Cursos.Remove(curso);
        _context.SaveChanges();
        return Ok("Curso deletado com sucesso");
    }
}

