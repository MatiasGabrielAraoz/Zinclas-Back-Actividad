FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["GestionApi.csproj", "./"]
RUN dotnet restore "GestionApi.csproj"

COPY . .
RUN dotnet publish "GestionApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app
COPY --from=build /app/publish .

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false


EXPOSE 5019
ENV ASPNETCORE_URLS=http://+:5019

ENTRYPOINT ["dotnet", "GestionApi.dll"]
