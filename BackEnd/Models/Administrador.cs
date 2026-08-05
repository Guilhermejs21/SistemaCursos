namespace Sistema.Models;

public class Administrador : Usuario
{
    public Administrador()
    {
    }

    public Administrador(
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

    public void AtivarUsuario(Usuario usuario)
    {
        usuario.Ativo = true;
    }

    public void DesativarUsuario(Usuario usuario)
    {
        usuario.Ativo = false;
    }

    public override void ExibirPerfil()
    {
        Console.WriteLine("Perfil: Administrador");
    }
}