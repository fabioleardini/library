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
import { Book } from '../models/Book';
import BookTable from './BookTable';

const BookSearch: React.FC = () => {
  const [searchBy, setSearchBy] = useState<string>('');
  const [searchValue, setSearchValue] = useState<string>('');
  const [books, setBooks] = useState<Book[]>([]);
  const [searched, setSearched] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleSearchByChange = (event: SelectChangeEvent) => {
    setSearchBy(event.target.value as string);
  };

  const handleSearchValueChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setSearchValue(event.target.value);
  };

  const handleSearch = async () => {
    if (!searchBy || !searchValue) {
      setError('Please select search criteria and enter a search value');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const results = await BookService.searchBooks(searchBy, searchValue);
      setBooks(results);
      setSearched(true);
    } catch (err) {
      setError('Failed to search books. Please try again.');
      console.error(err);
    } finally {
      setLoading(false);
    }
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
          
          <TextField
            label="Search Value"
            variant="outlined"
            fullWidth
            value={searchValue}
            onChange={handleSearchValueChange}
          />
          
          <Button 
            variant="contained" 
            onClick={handleSearch}
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
          {books.length > 0 ? (
            <BookTable books={books} />
          ) : (
            <Typography>No books found matching your search criteria.</Typography>
          )}
        </Box>
      )}
    </Box>
  );
};

export default BookSearch;