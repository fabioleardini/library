# Royal Library - Book Management System

## Overview
Royal Library is a full-stack book management system that allows users to search, view, add, update, and delete books in a library database. The application provides a clean and intuitive interface for managing a personal book library.

## Features
- Search books by title, author, ISBN, or ownership status
- View all books in the library
- Add new books to the library
- Edit existing book information
- Delete books from the library
- Track book availability (total copies vs. copies in use)
- Categorize books by type and category
- Manage ownership status (Own, Love, Want to Read)

## Technology Stack

### Frontend
- React with TypeScript
- React Router for navigation
- Material-UI for UI components

### Backend
- ASP.NET Core 9.0 Web API
- Clean Architecture pattern
  - Domain layer: Core entities and interfaces
  - Application layer: Business logic and services
  - Infrastructure layer: Data access and external services
  - API layer: Controllers and endpoints
- Entity Framework Core with In-Memory database

## Project Structure

```
/Library.API - ASP.NET Core Web API
/Library.Application - Application services and interfaces
/Library.Domain - Domain models and interfaces
/Library.Infrastructure - Data access and repositories
/library-client - React frontend application
```

## Getting Started

### Prerequisites
- .NET 9.0 SDK
- Node.js and npm

### Running the Backend
1. Navigate to the project root directory
2. Run the API project:
   ```
   cd Library.API
   dotnet run
   ```
3. The API will be available at http://localhost:5007

### Running the Frontend
1. Navigate to the client directory:
   ```
   cd library-client
   ```
2. Install dependencies:
   ```
   npm install
   ```
3. Start the development server:
   ```
   npm start
   ```
4. The frontend will be available at http://localhost:3000

## API Endpoints

- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `GET /api/books/search?searchBy={field}&searchValue={value}` - Search books
- `POST /api/books` - Add a new book
- `PUT /api/books/{id}` - Update an existing book
- `DELETE /api/books/{id}` - Delete a book