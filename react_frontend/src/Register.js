import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { TextField, Button, Container, Typography, Box } from '@mui/material';

const Register = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleRegister = async () => {
        try {
            const response = await axios.post('http://localhost:32768/api/user', {
                username,
                password,
            });

            // Giả sử rằng việc đăng ký sẽ tự động đăng nhập người dùng hoặc điều hướng tới trang đăng nhập
            navigate('/login'); // Chuyển hướng tới trang đăng nhập sau khi đăng ký thành công
        } catch (err) {
            setError('Đăng ký thất bại. Vui lòng thử lại.');
        }
    };

    return (
        <Container maxWidth="sm" style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', minHeight: '100vh' }}>
            <Box display="flex" flexDirection="column" alignItems="center" width="100%">
                <Typography variant="h4" gutterBottom>This is Register Page</Typography>
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
                <Button onClick={handleRegister} variant="contained" color="primary" style={{ marginTop: '16px' }}>
                    Register
                </Button>
            </Box>
        </Container>
    );
};

export default Register;
