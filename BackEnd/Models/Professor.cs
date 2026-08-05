
namespace Sistema.Models;

public class Professor : Usuario
{
    public List<Curso> Cursos { get; set; } = new();

    public Professor()
    {
    }

    public Professor(
        int idUsuario,
        string nomeCompleto,
        string cpf,
        DateTime dataNascimento,
        string telefone,
        string email,
        string senhaHash)
        : base(
            idUsuario,
            nomeCompleto,
            cpf,
            dataNascimento,
            telefone,
            email,
            senhaHash)
    {
    } 
    public override void ExibirPerfil()
    {
        Console.WriteLine("Perfil: Professor");
    }
}