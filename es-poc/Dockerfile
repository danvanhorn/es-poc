FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 64932
EXPOSE 44342

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["es-poc/es-poc.csproj", "es-poc/"]
RUN dotnet restore "es-poc/es-poc.csproj"
COPY . .
WORKDIR "/src/es-poc"
RUN dotnet build "es-poc.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "es-poc.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "es-poc.dll"]