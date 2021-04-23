#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["desafio-rdi.webapi/desafio-rdi.webapi.csproj", "desafio-rdi.webapi/"]
RUN dotnet restore "desafio-rdi.webapi/desafio-rdi.webapi.csproj"
COPY . .
WORKDIR "/src/desafio-rdi.webapi"

RUN dotnet build "desafio-rdi.webapi.csproj" -c Release -o /app/build
RUN  dotnet test

FROM build AS publish
RUN dotnet publish "desafio-rdi.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "desafio-rdi.webapi.dll"]