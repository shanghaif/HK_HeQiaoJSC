"use strict";

var drawCtrl = {};
var drawControl_device_model = {};
var drawControl_device_billboard = {};
//=====================================三维地图=======================================

/**
 * 创建地图
 */
function createMap() {
    if (MapX.util.webglreport()) {
        // MapX.api.setUrl("http://192.168.0.228:8080/api/3301120101002/");
        MapX.api.setUrl("http://172.18.19.212/api/aimap/");
        MapX.core.createMap({
            container: "cesiumContainer",
            animation: false,
            animationEndFun: function () {
            },
            data_type: "normal",//normal|virtual
            complete: function () {
                MapX.util.keyboardRoam(true);
                drawControl_device_model = new MapX.Draw({ hasEdit: false });
                drawControl_device_billboard = new MapX.Draw({ hasEdit: false });

            },
            error: function (msg) {
                //错误事件
                toastr.error(msg);
            }
        });
    }
}


/**
 * 创建地图
 */
function createMap(id) {
    if (MapX.util.webglreport()) {
        // MapX.api.setUrl("http://192.168.0.228:8080/api/3301120101002/");
        MapX.api.setUrl("http://172.18.19.212/api/aimap/");

        MapX.core.createMap({
            container: id,
            animation: false,
            animationEndFun: function () {
            },
            data_type: "normal",//normal|virtual
            complete: function () {
                MapX.util.keyboardRoam(true);
                drawControl_device_model = new MapX.Draw({ hasEdit: false });
                drawControl_device_billboard = new MapX.Draw({ hasEdit: false });
                loadDevice();
                addGeoJsonLayer();

            },
            error: function (msg) {
                //错误事件
                toastr.error(msg);
            }
        });
    }
}

function createMap2(id) {
    if (MapX.util.webglreport()) {
        // MapX.api.setUrl("http://192.168.0.228:8080/api/3301120101002/");
        MapX.api.setUrl("http://172.18.19.212/api/aimap/");

        MapX.core.createMap({
            container: id,
            animation: false,
            animationEndFun: function () {
            },
            data_type: "normal",//normal|virtual
            complete: function () {
                MapX.util.keyboardRoam(true);
                drawControl_device_model = new MapX.Draw({ hasEdit: false });
                drawControl_device_billboard = new MapX.Draw({ hasEdit: false });
                // loadDevice();
                // addGeoJsonLayer();

            },
            error: function (msg) {
                //错误事件
                toastr.error(msg);
            }
        });
    }
}

//=====================================三维地图=======================================

/**
 * 复位
 */
function home() {
    MapX.util.home();
}

/**
 * 键盘漫游
 */
var keyboard = false;
function keyboardRoam() {
    keyboard = !keyboard;
    MapX.util.keyboardRoam(keyboard);
}

function center() {
    MapX.widget.activate("widgets/centerXY/widget.js");
}

//====================================空间量算====================================

var measureControl = null;

/**
 * 获取测量控件
 */
function getMeasureControl() {
    if (!measureControl) {
        measureControl = new MapX.measure();
    }
    return measureControl;
}

/**
 * 测距
 * @param {Boolean} clampToGround 是否贴地
 */
function measuerLength(clampToGround) {
    getMeasureControl().measuerLength({
        terrain: clampToGround,
        addHeight: 0.5
    })
}

/**
 * 测面积
 * @param {Boolean} clampToGround 是否贴地
 */
function measureArea(clampToGround) {
    getMeasureControl().measureArea({
        terrain: clampToGround,
        addHeight: 0.5
    })
}

/**
 * 测高
 * @param {Boolean} isSuper 测量模式
 */
function measureHeight(isSuper) {
    getMeasureControl().measureHeight({
        isSuper: isSuper,
        addHeight: 0.5
    })
}

/**
 * 清空测量结果
 */
function clearMeasure() {
    getMeasureControl().clearMeasure();
}


/**
 * 加载设备
 */
function loadDevice() {

    drawControl_device_model.deleteAll();

    $.ajax(
        {
            url: baseUrl + 'api/v1/AppTongji/AppTongji/getThreemiaodian',
            method: 'get',
            success: function (result) {
                if (result && result.data) {
                    console.log(111111111);
                    console.log(result);
                    for (let i = 0; i < result.data.length; i++) {
                        const item = result.data[i];
                        var drawControl = drawControl_device_model;
                        if (drawControl) {
                            // 不同的设备，放在不同的层
                            item.options.center = item.center;
                            var style = {};
                            // style.url = "http://192.168.0.228:8080/model/gltf/WBZQJ.gltf";
                            if (item.shebeiAddress.indexOf('球机')==-1){
                                style.url="http://172.18.19.212/model/gltf/WBZQJ.gltf";
                            }else{
                                style.url="http://172.18.19.212/model/gltf/BGQ.gltf";
                            }
                            
                            var option = deviceOption(item, style, item.center);
                            item.options.position.z -= 85;
                            var entity = drawControl.attrToEntity(option, item.options.position);
                            entity.data = item;
                            (function () {
                                entity.click = function (e) {
                                    alert();
                                }
                            })(entity)
                        }
                    }
                    toastr.info("设备加载完成");
                    console.log("设备加载完成");
                    loadBillboard2();
                }
            },
            error: function (result) {
                console.log("数据请求失败")
                console.log(result);
            }
        });

        $.ajax(
            {
                url: 'http://112.17.130.233:4474/api/v1/AppTongji/AppTongji/getThreemiaodian',
                method: 'get',
                success: function (result) {
                    if (result && result.data) {
                        console.log(111111111);
                        console.log(result);
                        for (let i = 0; i < result.data.length; i++) {
                            const item = result.data[i];
                            var drawControl = drawControl_device_model;
                            if (drawControl) {
                                // 不同的设备，放在不同的层
                                item.options.center = item.center;
                                var style = {};
                                // style.url = "http://192.168.0.228:8080/model/gltf/WBZQJ.gltf";
                                if (item.shebeiAddress.indexOf('球机')==-1){
                                    style.url="http://172.18.19.212/model/gltf/WBZQJ.gltf";
                                }else{
                                    style.url="http://172.18.19.212/model/gltf/BGQ.gltf";
                                }
                                var option = deviceOption(item, style, item.center);
                                item.options.position.z -= 85;
                                var entity = drawControl.attrToEntity(option, item.options.position);
                                entity.data = item;
                                (function () {
                                    entity.click = function (e) {
                                        alert();
                                    }
                                })(entity)
                            }
                        }
                        toastr.info("设备加载完成");
                        console.log("设备加载完成");
                        loadBillboard2();
                    }
                },
                error: function (result) {
                    console.log("数据请求失败")
                    console.log(result);
                }
            });
    //加载设备列表
    //MapX.api.post(MapX.api.device_info_list, { on_map: true }, function (result) {
    //    if (result && result.data) {
    //        console.log(111111111);
    //        console.log(result);
    //        for (let i = 0; i < result.data.length; i++) {
    //            const item = result.data[i];
    //            var drawControl = drawControl_device_model;
    //            if (drawControl) {
    //                // 不同的设备，放在不同的层
    //                item.options.center = item.center;
    //                var style = {};
    //                style.url = "http://192.168.0.228:8080/model/gltf/WBZQJ.gltf";
    //                //style.url = item.options.url.replace("$serverURL$", "http://192.168.0.228:8080/model");
    //                //style.scale = item.options.scale;
    //                //style.position = item.options.position;
    //                var option = deviceOption(item, style, item.center);
    //                item.options.position.z -= 90;
    //                var entity = drawControl.attrToEntity(option, item.options.position);
    //                entity.data = item;
    //                (function () {
    //                    entity.click = function (e) {
    //                        alert();
    //                    }
    //                })(entity)
    //            }
    //        }
    //        toastr.info("设备加载完成");
    //        console.log("设备加载完成");
    //    }
    //});

    function deviceOption(data, style, center) {
        var option = {
            attr: data,
            type: "model",
            style: {
                //scale: style.scale,
                //modelUrl: style.url,
                //heading: style.position.heading,
                //pitch: style.position.pitch,
                //roll: style.position.roll,
                scale: 2,
                modelUrl: style.url,
                heading: 0,
                pitch: 0,
                roll: 0,

                fill: false,
                color: "#3388ff",
                opacity: 1,
                silhouette: false,
                clampToGround: false
            },
            center: center
        }
        return option;
    }
}

/**
 * 加载图标
 */
function loadBillboard() {
    drawControl_device_billboard.deleteAll();

    //获取所有模型
    var entitys = drawControl_device_model.getEntitys();
    console.log(2233);
    console.log(entitys);
    for (var i = 0; i < entitys.length; i++) {
        var item = entitys[i];

        var option = {
            type: "billboard",
            style: {
                image: "data/img/qiangji.png",      //图标  
                opacity: 1,
                scale: 1,
                rotation: 0,
                horizontalOrigin: "CENTER",
                verticalOrigin: "BOTTOM",
                scaleByDistance: true,
                scaleByDistance_far: 2000,
                scaleByDistance_farValue: 0.1,
                scaleByDistance_near: 1000,
                scaleByDistance_nearValue: 1,
                distanceDisplayCondition: true,
                distanceDisplayCondition_far: 1500,
                distanceDisplayCondition_near: 50,
                clampToGround: false,
                visibleDepth: true
            }
        }

        var entity = drawControl_device_billboard.attrToEntity(option, item.position);
        entity.data = item.data;
        entity.click = function (entity) {
            //定位到相机
            entity = drawControl_device_model.getEntityById(entity.data.id);
            entity.centerAt({
                complete: function () {
                    //打开视频
                    //MapX.widget.activate({
                    //    name: "视频监控",
                    //    uri: "widgets/camera/widget.js",
                    //    autoDisable: false,
                    //    disableOhter: false,
                    //    data: entity.data
                    //});
                    //toastr.info("打开视频");
                }
            });
        }
    }
}


function loadBillboard2() {
    drawControl_device_billboard.deleteAll();

    //获取所有模型
    var entitys = drawControl_device_model.getEntitys();
    console.log(2233);
    console.log(entitys);
    for (var i = 0; i < entitys.length; i++) {
        var item = entitys[i];
        var option={};
        if(item.data.shebeiAddress.indexOf('球机')==-1){
            option = {
                type: "billboard",
                style: {
                    image: "data/img/qiangji.png",      //图标  
                    opacity: 1,
                    scale: 1,
                    rotation: 0,
                    horizontalOrigin: "CENTER",
                    verticalOrigin: "BOTTOM",
                    scaleByDistance: true,
                    scaleByDistance_far: 2000,
                    scaleByDistance_farValue: 0.1,
                    scaleByDistance_near: 1000,
                    scaleByDistance_nearValue: 1,
                    distanceDisplayCondition: true,
                    distanceDisplayCondition_far: 1500,
                    distanceDisplayCondition_near: 50,
                    clampToGround: false,
                    visibleDepth: true
                }
            }
        }else{
            option = {
                type: "billboard",
                style: {
                    image: "data/img/qiuji.png",      //图标  
                    opacity: 1,
                    scale: 1,
                    rotation: 0,
                    horizontalOrigin: "CENTER",
                    verticalOrigin: "BOTTOM",
                    scaleByDistance: true,
                    scaleByDistance_far: 2000,
                    scaleByDistance_farValue: 0.1,
                    scaleByDistance_near: 1000,
                    scaleByDistance_nearValue: 1,
                    distanceDisplayCondition: true,
                    distanceDisplayCondition_far: 1500,
                    distanceDisplayCondition_near: 50,
                    clampToGround: false,
                    visibleDepth: true
                }
            }
        }
        

        var entity = drawControl_device_billboard.attrToEntity(option, item.position);

        entity.data = item.data;
        entity.click = function (entity) {
            console.log(1111111);
            console.log(entity);
            //定位到相机
            entity = drawControl_device_model.getEntityById(entity.data.id);
            entity.centerAt({
                complete: function () {
                    console.log(entity.data.indexcode);
                    getVUrl(entity.data.indexcode, pv_player);
                    $("#video_name").text("当前视频来源--" + entity.data.shebeiAddress);
                    $('#item2').popup({
                        time: 1000,
                        classAnimateShow: 'slideInUp',
                        classAnimateHide: 'fadeOut',
                        onPopupClose: function e() {
                            console.log('0');
                            pv_player.src([{
                                src: ""
                            }]);;
                            
                        },
                        onPopupInit: function e() {
                            console.log('1')
                            
                        }
                    }
                    )
                    
                }
            });
        }
    }
}

//***************************************单体化************************************* */

function addGeoJsonLayer() {
    var geoJson = [
        { file: "build.json", type: "build" }
    ];
    for (var i = 0; i < geoJson.length; i++) {
        buildFun.addPolygon({
            name: '河桥',
            url: "data/geojson/" + geoJson[i].file,
            color: "#ffff00",
            type: geoJson[i].type
        });
    }
}

var buildFun = {
    addPolygon: function (opts) {
        var that = this;

        //添加叠加的单体化数据 
        $.ajax({
            type: "get",
            dataType: "json",
            url: opts.url,
            success: function (featureCollection) {
                var dataSource = Cesium.GeoJsonDataSource.load(featureCollection);
                dataSource.then(function (dataSource) {
                    var entities = dataSource.entities.values;

                    that.showResultFenCeng(entities, opts); //分层分户 
                }).otherwise(function (error) {
                    if (!error) error = '未知错误';
                    haoutil.alert(error, "加载数据出错");
                });
            }
        });
    },

    //分层单体化
    showResultFenCeng: function (entities, opts) {
        var primitives = new Cesium.PrimitiveCollection();
        MapX.core.getViewer().scene.primitives.add(primitives);

        for (var i = 0; i < entities.length; i++) {
            var entity = entities[i];
            var positions = mapv3d.draw.attr.polygon.getPositions(entity);
            var attr = mapv3d.util.getAttrVal(entity.properties);
            attr.index = i;
            this.addFloorBox(primitives, positions, attr, opts);
        }
        return primitives;
    },
    highlighColor: new Cesium.Color.fromCssColorString("#00ff00").withAlpha(0.5),//高亮时颜色
    nullColor: new Cesium.Color(0.0, 0.0, 0.0, 0.01),
    addFloorBox: function (primitives, positions, attr, opts) {
        var that = this;
        var floor = attr;  //楼层层高配置信息 

        var floorCfg = floor;

        var minHight = floorCfg.DMheight;
        var maxHight = floorCfg.height;
        var id = attr.index;

        var classification = primitives.add(new Cesium.ClassificationPrimitive({
            geometryInstances: new Cesium.GeometryInstance({
                geometry: Cesium.PolygonGeometry.fromPositions({
                    positions: positions,
                    height: minHight,
                    extrudedHeight: maxHight,
                    vertexFormat: Cesium.PerInstanceColorAppearance.VERTEX_FORMAT
                }),
                attributes: {
                    color: Cesium.ColorGeometryInstanceAttribute.fromColor(that.nullColor),
                    show: new Cesium.ShowGeometryInstanceAttribute(true)
                },
                id: id
            }),
            classificationType: Cesium.ClassificationType.CESIUM_3D_TILE,
            show: true,
        }));

        //绑定数据，方便事件中获取 
        var data = floorCfg;
        data["id"] = id;

        classification.data = data;
        switch (opts.type) {
            case "build":
                var popupData = {}
                popupData[opts.name] = "id:" + attr.index
                //classification.popup = mapv3d.util.getPopup("all", popupData);
                break;
        }

        classification.mouseover = function (primitive) {//移入 
            var attributes = primitive.getGeometryInstanceAttributes(primitive.data.id);
            attributes.color = Cesium.ColorGeometryInstanceAttribute.toValue(that.highlighColor);
        }
        classification.mouseout = function (primitive) {//移出
            var attributes = primitive.getGeometryInstanceAttributes(primitive.data.id);
            attributes.color = Cesium.ColorGeometryInstanceAttribute.toValue(that.nullColor);
        }
        classification.click = function (primitive) {//移出
            var attributes = primitive.getGeometryInstanceAttributes(primitive.data.id);
            //attributes.color = Cesium.ColorGeometryInstanceAttribute.toValue(that.nullColor);
            document.getElementById("loudxxinfo").innerHTML = "";
            var undzkid = primitive.data.id;
            $.ajax({
                url: baseUrl + "api/v1/AppTongji/AppTongji/gettydzkuInfo",
                type: "get",
                data: { undzkid: undzkid },
                dataType: "json",
                success: function (data) {
                    console.log("地址库信息")
                    console.log(data)
                    if (data.data.length > 0) {
                        for (var i = 0; i < data.data.length; i++) {
                            if (data.data[i].issfysj == "1") {
                                document.getElementById("loudxxinfo").innerHTML += "<div style='color:red;' onclick='gettydzkdjsj(" + data.data[i].unifiedAddressLibraryId + ")'>" + data.data[i].sourceaddress + "<div></br>";
                            }
                            else {
                                document.getElementById("loudxxinfo").innerHTML += "<div onclick='gettydzkdjsj(" + data.data[i].unifiedAddressLibraryId + ")'>" + data.data[i].sourceaddress + "<div></br>";
                            }
                        }
                    }
                },
                error: function (data) {
                    //alert("未获取到数据");
                }
            });
            $('#itemldxx').popup({
                time: 1000,
                classAnimateShow: 'slideInUp',
                classAnimateHide: 'fadeOut',
                onPopupClose: function e() {
                    // console.log('0')
                },
                onPopupInit: function e() {
                    // console.log('1')
                }
            });
            document.getElementById("quyuxx").innerHTML = "（区域Id：" + undzkid + ")";
        }
    }
}
