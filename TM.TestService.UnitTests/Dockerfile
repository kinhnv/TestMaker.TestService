#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "Business/TestMaker.Business.Admin.UnitTests/TestMaker.Business.Admin.UnitTests.csproj"
WORKDIR "/src/Business/TestMaker.Business.Admin.UnitTests"