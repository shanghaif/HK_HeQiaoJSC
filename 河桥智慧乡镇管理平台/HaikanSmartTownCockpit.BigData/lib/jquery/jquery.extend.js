/*!
 * jQuery插件扩展
 *
 */
(function (window, jQuery, undefined) {
    "use strict";

    var $ = jQuery;

    $.extend({

        /**
         * 无参数方法异常处理
         * @param {any} callBank 执行函数
         * @param {any} exCallBack 异常处理
         */
        exception: function (callBank, exCallBack) {
            if (!callBank) {
                return;
            }

            try {
                return execute(callBank);
            } catch (e) {
                return execute(exCallBack, e);
            }

            function execute(fn, e) {
                var result = undefined;
                if (fn && $.isFunction(fn)) {
                    if (e) {
                        result = fn(e);
                    } else {
                        result = fn();
                    }
                    if ($.isNotEmpty(result)) {
                        return result;
                    }
                }
            }
        },

        /**
         * 判断是否为空，为空返回 true 不为空返回false
         * @param {any} value 值
         */
        isEmpty: function (value) {
            return ((value === undefined || value === null || value === "" || value === "undefined") ? true : false);
        },

        /**
         * 判断是否为空 为空返回 false 不为空返回true
         * @param {any} value 值
         */
        isNotEmpty: function (value) {
            return ((value === undefined || value === null || value === "" || value === "undefined") ? false : true);
        },

        /**
         * 设置默认值
         * @param {any} value 值
         * @param {any} _default 默认值
         */
        setDefault: function (value, _default) {
            if ($.isNotEmpty(value)) {
                return value;
            } else {
                if (typeof _default === "function") {
                    return _default();
                }
                return _default;
            }
        },

        /**
         * 判断是否为字符串，为字符串返回 true 否返回false
         * @param {any} obj 值
         */
        isString: function (obj) {
            if (typeof obj === "string") {
                return true;
            } else {
                return false;
            }
        },

        /**
         * 获取链接参数
         * @param {any} parameter 参数名称
         */
        getQueryString: function (parameter) {
            var reg = new RegExp("(^|&)" + parameter + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) {
                return unescape(r[2]);
            }
            return null;
        },

        /**
         * 判断值是否为空 为空返回默认值
         * @param {any} value 参数值
         * @param {any} type 参数类型
         */
        isEmptyValue: function (value, type) {
            if (this.isEmpty(value)) {
                switch (type) {
                    case "string":
                        value = "";
                        break;
                    case "number":
                        value = 0;
                        break;
                    case "boolean":
                        value = false;
                        break;
                    default:
                        value = "";
                        break;
                }
            }
            return value;
        },

        path: {
            //协议
            protocol: window.location.protocol,
            //地址
            hostname: window.location.hostname,
            //端口
            port: window.location.port,

            httpPath: function (virtual) {
                if (!virtual) {
                    virtual = "";
                }
                if (this.port === "") {
                    return this.protocol + "//" + this.combine(this.hostname, virtual);
                } else {
                    return this.protocol + "//" + this.combine(this.hostname + ":" + this.port, virtual);
                }
            },

            combine: function (path1, path2) {
                if ($.isArray(path1)) {
                    var path = "";
                    for (var i = 0; i < path1.length; i++) {
                        if ((i + 1) === path1.length) {
                            break;
                        }
                        if ($.isEmpty(path)) {
                            path = path1[i];
                        }
                        path = this.combine(path, path1[i + 1]);
                    }
                    return path
                } else if ($.isString(path1)) {
                    if ($.isEmpty(path2)) {
                        return path1;
                    }
                    return path1 + "/" + path2;
                }
            }
        },

        loadData: {
            /**
             * ajax
             * @param {any} url 访问地址
             * @param {any} type 访问类型 POST GET
             * @param {any} data 传值
             * @param {any} callBack 返回成功回调函数
             * @param {any} errorCallBack 返回失败回调函数
             */
            ajax: function (url, type, data, callBack, errorCallBack) {
                $.ajax({
                    url: url,
                    type: type,
                    data: data,
                    dataType: "json",
                    success: function (result) {
                        if (callBack) {
                            callBack(result);
                        }
                    },
                    error: function (ex) {
                        if (errorCallBack) {
                            errorCallBack(ex);
                        }
                    }
                });
            }
        },

        api: {
            /**
             * ajax
             * @param {any} url 访问地址
             * @param {any} type 访问类型 POST GET
             * @param {any} data 传值
             * @param {any} callBack 返回成功回调函数
             * @param {any} errorCallBack 返回失败回调函数
             */
            ajax: function (url, type, data, callBack, errorCallBack) {
                $.ajax({
                    url: url,
                    type: type,
                    data: JSON.stringify(data),
                    contentType: "application/json",
                    success: function (result) {
                        if (callBack) {
                            callBack(result);
                        }
                    },
                    error: function (ex) {
                        if (errorCallBack) {
                            errorCallBack(ex);
                        }
                    }
                });
            }
        },

        string: {
            empty: "",

            /**
             * 去除最后一个字符
             * @param {any} str 传入字符
             */
            removeLast: function (str) {

                return str.substring(0, str.length - 1);
            },

            /**
             * 字符串占位符：调用实例 $.string.format("12{0}4{1}", ["3", "5"]);
             * @param {any} str 初始值
             * @param {any} format 格式化参数
             */
            format: function (str, format) {

                if (!$.isFunction(String.prototype.format)) {
                    String.prototype.format = function (format) {
                        if (arguments.length == 0) return this;
                        var param = format;
                        var s = this;
                        if (typeof (param) == 'object') {
                            for (var key in param)
                                s = s.replace(new RegExp("\\{" + key + "\\}", "g"), param[key]);
                            return s;
                        } else {
                            for (var i = 0; i < arguments.length; i++)
                                s = s.replace(new RegExp("\\{" + i + "\\}", "g"), arguments[i]);
                            return s;
                        }
                    }
                }

                if (format) {
                    if (str) {
                        return str.format(format);
                    }
                } else {
                    return undefined;
                }
            }
        },

        date: {
            /**
             * 获取当前时间
             * @param {any} format 日期格式
             */
            now: function (format) {
                return this.format(new Date, format);
            },

            /**
             * 格式化时间
             * @param {any} date 时间
             * @param {any} format 日期格式
             */
            format: function (date, format) {
                if (!$.isFunction(Date.prototype.format)) {
                    Date.prototype.format = function (format) {
                        var date = {
                            "M+": this.getMonth() + 1,
                            "d+": this.getDate(),
                            "h+": this.getHours(),
                            "m+": this.getMinutes(),
                            "s+": this.getSeconds(),
                            "q+": Math.floor((this.getMonth() + 3) / 3),
                            "S+": this.getMilliseconds()
                        };
                        if (/(y+)/i.test(format)) {
                            format = format.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length));
                        }
                        for (var k in date) {
                            if (new RegExp("(" + k + ")").test(format)) {
                                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? date[k] : ("00" + date[k]).substr(("" + date[k]).length));
                            }
                        }
                        return format;
                    }
                }
                if (format) {
                    if (date) {
                        return date.format(format);
                    } else {
                        return this.now(format);
                    }
                } else {
                    return undefined;
                }
            }
        },

        /**
         * 获取uuid
         * */
        uuid: function () {
            var s = [];
            var hexDigits = "0123456789abcdef";
            for (var i = 0; i < 32; i++) {
                s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
            }
            s[14] = "4";  // bits 12-15 of the time_hi_and_version field to 0010
            s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);  // bits 6-7 of the clock_seq_hi_and_reserved to 01
            //s[8] = s[13] = s[18] = s[23] = "-";

            var uuid = s.join("");
            return uuid;
        },

        /**
         * 封装getStyle函数
         * @param {*} obj 
         * @param {*} attr 
         */
        getStyle(obj, attr) {
            return obj.currentStyle ? obj.currentStyle[attr] : getComputedStyle(obj, false)[attr];
        }
    });

    $.each(["get", "post"], function (i, method) {
        $.loadData[method] = function (url, data, callBack, errorCallBack) {
            if ($.isFunction(data)) {
                if ($.isNotEmpty(callBack)) {
                    errorCallBack = callBack;
                }
                callBack = data;
                data = undefined;
            }
            $.loadData.ajax(url, method, data, callBack, errorCallBack);
        };
    });

    $.each(["get", "post", "put", "delete"], function (i, method) {
        $.api[method] = function (url, data, callBack, errorCallBack) {
            if ($.isFunction(data)) {
                if ($.isNotEmpty(callBack)) {
                    errorCallBack = callBack;
                }
                callBack = data;
                data = undefined;
            }
            $.api.ajax(url, method, data, callBack, errorCallBack);
        };
    });

    $.fn.extend({
        imgHover: function (overImg, outImg, path) {
            /// <summary>图片鼠标移入移出方法</summary>
            /// <param name="overImg" type="String">鼠标移入图片</param>
            /// <param name="outImg" type="String">鼠标移出图片</param>
            /// <param name="path" type="String">文件路径</param>

            $(this).hover(function () {
                bind(this, overImg, path);
            }, function () {
                bind(this, outImg, path);
            });

            //设置鼠标手势
            $(this).css("cursor", "pointer");

            //设置图片
            function bind(dom, img, path) {
                $(dom).attr("src", img);
            }
        }
    });

})(window, jQuery);