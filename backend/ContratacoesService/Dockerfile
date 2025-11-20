FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ContratacoesService.csproj", "./"]
RUN dotnet restore "ContratacoesService.csproj"
COPY . .
RUN dotnet build "ContratacoesService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContratacoesService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContratacoesService.dll"]
