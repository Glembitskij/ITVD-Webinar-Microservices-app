# 🐳 Посібник з розгортання в Docker (ITVD Webinar)

Цей документ містить повний набір інструкцій для збірки, запуску та адміністрування мікросервісів проєкту. Виконуйте команди послідовно.

---

## 🏗️ 1. Збірка Docker-образів

Перед запуском контейнерів необхідно зібрати Docker-образи:

```bash
# Збірка Stock Service
docker build -t store-stock-api:v1 -f Store.Stock.Api/Dockerfile .

# Збірка Orders Service
docker build -t store-orders-api:v1 -f Store.Orders.Api/Dockerfile .
```

---

## 🗄️ 2. Запуск інфраструктури (SQL Server)

```bash
docker run -d \
  --name sql-server-container \
  -e "ACCEPT_EULA=Y" \
  -e "MSSQL_SA_PASSWORD=YourStrongPassword123!" \
  -p 1433:1433 \
  mcr.microsoft.com/mssql/server:2022-latest
```

### 🔐 Налаштування підключення

- **Host:** localhost,1433  
- **User:** sa  
- **Password:** YourStrongPassword123!  

---

## 🚀 3. Запуск мікросервісів

### 🔹 Stock Service

```bash
docker run -d \
  --name stock-api-service \
  -p 5291:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  store-stock-api:v1
```

---

### 🔹 Orders Service

```bash
docker run -d \
  --name orders-api-service \
  -p 7229:8080 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e ConnectionStrings__DefaultConnection="Server=host.docker.internal,1433;Database=StoreOrdersDb;User Id=sa;Password=YourStrongPassword123!;TrustServerCertificate=True;" \
  store-orders-api:v1
```

---

## 🔗 4. Моніторинг та тестування

Після запуску сервіси доступні через Swagger:

| Сервіс         | Посилання                         | Опис                      |
|----------------|----------------------------------|---------------------------|
| Stock Service  | http://localhost:5291/index.html | Перегляд залишків         |
| Orders Service | http://localhost:7229/index.html | Створення замовлень       |

---

## 🛠️ 5. Корисні команди Docker

### 📊 Моніторинг та логи

```bash
docker ps
docker logs -f orders-api-service
docker stats
```

---

### ⚙️ Керування контейнерами

```bash
docker stop $(docker ps -q)
docker start sql-server-container
docker restart orders-api-service
```

---

### 🧹 Очищення системи

```bash
docker rm -f $(docker ps -aq)
docker image prune -a
docker system prune
```

---

## ⚠️ Можливі проблеми

### ❗ Порт 1433 зайнятий

Вимкніть локальний SQL Server (Services.msc), якщо він конфліктує з контейнером.

---

### ❗ host.docker.internal

- ✅ Windows / Mac — працює автоматично  
- ⚠️ Linux — додайте параметр:

```bash
--add-host=host.docker.internal:host-gateway
```

---

### ❗ SQL Server не встигає стартувати

Якщо Orders Service не може підключитися:

```bash
docker restart orders-api-service
```

Зачекайте ~10 секунд після запуску SQL Server.
