namespace Sistema.Models;

public class Aluno : Usuario
{
    public List<Matricula> Matriculas { get; set; } = new();

    public List<UsuarioMedalha> UsuarioMedalhas { get; set; } = new();

    public List<Progresso> Progressos { get; set; } = new();

    public List<Certificado> Certificados { get; set; } = new();

    public Aluno()
    {
    }

    public Aluno(
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

    public void MatricularCurso(Matricula matricula)
    {
        Matriculas.Add(matricula);
    }

    public void AdicionarMedalha(UsuarioMedalha medalha)
    {
        UsuarioMedalhas.Add(medalha);
    }

    public void AdicionarProgresso(Progresso progresso)
    {
        Progressos.Add(progresso);
    }

    public void AdicionarCertificado(Certificado certificado)
    {
        Certificados.Add(certificado);
    }

    public override void ExibirPerfil()
    {
        Console.WriteLine("Perfil: Aluno");
    }
}