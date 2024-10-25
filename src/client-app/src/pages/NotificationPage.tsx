import React, {useEffect} from "react";
import {useNotification} from "../hooks/useNotification";
import ReactPaginate from "react-paginate";
import iconLeft from "../assets/icons/navigation-chevron-left.svg";
import iconRight from "../assets/icons/navigation-chevron-right.svg";

const NotificationPage: React.FC = () => {
    const { notifications, fetchNotifications, paginationData } = useNotification();
    const pageSize = 8;

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
        <div>
            <h1>Сповіщення</h1>
            {notifications.length > 0 ? (
                <ul>
                    {notifications.map((notification) => (
                        <li key={notification.id}>
                            <h2>{notification.title}</h2>
                            <p>{notification.message}</p>
                            <small>{new Date(notification.createdOnUtc).toLocaleString()}</small>
                        </li>
                    ))}
                </ul>
            ) : (
                <p>Немає сповіщень</p>
            )}

            {/* Пагинация */}
            {paginationData.totalPages > 1 && (
                <ReactPaginate
                    previousLabel={paginationData.currentPage > 1 ? <img alt="left" src={iconLeft} /> : <button  className="pointer-events-none cursor-default"></button >}
                    nextLabel={paginationData.currentPage < paginationData.totalPages ? <img alt="right" src={iconRight}/> :
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
    );
};




export default NotificationPage;