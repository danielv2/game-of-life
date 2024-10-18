# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder

WORKDIR /source
COPY ./ ./

RUN dotnet restore
RUN dotnet publish -c Release ./GOF.Host --output /app/ --no-restore

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
COPY --from=builder /app .

# Instalar dotnet-ef para rodar migrations
RUN dotnet tool install --global dotnet-ef

# Certifique-se de que o .NET global tools path esteja no PATH
ENV PATH="$PATH:/root/.dotnet/tools"

# Copiar o script de entrypoint
COPY deploy/entrypoint.sh /app/entrypoint.sh

# Dar permissão para o script ser executável
RUN chmod +x /app/entrypoint.sh

# Usar o script como entrypoint
ENTRYPOINT ["/app/entrypoint.sh"]
