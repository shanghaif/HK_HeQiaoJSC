/* 2017-12-4 08:27:25 | 修改 LYW */
//模块：视频窗口
MapX.widget.bindClass(MapX.widget.BaseWidget.extend({
    options: {
        //弹窗
        view: {
            type: "window",
            url: "view.html",
            windowOptions: {
                width: 500,
                height: 400,
                position: {
                    top: 50,
                    right: 50
                }
            }
        }
    },
    //初始化[仅执行1次]
    create: function () {

    },
    viewWindow: null,
    //每个窗口创建完成后调用
    winCreateOK: function (opt, result) {
        this.viewWindow = result;
    },
    //打开激活
    activate: function () {

    },
    //关闭释放
    disable: function () {
        this.viewWindow = null;

    }
}));
