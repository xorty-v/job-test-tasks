# MiraGames

## Быстрый старт через Docker Compose

```bash
docker compose up --build
```

Запустятся три контейнера:

- **backend** – API на http://localhost:5000
- **frontend** – React на http://localhost:5173
- **db** – PostgreSQL на 5432

## Ручной запуск без Docker

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

## Особенности

Проект не разделён на слои и остаётся минималистичным ради простоты.