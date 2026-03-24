# 1. ЗБІРКА (SDK)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копіюємо все (припускаємо, що контекст — це папка мікросервісу)
COPY . .

# Одразу публікуємо проект
RUN dotnet publish "Store.Orders.Api/Store.Orders.Api.csproj" -c Release -o /app/dist

# 2. ЗАПУСК (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/dist .

ENTRYPOINT ["dotnet", "Store.Orders.Api.dll"]