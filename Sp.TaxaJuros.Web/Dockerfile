#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Sp.TaxaJuros.Web/Sp.TaxaJuros.Web.csproj", "Sp.TaxaJuros.Web/"]
RUN dotnet restore "Sp.TaxaJuros.Web/Sp.TaxaJuros.Web.csproj"
COPY . .
WORKDIR "/src/Sp.TaxaJuros.Web"
RUN dotnet build "Sp.TaxaJuros.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sp.TaxaJuros.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sp.TaxaJuros.Web.dll"]