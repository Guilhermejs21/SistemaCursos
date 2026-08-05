using Microsoft.AspNetCore.Mvc;
using Sistema.DTOs;
using Sistema.Data;
using Sistema.Models;
using Sistema.Enums;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CertificadoController : ControllerBase
{
    private readonly AppDbContext _context;
    public CertificadoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var certificados = _context.Certificados
            .Select(c => new CertificadoDto
            {
                IdCertificado = c.IdCertificado,
                AlunoId = c.AlunoId,
                NomeAluno = c.Aluno.NomeCompleto,
                CursoId = c.CursoId,
                NomeCurso = c.Curso.NomeCurso,
                DataEmissao = c.DataEmissao,
                CargaHoraria = c.CargaHoraria,
                Codigo = c.Codigo

            })
            .ToList();

        return Ok(certificados);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var certificado = _context.Certificados
            .Where(c => c.IdCertificado == id)
            .Select(c => new CertificadoDto
            {
                IdCertificado = c.IdCertificado,
                AlunoId = c.AlunoId,
                NomeAluno = c.Aluno.NomeCompleto,
                CursoId = c.CursoId,
                NomeCurso = c.Curso.NomeCurso,
                DataEmissao = c.DataEmissao,
                CargaHoraria = c.CargaHoraria,
                Codigo = c.Codigo
            })
            .FirstOrDefault();

        if (certificado == null)
            {
            return NotFound("Certificado não encontrado.");
            }
        return Ok(certificado);
    }

    [HttpPost]
    public IActionResult Criar(CriarCertificadoDto dados)
    {
        var aluno = _context.Alunos.Find(dados.AlunoId);

        if (aluno is null)
        {
            return BadRequest("Aluno não encontrado.");
        }

        var curso = _context.Cursos.Find(dados.CursoId);

        if (curso is null)
        {
            return BadRequest("Curso não encontrado.");
        }

        var matriculaExiste = _context.Matriculas.Any(m =>
            m.AlunoId == dados.AlunoId &&
            m.CursoId == dados.CursoId &&
            m.Ativa);
        if (!matriculaExiste) { return BadRequest("Matrícula não encontrada"); }

        var certificadoExiste = _context.Certificados.Any(m =>
            m.AlunoId == dados.AlunoId &&
            m.CursoId == dados.CursoId);
        if (certificadoExiste) { return BadRequest("Certificado ja existe"); }

        var cursoTemAulas = _context.Aulas.Any(a =>
            a.CursoId == dados.CursoId );

        if (!cursoTemAulas){ return BadRequest("O curso não possui aulas cadastradas."); }

        var aulas = _context.Aulas
           .Where(a => a.CursoId == dados.CursoId);

        var aulasForamFeitas = aulas.All(a =>
            _context.Progressos.Any(p =>
                p.AlunoId == dados.AlunoId &&
                p.AulaId == a.IdAula &&
                p.StatusAula == StatusAula.Concluida));

        if (!aulasForamFeitas)
        {
            return BadRequest("O aluno não concluiu todas as aulas do curso.");
        }

        var certificado = new Certificado
        {
            DataEmissao = DateTime.Now,
            CargaHoraria = curso.CargaHoraria,
            Codigo = Guid.NewGuid().ToString(),
            AlunoId = dados.AlunoId,
            CursoId = dados.CursoId
        };


        _context.Certificados.Add(certificado);
        _context.SaveChanges();

        var resposta = new CertificadoDto
        {
            IdCertificado = certificado.IdCertificado,
            AlunoId = certificado.AlunoId,
            NomeAluno = aluno.NomeCompleto,
            CursoId = certificado.CursoId,
            NomeCurso = curso.NomeCurso,
            DataEmissao = certificado.DataEmissao,
            CargaHoraria = certificado.CargaHoraria,
            Codigo = certificado.Codigo
        };

        return CreatedAtAction(
            nameof(Buscar),
            new { id = resposta.IdCertificado },
            resposta
        );
    }

    [HttpDelete("{id}")]
            public IActionResult Deletar(int id)
            {
                var certificado = _context.Certificados.Find(id);

                if (certificado is null)
                {
                    return NotFound("Cer tificado não encontrado.");
                }

                _context.Certificados.Remove(certificado);
                _context.SaveChanges();

                return Ok("Certificado deletado com sucesso.");
            }
    
}