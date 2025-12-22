<h1 align="center">SCAI - Sistema de Controle de Acesso Imperial</h1>

<div align="center">
  <img src="images/scai_logo.png" alt="SCAI Logo" width="180"/>
</div>

<div align="center">

[![NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Build Status](https://img.shields.io/badge/build-passing-brightgreen?style=flat&logo=github-actions)](https://github.com/seuusuario/SCAI.API/actions)
[![License](https://img.shields.io/badge/license-MIT-blue?style=flat)](LICENSE.txt)
[![Star Wars](https://img.shields.io/badge/Empire-Galactic-black?style=flat&logo=star-wars)](https://starwars.fandom.com/wiki/Galactic_Empire)

</div>

<p align="center">
  <a href="#-sobre">Sobre</a> •
  <a href="#-arquitetura">Arquitetura</a> •
  <a href="#-stack">Stack</a> •
  <a href="#-segurança-e-rbac">Segurança</a> •
  <a href="#-instalação">Instalação</a> •
  <a href="#-testes">Testes</a>
</p>

<div align="center">
  <strong>[<a href="README.md">English Version</a>]</strong>
</div>

---

## 🌌 Sobre

**SCAI (Sistema de Controle de Acesso Imperial)** é um sistema de gerenciamento de estoque e cadeia de suprimentos desenvolvido para o Império Galáctico da saga Star Wars.

Construído como uma **API REST** utilizando **.NET 10**, o projeto simula um cenário corporativo real, demonstrando:
* Padrões de arquitetura moderna e desacoplada.
* Autenticação e autorização.
* Controle de acesso baseado em cargos (RBAC).
* CRUD com Entity Framework Core.

> *O objetivo principal é servir como uma referência para aplicações .NET de nível corporativo, utilizando a temática de Star Wars para tornar o aprendizado mais interessante e envolvente.*

---

## 🏗️ Arquitetura

A solução adota uma **Arquitetura Três Camadas**, promovendo a separação de responsabilidades e facilitando testes unitários.



### Detalhamento das Camadas

| Camada | Componente | Responsabilidade |
| :--- | :--- | :--- |
| **1. Apresentação** | `Controllers` | Ponto de entrada da API. Gerencia requisições HTTP, validação de DTOs e formatação de respostas padrão (Envelope Pattern). |
| **2. Negócio (Domain)** | `Services` | O coração da aplicação. Contém a lógica de negócios, validações de regras do Império e verificação de políticas de acesso. |
| **3. Acesso a Dados** | `Repositories` | Abstração do banco de dados utilizando **EF Core**. Gerencia transações e consultas SQL otimizadas. |

---

## 🛠️ Stack

Este projeto está na vanguarda do ecossistema .NET, utilizando recursos modernos e de alta performance.

| Categoria | Tecnologia | Detalhes |
| :--- | :--- | :--- |
| **Core** | **.NET 10** | C# 14, ASP.NET Core Web API |
| **Dados** | **SQL Server** | Entity Framework Core 10.0 |
| **Auth** | **JWT Bearer** | Tokens assinados digitalmente, BCrypt.Net-Next |
| **Docs** | **OpenAPI** | Swagger UI / Swashbuckle |
| **Testes** | **xUnit** | Moq para mocking de dependências |

---

## 🔐 Segurança e RBAC

A segurança é o pilar do Império. O sistema utiliza **Role-Based Access Control (RBAC)**.



### Hierarquia de permissões

| Função (Role) | Nível | Permissões |
| :--- | :---: | :--- |
| 🔴 **Sith** | 1 (Admin) | **Controle total**. Pode realizar todas as operações CRUD do sistema e acessar a leitura de itens com permissão 1. |
| 🟡 **Commander** | 2 (Gerente)| **Escrita**. Pode atualizar itens e acessar a leitura de itens com permissão 2. |
| ⚪ **Trooper** | 3 (Leitura) | **Apenas leitura**. Pode visualizar a lista de itens com permissão 3. |

### Fluxo de autenticação

1.  **Registro**: Endpoint `/api/auth/register`. Senhas são hashadas com **BCrypt**.
2.  **Login**: Endpoint `/api/auth/login`. Retorna um **JWT (JSON Web Token)**.
3.  **Acesso**: O client deve enviar o header: `Authorization: Bearer <token_jwt>`.

---

## ⚙️ Instalação e configuração

### Pré-requisitos

* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* SQL Server (Local, Docker ou Azure)
* IDE: Visual Studio 2022+, VS Code ou Rider

### 1. Clonar o repositório

```bash
git clone https://github.com/frodrigss/SCAI.API.git
cd SCAI.API
```

### 2. Configuração de ambiente
Para sua segurança, evite commitar credenciais sensíveis. Crie um arquivo `.env` no diretório `SCAI` ou use o User Secrets do .NET.

Exemplo de `.env`:

```env
# Database
ConnectionStrings__DbConnection="Server=localhost;Database=SCAI_DB;Trusted_Connection=True;MultipleActiveResultSets=true"

# Security
Jwt__Key="SUA_CHAVE_JWT"
Jwt__Issuer="SCAI.API"
Jwt__Audience="SCAI.Client"
```

### 3. Executar migrations e popular o banco
Execute as migrations para criar a estrutura do banco e popular o banco com os dados de `SeedData.cs` localizado no diretório `SCAI/Infrastructure/Data`.

```bash
cd SCAI
dotnet ef database update
```

### 4. Executar

```bash
dotnet run
# Acesse via http://localhost:5000 ou verifique a porta no console
```

---

## 🧪 Testes

A integridade do sistema é garantida por uma suíte de testes automatizados que estão localizados no projeto `SCAI.Tests`.

Execute os testes com o comando:

```bash
cd SCAI.Tests
dotnet test
```

### Cobertura

| Camada | Cobertura | Status |
| :--- | :---: | :--- |
| Controllers | 78% | ![badge](https://img.shields.io/badge/Controllers-78%25-yellow) |
| Services | 86% | ![badge](https://img.shields.io/badge/Services-86%25-yellow) |
| Repositories | N/A | ![badge](https://img.shields.io/badge/Repositories-N%2FA-lightgrey) |
| Models | 89% | ![badge](https://img.shields.io/badge/Models-89%25-yellow) |

---

## 📂 Estrutura de pastas

```
SCAI.API/
├── Images/                         # Imagens para a documentação
├── SCAI/                           # Projeto principal da API Web
│   ├── Controllers/                # Endpoints da API
│   ├── Infrastructure/             # Preocupações transversais (Dados, Auth)
│   │   ├── Data/                   # DbContext e Migrations
│   │   └── Interfaces/             # Interfaces de infraestrutura
│   ├── Migrations/                 # EF Core migrations
│   ├── Models/                     # Entidades de domínio e DTOs
│   ├── Properties/                 # Propriedades do projeto
│   ├── Repositories/               # Lógica de acesso a dados
│   ├── Services/                   # Lógica de negócio
│   ├── appsettings.json            # Configuração
│   └── Program.cs                  # Ponto de entrada & configuração DI
├── SCAI.Tests/                     # Projeto de testes unitários
├── .gitignore                      # Arquivo gitignore
├── LICENSE.txt                     # Licença MIT
├── README.md                       # Documentação em inglês
├── README.pt-BR.md                 # Documentação em português
└── SCAI.API.sln                    # Solução do Visual Studio
```

## 📄 Licença

Este projeto está licenciado sob a Licença MIT. Veja o arquivo [LICENSE](LICENSE.txt) para mais detalhes.
