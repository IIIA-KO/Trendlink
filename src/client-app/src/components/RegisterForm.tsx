import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { register } from '../api/authApi';
import { getCountries } from '../api/countriesAndStatesApi.ts';
import { getStates } from '../api/countriesAndStatesApi.ts';
import useAuth from '../hooks/useAuth';
import {CountryType} from "../models/CountryType.ts";
import {StateType} from "../models/StateType.ts";
import axios from 'axios';

const RegisterForm: React.FC = () => {
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [birthDate, setBirthDate] = useState('');
    const [email, setEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [password, setPassword] = useState('');
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [states, setStates] = useState<StateType[]>([]);
    const [selectedCountry, setSelectedCountry] = useState('');
    const [selectedState, setSelectedState] = useState('');
    const { login: authLogin } = useAuth();
    const navigate = useNavigate();

    useEffect(() => {
        const fetchCountries = async () => {
            const countriesData = await getCountries();
            setCountries(countriesData);
        };
        fetchCountries();
    }, []);

    useEffect(() => {
        const fetchStates = async () => {
            if (selectedCountry) {
                const statesData = await getStates(selectedCountry);
                setStates(statesData);
            }
        };
        fetchStates();
    }, [selectedCountry]);

    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            const userData = await register({
                firstName,
                lastName,
                birthDate,
                email,
                phoneNumber,
                password,
                stateId: selectedState,
            });
            authLogin(userData);
            navigate('/');
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response) {
                console.error('Registration error:', error.response.data);
            } else {
                console.error('Network/Server error:', error instanceof Error ? error.message : 'Unknown error');
            }
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
                placeholder="First Name"
                required
            />
            <input
                type="text"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
                placeholder="Last Name"
                required
            />
            <input
                type="date"
                value={birthDate}
                onChange={(e) => setBirthDate(e.target.value)}
                placeholder="Birth Date"
                required
            />
            <input
                type="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                placeholder="Email"
                required
            />
            <input
                type="tel"
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
                placeholder="Phone Number"
                required
            />
            <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                placeholder="Password"
                required
            />
            <select
                value={selectedCountry}
                onChange={(e) => setSelectedCountry(e.target.value)}
                required
            >
                <option value="">Select Country</option>
                {countries.map((country) => (
                    <option key={country.id} value={country.id}>
                        {country.name}
                    </option>
                ))}
            </select>
            <select
                value={selectedState}
                onChange={(e) => setSelectedState(e.target.value)}
                required
            >
                <option value="">Select State</option>
                {states.map((state) => (
                    <option key={state.id} value={state.id}>
                        {state.name}
                    </option>
                ))}
            </select>
            <button type="submit">Register</button>
        </form>
    );
};

export default RegisterForm;