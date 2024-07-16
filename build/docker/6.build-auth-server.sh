#!/bin/sh
. ./get-version.sh

echo Building mytelegram/mytelegram-auth-server:$imageVersion
docker build -t mytelegram/mytelegram-auth-server -f ./Dockerfile-auth-server ../../source
docker tag mytelegram/mytelegram-auth-server mytelegram/mytelegram-auth-server:$imageVersion
