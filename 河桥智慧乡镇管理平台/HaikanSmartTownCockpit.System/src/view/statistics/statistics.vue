<template>

  <div>
    <Row :gutter="10">
      <i-col
        :xs="12"
        :md="8"
        :lg="5"
        v-for="(infor, i) in inforCardData"
        :key="`infor-${i}`"
        style="height: 120px;padding-bottom: 20px;margin-right: 20px;"
      >
        <infor-card shadow :color="infor.color" :icon="infor.icon" :icon-size="36">
          <div class="count-to-wrapper">
            <p class="content-outer">
              <Button
                @click="handleSubmitRole(infor.title)"
                style="border: 0px solid white; background-color: white;"
              >
                <span id="count_to_398" class="count-to-count-text count-style">{{infor.count}}</span>
              </Button>
              <i class="count-to-unit-text"></i>
            </p>
          </div>
          <p>{{ infor.title }}</p>
        </infor-card>
      </i-col>
    </Row>

    <Row :gutter="10" style="margin-top: 10px;" v-if="show1"> 
      <i-col :md="24" :lg="test" style="margin-bottom: 20px;">
        <Card shadow>
          <chart-pie style="height: 300px;width: 50%;float: left;" :value="pieData" text="任务分布情况"></chart-pie>
          <chart-pie
            style="height: 300px;width: 50%;float: left;"
            :value="youxianData"
            text="优先级分布情况"
          ></chart-pie>
          <div style="clear: both;"></div>
        </Card>
      </i-col>
    </Row>
<!-- 
  <i-button type="primary" @click="tubiaolook" v-if="btnshow" :loading="loadingbtn">图表查看</i-button> -->
    <i-button type="primary" @click="qiehuan">{{yearmonth}}</i-button>
     <Date-picker type="year" v-if="yearx" :value="nowdyear" @on-change="dateChange" format="yyyy" placeholder="选择年份" ></Date-picker>
     <Date-picker type="month" v-if="monx"  :value="nowdmon" @on-change="dateChanges" format="yyyy-MM" placeholder="选择月份" ></Date-picker>
     <i-button type="primary" @click="tubiaolook" :loading="loadingbtn">查询</i-button>

    <Row :gutter="10" style="margin-top: 10px;" v-if="show2">
      <i-col :md="24" :lg="test" style="margin-bottom: 20px;">
        <Card shadow>
          <chart-bar
            style="height: 300px;width: 50%;float: left;"
            :value="barData"
            text="村庄任务负责排行"
          />
          <div style="float: left; width: 50%;">




            <chart-bar
            style="height: 300px;width: 100%;"
            :value="barDatas"
            text="科室任务负责排行统计"
          />


            <!-- <p
              style="height: 50px;text-align: center;font-size: 17.5px;color: #516b91;font-weight: bold;"
            >
              村庄任务负责排行统计
              <i-button
                icon="md-refresh"
                @click="qihuan"
                type="text"
                :loading="loading"
              >{{ordertext}}</i-button>
            </p>
            <i-table border :columns="columns1" :data="data1"></i-table> -->
          </div>
          <div style="clear: both;"></div>
        </Card>
      </i-col>
    </Row>
    <!-- 科室任务负责排行统计 -->
    <!-- <Row :gutter="10" style="margin-top: 10px;" v-if="show3">
      <i-col :md="24" :lg="test" style="margin-bottom: 20px;">
        <Card shadow>
          <chart-bar
            style="height: 300px;width: 50%;float: left;"
            :value="barDatas"
            text="科室任务负责排行统计"
          />
          <div style="float: left; width: 50%;">
            <p
              style="height: 50px;text-align: center;font-size: 17.5px;color: #516b91;font-weight: bold;"
            >
              科室任务负责排行统计
              <i-button
                icon="md-refresh"
                @click="qihuancanyu"
                type="text"
                :loading="loadings"
              >{{ordertext}}</i-button>
            </p>
            <i-table border :columns="columns2" :data="data2"></i-table>
          </div>
          <div style="clear: both;"></div>
        </Card>
      </i-col>
    </Row> -->
    <!--    <Row>
      <Card shadow>
        <example style="height: 310px;"/>
      </Card>
    </Row>-->
  </div>
</template>

<script>
import InforCard from "_c/info-card";
import CountTo from "_c/count-to";
import { ChartPie, ChartBar } from "_c/charts";
import Example from "../single-page/home/example.vue";
import {
  selectcount,
  selectfuzepaihang,
  selectfuzepaihangd,
  selectcanyupaihang,
  selectcanyupaihangd,
  selectjinzhan, //升序
  selectjinzhandesc, //降序
  selectmissionscanyu, //升序
  selectmissionscanyudesc, //降序
} from "@/api/statistics/statistics";
export default {
  name: "Diagram",
  components: {
    InforCard,
    CountTo,
    ChartPie,
    ChartBar,
    Example,
  },
  data() {
    return {
      nowdyear:"",
      nowdmon:"",
      yearx:true,
      monx:false,
      yearmonth:"按年查看",
      btnshow:true,
      loadingbtn:false,
      loading: false,
      loadings: false,
      order: "desc",
      ordertext: "降序",
      columns1: [
        {
          title: "负责人",
          key: "name",
        },
        {
          title: "负责任务总数",
          key: "sun",
          className: "table-font",
        },
        {
          title: "已逾期",
          key: "yuqi",
        },
        {
          title: "进行中",
          key: "jinxing",
        },
        {
          title: "已完成",
          key: "wancheng",
          // className: "table-font",
        },
      ],
      columns2: [
        {
          title: "参与人",
          key: "name",
        },
        {
          title: "参与任务总数",
          key: "sun",
          className: "table-font",
        },
        {
          title: "已逾期",
          key: "yuqi",
        },
        {
          title: "进行中",
          key: "jinxing",
        },
        {
          title: "已完成",
          key: "wancheng",
          // className: "table-font",
        },
      ],
      data1desc: [],
      data1asc: [],
      data1: [],
      data2desc: [],
      data2asc: [],
      data2: [],
      test: 100,
      show1: false,
      show2: false,
      show3: false,
      inforCardData: [
                {
          title: "所有任务",
          icon: "md-checkbox-outline",
          count: 0,
          color: "#19be6b",
        },
        {
          title: "上级交办",
          icon: "md-contacts",
          count: 0,
          color: "#19be6b",
        },
        {
          title: "下级上传",
          icon: "md-contacts",
          count: 0,
          color: "#2d8cf0",
        },
        {
          title: "已完成的任务",
          icon: "md-notifications-outline",
          count: 0,
          color: "#FF8000",
        },
        {
          title: "办理中的任务",
          icon: "md-stopwatch",
          count: 0,
          color: "#ed3f14",
        },
        {
          title: "今日到期的任务",
          icon: "md-paper",
          count: 0,
          color: "#B0E0E6",
        },
        {
          title: "已逾期的任务",
          icon: "md-color-filter",
          count: 0,
          color: "#19be6b",
        },
        {
          title: "逾期完成的任务",
          icon: "md-checkbox-outline",
          count: 0,
          color: "#FF8000",
        },
      ],
      youxianData: [
        {
          value: 0,
          name: "普通",
          itemStyle: {
            color: "#19be6b",
          },
        },
        {
          value: 0,
          name: "紧急",
          itemStyle: {
            color: "#2d8cf0",
          },
        },
        // {
        // 	value: 0,
        // 	name: '非常紧急',
        // 	itemStyle: {
        // 		color: '#ed3f14'
        // 	}
        // },
      ],
      pieData: [
        {
          value: 0,
          name: "已完成",
          itemStyle: {
            color: "#B0E0E6",
          },
        },
        {
          value: 0,
          name: "办理中",
          itemStyle: {
            color: "#ed3f14",
          },
        },

        {
          value: 0,
          name: "逾期",
          itemStyle: {
            color: "#19be6b",
          },
        },

      ],
      barData: {},
      barDatas: {},
    };
  },
  methods: {
    loadVoteinitiateList() {
      this.show1 = false; //隐藏统计图
      selectcount({
        userid: this.$store.state.user.userGuid,
      }).then((res) => {
        this.inforCardData[0].count = res.data.allcount; //所有任务
        this.inforCardData[1].count = res.data.sjjbcount; //上级交办
        this.inforCardData[2].count = res.data.xjsccount; //下级上传
        this.inforCardData[3].count = this.pieData[0].value =
          res.data.ywccount; //已完成
        this.inforCardData[4].count = this.pieData[1].value = res.data.blzcount; //办理中
        this.inforCardData[5].count = res.data.jrdqcount; //今日到期
        this.inforCardData[6].count = this.pieData[2].value =
          res.data.yyqcount; //已逾期
        this.inforCardData[7].count = res.data.yqwccount; //逾期完成
        this.youxianData[0].value = res.data.putong; //普通任务
        this.youxianData[1].value = res.data.jinji; //紧急任务
        this.show1 = true; //显示统计图
      });
 
      //村庄任务排行统计
      selectfuzepaihangd({date:this.nowdyear}).then((res) => {
        this.barData = res.data.data;
          //科室任务排行统计
      selectcanyupaihangd({date:this.nowdyear}).then((res) => {
        this.barDatas = res.data.data;
        this.show2 = true; //显示统计图
      });
      });
      
    },

    dateChange(date){
      console.log(date)
      this.nowdyear=date;
    },
     dateChanges(date){
      console.log(date)
      this.nowdmon=date;
    },

    tubiaolook(){
      this.show2 = false; 
      console.log("时间")
      console.log(this.nowdyear)
      this.loadingbtn=true;
      var dates=this.nowdyear;
    if(this.yearx)
    {
      dates=this.nowdyear;
    }
    if(this.monx){
      dates=this.nowdmon;
    }
    console.log(dates)
     //村庄任务排行统计
      selectfuzepaihangd({date:dates}).then((res) => {
        this.barData = res.data.data;
          //科室任务排行统计
      selectcanyupaihangd({date:dates}).then((res) => {
        console.log("科室任务排行统计")
        console.log(res)
        this.barDatas = res.data.data;
        this.show2 = true; //显示统计图
        this.loadingbtn=false;
      });
      });

      // selectjinzhan().then((res) => {
      //   // console.log(res.data.data)
      //   this.data1asc = res.data.data;   
      // });

      // selectjinzhandesc().then((res) => {
      //   // console.log(res.data.data)
      //   this.data1=this.data1desc = res.data.data;
      // });

      // selectmissionscanyu().then((res) => {
      //   this.data2asc = res.data.data;
      // });

      // selectmissionscanyudesc().then((res) => {
      //   this.data2=this.data2desc = res.data.data;
      //   this.loadingbtn=false;
      //   this.btnshow=false;
      // })

    },





    //升序降序排行
    qihuan() {
      this.loading = true;
      if (this.order == "desc") {
        this.order = "asc";
        this.ordertext = "升序";
        this.data1 = this.data1asc;
        this.loading = false;
      } else {
        this.order = "desc";
        this.ordertext = "降序";
        // console.log(res.data.data)
        this.data1 = this.data1desc;
        this.loading = false;
      }
    },
    //升序降序排行
    qihuancanyu() {
      this.loadings = true;
      if (this.order == "desc") {
        this.order = "asc";
        this.ordertext = "升序";
        this.data2 = this.data2asc;
        this.loadings = false;
      } else {
        this.order = "desc";
        this.ordertext = "降序";
        this.data2 = this.data2desc;
        this.loadings = false;
      }
    },

qiehuan(){
  if(this.yearmonth=="按年查看"){
    this.yearmonth="按月查看";
    this.yearx=false;
    this.monx=true;
  }else{
    this.yearmonth="按年查看"
    this.yearx=true;
    this.monx=false;
  }

},

    //跳转页面
    handleSubmitRole(type) {
      console.log(type);
      if (type == "上级交办") {
        this.$router.push({
          name: "taskmanagement_alltask",
        });
      } else if (type == "下级上传") {
        this.$router.push({
          name: "taskmanagement_alltaskxiaji",
        });
      } else if (type == "已完成的任务") {
        this.$router.push({
          name: "taskmanagement_yiwanchengtask",
        });
      } else if (type == "办理中的任务") {
        this.$router.push({
          name: "taskmanagement_banlizhong",
        });
      } else if (type == "所有任务") {
        this.$router.push({
          name: "taskmanagement_suoyourenwu",
          
        });
      } else if (type == "已逾期的任务") {
        this.$router.push({
          name: "taskmanagement_yuqitask",
        });
      } else if (type == "今日到期的任务") {
        this.$router.push({
          name: "taskmanagement_suoyourenwu",
          query: { data: "jrdq" }, //参数
        });
      } else if (type == "逾期完成的任务") {
        this.$router.push({
          name: "taskmanagement_yuqitask",
          query: { data: "yqwc" }, //参数
        });
      }
    },
  },
  //页面加载调用
  mounted() {
    let date = new Date();
    let m=date.getMonth() + 1;
    let month=m<10?"0"+m:m;
    this.nowdyear=""+date.getFullYear();
    this.nowdmon =date.getFullYear() + "-" + month;
    this.loadVoteinitiateList();

  },
};
</script>

<style lang="less">
.count-style {
  font-size: 45px;
}
.table-font {
  color: #800000;
}
</style>
