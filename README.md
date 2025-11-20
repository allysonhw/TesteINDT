# Sistema de Empréstimos - Arquitetura Hexagonal

Sistema de análise e contratação de empréstimos desenvolvido em TypeScript seguindo os princípios da **Arquitetura Hexagonal** (Ports & Adapters).

## 📋 Índice

- [Sobre o Projeto](#sobre-o-projeto)
- [Arquitetura](#arquitetura)
- [Regras de Negócio](#regras-de-negócio)
- [Tecnologias](#tecnologias)
- [Instalação e Execução](#instalação-e-execução)
- [Endpoints da API](#endpoints-da-api)
- [Estrutura dos Microserviços](#estrutura-dos-microserviços)
- [Migrations](#migrations)
- [Testando a Aplicação](#testando-a-aplicação)

## 🎯 Sobre o Projeto

Sistema completo de empréstimos que analisa propostas de clientes e realiza contratações seguindo regras de negócio específicas. Implementado como **arquitetura de microserviços** com:

- **Separação de responsabilidades** através da Arquitetura Hexagonal
- **Microserviços independentes** (Propostas e Contratações)
- **Containerização completa** com Docker Compose
- **Frontend moderno** em Angular 17
- **Backend robusto** em .NET 8 com Entity Framework Core

## 🏗️ Arquitetura

Sistema de **microserviços** seguindo **Arquitetura Hexagonal (Ports & Adapters)**:

```
├── backend/
│   ├── PropostasService/         # API de Propostas (.NET 8)
│   │   ├── Domain/              # Entidades e regras de negócio
│   │   ├── Application/         # Serviços e DTOs
│   │   ├── Controllers/         # Endpoints REST
│   │   └── Data/               # DbContext e migrations
│   └── ContratacoesService/     # API de Contratações (.NET 8)
│       ├── Domain/
│       ├── Application/
│       ├── Controllers/
│       └── Data/
├── frontend/                    # Aplicação Angular
│   ├── src/
│   │   ├── app/
│   │   │   ├── components/
│   │   │   ├── services/
│   │   │   └── models/
│   │   └── assets/
│   └── Dockerfile
└── docker-compose.yml           # Orquestração de containers
```

### Camadas da Arquitetura Hexagonal

**1. Domain (Núcleo)**
- Entidades: `Proposta`, `Contratacao`
- Regras de negócio puras, independentes de frameworks

**2. Application (Casos de Uso)**
- Serviços: `PropostaService`, `ContratacaoService`
- DTOs para comunicação entre camadas

**3. Infrastructure (Adapters)**
- Controllers: Adaptadores REST API
- Repositories: Entity Framework Core
- External Services: HTTP clients para comunicação entre serviços

## 📜 Regras de Negócio

### Proposta de Empréstimo

#### ✅ Aprovação Automática
Uma proposta é aprovada se atender **TODAS** as condições:
- ✔️ Idade >= 18 anos E <= 60 anos
- ✔️ Renda >= R$ 3.000,00
- ✔️ Score >= 500
- ✔️ Valor solicitado <= 10x a renda mensal

#### 💰 Cálculo de Taxa de Juros

**Por Idade:**
- 18-25 anos: +2%
- 26-40 anos: +1%
- 41-60 anos: +3%

**Por Score:**
- < 500: Reprovado
- 500-700: +3%
- 701-900: +2%
- > 900: +1%

### Contratação

#### ⚠️ Validações
- ✔️ Proposta deve estar **APROVADA**
- ✔️ CPF não pode estar em **lista de restrição**
- ✔️ Valor e taxa devem corresponder à proposta

## 🛠️ Tecnologias

- **Backend**: .NET 8 Web API
- **Frontend**: Angular 17 (Standalone Components)
- **Banco de Dados**: SQL Server 2022 Express
- **ORM**: Entity Framework Core 8.0
- **Containerização**: Docker & Docker Compose
- **Servidor Web (Frontend)**: Nginx
- **Documentação API**: Swagger/OpenAPI

## 🐳 Instalação e Execução

### Pré-requisitos
- Docker Desktop instalado
- Git (para clonar o repositório)

### Subir todos os serviços

```powershell
# Na raiz do projeto
docker-compose up --build
```

Isso irá iniciar:
- **SQL Server Express** na porta 1433
- **API Propostas** na porta 5001
- **API Contratações** na porta 5002
- **Frontend Angular** na porta 4200

### Acessar a aplicação

```
Frontend: http://localhost:4200
API Propostas: http://localhost:5001/swagger
API Contratações: http://localhost:5002/swagger
```

### Parar os serviços

```powershell
docker-compose down
```

Para remover volumes (dados do banco):

```powershell
docker-compose down -v
```

## 🔌 Endpoints da API

### API Propostas (porta 5001)

```http
POST   /api/propostas              # Criar proposta
GET    /api/propostas/{id}         # Obter por ID
GET    /api/propostas              # Listar todas
GET    /api/propostas/cpf/{cpf}    # Buscar por CPF
```

### API Contratações (porta 5002)

```http
POST   /api/contratacoes           # Criar contratação
GET    /api/contratacoes/{id}      # Obter por ID
GET    /api/contratacoes           # Listar todas
GET    /api/contratacoes/cpf/{cpf} # Buscar por CPF
```

## 📦 Estrutura dos Microserviços

### PropostasService

**Responsabilidades:**
- Análise de propostas de empréstimo
- Cálculo automático de taxas
- Validação de regras de aprovação

**Banco de Dados:** `PropostasDB`

### ContratacoesService

**Responsabilidades:**
- Efetivação de contratos
- Validação de restrições (CPF)
- Comunicação com PropostasService

**Banco de Dados:** `ContratacoesDB`

## 🗄️ Migrations

As migrations do Entity Framework Core são executadas **automaticamente** na inicialização dos containers através do código:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PropostasDbContext>();
    db.Database.Migrate();
}
```

### Criar nova migration manualmente

```powershell
# Entrar no container
docker exec -it api-propostas bash

# Criar migration
dotnet ef migrations add NomeDaMigration

# Aplicar migration
dotnet ef database update
```

## 🧪 Testando a Aplicação

1. Acesse `http://localhost:4200`
2. Crie uma proposta com dados válidos
3. Se aprovada, vá para a aba "Contratações"
4. Selecione a proposta aprovada
5. Efetive a contratação

### Exemplo de Dados para Teste

**Proposta Aprovada:**
```json
{
  "cpf": "12312312312",
  "nome": "João Silva",
  "idade": 30,
  "renda": 5000,
  "score": 750,
  "valorSolicitado": 20000
}
```

**CPFs em Restrição (Mock):**
- `12345678901`
- `98765432100`

## 🔍 Logs e Troubleshooting

Ver logs de um serviço específico:

```powershell
docker-compose logs api-propostas
docker-compose logs api-contratacoes
docker-compose logs frontend
docker-compose logs sqlserver
```

Ver logs em tempo real:

```powershell
docker-compose logs -f
```

## 🎨 Princípios Aplicados

- **SOLID** (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion)
- **Clean Architecture** (Independência de frameworks, testabilidade, separação de camadas)
- **Domain-Driven Design** (Entidades ricas, agregados, repositórios)
- **Microservices** (Serviços independentes, comunicação via HTTP, bancos de dados separados)

## 📝 Licença

MIT

## 👤 Autor

Desenvolvido como parte do teste técnico INDT - Arquitetura Hexagonal.
