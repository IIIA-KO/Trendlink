import CalendarComponent from "../components/CalendarComponent";
import {useState} from "react";


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
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-4">Календар</h1>

            {/* Селектор для вибору статусу */}
            <div className="flex space-x-4 mb-4">
                <select
                    value={selectedStatus !== null ? selectedStatus : ""}
                    onChange={(e) => setSelectedStatus(e.target.value !== "" ? Number(e.target.value) : null)}
                    className="border px-2 py-1 rounded"
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
                    className="border px-2 py-1 rounded"
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
                <label className="font-semibold">Select starting month:</label>
                <select
                    value={selectedMonth}
                    onChange={(e) => handleMonthChange(Number(e.target.value))}
                    className="border px-2 py-1 rounded"
                >
                    {Array.from({ length: 12 }).map((_, i) => (
                        <option key={i + 1} value={i + 1}>
                            {new Date(0, i).toLocaleString("en-US", { month: "long" })}
                        </option>
                    ))}
                </select>
            </div>

            {/* Відображаємо кілька календарів */}
            <div className="grid grid-cols-2 gap-4">
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
                    className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                >
                    Add More Months
                </button>
            </div>
        </div>
    );
};


export default CalendarPage;