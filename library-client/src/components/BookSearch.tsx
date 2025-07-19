import React, { useState } from 'react';
import { 
  Box, 
  Typography, 
  FormControl, 
  InputLabel, 
  Select, 
  MenuItem, 
  TextField, 
  Button, 
  Paper,
  SelectChangeEvent 
} from '@mui/material';
import { BookService } from '../services/BookService';
import { Book, OwnershipStatus } from '../models/Book';
import { PagedResult } from '../models/PagedResult';
import BookTable from './BookTable';
import Pagination from './Pagination';

const BookSearch: React.FC = () => {
  const [searchBy, setSearchBy] = useState<string>('');
  const [searchValue, setSearchValue] = useState<string>('');
  const [pagedResult, setPagedResult] = useState<PagedResult<Book> | null>(null);
  const [searched, setSearched] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize] = useState<number>(10);

  const handleSearchByChange = (event: SelectChangeEvent) => {
    setSearchBy(event.target.value as string);
  };

  const handleSearchValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchValue(event.target.value);
  };

  const handleSearch = async (page: number = 1) => {
    if (!searchBy || !searchValue) {
      setError('Please select search criteria and enter a search value');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const results = await BookService.searchBooksPaged(searchBy, searchValue, page, pageSize);
      setPagedResult(results);
      setSearched(true);
      setCurrentPage(page);
    } catch (err) {
      setError('Failed to search books. Please try again.');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handlePageChange = (page: number) => {
    handleSearch(page);
  };

  return (
    <Box sx={{ mt: 3 }}>
      <Typography variant="h4" gutterBottom>
        Search Books
      </Typography>
      
      <Paper sx={{ p: 3, mb: 3 }}>
        <Box sx={{ display: 'flex', alignItems: 'flex-end', gap: 2, mb: 2 }}>
          <FormControl sx={{ minWidth: 200 }}>
            <InputLabel id="search-by-label">Search By:</InputLabel>
            <Select
              labelId="search-by-label"
              value={searchBy}
              label="Search By"
              onChange={handleSearchByChange}
            >
              <MenuItem value="title">Title</MenuItem>
              <MenuItem value="author">Author</MenuItem>
              <MenuItem value="isbn">ISBN</MenuItem>
              <MenuItem value="status">Ownership Status</MenuItem>
            </Select>
          </FormControl>
          
          {searchBy === 'status' ? (
            <FormControl fullWidth>
              <InputLabel id="status-search-label">Ownership Status</InputLabel>
              <Select
                labelId="status-search-label"
                value={searchValue}
                label="Ownership Status"
                onChange={(e) => setSearchValue(e.target.value as string)}
              >
                <MenuItem value={OwnershipStatus.Own}>Own</MenuItem>
                <MenuItem value={OwnershipStatus.Love}>Love</MenuItem>
                <MenuItem value={OwnershipStatus.WantToRead}>Want to Read</MenuItem>
              </Select>
            </FormControl>
          ) : (
            <TextField
              label="Search Value"
              variant="outlined"
              fullWidth
              value={searchValue}
              onChange={handleSearchValueChange}
            />
          )}
          
          <Button 
            variant="contained" 
            onClick={() => handleSearch()}
            disabled={loading}
          >
            Search
          </Button>
        </Box>
        
        {error && (
          <Typography color="error" sx={{ mt: 2 }}>
            {error}
          </Typography>
        )}
      </Paper>
      
      {searched && (
        <Box>
          <Typography variant="h5" gutterBottom>
            Search Results:
          </Typography>
          {pagedResult && pagedResult.items.length > 0 ? (
            <>
              <Typography variant="body2" sx={{ mb: 2, color: 'text.secondary' }}>
                Found {pagedResult.totalCount} books matching your search
                (Page {pagedResult.currentPage} of {pagedResult.totalPages})
              </Typography>
              <BookTable books={pagedResult.items} />
              <Pagination
                currentPage={pagedResult.currentPage}
                totalPages={pagedResult.totalPages}
                onPageChange={handlePageChange}
                hasPreviousPage={pagedResult.hasPreviousPage}
                hasNextPage={pagedResult.hasNextPage}
              />
            </>
          ) : (
            !loading && <Typography>No books found matching your search criteria.</Typography>
          )}
        </Box>
      )}
    </Box>
  );
};

export default BookSearch;