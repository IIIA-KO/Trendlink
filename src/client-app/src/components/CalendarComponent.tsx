import React, {useEffect, useState} from 'react';
import { useCalendar } from '../hooks/useCalendar';
import {CooperationType} from "../types/CooperationType";
import {CalendarDayType} from "../types/CalendarDayType";
import {getTermsAndConditions} from "../services/termsofcooperation";
import {AdvertisementsType} from "../types/AdvertisementsType";
import CooperationDetails from "./CooperationDetails";
import EditCooperationModal from "./EditCooperationModal";

interface CalendarComponentProps {
    month: number;
    year: number;
}

const CalendarComponent: React.FC<CalendarComponentProps> = ({ month, year }) => {
    const { calendarData } = useCalendar();
    const [selectedDate, setSelectedDate] = useState<string | null>(null);
    const [details, setDetails] = useState<CooperationType[]>([]);
    const [editCooperation, setEditCooperation] = useState<CooperationType | null>(null);
    const [advertisements, setAdvertisements] = useState<AdvertisementsType[]>([]);

    const daysOfWeek = ['ПН', 'ВТ', 'СР', 'ЧТ', 'ПТ', 'СБ', 'НД'];

    useEffect(() => {
        const fetchTermsAndConditions = async () => {
            const termsAndConditions = await getTermsAndConditions();
            if (termsAndConditions) {
                setAdvertisements(termsAndConditions.advertisements);
            }
        };

        fetchTermsAndConditions();
    }, []);

    // Пошук назви реклами за її ID
    const getAdvertisementNameById = (advertisementId: string) => {
        const advertisement = advertisements.find(ad => ad.id === advertisementId);
        return advertisement ? advertisement.name : 'Реклама не знайдена';
    };

    // Обробка кліку на дату
    const handleDateClick = (date: string, cooperations: CooperationType[]) => {
        setSelectedDate(date);
        setDetails(cooperations);
    };

    const saveChanges = (updatedCooperation: CooperationType) => {
        console.log('Збережено зміни для кооперації:', updatedCooperation);
        setEditCooperation(null); // Закриваємо модальне вікно після збереження
    };

    const closeModal = () => {
        setEditCooperation(null);
    };

    // Генерація всіх днів місяця
    const generateMonthDays = (monthData: CalendarDayType[], currentMonth: number, currentYear: number) => {
        const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate();
        const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay() || 7;

        const daysArray = [];

        // Додаємо порожні дні перед початком місяця
        for (let i = 1; i < firstDayOfMonth; i++) {
            daysArray.push(<div key={`empty-${i}`} className="calendar-day-empty"></div>);
        }

        // Додаємо дні поточного місяця
        for (let i = 1; i <= daysInMonth; i++) {
            const currentDate = new Date(currentYear, currentMonth, i).toISOString().split('T')[0];
            const dayData = monthData.find(day => new Date(day.date).getDate() === i && new Date(day.date).getMonth() === currentMonth);
            const cooperations = dayData ? dayData.cooperations : [];

            daysArray.push(
                <div
                    key={currentDate}
                    className={`calendar-day w-10 h-10 flex items-center justify-center rounded-full cursor-pointer
                ${cooperations.length > 0 ? 'bg-green-500 text-white' : 'bg-gray-200 text-black'}
                ${selectedDate === currentDate ? 'bg-yellow-400 text-white' : ''}
                `}
                    onClick={() => handleDateClick(currentDate, cooperations)}
                >
                    {i}
                </div>
            );
        }

        return daysArray;
    };

    const adjustedMonth = month - 1; // Корекція для JavaScript (0-11 замість 1-12)

    return (
        <div className="flex space-x-6">
            {/* Календар */}
            <div className="calendar-container">
                <h3 className="text-center text-xl font-semibold mb-4">{new Date(year, adjustedMonth).toLocaleString('uk-UA', { month: 'long', year: 'numeric' })}</h3>
                <div className="grid grid-cols-7 gap-2 text-center mb-2">
                    {daysOfWeek.map((day) => (
                        <div key={day} className="text-sm font-semibold">{day}</div>
                    ))}
                </div>
                <div className="grid grid-cols-7 gap-2">
                    {calendarData && generateMonthDays(calendarData, adjustedMonth, year)}
                </div>
            </div>

            {/* Деталі кооперації */}
            <div className="details-container w-1/3 bg-gray-100 p-6 rounded-lg shadow-md">
                {details.length > 0 ? (
                    details.map((cooperation) => (
                        <CooperationDetails
                            key={cooperation.id}
                            cooperation={cooperation}
                            getAdvertisementNameById={getAdvertisementNameById}
                            openEditModal={setEditCooperation}
                        />
                    ))
                ) : (
                    <p>Немає кооперацій на вибрану дату. <button className="text-blue-500 hover:underline">Запланувати кооперацію</button></p>
                )}
            </div>

            {/* Модальне вікно для редагування */}
            {editCooperation && (
                <EditCooperationModal
                    cooperation={editCooperation}
                    onClose={closeModal}
                    onSave={saveChanges}
                />
            )}
        </div>
    );
};


export default CalendarComponent;