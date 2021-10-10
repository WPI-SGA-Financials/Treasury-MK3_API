#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Treasury.WebAPI/Treasury.WebAPI.csproj", "Treasury.WebAPI/"]
COPY ["Treasury.Application/Treasury.Application.csproj", "Treasury.Application/"]
COPY ["Treasury.Domain/Treasury.Domain.csproj", "Treasury.Domain/"]
RUN dotnet restore "Treasury.WebAPI/Treasury.WebAPI.csproj"
COPY . .
WORKDIR "/src/Treasury.WebAPI"
RUN dotnet build "Treasury.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Treasury.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Treasury.WebAPI.dll"]