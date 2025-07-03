# API de Gerenciamento de Tarefas (Task Management API)

Este repositório contém o código-fonte de uma API RESTful completa para gerenciamento de tarefas, desenvolvida como um desafio técnico para demonstrar conhecimento em desenvolvimento de software com .NET, aplicando as melhores práticas de arquitetura, qualidade de código e DevOps.

---

## ✨ Funcionalidades Principais

* **CRUD Completo:** Operações para Criar, Ler, Atualizar e Remover tarefas.
* **Filtros Avançados:** Capacidade de listar tarefas filtrando por `status` (Pendente, EmAndamento, Concluido) e `data de vencimento`.
* **Exclusão Lógica (Soft Delete):** Tarefas não são permanentemente removidas do banco de dados, garantindo a integridade dos dados e permitindo auditoria.
* **Documentação Interativa:** API 100% documentada com Swagger (OpenAPI) para fácil visualização e teste dos endpoints.

---

## 🏗️ Arquitetura e Padrões de Projeto

O projeto foi desenvolvido utilizando **Arquitetura Limpa (Clean Architecture)** para garantir uma clara separação de responsabilidades, alta testabilidade e fácil manutenção.

* **Camadas:**
    * **Domain:** O núcleo do negócio, contendo as entidades e as regras de negócio puras.
    * **Application:** Orquestra os casos de uso e a lógica da aplicação.
    * **Infrastructure:** Implementa os detalhes técnicos, como o acesso a dados com Entity Framework Core.
    * **Api:** A camada de apresentação, que expõe os endpoints RESTful.
* **Padrões de Projeto:**
    * **Repository & Unit of Work:** Abstrai o acesso a dados e gerencia transações de forma atômica.
    * **Injeção de Dependência (DI):** Usada extensivamente para desacoplar os componentes.
    * **DTO (Data Transfer Object):** Para garantir que a API exponha apenas os dados necessários.
* **Princípios:** O design segue os princípios **SOLID** para criar um código robusto e flexível.

---

## 🛠️ Tecnologias Utilizadas

* **.NET 8:** Framework principal para a construção da API.
* **ASP.NET Core:** Para a criação dos endpoints RESTful.
* **Entity Framework Core 8:** ORM para a persistência de dados.
* **SQL Server:** Banco de dados relacional.
* **xUnit & Moq:** Para a suíte de testes unitários.
* **Docker:** Para a containerização da aplicação.
* **GitHub Actions:** Para o pipeline de Integração Contínua e Entrega Contínua (CI/CD).

---

## 🚀 Como Executar o Projeto

Existem duas maneiras de executar a aplicação: localmente através do .NET SDK ou via Docker.

### 1. Executando Localmente

**Pré-requisitos:**
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* SQL Server (Express, Developer, etc.)

**Passos:**
1.  Clone o repositório:
    ```bash
    git clone [https://github.com/suhettgabriel/Desafio-TaskManagement-Api.git](https://github.com/suhettgabriel/Desafio-TaskManagement-Api.git)
    cd Desafio-TaskManagement-Api
    ```
2.  Configure a string de conexão no arquivo `TaskManagement.Api/appsettings.json`, apontando para a sua instância do SQL Server.
3.  Aplique as migrações do banco de dados. No terminal, na raiz do projeto, execute:
    ```bash
    dotnet ef database update --project TaskManagement.Infrastructure
    ```
4.  Execute a aplicação:
    ```bash
    dotnet run --project TaskManagement.Api
    ```
5.  A API estará disponível. Acesse o Swagger em `https://localhost:<porta>/index.html`.

### 2. Executando com Docker (Recomendado)

**Pré-requisitos:**
* [Docker Desktop](https://www.docker.com/products/docker-desktop/)

**Passos:**
1.  Clone o repositório.
2.  Certifique-se de que sua instância local do SQL Server está configurada para aceitar conexões de rede (via TCP/IP) e que o login por usuário e senha está habilitado (Autenticação Mista).
3.  Execute o comando `docker run` substituindo os placeholders `<sua_porta_sql>` e `<sua_senha_sa>`:
    ```bash
    # Primeiro, construa a imagem
    docker build -t task-management-api .

    # Depois, execute o contêiner
    docker run --rm -p 5000:8080 -e "ConnectionStrings__DefaultConnection=Server=host.docker.internal,<sua_porta_sql>;Database=TaskManagementDB;User Id=sa;Password=<sua_senha_sa>;TrustServerCertificate=True" --name meu-task-api task-management-api
    ```
4.  A API estará disponível. Acesse o Swagger em `http://localhost:5000`.

*Alternativamente, uma imagem pré-construída está disponível no Docker Hub:*
```bash
docker run --rm -p 5000:8080 -e "..." gabrielsuhett/task-management-api:latest
