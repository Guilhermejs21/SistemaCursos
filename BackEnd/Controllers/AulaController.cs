using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.DTOs;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AulaController : ControllerBase
{
    private readonly AppDbContext _context;

    public AulaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var aulas = _context.Aulas
            .Select(a => new AulaDto
            {
                IdAula = a.IdAula,
                TituloAula = a.TituloAula,
                ConteudoTexto = a.ConteudoTexto,
                Ordem = a.Ordem,
                CursoId = a.CursoId,
                NomeCurso = a.Curso.NomeCurso
            })
            .ToList();

        return Ok(aulas);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var aula = _context.Aulas
            .Where(a => a.IdAula == id)
            .Select(a => new AulaDto
            {
                IdAula = a.IdAula,
                TituloAula = a.TituloAula,
                ConteudoTexto = a.ConteudoTexto,
                Ordem = a.Ordem,
                CursoId = a.CursoId,
                NomeCurso = a.Curso.NomeCurso
            })
            .FirstOrDefault();

        if (aula is null)
        {
            return NotFound("Aula não encontrada.");
        }

        return Ok(aula);
    }

    [HttpPost]
    public IActionResult Criar(CriarAulaDto dados)
    {
        var curso = _context.Cursos.Find(dados.CursoId);

        if (curso is null) { return BadRequest("Curso não encontrado."); }

        var ordemExiste = _context.Aulas.Any(a =>
            a.CursoId == dados.CursoId &&
            a.Ordem == dados.Ordem);

        if (ordemExiste) { return BadRequest("Já existe uma aula com essa ordem neste curso."); }

        if (dados.Ordem <= 0) { return BadRequest("A ordem da aula deve ser maior que zero."); }

        var tituloExiste = _context.Aulas.Any(a =>
            a.CursoId == dados.CursoId &&
            a.TituloAula == dados.TituloAula);

        if (tituloExiste) { return BadRequest("Já existe uma aula com esse título neste curso."); }

        var aula = new Aula
        {
            TituloAula = dados.TituloAula,
            ConteudoTexto = dados.ConteudoTexto,
            Ordem = dados.Ordem,
            CursoId = dados.CursoId
        };

        _context.Aulas.Add(aula);
        _context.SaveChanges();

        var resposta = new AulaDto
        {
            IdAula = aula.IdAula,
            TituloAula = aula.TituloAula,
            ConteudoTexto = aula.ConteudoTexto,
            Ordem = aula.Ordem,
            CursoId = aula.CursoId,
            NomeCurso = curso.NomeCurso
        };

        return CreatedAtAction(
            nameof(Buscar),
            new { id = resposta.IdAula },
            resposta
        );

    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, AtualizarAulaDto dados)
    {
        var aula = _context.Aulas.Find(id);

        if (aula is null)
        {
            return NotFound("Aula não encontrada.");
        }

        var curso = _context.Cursos.Find(dados.CursoId);

        if (curso is null)
        {
            return BadRequest("Curso não encontrado.");
        }

        if (dados.Ordem <= 0)
        {
            return BadRequest("A ordem da aula deve ser maior que zero.");
        }

        var ordemExiste = _context.Aulas.Any(a =>
            a.IdAula != id &&
            a.CursoId == dados.CursoId &&
            a.Ordem == dados.Ordem);

        if (ordemExiste)
        {
            return BadRequest("Já existe outra aula com essa ordem neste curso.");
        }

        var tituloExiste = _context.Aulas.Any(a =>
            a.IdAula != id &&
            a.CursoId == dados.CursoId &&
            a.TituloAula == dados.TituloAula);

        if (tituloExiste)
        {
            return BadRequest("Já existe outra aula com esse título neste curso.");
        }

        aula.TituloAula = dados.TituloAula;
        aula.ConteudoTexto = dados.ConteudoTexto;
        aula.Ordem = dados.Ordem;
        aula.CursoId = dados.CursoId;

        _context.SaveChanges();

        var resposta = new AulaDto
        {
            IdAula = aula.IdAula,
            TituloAula = aula.TituloAula,
            ConteudoTexto = aula.ConteudoTexto,
            Ordem = aula.Ordem,
            CursoId = aula.CursoId,
            NomeCurso = curso.NomeCurso
        };

        return Ok(resposta);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var aula = _context.Aulas.Find(id);

        if (aula is null)
        {
            return NotFound("Aula não encontrada.");
        }

        var possuiAvaliacoes = _context.Avaliacoes
            .Any(a => a.AulaId == id);

        if (possuiAvaliacoes)
        {
            return BadRequest(
                "A aula não pode ser deletada porque possui avaliações."
            );
        }

        var possuiProgressos = _context.Progressos
            .Any(p => p.AulaId == id);

        if (possuiProgressos)
        {
            return BadRequest(
                "A aula não pode ser deletada porque possui progressos registrados."
            );
        }

        _context.Aulas.Remove(aula);
        _context.SaveChanges();

        return Ok("Aula deletada com sucesso.");
    }

}
