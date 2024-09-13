import {Field, ErrorMessage, useFormikContext, FormikTouched, FormikErrors} from 'formik';
import {useState} from "react";
import {UserType} from "../../models/UserType";

const PasswordInputField = () => {
    const [showPassword, setShowPassword] = useState(false);
    const { touched, errors } = useFormikContext<UserType>();

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
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
                name="password"
                type={showPassword ? 'text' : 'password'}
                placeholder="Password"
                className={getFieldClasses(touched.password, errors.password)}
            />
            <button
                type="button"
                className="absolute right-0 top-0 mt-2 mr-4 text-gray-300 hover:text-gray-600 focus:outline-none"
                onClick={togglePasswordVisibility}
            >
                {showPassword ? (
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={2}
                        stroke="currentColor"
                        className="h-5 w-5"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M21 12c0 1.2-4.03 6-9 6s-9-4.8-9-6c0-1.2 4.03-6 9-6s9 4.8 9 6Z"
                        />
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                        />
                    </svg>
                ) : (
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        strokeWidth={2}
                        stroke="currentColor"
                        className="h-5 w-5"
                    >
                        <path
                            strokeLinecap="round"
                            strokeLinejoin="round"
                            d="M3.933 13.909A4.357 4.357 0 0 1 3 12c0-1 4-6 9-6m7.6 3.8A5.068 5.068 0 0 1 21 12c0 1-3 6-9 6-.314 0-.62-.014-.918-.04M5 19 19 5m-4 7a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                        />
                    </svg>
                )}
            </button>
            <ErrorMessage
                name="password"
                component="div"
                className="text-red-500 text-xs mt-1 px-4"
            />
        </div>
    );
};

export default PasswordInputField;