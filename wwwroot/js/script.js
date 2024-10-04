document.addEventListener('DOMContentLoaded', function () {
    // Map of fuel type values to fuel type names
    const fuelTypeMap = {
        1: "Biomass",
        2: "Fossil Brown coal/Lignite",
        3: "Fossil Coal-derived gas",
        4: "Fossil Gas",
        5: "Fossil Hard coal",
        6: "Fossil Oil",
        7: "Fossil Oil shale",
        8: "Fossil Peat",
        9: "Geothermal",
        10: "Hydro Pumped Storage",
        11: "Hydro Run-of-river and poundage",
        12: "Hydro Water Reservoir",
        13: "Marine",
        14: "Nuclear",
        15: "Other renewable",
        16: "Solar",
        17: "Waste",
        18: "Wind Offshore",
        19: "Wind Onshore",
        100: "Other"
    };

    const ctx = document.getElementById('capacityChart').getContext('2d');
    let capacityChart = null;

    function initializeChart(labels, data, isFuelType, isGeoLocation) {
        if (capacityChart) {
            capacityChart.destroy();
        }

        capacityChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Total Unavailable Capacity',
                    data: data,
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    x: (() => {
                        if (!isFuelType && !isGeoLocation) {
                            return {
                                type: 'time',
                                time: {
                                    unit: 'hour',
                                    tooltipFormat: 'YYYY-MM-DD HH:mm',
                                    displayFormats: {
                                        hour: 'HH:mm'
                                    }
                                },
                                title: {
                                    display: true,
                                    text: 'Fuel Type'
                                }
                            };
                        } else if (isFuelType) {
                            return {
                                title: {
                                    display: true,
                                    text: 'Fuel Type'
                                }
                            };
                        } else if (isGeoLocation) {
                            return {
                                title: {
                                    display: true,
                                    text: 'Geo Location'
                                }
                            };
                        }
                    })(),
                    y: {
                        beginAtZero: true,
                        title: {
                            display: true,
                            text: 'Unavailable Capacity (MW)'
                        }
                    }
                }
            }
        });
    }

    // Fetch data from .NET API
    async function fetchData(startDateFull, endDateFull) {
        var startDate = new Date(startDateFull).toISOString().split('T')[0];
        var endDate = new Date(endDateFull).toISOString().split('T')[0];
        console.log(`Fetching data from ${startDate} to ${endDate}`);
        isFuelType = false;
        isGeoLocation = true;
        const response = await fetch(`http://localhost:5257/api/umm/production-unavailability-UI?startDate=${startDate}&endDate=${endDate}&isFuelType=${isFuelType}&isGeoLocation=${isGeoLocation}`);
        const data = await response.json();
        console.log(data);
        return {data, isFuelType, isGeoLocation};
    }

    // Update chart with new data
    async function updateChart() {
        const startDate = document.getElementById('start-date').value;
        const endDate = document.getElementById('end-date').value;

        const {data, isFuelType, isGeoLocation} = await fetchData(startDate, endDate);

        // TODO - this is wrong - if you are repeating the same code.
        if (isFuelType) {
            const labels = data.map(item => fuelTypeMap[item.fuelType]);
            const capacities = data.map(item => item.totalUnavailableCapacity);
            initializeChart(labels, capacities, isFuelType, isGeoLocation);
        }

        if (isGeoLocation) {
            const labels = data.map(item => item.areaName);
            const capacities = data.map(item => item.totalUnavailableCapacity);
            initializeChart(labels, capacities, isFuelType, isGeoLocation);
        }

        if (!isFuelType && !isGeoLocation) {
            const labels = data.map(item => item.time);
            const capacities = data.map(item => item.totalUnavailableCapacity);
            initializeChart(labels, capacities, isFuelType, isGeoLocation);
        }

        // const labels = data.map(item => fuelTypeMap[item.fuelType]);
        // const capacities = data.map(item => item.totalUnavailableCapacity);

        // initializeChart(labels, capacities, isFuelType, isGeoLocation);
    }

    // Initialize chart with default data
    updateChart();

    document.getElementById('update-chart').addEventListener('click', updateChart);
});
