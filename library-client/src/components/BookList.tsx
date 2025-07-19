import React, { useEffect, useState } from 'react';
import { Box, Typography, CircularProgress } from '@mui/material';
import { BookService } from '../services/BookService';
import { Book } from '../models/Book';
import { PagedResult } from '../models/PagedResult';
import BookTable from './BookTable';
import Pagination from './Pagination';

const BookList: React.FC = () => {
  const [pagedResult, setPagedResult] = useState<PagedResult<Book> | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize] = useState<number>(10);

  const fetchBooks = async (page: number = currentPage) => {
    setLoading(true);
    try {
      const data = await BookService.getAllBooksPaged(page, pageSize);
      setPagedResult(data);
      setError(null);
    } catch (err) {
      setError('Failed to fetch books. Please try again.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handlePageChange = (page: number) => {
    setCurrentPage(page);
    fetchBooks(page);
  };

  useEffect(() => {
    fetchBooks(currentPage);
  }, []);

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
        <CircularProgress />
      </Box>
    );
  }

  if (error) {
    return (
      <Box sx={{ mt: 3 }}>
        <Typography color="error">{error}</Typography>
      </Box>
    );
  }

  return (
    <Box sx={{ mt: 3 }}>
      <Typography variant="h4" gutterBottom>
        All Books
      </Typography>
      {pagedResult && pagedResult.items.length > 0 ? (
        <>
          <Typography variant="body2" sx={{ mb: 2, color: 'text.secondary' }}>
            Showing {pagedResult.items.length} of {pagedResult.totalCount} books
            (Page {pagedResult.currentPage} of {pagedResult.totalPages})
          </Typography>
          <BookTable books={pagedResult.items} onBookDeleted={() => fetchBooks(currentPage)} />
          <Pagination
            currentPage={pagedResult.currentPage}
            totalPages={pagedResult.totalPages}
            onPageChange={handlePageChange}
            hasPreviousPage={pagedResult.hasPreviousPage}
            hasNextPage={pagedResult.hasNextPage}
          />
        </>
      ) : (
        !loading && <Typography>No books found in the library.</Typography>
      )}
    </Box>
  );
};

export default BookList;