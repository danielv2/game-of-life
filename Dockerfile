FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder

WORKDIR /source
COPY ./ ./

# Install dotnet-ef
RUN dotnet tool install --global dotnet-ef

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet restore

RUN dotnet publish -c Release ./GOF.Host --output /app/ --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

COPY --from=builder /app .

COPY deploy/entrypoint.sh /app/entrypoint.sh

RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
