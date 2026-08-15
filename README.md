# Sistema de Cursos

Backend de uma plataforma de cursos desenvolvido como API REST em ASP.NET Core, com modelagem de usuários, cursos, aulas, matrículas, progresso, avaliações, certificados e medalhas.

## Funcionalidades implementadas

- Administração de alunos, professores e administradores
- Cadastro de cursos e aulas
- Matrículas
- Acompanhamento de progresso
- Avaliações
- Emissão de certificados
- Materiais de apoio
- Sistema de medalhas

## Tecnologias

- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

## Organização

- `Controllers`: endpoints da API
- `DTOs`: objetos de entrada e saída
- `Models`: entidades do domínio
- `Data`: contexto do Entity Framework Core
- `Migrations`: versionamento do banco
- `Enums`: valores controlados do domínio

## Como executar

1. Tenha o SQL Server disponível.
2. Confira `DefaultConnection` em `BackEnd/appsettings.json`.
3. Execute:

```bash
dotnet restore BackEnd/SistemaCursos.csproj
dotnet ef database update --project BackEnd
dotnet run --project BackEnd
```

A documentação Swagger estará no endereço informado no terminal quando a aplicação estiver em ambiente de desenvolvimento.

## Objetivo

Projeto de portfólio e aprendizado voltado à construção de uma API com domínio mais completo, DTOs, relacionamentos e persistência.
