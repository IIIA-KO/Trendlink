import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import {useProfile} from "../hooks/useProfile";

const PieGraph: React.FC = () => {

    const { genderData } = useProfile();

    if (!genderData) {
        return <div>Loading...</div>;
    }

    const series = genderData.map((item) => item.percentage);
    const labels = genderData.map((item) => item.gender)

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
            text: "",
        },
        dataLabels: {
            enabled: false,
        },
        colors: ["#F3AE5F", "#0B87BA"],
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