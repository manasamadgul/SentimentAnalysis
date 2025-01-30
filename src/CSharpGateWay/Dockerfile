FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/CSharpGateWay/CSharpGateWay.csproj", "CSharpGateWay/"]
RUN dotnet restore "CSharpGateWay/CSharpGateWay.csproj"

COPY src/CSharpGateWay/. CSharpGateWay/

WORKDIR "/src/CSharpGateWay"
RUN dotnet build "CSharpGateWay.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CSharpGateWay.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "CSharpGateWay.dll"]