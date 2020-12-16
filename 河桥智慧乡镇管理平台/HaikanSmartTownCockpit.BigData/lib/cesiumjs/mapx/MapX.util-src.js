"use script";

(function () {

    //===========================系统级============================

    /**
     * 检测浏览器webgl支持
     * @public
     */
    function webglreport() {
        return mapv3d.util.webglreport();
    }

    //===========================地图操作相关=============================

    /**
     * 复位
     * @param {Object} option 定位选项
     * @public
     */
    function home(option) {
        if (option) {
            MapX.camera.centerAt(MapX.core.getViewer().mapv.config.center, option);
        } else {
            MapX.core.getViewer().mapv.centerAt();
        }
    }

    /**
     * 键盘漫游
     * @param {Bool} enable 是否开启
     * @public
     */
    function keyboardRoam(enable) {
        MapX.core.getViewer().mapv.keyboardRoam.enable = enable;
    }

    //===========================坐标转换================================

    /**
     * 经纬度坐标转笛卡尔三维坐标
     * @param {Object|Array} position 经纬度坐标
     * @returns 笛卡尔三维坐标
     * @public
     */
    function wgs84ToCartesian3(position) {
        if (haoutil.isutil.isObject(position)) {
            position = Cesium.Cartesian3.fromDegrees(parseFloat(position.x), parseFloat(position.y), parseFloat(position.z));
        } else if (haoutil.isutil.isArray(position)) {
            var positions = haoutil.system.clone(position);
            position = [];
            for (var i = 0; i < positions.length; i++) {
                position.push(Cesium.Cartesian3.fromDegrees(parseFloat(positions[i].x), parseFloat(positions[i].y), parseFloat(positions[i].z)));
            }
        }
        return position;
    }

    /**
     * 笛卡尔三维坐标转经纬度坐标
     * @param {Object|Array} cartesian3 笛卡尔三维坐标
     * @returns 经纬度坐标
     * @public
     */
    function cartesian3ToWGS84(position) {
        if ((typeof position == "object") && position.constructor == Cesium.Cartesian3) {
            position = cartesian(position);
        } else if (haoutil.isutil.isArray(position)) {
            var positions = haoutil.system.clone(position);
            position = [];
            for (var i = 0; i < positions.length; i++) {
                position.push(cartesian(positions[i]));
            }
        }

        /**
         * 笛卡尔坐标转经纬度坐标方法
         * @param {Object} cartesian3 笛卡尔坐标
         * @returns 经纬度坐标
         */
        function cartesian(cartesian3) {

            var cartographic = Cesium.Cartographic.fromCartesian(cartesian3);

            function Point() {
                this.x = "";
                this.y = "";
                this.z = "";
            }

            Point.prototype.format = function () {
                this.x = parseFloat(this.x);
                this.y = parseFloat(this.y);
                this.z = parseFloat(this.z);
                return {
                    x: this.x,
                    y: this.y,
                    z: this.z,
                }
            }
            var point = new Point();
            point.x = Cesium.Math.toDegrees(cartographic.longitude).toFixed(6);
            point.y = Cesium.Math.toDegrees(cartographic.latitude).toFixed(6);
            point.z = cartographic.height.toFixed(1);
            return point.format();
        }

        return position;
    }

    /**
     * 笛卡尔二维坐标转笛卡尔三维坐标
     * @param {Object} cartesian2 笛卡尔二维坐标
     */
    function cartesian2ToCartesian3(cartesian2) {
        var cartesian3 = mapv3d.point.getCurrentMousePosition(MapX.core.getViewer().scene, {
            x: cartesian2.x,
            y: cartesian2.y
        });
        return cartesian3;
    }

    /**
     * 笛卡尔二维坐标转经纬度坐标
     * @param {Object} cartesian2 笛卡尔二维坐标
     * @returns 经纬度坐标
     */
    function cartesian2ToToWGS84(cartesian2) {
        var cartesian3 = mapv3d.point.getCurrentMousePosition(MapX.core.getViewer().scene, {
            x: cartesian2.x,
            y: cartesian2.y
        });
        return cartesian3ToWGS84(cartesian3);
    }

    window.MapX.util = {
        webglreport: webglreport,
        home: home,
        keyboardRoam: keyboardRoam,
        wgs84ToCartesian3: wgs84ToCartesian3,
        cartesian3ToWGS84: cartesian3ToWGS84,
        cartesian2ToCartesian3: cartesian2ToCartesian3,
        cartesian2ToToWGS84: cartesian2ToToWGS84
    }
})();