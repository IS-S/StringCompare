FROM docker-proxy.artifactory.tcsbank.ru/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY src/SomeProject.sln ./
COPY src/**/*.csproj ./

RUN for f in *.csproj; do \
        filename=$(basename $f) && \
        dirname=${filename%.*} && \
        mkdir $dirname && \
        mv $filename ./$dirname/; \
    done

ENV NUGET_CERT_REVOCATION_MODE=offline
ENV DOTNET_USE_POLLING_FILE_WATCHER true
RUN dotnet restore --no-cache -r linux-x64 SomeProject.Web -s http://vm-nuget01.tcsbank.ru/nuget -s https://nexus.tcsbank.ru/repository/nuget.org-proxy/ --packages /nuget/packages /p:PublishReadyToRun=true

COPY src ./

RUN dotnet publish SomeProject.Web -c Release -o /published/Web --no-restore -r linux-x64 --self-contained true /p:PublishReadyToRun=true

FROM docker-proxy.artifactory.tcsbank.ru/dotnet/runtime-deps:6.0 AS base
WORKDIR /app

ENV TZ=Europe/Moscow
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

FROM base AS production
WORKDIR /app
COPY --from=build-env /published/Web ./
ENTRYPOINT ["./SomeProject.Web"]

