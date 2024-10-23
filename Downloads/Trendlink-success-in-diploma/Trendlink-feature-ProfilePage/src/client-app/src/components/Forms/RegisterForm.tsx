import React, {ChangeEvent, useEffect, useState} from 'react';
import * as Yup from 'yup';
import { useNavigate } from 'react-router-dom';
import { register } from '../../services/auth';
import { getCountries, getStates } from '../../services/countriesAndStates';
import useAuth from '../../hooks/useAuth';
import { CountryType } from "../../types/CountryType";
import { StateType } from "../../types/StateType";
import { Formik, Field, Form, ErrorMessage } from 'formik';
import axios from 'axios';
import PasswordInputField from "../Inputs/PasswordInputField";
import EmailInputFiled from "../Inputs/EmailInputField";
import AuthSubButton from "../Buttons/AuthSubButton";

const RegisterForm: React.FC = () => {
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [states, setStates] = useState<StateType[]>([]);
    const [selectedCountryId, setSelectedCountryId] = useState<string>('');
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
        firstName: Yup.string()
            .required('Required field'),

        lastName: Yup.string()
            .required('Required field'),

        birthDate: Yup.date()
            .required('Required field')
            .max(new Date(new Date().setFullYear(new Date().getFullYear() - 18)), 'You must be at least 18 years old'),

        email: Yup.string()
            .email('Invalid email format')
            .required('Required field'),

        phoneNumber: Yup.string()
            .required('Required field')
            .min(10, 'PhoneNumber must not be less than 10 characters.')
            .max(20, 'PhoneNumber must not exceed 20 characters.')
            .matches(/^\d{10,20}$/, 'PhoneNumber not valid'),

        password: Yup.string()
            .required('Required field')
            .min(8, 'Password length must be at least 8.')
            .max(16, 'Password length must not exceed 16.')
            .matches(/[A-Z]/, 'Password must contain at least one uppercase letter.')
            .matches(/[a-z]/, 'Password must contain at least one lowercase letter.')
            .matches(/\d/, 'Password must contain at least one digit.')
            .matches(/[!@#$%^&*(),.?":{}|<>]/, 'Password must contain at least one special character.'),

        countryId: Yup.string()
            .required('Required field'),

        stateId: Yup.string()
            .required('Required field')
    });

    const handleSubmit = async (values: typeof initialValues, { setSubmitting }: { setSubmitting: (isSubmitting: boolean) => void }) => {
        try {
            const userData = await register(values);
            authLogin(userData!);
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
            if (selectedCountryId) {
                const statesData = await getStates(selectedCountryId);
                setStates(statesData);
            }
        };

        fetchStates();
    }, [selectedCountryId])

    const getFieldClasses = (touched: boolean | undefined, error: string | undefined) => {
        return `bg-transparent w-full px-4 py-2 border-b-2 focus:outline-none ${touched && error ? 'border-red-500' : 'border-gray-300 focus:border-blue-500'}`;
    };

    return (
        <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={handleSubmit}
        >
            {({touched, errors, setFieldValue }) => (
                <Form className="space-y-4">
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
                            className="text-red-500 text-xs mt-1 px-4"
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
                            className="text-red-500 text-xs mt-1 px-4"
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
                            className="text-red-500 text-xs mt-1 px-4"
                        />
                    </div>

                    <EmailInputFiled/>

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
                            className="text-red-500 text-xs mt-1 px-4"
                        />
                    </div>

                    <PasswordInputField/>

                    <div className="text-[0.85rem]">
                        <Field
                            as="select"
                            name="countryId"
                            className={getFieldClasses(touched.countryId, errors.countryId)}
                            onChange={async (e: ChangeEvent<HTMLSelectElement>) => {
                                const countryId = e.target.value;
                                setFieldValue('countryId', countryId);
                                setFieldValue('stateId', '');

                                setSelectedCountryId(countryId);
                            }}
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
                            className="text-red-500 text-xs mt-1 px-4"
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
                            className="text-red-500 text-xs mt-1 px-4"
                        />
                    </div>
                    <div className="flex justify-between items-center">
                        <AuthSubButton buttonText="Register" />
                    </div>
                </Form>
            )}
        </Formik>
    );
};

export default RegisterForm;