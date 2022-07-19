#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
WORKDIR /app/FileUpload
WORKDIR /var/Charlotte-backend/Logs
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Charlotte/Charlotte.csproj", "Charlotte/"]
COPY ["Charlotte.Database/Charlotte.Database.csproj", "Charlotte.Database/"]
COPY ["Charlotte.Services/Charlotte.Services.csproj", "Charlotte.Services/"]
RUN dotnet restore "Charlotte/Charlotte.csproj"
COPY . .
WORKDIR "/src/Charlotte"
RUN dotnet build "Charlotte.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Charlotte.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Charlotte.dll"]