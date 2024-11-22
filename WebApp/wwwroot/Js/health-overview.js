function renderHeartRateChart(canvasId) {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
            datasets: [{
                label: 'Heart Rate (bpm)',
                data: [72, 75, 70, 74, 73],
                borderColor: '#004085',
                fill: false,
            }]
        },
        options: { responsive: true }
    });
}

function renderWeightChart(canvasId) {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
            datasets: [{
                label: 'Weight (kg)',
                data: [70, 69.5, 69, 70, 68.5],
                borderColor: '#f7ea5d',
                fill: false,
            }]
        },
        options: { responsive: true }
    });
}

// Export to make these callable from Blazor
window.renderHeartRateChart = renderHeartRateChart;
window.renderWeightChart = renderWeightChart;
