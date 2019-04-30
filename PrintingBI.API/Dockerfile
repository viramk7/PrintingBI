FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["PrintingBI.API/PrintingBI.API.csproj", "PrintingBI.API/"]
RUN dotnet restore "PrintingBI.API/PrintingBI.API.csproj"
COPY . .
WORKDIR "/src/PrintingBI.API"
RUN dotnet build "PrintingBI.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PrintingBI.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PrintingBI.API.dll"]