import { Formik, Form, Field, FieldArray, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import {
    createTermsAndConditions,
    getTermsAndConditions,
    updateTermsAndConditions
} from "../../services/termsofcooperation";
import {TermsAndConditionsType} from "../../types/TermsAndConditionsType";
import {useEffect, useState} from "react";
import plus from "../../assets/navigation-plus.svg"
import {createAdvertisement, updateAdvertisement} from "../../services/advertisements";
import {useNavigate} from "react-router-dom";


const TermsOfCooperationForm: React.FC = () => {
    const navigate = useNavigate();
    const [termsData, setTermsData] = useState<Omit<TermsAndConditionsType, 'userId'> | null>(null);
    const [isEditing, setIsEditing] = useState(false);

    useEffect(() => {
        const fetchTerms = async () => {
            try {
                const data = await getTermsAndConditions();
                if (data) {
                    setTermsData(data);
                    setIsEditing(true);
                } else {
                    setTermsData({
                        id: '',
                        description: '',
                        advertisements: [
                            {
                                id: '',
                                name: '',
                                priceAmount: 0,
                                priceCurrency: '',
                                description: ''
                            }
                        ]
                    });
                }
            } catch (error) {
                console.error("Error fetching terms and conditions:", error);
            }
        };

        fetchTerms();
    }, []);

    const advertisementSchema = Yup.object({
        name: Yup.string().required('Required'),
        priceAmount: Yup.number().required('Required').positive('Must be a positive number'),
        priceCurrency: Yup.string().required('Required'),
        description: Yup.string().required('Required')
    });

    const validationSchema = Yup.object({
        description: Yup.string().required('Required'),
        advertisements: Yup.array().of(advertisementSchema)
    });

    const handleSubmit = async (values: Omit<TermsAndConditionsType, 'userId'>) => {
        try {
            if (isEditing) {
                await updateTermsAndConditions(values.id, { description: values.description });

                await Promise.all(
                    values.advertisements.map(async (advertisement) => {
                        if (advertisement.id) {
                            await updateAdvertisement(advertisement.id, advertisement);
                        } else {
                            await createAdvertisement(advertisement);
                        }
                    })
                );

                console.log('Terms and advertisements updated:', values);
            } else {
                const response = await createTermsAndConditions({ description: values.description });
                console.log('Terms created:', response);

                await Promise.all(
                    values.advertisements.map(async (advertisement) => {
                        await createAdvertisement(advertisement);
                    })
                );
            }

            navigate('/');
        } catch (error) {
            console.error('Error saving terms and advertisements:', error);
        }
    };

    if (!termsData) {
        return <div>Loading...</div>;
    }

    return (
        <Formik
            initialValues={termsData}
            validationSchema={validationSchema}
            onSubmit={handleSubmit}
            enableReinitialize={true}
        >
            {({ values }) => (
                <Form className="w-[calc(100%-90px)] h-auto flex flex-col mt-12">
                    <div className="w-full h-full flex flex-row">
                        <div className="flex flex-row w-2/3">
                            <FieldArray name="advertisements">
                                {({push, remove}) => (
                                    <div>
                                        {values.advertisements.map((_, index) => (
                                            <div key={index}
                                                 className="advertisement-section flex flex-row justify-center items-center gap-4">
                                                <div className="flex flex-col w-8/12">
                                                    <label htmlFor="description"
                                                           className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Types
                                                        of services</label>
                                                    <div
                                                        className="border border-transparent rounded-[10px] flex items-center justify-center p-2">
                                                        <Field
                                                            name={`advertisements.${index}.name`}
                                                            className="form-control focus:outline-none h-full w-full"
                                                            placeholder="Enter advertisement name"
                                                        />
                                                    </div>
                                                    <ErrorMessage name={`advertisements.${index}.name`} component="div"
                                                                  className="text-red-500 text-[14px]"/>
                                                </div>
                                                <div className="flex flex-col w-3/12">
                                                    <label htmlFor="description"
                                                           className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Price</label>
                                                    <div
                                                        className="border border-transparent rounded-[10px] flex items-center justify-center p-2">
                                                        <Field
                                                            name={`advertisements.${index}.priceAmount`}
                                                            type="number"
                                                            className="form-control focus:outline-none h-full w-full"
                                                            placeholder="Enter price amount"
                                                        />
                                                    </div>
                                                    <ErrorMessage name={`advertisements.${index}.priceAmount`}
                                                                  component="div" className="text-red-500 text-[14px]"/>
                                                </div>
                                                <div className="flex flex-col w-3/12">
                                                    <label htmlFor="description"
                                                           className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Price
                                                        Currency</label>
                                                    <div
                                                        className="border border-transparent rounded-[10px] flex items-center justify-center p-2">
                                                        <Field
                                                            name={`advertisements.${index}.priceCurrency`}
                                                            className="form-control focus:outline-none h-full w-full"
                                                            placeholder="Enter price currency (e.g., USD)"
                                                        />
                                                    </div>
                                                    <ErrorMessage name={`advertisements.${index}.priceCurrency`}
                                                                  component="div" className="text-red-500 text-[14px]"/>
                                                </div>
                                                <div className="flex flex-col w-4/12">
                                                    <label htmlFor="description"
                                                           className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Advertisement
                                                        description</label>
                                                    <div
                                                        className="border border-transparent rounded-[10px] flex items-center justify-center p-2">
                                                        <Field
                                                            as="textarea"
                                                            name={`advertisements.${index}.description`}
                                                            className="form-control focus:outline-none h-full w-full"
                                                            placeholder="Enter advertisement description"
                                                        />
                                                    </div>
                                                    <ErrorMessage name={`advertisements.${index}.description`}
                                                                  component="div" className="text-red-500 text-[14px]"/>
                                                </div>
                                                <div className="flex justify-between">
                                                    <button
                                                        type="button"
                                                        onClick={() => remove(index)}
                                                        className="text-red-500"
                                                    >
                                                        Remove
                                                    </button>
                                                    <button
                                                        type="button"
                                                        onClick={() => push({
                                                            id: '',
                                                            name: '',
                                                            priceAmount: 0,
                                                            priceCurrency: '',
                                                            description: ''
                                                        })}
                                                        className="text-blue-500"
                                                    >
                                                        Add Advertisement
                                                    </button>
                                                </div>
                                            </div>
                                        ))}
                                    </div>
                                )}
                            </FieldArray>
                        </div>

                        <div className="flex flex-col w-1/3">
                            <label htmlFor="description"
                                   className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Description</label>
                            <div
                                className="border border-transparent rounded-[10px] flex items-center justify-center p-8">
                                <Field
                                    as="textarea"
                                    name="description"
                                    className="form-control relative focus:outline-none w-full h-full resize-none-indicator"
                                    placeholder="Enter terms description"
                                />
                            </div>
                            <ErrorMessage name="description" component="div" className="text-red-500"/>
                        </div>
                    </div>

                    <div className="my-8">
                        <button type="submit"
                                className="w-1/2 h-full text-center bg-primary text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform">
                            <p className="m-2">Save changes</p>
                        </button>
                    </div>
                </Form>
            )}
        </Formik>
    );
};


export default TermsOfCooperationForm;