#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM ccr.ccs.tencentyun.com/magicodes/aspnetcore-runtime:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Magicodes.Webhooks/Magicodes.Webhooks.csproj", "Magicodes.Webhooks/"]
RUN dotnet restore "Magicodes.Webhooks/Magicodes.Webhooks.csproj"
COPY . .
WORKDIR "/src/Magicodes.Webhooks"
RUN dotnet build "Magicodes.Webhooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Magicodes.Webhooks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Magicodes.Webhooks.dll"]