"use script";

/**
 * 测量相关类
 */
(function () {

    MapX.measure = function (viewer) {
        if (!viewer) {
            viewer = MapX.core.getViewer();
        }
        return new mapv3d.analysi.Measure({
            viewer: viewer
        });
    };
})();