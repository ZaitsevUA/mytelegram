#!/bin/sh
v=$(head -n 1 ../version.txt)
currentDate=`date +%-m%d`
export version=$v.$currentDate
export imageVersion=$version
echo version: $version
echo image version: $imageVersion
