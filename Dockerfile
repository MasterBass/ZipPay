FROM mcr.microsoft.com/dotnet/sdk:3.1

COPY . /app
WORKDIR /app

# Explicitly ask for path to be added
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 3.1.8

ENV ASPNETCORE_ENVIRONMENT docker
ENV ASPNETCORE_URLS=http://+:80  


RUN dotnet restore TestProject.WebAPI.sln
RUN dotnet build TestProject.API/TestProject.API.csproj -c Release

EXPOSE 80/tcp

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh