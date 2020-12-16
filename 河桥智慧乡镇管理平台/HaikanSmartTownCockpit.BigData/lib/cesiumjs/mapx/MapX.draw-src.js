"use script";

/**
 * 标绘类
 * @description 提供文字、点、线、面、立体、模型等各类entity对象的绘制
 */
(function (window) {

    /**
     * 扩展Draw
     * @description 扩展坐转换方法
     */
    mapv3d.Draw.prototype.attrToEntity = function (attr, position) {
        position = MapX.util.wgs84ToCartesian3(position);
        var entity = this.attributeToEntity(attr, position);

        //entity扩展方法
        entity.centerAt = function (option) {
            MapX.camera.centerAt(entity.data.center, option);
        }
        entity.show = function (visible) {
            entity.show = visible;
        }
        return entity;
    }

    /**
     * 获取entity坐标
     */
    mapv3d.Draw.prototype.getPosition = function (entity) {
        var positions = MapX.core.getViewer().mapv.draw.getPositions(entity);
        return MapX.util.cartesianToWGS84(positions);
    }

    /**
    * @class
    * @public
    */
    window.MapX.Draw = function (viewer, opts) {
        if (!viewer && !opts) {
            viewer = MapX.core.getViewer();
            opts = {}
        } else if (viewer && viewer.hasOwnProperty("hasEdit")) {
            opts = viewer;
            viewer = MapX.core.getViewer();
        } else {
            opts = {}
        }
        return new mapv3d.Draw(viewer, opts);
    };

    window.MapX.draw = mapv3d.draw;
})(window);