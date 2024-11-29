// Render the Heart Rate Chart
function renderHeartRateChart(canvasId) {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
            datasets: [
                {
                    label: 'Heart Rate',
                    data: [70, 72, 68, 75, 73, 78, 77],
                    backgroundColor: 'rgba(255, 99, 132, 0.2)',
                    borderColor: 'rgba(255, 99, 132, 1)',
                    borderWidth: 1,
                    fill: true,
                },
            ],
        },
        options: {
            responsive: true,
            plugins: {
                legend: { display: true, position: 'top' },
            },
        },
    });
}

// Render the Weight Chart
function renderWeightChart(canvasId) {
    const ctx = document.getElementById(canvasId).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
            datasets: [
                {
                    label: 'Weight (kg)',
                    data: [70, 71, 72, 71, 70, 69, 68],
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1,
                    fill: true,
                },
            ],
        },
        options: {
            responsive: true,
            plugins: {
                legend: { display: true, position: 'top' },
            },
        },
    });
}
