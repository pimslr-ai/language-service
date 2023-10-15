FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LanguageService/LanguageService.csproj", "LanguageService/"]
RUN dotnet restore "LanguageService/LanguageService.csproj"
COPY . .
WORKDIR "/src/LanguageService"
RUN dotnet build "LanguageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LanguageService.csproj" -c Release -o /app/publish

# Add a COPY command to copy the credentials.json file
COPY credentials.json /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LanguageService.dll"]
