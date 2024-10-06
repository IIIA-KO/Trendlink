import {NavLink} from "react-router-dom";
import { useState } from "react";
import profileIcon from '../assets/icons/profile-icon.svg';
import searchIcon from '../assets/icons/search-icon.svg';
import statisticsIcon from '../assets/icons/statistics-icon.svg';
import calendarIcon from '../assets/icons/calendar-icon.svg';
import reviewsIcon from '../assets/icons/reviews-icon.svg'
import chatIcon from '../assets/icons/chat-icon.svg';
import newsIcon from '../assets/icons/news-icon.svg';
import notificationsIcon from '../assets/icons/notifications-icon.svg';
import bookmarksIcon from '../assets/icons/bookmarks-icon.svg';
import termsOfCooperationIcon from '../assets/icons/termsOfcooperation-icon.svg';
import settingsIcon from '../assets/icons/settings-icon.svg';
import supportsIcon from '../assets/icons/supports-icon.svg';
import logo from '../assets/logo/logo-trendlink-white.svg'

const NavBar: React.FC = () => {
    const [open, setOpen] = useState(false);
    const Menus = [
        { title: "Profile", icon: profileIcon, path: "/profile" },
        { title: "Search", icon: searchIcon, path: "/" },
        { title: "Chat", icon: chatIcon, path: "/" },
        { title: "Statistics", icon: statisticsIcon, path: "/"},
        { title: "Calendar ", icon: calendarIcon, path: "/" },
        { title: "Reviews", icon: reviewsIcon, path: "/" },
        { title: "News", icon: newsIcon, path: "/" },
        { title: "Notifications ", icon: notificationsIcon, path: "/" },
        { title: "Bookmarks", icon: bookmarksIcon, path: "/" },
        { title: "Terms of cooperation", icon: termsOfCooperationIcon, path: "/" },
        { title: "Settings", icon: settingsIcon, path: "/" },
        { title: "Supports", icon: supportsIcon, path: "/" },
    ];

    return (
        <div className="flex bg-main-green rounded-[40px]">
            <div
                className={`${open ? "w-[344px]" : "w-[120px]"} bg-dark-purple h-[900px] pt-8 relative duration-700 flex-col items-center justify-between`}
                onMouseEnter={() => setOpen(true)}
                onMouseLeave={() => setOpen(false)}
            >
                <div className="flex flex-col items-center">
                    <img
                        src={logo}
                        className={`cursor-pointer duration-500`}
                    />
                </div>

                <ul className="flex flex-col mt-2 w-full">
                    {Menus.map((Menu, index) => (
                        <li
                            key={index}
                            className={`flex items-center w-full h-full pb-3.5 pt-3.5 cursor-pointer hover:bg-second-green text-white text-sm transition-all duration-300
                            ${open ? "pl-12" : "pl-11 justify-start"}`}
                        >
                            <img src={Menu.icon} className="w-8 h-8"/>
                            <NavLink to={Menu.path} className={`flex-grow duration-300 ease-in-out ml-6 ${open ? "opacity-100" : "opacity-0"} origin-left whitespace-nowrap font-inter text-[16px]`}>
                                {Menu.title}
                            </NavLink>
                        </li>
                    ))}
                </ul>
            </div>
        </div>
    );
};

export default NavBar;