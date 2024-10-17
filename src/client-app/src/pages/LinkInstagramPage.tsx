import React, { useEffect } from 'react';
import { instagramClientId, instagramRedirectUri, instagramConfigId, instagramScope, instagramResponseType } from "../utils/constants";

const LinkInstagramPage: React.FC = () => {

    const handleInstagramLink = () => {
        const authUrl = `https://www.facebook.com/v20.0/dialog/oauth?client_id=${instagramClientId}&redirect_uri=${instagramRedirectUri}&scope=${instagramScope}&response_type=${instagramResponseType}&config_id=${instagramConfigId}`;
        window.location.href = authUrl;
    };

    console.log(localStorage.getItem('isInstagramLinked'));

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gray-50 p-6">
            <h1 className="text-2xl font-bold text-gray-800 mb-4">
                Connect your Professional Business or Creator Instagram Account with Facebook Pages
            </h1>
            <p className="text-center text-gray-600 mb-2">
                Your Instagram Account must have at least 100 followers.
            </p>
            <p className="text-center text-gray-600 mb-2">
                Instagram accounts must be connected to your Facebook Business Page and you must be the owner or admin of the Facebook Business Page.
            </p>
            <p className="text-center text-gray-600 mb-4">
                Please select <b>only one</b> Facebook Business Page and <b>only one</b> Instagram account that is connected to it.
            </p>
            <hr className="my-4 w-full border-gray-300" />
            <div className="flex flex-col space-y-2">
                <button
                    onClick={handleInstagramLink}
                    className="px-6 py-3 text-white bg-blue-600 rounded-lg shadow hover:bg-blue-700 transition duration-200"
                >
                    Link Instagram
                </button>
            </div>
        </div>
    );
}

export default LinkInstagramPage;