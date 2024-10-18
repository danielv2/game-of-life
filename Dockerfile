FROM mcr.microsoft.com/dotnet/sdk:7.0 AS builder

WORKDIR /source
COPY ./ .

RUN dotnet restore
RUN dotnet publish -c Release ./GOF.Host --output /app/ --no-restore
RUN dotnet tool install --global dotnet-ef --version 7.0.20
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet ef database update --project GOF.Host --startup-project GOF.Host

FROM  mcr.microsoft.com/dotnet/aspnet:7.0 as base

WORKDIR /app
COPY --from=builder /app .

ENTRYPOINT ["dotnet", "GOF.Host.dll"]