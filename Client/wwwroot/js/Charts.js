/* ChartJS
 * -------
 * Here we will create a few charts using ChartJS
 */
var employee = [];
var departments = [];
var namaDept = [];
var genders = ['Male', 'Female'];
var maleData = [];
var femaleData = [];

$(document).ready(function () {
    $(function () {
        // Get data Employees and Departments from API
        $.ajax({
            url: "http://localhost:8086/api/Employees",
            type: "GET",
            contentType: "application/json; charset=utf-8",
            "datatype": "json",
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem('token')
            },
            success: function (getResult) {
                //debugger;
                employee = getResult.data;
                // Process Departments data
                $.ajax({
                    url: "http://localhost:8086/api/Departments",
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    "datatype": "json",
                    headers: {
                        'Authorization': 'Bearer ' + sessionStorage.getItem('token')
                    },
                    success: function (getResult) {
                        //debugger;
                        console.log(employee);
                        departments = getResult.data;
                        console.log(departments);
                        departments.forEach(function (item) {
                            namaDept.push(item.name);
                        });
                        console.log(namaDept);

                        //call callStackedBarChart
                        callStackedBarChart();
                    },
                    error: function (errorMessage) {
                        alert(errorMessage.responseText);
                    }
                });    
            },
            error: function (errorMessage) {
                alert(errorMessage.responseText);
            }
        });

    });
});

function getDataChart() {
    // data bar chart
    //debugger;
    departments.forEach(function (dept) {
        var maleCount = 0;
        var femaleCount = 0;
        employee.forEach(function (emp) {
            if (emp.department.id === dept.id) {
                if (emp.gender === 0) {
                    maleCount++;
                } else if (emp.gender === 1) {
                    femaleCount++;
                }
                console.log("Male :" + maleCount);
                console.log("Female :" + femaleCount);
            }
        });
       // debugger;
        maleData.push(maleCount);
        femaleData.push(femaleCount);
    });

    var chartData = {
        //nama department
        labels: namaDept,
        // dataset gender male or female
        datasets: [
            {
                label: genders[0],
                backgroundColor: 'rgba(60,141,188,0.9)',
                borderColor: 'rgba(60,141,188,0.8)',
                pointRadius: false,
                pointColor: '#3b8bba',
                pointStrokeColor: 'rgba(60,141,188,1)',
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(60,141,188,1)',
                data: maleData // data jumlah gender per employee
            },
            {
                label: genders[1],
                backgroundColor: 'rgba(255, 192, 203, 0.9)', 
                borderColor: 'rgba(255, 192, 203, 0.8)', 
                pointRadius: false,
                pointColor: '#c1a5b0', 
                pointStrokeColor: '#c1a5b0', 
                pointHighlightFill: '#fff',
                pointHighlightStroke: 'rgba(220,220,220,1)',
                data: femaleData // data jumlah gender per employee
            },
        ]
    }
    return chartData;
}

function callStackedBarChart() {
    //---------------------
    //- STACKED BAR CHART -
    //---------------------
    var chartData = getDataChart();
    var stackedBarChartCanvas = $('#stackedBarChart').get(0).getContext('2d')
    var stackedBarChartData = $.extend(true, {}, chartData)

    var stackedBarChartOptions = {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            xAxes: [{
                stacked: true,
            }],
            yAxes: [{
                stacked: true,
            }]
        }
    }

    new Chart(stackedBarChartCanvas, {
        type: 'bar',
        data: stackedBarChartData,
        options: stackedBarChartOptions
    })
}