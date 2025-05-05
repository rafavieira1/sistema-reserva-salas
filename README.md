# Sistema de Reservas de Salas

API RESTful desenvolvida em .NET 8 com ASP.NET Core para gerenciamento de salas e reservas, utilizando Entity Framework Core com persistência em PostgreSQL. A aplicação é documentada com Swagger e possui arquitetura em camadas para organização e escalabilidade.

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core (.NET 8)
- Entity Framework Core
- PostgreSQL
- Docker (para banco de dados)
- Swagger (Swashbuckle)
- JWT (JSON Web Tokens)
- BCrypt para hash de senhas
- Insomnia (testes manuais)
- Git + GitHub

---

## 📁 Estrutura do Projeto

```
MonolitoBackend/
├── MonolitoBackend.Api             # Camada da API (Controllers, Program.cs, Swagger)
├── MonolitoBackend.Core            # Entidades, interfaces e contratos
├── MonolitoBackend.Infrastructure  # DbContext, repositórios e serviços
├── docker-compose.yml              # Subida do PostgreSQL com Docker
```

---

## 🔧 Como Rodar o Projeto Localmente

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
```

### 2. Suba o banco de dados com Docker

```bash
docker-compose up -d
```

Certifique-se de que o PostgreSQL está rodando na porta 5432.

### 3. Configure a conexão e JWT no `appsettings.json`

No projeto `MonolitoBackend.Api`, edite o arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=salasdb;Username=usuario;Password=senha"
  },
  "JwtSettings": {
    "Secret": "sua_chave_secreta_muito_segura_com_pelo_menos_32_caracteres",
    "ExpirationInHours": 24
  }
}
```

### 4. Rode as migrations

```bash
dotnet ef database update --project MonolitoBackend.Infrastructure --startup-project MonolitoBackend.Api
```

### 5. Rode o projeto

```bash
cd MonolitoBackend.Api
dotnet run
```

A API estará disponível em:  
`http://localhost:5027/swagger`

---

## 🔐 Autenticação e Autorização

A API utiliza JWT (JSON Web Tokens) para autenticação e autorização. Todos os endpoints (exceto login e registro) requerem um token JWT válido.

### 1. Registro de Usuário (`POST /api/auth/register`)
```json
{
  "name": "Usuário Teste",
  "email": "teste@exemplo.com",
  "password": "senha123",
  "role": "User"
}
```

### 2. Login (`POST /api/auth/login`)
```json
{
  "email": "teste@exemplo.com",
  "password": "senha123"
}
```

### 3. Usando o Token
Após o login, você receberá um token JWT. Use-o no header das requisições:
```
Authorization: Bearer seu_token_aqui
```

---

## 🧪 Testes de API

Você pode testar a API via Swagger UI ou Insomnia/Postman.

### Exemplos de payload:

#### Criar Sala (`POST /rooms`)
```json
{
  "name": "Sala de Reunião",
  "capacity": 15,
  "hasProjector": true
}
```

#### Criar Reserva (`POST /reservations`)
```json
{
  "roomId": 1,
  "reservedBy": "Rafael Vieira",
  "startTime": "2024-04-29T09:00:00Z",
  "endTime": "2024-04-29T10:00:00Z"
}
```

---

## ✅ Funcionalidades Implementadas

- [x] Cadastro, listagem, atualização e remoção de salas
- [x] Cadastro, listagem, atualização e remoção de reservas
- [x] Listar reservas por sala
- [x] Validação de dados via `[ApiController]`
- [x] Integração com banco PostgreSQL via Docker
- [x] Documentação automática com Swagger
- [x] Autenticação com JWT
- [x] Hash de senhas com BCrypt
- [x] Autorização baseada em roles
- [x] Proteção de endpoints com `[Authorize]`

---

## 🔒 Segurança

- Senhas são armazenadas com hash usando BCrypt
- Tokens JWT com expiração configurável
- Endpoints protegidos com autenticação
- Suporte a diferentes níveis de acesso (roles)
- Validação de dados de entrada
- Proteção contra SQL Injection via EF Core

---

## ✍️ Autor

Desenvolvido por **Rafael Silva Vieira** como parte de projeto acadêmico.
