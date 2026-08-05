using Microsoft.AspNetCore.Mvc;
using Sistema.DTOs;
using Sistema.Data;
using Sistema.Models;
using Sistema.Enums;

namespace Sistema.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AvaliacaoController : ControllerBase
{
    private readonly AppDbContext _context;
    public AvaliacaoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        var avaliacoes = _context.Avaliacoes
            .Select(a => new AvaliacaoDto
            {
                IdAvaliacao = a.IdAvaliacao,
                Pergunta = a.Pergunta,
                OpcaoA = a.OpcaoA,
                OpcaoB = a.OpcaoB,
                OpcaoC = a.OpcaoC,
                OpcaoD = a.OpcaoD,
                RespostaCorreta = a.RespostaCorreta,
                AulaId = a.AulaId,
                TituloAula = a.Aula.TituloAula
            })
            .ToList();

        return Ok(avaliacoes);
    }

    [HttpGet("{id}")]
    public IActionResult Buscar(int id)
    {
        var avaliacao = _context.Avaliacoes
            .Where(a => a.IdAvaliacao == id)
            .Select(a => new AvaliacaoDto
            {
                IdAvaliacao = a.IdAvaliacao,
                Pergunta = a.Pergunta,
                OpcaoA = a.OpcaoA,
                OpcaoB = a.OpcaoB,
                OpcaoC = a.OpcaoC,
                OpcaoD = a.OpcaoD,
                RespostaCorreta = a.RespostaCorreta,
                AulaId = a.AulaId,
                TituloAula = a.Aula.TituloAula
            })
            .FirstOrDefault();

        if (avaliacao is null)
        {
            return NotFound("Avaliação não encontrada.");
        }

        return Ok(avaliacao);
    }

    [HttpPost]
    public IActionResult Criar(CriarAvaliacaoDto dados)
    {
        var aula = _context.Aulas.Find(dados.AulaId);

        if (aula is null)
        {
            return BadRequest("Aula não encontrada.");
        }

        if (string.IsNullOrWhiteSpace(dados.Pergunta))
        {
            return BadRequest("A pergunta deve ser informada.");
        }

        if (string.IsNullOrWhiteSpace(dados.OpcaoA) ||
            string.IsNullOrWhiteSpace(dados.OpcaoB) ||
            string.IsNullOrWhiteSpace(dados.OpcaoC) ||
            string.IsNullOrWhiteSpace(dados.OpcaoD))
        {
            return BadRequest("Todas as opções devem ser preenchidas.");
        }

        var respostaCorreta = dados.RespostaCorreta
            .Trim()
            .ToUpper();

        if (respostaCorreta != "A" &&
            respostaCorreta != "B" &&
            respostaCorreta != "C" &&
            respostaCorreta != "D")
        {
            return BadRequest("A resposta correta deve ser A, B, C ou D.");
        }

        var perguntaExiste = _context.Avaliacoes.Any(a =>
            a.AulaId == dados.AulaId &&
            a.Pergunta == dados.Pergunta);

        if (perguntaExiste)
        {
            return BadRequest("Essa pergunta já existe nesta aula.");
        }

        var avaliacao = new Avaliacao
        {
            Pergunta = dados.Pergunta,
            OpcaoA = dados.OpcaoA,
            OpcaoB = dados.OpcaoB,
            OpcaoC = dados.OpcaoC,
            OpcaoD = dados.OpcaoD,
            RespostaCorreta = respostaCorreta,
            AulaId = dados.AulaId
        };

        _context.Avaliacoes.Add(avaliacao);
        _context.SaveChanges();

        var resposta = new AvaliacaoDto
        {
            IdAvaliacao = avaliacao.IdAvaliacao,
            Pergunta = avaliacao.Pergunta,
            OpcaoA = avaliacao.OpcaoA,
            OpcaoB = avaliacao.OpcaoB,
            OpcaoC = avaliacao.OpcaoC,
            OpcaoD = avaliacao.OpcaoD,
            RespostaCorreta = avaliacao.RespostaCorreta,
            AulaId = avaliacao.AulaId,
            TituloAula = aula.TituloAula
        };

        return CreatedAtAction(
            nameof(Buscar),
            new { id = resposta.IdAvaliacao },
            resposta
        );
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, AtualizarAvaliacaoDto dados)
    {
        var avaliacao = _context.Avaliacoes.Find(id);

        if (avaliacao is null)
        {
            return NotFound("Avaliação não encontrada.");
        }

        var aula = _context.Aulas.Find(dados.AulaId);

        if (aula is null)
        {
            return BadRequest("Aula não encontrada.");
        }

        if (string.IsNullOrWhiteSpace(dados.Pergunta))
        {
            return BadRequest("A pergunta deve ser informada.");
        }

        if (string.IsNullOrWhiteSpace(dados.OpcaoA) ||
            string.IsNullOrWhiteSpace(dados.OpcaoB) ||
            string.IsNullOrWhiteSpace(dados.OpcaoC) ||
            string.IsNullOrWhiteSpace(dados.OpcaoD))
        {
            return BadRequest("Todas as opções devem ser preenchidas.");
        }

        var respostaCorreta = dados.RespostaCorreta
            .Trim()
            .ToUpper();

        if (respostaCorreta != "A" &&
            respostaCorreta != "B" &&
            respostaCorreta != "C" &&
            respostaCorreta != "D")
        {
            return BadRequest("A resposta correta deve ser A, B, C ou D.");
        }

        var perguntaExiste = _context.Avaliacoes.Any(a =>
            a.IdAvaliacao != id &&
            a.AulaId == dados.AulaId &&
            a.Pergunta == dados.Pergunta);

        if (perguntaExiste)
        {
            return BadRequest("Essa pergunta já existe nesta aula.");
        }

        avaliacao.Pergunta = dados.Pergunta;
        avaliacao.OpcaoA = dados.OpcaoA;
        avaliacao.OpcaoB = dados.OpcaoB;
        avaliacao.OpcaoC = dados.OpcaoC;
        avaliacao.OpcaoD = dados.OpcaoD;
        avaliacao.RespostaCorreta = respostaCorreta;
        avaliacao.AulaId = dados.AulaId;

        _context.SaveChanges();

        var resposta = new AvaliacaoDto
        {
            IdAvaliacao = avaliacao.IdAvaliacao,
            Pergunta = avaliacao.Pergunta,
            OpcaoA = avaliacao.OpcaoA,
            OpcaoB = avaliacao.OpcaoB,
            OpcaoC = avaliacao.OpcaoC,
            OpcaoD = avaliacao.OpcaoD,
            RespostaCorreta = avaliacao.RespostaCorreta,
            AulaId = avaliacao.AulaId,
            TituloAula = aula.TituloAula
        };

        return Ok(resposta);
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var avaliacao = _context.Avaliacoes.Find(id);

        if (avaliacao is null)
        {
            return NotFound("Avaliação não encontrada.");
        }

        _context.Avaliacoes.Remove(avaliacao);
        _context.SaveChanges();

        return Ok("Avaliação deletada com sucesso.");
    }
}