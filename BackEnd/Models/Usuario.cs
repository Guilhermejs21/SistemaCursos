using System.ComponentModel.DataAnnotations;

namespace Sistema.Models;

public abstract class Usuario
{
    [Key]
    public int IdUsuario { get; set; }
    public string NomeCompleto { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public DateTime DataNascimento { get; set; }
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    // Nunca armazenaremos a senha em texto puro
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; }

    protected Usuario()
    {
        Ativo = true;
    }

    protected Usuario(
        int idUsuario,
        string nomeCompleto,
        string cpf,
        DateTime dataNascimento,
        string telefone,
        string email,
        string senhaHash)
    {
        IdUsuario = idUsuario;
        NomeCompleto = nomeCompleto;
        CPF = cpf;
        DataNascimento = dataNascimento;
        Telefone = telefone;
        Email = email;
        SenhaHash = senhaHash;
        Ativo = true;
    }

    public abstract void ExibirPerfil();
}