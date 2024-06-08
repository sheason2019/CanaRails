FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Postgres 数据库使用的相关环境变量
ENV Host ""
ENV Database ""
ENV Username ""
ENV Password ""

WORKDIR /app

# 复制 dll 文件
COPY ./bin/Release/net8.0 ./
# 复制静态资源
COPY ./wwwroot ./wwwroot

ENTRYPOINT ["dotnet", "./Dashboard.dll"]
