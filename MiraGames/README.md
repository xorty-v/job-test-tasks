# MiraGames

## Быстрый старт через Docker Compose

```bash
docker compose up --build
```

Запустятся три контейнера:

- **backend** – API на http://localhost:5000
- **frontend** – React на http://localhost:5173
- **db** – PostgreSQL на 5432

## Учётные данные по умолчанию

```
admin@mirra.dev / admin123
```

## Примеры запросов

### Авторизация

```bash
curl -X POST http://localhost:5000/auth/login \
     -H "Content-Type: application/json" \
     -d '{"email":"admin@mirra.dev","password":"admin123"}'
```

В ответе придёт пара токенов. Используйте `accessToken` для авторизации в следующих запросах:

```bash
curl http://localhost:5000/clients \
     -H "Authorization: Bearer <ACCESS_TOKEN>"
```

```bash
curl -X POST http://localhost:5000/rate \
     -H "Authorization: Bearer <ACCESS_TOKEN>" \
     -H "Content-Type: application/json" \
     -d '{"value":12}'
```

## ✅ Бонусы

- Реализована полноценная авторизация через JWT (HS256) с поддержкой refresh‑токенов
- Полный CRUD для `/clients`
- История платежей отображается на UI
- Docker Compose с тремя контейнерами:
  - Backend с миграциями
  - Frontend (Vite + React)
  - PostgreSQL

## Оправдание :)

В рамках выполнения ТЗ сознательно не усложнял архитектуру и не разделял проект на отдельные слои (Domain, Application и т.д.), чтобы сосредоточиться на основной логике и требованиях. Такой подход был выбран для компактности. При разработке полноценного проекта я бы использовал слоистую архитектуру с явным разделением ответственности.
