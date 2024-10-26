import Chart from "react-apexcharts";
import {useAudience} from "../hooks/useAudience";
import {useEffect, useState} from "react";
import {AudienceAgeData} from "../types/AudienceAgeDataType";
import {ApexOptions} from "apexcharts";

const StraightGraph: React.FC = () => {

    const { ageData, fetchAudienceAgePercentage } = useAudience();

    useEffect(() => {
        fetchAudienceAgePercentage();
    }, [fetchAudienceAgePercentage]);

    const chartOptions: ApexOptions = {
        chart: {
            type: "bar" as const,
        },
        xaxis: {
            categories: ageData?.map(item => item.ageRange) || [],
        },
        yaxis: {
            labels: {
                formatter: (value: number) => `${value.toFixed(0)}%`,
            },
        },
        plotOptions: {
            bar: {
                horizontal: false,
                columnWidth: "50%",
            },
        },
        dataLabels: {
            enabled: false,
        },
        fill: {
            colors: ["#0b87ba"],
        },
        title: {
            text: "Вік",
            align: "left",
            style: {
                fontSize: '16px',
                fontWeight: 'bold',
                color: '#3c3c3c',
            },
        },
    };

    const chartSeries = [
        {
            name: "Percentage",
            data: ageData ? ageData.map(item => item.percentage) : [],
        },
    ];

    return (
        <div className="relative w-full h-full flex justify-center items-center">
            {ageData ? (
                <Chart
                    options={chartOptions}
                    series={chartSeries}
                    type="bar"
                    height={300}
                    width={500}
                />
            ) : (
                <p>Loading age data...</p>
            )}
        </div>
    );
};


export default StraightGraph;