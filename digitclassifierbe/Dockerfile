FROM mcr.microsoft.com/dotnet/sdk:5.0 AS environment

WORKDIR /app
COPY ./src/Api/Api.csproj .
RUN dotnet restore

COPY . .
WORKDIR /app/src/Api/
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0
# ENV ASPNETCORE_URLS=http://*:5000
# EXPOSE 5000
WORKDIR /app
COPY --from=environment /app/src/Api/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Api.dll