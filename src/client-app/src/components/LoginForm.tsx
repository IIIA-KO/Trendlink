import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../api/authApi';
import useAuth from '../hooks/useAuth';
import axios from "axios";

const LoginForm: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { login: authLogin } = useAuth();
    const navigate = useNavigate();

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            const userData = await login({ email, password });
            authLogin(userData);
            navigate('/');
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response) {
                console.error('Authentication error:', error.response.data);
            } else {
                console.error('Network/Server error:', error instanceof Error ? error.message : 'Unknown error');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
                required
            />
            <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
                required
            />
            <button type="submit">Login</button>
        </form>
    );
};

export default LoginForm;