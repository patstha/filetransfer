#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FileTransfer.Console/FileTransfer.Console.csproj", "FileTransfer.Console/"]
COPY ["FileTransfer.Persistence/FileTransfer.Persistence.csproj", "FileTransfer.Persistence/"]
RUN dotnet restore "FileTransfer.Console/FileTransfer.Console.csproj"
COPY . .
WORKDIR "/src/FileTransfer.Console"
RUN dotnet build "FileTransfer.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileTransfer.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileTransfer.Console.dll"]
