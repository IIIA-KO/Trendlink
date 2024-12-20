import {NavLink} from "react-router-dom";
import { useState } from "react";
import profileIcon from '../assets/icons/profile-icon.svg';
import statisticsIcon from '../assets/icons/statistics-icon.svg';
import calendarIcon from '../assets/icons/calendar-icon.svg';
import notificationsIcon from '../assets/icons/notifications-icon.svg';
import termsOfCooperationIcon from '../assets/icons/termsOfcooperation-icon.svg';
import logoutIcon from '../assets/icons/Logout-icon.svg';
import searchIcon from '../assets/icons/search-icon.svg';
import logo from '../assets/logo/logo-trendlink-white.svg'

const NavBar: React.FC = () => {
    const [open, setOpen] = useState(false);
    const Menus = [
        { title: "Profile", icon: profileIcon, path: "/" },
        { title: "Search", icon: searchIcon, path: "/searchbloggers" },
        { title: "Statistics", icon: statisticsIcon, path: "/statistics"},
        { title: "Calendar ", icon: calendarIcon, path: "/calendar" },
        { title: "Notifications ", icon: notificationsIcon, path: "/notifications" },
        { title: "Terms of cooperation", icon: termsOfCooperationIcon, path: "/termsofcooperation" },
        { title: "Logout", icon: logoutIcon, path: "/logout" },
    ];

    return (
        <div className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-24">
            <div className="fixed top-6 z-50 bg-main-green rounded-[40px]">
                <div
                    className={`${open ? "w-[344px]" : "sm:w-[80px] md:w-[90px] lg:w-[100px] xl:w-[120px] 2xl:w-[120px]"} h-[900px] pt-8 relative duration-700 flex-col items-center justify-between`}
                    onMouseEnter={() => setOpen(true)}
                    onMouseLeave={() => setOpen(false)}
                >
                    <div className="flex flex-col items-center justify-center w-full h-20">
                        <img
                            src={logo}
                            alt="Trendlink"
                            className={`cursor-pointer duration-500 sm:w-14 sm:h-14 md:w-16 md:h-16 lg:w-18 lg:h-18 xl:w-20 xl:h-20 2xl:w-20 2xl:h-20`}
                        />
                    </div>

                    <ul className="flex flex-col mt-2 w-full">
                        {Menus.map((Menu, index) => (
                            <li
                                key={index}
                                className={`flex items-center w-full h-full pb-3.5 pt-8 cursor-pointer hover:bg-second-green text-white text-sm transition-all duration-300
                            ${open ? "pl-12" : "sm:pl-7 md:pl-8 lg:pl-8 xl:pl-11 2xl:pl-11 justify-start"}`}
                            >
                                <img alt={`${Menu.title} menu item`}  src={Menu.icon} className="sm:w-6 sm:h-6 md:w-7 md:h-7 lg:w-7 lg:h-7 xl:w-8 xl:h-8 2xl:w-8 2xl:h-8"/>
                                <NavLink to={Menu.path}
                                         className={`flex-grow duration-300 ease-in-out ml-6 ${open ? "opacity-100" : "opacity-0"} origin-left whitespace-nowrap font-inter text-[16px]`}>
                                    {Menu.title}
                                </NavLink>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
};

export default NavBar;