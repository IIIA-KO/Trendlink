import noProfile from "../assets/icons/no-profile.svg";
import locationIcon from "../assets/icons/location-icon.svg";
import NavButton from "./Buttons/NavButton";
import phoneIcon from "../assets/icons/telephone-icon.svg";
import emailIcon from "../assets/icons/email-icon.svg";
import {TopBarType} from "../types/TopBarType";

const TopBar: React.FC<TopBarType> = ({ user }) => {

    if (!user) {
        return null;
    }

    return (
        <div className="h-1/4 w-full flex flex-row">
            <div className="h-[300px] w-[120rem] flex flex-col gap-3">
                <div className="h-2/5 w-full flex flex-row mt-4">
                    <div className="h-full w-full flex items-center">
                        <img src={user?.profilePhotoUri || noProfile} alt="Profile avatar" className="rounded-full sm:w-14 md:w-16 lg:w-20 xl:w-24 2xl:w-28 sm:h-14 md:h-16 lg:h-20 xl:h-24 2xl:h-28 sm:ml-6 md:ml-9 lg:ml-7 xl:ml-8 2xl:ml-10" />
                    </div>
                    <div
                        className="h-full w-full flex flex-col sm:gap-2 md:gap-3 lg:gap-3 xl:gap-4 2xl:gap-4 text-left">
                        <div className="mt-2">
                            <p className='font-inter font-bold sm:text-[17px] md:text-[18px] lg:text-[18px] xl:text-[19px] 2xl:text-[19px]'>{user?.firstName} {user?.lastName}</p>
                        </div>
                        <div>
                            <a href={`https://www.instagram.com/${user?.instagramAccountUsername}`}
                                className='font-inter font-regular sm:text-[15px] md:text-[16px] lg:text-[16px] xl:text-[17px] 2xl:text-[17px] text-blue-500'>
                                {user?.instagramAccountUsername}
                            </a>
                        </div>
                    </div>
                    <div className="h-full w-full flex items-start flex-col gap-4">
                        <div className="mt-2">
                            <p className='font-inter font-regular text-main-black sm:text-[15px] md:text-[16px] lg:text-[16px] xl:text-[17px] 2xl:text-[17px]'>{user?.accountCategory}</p>
                        </div>
                        <div>
                            <p className='font-inter font-regular text-main-black sm:text-[15px] md:text-[16px] lg:text-[16px] xl:text-[17px] 2xl:text-[17px] inline-flex items-center'>
                                <img src={locationIcon} alt="Location icon" className="w-5 h-5 mr-1" />
                                {user?.countryName} / {user?.stateName}
                            </p>
                        </div>
                    </div>
                </div>
                <div className="h-auto w-full flex items-center">
                    <p className='font-inter font-regular text-[14px] text-left max-w-[50rem] ml-10'>
                        {user?.bio}
                    </p>
                </div>
                <div className="h-1/5 w-full flex justify-start items-center">
                    <NavButton buttonText={'Edit Profile'} redirectTo={'/editprofile'} width={'w-[250px]'} height={'h-[35px]'} className={'ml-10'} />
                </div>
            </div>
            <div className="h-auto w-full flex flex-col gap-2 justify-center items-start">
                <div className="flex flex-wrap items-center w-full sm:w-auto md:ml-10 lg:ml-60 xl:ml-70 2xl:ml-70">
                    <img src={phoneIcon} className="w-5 h-5 mr-1" alt="Phone Icon" />
                    <p className="text-[16px] font-regular">{user?.phoneNumber}</p>
                </div>
                <div className="flex items-center w-full sm:w-auto md:ml-10 lg:ml-60 xl:ml-70 2xl:ml-70">
                    <img src={emailIcon} className="w-5 h-5 mr-1" alt="Email Icon" />
                    <p className="text-[16px] font-regular">{user?.email}</p>
                </div>
            </div>
        </div>
    )
        ;
};

export default TopBar;