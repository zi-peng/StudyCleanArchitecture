FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Study.CleanArchitecture.WebAPI/Study.CleanArchitecture.WebAPI.csproj", "src/Study.CleanArchitecture.WebAPI/"]
RUN dotnet restore "src/Study.CleanArchitecture.WebAPI/Study.CleanArchitecture.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Study.CleanArchitecture.WebAPI"
RUN dotnet build "Study.CleanArchitecture.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Study.CleanArchitecture.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Study.CleanArchitecture.WebAPI.dll"]
