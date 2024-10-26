import React, {useEffect} from "react";
import {useNotification} from "../hooks/useNotification";
import ReactPaginate from "react-paginate";
import {useState} from "react";
import TopBar from "../components/TopBar";
import { useUser } from "../hooks/useUser";
const NotificationPage: React.FC = () => {
    const { notifications, fetchNotifications, paginationData } = useNotification();
    const pageSize = 8;
    const { user, fetchUserData } = useUser();


    useEffect(() => {
        fetchNotifications({ pageNumber: 1, pageSize });

    }, []);

    if (!notifications) {
        return <p>Завантаження сповіщень...</p>;
    }

    const handlePageChange = (selectedItem: { selected: number }) => {
        const newPageNumber = selectedItem.selected + 1;
        fetchNotifications({ pageNumber: newPageNumber, pageSize });
    };

    return (
        <div className="bg-background flex justify-start h-auto w-auto">

        <div className="w-5/6 h-auto">
            <div className="flex flex-col gap-2  bg-custom-bg bg-cover bg-no-repeat rounded-[50px] h-auto w-auto min-h-screen min-w-screen sm:mr-24 md:mr-32 lg:mr-42 xl:mr-64 mt-10">
                <TopBar user={user} />
               
                <div className="h-1/4 w-full text-center text-black">
                <div className="h-1/4 w-full inline-block text-center text-black">
            <div className="p-6"></div>
        <div>
            <h1>Сповіщення</h1>
            {notifications.length > 0 ? (
                <ul>
                    {notifications.map(notification => (
                        <li key={notification.id} className="grid grid-cols-3 gap-2 b-b">
                            <h2>{notification.title}</h2>
                            <p>{notification.message}</p>
                            <small><i>{new Date(notification.createdOnUtc).toLocaleString()}</i></small>
                        </li>
                    ))}
                </ul>
            ) : (
                <p className="font-normal font-['Inter']">Немає сповіщень</p>
            )}

            {/* Пагинация */}
            {paginationData.totalPages > 1 && (
                <ReactPaginate
                    previousLabel={paginationData.currentPage > 1 ? <img alt="left" /> : <button  className="pointer-events-none cursor-default"></button >}
                    nextLabel={paginationData.currentPage < paginationData.totalPages ? <img alt="right"/> :
                        <button className="pointer-events-none cursor-default"></button>}
                    breakLabel={"..."}
                    pageCount={paginationData.totalPages}
                    marginPagesDisplayed={2}
                    pageRangeDisplayed={3}
                    onPageChange={handlePageChange}
                    containerClassName="flex flex-row gap-6"
                    activeClassName="bg-gray-10 text-main-black rounded-full py-2 px-4"
                    pageClassName="p-2 hover:scale-110 active:scale-90 rounded-full cursor-pointer"
                    previousClassName="p-2 hover:scale-110 active:scale-90 rounded-full"
                    nextClassName="p-2 hover:scale-110 active:scale-90 rounded-full"
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