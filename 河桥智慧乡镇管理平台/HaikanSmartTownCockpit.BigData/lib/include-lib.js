//第三方类库加载管理js，方便切换lib
(function () {
   var r = new RegExp("(^|(.*?\\/))(include-lib\.js)(\\?|$)"),
      s = document.getElementsByTagName("script"), targetScript;
   for (var i = 0; i < s.length; i++) {
      var src = s[i].getAttribute("src");
      if (src) {
         var m = src.match(r);
         if (m) {
            targetScript = s[i];
            break;
         }
      }
   }

   //当前版本号,用于清理浏览器缓存

   // cssExpr 用于判断资源是否是css
   var cssExpr = new RegExp("\\.css");

   function inputLibs(list) {
      if (list == null || list.length == 0) return;

      for (var i = 0, len = list.length; i < len; i++) {
         var url = list[i];
         if (cssExpr.test(url)) {
            var css = '<link rel="stylesheet" href="' + url + '">';
            document.writeln(css);
         } else {
            var script = '<script type="text/javascript" src="' + url + '"><' + '/script>';
            document.writeln(script);
         }
      }
   }

   //加载类库资源文件
   function load() {
      var arrInclude = (targetScript.getAttribute('include') || "").split(",");
      var libpath = (targetScript.getAttribute('libpath') || "");

      if (libpath.lastIndexOf('/') != libpath.length - 1)
         libpath += "/";

      //js库配置
      var libsConfig = {
         'jquery': [
            libpath + "lib/jquery/jquery-2.1.4.min.js",
            libpath + "lib/jquery/jquery.extend.js"
         ],
         'jquery.scrollTo': [
            libpath + "lib/jquery/scrollTo/jquery.scrollTo.min.js",
         ],
         'jquery.range': [
            libpath + "lib/jquery/range/range.css",
            libpath + "lib/jquery/range/range.js",
         ],
         'jquery.ztree': [
            libpath + "lib/jquery/ztree/css/zTreeStyle/zTreeStyle.css",
            libpath + "lib/jquery/ztree/css/mars/ztree-mars.css",
            libpath + "lib/jquery/ztree/js/jquery.ztree.all.min.js",
         ],
         'jquery.mCustomScrollbar': [
            libpath + "lib/jquery/mCustomScrollbar/jquery.mCustomScrollbar.css",
            libpath + "lib/jquery/mCustomScrollbar/jquery.mCustomScrollbar.js",
         ],
         'jquery.lazyload': [
            libpath + "lib/jquery/lazyload/jquery.lazyload.min.js",
         ],
         'jquery.btnbar': [
            libpath + "lib/jquery/btnbar/btnbar.css",
            libpath + "lib/jquery/btnbar/btnbar.js",
         ],
         'font-awesome': [
            libpath + "lib/fonts/font-awesome/css/font-awesome.min.css",
         ],
         'font-mapvgis': [
            libpath + "lib/fonts/mapvgis/iconfont.css",
         ],
         'web-icons': [
            libpath + "lib/fonts/web-icons/web-icons.css",
         ],
         'animate': [
            libpath + "lib/animate/animate.css",
         ],
         'admui': [
            libpath + "lib/admui/css/index.css",
            libpath + "lib/admui/js/global/core.js", //核心
            libpath + "lib/admui/js/global/configs/site-configs.js",
            libpath + "lib/admui/js/global/components.js",
         ],
         'admui-frame': [
            libpath + "lib/admui/css/site.css",
            libpath + "lib/admui/js/app.js",
         ],
         'bootstrap': [
            libpath + "lib/bootstrap/bootstrap.css",
            libpath + "lib/bootstrap/bootstrap.min.js",
         ],
         'bootstrap-table': [
            libpath + "lib/bootstrap/bootstrap-table/bootstrap-table.css",
            libpath + "lib/bootstrap/bootstrap-table/bootstrap-table.min.js",
            libpath + "lib/bootstrap/bootstrap-table/locale/bootstrap-table-zh-CN.js"
         ],
         'bootstrap-select': [
            libpath + "lib/bootstrap/bootstrap-select/bootstrap-select.css",
            libpath + "lib/bootstrap/bootstrap-select/bootstrap-select.min.js",
         ],
         'bootstrap-checkbox': [
            libpath + "lib/bootstrap/bootstrap-checkbox/awesome-bootstrap-checkbox.css",
         ],
         'toastr': [
            libpath + "lib/toastr/toastr.css",
            libpath + "lib/toastr/toastr.js",
         ],
         'layer': [
            libpath + "lib/layer/theme/default/layer.css",
            libpath + "lib/layer/theme/retina/retina.css",
            libpath + "lib/layer/theme/mapv/layer.css",
            libpath + "lib/layer/layer.js",
         ],
         'haoutil': [
            libpath + "lib/hao/haoutil.js"
         ],
         'echarts': [
            libpath + "lib/echarts/echarts.min.js",
            libpath + "lib/echarts/dark.js"
         ],
         'echarts-gl': [
            libpath + "lib/echarts/echarts.min.js",
            libpath + "lib/echarts/echarts-gl.min.js"
         ],
         'turf': [
            libpath + "lib/turf/turf.min.js"
         ],
         "crypto": [
            //crypto
            libpath + "lib/crypto-js/crypto-js.js"
         ],
         'mapv3d': [//三维地球“主库”
            libpath + "lib/cesiumjs/Cesium/Widgets/widgets.css", //cesium
            libpath + "lib/cesiumjs/Cesium/Cesium.js",
            libpath + "lib/cesiumjs/plugins/compatible/version.js", //cesium版本兼容处理

            libpath + "lib/cesiumjs/mapv3d/mapv3d.css", //mapv3d
            libpath + "lib/cesiumjs/mapv3d/mapv3d-src.js",

            libpath + "lib/cesiumjs/plugins/navigation/mapv3d-navigation.css", //导航
            libpath + "lib/cesiumjs/plugins/navigation/mapv3d-navigation.js",

            libpath + "lib/cesiumjs/plugins/esri/mapv3d-esri.js", //arcgis矢量图层扩展，无此需求时注释即可 


            //MapX主库
            libpath + "lib/cesiumjs/mapx/MapX.api-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.core-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.tool-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.util-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.camera-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.draw-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.widget-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.creater-src.js",
            libpath + "lib/cesiumjs/mapx/MapX.measure-src.js"
         ]
      };

      //添加MapX属性
      window.MapX = {};

      for (var i in arrInclude) {
         var key = arrInclude[i];
         inputLibs(libsConfig[key]);
      }
   }

   load();
})();
