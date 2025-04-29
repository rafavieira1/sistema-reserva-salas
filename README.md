
# Sistema de Reservas de Salas

API RESTful desenvolvida em .NET 8 com ASP.NET Core para gerenciamento de salas e reservas, utilizando Entity Framework Core com persistência em PostgreSQL. A aplicação é documentada com Swagger e possui arquitetura em camadas para organização e escalabilidade.

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core (.NET 8)
- Entity Framework Core
- PostgreSQL
- Docker (para banco de dados)
- Swagger (Swashbuckle)
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

### 3. Configure a conexão no `appsettings.json`

No projeto `MonolitoBackend.Api`, edite o arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=salasdb;Username=usuario;Password=senha"
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
  "reservedBy": "João Silva",
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

---

## ✍️ Autor

Desenvolvido por **Rafael Silva Vieira** como parte de projeto acadêmico.
