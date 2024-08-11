import React, { useEffect, useState } from 'react';
import * as Yup from 'yup';
import { useNavigate } from 'react-router-dom';
import { register } from '../api/authApi';
import { getCountries, getStates } from '../api/countriesAndStatesApi.ts';
import useAuth from '../hooks/useAuth';
import { CountryType } from "../models/CountryType.ts";
import { StateType } from "../models/StateType.ts";
import { Formik, Field, Form, ErrorMessage } from 'formik';
import axios from 'axios';

const RegisterForm: React.FC = () => {
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [states, setStates] = useState<StateType[]>([]);
    const { login: authLogin } = useAuth();
    const navigate = useNavigate();

    const initialValues = {
        firstName: '',
        lastName: '',
        birthDate: '',
        email: '',
        phoneNumber: '',
        password: '',
        countryId: '',
        stateId: '',
    };

    const validationSchema = Yup.object({
        firstName: Yup.string().required('First Name is required'),
        lastName: Yup.string().required('Last Name is required'),
        birthDate: Yup.string().required('Birth Date is required'),
        email: Yup.string().email('Invalid email address').required('Email is required'),
        phoneNumber: Yup.string().required('Phone Number is required'),
        password: Yup.string().min(6, 'Password must be at least 6 characters').required('Password is required'),
        countryId: Yup.string().required('Country is required'),
        stateId: Yup.string().required('State is required'),
    });

    const handleSubmit = async (values: typeof initialValues, { setSubmitting }: { setSubmitting: (isSubmitting: boolean) => void }) => {
        try {
            const userData = await register(values);
            authLogin(userData);
            navigate('/');
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response) {
                console.error('Registration error:', error.response.data);
            } else {
                console.error('Network/Server error:', error instanceof Error ? error.message : 'Unknown error');
            }
        } finally {
            setSubmitting(false);
        }
    };

    useEffect(() => {
        const fetchCountries = async () => {
            const countriesData = await getCountries();
            setCountries(countriesData);
        };
        fetchCountries();
    }, []);

    useEffect(() => {
        const fetchStates = async () => {
            if (initialValues.countryId) {
                const statesData = await getStates(initialValues.countryId);
                setStates(statesData);
            }
        };
        fetchStates();
    }, [initialValues.countryId]);

    const getFieldClasses = (touched: boolean | undefined, error: string | undefined) => {
        return `w-full px-4 py-2 border-b-2 focus:outline-none ${touched && error ? 'border-red-500' : 'border-gray-300 focus:border-blue-500'}`;
    };

    return (
        <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={handleSubmit}
        >
            {({touched, errors }) => (
                <Form className="space-y-6">
                    <div className="text-[0.85rem]">
                        <Field
                            name="firstName"
                            type="text"
                            placeholder="First Name"
                            className={getFieldClasses(touched.firstName, errors.firstName)}
                        />
                        <ErrorMessage
                            name="firstName"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            name="lastName"
                            type="text"
                            placeholder="Last Name"
                            className={getFieldClasses(touched.lastName, errors.lastName)}
                        />
                        <ErrorMessage
                            name="lastName"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            name="birthDate"
                            type="date"
                            placeholder="Birth Date"
                            className={getFieldClasses(touched.birthDate, errors.birthDate)}
                        />
                        <ErrorMessage
                            name="birthDate"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            name="email"
                            type="email"
                            placeholder="Email"
                            className={getFieldClasses(touched.email, errors.email)}
                        />
                        <ErrorMessage
                            name="email"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            name="phoneNumber"
                            type="tel"
                            placeholder="Phone Number"
                            className={getFieldClasses(touched.phoneNumber, errors.phoneNumber)}
                        />
                        <ErrorMessage
                            name="phoneNumber"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            name="password"
                            type="password"
                            placeholder="Password"
                            className={getFieldClasses(touched.password, errors.password)}
                        />
                        <ErrorMessage
                            name="password"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            as="select"
                            name="countryId"
                            className={getFieldClasses(touched.countryId, errors.countryId)}
                        >
                            <option value="">Select Country</option>
                            {countries.map((country) => (
                                <option key={country.id} value={country.id}>
                                    {country.name}
                                </option>
                            ))}
                        </Field>
                        <ErrorMessage
                            name="countryId"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="text-[0.85rem]">
                        <Field
                            as="select"
                            name="stateId"
                            className={getFieldClasses(touched.stateId, errors.stateId)}
                        >
                            <option value="">Select State</option>
                            {states.map((state) => (
                                <option key={state.id} value={state.id}>
                                    {state.name}
                                </option>
                            ))}
                        </Field>
                        <ErrorMessage
                            name="stateId"
                            component="div"
                            className="text-red-500 text-xs mt-1"
                        />
                    </div>
                    <div className="flex justify-between items-center">
                        <button
                            type="submit"
                            className="w-full h-[47px] py-2 bg-gray-800 text-white text-[1rem] mt-4 rounded-full hover:bg-gray-700 transition-colors"
                        >
                            Register
                        </button>
                    </div>
                </Form>
            )}
        </Formik>
    );
};

export default RegisterForm;