"use script";

/**
 * MapX核心模块
 * @description MapX核心模块
 * @author LYW
 */
(function (window) {

    const key1 = CryptoJS.enc.Utf8.parse("C0EDDB8F25A841DC");
    const v1 = CryptoJS.enc.Utf8.parse("94355E7A4BCCECE0");
    const key2 = CryptoJS.enc.Utf8.parse("479543A8E8863866");
    const v2 = CryptoJS.enc.Utf8.parse("62E8CFBEE90FE5D2");

    // const keyTime = CryptoJS.enc.Utf8.parse("C0EDDB8F25A841DC");
    // const ivTime = CryptoJS.enc.Utf8.parse("94355E7A4BCCECE0");

    //key2加密的机器码
    const encryptedKey2MachineId = "F980CDB46A3CC7FE28D8F7854C7800E8FA62693C300A9BE86A799C2A0D9FFC7CFF6B1032420940D94E3EA07326DCD8802C9CA243B0223F4D90C0ECC27D6F642ADF93ADF88452BC2AD54398EE87E0BA5B";
    const encryptedKeyTime = "B3D7218F1A7A96B8D1F6E6E16B0DFAFF";

    var dataType = "";
    var viewer = null;

    //===========================核心业务===========================

    /**
     * 初始化地图
     * @param {String} api WebAPI地址
     * @param {Object} [options={}] 地图配置数据
     * @param {String} [options.container] 地图容器
     * @param {String} [options.animation] 开场动画
     * @param {String} [options.data_type] 数据类型
     * @public
     */
    function createMap(api, options) {

        if (api && typeof api === "string") {
            MapX.api.setUrl(api);
        } else if (api && typeof api === "object") {
            options = api;
        }

        if (!options) {
            if (options.error) {
                options.error("Error: 无效参数!");
            }
            return;
        }

        //判断WebAPI是否配置
        if (MapX.api.getUrl() === "") {
            console.log("%cError: WebAPI地址未指定!", "color: rgb(255, 0, 0)");

            if (options.error) {
                options.error("Error: WebAPI地址未指定!");
            }
            return;
        }

        //验证授权
        var result = authorize();
        if (result) {

        } else {
            if (options.error) {
                options.error("MapX未授权!");
            }
            console.log("%cError: MapX未授权!", "color: rgb(255, 0, 0)");
        }

        //地图模式
        window.MapX.core.data_type = dataType = options.data_type;
        setDataType(dataType);

        //初始化接口数据
        initData(options);

    }

    /**
     * 初始化数据
     * @description 获取地图数据
     * @private
     */
    function initData(options) {

        var ajax_sys_config = $.ajax(MapX.api.sys_config, { type: "get" });                 //平台配置
        var ajax_map_model_list = $.ajax(MapX.api.map_model_list, { type: "post" });        //模型信息
        var ajax_map_build_list = $.ajax(MapX.api.map_build_list, { type: "get" });         //建筑信息
        var ajax_map_floor_list = $.ajax(MapX.api.map_floor_list, { type: "post" });        //楼层信息
        var ajax_map_under_list = $.ajax(MapX.api.map_under_list, { type: "get" });         //地下模型

        /**
         * 获取平台数据
         * ajax_sys_config：平台配置
         * ajax_map_model_list：模型信息
         * ajax_map_build_list：建筑信息
         * ajax_map_floor_list：楼层信息
         * ajax_map_under_list：地下模型
         */
        $.when(ajax_sys_config, ajax_map_model_list, ajax_map_build_list, ajax_map_floor_list, ajax_map_under_list).then(function (rlt_sys_config, rlt_map_model_list, rlt_map_build_list, rlt_map_floor_list, rlt_map_under_list) {
            //数据获取成功
            if (rlt_sys_config[1] === "success" && rlt_map_model_list[1] === "success" && rlt_map_build_list[1] === "success" && rlt_map_floor_list[1] === "success" && rlt_map_under_list[1] === "success") {

                var sys_config = MapX.tool.verifyResponse(rlt_sys_config[0]);
                var map_model_list = MapX.tool.verifyResponse(rlt_map_model_list[0]);
                var map_build_list = MapX.tool.verifyResponse(rlt_map_build_list[0]);
                var map_floor_list = MapX.tool.verifyResponse(rlt_map_floor_list[0]);
                var map_under_list = MapX.tool.verifyResponse(rlt_map_under_list[0]);

                //加载楼层数据
                MapX.tool.loadBuildData(map_build_list, map_floor_list, map_under_list);

                if (!map_model_list) {
                    //数据未授权
                    console.log("%cError: " + rlt_map_model_list[0].msg, "color: rgb(255, 0, 0)");
                    return;
                }

                initMap(sys_config, map_model_list, options);

                initWidget();

                if (options.complete) {
                    options.complete();
                }
            }
        });
    }

    /**
     * 初始化地图
     * @param {Object} sys_config 平台配置
     * @param {Array} map_model_list 模型列表
     * @param {Object} [options={}] 地图配置数据
     * @param {String} [options.container] 地图容器
     * @param {String} [options.animation] 开场动画
     * @param {String} [options.data_type] 数据类型
     */
    function initMap(sys_config, map_model_list, options) {
        /**
         * 地图加载配置
         */
        var configOptions = {
            serverURL: sys_config.data_server_uri,
            map3d: {
                homeButton: false,
                navigationHelpButton: false,
                fullscreenButton: false,
                center: sys_config.center_location,
                basemaps: [
                    {
                        id: "normal",
                        name: "ArcGIS 切片",
                        type: "arcgis_cache",
                        url: "$serverURL$/normal/tiles/arcgis-img/L{arc_z}/R{arc_y}/C{arc_x}.png",
                        visible: (options.data_type === "normal")
                    },
                    {
                        id: "virtual",
                        name: "ArcGIS 切片",
                        type: "arcgis_cache",
                        url: "$serverURL$/virtual/tiles/arcgis-img/L{arc_z}/R{arc_y}/C{arc_x}.png",
                        visible: (options.data_type === "virtual")
                    }
                ],
                operationallayers: []
            }
        }

        //拼接数据
        for (var i = 0; i < map_model_list.length; i++) {
            var item = map_model_list[i];
            var layer = {
                id: item.id,
                pid: item.pid,
                name: item.node_name
            }
            if (item.node_type === "group") {
                layer.type = "group";
            } else if (item.node_type === "3dtiles") {
                layer.type = "3dtiles";
                layer.url = item.data_url.replace("$serverURL$", "$serverURL$/" + item.data_type);

                layer.offset = { z: -90 };

                //判断是否显示
                if (options.data_type === item.data_type) {
                    //是否为室内，室内模型则隐藏
                    if (MapX.tool.isBuild(layer.id)) {
                        //判断是否为外壳
                        if (MapX.tool.isShell(layer.id)) {
                            layer.visible = true;
                        } else {
                            layer.visible = false;
                        }
                    } else {
                        layer.visible = true;
                    }
                }
            }

            //添加到operationallayers
            configOptions.map3d.operationallayers.push(layer);
        }

        //创建地图
        var viewer = mapv3d.createMap({
            id: options.container,
            data: configOptions.map3d,
            serverURL: configOptions.serverURL
        });
        //viewer.imageryLayers.get(0).show = false;

        //options.animation
        if (options.animation) {
            viewer.MapX.openFlyAnimation(options.animationEndFun);
        }

        viewer.scene.postProcessStages.fxaa.enabled = true;

        //开启深度监测
        viewer.scene.globe.depthTestAgainstTerrain = true;

        // 亮度设置
        // var stages = viewer.scene.postProcessStages;
        // viewer.scene.brightness = viewer.scene.brightness || stages.add(Cesium.PostProcessStageLibrary.createBrightnessStage());
        // viewer.scene.brightness.enabled = true;
        // viewer.scene.brightness.uniforms.brightness = Number(1.2);

        //添加事件
        var handler = new Cesium.ScreenSpaceEventHandler(viewer.canvas);
        handler.setInputAction(function (event) {
            var camera = mapv3d.point.getCameraView(viewer);
            //相机视角
            console.log(JSON.stringify(camera));
            //-----------------------------------------
            var point = MapX.util.cartesian2ToToWGS84(event.position);
            //鼠标视角
            console.log(JSON.stringify(point));

        }, Cesium.ScreenSpaceEventType.LEFT_CLICK);

        //设置viewer
        setViewer(viewer);
    }

    /**
     * 初始化widget相关
     */
    function initWidget() {
        var widgetCfg = {
            "debugger": false,
            "version": "20200102",
            "defaultOptions": {
                "style": "dark",
                "windowOptions": {
                    "skin": "layer-mapv-dialog animation-scale-up",
                    "position": {
                        "top": 50,
                        "right": 10
                    },
                    "maxmin": false,
                    "resize": true
                },
                "autoReset": false,
                "autoDisable": true,
                "disableOther": true
            },
            "widgetsAtStart": [],
            "widgets": []
        }
        mapv3d.widget.init(getViewer(), widgetCfg);
    }

    /**
     * 授权认证
     * @description Core私有方法，不对外开放
     * @private
     */
    function authorize() {
        //接口地址
        var sys_authorize_machine = MapX.api.sys_authorize_machine;

        //日期加密
        // const encryptedTime = haoutil.aes.encrypt(new Date().format("yyyy-MM-dd"), keyTime, ivTime);
        const encryptedTime = haoutil.aes.encrypt($.date.now("yyyy-MM-dd"), key1, v1);

        //授权验证
        var result = haoutil.ajax.sync(sys_authorize_machine, { timestamp: encryptedTime });
        //验证结果
        var data = MapX.tool.verifyResponse(result);

        if (data) {
            //验证授权时间
            var date = haoutil.aes.decrypt(encryptedKeyTime, key1, v1);
            const authDate = new Date(date);
            const clientDate = Date.now();
            if (authDate < clientDate) {
                //授权过期
                return false;
            }

            //key1加密的机器码
            const encryptedKey1MachineId = data.machine_id;
            //解密机器码
            const machineId = haoutil.aes.decrypt(encryptedKey1MachineId, key1, v1);
            //key2加密机器码
            const encryptedMachineId = haoutil.aes.encrypt(machineId, key2, v2);
            if (encryptedMachineId === encryptedKey2MachineId) {
                //已授权
                return true;
            } else {
                //未授权
                return false;
            }
        } else {
            //获取机器码失败
            console.log("获取机器码失败!");
        }
    }

    /**
     * 获取viewer
     * @public
     */
    function getViewer() {
        return viewer;
    }

    /**
     * 设置viewer
     * @param {Viewer} _viewer viewer
     */
    function setViewer(_viewer) {
        viewer = _viewer;
    }

    /**
     * 获取数据类型
     */
    function getDataType() {
        return dataType;
    }

    /**
     * 设置数据类型
     * @param {String} dataType 数据类型
     */
    function setDataType(_dataType) {
        dataType = _dataType;
    }

    //=====================================图层操作相关======================================

    /**
     * 获取所有图层
     * @public
     */
    function operationallayers() {
        return getViewer().mapv.arrOperationallayers;
    }

    /**
     * 获取layer
     * @param {String} key 字段值
     * @param {String} keyname 字段名称
     * @param {String} property 属性名称
     * @public
     */
    function getLayer(key, keyname, property) {
        var layer = getViewer().mapv.getLayer(key, keyname);
        if (property && layer[property]) {
            return layer[property];
        }
        return layer
    }

    window.MapX.core = {
        createMap: createMap,
        getViewer: getViewer,
        getDataType: getDataType,
        setDataType: setDataType,
        operationallayers: operationallayers,
        getLayer: getLayer
    };
})(window);