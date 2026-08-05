using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunosController : ControllerBase
{
    private readonly AppDbContext _context;

    public AlunosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var alunos = _context.Alunos.ToList();

        return Ok(alunos);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var aluno = _context.Alunos.Find(id);
        if (aluno is null) { return NotFound("Aluno não encontrado"); }

        return Ok(aluno);
    }

    [HttpPost]
    public IActionResult Criar(Aluno aluno)
    {
        _context.Alunos.Add(aluno);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Buscar), new { id = aluno.IdUsuario }, aluno);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Aluno aluno)
    {
        var alunoExistente = _context.Alunos.Find(id);
        if (alunoExistente is null) { return NotFound("Aluno não encontrado"); }

        alunoExistente.NomeCompleto = aluno.NomeCompleto;
        alunoExistente.CPF = aluno.CPF;
        alunoExistente.DataNascimento = aluno.DataNascimento;
        alunoExistente.Telefone = aluno.Telefone;
        alunoExistente.Email = aluno.Email;
        alunoExistente.SenhaHash = aluno.SenhaHash;
        alunoExistente.Ativo = aluno.Ativo;

        _context.SaveChanges();
        return Ok(alunoExistente);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var aluno = _context.Alunos.Find(id);
        if (aluno is null) { return NotFound("Aluno não encontrado"); }
        _context.Alunos.Remove(aluno);
        _context.SaveChanges();
        return Ok("Aluno deletado com sucesso");
    }
}
