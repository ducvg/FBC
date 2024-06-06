# Stage 1: Use the .NET runtime image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS runtime
WORKDIR /app

EXPOSE 5000

COPY /publish/* .

ENTRYPOINT ["dotnet", "FBC.dll"]