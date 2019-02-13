FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
COPY . /app
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 9222
EXPOSE 5000 
ENTRYPOINT ["dotnet", "es-poc.dll"]