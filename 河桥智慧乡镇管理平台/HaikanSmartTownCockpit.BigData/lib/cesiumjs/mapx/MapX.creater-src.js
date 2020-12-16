"use script";

/**
 * 对象类
 */
(function () {

    /**
     * divPoint对象
     * @param {Viewer} viewer viewer
     * @param {Object} option 传入参数
     */
    function divPoint(viewer, option) {
        if (!option) {
            option = viewer;
            viewer = MapX.core.getViewer();
        }
        var divPoint = new mapv3d.DivPoint(viewer, option);

        return divPoint;
    }

    MapX.creater = {
        divPoint: divPoint
    }
})();