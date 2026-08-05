using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProfessoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var professores = _context.Professores.ToList();

        return Ok(professores);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var professor = _context.Professores.Find(id);
        if (professor is null) { return NotFound("Professor não encontrado"); }

        return Ok(professor);
    }

    [HttpPost]
    public IActionResult Criar(Professor professor)
    {
        _context.Professores.Add(professor);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Buscar), new { id = professor.IdUsuario }, professor);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Professor professor)
    {
        var professorExistente = _context.Professores.Find(id);
        if (professorExistente is null) { return NotFound("Professor não encontrado"); }

        professorExistente.NomeCompleto = professor.NomeCompleto;
        professorExistente.CPF = professor.CPF;
        professorExistente.DataNascimento = professor.DataNascimento;
        professorExistente.Telefone = professor.Telefone;
        professorExistente.Email = professor.Email;
        professorExistente.SenhaHash = professor.SenhaHash;
        professorExistente.Ativo = professor.Ativo;

        _context.SaveChanges();
        return Ok(professorExistente);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var professor = _context.Professores.Find(id);
        if (professor is null) { return NotFound("Professor não encontrado"); }
        _context.Professores.Remove(professor);
        _context.SaveChanges();
        return Ok("Professor deletado com sucesso");
    }
}
