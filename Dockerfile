# Используем официальный .NET SDK образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Устанавливаем рабочую директорию внутри контейнера
WORKDIR /app

# Копируем CSPROJ и восстанавливаем зависимости
COPY *.sln .
COPY EventTrackingSystem.*/*.csproj ./
RUN for file in *.csproj; do mkdir -p ${file%.*} && mv $file ${file%.*}/; done
RUN dotnet restore

# Копируем остальной код
COPY . ./

# Публикуем в папку /out
RUN dotnet publish EventTrackingSystem.Api/EventTrackingSystem.Api.csproj -c Release -o /out

# Используем минимальный runtime-образ
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Устанавливаем рабочую директорию
WORKDIR /app

# Копируем собранный проект
COPY --from=build /out .

# Открываем порт (если Render требует)
EXPOSE 80

# Стартовое приложение
ENTRYPOINT ["dotnet", "EventTrackingSystem.Api.dll"]