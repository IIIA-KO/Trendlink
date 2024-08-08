import { Link } from "react-router-dom";
import { useEffect } from 'react';

const Home = () => {
    useEffect(() => {
        console.log('Home component mounted');
    }, []);

    return (
        <>
            <h1>Trendlink</h1>
            <Link to="/login">
                <button>Login with Google</button>
            </Link>

            <br />

            <Link to="/link-instagram">
                <button>Link Instagram</button>
            </Link>
        </>
    )
};

export default Home;