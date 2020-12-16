/**
*  JS常用静态方法类库  
*  版本信息：v2.5.1
*  编译日期：2020-3-26 19:31:59    
*  版权所有：Copyright by 图洋科技 LYW
*/
var haoutil = haoutil || {};

haoutil.version = "2.4";
haoutil.name = "LYW 通用常用JS方法类库";
haoutil.author = "LYW";
haoutil.update = "2020-1-2";
haoutil.website = ''


/* 2017-11-6 10:15:31 | 修改 LYW  */
//js原生对象扩展


//标识是否扩展数组对象
if (!window.noArrayPrototype) {
    //扩展array数组方法,不要用for(var i in arr)来循环数组
    Array.prototype.indexOf = Array.prototype.indexOf || function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) return i;
        }
        return -1;
    };
    Array.prototype.remove = Array.prototype.remove || function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) {
                this.splice(i, 1);
                break;
            }
        }
    };
    Array.prototype.insert = Array.prototype.insert || function (item, index) {
        if (index == null) index = 0;
        this.splice(index, 0, item);
    }; 
}

 

String.prototype.startsWith = String.prototype.startsWith || function (str) {
    return this.slice(0, str.length) == str;
}; 
//判断当前字符串是否以str结束 
String.prototype.endsWith = String.prototype.endsWith || function (str) {
    return this.slice(-str.length) == str;
};
String.prototype.replaceAll = String.prototype.replaceAll || function (oldstring, newstring) {
    return this.replace(new RegExp(oldstring, "gm"), newstring);
}

/**  
 * 对Date的扩展，将 Date 转化为指定格式的String 
 * 月(M)、日(d)、12小时(h)、24小时(H)、分(m)、秒(s)、周(E)、季度(q) 可以用 1-2 个占位符 
 * 年(y)可以用 1-4 个占位符，毫秒(S)只能用 1 个占位符(是 1-3 位的数字) 
 * 
 * (new Date()).format("yyyy-M-d HH:mm:ss") ==> 2017-1-9 08:35:26
 * (new Date()).format("yyyy-M-d h:m:s.S") ==> 2016-7-2 8:9:4.18 
 * (new Date()).format("yyyy-MM-dd hh:mm:ss.S") ==> 2016-07-02 08:09:04.423 
 * (new Date()).format("yyyy-MM-dd E HH:mm:ss") ==> 2016-03-10 二 20:09:04 
 * (new Date()).format("yyyy-MM-dd EE hh:mm:ss") ==> 2016-03-10 周二 08:09:04 
 * (new Date()).format("yyyy-MM-dd EEE hh:mm:ss") ==> 2016-03-10 星期二 08:09:04 
 */
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份       
        "d+": this.getDate(), //日       
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时       
        "H+": this.getHours(), //小时       
        "m+": this.getMinutes(), //分       
        "s+": this.getSeconds(), //秒       
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度       
        "S": this.getMilliseconds() //毫秒       
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};


/**
 * AES加密解密
 */
haoutil.aes = (function () {
    // "AES加密解密 相关操作类";
    //============内部私有属性及方法============

    /**
     * AES加密
     * @param {string} word 加密字符串
     * @param {string} key 密钥
     * @param {string} iv 初始向量 initial vector 16 位
     */
    function encrypt(word, key, iv) {
        let srcs = CryptoJS.enc.Utf8.parse(word);
        let encrypted = CryptoJS.AES.encrypt(srcs, key, { iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
        return encrypted.ciphertext.toString().toUpperCase();
    }

    /**
     * AES解密
     * @param {string} word 解密字符串
     * @param {string} key 密钥$
     * @param {string} iv 初始向量 initial vector 16 位
     */
    function decrypt(word, key, iv) {
        let encryptedHexStr = CryptoJS.enc.Hex.parse(word);
        let srcs = CryptoJS.enc.Base64.stringify(encryptedHexStr);
        let decrypt = CryptoJS.AES.decrypt(srcs, key, { iv: iv, mode: CryptoJS.mode.CBC, padding: CryptoJS.pad.Pkcs7 });
        let decryptedStr = decrypt.toString(CryptoJS.enc.Utf8);
        return decryptedStr.toString();
    }

    //===========对外公开的属性及方法=========
    return {
        encrypt: encrypt,
        decrypt: decrypt
    }
})();

haoutil.ajax = (function () {
    // "ajax处理 相关操作类";
    //============内部私有属性及方法============
    function sync(url, data) {
        var type = "get";
        if ($.isNotEmpty(data)) {
            type = "post";
        }
        var resultData;
        $.ajax({
            url: url,
            type: type,
            data: data,
            async: false,
            success: function (result) {
                resultData = result;
            }
        });
        return resultData;
    }

    //===========对外公开的属性及方法=========
    return {
        sync: sync
    }
})();

/* 2017-12-5 13:38:32 | 修改 LYW  */
haoutil.isutil = (function () {
    // "判断 相关操作类";

    //============内部私有属性及方法============
    function isArray(obj) {
        if (typeof Array.isArray === "function") {
            return Array.isArray(obj);
        } else {
            return Object.prototype.toString.call(obj) === "[object Array]";
        }
    } 
    
    function isString(str) {
        return (typeof str == 'string') && str.constructor == String;
    }

    function isNumber(obj) {
        return (typeof obj == 'number') && obj.constructor == Number;
    }

    function isDate(obj) {
        return (typeof obj == 'object') && obj.constructor == Date;
    }

    function isFunction(obj) {
        return (typeof obj == 'function') && obj.constructor == Function;
    }

    function isObject(obj) {
        return (typeof obj == 'object') && obj.constructor == Object;
    }



    function isNull(value) { 
        if (value == null) return true;
        if (isString(value) && value == "") return true;
        if (isNumber(value) && isNaN(value)) return true;

        return false;
    }

    function isNotNull(value) { 
        return !isNull(value);
    }

    //===========对外公开的属性及方法=========
    return {
        isNull: isNull,
        isNotNull: isNotNull,
        isArray: isArray,
        isString: isString,
        isNumber: isNumber,
        isDate: isDate,
        isFunction: isFunction,
        isObject: isObject
    };
})();

haoutil.system = (function () {
    // "系统级 相关操作类";
    //============内部私有属性及方法============

    /**
     * 克隆对象
     * @param {Object} obj 要复制的对象
     * @param {Array} removeKeys 移除属性
     * @returns 新对象
     */
    function clone(obj, removeKeys) {
        if (null == obj || "object" != typeof obj) return obj;

        if (removeKeys == null) removeKeys = ["_parent", "_class"];//排除一些不拷贝的属性

        // Handle Date
        if (haoutil.isutil.isDate(obj)) {
            var copy = new Date();
            copy.setTime(obj.getTime());
            return copy;
        }

        // Handle Array
        if (haoutil.isutil.isArray(obj)) {
            var copy = [];
            for (var i = 0, len = obj.length; i < len; ++i) {
                copy[i] = clone(obj[i], removeKeys);
            }
            return copy;
        }

        // Handle Object
        if (typeof obj === 'object') {
            var copy = {};
            for (var attr in obj) {
                if (typeof attr === 'function') continue;
                if (removeKeys.indexOf(attr) != -1) continue;

                if (obj.hasOwnProperty(attr))
                    copy[attr] = clone(obj[attr], removeKeys);
            }
            return copy;
        }
        return obj;
    }

    //===========对外公开的属性及方法=========
    return {
        clone: clone
    }
})();