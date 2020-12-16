if(!self.__appxInited) {
self.__appxInited = 1;


require('./config$');


      if( navigator.userAgent && (navigator.userAgent.indexOf('LyraVM') > 0 || navigator.userAgent.indexOf('AlipayIDE') > 0) ) {
        var AFAppX = self.AFAppX.getAppContext ? self.AFAppX.getAppContext().AFAppX : self.AFAppX;
      } else {
        importScripts('https://appx/af-appx.worker.min.js');
        var AFAppX = self.AFAppX;
      }
      self.getCurrentPages = AFAppX.getCurrentPages;
      self.getApp = AFAppX.getApp;
      self.Page = AFAppX.Page;
      self.App = AFAppX.App;
      self.my = AFAppX.bridge || AFAppX.abridge;
      self.abridge = self.my;
      self.Component = AFAppX.WorkerComponent || function(){};
      self.$global = AFAppX.$global;
      self.requirePlugin = AFAppX.requirePlugin;
    

if(AFAppX.registerApp) {
  AFAppX.registerApp({
    appJSON: appXAppJson,
  });
}



function success() {
require('../../app');
require('../../uview-ui/components/u-mask/u-mask?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../uview-ui/components/u-icon/u-icon?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../uview-ui/components/u-popup/u-popup?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../uview-ui/components/u-button/u-button?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTask?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/PriorityShow?hash=0ac898e91e50ea3432832138fbd892a389791f2b');
require('../../components/t-table/t-table?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../components/t-table/t-th?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../components/t-table/t-tr?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../components/t-table/t-td?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/TongJi/TongJi?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../uview-ui/components/u-badge/u-badge?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../uview-ui/components/u-tabs-swiper/u-tabs-swiper?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Personallog/personalloglist?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../components/uni-drawer/uni-drawer?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/index/index?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTaskShow?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/AccomplishTask?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MissionJournal?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/MessageAdd/TaskAdd2?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/MessageAdd/PersonalDiaryAdd?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/MessageAdd/PriorityAdd?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/AccomplishPriority?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/Taskywc?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/index/new_file?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/index/login?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/index/personnel?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTaskzonjie?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTaskUser?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTaskzonjieList?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/huibao?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTaskzonjieShow?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Task/MyTaskList?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/PriorityAdd?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/PriorityDepartList?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/PriorityGZHJList?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/PriorityGZHJShows?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/PriorityUserList?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Personallog/userlist?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Personallog/datalist?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Personallog/datadatas?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/Priority/MessageDetail?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/MessageAdd/TaskAdd?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/MessageAdd/AddTask?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
require('../../pages/MessageAdd/MessageList?hash=7da53690ff015fafc40ebbe2de99bf33c6f02acd');
}
self.bootstrapApp ? self.bootstrapApp({ success }) : success();
}