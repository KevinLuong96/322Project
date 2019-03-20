FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
COPY . .
RUN dotnet restore 322Api.csproj
COPY . ./
RUN dotnet publish 322Api.csproj -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "322Api.dll"]
