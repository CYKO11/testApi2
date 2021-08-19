#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["testApi2/testApi2.csproj", "testApi2/"]
RUN dotnet restore "testApi2/testApi2.csproj"
COPY . .
WORKDIR "/src/testApi2"
RUN dotnet build "testApi2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "testApi2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "testApi2.dll"]