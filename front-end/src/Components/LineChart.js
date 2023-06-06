import React, { useState, useEffect } from "react";
import Chart from "chart.js/auto";
import { Line } from "react-chartjs-2";

const LineChart = ({ priceHistory }) => {
  const [dates, setDates] = useState([]);
  const [prices, setPrices] = useState([]);
  const [options, setOptions] = useState("");

  useEffect(() => {
    console.log(priceHistory);
    const uniqueDates = [
      ...new Set(
        priceHistory.map((DayPriceHistory) => DayPriceHistory.currentDate)
      )
    ];

    const tempDates = uniqueDates.map((date) => formatDate(date));
    const tempPrices = uniqueDates.map((date) => {
      const price = priceHistory.find(
        (DayPriceHistory) => DayPriceHistory.currentDate === date
      ).currentPrice;
      return price.toFixed(2);
    });

    setDates(tempDates);
    setPrices(tempPrices);

    const minPrice = Math.min(...tempPrices);
    const maxPrice = Math.max(...tempPrices);

    const chartOptions = {
      maintainAspectRatio: false,
      scales: {
        y: {
          min: Math.floor(minPrice) - 1,
          max: Math.ceil(maxPrice) + 1,
          ticks: {
            callback: function (value, index, values) {
              return value.toFixed(2);
            }
          }
        }
      }
    };

    setOptions(chartOptions);
  }, [priceHistory]);

  const formatDate = (dateString) => {
    const [day, month, year] = dateString.split("/");
    return `${day}.${month}.${year}`;
  };

  const data = {
    labels: dates,
    datasets: [
      {
        label: "Стоимость товара",
        backgroundColor: "#6233F8",
        borderColor: "#6233F8",
        tension: 0.6,
        data: prices,
        fill: {
          target: "origin",
          above: "rgba(98, 51, 248, 0.5)",
          below: "rgba(98, 51, 248, 0.2)"
        }
      }
    ]
  };

  return (
    <div style={{ width: "100%", height: "260px" }}>
      <Line data={data} options={options} />
    </div>
  );
};

export default LineChart;