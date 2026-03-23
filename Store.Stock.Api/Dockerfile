# ЕТАП 1: Збірка (Build)
# Використовуємо повний SDK для компіляції
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копіюємо файл проекту для відновлення залежностей (оптимізація кешу Docker)
COPY ["Store.Stock.Api/Store.Stock.Api.csproj", "Store.Stock.Api/"]
RUN dotnet restore "Store.Stock.Api/Store.Stock.Api.csproj"

# Копіюємо весь інший код
COPY . .

# Переходимо в папку проекту і публікуємо додаток
WORKDIR "/src/Store.Stock.Api"
RUN dotnet publish "Store.Stock.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ЕТАП 2: Рантайм (Runtime)
# Використовуємо легкий образ лише з необхідними бібліотеками для запуску
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

# Відкриваємо порт 8080 (стандарт для .NET контейнерів)
EXPOSE 8080

# Копіюємо тільки зібрані файли з першого етапу
COPY --from=build /app/publish .

# Запуск додатку
ENTRYPOINT ["dotnet", "Store.Stock.Api.dll"]