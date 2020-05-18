# merchant

## Intro

merchant service for Cloud Mall

[![Build Status](https://weihanli.visualstudio.com/Pipelines/_apis/build/status/CloudMall.storage?branchName=master)](https://weihanli.visualstudio.com/Pipelines/_build/latest?definitionId=30&branchName=master)

## Docker

``` bash
# remove before docker image if exists
# docker rmi cloudmall/merchant:latest

docker run -d -p 5060:80 --name merchant-service cloudmall/merchant:latest

docker run -d -p 5060:80 -v /etc/configs/merchant.appsettings.json:/app/appsettings.Production.json --name merchant-service cloudmall/merchant:latest
```
