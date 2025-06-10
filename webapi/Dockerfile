from mcr.microsoft.com/dotnet/sdk:7.0 as build
workdir /src
expose 80
copy *.csproj .
run dotnet restore
copy . .
run dotnet publish -c Release -o /app/publish
from mcr.microsoft.com/dotnet/aspnet:7.0 as final
workdir /app
copy --from=build /app/publish .
entrypoint ["dotnet", "webapi.dll"]
