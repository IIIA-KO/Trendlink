import { instagramClientId, instagramRedirectUri, instagramConfigId, instagramScope, instagramResponseType } from "../utils/constants";
import UniversalButton from "../components/Buttons/UniversalButton";

const LinkInstagramPage: React.FC = () => {
    const handleInstagramLink = () => {
        const authUrl = `https://www.facebook.com/v20.0/dialog/oauth?client_id=${instagramClientId}&redirect_uri=${instagramRedirectUri}&scope=${instagramScope}&response_type=${instagramResponseType}&config_id=${instagramConfigId}`;
        window.location.href = authUrl;
    };

    return (
        <div className="flex flex-col items-center justify-center h-screen bg-gray-50 p-6">
            <h1 className="text-2xl font-inter font-bold text-main-black mb-4">
                Connect your Professional Business or Creator Instagram Account with Facebook Pages
            </h1>
            <div className="h-auto w-1/2 justify-center text-left font-inter text-main-black my-8">
                <ul className="list-disc space-y-2">
                    <li>
                        <p>
                            Your Instagram Account must have at least 100 followers.
                        </p>
                    </li>
                    <li>
                        <p>
                            Instagram accounts must be connected to your Facebook Business Page and you must be the
                            owner or
                            admin of the Facebook Business Page.
                        </p>
                    </li>
                    <li>
                        <p>
                            Please select <b>only one</b> Facebook Business Page and <b>only one</b> Instagram account
                            that is
                            connected to it.
                        </p>
                    </li>
                </ul>
            </div>
            <div className="flex flex-col w-1/2">
                <UniversalButton buttonText={"Link Instagram"} onClick={handleInstagramLink}/>
            </div>
        </div>
    );
}

export default LinkInstagramPage;