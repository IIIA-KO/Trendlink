import React, {useEffect, useState} from 'react';
import { useCalendar } from '../hooks/useCalendar';
import {CooperationType} from "../types/CooperationType";
import {CalendarDayType} from "../types/CalendarDayType";
import {getTermsAndConditions} from "../services/termsofcooperation";
import {AdvertisementsType} from "../types/AdvertisementsType";
import CooperationDetails from "./CooperationDetails";
import {CalendarComponentProps} from "../types/CalendarComponentProps";

const CalendarComponent: React.FC<CalendarComponentProps> = ({ month, year, selectedStatus }) => {
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
        return advertisement ? advertisement.name : "Advertisement not found";
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

    const filterCooperationsByStatus = (cooperations: CooperationType[]) => {
        return selectedStatus !== null
            ? cooperations.filter((c) => c.status === selectedStatus)
            : cooperations;
    };

    const generateMonthDays = (monthData: CalendarDayType[], currentMonth: number, currentYear: number) => {
        const daysInMonth = new Date(currentYear, currentMonth + 1, 0).getDate();
        const firstDayOfMonth = new Date(currentYear, currentMonth, 1).getDay() || 7;

        const daysArray = [];

        for (let i = 1; i < firstDayOfMonth; i++) {
            daysArray.push(<div key={`empty-${i}`} className="calendar-day-empty"></div>);
        }

        for (let i = 1; i <= daysInMonth; i++) {
            const currentDate = `${currentYear}-${String(currentMonth + 1).padStart(2, "0")}-${String(i).padStart(2, "0")}`;
            const dayData = monthData.find(
                (day) =>
                    new Date(day.date).getDate() === i &&
                    new Date(day.date).getMonth() === currentMonth &&
                    new Date(day.date).getFullYear() === currentYear
            );
            const cooperations = dayData ? dayData.cooperations : [];
            const filteredCooperations = filterCooperationsByStatus(cooperations);
            const isBlocked = dayData?.isBlocked || false;

            daysArray.push(
                <div
                    key={currentDate}
                    className={`calendar-day w-10 h-10 flex items-center justify-center rounded-full cursor-pointer
                    ${filteredCooperations.length > 0 ? "bg-[#009d9f] font-normal font-['Inter'] text-white" : "font-normal font-['Inter'] text-black"}
                    ${selectedDate === currentDate ? "bg-yellow-400 font-normal font-['Inter'] text-white" : ""}
                `}
                    onClick={() => handleDateClick(currentDate, filteredCooperations, isBlocked)}
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
            <div className="justify-start text-center items-center bg-[#eff7ff] rounded-[20px] shadow">
                <h3 className="text-[#3c3c3c] text-sm font-normal font-['Inter'] justify-center uppercase tracking-tight">
                    {new Date(year, adjustedMonth).toLocaleString("en-US", {
                        month: "long",
                        year: "numeric",
                    })}
                </h3>
                <div className="w-[251px] h-[0px] ml-5px border border-[#e4e5e7]"></div>
                <div className="grid grid-cols-7 gap-2 text-center mb-2">
                    {daysOfWeek.map((day) => (
                        <div key={day} className="grow shrink basis-0 text-center text-[#7e818c] text-[10px] font-normal font-['Inter'] uppercase">
                            {day}
                        </div>
                    ))}
                </div>
                <div className="grid grid-cols-7 gap-2">
                    {calendarData && generateMonthDays(calendarData, adjustedMonth, year)}
                </div>
            </div>

            <div className="h-full w-1/3 bg-[#eff7ff] rounded-[20px] shadow p-6 font-['Inter'] rounded-lg shadow-md">
                {selectedDate && !isPastDate && (
                    <div className="flex justify-between items-center mb-4">
                        <button
                            onClick={handleBlockToggle}
                            className={`px-4 py-2 rounded ${
                                isBlocked ? " bg-[#f4b400] text-white text-sm font-normal font-['Inter'] rounded-[40px] text-white" : " bg-[#009d9f] text-white text-sm font-normal font-['Inter'] rounded-[40px] text-white"
                            }`}
                        >
                            {isBlocked ? "Unblock date" : "Block date"}
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
                    <p className="font-normal font-['Inter']">There are no cooperations for the selected date.</p>
                )}
            </div>
        </div>
    );
};

export default CalendarComponent;