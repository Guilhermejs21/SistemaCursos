using Microsoft.AspNetCore.Mvc;
using Sistema.Data;
using Sistema.DTOs;
using Sistema.Enums;
using Sistema.Models;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProgressoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var progressos = _context.Progressos
            .Select(p => new ProgressoDto
            {
                IdProgresso = p.IdProgresso,
                AlunoId = p.AlunoId,
                NomeAluno = p.Aluno.NomeCompleto,
                AulaId = p.AulaId,
                TituloAula = p.Aula.TituloAula,
                NotaObtida = p.NotaObtida,
                DataTentativa = p.DataTentativa,
                StatusAula = p.StatusAula
            })
            .ToList();

        return Ok(progressos);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var progresso = _context.Progressos
            .Where(p => p.IdProgresso == id)
            .Select(p => new ProgressoDto
            {
                IdProgresso = p.IdProgresso,
                AlunoId = p.AlunoId,
                NomeAluno = p.Aluno.NomeCompleto,
                AulaId = p.AulaId,
                TituloAula = p.Aula.TituloAula,
                NotaObtida = p.NotaObtida,
                DataTentativa = p.DataTentativa,
                StatusAula = p.StatusAula
            })
            .FirstOrDefault();

        if (progresso is null)
        {
            return NotFound("Progresso não encontrado.");
        }

        return Ok(progresso);
    }

    [HttpPost]
    public IActionResult Criar(CriarProgressoDto dados)
    {
        var aluno = _context.Alunos.Find(dados.AlunoId);

        if (aluno is null)
        {
            return BadRequest("Aluno não encontrado.");
        }

        var aula = _context.Aulas.Find(dados.AulaId);

        if (aula is null)
        {
            return BadRequest("Aula não encontrada.");
        }

        var estaMatriculado = _context.Matriculas.Any(m =>
            m.AlunoId == dados.AlunoId &&
            m.CursoId == aula.CursoId &&
            m.Ativa);

        if (!estaMatriculado)
        {
            return BadRequest(
                "O aluno não está matriculado no curso desta aula."
            );
        }

        var existeProgresso = _context.Progressos.Any(p =>
            p.AlunoId == dados.AlunoId &&
            p.AulaId == dados.AulaId);

        if (existeProgresso)
        {
            return BadRequest(
                "Progresso já registrado para este aluno e aula."
            );
        }

        if (!Enum.IsDefined(typeof(StatusAula), dados.StatusAula))
        {
            return BadRequest("Status da aula inválido.");
        }

        if (dados.NotaObtida.HasValue &&
            (dados.NotaObtida < 0 || dados.NotaObtida > 10))
        {
            return BadRequest("A nota deve estar entre 0 e 10.");
        }

        if (dados.StatusAula == StatusAula.NaoIniciada &&
            dados.NotaObtida.HasValue)
        {
            return BadRequest(
                "Uma aula não iniciada não pode possuir nota."
            );
        }

        if (dados.StatusAula == StatusAula.Concluida &&
            !dados.NotaObtida.HasValue)
        {
            return BadRequest(
                "Uma aula concluída deve possuir nota."
            );
        }

        var progresso = new Progresso
        {
            AlunoId = dados.AlunoId,
            AulaId = dados.AulaId,
            NotaObtida = dados.NotaObtida,
            DataTentativa = DateTime.Now,
            StatusAula = dados.StatusAula
        };

        _context.Progressos.Add(progresso);
        _context.SaveChanges();

        var resposta = new ProgressoDto
        {
            IdProgresso = progresso.IdProgresso,
            AlunoId = progresso.AlunoId,
            NomeAluno = aluno.NomeCompleto,
            AulaId = progresso.AulaId,
            TituloAula = aula.TituloAula,
            NotaObtida = progresso.NotaObtida,
            DataTentativa = progresso.DataTentativa,
            StatusAula = progresso.StatusAula
        };

        return CreatedAtAction(
            nameof(Buscar),
            new { id = resposta.IdProgresso },
            resposta
        );
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(
        int id,
        AtualizarProgressoDto dados)
    {
        var progresso = _context.Progressos.Find(id);

        if (progresso is null)
        {
            return NotFound("Progresso não encontrado.");
        }

        if (!Enum.IsDefined(typeof(StatusAula), dados.StatusAula))
        {
            return BadRequest("Status da aula inválido.");
        }

        if (dados.NotaObtida.HasValue &&
            (dados.NotaObtida < 0 || dados.NotaObtida > 10))
        {
            return BadRequest("A nota deve estar entre 0 e 10.");
        }

        if (dados.StatusAula == StatusAula.NaoIniciada &&
            dados.NotaObtida.HasValue)
        {
            return BadRequest(
                "Uma aula não iniciada não pode possuir nota."
            );
        }

        if (dados.StatusAula == StatusAula.Concluida &&
            !dados.NotaObtida.HasValue)
        {
            return BadRequest(
                "Uma aula concluída deve possuir nota."
            );
        }

        progresso.NotaObtida = dados.NotaObtida;
        progresso.StatusAula = dados.StatusAula;
        progresso.DataTentativa = DateTime.Now;

        _context.SaveChanges();

        var resposta = _context.Progressos
            .Where(p => p.IdProgresso == progresso.IdProgresso)
            .Select(p => new ProgressoDto
            {
                IdProgresso = p.IdProgresso,
                AlunoId = p.AlunoId,
                NomeAluno = p.Aluno.NomeCompleto,
                AulaId = p.AulaId,
                TituloAula = p.Aula.TituloAula,
                NotaObtida = p.NotaObtida,
                DataTentativa = p.DataTentativa,
                StatusAula = p.StatusAula
            })
            .FirstOrDefault();

        return Ok(resposta);
    }
}