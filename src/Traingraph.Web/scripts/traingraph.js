function checkJquery() {
  if (window.jQuery) {

  } else {
    window.setTimeout(checkJquery, 100);
  }
}
function loadStackColumnCharts() {

  var categories = ['130', '131', '132', '133', '134'];

  var wagenOnlineOffline = {
    title: "Online / offline Zeit (Minuten)",
    categories: categories,
    series: [{
      name: 'Online',
      data: [0, 1420, 520, 1420, 1420],
      color: '#458B00',
    }, {
      name: 'Offline',
      data: [1420, 0, 900, 0, 0],
      color: '#606060',
    }]
  };

  var unterbrechungen = {
    title: "Unterbrechungen",
    categories: categories,
    series: [{
      data: [0, 4, 3, 1, 0],
      color: '#0000ff',
    }]
  };

  loadStackColumnChart("chart-onlineoffline", wagenOnlineOffline);
  loadStackColumnChart("chart-unterbrechungen", unterbrechungen);
}

//checkJquery();

function loadMonitor() {

  $('#monitor').highcharts({
      chart: {
        zoomType: 'xy'
      },
      title: {
        text: 'Zugverfolgung auf dem TrainGraph'
      },
      subtitle: {
        text: 'Source: blb.csie-data.com'
      },
      xAxis: [{
        //categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
        //    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        crosshair: true,
        labels: {
          format: '{value} km'
        },
        max: 33.3,
      }],
    /*
          yAxis: [{ // left y axis
                title: {
                    text: null
                },
                labels: {
                    align: 'left',
                    x: 3,
                    y: 16,
                    format: '{value:.,0f}'
                },
                showFirstLabel: false
            }, { // right y axis
                linkedTo: 0,
                gridLineWidth: 0,
                opposite: true,
                title: {
                    text: null
                },
                labels: {
                    align: 'right',
                    x: -3,
                    y: 16,
                    format: '{value:.,0f}'
                },
                showFirstLabel: false
            }],

    */

      yAxis: [{ // Primary yAxis
  //      labels: {
  //        formatter: function () {
  //          /*
  //          minute: '%H:%M',
	//hour: '%H:%M',
  //          */
  //          var monthStr = Highcharts.dateFormat('%H:%M', this.value);
  //          //var firstLetter = monthStr.substring(0, 1);
  //          return monthStr;
  //          //console.log(this.value);
  //        },
  //        //style: {
  //        //  color: Highcharts.getOptions().colors[1]
  //        //},
         
        //      },

        // startPeriod = Date.UTC(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
        min: Date.UTC(2015, 10, 13, 5),
        //5 * 60 * 60 * 1000,
        max: Date.UTC(2015, 10, 13, 23),
        tickInterval: 10*60*1000,
        showFirstLabel: false,
        /*
        plotLines: [{
                    value: minRate,
                    color: 'green',
                    dashStyle: 'shortdash',
                    width: 2,
                    label: {
                        text: 'Last quarter minimum'
                    }
                },
        */
        reversed: true,
        type: 'datetime',
        title: {
          text: null,
          //style: {
          //  color: Highcharts.getOptions().colors[1]
          //}
        },
      }, { // right y axis
        linkedTo: 0,
        gridLineWidth: 0,
        opposite: true,
        title: {
          text: ''
        },
        //labels: {
        //  format: '{value}',
        //  style: {
        //    color: Highcharts.getOptions().colors[1]
        //  },

        //},
        type: 'datetime',
        reversed: true,
        //labels: {
        //  align: 'right',
        //  x: -3,
        //  y: 16,
        //  format: '{value:.,0f}'
        //},
        showFirstLabel: false
      }],
      tooltip: {
        shared: true
      },
      legend: {
        layout: 'vertical',
        align: 'left',
        x: 120,
        verticalAlign: 'top',
        y: 100,
        floating: true,
        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
      },
      series: [
      //  {
      //  name: 'Rainfall',
      //  type: 'column',
      //  yAxis: 1,
      //  data: [49.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4],
      //  tooltip: {
      //    valueSuffix: ' mm'
      //  }

      //},
      {
        name: '8662',
        type: 'spline',
        labels: {
          format: '{value}',
          style: {
            color: Highcharts.getOptions().colors[1]
          },

        },
        //pointInterval: 24 * 3600 * 1000,
        data: [[6.0, Date.UTC(2015, 10, 13, 5, 12)], [6.9, Date.UTC(2015, 10, 13, 6, 12)]],
        tooltip: {
          valueSuffix: 'min'
        },
        color: '#FF00FF',
      }]
    });


}


function loadStackColumnChart(containerId, data) {
  $('#' + containerId).highcharts({
      chart: {
        type: 'column'
      },
      title: {
        text: data.title
      },
      xAxis: {
        categories: data.categories
      },
      yAxis: {
        min: 0,
        title: {
          text: 'Minuten'
        },
        stackLabels: {
          enabled: true,
          style: {
            fontWeight: 'bold',
            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
          }
        }
      },
      legend: {
        align: 'right',
        x: -30,
        verticalAlign: 'top',
        y: 25,
        floating: true,
        backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
        borderColor: '#CCC',
        borderWidth: 1,
        shadow: false
      },
      tooltip: {
        formatter: function () {
          return '<b>' + this.x + '</b><br/>' +
              this.series.name + ': ' + this.y + '<br/>' +
              'Total: ' + this.point.stackTotal;
        }
      },
      plotOptions: {
        column: {
          stacking: 'normal',
          dataLabels: {
            enabled: true,
            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
            style: {
              textShadow: '0 0 3px black'
            }
          }
        }
      },
      series: data.series
  }, function (chart) {
    // if no name - hide legend
    if (typeof data.series[0].name === 'undefined') {
      chart.legendHide();
    }
  });
}