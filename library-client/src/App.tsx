import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Container, CssBaseline, ThemeProvider, createTheme } from '@mui/material';
import Header from './components/Header';
import BookList from './components/BookList';
import BookSearch from './components/BookSearch';
import BookForm from './components/BookForm';

const theme = createTheme({
  palette: {
    primary: {
      main: '#1976d2',
    },
    secondary: {
      main: '#dc004e',
    },
  },
});

const App: React.FC = () => {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <Header title="Royal Library" />
        <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
          <Routes>
            <Route path="/" element={<BookSearch />} />
            <Route path="/books" element={<BookList />} />
            <Route path="/books/add" element={<BookForm />} />
            <Route path="/books/edit/:id" element={<BookForm />} />
          </Routes>
        </Container>
      </Router>
    </ThemeProvider>
  );
};

export default App;