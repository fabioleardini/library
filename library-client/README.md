# Royal Library - Client Application

This is the frontend client for the Royal Library Book Management System. It's built with React, TypeScript, and Material-UI.

## Prerequisites

- Node.js (v14 or later)
- npm (v6 or later)

## Getting Started

1. Install dependencies:

```bash
npm install
```

2. Start the development server:

```bash
npm start
```

The application will be available at [http://localhost:3000](http://localhost:3000).

## Features

- View all books in the library
- Search books by title, author, ISBN, or ownership status
- Add new books to the library
- Edit existing book details
- Delete books from the library

## API Connection

The client connects to the backend API at `http://localhost:5007/api`. If your API is running on a different port, you'll need to update the `API_URL` in `src/services/BookService.ts`.

## Building for Production

To create a production build:

```bash
npm run build
```

The build artifacts will be stored in the `build/` directory.