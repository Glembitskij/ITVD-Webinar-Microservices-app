# 1. ЗБІРКА (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копіюємо все (простіше для маленьких проектів)
COPY . . 
# Відновлюємо та публікуємо однією командою
RUN dotnet publish "Store.Stock.Api/Store.Stock.Api.csproj" -c Release -o /app/dist

# 2. ЗАПУСК (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080

# Забираємо тільки готовий результат
COPY --from=build /app/dist .

ENTRYPOINT ["dotnet", "Store.Stock.Api.dll"]