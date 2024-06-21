import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { TextField, Button, Container, Typography, Box } from '@mui/material';

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async () => {
        try {
            const response = await axios.post('http://103.109.37.107:81/api/user/login', {
                username,
                password,
            });

            const { token } = response.data;
            localStorage.setItem('token', token); // Lưu token vào localStorage
            navigate('/product-list'); // Chuyển hướng tới trang danh sách sản phẩm
        } catch (err) {
            setError('Username hoặc password không đúng');
        }
    };

    return (
        <Container maxWidth="sm" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', minHeight: '100vh' }}>
            <Box display="flex" flexDirection="column" alignItems="center" width="100%">
                <Typography variant="h4" gutterBottom>This is Login Page</Typography>
                <TextField
                    label="Username"
                    variant="outlined"
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                    margin="normal"
                    fullWidth
                />
                <TextField
                    label="Password"
                    type="password"
                    variant="outlined"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    margin="normal"
                    fullWidth
                />
                {error && <Typography variant="body1" color="error">{error}</Typography>}
                <Button onClick={handleLogin} variant="contained" color="primary" style={{ marginTop: '16px' }}>
                    Login
                </Button>
            </Box>
        </Container>
    );
};

export default Login;
