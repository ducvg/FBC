kill -9 $(lsof -t -i :5062)
dotnet build
dotnet publish -c Release -o ./publish
# dotnet publish/FBC.dll
dotnet run