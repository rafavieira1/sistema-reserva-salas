# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar os arquivos de projeto
COPY ["MonolitoBackend.Api/MonolitoBackend.Api.csproj", "MonolitoBackend.Api/"]
COPY ["MonolitoBackend.Core/MonolitoBackend.Core.csproj", "MonolitoBackend.Core/"]
COPY ["MonolitoBackend.Infrastructure/MonolitoBackend.Infrastructure.csproj", "MonolitoBackend.Infrastructure/"]
COPY ["SistemaReservaSalas.sln", "./"]

# Restaurar as dependências
RUN dotnet restore

# Copiar o resto do código
COPY . .

# Publicar a aplicação
RUN dotnet publish -c Release -o /app/publish

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta que a aplicação usa
EXPOSE 5027

# Definir o comando para executar a aplicação
ENTRYPOINT ["dotnet", "MonolitoBackend.Api.dll"] 