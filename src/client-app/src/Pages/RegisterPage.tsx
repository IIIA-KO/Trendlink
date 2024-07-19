import React, {SyntheticEvent, useState} from "react";
import {Redirect} from "react-router-dom";

const RegisterPage = () => {
    const [firstname, setFirstName] = useState('')
    const [lastname, setLastName] = useState('')
    const [birthdate, setBirthdate] = useState('')
    const [email, setEmail] = useState('')
    const [phone, setPhone] = useState('')
    const [password, setPassword] = useState('')
    const [redirect, setRedirect] = useState<boolean>(false);

    const submit = async (e : SyntheticEvent) => {
        e.preventDefault();

        const response = await fetch("http://localhost:5001/users/register", {
            method: 'POST',
            body: JSON.stringify({
                firstname,
                lastname,
                birthdate,
                email,
                phone,
                password
            })
        });

        const content = await response.json();
        console.log(content)

        setRedirect(true);
    }

    if (redirect) {
        return <Redirect to="/login/"/>;
    }

    return(
        <form>
            <input id="inputFirstName" placeholder="firstname" onChange={e => setFirstName(e.target.value)} required />
            <input id="inputLastName" placeholder="lastname" onChange={e => setLastName(e.target.value)} required/>
            <input type="date" id="inputBirthDate" placeholder="birthDate" onChange={e => setBirthdate(e.target.value)} required/>
            <input type="email" id="inputEmail" placeholder="e-mail" onChange={e => setEmail(e.target.value)} required/>
            <input type="tel" pattern="[0-9]{3}-[0-9]{3}-[0-9]{2}-[0-9]{2}" id="inputPhoneNumber" placeholder="phoneNumber" onChange={e => setPhone(e.target.value)} required/>
            <input type="password" id="inputePassword" placeholder="password" onChange={e => setPassword(e.target.value)} required/>
            <button type="submit">Регестрація</button>
        </form>
    )
};

export default RegisterPage;