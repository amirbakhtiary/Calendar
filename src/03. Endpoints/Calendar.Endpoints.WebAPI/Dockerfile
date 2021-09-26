#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/03. Endpoints/Calendar.Endpoints.WebAPI/Calendar.Endpoints.WebAPI.csproj", "src/03. Endpoints/Calendar.Endpoints.WebAPI/"]
RUN dotnet restore "src/03. Endpoints/Calendar.Endpoints.WebAPI/Calendar.Endpoints.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/03. Endpoints/Calendar.Endpoints.WebAPI"
RUN dotnet build "Calendar.Endpoints.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Calendar.Endpoints.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calendar.Endpoints.WebAPI.dll"]