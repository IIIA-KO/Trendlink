import {SyntheticEvent, useState} from "react";

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const submit = async (e : SyntheticEvent) => {
        e.preventDefault();

        await fetch("http://localhost:5001/users/login", {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
            body: JSON.stringify({
                email,
                password
            })
        })
    }

    return (
        <section className="LoginPage">
            <h2>TrendLink</h2>
            <h1>Мережа для успішної співпраці з блогерами</h1>
            <form onSubmit={submit}>
                <input type="email" id="inputEmail" placeholder="e-mail" required onChange={ e => setEmail(e.target.value) } />
                <input type="password" id="inputePassword" placeholder="password" required onChange={ e => setPassword(e.target.value) } />
                <button type="submit">Увійти</button>
            </form>
        </section>
    );
}

export default LoginPage;