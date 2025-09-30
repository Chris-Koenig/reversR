# Full-Stack Echo API Demo

A simple full-stack application demonstrating a .NET 8 Minimal API backend with React 18 frontend.

## Architecture

### Backend (.NET 8 Minimal API)
- **Location**: `backend/EchoApi/`
- **Port**: 5000
- **Features**:
  - POST `/api/echo` endpoint
  - FluentValidation for input validation
  - Serilog for structured logging
  - Clean architecture with separated concerns
  - CORS configuration for frontend access

### Frontend (React 18 + Vite + TypeScript)
- **Location**: `frontend/`
- **Port**: 5173
- **Features**:
  - TypeScript for type safety
  - Centralized API client for DRY code
  - Clean, responsive UI with Flexbox
  - Error handling and loading states

## Project Structure

```
reversR/
├── backend/
│   ├── EchoApi/
│   │   ├── Models/
│   │   │   └── EchoModels.cs          # DTOs
│   │   ├── Services/
│   │   │   └── EchoService.cs         # Business logic
│   │   ├── Validators/
│   │   │   └── EchoRequestValidator.cs # Input validation
│   │   └── Program.cs                  # API configuration
│   └── EchoApi.Tests/
│       ├── Services/
│       │   └── EchoServiceTests.cs    # Service tests
│       └── Validators/
│           └── EchoRequestValidatorTests.cs # Validation tests
└── frontend/
    └── src/
        ├── types/
        │   └── api.ts                  # TypeScript interfaces
        ├── services/
        │   └── apiClient.ts           # API communication
        ├── App.tsx                    # Main component
        └── App.css                    # Styling
```

## Getting Started

### Prerequisites
- .NET 8 SDK
- Node.js 18+
- npm

### Running the Application

1. **Start the Backend** (Terminal 1):
   ```bash
   cd backend/EchoApi
   dotnet run
   ```
   Backend will be available at: http://localhost:5000

2. **Start the Frontend** (Terminal 2):
   ```bash
   cd frontend
   npm install  # Only needed first time
   npm run dev
   ```
   Frontend will be available at: http://localhost:5173

### Running Tests

**Backend Tests**:
```bash
cd backend/EchoApi.Tests
dotnet test
```

## API Documentation

### POST /api/echo

**Request**:
```json
{
  "text": "Hello World"
}
```

**Response (Success)**:
```json
{
  "text": "Hello World"
}
```

**Response (Validation Error)**:
```json
{
  "errors": ["Text cannot be empty"]
}
```

**Validation Rules**:
- Text cannot be empty
- Text cannot exceed 500 characters

## Technical Highlights

### Backend
- **Single Responsibility Principle**: Service, validator, and DTOs are separated
- **Dependency Injection**: Proper DI container usage
- **Structured Logging**: Serilog with console output
- **Input Validation**: FluentValidation with custom error messages
- **CORS**: Configured to allow frontend origin only
- **Type Safety**: Record types for immutable DTOs

### Frontend
- **Type Safety**: Full TypeScript coverage
- **Error Handling**: Comprehensive error display
- **API Abstraction**: Reusable API client
- **User Experience**: Loading states and input validation
- **Responsive Design**: Clean, centered layout

## Development Features

- **Hot Reload**: Both frontend (Vite HMR) and backend (dotnet watch) support hot reload
- **Type Safety**: End-to-end type safety from API to UI
- **Testing**: Unit tests for business logic and validation
- **Clean Code**: Separation of concerns and DRY principles