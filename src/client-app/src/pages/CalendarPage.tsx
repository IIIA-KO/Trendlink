import CalendarComponent from "../components/CalendarComponent";


const CalendarPage: React.FC = () => {

    const currentMonth = 10;
    const currentYear = 2024;

    return (
        <div className="p-6">
            <h1 className="text-2xl font-bold mb-4">Календар</h1>
            <CalendarComponent month={currentMonth} year={currentYear} />
        </div>
    );
}

export default CalendarPage;