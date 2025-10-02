Чтобы добавить новую миграцию, нужно:
1. Запустить проект
2. Найти строку подключения (например, ConnectionStrings__mmr)- это можно сделать в настройках джобы postgresMigration
3. После этого положить её в appsettings.json
    ```
    "ConnectionStrings": {
        "mmr": "Host=localhost;Port=PORT;Username=USERNAME;Password=PASSWORD;Database=mmr"
    }
   ```
4. Запустить команду `dotnet ef migrations add ENTER_MIGRATION_NAME --project ..\Domain\Domain.csproj` из корня проекта.
5. Миграция появится в папке Domain/Migrations.