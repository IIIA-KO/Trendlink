import React, {useEffect, useState} from 'react';
import { useCalendar } from '../hooks/useCalendar';
import {CooperationType} from "../types/CooperationType";
import {CalendarDayType} from "../types/CalendarDayType";
import {getTermsAndConditions} from "../services/termsofcooperation";
import {AdvertisementsType} from "../types/AdvertisementsType";
import CooperationDetails from "./CooperationDetails";
import {CalendarComponentProps} from "../types/CalendarComponentProps";

const CalendarComponent: React.FC<CalendarComponentProps> = ({ month, year }) => {
    const { calendarData, blockSelectedDate, unblockSelectedDate } = useCalendar();
    const [selectedDate, setSelectedDate] = useState<string | null>(null);
    const [details, setDetails] = useState<CooperationType[]>([]);
    const [advertisements, setAdvertisements] = useState<AdvertisementsType[]>([]);
    const [isBlocked, setIsBlocked] = useState<boolean>(false);

    const daysOfWeek = ['MO', 'TU', 'WE', 'TH', 'FR', 'SA', 'SU'];

    useEffect(() => {
        const fetchTermsAndConditions = async () => {
            const termsAndConditions = await getTermsAndConditions();
            if (termsAndConditions) {
                setAdvertisements(termsAndConditions.advertisements);
            }
        };

        fetchTermsAndConditions();
    }, []);

    const getAdvertisementNameById = (advertisementId: string) => {
        const advertisement = advertisements.find(ad => ad.id === advertisementId);
        return advertisement ? advertisement.name : 'Реклама не знайдена';
    };

    const handleDateClick = (date: string, cooperations: CooperationType[], isBlocked: boolean) => {
        setSelectedDate(date);
        setDetails(cooperations);
        setIsBlocked(isBlocked);
    };

    const handleBlockToggle = async () => {
        if (selectedDate) {
            if (isBlocked) {
                await unblockSelectedDate(selectedDate);
            } else {
                await blockSelectedDate(selectedDate);
            }
            setIsBlocked(!isBlocked);
        }
    };

    const generateMonthDays = (monthData: CalendarDayType[], currentMonth: number, currentYear: number) => {
        const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate();
        const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay() || 7;

        const daysArray = [];

        for (let i = 1; i < firstDayOfMonth; i++) {
            daysArray.push(<div key={`empty-${i}`} className="calendar-day-empty"></div>);
        }

        for (let i = 1; i <= daysInMonth; i++) {
            const currentDate = `${currentYear}-${String(currentMonth + 1).padStart(2, '0')}-${String(i).padStart(2, '0')}`;
            const dayData = monthData.find(day => new Date(day.date).getDate() === i && new Date(day.date).getMonth() === currentMonth && new Date(day.date).getFullYear() === currentYear);
            const cooperations = dayData ? dayData.cooperations : [];
            const isBlocked = dayData?.isBlocked || false;

            daysArray.push(
                <div
                    key={currentDate}
                    className={`calendar-day w-10 h-10 flex items-center justify-center rounded-full cursor-pointer
                    ${cooperations.length > 0 ? 'bg-green-500 text-white' : 'bg-gray-200 text-black'}
                    ${selectedDate === currentDate ? 'bg-yellow-400 text-white' : ''}
                `}
                    onClick={() => handleDateClick(currentDate, cooperations, isBlocked)}
                >
                    {i}
                </div>
            );
        }

        return daysArray;
    };

    const adjustedMonth = month - 1;

    const isPastDate = selectedDate && new Date(selectedDate) < new Date();

    return (
        <div className="flex space-x-6">
            <div className="border-2 h-full">
                <h3 className="text-center text-xl font-semibold mb-4">{new Date(year, adjustedMonth).toLocaleString('en-US', { month: 'long', year: 'numeric' })}</h3>
                <div className="grid grid-cols-7 gap-2 text-center mb-2">
                    {daysOfWeek.map((day) => (
                        <div key={day} className="text-sm font-semibold">{day}</div>
                    ))}
                </div>
                <div className="grid grid-cols-7 gap-2">
                    {calendarData && generateMonthDays(calendarData, adjustedMonth, year)}
                </div>
            </div>

            <div className="h-full w-1/3 bg-gray-100 p-6 rounded-lg shadow-md border-2">
                {selectedDate && !isPastDate && (
                    <div className="flex justify-between items-center mb-4">
                        <button
                            onClick={handleBlockToggle}
                            className={`px-4 py-2 rounded ${isBlocked ? 'bg-red-500 text-white' : 'bg-green-500 text-white'}`}
                        >
                            {isBlocked ? 'Unblock date' : 'Block date'}
                        </button>
                    </div>
                )}

                {details.length > 0 ? (
                    details.map((cooperation) => (
                        <CooperationDetails
                            key={cooperation.id}
                            cooperation={cooperation}
                            getAdvertisementNameById={getAdvertisementNameById}
                        />
                    ))
                ) : (
                    <p>There are no cooperations for the selected date.</p>
                )}
            </div>
        </div>
    );
};



export default CalendarComponent;