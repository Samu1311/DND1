// Get the selected graph type
function getSelectedGraphType(selectorId) {
    const dropdown = document.getElementById(selectorId);
    return dropdown.value;
}

// Render the selected graph
function renderSelectedGraph(canvasId, graphType) {
    const ctx = document.getElementById(canvasId).getContext('2d');

    let chartData, chartOptions;

    if (graphType === "heartRate") {
        chartData = {
            labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
            datasets: [{
                label: "Heart Rate",
                data: [70, 72, 68, 75, 73, 78, 77],
                backgroundColor: "rgba(255, 99, 132, 0.2)",
                borderColor: "rgba(255, 99, 132, 1)",
                borderWidth: 1,
                fill: false
            }]
        };
        chartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: true, position: "top" },
            },
            scales: {
                y: { beginAtZero: true },
            }
        };
    } else if (graphType === "weight") {
        chartData = {
            labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul"],
            datasets: [{
                label: "Weight",
                data: [70, 71, 72, 71, 70, 69, 68],
                backgroundColor: "rgba(54, 162, 235, 0.2)",
                borderColor: "rgba(54, 162, 235, 1)",
                borderWidth: 1,
                fill: false
            }]
        };
        chartOptions = {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: true, position: "top" },
            },
            scales: {
                y: { beginAtZero: true },
            }
        };
    }

    new Chart(ctx, {
        type: "line",
        data: chartData,
        options: chartOptions
    });

    // Update the graph title dynamically
    document.getElementById("graphTitle").innerText =
        graphType === "heartRate" ? "Heart Rate Trend" : "Weight Progress";
}
