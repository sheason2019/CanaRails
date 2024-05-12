FROM mcr.microsoft.com/dotnet/asp.net:8.0 AS runtime

WORKDIR /app

COPY ./dist ./

ENTRYPOINT ["./Integration"]
