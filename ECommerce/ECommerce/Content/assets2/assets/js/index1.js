function sales() {
    'use strict'

    // SALES
	document.querySelector(".chartjs-demo").innerHTML = '<canvas id="sales" class="chart-dropshadow"></canvas>';
    var myCanvas = document.getElementById("sales");
    myCanvas.height = "315";

    var myCanvasContext = myCanvas.getContext("2d");
    var gradientStroke1 = myCanvasContext.createLinearGradient(0, 80, 0, 280);
    gradientStroke1.addColorStop(0, hexToRgba(myVarVal, 0.1) || 'rgba(82, 92, 229, 0.1)');
    gradientStroke1.addColorStop(1, hexToRgba(myVarVal, 0.01) || 'rgba(82, 92, 229, 0.01)');

    var gradientStroke2 = myCanvasContext.createLinearGradient(0, 80, 0, 280);
    gradientStroke2.addColorStop(0, hexToRgba(253, 83, 97, 0.1) || 'rgba(253, 82, 97, 0.1)');
    gradientStroke2.addColorStop(1, hexToRgba(253, 83, 97, 0.01) || 'rgba(253, 82, 97, 0.01)');
    new Chart(myCanvas, {
        type: 'line',
        data: {
            labels: ["Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec"],
            type: 'line',
			tooltipFillColor: "rgba(51, 51, 51, 0.55)",
            datasets: [{
                label: 'Total Income',
                data: [47, 45, 154, 38, 156, 24, 45, 31, 137, 39, 162, 51],
                backgroundColor: gradientStroke1,
                borderColor: myVarVal,
                pointBackgroundColor: '#fff',
                pointHoverBackgroundColor: gradientStroke1,
                pointBorderColor: myVarVal,
                pointHoverBorderColor: gradientStroke1,
                pointBorderWidth: 0,
                pointRadius: 0,
                pointHoverRadius: 0,
                borderWidth: 1,
                fill: 'origin',
                lineTension: 0.25,
            }, {
                label: 'Total Expenses',
                data: [61, 27, 54, 143, 99, 46, 120, 45, 54, 138, 56, 24],
                backgroundColor: gradientStroke2,
                borderColor: '#fd5261',
                pointBackgroundColor: '#fff',
                pointHoverBackgroundColor: gradientStroke2,
                pointBorderColor: '#fd5261',
                pointHoverBorderColor: gradientStroke2,
                pointBorderWidth: 0,
                pointRadius: 0,
                pointHoverRadius: 0,
                borderWidth: 1,
                fill: 'origin',
                lineTension: 0.25,

            }]
        },
		chart: {
			fontFamily: 'Poppins'
		},
        options: {
            responsive: true,
            maintainAspectRatio: false,
            bezierCurve : false,
			interaction: {
                intersect: false,
                mode: 'index',
            },
			plugins: {
				legend: {
					display: true,
					labels: {
						color: '#a7b4c9',
						font: {
							family: 'Poppins'
						}
					},
				},
				tooltip: {
					enabled: true,
				}
			},
             scales: {
                x: {
                    grid: {
                        display: true,
                        drawBorder: false,
                        color: 'rgba(167, 180, 201 ,0.1)',
						zeroLineColor: 'rgba(167, 180, 201 ,0.5)'
                    },
                    ticks: {
                        color: '#a7b4c9',
                        autoSkip: true,
						font: {
							family: 'Poppins', // Your font family
						},
                    },
                },
                y: {
                    ticks: {
                        min: 0,
                        max: 200,
                        stepSize: 40,
                        color: '#a7b4c9',
                    },
                    grid: {
                        display: false,
                    },
                },
            },
        }
    });
}

/*-----canvasDoughnut-----*/
function canvasDoughnut1() {
	document.querySelector(".canvasd").innerHTML = '<canvas id="canvasDoughnut1" class="chartsh h-315"></canvas>';
	if ($('#canvasDoughnut1').length) {
		var ctx = document.getElementById("canvasDoughnut1").getContext("2d");
		new Chart(ctx, {
			type: 'doughnut',
			data: {
				labels: ['Mens', 'Womens', 'Kids', 'Electronics', 'Home & Furniture'],
				datasets: [{
					data: [56, 20, 30, 12, 22],
					backgroundColor: [myVarVal, '#9c52fd', '#24e4ac', "#ffa70b", "#ec5444"],
					borderColor:'transparent',
				}],
				
			},
			options: {
				responsive: true,
				resize: true,
				maintainAspectRatio: false,
				plugins: {
					legend: {
						display: true,
						position: 'bottom',
						labels: {
							color: '#a7b4c9',
							padding: 15,
							usePointStyle: true,
							boxWidth: 5,
							font: {
								family: "Poppins"
							}
						}
					},
				},
				cutout: 75,
			},
		});
	}
}
/*-----canvasDoughnut-----*/


/*-----AreaChart chartjs-----*/
function widgetChart1 () {
	
	document.querySelector(".chart-wrapper").innerHTML = '<canvas id="widgetChart1" class="overflow-hidden"></canvas>';
	var ctx = document.getElementById("widgetChart1");
	var myChart = new Chart(ctx, {
		type: 'line',
		tooltipFillColor: "rgba(51, 51, 51, 0.55)",
		data: {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
			type: 'line',
			datasets: [{
				data: [25, 31, 21, 29, 40, 23, 41],
				label: '',
				backgroundColor: hexToRgba(myVarVal, 0.21),
				fill: true,
				borderColor: [myVarVal],
				borderWidth: 2,
                pointBorderColor: 'transparent',
				pointBackgroundColor: 'transparent',
				lineTension: 0.28,
			} ]
		},
		options: {
			maintainAspectRatio: false,
			responsive: true,
            interaction: {
                intersect: false,
                mode: 'index',
            },
			plugins: {
				legend: {
					display: false,
					font: {
						family: "Poppins"
					}
				},
				tooltip: {
					enabled: true
				}			
			},
			scales: {
				x: {
					grid: {
						display: false,
					},
					ticks: {
						display: false,
						fontSize: 2,
						fontColor: 'transparent'
					},
				},
				y: {
					display: false,
				}
			},
			
		}
	});
}