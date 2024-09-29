function canvasDoughnut2() {

	/*-----canvasDoughnut-----*/
	document.querySelector(".canvasD2").innerHTML = '<canvas id="canvasDoughnut2" class="h-230"></canvas>';
	if ($('#canvasDoughnut2').length) {
		var ctx = document.getElementById("canvasDoughnut2").getContext("2d");
		new Chart(ctx, {
			type: 'doughnut',
			data: {
				labels: ['Organic', 'Direct', 'Campagion',],
				datasets: [{
					data: [56, 20, 30],
					backgroundColor: [myVarVal, '#1cc5ef', '#24e4ac'],
					borderColor:'transparent',
				}]
			},
			options: {
				responsive: true,
				maintainAspectRatio: false,
				plugins: {
					legend: {
						display: false,
					},		
				},
				cutout: 80,
			}
		});
	}
	/*-----canvasDoughnut-----*/
	
}
function AduienceOverview() {
	var chart = document.getElementById('echart');
	var chartdata = [{
		name: 'New Visitors',
		type: 'bar',
		data: [10, 15, 9, 18, 10, 15, 7, 14],
		symbolSize: 10,
		barWidth: '20%',
		itemStyle: {
			normal: {
				barBorderRadius: [0, 0, 0, 0],
				color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
					offset: 0,
					color: myVarVal
				}, {
					offset: 1,
					color: myVarVal
				}])
			}
		},
	}, {
		name: 'Return Visitors',
		type: 'bar',
		data: [10, 14, 10, 15, 9, 25, 15, 18],
		symbolSize: 10,
		barWidth: '20%',
		itemStyle: {
			normal: {
				barBorderRadius: [0, 0, 0, 0],
				color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
					offset: 0,
					color: '#24e4ac'
				}, {
					offset: 1,
					color: '#24e4ac'
				}])
			}
		},
	}];
		var barChart = echarts.init(chart);
		var option = {
			grid: {
				top: '6',
				right: '0',
				bottom: '17',
				left: '25',
			},
			xAxis: {
				data: ['2014', '2015', '2016', '2017', '2018', '2019'],
				axisLine: {
					lineStyle: {
						color: 'rgba(112, 131, 171, .1)'
					}
				},

				axisLabel: {
					textStyle: {
						color: '#a7b4c9',
						fontSize: 10
					}
				},
			},
			tooltip: {
				show: true,
				showContent: true,
				alwaysShowContent: true,
				triggerOn: 'mousemove',
				trigger: 'axis',
				axisPointer: {
					label: {
						show: false,
					}
				}
			},
			yAxis: {
				splitLine: {
					lineStyle: {
						color: 'rgba(112, 131, 171, .1)'
					}
				},
				axisLine: {
					lineStyle: {
						color: 'rgba(119, 119, 142, 0.2)'
					}
				},
				axisLabel: {
					textStyle: {
						color: '#a7b4c9',
						fontSize: 10
					}
				}
			},
			series: chartdata,
			color: [myVarVal, '#24e4ac']
		};
		barChart.setOption(option);
		window.addEventListener('resize',function(){
			barChart.resize();
		})
	
	}

function countries() {
	var options = {
		series: [{
		data: [550, 410, 320, 620, 540, 580, 690, 1100, 1200, 1380]
	  }],
		chart: {
		type: 'bar',
		height: 388,
		toolbar: {
			show: true
		}
	  },
	  plotOptions: {
		bar: {
		  borderRadius: 4,
		  horizontal: true,
		  colors: {
			ranges: [{
				from: -100,
				to: -46,
				color: '#adb5be'
			}, {
				from: -45,
				to: 0,
				color: '#adb5be'
			}]
			},
		}
	  },
	  colors: [myVarVal],
	  grid: {
		show: false,
		borderColor: '#f2f6f7',
	  },
	  
	  dataLabels: {
		enabled: false
	  },
	  xaxis: {
		axisTicks: {
			show: true,
			color: '#adb5be',
		},
		categories: ['South Korea', 'Ireland', 'United Kingdom', 'Canada', 'Italy', 'France', 'Japan',
		  'United States', 'China', 'Australia'
		],
		min: 0,
		max: 1500,
		range: 400,
		labels: {
			show: true,
			style: {
			color: '#adb5be',
			  fontSize: '11px',
			  fontFamily: 'Poppins, sans-serif',
			  fontWeight: 400,
			  cssClass: 'apexcharts-xaxis-label',
			},
		},
	  },
	  yaxis: {
		labels: {
			show: true,
			style: {
			color: '#adb5be',
			  fontSize: '11px',
			  fontFamily: 'Poppins, sans-serif',
			  fontWeight: 400,
			  cssClass: 'apexcharts-xaxis-label',
			},
		  },
	  }
	  };
	  document.getElementById('countries').innerHTML = '';
	  var chart = new ApexCharts(document.querySelector("#countries"), options);
	  chart.render();

}

