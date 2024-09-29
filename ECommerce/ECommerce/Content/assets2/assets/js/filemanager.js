function storage() {
          
            var options = {
              series: [76],
              colors: [myVarVal],
              chart: {
              height: 300,
              type: 'radialBar',
              offsetY: -47,
              sparkline: {
                enabled: true
              }
            },
            plotOptions: {
              radialBar: {
                startAngle: -90,
                endAngle: 90,
                track: {
                  background: "#f0f0f8",
                  strokeWidth: '97%',
                  margin: 10, // margin is in pixels
                  dropShadow: {
                    enabled: false,
                }
                },
                dataLabels: {
                  name: {
                    show: true,
                    fontSize:"11",
                    color:'#828caa',
                    offsetY: -10,
                  },
                  value: {
                    offsetY: -50,
                    fontSize: '15px',
                    color:'#828caa',
                  }
                }
              }
            },
            grid: {
              padding: {
                top: 6
              }
            },
            fill: {
              gradient: {
                  shade: 'light',
                  shadeIntensity: 0.4,
                  inverseColors: false,
                  opacityFrom: 1,
                  opacityTo: 1,
                  stops: [0, 50, 53, 91]
              },
          },
            labels: ['Storage Usage'],
            };

  document.getElementById('storage-usage').innerHTML = '';
  var chart = new ApexCharts(document.querySelector("#storage-usage"), options);
  chart.render();
}