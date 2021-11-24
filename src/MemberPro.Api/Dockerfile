#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /tmp
COPY ["src/MemberPro.Api/MemberPro.Api.csproj", "src/MemberPro.Api/"]
RUN dotnet restore "src/MemberPro.Api/MemberPro.Api.csproj"
COPY . .
WORKDIR "/tmp/src/MemberPro.Api"
RUN dotnet build "MemberPro.Api.csproj" -c Release

FROM build AS publish
RUN dotnet publish "MemberPro.Api.csproj" --no-build -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MemberPro.Api.dll"]
