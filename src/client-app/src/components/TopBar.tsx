import noProfile from "../assets/icons/no-profile.svg";
import locationIcon from "../assets/icons/location-icon.svg";
import star from "../assets/star.svg";
import NavButton from "./Buttons/NavButton";
import phoneIcon from "../assets/icons/telephone-icon.svg";
import emailIcon from "../assets/icons/email-icon.svg";
import React from "react";
import {useProfile} from "../hooks/useProfile";

const TopBar: React.FC = () => {

    const { user, loading, error } = useProfile();

    if (loading) {
        return <p>Завантаження...</p>;
    }

    if (error) {
        return <p>{error}</p>;
    }

    return (
        <div className="relative bg-custom-bg bg-no-repeat bg-cover rounded-[40px] ml-40 mr-40 w-full h-[900px]">
            <div className="relative max-w-[calc(100%-72px)] h-1/3 mt-9 mx-auto">
                <div className="absolute top-0 left-0 w-28 h-28 rounded-full">
                    <img src={user?.profilePhotoUri || noProfile} alt="Profile avatar" className="w-28 h-28"/>
                </div>

                <div className="grid grid-cols-[35rem_35rem] items-center ml-36">
                    <p className='font-inter font-bold text-[19px]'>{user?.firstName || 'John'} {user?.lastName || 'Doe'}</p>
                    <p className='font-inter font-regular text-main-black text-[17px]'>{user?.accountCategory || 'Creator'}</p>
                </div>

                <div className="grid grid-cols-[33rem_33rem] items-center ml-36 gap-x-8 mt-2">
                    <p className='font-inter font-regular text-[17px]'>Style for every day</p>
                    <p className='font-inter font-regular text-main-black text-[17px] inline-flex items-center'>
                        <img src={locationIcon} alt="Location icon" className="w-5 h-5 mr-1"/>
                        {user?.countryName || 'UA'} / {user?.stateName || 'Kyiv'}
                    </p>
                </div>

                <div className="grid grid-cols-[36rem] items-center ml-36 mt-2">
                    <p className='font-inter font-regular text-[16px]'>Fashion and style</p>
                </div>

                <div className="flex gap-1 items-center ml-36 mt-2">
                    {[...Array(5)].map((_, index) => (
                        <img key={index} src={star} alt="Star" className="w-6 h-6"/>
                    ))}
                </div>

                <div className="flex mt-6">
                    <p className='font-inter font-regular text-[14px] max-w-[50rem]'>
                        {user?.bio || 'Welcome to the world of style and fashion! I am Natalia, a fashion enthusiast and a passionate lover of trends. Here you will find inspiration for creating unique looks, advice on choosing current outfits and accessories, as well as reviews of your favorites. Subscribe to stay up to date with the latest fashion trends and share your impressions with me!.'}
                    </p>
                </div>

                <div className="absolute bottom-0 left-0">
                    <NavButton buttonText={'Edit Profile'} redirectTo={'/'} width={'w-[250px]'} height={'h-[35px]'}/>
                </div>

                <div className="absolute right-0 flex flex-col items-start mr-8 -mt-28">
                    <div className="flex items-center">
                        <img src={phoneIcon} className="w-5 h-5 mr-1" alt="Phone Icon"/>
                        <p className="text-[16px] font-regular">{user?.phoneNumber || '+123 456 7890'}</p>
                    </div>
                    <div className="flex items-center mt-2">
                        <img src={emailIcon} className="w-5 h-5 mr-1" alt="Email Icon"/>
                        <p className="text-[16px] font-regular">{user?.email || 'email@example.com'}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default TopBar;