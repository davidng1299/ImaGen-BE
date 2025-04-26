# ImaGen Backend Server
The backend web server for the ImaGen project, which handles user data and AI image generation logic.

## Frameworks & Tools
- .NET 9.0
- Entity Framework Core
- Swagger API
- Docker
- PostgreSQL server

## Getting Started
### Prerequisites
- .NET Runtime 9.0
- Visual Studio 2022
- A running PostgreSQL Database server

### Installation
Clone the project
```bash
git clone https://github.com/duckng99/ImaGen-BE.git
cd ImaGen-BE
```

Restore packages
```bash
dotnet restore
```

Apply database migrations
```bash
dotnet ef database update
```
## Running the Application
```bash
dotnet run
```

## API Documentation
After running the app, navigate to: https://localhost:5001/swagger

## Configuration
This project uses .env file to load environment variables that overwrites the values specified in `appsettings.json` at runtime. You will need to set up your own AWS and OpenAI API and store the API keys in a .env file.

## Contact
If you have any question or would like to contribute to this project, please feel free to contact me at davidng2312@gmail.com.
