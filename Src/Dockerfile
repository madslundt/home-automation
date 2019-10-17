FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Src/Src.csproj", "Src/"]
RUN dotnet restore "Src/Src.csproj"
COPY . .
WORKDIR "/src/Src"
RUN dotnet build "Src.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Src.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Src.dll"]