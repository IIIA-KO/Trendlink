import React, {useEffect, useState} from "react";
import {useNotification} from "../hooks/useNotification";
import {useUser} from "../hooks/useUser";
import {NotificationType} from "../types/NotificationType";
import ReactPaginate from "react-paginate";
import TopBar from "../components/TopBar";
import Navbar from "../components/Navbar";

const NotificationPage: React.FC = () => {
    const { notifications } = useNotification();
    const { user } = useUser();

    const [pageNumber, setPageNumber] = useState(1);
    const pageSize = 9;  // Кількість записів на сторінку

    if (!notifications) {
        return <p>Завантаження сповіщень...</p>; // Показуємо повідомлення, поки notifications завантажуються
    }

    const totalPages = Math.ceil(notifications.length / pageSize);

    if (pageNumber > totalPages && totalPages > 0) {
        setPageNumber(1);
    }

    const currentNotifications = notifications.slice((pageNumber - 1) * pageSize, pageNumber * pageSize);

    const handlePageChange = (selectedItem: { selected: number }) => {
        setPageNumber(selectedItem.selected + 1);
    };

    return (
        <div className="bg-background flex justify-start h-auto w-auto">
        <div className="h-auto w-1/6 flex justify-start items-center pl-1 sm:pl-4 md:pl-6 lg:pl-10 xl:pl-22 2xl:pl-28">
            <Navbar />
        </div>
        <div className="w-5/6 h-auto">
            <div className="flex flex-col gap-2  bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                <TopBar user={null} />
               
                <div className="h-1/4 w-full text-center text-black">
                <div className="h-1/4 w-full inline-block text-center text-black">
            <div className="p-6"></div>
        <div>
            <h1 className="font-normal font-['Inter']"><u><b>Сповіщення</b></u></h1>
            {currentNotifications.length > 0 ? (
                <ul className="w-70% ml-10 mr-10">
                    {currentNotifications.map(notification => (
                        <li className="justify-start mt-2 items-start border-b" key={notification.id}>
                                          <h2 className="font-normal  bg-blue-50 rounded-3xl font-['Inter'] w-32 h-6 text-cente">{notification.title}</h2>
                                          <div className="inline-flex gap-4">
                                          <p className="font-normal font-['Inter']">{notification.message}</p>
                                          <p><small className="font-normal font-['Inter'] text-light-gray"><i>{new Date(notification.createdOnUtc).toLocaleString()}</i></small></p>
                                          </div>
                        </li>
                    ))}
                </ul>
            ) : (
                <p className="font-normal font-['Inter']">Немає сповіщень</p>
            )}

            {/* Пагінація */}
            {totalPages > 1 && (
                <ReactPaginate
                    previousLabel={pageNumber > 1 ? '← Попередня' : null}
                    nextLabel={'Наступна →'}
                    breakLabel={'...'}
                    pageCount={totalPages}
                    marginPagesDisplayed={2}
                    pageRangeDisplayed={3}
                    onPageChange={handlePageChange}
                    containerClassName={'pagination'}
                    activeClassName={'active'}
                />
            )}
        </div>
        </div>
        </div>
        </div>
        </div>
        </div>
    );
};



export default NotificationPage;