import React from 'react';

import RegisterForm from "../components/RegisterForm.tsx";
//import { Formik, Field, Form, ErrorMessage } from 'formik';
//import * as Yup from 'yup';


const RegisterPage: React.FC = () => {
    return (
        <div>
            <h1>Register</h1>
            <RegisterForm />
        </div>
    );
};


export default RegisterPage;