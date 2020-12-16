/**
 * Created by Administrator on 2017/5/17.
 */

$(function () {
    // index();
    $(".index_nav ul li").each(function (index) {
        $(this).click(function () {
            $(this).addClass("nav_active").siblings().removeClass("nav_active");
            $(".index_tabs .inner").eq(index).fadeIn().siblings("div").stop().hide();
            if (index == 1) {
                yingXiao();
            } else if (index == 2) {
                shouRu();
            } else if (index == 3) {
                AnQuan();
            } else if (index == 4) {
                user();
            } else if (index == 5) {
                manage();
            }
        })
    });
    $(".tabs ul li").each(function (index) {
        $(this).click(function () {
            $(".tabs ul li div .div").removeClass("tabs_active");
            $(this).find("div .div").addClass("tabs_active");
            $(".tabs_map>div").eq(index).fadeIn().siblings("div").stop().hide();
        })
    });
    $(".middle_top_bot ul li").each(function () {
        $(this).click(function () {
            $(".middle_top_bot ul li").removeClass("middle_top_bot_active");
            $(this).addClass("middle_top_bot_active");
        })
    });

});

function user() {
    ////游客来源分析
    //$(function () {
    //    var myChart = echarts.init($("#container1")[0]);
    //    var option = {
    //        tooltip: {
    //            trigger: 'item',
    //            formatter: "{a} <br/>{b} : {c} ({d}%)"
    //        },
    //        legend: {
    //            x: 'center',
    //            y: "16",
    //            data: ['盈利', '亏损'],
    //            textStyle: {
    //                color: "#e9ebee"
    //            }
    //        },
    //        series: [
    //            {
    //                itemStyle: {
    //                    normal: {
    //                        label: {
    //                            show: false
    //                        },
    //                        labelLine: {
    //                            show: false
    //                        }
    //                    }
    //                },
    //                name: '',
    //                type: 'pie',
    //                radius: '55%',
    //                center: ['50%', '60%'],
    //                data: [
    //                    {
    //                        value: 90, name: '盈利',
    //                        itemStyle: {
    //                            normal: {
    //                                color: "#2865aa"
    //                            }
    //                        }
    //                    },
    //                    {
    //                        value: 10, name: '亏损',
    //                        itemStyle: {
    //                            normal: {
    //                                color: "#ff81cb"
    //                            }
    //                        }
    //                    }
    //                ]
    //            }
    //        ]
    //    };



    //    myChart.setOption(option);
    //});

    //种植面积分析
    $(function () {
        var myChart = echarts.init($("#container2")[0]);
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['游客较少', '游客中等', '游客较多'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "游客溢出",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '游客溢出',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '游客较少',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '游客中等',
                            itemStyle: {
                                normal: {
                                    color: '#ffea00'

                                }
                            }
                        },
                        {
                            value: 5, name: '游客较多',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });

    //营收分析
    $(function () {
        var myChart = echarts.init($("#container1")[0]);
        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    lineStyle: {
                        color: '#57617B'
                    }
                }
            },
            legend: {

                //icon: 'vertical',
                data: ['河桥老街', '唐昌首镇'],
                //align: 'center',
                // right: '35%',
                top: '0',
                textStyle: {
                    color: "#fff"
                },
                // itemWidth: 15,
                // itemHeight: 15,
                itemGap: 20,
            },
            grid: {
                left: '0',
                right: '20',
                top: '10',
                bottom: '0',
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255,255,255,.6)'
                    }
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                },
                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
            }, {

            }],
            yAxis: [{
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255,255,255,.6)'
                    }
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                }
            }],
            series: [{
                name: '河桥老街',
                type: 'line',
                smooth: true,
                symbol: 'circle',
                symbolSize: 5,
                showSymbol: false,
                lineStyle: {
                    normal: {
                        width: 2
                    }
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(24, 163, 64, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(24, 163, 64, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#cdba00',
                        borderColor: 'rgba(137,189,2,0.27)',
                        borderWidth: 12
                    }
                },
                data: [120, 58, 142, 134, 150, 120, 110, 125, 145, 122, 165, 122]
            }, {
                name: '唐昌首镇',
                type: 'line',
                smooth: true,
                symbol: 'circle',
                symbolSize: 5,
                showSymbol: false,
                lineStyle: {
                    normal: {
                        width: 2
                    }
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(39, 122,206, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(39, 122,206, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#277ace',
                        borderColor: 'rgba(0,136,212,0.2)',
                        borderWidth: 12
                    }
                },
                data: [120, 110, 125, 145, 122, 165, 122, 144, 182, 148, 134, 150]
            }]
        };


        myChart.setOption(option);
    });



    //危房信息
    $(function () {
        var myChart = echarts.init(document.getElementById('container5'));
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['民宿', '酒店'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "入驻情况",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '入驻情况',
                    type: 'pie',
                    radius: ['40%', '60%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '民宿',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '酒店',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        }
                    ]
                }
            ]
        };
        myChart.setOption(option);
    });

    //景点分布
    $(function () {
        var myChart = echarts.init($("#container6")[0]);
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                x: 46,
                y: 30,
                x2: 32,
                y2: 40,
                borderWidth: 0
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: ['河桥村', '金燕村', '罗山村', '泥骆村', '蒲村', '七都村'],

                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '景点数量（个）',
                    type: 'bar',
                    barWidth: '30',
                    data: [5, 9, 10, 7, 6, 3, 8, 4, 6, 7, 3, 3, 1],
                    itemStyle: {
                        normal: {
                            color: "#0aff6c"
                        }
                    }

                }
            ]
        };
        myChart.setOption(option);
    });

    //商家入驻率
    $(function () {
        var myChart = echarts.init($("#container9")[0]);
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['男', '女'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "男女比例",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '男女比例',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 8, name: '男',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        },
                        {
                            value: 2, name: '女',
                            itemStyle: {
                                normal: {
                                    color: '#ff425d'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });

    //企业数量
    $(function () {
        var myChart = echarts.init($("#container10")[0]);
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['杭州市', '宁波市', '温州市', '嘉兴市'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "游客来源",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '游客来源',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '杭州市',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '宁波市',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        },
                        {
                            value: 3, name: '温州市',
                            itemStyle: {
                                normal: {
                                    color: '#9b04f9'

                                }
                            }
                        },
                        {
                            value: 3, name: '嘉兴市',
                            itemStyle: {
                                normal: {
                                    color: '#ffea00'

                                }
                            }
                        }
                    ]
                }
            ]
        };
        myChart.setOption(option);
    });

    //旅游收入分析
    $(function () {
        var myChart = echarts.init($("#container11")[0]);
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['幼年', '少年', '青年', '中年', '老年'],
                textStyle: {
                    color: "#e9ebee"

                }
            },

            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "游客年龄",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '游客年龄',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '幼年',
                            itemStyle: {
                                normal: {
                                    color: '#8CC142'

                                }
                            }
                        },
                        {
                            value: 4, name: '少年',
                            itemStyle: {
                                normal: {
                                    color: '#23977A'

                                }
                            }
                        },
                        {
                            value: 5, name: '青年',
                            itemStyle: {
                                normal: {
                                    color: '#36AFCE'

                                }
                            }
                        },
                        {
                            value: 5, name: '中年',
                            itemStyle: {
                                normal: {
                                    color: '#1A71AF'

                                }
                            }
                        },
                        {
                            value: 6, name: '老年',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });

    //房产信息
    $(function () {
        var myChart = echarts.init($("#container12")[0]);
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                x: 100,
                y: 30,
                x2: 32,
                y2: 40,
                borderWidth: 0
            },
            xAxis: {
                type: 'value',
                x: '180',
                splitLine: {
                    show: false
                },
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: '#a4a7ab'
                    }
                }
            },
            yAxis: {
                type: 'category',
                data: ['柳栖民宿', '常青树民宿', '临安水云阁民宿', '临安归隐民宿', '栗溪民宿'],
                splitLine: {
                    show: false
                },
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: '#a4a7ab'
                    }
                }
            },
            series: [
                {
                    name: '空房数量（间）',
                    type: 'bar',
                    stack: '总量',
                    label: {
                        normal: {
                            show: true,
                            position: 'insideRight'
                        }
                    },
                    data: [9, 12, 2, 23, 5, 8, 8],
                    itemStyle: {
                        normal: {
                            color: "#ff7d0a"
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });

}

function manage() {

    //就业男女比例
    $(function () {
        var myChart = echarts.init($("#userContainerSex")[0]);
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['已租', '未租'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "租房信息",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '租房信息',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '已租',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '未租',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        }
                    ]
                }
            ]
        };
        myChart.setOption(option);
    });

    //就业率
    $(function () {
        var myChart = echarts.init($("#userContainerManage")[0]);
        var option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: "16",
                data: ['杭州市', '宁波市', '温州市', '嘉兴市', '湖州市', '其他地区'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            series: [
                {
                    itemStyle: {
                        normal: {
                            label: {
                                show: false
                            },
                            labelLine: {
                                show: false
                            }
                        }
                    },
                    name: '',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: [
                        {
                            value: 335, name: '杭州市',
                            itemStyle: {
                                normal: {
                                    color: "#1afffd"
                                }
                            }
                        },
                        {
                            value: 310, name: '宁波市',
                            itemStyle: {
                                normal: {
                                    color: "#2e7cff"
                                }
                            }
                        },
                        {
                            value: 234, name: '温州市',
                            itemStyle: {
                                normal: {
                                    color: "#ffcb89"
                                }
                            }
                        },
                        {
                            value: 135, name: '嘉兴市',
                            itemStyle: {
                                normal: {
                                    color: "#005ea1"
                                }
                            }
                        },
                        {
                            value: 148, name: '湖州市',
                            itemStyle: {
                                normal: {
                                    color: "#45c0ff"
                                }
                            }
                        },
                        {
                            value: 148, name: '其他地区',
                            itemStyle: {
                                normal: {
                                    color: "#999"
                                }
                            }
                        }

                    ]
                }
            ]
        };



        myChart.setOption(option);
    });

    //年龄段比例
    $(function () {
        var myChart = echarts.init($("#userContainerAge")[0]);
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                x: 30,
                y: 10,
                x2: 10,
                y2: 30,
                borderWidth: 0
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: ['2月', '4月', '6月', '8月', '10月', '12月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '矫正人员',
                    type: 'bar',
                    barWidth: '30',
                    data: [124, 522, 235, 365, 214, 265,],
                    itemStyle: {
                        normal: {
                            color: "#00ffff"
                        }
                    }

                }
            ]
        };
        myChart.setOption(option);
    });

    //落户人数分析
    $(function () {
        var myChart = echarts.init(document.getElementById('container4'));
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            //legend: {
            //    data:['散客（万人）','团队（万人）'],
            //    y:'20',
            //    textStyle: { fontWeight: 'bold', color: '#a4a7ab' }
            //},
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            grid: {
                x: 30,
                y: 10,
                x2: 10,
                y2: 30,
                borderWidth: 0
            },
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '已解决事件（件）',
                    type: 'bar',
                    stack: '广告',
                    barWidth: '30',
                    data: [320, 332, 301, 334, 390, 330, 320],
                    itemStyle: {
                        normal: {
                            color: "#ffea00"
                        }
                    }
                },
                {
                    name: '未解决事件（件）',
                    type: 'bar',
                    stack: '广告',
                    data: [120, 132, 101, 134, 90, 230, 210],
                    itemStyle: {
                        normal: {
                            color: "#0084e7"
                        }
                    }
                }
            ]
        };


        myChart.setOption(option);
    });


    //各景点人数统计
    $(function () {
        var myChart = echarts.init($("#userContainerAttendance")[0]);
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                x: 46,
                y: 30,
                x2: 32,
                y2: 40,
                borderWidth: 0
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: ['河桥古镇', '柳溪江', '河桥庙会', '曙光亭', '唐昌首镇门楼'],

                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '景点人数（人）',
                    type: 'bar',
                    barWidth: '30',
                    data: [760, 560, 390, 770, 860, 950, 580, 4, 6, 7, 3, 3, 1],
                    itemStyle: {
                        normal: {
                            color: "#1afffd"
                        }
                    }

                }
            ]
        };
        myChart.setOption(option);
    });

    //停车位分析
    $(function () {
        var myChart = echarts.init($("#userContainerPersonal")[0]);
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['小区', '社区'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "智慧社区",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '智慧社区',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '小区',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '社区',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });



    //游客数据分析
    $(function () {
        var myChart = echarts.init(document.getElementById('userContainerFlow'));
        var option = {
            tooltip: {
                trigger: 'axis'
            },
            grid: {
                x: 50,
                y: 10,
                x2: 30,
                y2: 30,
                borderWidth: 0
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }

                }
            ],
            series: [
                {
                    name: '总人数（人）',
                    type: 'line',
                    stack: '人',
                    data: [120, 120, 101, 134, 90, 300, 100, 130, 240, 250, 207, 50],
                    itemStyle: {
                        normal: {
                            color: "#1afffd"
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });

    //智慧公厕使用情况分析
    $(function () {
        var myChart = echarts.init($("#userContainerIllegal")[0]);
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            //legend: {
            //    data:['散客人数（万人）','团队人数（万人）'],
            //    textStyle: { fontWeight: 'bold', color: '#a4a7ab' }
            //},
            grid: {
                x: 40,
                y: 10,
                x2: 20,
                y2: 30,
                borderWidth: 0
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '已损坏（个）',
                    type: 'bar',
                    //stack: '广告',
                    data: [50, 60, 50, 50, 45, 40, 25],
                    itemStyle: {
                        normal: {
                            color: "#ffea00"
                        }
                    }
                },
                {
                    name: '良好（个）',
                    type: 'bar',
                    data: [60, 65, 55, 56, 58, 45, 30],
                    itemStyle: {
                        normal: {
                            color: "#00ffff"
                        }
                    }
                }

            ]
        };
        myChart.setOption(option);
    });

    //各行业收入情况
    $(function () {
        var myChart = echarts.init(document.getElementById('userContainerComplaint'));
        var option = {
            tooltip: {
                trigger: 'axis'
            },
            grid: {
                x: 60,
                y: 30,
                x2: 32,
                y2: 40,
                borderWidth: 0
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }

                }
            ],
            series: [
                {
                    name: '优秀',
                    type: 'line',
                    stack: '人',
                    data: [120, 132, 101, 134, 90, 230, 210, 230, 245, 256, 278, 300],
                    itemStyle: {
                        normal: {
                            color: "#45c0ff"
                        }
                    }
                },
                {
                    name: '良好',
                    type: 'line',
                    stack: '人',
                    data: [220, 182, 191, 234, 290, 330, 310, 230, 245, 256, 278, 300],
                    itemStyle: {
                        normal: {
                            color: "#0ad5ff"
                        }
                    }
                },
                {
                    name: '一般',
                    type: 'line',
                    stack: '人',
                    data: [220, 182, 191, 234, 290, 330, 310, 230, 245, 256, 278, 300],
                    itemStyle: {
                        normal: {
                            color: "#005ea1"
                        }
                    }
                },
                {
                    name: '较差',
                    type: 'line',
                    stack: '人',
                    data: [220, 182, 191, 234, 290, 330, 310, 230, 245, 256, 278, 300],
                    itemStyle: {
                        normal: {
                            color: "#ffcb89"
                        }
                    }
                },
                {
                    name: '差劲',
                    type: 'line',
                    stack: '人',
                    data: [220, 182, 191, 234, 290, 330, 310, 230, 245, 256, 278, 300],
                    itemStyle: {
                        normal: {
                            color: "#2e7cff"
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });

    //商家入驻
    $(function () {
        var myChart = echarts.init(document.getElementById('userContainerPrize'));
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            //legend: {
            //    data:['散客人数（万人）','团队人数（万人）'],
            //    textStyle: { fontWeight: 'bold', color: '#a4a7ab' }
            //},
            grid: {
                x: 46,
                y: 30,
                x2: 32,
                y2: 40,
                borderWidth: 0
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['肯德基', '麦当劳', '劳力士', '绝味鸭脖', '周黑鸭'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '已建设商店',
                    type: 'bar',
                    data: [5, 7, 4, 1, 2, 9, 6, 5, 7],
                    itemStyle: {
                        normal: {
                            color: "#1afffd"
                        }
                    }
                },
                {
                    name: '正在建设商店',
                    type: 'bar',
                    stack: '广告',
                    data: [2, 1, 4, 9, 7, 5, 2, 7, 6],
                    itemStyle: {
                        normal: {
                            color: "#2e7cff"
                        }
                    }
                }
            ]
        };


        myChart.setOption(option);
    });

    //游客溢出情况
    $(function () {
        var myChart = echarts.init(document.getElementById('userContainerReason'));
        var option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },

            legend: {
                x: 'center',
                y: "16",
                data: ['溢出较少', '溢出中等', '溢出较多'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            series: [
                {
                    name: '溢出次数',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: [
                        {
                            value: 335, name: '溢出较少',
                            itemStyle: {
                                normal: {
                                    color: "#1afffd"
                                }
                            }
                        },
                        {
                            value: 310, name: '溢出中等',
                            itemStyle: {
                                normal: {
                                    color: "#2e7cff"
                                }
                            }
                        },
                        {
                            value: 234, name: '溢出较多',
                            itemStyle: {
                                normal: {
                                    color: "#ffcb89"
                                }
                            }
                        }
                    ],
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });

    //游客报警情况
    $(function () {
        var myChart = echarts.init(document.getElementById('userContainerHandle'));
        var option = {
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                x: 'center',
                y: "16",
                data: ['已处理', '未处理', '超期未处理', '其他'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            series: [
                {
                    name: '处理情况',
                    type: 'pie',
                    radius: '55%',
                    center: ['50%', '60%'],
                    data: [
                        {
                            value: 335, name: '已处理',
                            itemStyle: {
                                normal: {
                                    color: "#2e7cff"
                                }
                            }
                        },
                        {
                            value: 310, name: '未处理',
                            itemStyle: {
                                normal: {
                                    color: "#ffcb89"
                                }
                            }
                        },
                        {
                            value: 234, name: '超期未处理',
                            itemStyle: {
                                normal: {
                                    color: "#2864ab"
                                }
                            }
                        },
                        {
                            value: 148, name: '其他',
                            itemStyle: {
                                normal: {
                                    color: "#e15828"
                                }
                            }
                        }
                    ],
                    itemStyle: {
                        emphasis: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });
}

function yingXiao() {
    // 营销分析
    // 24小时购买时间分析
    $(function () {
        var myChart = echarts.init($("#buyTime")[0]);
        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    lineStyle: {
                        color: '#57617B'
                    }
                }
            },
            legend: {

                //icon: 'vertical',
                data: ['河桥村', '金燕村'],
                //align: 'center',
                // right: '35%',
                top: '0',
                textStyle: {
                    color: "#fff"
                },
                // itemWidth: 15,
                // itemHeight: 15,
                itemGap: 20,
            },
            grid: {
                left: '0',
                right: '20',
                top: '10',
                bottom: '0',
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255,255,255,.6)'
                    }
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                },
                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
            }, {




            }],
            yAxis: [{
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255,255,255,.6)'
                    }
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                }
            }],
            series: [{
                name: '河桥村',
                type: 'line',
                smooth: true,
                symbol: 'circle',
                symbolSize: 5,
                showSymbol: false,
                lineStyle: {
                    normal: {
                        width: 2
                    }
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(24, 163, 64, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(24, 163, 64, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#cdba00',
                        borderColor: 'rgba(137,189,2,0.27)',
                        borderWidth: 12
                    }
                },
                data: [120, 58, 142, 134, 150, 120, 110, 125, 145, 122, 165, 122]
            }, {
                name: '金燕村',
                type: 'line',
                smooth: true,
                symbol: 'circle',
                symbolSize: 5,
                showSymbol: false,
                lineStyle: {
                    normal: {
                        width: 2
                    }
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(39, 122,206, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(39, 122,206, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#277ace',
                        borderColor: 'rgba(0,136,212,0.2)',
                        borderWidth: 12
                    }
                },
                data: [120, 110, 125, 145, 122, 165, 122, 144, 182, 148, 134, 150]
            }]
        };

        myChart.setOption(option);
    });




    // 流动人口年龄段分布
    $(function () {
        var myChart = echarts.init($("#rode01")[0]);
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['幼年', '少年', '青年', '中年', '老年'],
                textStyle: {
                    color: "#e9ebee"

                }
            },

            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "游客年龄",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '游客年龄',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '幼年',
                            itemStyle: {
                                normal: {
                                    color: '#8CC142'

                                }
                            }
                        },
                        {
                            value: 4, name: '少年',
                            itemStyle: {
                                normal: {
                                    color: '#23977A'

                                }
                            }
                        },
                        {
                            value: 5, name: '青年',
                            itemStyle: {
                                normal: {
                                    color: '#36AFCE'

                                }
                            }
                        },
                        {
                            value: 5, name: '中年',
                            itemStyle: {
                                normal: {
                                    color: '#1A71AF'

                                }
                            }
                        },
                        {
                            value: 6, name: '老年',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });
    //// 流动人员与本地居民分析
    //$(function () {
    //    var myChart = echarts.init($("#bookAret")[0]);
    //    option = {

    //        tooltip: {
    //            trigger: 'axis'
    //        },
    //        grid: {
    //            x: 46,
    //            y: 30,
    //            x2: 30,
    //            y2: 40,
    //            borderWidth: 0
    //        },
    //        legend: {
    //            data: ['流动人员', '本地居民'],
    //            textStyle: {
    //                color: "#e9ebee"

    //            }
    //        },

    //        calculable: false,
    //        xAxis: [
    //            {
    //                type: 'category',
    //                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
    //                axisLabel: {
    //                    show: true,
    //                    textStyle: {
    //                        color: '#a4a7ab',
    //                        align: 'center'
    //                    },
    //                    splitLine: {
    //                        show: false
    //                    },
    //                }
    //            }

    //        ],
    //        yAxis: [
    //            {
    //                type: 'value',
    //                axisLabel: {
    //                    show: true,
    //                    textStyle: {
    //                        color: '#a4a7ab',
    //                        align: 'right'
    //                    }
    //                },
    //                splitLine: {
    //                    show: false
    //                },
    //            }
    //        ],
    //        series: [
    //            {
    //                name: '流动人员',
    //                type: 'bar',
    //                data: [100, 80, 136, 150, 120, 56, 200, 162, 105, 63, 169, 236],
    //                markPoint: {
    //                    data: [
    //                        { type: 'max', name: '最大值' },
    //                        { type: 'min', name: '最小值' }
    //                    ]
    //                },
    //                markLine: {
    //                    data: [
    //                        { type: 'average', name: '平均值' }
    //                    ]
    //                }
    //            },
    //            {
    //                name: '本地居民',
    //                type: 'bar',
    //                data: [983, 820, 1236, 930, 1600, 1032, 890, 1300, 1921, 984, 1960, 2630],
    //                markPoint: {
    //                    data: [
    //                        { name: '月最高', value: 2630, xAxis: 12, yAxis: 2630, symbolSize: 18 },
    //                        { name: '月最低', value: 820, xAxis: 2, yAxis: 830 }
    //                    ]
    //                },
    //                markLine: {
    //                    data: [
    //                        { type: 'average', name: '平均值' }
    //                    ]
    //                }
    //            }
    //        ]
    //    };


    //    myChart.setOption(option);
    //});
    // 流动人口数量分析
    $(function () {
        var myChart = echarts.init($("#bookBmonth")[0]);
        var option = {
            tooltip: {   //提示框，鼠标悬浮交互时的信息提示
                show: true,
                trigger: 'axis'
            },
            grid: {
                x: 30,
                y: 10,
                x2: 20,
                y2: 20,
                borderWidth: 0
            },
            legend: {
                data: [],
                orient: 'vertical',
                textStyle: { fontWeight: 'bold', color: '#a4a7ab' }
            },

            calculable: false,
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }

            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '违章停车数',
                    type: 'line',
                    stack: '订单数',
                    data: [54, 25, 45, 54, 7, 54, 54, 16, 21, 34, 45, 24],
                    itemStyle: {
                        normal: {
                            color: '#02bcbc'
                        }
                    }
                }
            ]
        };

        myChart.setOption(option);
    });
    // 流动人口本地落户分析
    $(function () {
        var myChart = echarts.init($("#whearAbook")[0]);
        option = {
            tooltip: {
                trigger: 'axis'
            },
            grid: {
                x: 35,
                y: 50,
                x2: 20,
                y2: 20,
                borderWidth: 0
            },

            calculable: false,
            legend: {
                data: ['消防主机', '消防设备'],
                textStyle: {
                    color: "#e9ebee"

                }
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }

                }
            ],
            yAxis: [
                {
                    type: 'value',

                    name: '数量',
                    axisLabel: {
                        formatter: '{value} ',
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    },
                    splitLine: {
                        show: false
                    },
                }

            ],
            series: [

                {
                    name: '消防主机',
                    type: 'bar',
                    data: [45, 22, 45, 30, 44, 41, 38, 34, 28, 35, 32, 38],
                    itemStyle: {
                        normal: {
                            color: "#0ad5ff"
                        }
                    }
                },
                {
                    name: '消防设备',
                    type: 'bar',
                    data: [36, 28, 40, 32, 41, 29, 32, 42, 35, 42, 35, 45],
                    itemStyle: {
                        normal: {
                            color: "#005ea1"
                        }
                    }
                }
            ]
        };

        myChart.setOption(option);
    });
    // 城管执法
    $(function () {
        var myChart = echarts.init($("#rodeAbook")[0]);
        option = {
            tooltip: {
                trigger: 'axis'
            },
            grid: {
                x: 30,
                y: 30,
                x2: 30,
                y2: 20,
                borderWidth: 0
            },

            calculable: false,
            legend: {
                data: ['已解决', '为解决', '正在解决'],
                textStyle: {
                    color: "#e9ebee"

                }
            },
            xAxis: [
                {
                    type: 'category',
                    data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }

                }
            ],
            yAxis: [
                {
                    type: 'value',
                    name: '开放停车位',
                    axisLabel: {
                        formatter: '{value} ',
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    },
                    splitLine: {
                        show: false
                    },
                },
                {
                    type: 'value',
                    name: '车流量',
                    axisLabel: {
                        formatter: '{value} ',
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    },
                    splitLine: {
                        show: false
                    },
                },

            ],
            series: [

                {
                    name: '已解决',
                    type: 'bar',
                    stack: '车流量',
                    data: [120, 132, 101, 134, 90, 230, 210],
                    itemStyle: {
                        normal: {
                            color: "#ffcb89"
                        }
                    }
                },
                {
                    name: '未解决',
                    type: 'bar',
                    stack: '车流量',
                    data: [220, 232, 301, 234, 190, 330, 210],
                    itemStyle: {
                        normal: {
                            color: "#005ea1"
                        }
                    }
                },

                {
                    name: '正在解决',
                    type: 'line',
                    yAxisIndex: 1,
                    data: [152, 245, 101, 134, 86, 230, 210],
                    itemStyle: {
                        normal: {
                            color: "#0ad5ff"
                        }
                    }
                }
            ]
        };

        myChart.setOption(option);
    });
    // 居民纠纷事件上报数量分析
    $(function () {
        var myChart = echarts.init($("#seaAbook01")[0]);
        option = {


            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['楼道卫生', '噪音', '宠物', '房屋漏水'], textStyle: {
                    color: "#e9ebee"

                }
            },

            calculable: false,
            series: [

                {
                    name: '居民纠纷事件上报数量分析',
                    type: 'pie',
                    radius: '70%',
                    center: ['50%', '60%'],
                    splitLine: { show: false },
                    roseType: 'area',
                    x: '50%',               // for funnel
                    max: 40,                // for funnel
                    sort: 'ascending',     // for funnel
                    data: [
                        {
                            value: 2560, name: '楼道卫生',
                            itemStyle: {
                                normal: {
                                    color: "#2e7cff"
                                }
                            }
                        },
                        {
                            value: 3690, name: '噪音',
                            itemStyle: {
                                normal: {
                                    color: "#ffcb89"
                                }
                            }
                        },
                        {
                            value: 5690, name: '宠物',
                            itemStyle: {
                                normal: {
                                    color: "#005ea1"
                                }
                            }
                        },
                        {
                            value: 6312, name: '房屋漏水',
                            itemStyle: {
                                normal: {
                                    color: "#0ad5ff"
                                }
                            }
                        }
                    ]
                }
            ]
        };


        myChart.setOption(option);
    });
    // 居民纠纷解决情况分析
    $(function () {
        var myChart = echarts.init($("#actionBook")[0]);
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['民宿', '酒店'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "入驻情况",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '入驻情况',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '民宿',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '酒店',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });
    // 居民纠纷上报记录
    $(function () {
        var myChart = echarts.init($("#sperceBook01")[0]);

        // 指定图表的配置项和数据
        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                x: 30,
                y: 10,
                x2: 10,
                y2: 30,
                borderWidth: 0
            },
            calculable: true,
            xAxis: [
                {
                    type: 'category',
                    data: ['医药企业', '制造业企业', '印刷企业', '电子企业', '其他企业'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }
            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '企业数量',
                    type: 'bar',
                    barWidth: '30',
                    data: [12, 12, 11, 8, 16,],
                    itemStyle: {
                        normal: {
                            color: "#ffea00"
                        }
                    }

                }
            ]
        };


        myChart.setOption(option);
    });


}

function shouRu() {
    // 应急预警统计
    $(function () {
        var myChart = echarts.init($("#zhanbi02")[0]);
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['大学文化', '高中文化', '初中文化', '无学历'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "文化程度",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '文化程度',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '大学文化',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '高中文化',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        },
                        {
                            value: 3, name: '初中文化',
                            itemStyle: {
                                normal: {
                                    color: '#9b04f9'

                                }
                            }
                        },
                        {
                            value: 3, name: '无学历',
                            itemStyle: {
                                normal: {
                                    color: '#ffea00'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });
    // 支出渠道分析
    $(function () {
        var myChart = echarts.init($("#zhanbi03")[0]);
        option = {

            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                },
                formatter: function (params) {
                    var tar = params[0];
                    return tar.name + '<br/>' + tar.seriesName + ' : ' + tar.value;
                }
            },

            xAxis: [
                {
                    type: 'category',
                    splitLine: { show: false },
                    data: ['自然灾害', '事故灾难', '公共卫生事件', '社会安全事件', '旅游突然事件'],
                    axisLabel: {
                        formatter: '{value} ',
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    },
                    splitLine: { show: false }
                }
            ],
            grid: {
                x: 50,
                y: 30,
                x2: 20,
                y2: 20,
                borderWidth: 0
            },
            yAxis: [
                {
                    type: 'value',
                    axisLabel: {
                        formatter: '{value} ',
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    },
                    splitLine: { show: false }
                }
            ],
            series: [
                {
                    name: '完成事件',
                    type: 'bar',
                    stack: '总量',
                    itemStyle: {
                        normal: {
                            color: '#1afffd'
                        }
                    },
                    data: [0, 1700, 1400, 1200, 300, 0]
                },
                {
                    name: '突发事件',
                    type: 'bar',
                    stack: '总量',
                    itemStyle: {
                        normal: {
                            color: '#28a3e1'
                        }
                    },
                    data: [2900, 1200, 300, 200, 900, 300]
                }
            ]
        };


        myChart.setOption(option);
    });
    // 应急处理情况
    $(function () {
        var myChart = echarts.init($("#allAly01")[0]);
        // 指定图表的配置项和数据
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['男', '女'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "男女比例",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '男女比例',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 8, name: '男',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        },
                        {
                            value: 2, name: '女',
                            itemStyle: {
                                normal: {
                                    color: '#ff425d'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });

    // 年龄分析
    $(function () {
        var myChart = echarts.init($("#age01")[0]);
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['60后', '70后', '80后', '90后'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "年龄分析",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '年龄分析',
                    type: 'pie',
                    radius: ['50%', '80%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '60后',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '70后',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        },
                        {
                            value: 3, name: '80后',
                            itemStyle: {
                                normal: {
                                    color: '#9b04f9'

                                }
                            }
                        },
                        {
                            value: 3, name: '90后',
                            itemStyle: {
                                normal: {
                                    color: '#ffea00'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });


    //$(function () {
    //    var myChart = echarts.init($("#allAly02")[0]);
    //    option = {
    //        title: {
    //            text: '应急评价分析',
    //            textStyle: {
    //                color: "#e9ebee"

    //            },

    //            x: 'center'
    //        },
    //        tooltip: {
    //            trigger: 'item',
    //            formatter: "{a} <br/>{b} : {c} ({d}%)"
    //        },
    //        legend: {
    //            orient: 'vertical',
    //            x: 'left',
    //            data: ['优秀', '良好', '一般', '差'],
    //            textStyle: {
    //                color: "#e9ebee"

    //            }
    //        },

    //        calculable: false,
    //        series: [
    //            {
    //                name: '保洁评价分析',
    //                type: 'pie',
    //                radius: '50%',
    //                center: ['50%', '60%'],
    //                data: [
    //                    {
    //                        value: 335, name: '优秀',
    //                        itemStyle: {
    //                            normal: {
    //                                color: '#06b8f8'

    //                            }
    //                        }
    //                    },
    //                    {
    //                        value: 310, name: '良好',
    //                        itemStyle: {
    //                            normal: {
    //                                color: '#ff5c58'

    //                            }
    //                        }
    //                    },
    //                    {
    //                        value: 234, name: '一般',
    //                        itemStyle: {
    //                            normal: {
    //                                color: '#ffffb3'

    //                            }
    //                        }
    //                    },
    //                    {
    //                        value: 135, name: '差',
    //                        itemStyle: {
    //                            normal: {
    //                                color: '#fee581'

    //                            }
    //                        }
    //                    }
    //                ]
    //            }
    //        ]
    //    };


    //    myChart.setOption(option);
    //});
    $(function () {
        var myChart = echarts.init($("#allAly03")[0]);
        var option = {
            tooltip: {   //提示框，鼠标悬浮交互时的信息提示
                show: true,
                trigger: 'axis'
            },
            grid: {
                x: 30,
                y: 10,
                x2: 20,
                y2: 30,
                borderWidth: 0
            },
            legend: {
                data: [],
                orient: 'vertical',
                textStyle: { fontWeight: 'bold', color: '#a4a7ab' }
            },

            calculable: false,
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }

            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '违章停车数',
                    type: 'line',
                    stack: '订单数',
                    data: [54, 25, 45, 54, 7, 54, 54, 16, 21, 34, 45, 24],
                    itemStyle: {
                        normal: {
                            color: '#ffea00'
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });
}

function AnQuan() {
    // 人事变动分析
    $(function () {
        var myChart = echarts.init($("#shijian01")[0]);
        option = {

            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c} ({d}%)"
            },
            legend: {
                orient: 'vertical',
                x: 'left',
                data: ['游客已登记', '游客滞留', '游客已疏散'],
                textStyle: {
                    color: "#e9ebee"
                }
            },
            calculable: false,
            graphic: {       //图形中间文字
                type: "text",
                left: "center",
                top: "center",
                style: {
                    text: "住宿比例",
                    textAlign: "center",
                    fill: "#fff",
                    fontSize: 20
                }
            },
            series: [
                {
                    name: '住宿比例',
                    type: 'pie',
                    radius: ['40%', '60%'],
                    center: ['50%', '50%'],
                    data: [
                        {
                            value: 5, name: '游客已登记',
                            itemStyle: {
                                normal: {
                                    color: '#00ffff'

                                }
                            }
                        },
                        {
                            value: 4, name: '游客滞留',
                            itemStyle: {
                                normal: {
                                    color: '#ffea00'

                                }
                            }
                        },
                        {
                            value: 5, name: '游客已疏散',
                            itemStyle: {
                                normal: {
                                    color: '#0a8cff'

                                }
                            }
                        }
                    ]
                }
            ]
        };

        myChart.setOption(option);
    });
    // 党员情况分析
    $(function () {
        var myChart = echarts.init($("#shijian02")[0]);
        var option = {
            tooltip: {   //提示框，鼠标悬浮交互时的信息提示
                show: true,
                trigger: 'axis'
            },
            grid: {
                x: 30,
                y: 10,
                x2: 20,
                y2: 20,
                borderWidth: 0
            },
            legend: {
                data: [],
                orient: 'vertical',
                textStyle: { fontWeight: 'bold', color: '#a4a7ab' }
            },

            calculable: false,
            xAxis: [
                {
                    type: 'category',
                    boundaryGap: false,
                    data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'center'
                        }
                    }
                }

            ],
            yAxis: [
                {
                    type: 'value',
                    splitLine: {
                        show: false
                    },
                    axisLabel: {
                        show: true,
                        textStyle: {
                            color: '#a4a7ab',
                            align: 'right'
                        }
                    }
                }
            ],
            series: [
                {
                    name: '违章停车数',
                    type: 'line',
                    stack: '订单数',
                    data: [54, 25, 45, 54, 7, 54, 54, 16, 21, 34, 45, 24],
                    itemStyle: {
                        normal: {
                            color: '#ffea00'
                        }
                    }
                }
            ]
        };


        myChart.setOption(option);
    });


    // 党员年龄段分析
    $(function () {
        var myChart = echarts.init($("#shijian03")[0]);
        var labelTop = {
            normal: {
                label: {
                    show: true,
                    position: 'center',
                    formatter: '{b}',
                    textStyle: {
                        baseline: 'bottom'
                    }
                },
                labelLine: {
                    show: false
                }
            }
        };
        var labelFromatter = {
            normal: {
                label: {
                    formatter: function (params) {
                        return 100 - params.value + '%'
                    },
                    textStyle: {
                        baseline: 'top'
                    }
                }
            },
        }
        var labelBottom = {
            normal: {
                color: '#111b21',
                label: {
                    show: true,
                    position: 'center'
                },
                labelLine: {
                    show: false
                }
            },
            emphasis: {
                color: 'rgba(0,0,0,0)'
            }
        };
        var radius = [40, 55];
        option = {
            legend: {
                x: 'center',

                textStyle: {
                    color: "#fff"

                },
                data: [
                    '60后', '70后', '80后', '90后', '00后'
                ]
            },


            series: [
                {
                    type: 'pie',
                    center: ['10%', '30%'],
                    radius: radius,
                    x: '0%', // for funnel
                    itemStyle: labelFromatter,
                    data: [
                        { name: '', value: 46, itemStyle: labelBottom },
                        { name: '60后', value: 60, itemStyle: labelTop }
                    ],
                    itemStyle: {
                        normal: {
                            color: "#0ad5ff"
                        }
                    }
                },
                {
                    type: 'pie',
                    center: ['30%', '30%'],
                    radius: radius,
                    x: '20%', // for funnel
                    itemStyle: labelFromatter,
                    data: [
                        { name: '', value: 56, itemStyle: labelBottom },
                        { name: '70后', value: 44, itemStyle: labelTop }
                    ],
                    itemStyle: {
                        normal: {
                            color: "#ffcb89"
                        }
                    }
                },
                {
                    type: 'pie',
                    center: ['50%', '30%'],
                    radius: radius,
                    x: '40%', // for funnel
                    itemStyle: labelFromatter,
                    data: [
                        { name: '', value: 65, itemStyle: labelBottom },
                        { name: '80后', value: 35, itemStyle: labelTop }
                    ],
                    itemStyle: {
                        normal: {
                            color: "#2e7cff"
                        }
                    }
                },
                {
                    type: 'pie',
                    center: ['70%', '30%'],
                    radius: radius,
                    x: '60%', // for funnel
                    itemStyle: labelFromatter,
                    data: [
                        { name: '', value: 70, itemStyle: labelBottom },
                        { name: '90后', value: 30, itemStyle: labelTop }

                    ],
                    itemStyle: {
                        normal: {
                            color: "#4cffd3"
                        }
                    }
                },


                {
                    type: 'pie',
                    center: ['90%', '30%'],
                    radius: radius,
                    // for funnel
                    x: '80%', // for funnel
                    itemStyle: labelFromatter,
                    data: [
                        { name: '', value: 70, itemStyle: labelBottom },
                        { name: '00后', value: 11, itemStyle: labelTop }
                    ],
                    itemStyle: {
                        normal: {
                            color: "#feb602"
                        }
                    }
                }
            ]
        };
        myChart.setOption(option);
    });
    // 党员分布情况分析
    $(function () {
        var myChart = echarts.init($("#shijian04")[0]);
        option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    lineStyle: {
                        color: '#57617B'
                    }
                }
            },
            legend: {

                //icon: 'vertical',
                data: ['前一天降水量', '当前降水量'],
                //align: 'center',
                // right: '35%',
                top: '0',
                textStyle: {
                    color: "#fff"
                },
                // itemWidth: 15,
                // itemHeight: 15,
                itemGap: 20,
            },
            grid: {
                left: '0',
                right: '20',
                top: '10',
                bottom: '0',
                containLabel: true
            },
            xAxis: [{
                type: 'category',
                boundaryGap: false,
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255,255,255,.6)'
                    }
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                },
                data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
            }, {




            }],
            yAxis: [{
                axisLabel: {
                    show: true,
                    textStyle: {
                        color: 'rgba(255,255,255,.6)'
                    }
                },
                axisLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                },
                splitLine: {
                    lineStyle: {
                        color: 'rgba(255,255,255,.1)'
                    }
                }
            }],
            series: [{
                name: '前一天降水量',
                type: 'line',
                smooth: true,
                symbol: 'circle',
                symbolSize: 5,
                showSymbol: false,
                lineStyle: {
                    normal: {
                        width: 2
                    }
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(24, 163, 64, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(24, 163, 64, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#ff0000',
                        borderColor: 'rgba(137,189,2,0.27)',
                        borderWidth: 12
                    }
                },
                data: [120, 58, 142, 134, 150, 120, 110, 125, 145, 122, 165, 122]
            }, {
                name: '当前降水量',
                type: 'line',
                smooth: true,
                symbol: 'circle',
                symbolSize: 5,
                showSymbol: false,
                lineStyle: {
                    normal: {
                        width: 2
                    }
                },
                areaStyle: {
                    normal: {
                        color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [{
                            offset: 0,
                            color: 'rgba(39, 122,206, 0.3)'
                        }, {
                            offset: 0.8,
                            color: 'rgba(39, 122,206, 0)'
                        }], false),
                        shadowColor: 'rgba(0, 0, 0, 0.1)',
                        shadowBlur: 10
                    }
                },
                itemStyle: {
                    normal: {
                        color: '#00ffff',
                        borderColor: 'rgba(0,136,212,0.2)',
                        borderWidth: 12
                    }
                },
                data: [120, 110, 125, 145, 122, 165, 122, 144, 182, 148, 134, 150]
            }]
        };


        myChart.setOption(option);
    });
}