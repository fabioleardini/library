import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import {
  Box,
  Typography,
  TextField,
  Button,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  Grid,
  Paper,
  CircularProgress,
  SelectChangeEvent
} from '@mui/material';
import { Book } from '../models/Book';
import { BookService } from '../services/BookService';

const initialBookState: Omit<Book, 'id' | 'author' | 'availableCopies'> = {
  title: '',
  firstName: '',
  lastName: '',
  totalCopies: 0,
  copiesInUse: 0,
  type: '',
  isbn: '',
  category: ''
};

const BookForm: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();
  const [book, setBook] = useState<any>(initialBookState);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

  const isEditMode = !!id;

  useEffect(() => {
    if (isEditMode) {
      const fetchBook = async () => {
        setLoading(true);
        try {
          const data = await BookService.getBookById(parseInt(id));
          setBook(data);
          setError(null);
        } catch (err) {
          setError('Failed to fetch book details. Please try again.');
          console.error(err);
        } finally {
          setLoading(false);
        }
      };

      fetchBook();
    }
  }, [id, isEditMode]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setBook({ ...book, [name]: value });
  };

  const handleNumberInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setBook({ ...book, [name]: parseInt(value) || 0 });
  };

  const handleSelectChange = (e: SelectChangeEvent) => {
    const { name, value } = e.target;
    setBook({ ...book, [name]: value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError(null);

    try {
      if (isEditMode) {
        await BookService.updateBook(book);
      } else {
        await BookService.addBook(book);
      }
      navigate('/books');
    } catch (err) {
      setError(`Failed to ${isEditMode ? 'update' : 'add'} book. Please try again.`);
      console.error(err);
    } finally {
      setIsSubmitting(false);
    }
  };

  if (loading) {
    return (
      <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}>
        <CircularProgress />
      </Box>
    );
  }

  return (
    <Box sx={{ mt: 3 }}>
      <Typography variant="h4" gutterBottom>
        {isEditMode ? 'Edit Book' : 'Add New Book'}
      </Typography>

      <Paper sx={{ p: 3 }}>
        {error && (
          <Typography color="error" sx={{ mb: 2 }}>
            {error}
          </Typography>
        )}

        <form onSubmit={handleSubmit}>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <TextField
                name="title"
                label="Title"
                fullWidth
                required
                value={book.title}
                onChange={handleInputChange}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                name="firstName"
                label="Author First Name"
                fullWidth
                required
                value={book.firstName}
                onChange={handleInputChange}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                name="lastName"
                label="Author Last Name"
                fullWidth
                required
                value={book.lastName}
                onChange={handleInputChange}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                name="totalCopies"
                label="Total Copies"
                type="number"
                fullWidth
                required
                value={book.totalCopies}
                onChange={handleNumberInputChange}
                inputProps={{ min: 0 }}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                name="copiesInUse"
                label="Copies In Use"
                type="number"
                fullWidth
                required
                value={book.copiesInUse}
                onChange={handleNumberInputChange}
                inputProps={{ min: 0, max: book.totalCopies }}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <FormControl fullWidth>
                <InputLabel id="type-label">Type</InputLabel>
                <Select
                  labelId="type-label"
                  name="type"
                  value={book.type}
                  label="Type"
                  onChange={handleSelectChange}
                >
                  <MenuItem value="Hardcover">Hardcover</MenuItem>
                  <MenuItem value="Paperback">Paperback</MenuItem>
                  <MenuItem value="E-Book">E-Book</MenuItem>
                  <MenuItem value="Audiobook">Audiobook</MenuItem>
                </Select>
              </FormControl>
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                name="isbn"
                label="ISBN"
                fullWidth
                value={book.isbn}
                onChange={handleInputChange}
              />
            </Grid>

            <Grid item xs={12} sm={6}>
              <TextField
                name="category"
                label="Category"
                fullWidth
                value={book.category}
                onChange={handleInputChange}
              />
            </Grid>



            <Grid item xs={12} sx={{ mt: 2 }}>
              <Button
                type="submit"
                variant="contained"
                color="primary"
                disabled={isSubmitting}
                sx={{ mr: 2 }}
              >
                {isSubmitting ? 'Saving...' : isEditMode ? 'Update Book' : 'Add Book'}
              </Button>
              <Button
                variant="outlined"
                onClick={() => navigate('/books')}
                disabled={isSubmitting}
              >
                Cancel
              </Button>
            </Grid>
          </Grid>
        </form>
      </Paper>
    </Box>
  );
};

export default BookForm;