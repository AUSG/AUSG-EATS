FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AUSG.Eats.Presentation/AUSG.Eats.Presentation.csproj", "AUSG.Eats.Presentation/"]
RUN dotnet restore "AUSG.Eats.Presentation/AUSG.Eats.Presentation.csproj"
COPY . .
WORKDIR "/src/AUSG.Eats.Presentation"
RUN dotnet build "AUSG.Eats.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AUSG.Eats.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AUSG.Eats.Presentation.dll"]
