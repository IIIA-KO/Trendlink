import CalendarComponent from "../components/CalendarComponent";
import {useState} from "react";
import Navbar from "../components/Navbar";
import TopBar from "../components/TopBar";


const CalendarPage: React.FC = () => {

    const currentDate = new Date();
    const currentMonth = currentDate.getMonth() + 1;
    const currentYear = currentDate.getFullYear();

    const [selectedMonth, setSelectedMonth] = useState<number>(currentMonth);
    const [selectedYear, setSelectedYear] = useState<number>(currentYear);

    const [numMonths, setNumMonths] = useState<number>(2);
    const [selectedStatus, setSelectedStatus] = useState<number | null>(null);

    const addMoreMonths = () => {
        setNumMonths((prev) => prev + 2);
    };

    const handleMonthChange = (newMonth: number) => {
        setSelectedMonth(newMonth);
    };

    const handleYearChange = (newYear: number) => {
        setSelectedYear(newYear);
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
            <div className="p-6">

            {/* Селектор для вибору статусу */}
            <div className="flex space-x-4 mb-4">
                <select
                    value={selectedStatus !== null ? selectedStatus : ""}
                    onChange={(e) => setSelectedStatus(e.target.value !== "" ? Number(e.target.value) : null)}
                    className="h-[35px] px-2.5 py-1 text-[#c0bebe] text-[11px] font-normal font-['Inter'] rounded-[5px] border border-[#c0bebe] gap-2.5 inline-flex"
                >
                    <option value="">All Statuses</option>
                    <option value={1}>Pending</option>
                    <option value={2}>Confirmed</option>
                    <option value={3}>Rejected</option>
                    <option value={4}>Cancelled</option>
                    <option value={5}>Done</option>
                    <option value={6}>Completed</option>
                </select>

                {/* Селектор року */}
                <select
                    value={selectedYear}
                    onChange={(e) => handleYearChange(Number(e.target.value))}
                    className="h-[35px] px-2.5 py-1 rounded-[5px] text-[#c0bebe] ml-[] text-[11px] font-normal font-['Inter'] border border-[#c0bebe] gap-2.5 inline-flex"
                >
                    {Array.from({ length: 10 }).map((_, i) => (
                        <option key={i} value={currentYear - 5 + i}>
                            {currentYear - 5 + i}
                        </option>
                    ))}
                </select>
            </div>

            {/* Вибір місяця тільки для першого календаря */}
            <div className="flex items-center space-x-2 mb-2">
                <label className="text-[#c0bebe] text-[14px] font-normal font-['Inter']">Select starting month:</label>
                <select
                    value={selectedMonth}
                    onChange={(e) => handleMonthChange(Number(e.target.value))}
                    className="h-[35px] px-2.5 py-1 rounded-[5px] text-[#c0bebe] text-[11px] font-normal font-['Inter'] border border-[#c0bebe] gap-2.5 inline-flex"
                >
                    {Array.from({ length: 12 }).map((_, i) => (
                        <option key={i + 1} value={i + 1}>
                            {new Date(0, i).toLocaleString("en-US", { month: "long" })}
                        </option>
                    ))}
                </select>
            </div>

            {/* Відображаємо кілька календарів */}
            <div className="grid grid-cols-1 gap-4">
                {Array.from({ length: numMonths }).map((_, index) => {
                    const displayedMonth = (selectedMonth + index - 1) % 12 + 1;
                    const displayedYear =
                        selectedMonth + index > 12 ? selectedYear + 1 : selectedYear;

                    return (
                        <CalendarComponent
                            key={`${displayedYear}-${displayedMonth}`}
                            month={displayedMonth}
                            year={displayedYear}
                            selectedStatus={selectedStatus}
                        />
                    );
                })}
            </div>

            {/* Кнопка для додавання ще двох місяців */}
            <div className="mt-6">
                <button
                    onClick={addMoreMonths}
                    className="w-[238px] py-2.5 bg-[#009d9f] text-white text-sm font-normal font-['Inter'] rounded-[40px] justify-center items-center gap-2.5 inline-flex"
                >
                    Add More Months
                </button>
                </div>
        </div>
                    </div>
                    <div className="h-full relative w-1/2 mx-10 my-6">
                    </div>
               </div>
            </div>
        </div>
   </div>
    );
};


export default CalendarPage;