# Sử dụng image chính thức của .NET 8 SDK để build ứng dụng
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy toàn bộ project vào container
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build ứng dụng
RUN dotnet publish -c Release -o out

# Tạo image chạy ứng dụng từ image runtime của .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Chạy ứng dụng
CMD ["dotnet", "ElectricCarStoreAPI.dll"]
