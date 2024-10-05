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
        { title: "Profile", icon: profileIcon, path: "/" },
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
                className={`${open ? "w-344" : "w-120"} bg-dark-purple h-[900px] p-4 pt-8 relative duration-300 flex-col items-center justify-between`}
                onMouseEnter={() => setOpen(true)}
                onMouseLeave={() => setOpen(false)}
            >
                <div className="flex flex-col items-center">
                    <img
                        src={logo}
                        className={`cursor-pointer duration-500`}
                    />
                </div>

                <ul className="flex flex-col gap-6 mt-5 w-full">
                    {Menus.map((Menu, index) => (
                        <li
                            key={index}
                            className={`flex items-center p-2 rounded-md cursor-pointer hover:bg-light-white text-gray-300 text-sm transition-all duration-700
                            ${open ? "justify-start pl-4" : "justify-center"}`}
                        >
                            <img src={Menu.icon}/>
                            <NavLink to="${Menus.path}" className={`${!open && "hidden"} origin-left whitespace-nowrap transition-all duration-700 ml-6 font-inter text-[16px] `}>
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