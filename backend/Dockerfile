# Базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем файлы проектов
COPY ["TaskManager.API/TaskManager.API.csproj", "TaskManager.API/"]
COPY ["TaskManager.Domain/TaskManager.Domain.csproj", "TaskManager.Domain/"]
COPY ["TaskManager.Application/TaskManager.Application.csproj", "TaskManager.Application/"]
COPY ["TaskManager.Infrastructure/TaskManager.Infrastructure.csproj", "TaskManager.Infrastructure/"]

# Восстанавливаем зависимости
RUN dotnet restore "TaskManager.API/TaskManager.API.csproj"

# Копируем всё остальное
COPY . .

# Публикуем проект
RUN dotnet publish "TaskManager.API/TaskManager.API.csproj" -c Release -o /app/publish

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Открываем порт
EXPOSE 80
ENTRYPOINT ["dotnet", "TaskManager.API.dll"]