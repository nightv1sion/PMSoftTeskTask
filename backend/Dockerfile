FROM mcr.microsoft.com/dotnet/sdk:5.0 as build-image
WORKDIR /home/app 
COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done
WORKDIR /home/app/API/
RUN dotnet restore
COPY . .
RUN dotnet test ./API.Tests/API.Tests.csproj
RUN dotnet publish ./API/API.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /publish
COPY --from=build-image /publish .

ENTRYPOINT [ "dotnet", "API.dll" ]