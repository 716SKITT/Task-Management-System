FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["TaskManager.API/TaskManager.API.csproj", "TaskManager.API/"]
COPY ["TaskManager.Domain/TaskManager.Domain.csproj", "TaskManager.Domain/"]
COPY ["TaskManager.Application/TaskManager.Application.csproj", "TaskManager.Application/"]
COPY ["TaskManager.Infrastructure/TaskManager.Infrastructure.csproj", "TaskManager.Infrastructure/"]

RUN dotnet restore "TaskManager.API/TaskManager.API.csproj"

COPY . .

RUN dotnet publish "TaskManager.API/TaskManager.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]