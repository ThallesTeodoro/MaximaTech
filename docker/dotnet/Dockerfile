FROM mcr.microsoft.com/dotnet/sdk:5.0
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
WORKDIR /app

EXPOSE 5000

RUN apt-get update && apt-get upgrade -y

RUN apt-get install -y libc6-dev libgdiplus

ENTRYPOINT dotnet watch --project source/MaximaTech.Web run --urls=http://+:5000
