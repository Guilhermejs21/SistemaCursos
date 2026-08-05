using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdministradoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdministradoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var administradores = _context.Administradores.ToList();

        return Ok(administradores);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var administrador = _context.Administradores.Find(id);
        if (administrador is null) { return NotFound("Administrador não encontrado"); }

        return Ok(administrador);
    }

    [HttpPost]
    public IActionResult Criar(Administrador administrador)
    {
        _context.Administradores.Add(administrador);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Buscar), new { id = administrador.IdUsuario }, administrador);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Administrador administrador)
    {
        var administradorExistente = _context.Administradores.Find(id);
        if (administradorExistente is null) { return NotFound("Administrador não encontrado"); }

        administradorExistente.NomeCompleto = administrador.NomeCompleto;
        administradorExistente.CPF = administrador.CPF;
        administradorExistente.DataNascimento = administrador.DataNascimento;
        administradorExistente.Telefone = administrador.Telefone;
        administradorExistente.Email = administrador.Email;
        administradorExistente.SenhaHash = administrador.SenhaHash;
        administradorExistente.Ativo = administrador.Ativo;

        _context.SaveChanges();
        return Ok(administradorExistente);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var administrador = _context.Administradores.Find(id);
        if (administrador is null) { return NotFound("Administrador não encontrado"); }
        _context.Administradores.Remove(administrador);
        _context.SaveChanges();
        return Ok("Administrador deletado com sucesso");
    }
}
