# Bước 1: Sử dụng image của OpenJDK
FROM openjdk:17-jdk-slim

# Bước 2: Tạo thư mục làm việc trong container
WORKDIR /app

# Bước 3: Copy file JAR từ máy host vào container
COPY target/identity_service-0.0.1-SNAPSHOT.jar app.jar

# Bước 4: Expose cổng mà ứng dụng sử dụng
EXPOSE 9090

# Bước 5: Chạy ứng dụng
CMD ["java", "-jar", "app.jar"]