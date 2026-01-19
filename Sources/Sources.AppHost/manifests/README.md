# Фикс кодировки в PS 
`[Console]::OutputEncoding = [System.Text.Encoding]::GetEncoding("utf-8")`

# Собрать проект
- `docker login --username oauth --password [Сюда токен] -CA container-registry.cloud.yandex.net/crppvgkl2ft86f0jdvnc`
- `aspirate build -cr "container-registry.cloud.yandex.net" -crp "crppvgkl2ft86f0jdvnc" -ct 1.0.0`

