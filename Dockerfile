# Dockerfile para desarrollo y depuración
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

# Instala debugger y herramientas
RUN apt-get update && apt-get install -y unzip curl && \
    curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg

WORKDIR /src

# Copia todo el código
COPY . .

# Restaura paquetes
RUN dotnet restore "./CallCenter/CallCenter.csproj"

# Expone el puerto HTTP por defecto (puedes cambiarlo si usas otro)
EXPOSE 5000

# Comando para desarrollo, no publica aún
CMD ["dotnet", "run", "--project", "CallCenter/CallCenter.csproj", "--urls", "http://0.0.0.0:5000"]
