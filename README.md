# Sistema de Reserva de Salas

Sistema de gerenciamento de reservas de salas com autenticação JWT, tratamento de exceções centralizado e arquitetura em camadas.

## 🚀 Tecnologias

- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Docker
- Swagger/OpenAPI
- Insomnia (testes manuais)
## 📋 Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [PostgreSQL](https://www.postgresql.org/download/) (opcional, se não quiser usar Docker)

## 🔧 Configuração do Ambiente

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/sistema-reserva-salas.git
cd sistema-reserva-salas
```

2. Inicie o PostgreSQL com Docker:
```bash
docker-compose up -d
```

## 🛠️ Instalação

1. Restaure as dependências:
```bash
dotnet restore
```

2. Aplique as migrações do banco de dados:
```bash
cd MonolitoBackend.Api
dotnet ef database update
```

3. Compile o projeto:
```bash
dotnet build
```

## 🚀 Executando a Aplicação

1. Inicie a API:
```bash
cd MonolitoBackend.Api
dotnet run
```

2. Acesse a documentação Swagger:
```
http://localhost:5027/swagger
```

## 📁 Estrutura do Projeto

```
sistema-reserva-salas/
├── MonolitoBackend.Api/           # Camada de API
├── MonolitoBackend.Core/          # Camada de domínio
└── MonolitoBackend.Infrastructure/# Camada de infraestrutura
```

### Camadas

- **API**: Controllers, configuração da aplicação
- **Core**: Entidades, interfaces, DTOs
- **Infrastructure**: Implementações, repositórios, serviços

## 🔐 Autenticação

A API usa autenticação JWT. Para obter um token:

1. Registre um usuário:
```http
POST /api/auth/register
{
    "name": "Usuário Teste",
    "email": "teste@email.com",
    "password": "Senha123!"
}
```

2. Faça login:
```http
POST /api/auth/login
{
    "email": "teste@email.com",
    "password": "Senha123!"
}
```

3. Use o token retornado no header:
```
Authorization: Bearer {seu_token}
```
## 🧪 Testando a API

Endpoints de teste disponíveis:

- `GET /api/test/public` - Endpoint público
- `GET /api/test/private` - Endpoint que requer autenticação
- `GET /api/test/test-exception` - Teste de exceção não autorizada
- `GET /api/test/test-validation` - Teste de exceção de validação
- `GET /api/test/test-not-found` - Teste de recurso não encontrado
- `GET /api/test/test-invalid-operation` - Teste de operação inválida
- `GET /api/test/test-not-supported` - Teste de operação não suportada
- `GET /api/test/test-error` - Teste de erro interno

## 🔍 Tratamento de Exceções

O sistema possui um middleware centralizado para tratamento de exceções que retorna respostas padronizadas:

```json
{
    "status": 400,
    "error": "Mensagem de erro",
    "timestamp": "2024-03-14T12:00:00Z",
    "errorId": "guid-único",
    "path": "/api/test/test-validation",
    "method": "GET"
}
```

## 📝 Logs

Os logs são gerados automaticamente para todas as exceções, incluindo:
- ID do erro
- Caminho da requisição
- Método HTTP
- Mensagem de erro
- Stack trace

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

