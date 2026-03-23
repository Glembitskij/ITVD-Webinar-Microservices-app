# Етап 1: Збірка
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Копіюємо файл проекту та відновлюємо залежності
COPY ["Store.Orders.Api/Store.Orders.Api.csproj", "Store.Orders.Api/"]
RUN dotnet restore "Store.Orders.Api/Store.Orders.Api.csproj"

# Копіюємо все інше і збираємо
COPY . .
WORKDIR "/src/Store.Orders.Api"
RUN dotnet publish "Store.Orders.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Етап 2: Runtime (фінальний образ)
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Store.Orders.Api.dll"]