var color = Chart.helpers.color;
	function createConfig(legendPosition, colorName) {
		return {
			type: 'line',
			data: {
				labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
				datasets: [{
					label: 'My First dataset',
					data: [
						randomScalingFactor(),
						randomScalingFactor(),
						randomScalingFactor(),
						randomScalingFactor(),
						randomScalingFactor(),
						randomScalingFactor(),
						randomScalingFactor()
					],
					backgroundColor: color(window.chartColors[colorName]).alpha(0.5).rgbString(),
					borderColor: window.chartColors[colorName],
					borderWidth: 1
				}]
			},
			
			options: {
				responsive: true,
				maintainAspectRatio: false,
				plugins: {
					legend: {
						display: true,
						position: 'bottom',
						labels: {
							color: '#a7b4c9',
							font: {
								family: 'Poppins'
							}
						},
					},
					tooltip: {
						enabled: true
					}
				},
				
				scales: {
					x: {
						display: true,
						scaleLabel: {
							display: true,
							labelString: 'Month'
						},
						ticks: {
							color: '#a7b4c9',
							font: {
								family: 'Poppins', // Your font family
							},
						},
						grid: {
							color: 'rgba(167, 180, 201 ,0.1)',
						}
					},
					y: {
						display: true,
						scaleLabel: {
							display: true,
							labelString: 'Value'
						},
						ticks: {
							color: '#a7b4c9',
							font: {
								family: 'Poppins', // Your font family
							},
						},
						grid: {
							color: 'rgba(167, 180, 201 ,0.1)',
						}
					}
				},
				
			}
		};
	}

	window.onload = function() {
		[{
			id: 'chart-legend-top',
			legendPosition: 'top',
			color: 'primary'
		}, {
			id: 'chart-legend-right',
			legendPosition: 'right',
			color: 'secondary'
		}, {
			id: 'chart-legend-bottom',
			legendPosition: 'bottom',
			color: 'success'
		}, {
			id: 'chart-legend-left',
			legendPosition: 'left',
			color: 'danger'
		}].forEach(function(details) {
			var ctx = document.getElementById(details.id).getContext('2d');
			var config = createConfig(details.legendPosition, details.color);
			new Chart(ctx, config);
			ctx.shadowBlur = 10;
			ctx.shadowOffsetX = 8;
			ctx.shadowOffsetY = 8;
		});
	};