import React, {useEffect, useState} from "react";
import {useNotification} from "../hooks/useNotification";
import {useUser} from "../hooks/useUser";
import {NotificationType} from "../types/NotificationType";
import ReactPaginate from "react-paginate";

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
        <div>
            <h1>Сповіщення</h1>
            {currentNotifications.length > 0 ? (
                <ul>
                    {currentNotifications.map(notification => (
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
    );
};



export default NotificationPage;