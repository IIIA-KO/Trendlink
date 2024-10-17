import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import {useProfile} from "../hooks/useProfile";

const PieGraph: React.FC = () => {
    const { genderData } = useProfile();

    if (!genderData) {
        return <div>Loading...</div>;
    }

    const series = genderData.genderPercentages.map((item) => Math.round(item.percentage));
    
    const labels = genderData.genderPercentages.map((item) => {
        switch (item.gender) {
            case "M":
                return "Male";
            case "F":
                return "Female";
            default:
                return "Unknown";
        }
    });

    const chartConfig: ApexOptions = {
        series: series,
        chart: {
            type: "pie",
            toolbar: {
                show: false,
            },
        },
        labels: labels,
        title: {
            text: "Gender",
        },
        dataLabels: {
            enabled: false,
        },
        colors: ["#F3AE5F", "#0B87BA", "#C0C0C0"],
        legend: {
            show: false,
        },
    };

    return (
        <div className="relative w-full h-full flex justify-center items-center">
            <Chart
                options={chartConfig}
                series={chartConfig.series}
                type="pie"
                width={200}
                height={200}
            />
        </div>
    );
};


export default PieGraph;