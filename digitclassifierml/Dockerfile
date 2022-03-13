FROM mcr.microsoft.com/dotnet/sdk:5.0 AS environment

WORKDIR /app
COPY . /app
# COPY ./src/Api/Api.csproj .
RUN dotnet restore ./src/Api/Api.csproj

COPY . ./app
COPY /src/Trainer/Model/model.zip /src/Trainer/Model/model.zip
WORKDIR /app/src/Api/
RUN dotnet publish -c release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN apt-get update && apt-get install -y libgdiplus
# ENV ASPNETCORE_URLS=http://*:5000
# EXPOSE 5000
WORKDIR /app
COPY /src/Trainer/Model/model.zip /src/Trainer/Model/model.zip
COPY . /app
COPY /src/Trainer/Model/model.zip /src/Trainer/Model/model.zip
COPY --from=environment /app/src/Api/out .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Api.dll