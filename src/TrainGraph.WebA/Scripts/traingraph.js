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

function loadMonitor(containerId) {
    var options = {
      chart: {
        //events : {
        //  load : function () {
        //    //// set up the updating of the chart each second
        //    var series = this.series[0];
        //    setInterval(function () {
        //      var x = (new Date()).getTime(), // current time
        //          y = Math.round(Math.random() * 100);
        //      series.addPoint([x, y], true, true);
        //    }, 1000);
        //  }
        //},
        type: 'spline'
      },
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
        crosshair: true,
        reversed: true,
        labels: {
          format: '{value} km'
        },
      
        //max: 33300,
      }, {
        linkedTo: 0,
        gridLineWidth: 0,
        opposite: true,
        reversed: true,
        labels: {
          format: '{value} km'
        },
        //tickInterval:5
      }],
      yAxis: [{ // Primary yAxis
        // startPeriod = Date.UTC(startDate.getFullYear(), startDate.getMonth(), startDate.getDate());
        //min: Date.UTC(2015, 10, 13, 5),
        //5 * 60 * 60 * 1000,
        //max: Date.UTC(2015, 10, 13, 23),
        tickInterval: 10 * 60 * 1000, // 10 minutes
        showFirstLabel: false,
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
        type: 'datetime',
        reversed: true,
        showFirstLabel: false
      }],
      tooltip: {
        //shared: true,
        useHTML: true,
        formatter: function () {
          //console.log(this);
          var d = Highcharts.dateFormat('%H:%M', this.y);
          var cStyle = "style = 'color:" + this.series.color + ";'";
          var str = "<b " + cStyle + ">O </b> " + this.series.name + " at <b>" + parseFloat(this.x).toFixed(2) + "</b> km <br> in <b>" + d + "</b>";
          return str;
        },
      },
      legend: {
        layout: 'vertical',
        align: 'left',
        x: 0,
        verticalAlign: 'top',
        y: 0,
        floating: true,
        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'
      },
      series: []
    };

    var OnlineTrains = (function () {
      OnlineTrains.data = {};

      function OnlineTrains(data) {
        if (data) {
          OnlineTrains.setData(data);
        }
      }
      OnlineTrains.setData = function (data) {
        data.forEach(function (item) {
          OnlineTrains.data[item.CarId] = item;
        });
      }

      OnlineTrains.updateTrains = function (data) {
        OnlineTrains.data.length = 0;
        OnlineTrains.setData(data);
      };

      OnlineTrains.addOrUpdateItem = function (item) {
        OnlineTrains.data[item.name] = item;
      };

      OnlineTrains.getItem = function (name) {
        return OnlineTrains.data[name];
      };
    
      return OnlineTrains;
    })();

    var createChartPoint = function (el, xLengthPCorrection, correction) {
      var date = new Date(el.d);

      //console.log(date.getFullYear(),
      //  date.getMonth() + 1, date.getDate(), date.getHours() + timeOffset, date.getMinutes(), date.getSeconds());
      var dd = Date.UTC(date.getFullYear(),
        date.getMonth() + 1, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds());
      // correct data
      var xKm = 0;

      //if (currentTrain.Direction !== -1)
      //  xKm = correction * (xLengthP - el.p - xLengthPCorrection);
      //else
      xKm = parseFloat(((el.p - xLengthPCorrection) * correction).toFixed(2));
      //console.log(xKm);
      //console.log(xKm.toFixed(2));

      return [xKm, dd];
    }

    var getChartSeries = function (callback, getLast) {

      $.getJSON((document.location.pathname.length == 1 ? '' : document.location.pathname) + "/api/tis", function (data) {
        if (data.trainGraphData !== null) {

          var curves = data.trainGraphData.Curves;
          var series = [];
          //var timeOffset = 0;
          var xLengthP = 1236;
          var xLengthKm = 33.3;
          var xLengthPCorrection = 0;
          OnlineTrains.setData(data.onlineTrains);

          curves.forEach(function (curve, index) {
            var item = {};
            var currentTrain = OnlineTrains.getItem(curve.c);
            item.name = cleanUpId(curve.n);

            //console.log(currentTrain);
            //item.id = cleanUpId(curve.n);
            //item.type = 'spline';
            item.data = [];

            var correction = xLengthKm / xLengthP;

            if (typeof getLast !== 'undefined') {
              var curveLen = curve.s.length;
              if (curveLen > 0)
                item.data.push(createChartPoint(curve.s[curveLen - 1], xLengthPCorrection, correction));
            } else {
              curve.s.forEach(function (el, index) {
                item.data.push(createChartPoint(el, xLengthPCorrection, correction));
              });
            }

            series.push(item);
          });

          callback(series);
        } else {
          console.log(data.Error);
          callback(null);

        }
      });
    };
    
    var stations = [];
    stations.push(['Freilassing', 33.3]);
    stations.push(['Freilassing Hofham', 31.9]);
    stations.push(['Ainring', 30.0]);
    stations.push(['Hammerau', 27.5]);
    stations.push(['Piding', 22.6]);
    stations.push(['Bad Reichenhail', 18.6]);
    stations.push(['BR Kirchberg', 17.0]);
    stations.push(['Bayerisch Gmain', 15.2]);
    stations.push(['Bischofswiesen', 5.1]);
    stations.push(['Berchtesgaden Hbf', 0]);

    var xPlotlines = [];
    stations.forEach(function (data) {
      var st = {
        value: data[1],
        color: 'gray',
        dashStyle: 'shortdash',
        width: 1,
        label: {
          text: data[0]
        },
      
      }
      xPlotlines.push(st);
    })

    options.xAxis[0].plotLines = xPlotlines;

    getChartSeries(function (data) {
      options.series = data;
      options.chart.renderTo = containerId;

      // initialize Chart
      var chart = new Highcharts.Chart(options);
   
      var blockRequest = !1;
      // count of iterations
      var count = 10000;
      //console.log(chart.series[0].id);
      setInterval(function () {
        if (blockRequest === !1 && count > 0) {
          blockRequest = !0;
          getChartSeries(function (series) {
            var shouldRedraw = !1;
            //console.log(series);
            if (series !== null) {
              series.forEach(function (el, index) {
                //var lastElement = el.data[el.data.length - 1];
                //console.log(lastElement);
                // find this series on chart
                var lLen = el.data.length;
                if (lLen > 0) {
                  var s = getLastSeriesIdByName(chart.series, el.name);
                  if (s !== null) {
                    //console.log(s[0].data.length);
                    console.log('found match series:');
                    console.log(s);
                    console.log('with:');
                    console.log(el);
                    var sLen = s[0].data.length;

                    // s[0] is current chart series
                    if (sLen > 0) {
                      var currY = s[0].data[sLen - 1].y;
                      var updatedY = el.data[lLen - 1][1];
                      //console.log('compare y coords:');
                      //console.log(currY, updatedY);
                      // compare new values with existing
                      if (currY !== updatedY) {
                        //shouldRedraw = !0;
                        // addPoint (Object options, [Boolean redraw], [Boolean shift], [Mixed animation])
                        console.log('add point at');
                        console.log(el.name);
                        console.log([el.data[lLen - 1][0], updatedY]);

                        chart.series[s[1]].addPoint({ x: el.data[lLen - 1][0], y: updatedY }, false, false);
                        console.log('new series:');
                        console.log(chart.series[s[1]]);
                        shouldRedraw = !0;
                      }
                    }

                  } else {
                    // TODO: should add new series
                    //addSeries (Object options, [Boolean redraw], [Mixed animation])
                    console.log('add new series');
                    console.log(el);
                    chart.addSeries(el, false);
                  }
                }
              });
              //if (shouldRedraw)
              //console.log('chart redraw?');
              if(shouldRedraw)
                chart.redraw();
            }
            blockRequest = !1;
            count--;
          }, true);

        }
      }, 4*1000);
    });
}


/**
* Get Last (in datetime) series. Because there are more than one
* series with identical name
*/
function getLastSeriesIdByName(series, name) {
  //var result = [];
  for (var i = 0, len = series.length; i < len; i++) {
    if (series[i].name === name) {
      //if (result.length > 0) {
      //  if(series[i].data.y > result[0].data.y)
          return [series[i], i];
      //} else {
        //result = [series[i], i];
      //}
    }
  }
    return null; // The object was not found
}


function cleanUpId(id) {
  //return id.replace(/^[^a-z]+|[^\w:.-]+/gi, "");
  //return "id" + id + Math.round(Math.random() * 100);
  while (id.charAt(0) === '-')
    id = id.substr(1);
  return id;
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
        align: 'left',
        x: -100,
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

function getTrainData() {
  $.get(document.location.pathname + "/api/tis", function (data) {
    console.log(data);
  });
}

function postTrainData() {
  var dataIn =
  {
    clientLastRealTimeTrainGraphTimeValue: '2015-11-13T15:30:43.511',
    getRegularTraingraphData: false,
    getServerTimes: false,
    traingraphStartValue: '2015-11-13T05:00:00.000',
    traingraphStopValue: '2012-11-14T01:00:00.000'
  };

  $.post(document.location.pathname + "/api/tis", dataIn, function (data) {
    console.log(data);
  });
}
