import React, { ChangeEvent, useEffect, useState } from 'react';
import * as Yup from 'yup';
import { Formik, Field, Form, ErrorMessage, FormikHelpers } from 'formik';
import { useNavigate } from 'react-router-dom';
import { useProfile } from '../../hooks/useProfile';
import { useUser } from '../../hooks/useUser';
import { getCountries, getStates } from '../../services/countriesAndStates';
import { CountryType } from "../../types/CountryType";
import { StateType } from "../../types/StateType";
import { ProfileType } from '../../types/ProfileType';
import AuthSubButton from '../../components/Buttons/AuthSubButton';
import Modal from '../../components/Modal';
import { accountCategories } from '../../utils/constants'


const ProfileForm: React.FC = () => {
    const navigate = useNavigate();
    const { updateProfileData, uploadPhoto } = useProfile();
    const { user } = useUser();
    const [countries, setCountries] = useState<CountryType[]>([]);
    const [states, setStates] = useState<StateType[]>([]);
    const [selectedCountryId, setSelectedCountryId] = useState<string | undefined>(undefined);
    const [selectedStateId, setSelectedStateId] = useState<string | undefined>(undefined);
    const [photoFile, setPhotoFile] = useState<File | null>(null);
    const [previewSrc, setPreviewSrc] = useState<string | null>(null);
    const [isModalOpen, setIsModalOpen] = useState(false);

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
            const updatedValues = {
                ...values,
                accountCategory: Number(values.accountCategory),
            };

            await updateProfileData(updatedValues);
            if (photoFile) {
                await uploadPhoto(photoFile);
            }
            navigate('/');
        } catch (error) {
            console.error('Failed to update profile', error);
        } finally {
            actions.setSubmitting(false);
        }
    };

    const handlePhotoChange = (file: File) => {
        setPhotoFile(file);

        const reader = new FileReader();
        reader.onloadend = () => {
            setPreviewSrc(reader.result as string);
            setIsModalOpen(true);
        };
        reader.readAsDataURL(file);
    };

    const handleFileInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files[0]) {
            handlePhotoChange(e.target.files[0]);
        }
    };

    const handleDrop = (e: React.DragEvent) => {
        e.preventDefault();
        if (e.dataTransfer.files && e.dataTransfer.files[0]) {
            handlePhotoChange(e.dataTransfer.files[0]);
        }
    };

    const handleDragOver = (e: React.DragEvent) => {
        e.preventDefault();
    };

    const handleModalConfirm = () => {
        setIsModalOpen(false);
    };

    const handleModalDiscard = () => {
        setPhotoFile(null);
        setPreviewSrc(null);
        setIsModalOpen(false);
    };

    const handleCancel = () => {
        navigate(-1);
    };

    if (!user || !selectedCountryId || !selectedStateId) return <div>Loading...</div>;

    return (
        <Formik
            initialValues={{
                firstName: user.firstName,
                lastName: user.lastName,
                birthDate: user.birthDate,
                stateId: selectedStateId,
                bio: user.bio || '',
                accountCategory: Number(user.accountCategory) || 1,
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
                            setFieldValue('accountCategory', Number(e.target.value));
                        }}>
                            {accountCategories.map(category => (
                                <option key={category.id} value={category.id}>{category.name}</option>
                            ))}
                        </Field>
                        <ErrorMessage name="accountCategory" component="div" className="error" />
                    </div>

                    <div>
                        <label htmlFor="profilePhoto">Profile Photo</label>
                        <div
                            className="drag-drop-area"
                            onDrop={handleDrop}
                            onDragOver={handleDragOver}
                            style={{border: '2px dashed #ccc', padding: '20px', textAlign: 'center'}}
                        >
                            <p>Drag and drop a file here, or click to select a file</p>
                            <input type="file" onChange={handleFileInputChange} style={{display: 'none'}}/>
                        </div>
                    </div>

                    {isModalOpen && (
                        <Modal onClose={handleModalDiscard}>
                            <div className="h-full w-full flex items-center">
                                <img
                                    src={previewSrc || user?.profilePhotoUri || '/path/to/default/photo'}
                                    alt="Profile avatar"
                                    className="rounded-full sm:w-14 md:w-16 lg:w-20 xl:w-24 2xl:w-28 sm:h-14 md:h-16 lg:h-20 xl:h-24 2xl:h-28"
                                />
                            </div>
                            <div className="flex justify-between mt-4">
                                <button
                                    onClick={handleModalConfirm}
                                    className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                                >
                                    Keep this photo
                                </button>
                                <button
                                    onClick={handleModalDiscard}
                                    className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600"
                                >
                                    Discard
                                </button>
                            </div>
                        </Modal>
                    )}

                    <div className="button-group">
                        <AuthSubButton buttonText={"Save Changes"}/>
                        <button type="button" onClick={handleCancel}>Cancel</button>
                    </div>
                </Form>
            )}
        </Formik>
    );
};

export default ProfileForm;