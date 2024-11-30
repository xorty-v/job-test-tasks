# Тестовое задание для Роболайн

## Требования

- .NET SDK 8.0
- Docker и Docker Compose

## Установка

1. Установите [Docker](https://www.docker.com/get-started) и [Docker Compose](https://docs.docker.com/compose/install/).
2. Установите .NET SDK [здесь](https://dotnet.microsoft.com/download).

## Запуск

1. Склонируйте репозиторий:
    ```bash
    git clone https://github.com/xorty-v/Roboline.git
    cd Roboline
    ```

2. Соберите и запустите контейнеры:
    ```bash
    docker-compose up -d --build
    ```

3. Приложение будет доступно по адресу: `http://localhost:5000/swagger/index.html`.

4. Для остановки контейнеров выполните:
```bash
docker-compose down
```

## Конфигурация

- Базу данных можно изменить в файле `docker-compose.yml` в секции `environment`.
