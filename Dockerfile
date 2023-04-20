FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Console Library Management Program.csproj", "./"]
RUN dotnet restore "Console Library Management Program.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Console Library Management Program.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Console Library Management Program.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Console Library Management Program.dll"]
