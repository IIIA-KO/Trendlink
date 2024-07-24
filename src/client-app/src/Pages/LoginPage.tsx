import React, {useEffect, useState} from 'react';
//import { Formik, Field, Form, ErrorMessage } from 'formik';
//import * as Yup from 'yup';
import { useAuth } from '../context/AuthContext';
import axios from 'axios';

const LoginPage: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { login } = useAuth();
   
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        await login(email, password);
    };

    const [countries, setCountries] = useState([]);

    useEffect(() => {
       axios.get("https://localhost:5001/api/countries")
       .then(response => setCountries(response.data))
    }, []);

    return (
        <ul>
            {countries.map((country: any) => (
                <li key={country.id}>{country.name}</li>
            ))}
        </ul>
    );
};

export default LoginPage;