var thisWidget;

//当前页面业务
function initWidgetView(_thisWidget) {
    thisWidget = _thisWidget;
    console.log("相机编码：" + thisWidget.config.data.device_code);
}