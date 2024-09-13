import { useNavigate } from 'react-router-dom';
import { login } from '../../api/authApi';
import useAuth from '../../hooks/useAuth';
import axios from "axios";
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import PasswordInputField from "../Inputs/PasswordInputField";
import EmailInputFiled from "../Inputs/EmailInputField";
import AuthSubButton from "../Buttons/AuthSubButton";

const LoginForm: React.FC = () => {

    const { login: authLogin } = useAuth();
    const navigate = useNavigate();

    const initialValues = {
        email: '',
        password: '',
    };

    const validationSchema = Yup.object({
        email: Yup.string()
            .email('Invalid email format')
            .required('Required field'),

        password: Yup.string()
            .required('Required field')
            .min(8, 'Password length must be at least 8.')
            .max(16, 'Password length must not exceed 16.')
    });

    const handleSubmit = async (values: { email: string; password: string }) => {
        try {
            const userData = await login(values);
            authLogin(userData);
            navigate('/');
        } catch (error: unknown) {
            if (axios.isAxiosError(error) && error.response) {
                console.error('Authentication error:', error.response.data);
            } else {
                console.error('Network/Server error:', error instanceof Error ? error.message : 'Unknown error');
            }
        }
    };

    return (
        <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={handleSubmit}
        >

                <Form className="space-y-6">

                    <EmailInputFiled/>

                    <PasswordInputField/>

                    <div className="-mt-4 flex items-center text-[0.75rem]">
                        <a className="ml-auto font-inter font-light transition-transform duration-400 ease-in-out hover:text-blue-700 hover:scale-105 transform-origin-center">
                            Forgot your password?
                        </a>
                    </div>

                    <div className="flex justify-between items-center">
                        <AuthSubButton buttonText="Login" />
                    </div>
                </Form>
        </Formik>
    );
};

export default LoginForm;