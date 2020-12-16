"use script";

/**
 * 业务工具类
 */
(function (window) {

    /**
     * 验证数据
     * @param {Object} result 数据
     * @description 验证WebAPI返回数据
     */
    function verifyResponse(result) {
        if (result.code === 0) {
            return result.data;
        } else {
            return false
        }
    }

    //==================================建筑楼层相关==================================

    /**
     * @private
     */
    var buildData = { list: [], obj: {} }, floorData = { list: [], obj: {} }, underData = { list: [], obj: {} };

    /**
     * 获取建筑
     */
    function getBuildData() {
        return buildData;
    }

    /**
     * 获取楼层
     */
    function getFloorData() {
        return floorData;
    }

    /**
     * 获取楼层数据
     * @param {Array} build 建筑列表
     * @public
     */
    function loadBuildData(builds, floors, unders) {
        buildData.list = builds;
        var cFloors = haoutil.system.clone(floors);
        for (var i = 0; i < buildData.list.length; i++) {
            var build = builds[i];
            buildData.obj[build.id] = build;
            buildData.obj[build.id].floors = [];

            cFloors.forEach(floor => {
                if (floor.build_id === build.id) {
                    buildData.obj[build.id].floors.push(floor);
                }
            });
        }

        floorData.list = floors;
        for (var i = 0; i < floorData.list.length; i++) {
            var floor = floors[i];
            floorData.obj[floor.id] = floor;
        }

        underData.list = unders;
        for (var i = 0; i < underData.list.length; i++) {
            var under = unders[i];
            underData.obj[under.id] = floor;
        }
    }

    /**
     * 获取建筑信息
     * @param {String} buildId 建筑Id
     */
    function getBuild(buildId) {
        return floorData.obj[floorId];
    }

    /**
     * 获取楼层信息
     * @param {String} floorId 楼层Id
     */
    function getFloor(floorId) {
        return floorData.obj[floorId];
    }

    /**
     * 判断是否为室内模型
     * @param {String} id 模型Id
     */
    function isBuild(id) {
        id = id.replace("_normal", "").replace("_virtual", "");
        if (floorData.obj.hasOwnProperty(id)) {
            return true;
        }
        return false;
    }

    /**
     * 判断是否为外壳
     * @param {String} id 模型Id
     */
    function isShell(id) {
        id = id.replace("_normal", "").replace("_virtual", "");
        var floor = getFloor(id);
        var floorArr = buildData.obj[floor.build_id].floors;
        var floorPop = floorArr[floorArr.length - 1];

        if (floor.id === floorPop.id) {
            return true;
        }
    }

    /**
     * 获取建筑外壳模型
     * @param {String} buildId 建筑Id
     */
    function getShell(buildId, property) {
        var floorArr = buildData.obj[buildId].floors;
        var floorPop = floorArr[floorArr.length - 1];
        //当前模型类型
        var data_type = MapX.core.getDataType();
        var layer = MapX.core.getLayer(floorPop.id + "_" + data_type, "id", property);
        return layer;
    }

    /**
     * 建筑分层
     * @param {String} buildId 建筑Id
     * @param {String} floorId 楼层Id
     * @public
     */
    function liftBuild(buildId, floorId) {
        //当前模型类型
        var data_type = MapX.core.getDataType();
        var floors = buildData.obj[buildId].floors;

        //隐藏外壳
        var floorPop = floors[floors.length - 1];
        if (floorId === floorPop.id) {
            //建筑楼层复位
            resetBuild(buildId, floorId);
            return;
        }
        var layerPop = MapX.core.getLayer(floorPop.id + "_" + data_type, "id");
        layerPop.setVisible(false);

        var visbile = false;
        floors.forEach(floor => {
            var layer = MapX.core.getLayer(floor.id + "_" + data_type, "id");
            if (!visbile) {
                layer.setVisible(true);
            } else {
                layer.setVisible(false);
            }

            if (floorId === floor.id) {
                visbile = true;
            }
        });
    }

    /**
     * 取消建筑分层
     * @param {String} buildId 建筑Id
     * @public
     */
    function resetBuild(buildId) {
        //当前模型类型
        var data_type = MapX.core.getDataType();

        var floors = buildData.obj[buildId].floors;
        floors.forEach((floor, index) => {
            var layer = MapX.core.getLayer(floor.id + "_" + data_type, "id");
            if (floors.length - 1 === index) {
                layer.setVisible(true);
            } else {
                layer.setVisible(false);
            }
        });
    }

    /**
     * 地下模式
     * @public
     */
    function undergroundMode(enable) {
        //地图图层
        var operationallayers = MapX.core.operationallayers();
        if (enable) {
            //显示地形开挖

            //开启地下模式
            for (var i = 0; i < operationallayers.length; i++) {
                var layer = operationallayers[i];
                if (underData.obj.hasOwnProperty(layer.config.id.replace("_normal", "").replace("_virtual", ""))) {
                    layer.setVisible(true);
                } else {
                    layer.setVisible(false);
                }
            }
        } else {
            for (var i = 0; i < operationallayers.length; i++) {
                var layer = operationallayers[i];
                layer.setVisible(true);
            }
            //隐藏室内楼层
            buildData.list.forEach(build => {
                resetBuild(build.id);
            });

            //关闭地形开挖

            //当前模型类型
            var data_type = MapX.core.getDataType();
            //隐藏地下
            underData.list.forEach(under => {
                var layer = MapX.core.getLayer(under.id + "_" + data_type, "id");
                layer.setVisible(false);
            });
        }
    }

    window.MapX.tool = {
        verifyResponse: verifyResponse,
        loadBuildData: loadBuildData,
        isBuild: isBuild,
        isShell: isShell,
        getShell: getShell,
        liftBuild: liftBuild,
        resetBuild: resetBuild,
        undergroundMode: undergroundMode,
        getBuildData: getBuildData,
        getFloorData: getFloorData
    }
})(window);