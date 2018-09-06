FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app
RUN apt-get update \
    && apt-get upgrade -y \
    && curl -sL https://deb.nodesource.com/setup_8.x | bash - \
    && apt-get install -y nodejs

# Copy csproj and restore as distinct layers
COPY src/Din/*.csproj ./Din/
COPY src/Din.Data/*.csproj ./Din.Data/
COPY src/Din.Service/*.csproj ./Din.Service/
COPY src/Din.Tests/*.csproj ./Din.Tests/
COPY src/Din.sln ./
COPY src/nuget.config ./
RUN dotnet restore ./

# Copy everything else
COPY src/ ./

# Restore npm packages
WORKDIR ./Din
RUN npm i
RUN npm i -g gulp
RUN gulp build

# Publish
WORKDIR /app
RUN dotnet publish ./Din/ -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/Din/out .
ENTRYPOINT ["dotnet", "Din.dll"]
