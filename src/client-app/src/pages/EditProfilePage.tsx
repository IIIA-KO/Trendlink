import React, { ChangeEvent, useEffect, useState } from 'react';
import * as Yup from 'yup';
import { Formik, Field, Form, ErrorMessage, FormikHelpers } from 'formik';
import { useNavigate } from 'react-router-dom';
import { useProfile } from '../hooks/useProfile';
import { useUser } from '../hooks/useUser';
import { getCountries, getStates } from '../services/countriesAndStates';
import { CountryType } from "../types/CountryType";
import { StateType } from "../types/StateType";
import { ProfileType } from '../types/ProfileType'; // Імпортуємо тип профілю
import AuthSubButton from '../components/Buttons/AuthSubButton';

const accountCategories = [
    { id: 0, name: "None" },
    { id: 1, name: "Cooking and Food" },
    { id: 2, name: "Fashion and Style" },
    { id: 3, name: "Clothing and Footwear" },
    { id: 4, name: "Horticulture" },
    { id: 5, name: "Animals" },
    { id: 6, name: "Cryptocurrency" },
    { id: 7, name: "Technology" },
    { id: 8, name: "Travel" },
    { id: 9, name: "Education" },
    { id: 10, name: "Fitness" },
    { id: 11, name: "Art" },
    { id: 12, name: "Photography" },
    { id: 13, name: "Music" },
    { id: 14, name: "Sports" },
    { id: 15, name: "Health and Wellness" },
    { id: 16, name: "Gaming" },
    { id: 17, name: "Parenting" },
    { id: 18, name: "DIY and Crafts" },
    { id: 19, name: "Literature" },
    { id: 20, name: "Science" },
    { id: 21, name: "History" },
    { id: 22, name: "News" },
    { id: 23, name: "Politics" },
    { id: 24, name: "Finance" },
    { id: 25, name: "Environment" },
    { id: 26, name: "Real Estate" },
    { id: 27, name: "Automobiles" },
    { id: 28, name: "Movies and TV" },
    { id: 29, name: "Comedy" },
    { id: 30, name: "Home Decor" },
    { id: 31, name: "Relationships" },
    { id: 32, name: "Self Improvement" },
    { id: 33, name: "Entrepreneurship" },
    { id: 34, name: "Legal Advice" },
    { id: 35, name: "Marketing" },
    { id: 36, name: "Mental Health" },
    { id: 37, name: "Personal Development" },
    { id: 38, name: "Religion and Spirituality" },
    { id: 39, name: "Social Media" },
];


const EditProfilePage: React.FC = () => {
    const navigate = useNavigate();
    const { updateProfileData, uploadPhoto } = useProfile();
    const { user } = useUser(); // Отримуємо користувача з контексту
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [states, setStates] = useState<StateType[]>([]);
    const [selectedCountryId, setSelectedCountryId] = useState<string | undefined>(undefined);
    const [selectedStateId, setSelectedStateId] = useState<string | undefined>(undefined);

    // Завантаження списку країн
    useEffect(() => {
        const fetchCountries = async () => {
            try {
                const response = await getCountries();
                setCountries(response);
            } catch (error) {
                console.error('Failed to load countries', error);
            }
        };

        fetchCountries();
    }, []);

    // Завантаження штатів при виборі країни
    useEffect(() => {
        if (selectedCountryId) {
            const fetchStates = async () => {
                try {
                    const response = await getStates(selectedCountryId);
                    setStates(response);
                } catch (error) {
                    console.error('Failed to load states', error);
                }
            };

            fetchStates();
        }
    }, [selectedCountryId]);

    // Встановлюємо країну та штат на основі назв, які прийшли з API
    useEffect(() => {
        if (user && countries.length > 0) {
            const country = countries.find(c => c.name === user.countryName);
            if (country) {
                setSelectedCountryId(country.id);
                const fetchStates = async () => {
                    const responseStates = await getStates(country.id);
                    setStates(responseStates);

                    const state = responseStates.find(s => s.name === user.stateName);
                    if (state) {
                        setSelectedStateId(state.id);
                    }
                };
                fetchStates();
            }
        }
    }, [user, countries]);

    const validationSchema = Yup.object({
        firstName: Yup.string().required('Required field'),
        lastName: Yup.string().required('Required field'),
        birthDate: Yup.date().required('Required field'),
        stateId: Yup.string().required('Required field'),
        bio: Yup.string().max(500, 'Bio should not exceed 500 characters'),
        accountCategory: Yup.number().required('Required field'),
    });

    const handleProfileSubmit = async (values: ProfileType, actions: FormikHelpers<ProfileType>) => {
        try {
            // Перетворюємо значення на number перед відправкою
            const updatedValues = {
                ...values,
                accountCategory: Number(values.accountCategory), // Конвертуємо у number
            };

            await updateProfileData(updatedValues);
            navigate('/');
        } catch (error) {
            console.error('Failed to update profile', error);
        } finally {
            actions.setSubmitting(false);
        }
    };

    const handlePhotoChange = async (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            const file = e.target.files[0];
            try {
                await uploadPhoto(file);
            } catch (error) {
                console.error('Failed to upload photo', error);
            }
        }
    };

    if (!user || !selectedCountryId || !selectedStateId) return <div>Loading...</div>;

    return (
        <div className="edit-profile-page">
            <h1>Edit Profile</h1>

            <Formik
                initialValues={{
                    firstName: user.firstName,
                    lastName: user.lastName,
                    birthDate: user.birthDate,
                    stateId: selectedStateId, // Використовуємо ID штату
                    bio: user.bio || '',
                    accountCategory: Number(user.accountCategory) || 1, // Конвертація accountCategory у число
                }}
                validationSchema={validationSchema}
                onSubmit={handleProfileSubmit}
                enableReinitialize
            >
                {({ setFieldValue }) => (
                    <Form className="profile-form">
                        <div>
                            <label htmlFor="firstName">First Name</label>
                            <Field name="firstName" type="text" />
                            <ErrorMessage name="firstName" component="div" className="error" />
                        </div>

                        <div>
                            <label htmlFor="lastName">Last Name</label>
                            <Field name="lastName" type="text" />
                            <ErrorMessage name="lastName" component="div" className="error" />
                        </div>

                        <div>
                            <label htmlFor="birthDate">Birth Date</label>
                            <Field name="birthDate" type="date" />
                            <ErrorMessage name="birthDate" component="div" className="error" />
                        </div>

                        <div>
                            <label htmlFor="country">Country</label>
                            <Field as="select" name="country" onChange={(e: ChangeEvent<HTMLSelectElement>) => {
                                const countryId = e.target.value;
                                setSelectedCountryId(countryId);
                                setFieldValue('country', countryId);
                            }}>
                                {countries.map(country => (
                                    <option key={country.id} value={country.id}>{country.name}</option>
                                ))}
                            </Field>
                        </div>

                        <div>
                            <label htmlFor="state">State</label>
                            <Field as="select" name="stateId" value={selectedStateId || ''} onChange={(e: ChangeEvent<HTMLSelectElement>) => {
                                setSelectedStateId(e.target.value);
                                setFieldValue('stateId', e.target.value);
                            }}>
                                {states.map(state => (
                                    <option key={state.id} value={state.id}>{state.name}</option>
                                ))}
                            </Field>
                            <ErrorMessage name="stateId" component="div" className="error" />
                        </div>

                        <div>
                            <label htmlFor="bio">Bio</label>
                            <Field as="textarea" name="bio" />
                            <ErrorMessage name="bio" component="div" className="error" />
                        </div>

                        <div>
                            <label htmlFor="accountCategory">Account Category</label>
                            <Field as="select" name="accountCategory" onChange={(e: ChangeEvent<HTMLSelectElement>) => {
                                setFieldValue('accountCategory', Number(e.target.value)); // Конвертуємо string у number
                            }}>
                                {accountCategories.map(category => (
                                    <option key={category.id} value={category.id}>{category.name}</option>
                                ))}
                            </Field>
                            <ErrorMessage name="accountCategory" component="div" className="error" />
                        </div>

                        <div>
                            <label htmlFor="profilePhoto">Profile Photo</label>
                            <input type="file" onChange={handlePhotoChange} />
                        </div>

                        <AuthSubButton buttonText={"Update Profile"} />
                    </Form>
                )}
            </Formik>
        </div>
    );
};


export default EditProfilePage;