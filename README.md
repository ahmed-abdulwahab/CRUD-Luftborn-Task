# CRUD-Luftborn-Task

# Overview
**This project is built using ASP.NET Core with Entity Framework and Identity for authentication and role management.
It includes user registration, login, and CRUD operations while generating JWT tokens.
Unlike the built-in ASP.NET Core Identity functions (such as UserManager and UserRole), this project implements authentication and role management manually.
The application also features Single Sign-On (SSO) with Google authentication, tested with a single endpoint in a separate controller.
please make sure to be logged out when using the SSO google authenticate to use the exact google token thanks!**

# Features
- Implemented ASP.NET Core Identity with manual role and user management
- User registration and login pages
- CRUD operations with JWT token generation
- Database creation with seed data
- Applied a simple repository-unit of work pattern (without transaction statements as it's a simple task)
- Mapped all entities to their DTOs using AutoMapper
- Implemented optional SSO with Google authentication for testing
- Applied filtration in data retrieval
- Added an exception-handling middleware (not fully applied throughout the app yet)

# Frontend (Angular)
- Used Bootstrap components with modifications
- Applied simple CSS styling on some pages
- Added notes inside the app to clarify functionality and usage

# Technologies Used
- ASP.NET Core 8
- Entity Framework Core
- JWT Authentication
- AutoMapper
- Angular with Bootstrap
- SQL Server






![Screenshot 2025-02-01 073512](https://github.com/user-attachments/assets/7649a3a7-9be9-49dd-86d7-5dd633bd1c6a)

![image](https://github.com/user-attachments/assets/9d8f4eb5-dfe4-44bb-8ea9-db0507324436)

![image](https://github.com/user-attachments/assets/31541ae8-2e0c-42e7-a8a7-f7842cafe758)

![image](https://github.com/user-attachments/assets/18c48b5d-1f1a-4007-9bb7-6990f7eb96f2)

![image](https://github.com/user-attachments/assets/4e4cd260-d9f6-4891-a385-ff7a223eb2ad)

![image](https://github.com/user-attachments/assets/e2e160ac-1480-4436-b467-93e1c3b60ba5)

![image](https://github.com/user-attachments/assets/99dc6fdd-cbc8-43f8-b3e4-bb9e18aeb3b7)




















