import React, {ChangeEvent, useEffect, useState} from 'react';
import * as Yup from 'yup';
import { getCountries, getStates } from '../../services/countriesAndStates';
import { CountryType } from "../../types/CountryType";
import { StateType } from "../../types/StateType";
import { Formik, Field, Form, ErrorMessage } from 'formik';
import {googleClientId, googleRedirectUri, googleScope, googleResponseType} from "../../utils/constants";

const GoogleRegisterForm: React.FC = () => {
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [states, setStates] = useState<StateType[]>([]);
    const [selectedCountryId, setSelectedCountryId] = useState<string>('');

    const initialValues = {
        birthDate: '',
        phoneNumber: '',
        countryId: '',
        stateId: '',
    };

    const validationSchema = Yup.object({

        birthDate: Yup.date()
            .required('Required field')
            .max(new Date(new Date().setFullYear(new Date().getFullYear() - 18)), 'You must be at least 18 years old'),

        phoneNumber: Yup.string()
            .required('Required field')
            .min(10, 'PhoneNumber must not be less than 10 characters.')
            .max(20, 'PhoneNumber must not exceed 20 characters.')
            .matches(/^\d{10,20}$/, 'PhoneNumber not valid'),

        countryId: Yup.string()
            .required('Required field'),

        stateId: Yup.string()
            .required('Required field')
    });

    const handleSubmit = async (values: typeof initialValues, { setSubmitting }: { setSubmitting: (isSubmitting: boolean) => void }) => {
        try {
            const { birthDate, phoneNumber, stateId } = values;

            const authUrl = `https://accounts.google.com/o/oauth2/v2/auth?client_id=${googleClientId}&redirect_uri=${googleRedirectUri}&response_type=${googleResponseType}&scope=${googleScope}&state=${encodeURIComponent(`birthDate=${birthDate}&phoneNumber=${phoneNumber}&stateId=${stateId}`)}`;

            window.location.href = authUrl;
        } catch (error) {
            console.error('Registration error:', error);
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
                <Form className="space-y-6">
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
                            {countries
                                .slice()
                                .sort((a, b) => a.name.localeCompare(b.name))
                                .map((country) => (
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
                            {states
                                .slice()
                                .sort((a, b) => a.name.localeCompare(b.name))
                                .map((state) => (
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
                        <button
                            type="submit"
                            className="w-full h-[47px] py-2 bg-gray-800 text-white text-[1rem] mt-4 rounded-full transition duration-500 ease-in-out hover:bg-gray-700 hover:scale-110 active:scale-90 focus:scale-100 transform"
                        >
                            Register with Google
                        </button>
                    </div>
                </Form>
            )}
        </Formik>
    );
};

export default GoogleRegisterForm;