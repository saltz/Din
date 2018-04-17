#!/usr/bin/env bash
set -e
artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then
  rm -R $artifactsFolder
fi

dotnet restore src/
dotnet test src/Din.Tests/
dotnet build src/Din/

revision=${TRAVIS_JOB_ID:=1}
revision=$(printf "%04d" $revision)

dotnet pack ./src/Din -c Release -o ./artifacts --version-suffix=$revision
