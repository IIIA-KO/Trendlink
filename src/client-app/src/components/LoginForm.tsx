import { useNavigate } from 'react-router-dom';
import { login } from '../api/authApi';
import useAuth from '../hooks/useAuth';
import axios from "axios";
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';

const LoginForm: React.FC = () => {

    const { login: authLogin } = useAuth();
    const navigate = useNavigate();

    //const toggleForm = () => setIsLogin(!isLogin);

    const initialValues = {
        email: '',
        password: '',
    };

    const validationSchema = Yup.object({
        email: Yup.string().email('Invalid email address').required('Required'),
        password: Yup.string().required('Required'),
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

    const getFieldClasses = (touched: boolean | undefined, error: string | undefined) => {
        return `w-full px-4 py-2 border-b-2 focus:outline-none ${touched && error ? 'border-red-500' : 'border-gray-300 focus:border-blue-500'}`;
    };

    return (
        <Formik
            initialValues={initialValues}
            validationSchema={validationSchema}
            onSubmit={handleSubmit}
        >

            {({touched, errors }) => (
            <Form className="space-y-6">
                <div className="text-[0.85rem]">
                    <Field
                        name="email"
                        type="email"
                        placeholder="E-mail"
                        className={getFieldClasses(touched.email, errors.email)}
                    />
                    <ErrorMessage
                        name="email"
                        component="div"
                        className="text-red-500 text-xs mt-1"
                    />
                </div>
                <div className="text-[0.85rem]">
                    <Field
                        name="password"
                        type="password"
                        placeholder="Password"
                        className={getFieldClasses(touched.password, errors.password)}
                    />
                    <ErrorMessage
                        name="password"
                        component="div"
                        className="text-red-500 text-xs mt-1"
                    />
                </div>
                <div className="flex items-center text-[0.75rem]">
                    <a className="ml-auto transition-transform duration-250 ease-in-out hover:scale-105 transform-origin-center">Forgot your password?</a>
                </div>
                <div className="flex justify-between items-center">
                    <button
                        type="submit"
                        className="w-full h-[47px] py-2 bg-gray-800 text-white text-[1rem] mt-4 rounded-full hover:bg-gray-700 transition-colors"
                    >
                        Login
                    </button>
                </div>
            </Form>
                )}
        </Formik>
    );
};

export default LoginForm;