"use script";

/**
 * WebAPI接口
 * @description WebAPI接口信息
 */
(function (window) {

    var baseUrl = "";

    function api() {

        this.secret = "";

        this.sys_authorize_machine = baseUrl + "/sys/authorize/machine";
        this.sys_config = baseUrl + "/sys/config";

        this.map_model_list = baseUrl + "/map/model/list";
        this.map_build_list = baseUrl + "/map/build/list";
        this.map_floor_list = baseUrl + "/map/floor/list";
        this.map_under_list = baseUrl + "/map/under/list";

        this.device_info_list = baseUrl + "/device/info/list";

        /**
         * 设置WebAPI地址
         * @param {String} url WebAPI肚子
         */
        api.prototype.setUrl = function (url, secret) {
            baseUrl = url;
            var last = url.substr(url.length - 1, 1);
            if (last === "/") {
                baseUrl = url.substr(0, url.length - 1);
            }
            this.secret = secret;
            window.MapX.api = new api();
        }

        /**
         * 获取WebAPI地址
         */
        api.prototype.getUrl = function () {
            return baseUrl;
        }

        //=============接口调用==============
        api.prototype.get = function (url, callback) {
            execute(url, "get", callback);
        }
        api.prototype.post = function (url, data, callback) {
            if (haoutil.isutil.isFunction(data)) {
                callback = data;
                data = {};
            }
            execute(url, "post", data, callback);
        }

        function execute(url, type, data, callback) {
            //判断WebAPI是否配置
            if (MapX.api.getUrl() === "") {
                console.log("%cError: WebAPI地址未指定!", "color: rgb(255, 0, 0)");
                return;
            }

            if (haoutil.isutil.isFunction(data)) {
                callback = data;
                data = {};
            }

            $.ajax({
                url: url,
                data: data,
                type: type,
                success: function (result) {
                    callback(result);
                }
            });
        }
    }

    window.MapX.api = new api();

})(window);