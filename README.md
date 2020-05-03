# merchant

## Intro

merchant service for Cloud Mall

## Docker

``` bash
# remove before docker image if exists
# docker rmi cloudmall/merchant:latest

docker run -d -p 5060:80 --name merchant-service cloudmall/merchant:latest

docker run -d -p 5060:80 -v /etc/configs/merchant.appsettings.json:/app/appsettings/Production.json --name merchant-service cloudmall/merchant:latest
```
