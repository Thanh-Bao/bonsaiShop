import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link, useLocation } from 'react-router-dom';
import Login from './Login';
import { AppBar, Toolbar, Tabs, Tab, Container } from '@mui/material';
import Register from './Register';
import ProductList from './ProductList';
import CreateProduct from './CreateProduct';
import ProtectedRoute from './ProtectedRoute';


const NavBar = () => {
  const location = useLocation();
  const currentTab = location.pathname;

  return (
    <AppBar position="static">
      <Toolbar>
        <Tabs value={currentTab}>
          <Tab label="Login" value="/login" component={Link} to="/login" />
          <Tab label="Register" value="/register" component={Link} to="/register" />
          <Tab label="Product List" value="/product-list" component={Link} to="/product-list" />
          <Tab label="Create Product" value="/create-product" component={Link} to="/create-product" />
        </Tabs>
      </Toolbar>
    </AppBar>
  );
};

const App = () => {

  return (
    <Router>
      <NavBar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/product-list" element={<ProtectedRoute><ProductList /></ProtectedRoute>} />
        <Route path="/create-product" element={<ProtectedRoute><CreateProduct /></ProtectedRoute>} />
      </Routes>
    </Router>
  );
};

export default App;
