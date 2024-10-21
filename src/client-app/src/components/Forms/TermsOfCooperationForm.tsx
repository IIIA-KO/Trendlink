import { Formik, Form, Field, FieldArray, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import {
    createTermsAndConditions,
    getTermsAndConditions,
    updateTermsAndConditions
} from "../../services/termsofcooperation";
import {TermsAndConditionsType} from "../../types/TermsAndConditionsType";
import {useEffect, useState} from "react";
import addIcon from "../../assets/navigation-plus.svg"
import removeIcon from "../../assets/icons/remove-icon.svg"
import {
    createAdvertisement,
    deleteAdvertisement,
    updateAdvertisement
} from "../../services/advertisements";
import {useNavigate} from "react-router-dom";


const TermsOfCooperationForm: React.FC = () => {
    const [termsData, setTermsData] = useState<Omit<TermsAndConditionsType, 'userId'> | null>(null);
    const [isEditing, setIsEditing] = useState(false);
    const [adsToDelete, setAdsToDelete] = useState<string[]>([]);

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
                await updateTermsAndConditions({ description: values.description });

                await Promise.all(
                    values.advertisements.map(async (advertisement) => {
                        if (advertisement.id) {
                            await updateAdvertisement(advertisement.id, advertisement);
                        } else {
                            await createAdvertisement(advertisement);
                        }
                    })
                );

                await Promise.all(
                    adsToDelete.map(async (adId) => {
                        await deleteAdvertisement(adId);
                    })
                );

            } else {
                const response = await createTermsAndConditions({ description: values.description });

                await Promise.all(
                    values.advertisements.map(async (advertisement) => {
                        await createAdvertisement(advertisement);
                    })
                );
            }

        } catch (error) {
            console.error('Error saving terms and advertisements:', error);
        }
    };

    const handleRemoveAd = (index: number, adId: string) => {
        // Додаємо до списку для видалення
        if (adId) {
            setAdsToDelete((prev) => [...prev, adId]);
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
            {({ values, setFieldValue }) => (
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
                                                        className="border border-gray-10 rounded-[10px] flex items-center justify-center p-2">
                                                        <Field
                                                            name={`advertisements.${index}.name`}
                                                            className="form-control focus:outline-none h-full w-full"
                                                            placeholder="Enter advertisement name"
                                                            disabled={!!values.advertisements[index].id}
                                                            title={values.advertisements[index].name}
                                                        />
                                                    </div>
                                                    <ErrorMessage name={`advertisements.${index}.name`} component="div"
                                                                  className="text-red-500 text-[14px]"/>
                                                </div>
                                                <div className="flex flex-col w-3/12">
                                                    <label htmlFor="description"
                                                           className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Price</label>
                                                    <div
                                                        className="border border-gray-10 rounded-[10px] flex items-center justify-center p-2">
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
                                                        className="border border-gray-10 rounded-[10px] flex items-center justify-center p-2">
                                                        <Field
                                                            as="select"
                                                            name={`advertisements.${index}.priceCurrency`}
                                                            className="form-control focus:outline-none h-full w-full bg-transparent"
                                                        >
                                                            <option value="USD" className="text-center">$</option>
                                                            <option value="EUR" className="text-center">€</option>
                                                            <option value="UAH" className="text-center">₴</option>
                                                        </Field>
                                                    </div>
                                                    <ErrorMessage name={`advertisements.${index}.priceCurrency`}
                                                                  component="div" className="text-red-500 text-[14px]"/>
                                                </div>
                                                <div className="flex flex-col w-4/12">
                                                    <label htmlFor="description"
                                                           className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Advertisement
                                                        description</label>
                                                    <div
                                                        className="border border-gray-10 rounded-[10px] flex items-center justify-center p-2">
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
                                                <div className="flex justify-center items-center mt-6">
                                                    <button
                                                        type="button"
                                                        onClick={() => {
                                                            handleRemoveAd(index, values.advertisements[index].id);
                                                            remove(index);
                                                        }}
                                                        className="text-red-500"
                                                    >
                                                        <img src={removeIcon} alt="remove icon button" className="w-8 h-8 transition duration-500 ease-in-out hover:border-hover hover:scale-110 active:scale-90"/>
                                                    </button>
                                                </div>
                                            </div>
                                        ))}
                                        <div className="flex justify-center items-center">
                                            <button
                                                type="button"
                                                onClick={() => push({
                                                    id: '',
                                                    name: '',
                                                    priceAmount: 0,
                                                    priceCurrency: '',
                                                    description: ''
                                                })}
                                                className="text-blue-500 mt-4"
                                            >
                                                <img src={addIcon} alt="add icon button" className="w-8 h-8 transition duration-500 ease-in-out hover:border-hover hover:scale-110 active:scale-90"/>
                                            </button>
                                        </div>
                                        <div className="my-8 flex justify-center items-center">
                                            <button type="submit"
                                                    className="w-1/2 h-full text-center bg-primary text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform">
                                                <p className="m-2">Save changes</p>
                                            </button>
                                        </div>
                                    </div>
                                )}
                            </FieldArray>
                        </div>

                        <div className="flex flex-col w-1/3">
                            <label htmlFor="description"
                                   className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Description</label>
                            <div
                                className="border border-gray-10 rounded-[10px] flex items-center justify-center p-8">
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


                </Form>
            )}
        </Formik>
    );
};

export default TermsOfCooperationForm;