#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS build
WORKDIR /src
COPY ["TaxMicroserviceTakeHomeAssesment.csproj", "."]
RUN dotnet restore "./TaxMicroserviceTakeHomeAssesment.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TaxMicroserviceTakeHomeAssesment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxMicroserviceTakeHomeAssesment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxMicroserviceTakeHomeAssesment.dll"]