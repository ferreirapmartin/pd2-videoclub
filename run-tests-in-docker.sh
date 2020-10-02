#!/bin/bash

docker build . --target test -t pd2-videoclub:test
id=$(docker create pd2-videoclub:test)
docker cp $id:/src/TestResults ./testresult
docker rm $id