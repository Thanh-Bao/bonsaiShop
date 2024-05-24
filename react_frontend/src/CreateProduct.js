import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import {
    TextField, Button, CircularProgress, Container, Typography, Box, Dialog, DialogTitle, DialogContent, DialogActions, List, ListItem, ListItemText
} from '@mui/material';

const CreateProduct = () => {
    const [name, setName] = useState('');
    const [price, setPrice] = useState('');
    const [quantity, setQuantity] = useState('');
    const [description, setDescription] = useState('');
    const [loading, setLoading] = useState(false);
    const [errorMessages, setErrorMessages] = useState({});
    const [success, setSuccess] = useState('');
    const [open, setOpen] = useState(false);
    const navigate = useNavigate();

    const handleCreateProduct = async () => {
        setLoading(true);
        setErrorMessages({});
        setSuccess('');

        try {
            const token = localStorage.getItem('token');
            const response = await axios.post('http://localhost:32768/api/product', {
                name,
                price,
                quantity,
                description,
            }, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            setSuccess('Product created successfully');
            setTimeout(() => navigate('/product-list'), 2000); // Chuyển hướng sau 2 giây
        } catch (err) {
            if (err.response && err.response.data && err.response.data.errors) {
                setErrorMessages(err.response.data.errors);
                setOpen(true);
            } else {
                setErrorMessages({ general: ['Failed to create product'] });
                setOpen(true);
            }
        } finally {
            setLoading(false);
        }
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <Container>
            <Typography variant="h4" gutterBottom>Create Product</Typography>
            <Box display="flex" flexDirection="column" alignItems="center">
                <TextField
                    label="Name (từ 10 đến 255 ký tự)"
                    variant="outlined"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    margin="normal"
                    fullWidth
                />
                <TextField
                    label="Price (lớn hơn 100.000VND và nhỏ hơn 1 triệu"
                    variant="outlined"
                    type="number"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                    margin="normal"
                    fullWidth
                />
                <TextField
                    label="Quantity (lớn hơn 0 và nhỏ hơn 1000)"
                    variant="outlined"
                    type="number"
                    value={quantity}
                    onChange={(e) => setQuantity(e.target.value)}
                    margin="normal"
                    fullWidth
                />
                <TextField
                    label="Description (từ 255 ký tự đến 1024 ký tự)"
                    variant="outlined"
                    multiline
                    rows={4}
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    margin="normal"
                    fullWidth
                />
                {loading ? <CircularProgress style={{ margin: '16px 0' }} /> : null}
                {success && <Typography variant="body1" color="primary">{success}</Typography>}
                <Button
                    onClick={handleCreateProduct}
                    variant="contained"
                    color="primary"
                    disabled={loading}
                    style={{ marginTop: '16px' }}
                >
                    Create Product
                </Button>
            </Box>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Error</DialogTitle>
                <DialogContent>
                    <List>
                        {Object.keys(errorMessages).map((key) => (
                            errorMessages[key].map((message, index) => (
                                <ListItem key={index}>
                                    <ListItemText primary={`${key}: ${message}`} />
                                </ListItem>
                            ))
                        ))}
                    </List>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Close
                    </Button>
                </DialogActions>
            </Dialog>
        </Container>
    );
};

export default CreateProduct;
