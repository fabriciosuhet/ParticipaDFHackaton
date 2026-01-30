# 1. Estágio de Build (SDK 9.0)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
WORKDIR /src

# Copia e restaura
COPY ["SolucaoParticipaDF.API/SolucaoParticipaDF.API.csproj", "SolucaoParticipaDF.API/"]
RUN dotnet restore "SolucaoParticipaDF.API/SolucaoParticipaDF.API.csproj"

# Copia o resto e publica
COPY . .
WORKDIR "/src/SolucaoParticipaDF.API"
RUN dotnet publish "SolucaoParticipaDF.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 2. Estágio Final (Runtime 9.0)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Instala libgdiplus (Essencial para Excel no Linux, continua necessário no .NET 9)
RUN apt-get update && apt-get install -y libgdiplus

# Copia a aplicação compilada
COPY --from=build /app/publish .

# Porta padrão
ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "SolucaoParticipaDF.API.dll"]