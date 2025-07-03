# --- Est�gio 1: Build da Aplica��o ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copia os arquivos de projeto (.csproj) e o .sln primeiro.
COPY *.sln .
COPY TaskManagement.Api/*.csproj ./TaskManagement.Api/
COPY TaskManagement.Application/*.csproj ./TaskManagement.Application/
COPY TaskManagement.Domain/*.csproj ./TaskManagement.Domain/
COPY TaskManagement.Infrastructure/*.csproj ./TaskManagement.Infrastructure/
COPY TaskManagement.Tests/*.csproj ./TaskManagement.Tests/

# Restaura todos os pacotes NuGet da solu��o.
RUN dotnet restore TaskManagement.sln

# Copia todo o resto do c�digo fonte para o container.
COPY . .

# Publica a aplica��o em modo Release.
WORKDIR /source/TaskManagement.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# --- Est�gio 2: Imagem Final ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "TaskManagement.Api.dll"]