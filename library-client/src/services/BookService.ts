import { Book } from '../models/Book';
import { PagedResult } from '../models/PagedResult';

// Update API URL to match the port in launchSettings.json
const API_URL = 'http://localhost:5007/api';

export const BookService = {
  getAllBooks: async (): Promise<Book[]> => {
    const response = await fetch(`${API_URL}/books`);
    if (!response.ok) {
      throw new Error('Failed to fetch books');
    }
    return response.json();
  },

  getBookById: async (id: number): Promise<Book> => {
    const response = await fetch(`${API_URL}/books/${id}`);
    if (!response.ok) {
      throw new Error('Failed to fetch book');
    }
    return response.json();
  },

  searchBooks: async (searchBy: string, searchValue: string): Promise<Book[]> => {
    const response = await fetch(`${API_URL}/books/search?searchBy=${searchBy}&searchValue=${searchValue}`);
    if (!response.ok) {
      throw new Error('Failed to search books');
    }
    return response.json();
  },

  addBook: async (book: Omit<Book, 'id'>): Promise<Book> => {
    const response = await fetch(`${API_URL}/books`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book),
    });
    if (!response.ok) {
      throw new Error('Failed to add book');
    }
    return response.json();
  },

  updateBook: async (book: Book): Promise<Book> => {
    const response = await fetch(`${API_URL}/books/${book.id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(book),
    });
    if (!response.ok) {
      throw new Error('Failed to update book');
    }
    return response.json();
  },

  deleteBook: async (id: number): Promise<void> => {
    const response = await fetch(`${API_URL}/books/${id}`, {
      method: 'DELETE',
    });
    if (!response.ok) {
      throw new Error('Failed to delete book');
    }
  },

  getAllBooksPaged: async (page: number = 1, pageSize: number = 20): Promise<PagedResult<Book>> => {
    const response = await fetch(`${API_URL}/books/paged?page=${page}&pageSize=${pageSize}`);
    if (!response.ok) {
      throw new Error('Failed to fetch paged books');
    }
    return response.json();
  },

  searchBooksPaged: async (searchBy: string, searchValue: string, page: number = 1, pageSize: number = 20): Promise<PagedResult<Book>> => {
    const response = await fetch(`${API_URL}/books/search/paged?searchBy=${searchBy}&searchValue=${searchValue}&page=${page}&pageSize=${pageSize}`);
    if (!response.ok) {
      throw new Error('Failed to search paged books');
    }
    return response.json();
  },
};