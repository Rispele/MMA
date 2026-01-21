# Фикс кодировки в PS 
`[Console]::OutputEncoding = [System.Text.Encoding]::GetEncoding("utf-8")`

# Собрать проект
- `docker login --username oauth --password [Сюда токен] container-registry.cloud.yandex.net/crppvgkl2ft86f0jdvnc`
- `aspirate build -cr "container-registry.cloud.yandex.net" -crp "crppvgkl2ft86f0jdvnc" -ct 1.0.0`

# sked and dns
ip route add 10.74.0.0/16 dev ppp0

# check
ping 10.74.240.10 # dns
ping 10.74.225.236 # sked
ping sked-tst.dit.urfu.ru