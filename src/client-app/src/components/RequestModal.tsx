import React, {useEffect, useState} from "react";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import {requestCooperation} from "../services/cooperations";
import {RequestCooperationType} from "../types/RequestCooperationType";
import {AdvertisementsType} from "../types/AdvertisementsType";
import successfullyCircle from "../assets/successfully-circle.svg"

interface RequestModalProps {
    advertisements: AdvertisementsType[];
    isOpen: boolean;
    onClose: () => void;
}

const RequestModal: React.FC<RequestModalProps> = ({ advertisements, isOpen, onClose }) => {

    const [isSubmitted, setIsSubmitted] = useState(false);

    useEffect(() => {
        if (isOpen) {
            setIsSubmitted(false);
        }
    }, [isOpen]);

    if (!isOpen) return null;

    const initialValues: RequestCooperationType = {
        advertisementId: "",
        scheduledOnUtc: "",
        name: "",
        description: ""
    };

    const validationSchema = Yup.object({
        advertisementId: Yup.string().required("Please select an advertisement"),
        scheduledOnUtc: Yup.string().required("Required"),
        name: Yup.string().required("Required"),
        description: Yup.string().required("Required")
    });

    const handleSubmit = async (values: RequestCooperationType) => {
        const response = await requestCooperation(values);
        if (response) {
            setIsSubmitted(true);
        }
    };

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
            <div className="flex flex-col justify-center  bg-whitesmoke-100 p-8 rounded-[20px] w-[60%] h-[60%] shadow-lg">
                {isSubmitted ? (
                    <div className="flex flex-col justify-center items-center gap-4 text-center">
                        <img alt="successfully circle" src={successfullyCircle} />
                        <h2 className="text-2xl font-semibold mb-4">Request sent successfully!</h2>
                        <button
                            onClick={onClose}
                            className="mt-8 px-8 py-2 bg-primary text-center text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                        >
                            Return
                        </button>
                    </div>
                ) : (
                    <>
                        <h2 className="text-[20px] font-inter font-regular mb-4">Advertising order</h2>
                        <Formik
                            initialValues={initialValues}
                            validationSchema={validationSchema}
                            onSubmit={handleSubmit}
                        >
                            {({isSubmitting}) => (
                                <Form className="flex flex-col gap-32">
                                    <div className="flex flex-row">
                                        <div className="w-1/2 mr-32">
                                            <div className="mb-4">
                                                <label
                                                    className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Select
                                                    advertising
                                                </label>
                                                <Field
                                                    as="select"
                                                    name="advertisementId"
                                                    className="bg-textPrimary rounded-[8px] p-2 w-full"
                                                >
                                                    <option value="" label="Оберіть рекламу"/>
                                                    {advertisements.map((ad) => (
                                                        <option key={ad.id} value={ad.id}>
                                                            {ad.name} - {ad.priceAmount} {ad.priceCurrency}
                                                        </option>
                                                    ))}
                                                </Field>
                                                <ErrorMessage name="advertisementId" component="div"
                                                              className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2"/>
                                            </div>

                                            <div className="mb-4">
                                                <label
                                                    className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Name
                                                </label>
                                                <Field
                                                    type="text"
                                                    name="name"
                                                    className="bg-textPrimary rounded-[8px] p-2 w-full"
                                                />
                                                <ErrorMessage name="name" component="div"
                                                              className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2"/>
                                            </div>
                                            <div className="mb-4">
                                                <label
                                                    className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Description</label>
                                                <Field
                                                    as="textarea"
                                                    name="description"
                                                    className="bg-textPrimary rounded-[8px] p-2 w-full"
                                                />
                                                <ErrorMessage name="description" component="div"
                                                              className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2"/>
                                            </div>
                                        </div>
                                        <div className="w-1/2">
                                            <div className="mb-4">
                                                <label
                                                    className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2">Date
                                                    and time</label>
                                                <Field
                                                    type="datetime-local"
                                                    name="scheduledOnUtc"
                                                    className="bg-textPrimary rounded-[8px] p-2 w-full"
                                                />
                                                <ErrorMessage name="scheduledOnUtc" component="div"
                                                              className="font-inter inter-light-italic text-[12px] text-text-2 my-1 ml-2"/>
                                            </div>
                                        </div>
                                    </div>

                                    <div className="flex justify-center gap-12">
                                        <button
                                            type="button"
                                            onClick={onClose}
                                            className="px-8 py-2 bg-red-600 text-center text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-red-400 hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                                        >
                                            Cancel
                                        </button>
                                        <button
                                            type="submit"
                                            disabled={isSubmitting}
                                            className="px-8 py-2 bg-primary text-center text-textPrimary text-[1rem] rounded-full transition duration-500 ease-in-out hover:bg-hover hover:border-hover hover:scale-105 active:scale-90 active:bg-transparent active:border-primary active:text-textSecondary focus:scale-100 transform"
                                        >
                                            Send
                                        </button>
                                    </div>
                                </Form>
                            )}
                        </Formik>
                    </>
                )}
            </div>
        </div>
    );
};

export default RequestModal;