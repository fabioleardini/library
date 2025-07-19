import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';

interface HeaderProps {
  title: string;
}

const Header: React.FC<HeaderProps> = ({ title }) => {
  return (
    <AppBar position="static">
      <Toolbar>
        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
          {title}
        </Typography>
        <Box>
          <Button color="inherit" component={RouterLink} to="/">
            Search
          </Button>
          <Button color="inherit" component={RouterLink} to="/books">
            All Books
          </Button>
          <Button color="inherit" component={RouterLink} to="/books/add">
            Add Book
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Header;