"use script";

/**
 * 相机
 */
(function () {

    /**
     * 定位中心点
     * @param {Object} center 中心点坐标
     * @param {Object} options 定位选项
     */
    function centerAt(center, options) {
        MapX.core.getViewer().mapv.centerAt(center, options);
    }

    MapX.camera = {
        centerAt: centerAt
    };
})();