import React, { useState } from 'react';
import { Field, ErrorMessage, useFormikContext, FormikTouched, FormikErrors } from 'formik';
import {UserType} from "../../models/UserType.ts";

const EmailInputFiled: React.FC = () => {
    const { touched, errors, setFieldValue } = useFormikContext<UserType>();

    const [email, setEmail] = useState('');

    const handleClear = () => {
        setEmail('');
        setFieldValue('email', ''); // Очищаем значение в Formik
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setEmail(e.target.value);
        setFieldValue('email', e.target.value);
    };

    const getFieldClasses = (
        touched: FormikTouched<UserType>['email'],
        error: FormikErrors<UserType>['email']
    ) => {
        const baseClasses = "bg-transparent w-full px-4 py-2 border-b-2 focus:outline-none";
        return touched && error ? `${baseClasses} border-red-500` : `${baseClasses} border-gray-300 focus:border-blue-500`;
    };

    return (
        <div className="relative text-[0.85rem]">
            <Field
                name="email"
                type="email"
                placeholder="E-mail"
                value={email}
                onChange={handleChange}
                className={getFieldClasses(touched.email, errors.email)}
            />
            {email && (
                <button
                    type="button"
                    onClick={handleClear}
                    className="absolute right-0 top-0 mt-3 mr-4 text-gray-300 hover:text-gray-600 focus:outline-none"
                >
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        className="h-5 w-5"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            strokeWidth={2}
                            d="M6 18L18 6M6 6l12 12"
                        />
                    </svg>
                </button>
            )}
            <ErrorMessage
                name="email"
                component="div"
                className="text-red-500 text-xs mt-1 px-4"
            />
        </div>
    );
};

export default EmailInputFiled;