<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.userinfo.query.totalCount"
        :pageSize="stores.userinfo.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw"
                      placeholder="请输入灾害名称"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                    v-can="'delete'"
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
                  ></Button>
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="添加"
                >添加</Button>
                <Button
                  v-can="'import'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImportCuisine"
                  title="导入"
                >导入</Button>
                <Button
                  v-can="'export'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisine('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisineyj('export')"
                  title="一键导出"
                >一键导出</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
          slot="table"
          ref="tables"
          :border="false"
          size="small"
          :highlight-row="true"
          :data="stores.userinfo.data"
          :columns="stores.userinfo.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <!-- <template slot-scope="{row, index}" slot="auditState">
            <span>{{renderAuditState(row.auditState)}}</span>
          </template>-->
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" v-can="'deletes'" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button
                v-can="'edit'"
                type="primary"
                size="small"
                shape="circle"
                icon="md-create"
                @click="handleEdit(row)"
              ></Button>
            </Tooltip>
            <!-- <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleDetail(row)"
              ></Button>
            </Tooltip>-->
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="1200"
      :mask-closable="false"
      :mask="true"
    >
      <Form
        :model="formModel.fields"
        ref="formdispatch"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <FormItem label="灾害名称" prop="disasterName">
            <Input v-model="formModel.fields.disasterName" placeholder="灾害名称" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="灾害位置" prop="disasterAddress">
            <Input v-model="formModel.fields.disasterAddress" placeholder="灾害位置" />
          </FormItem>
        </Row>

        <Row>
          <Col span="30">
            <ButtonGroup shape="circle" style="margin-right: 2px">
              <Button :type="button.add" @click="checkNoEdit">增加区域</Button>
              <Button :type="button.edit" @click="checkEdit">修改区域</Button>
            </ButtonGroup>
            <Button type="warning" @click="removeMarkers">清除全部坐标</Button>提示:左键点击放置点,右键结束区域绘制
            <baidu-map
              ref="sbdmp"
              v-if="formModel.opened"
              style="width: 1100px;height: 700px"
              :center="center"
              :zoom="zoom"
              :scroll-wheel-zoom="true"
              @click="paintPolyline"
              @mousemove="syncPolyline"
              @rightclick="newPolyline"
            >
              <bm-scale anchor="BMAP_ANCHOR_TOP_RIGHT"></bm-scale>
              <bm-navigation anchor="BMAP_ANCHOR_TOP_RIGHT"></bm-navigation>
              <bm-map-type
                :mapTypes="['BMAP_HYBRID_MAP','BMAP_NORMAL_MAP' ]"
                anchor="BMAP_ANCHOR_TOP_LEFT"
              ></bm-map-type>
              <bm-polygon
                :path="path"
                v-for="(path,index) in polygonPaths"
                :key="index"
                :editing="edit"
                stroke-color="blue"
                fill-color="#fb614d"
                :stroke-opacity="0.5"
                :stroke-weight="2"
                @lineupdate="updatePolygonPath($event,index)"
              ></bm-polygon>
            </baidu-map>
            <br />
          </Col>
        </Row>
        <!-- <Row :gutter="16">
          <FormItem label="处理状态" prop="disasterStaues">
            <Input v-model="formModel.fields.disasterStaues" placeholder="处理状态" />
          </FormItem>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="经度" prop="lon">
              <Input v-model="formModel.fields.lon" placeholder="经度" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="纬度" prop="lat">
              <Input v-model="formModel.fields.lat" placeholder="纬度" />
            </FormItem>
          </Col>
        </Row>-->
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer title="详情" v-model="formModel1.opened" width="600" :mask-closable="false" :mask="false">
      <Form :model="formModel1.fields" ref="formdispatch22" label-position="top">
        <Row :gutter="16">
          <FormItem label="灾害名称" prop="disasterName">
            <Input v-model="formModel1.fields.disasterName" placeholder="灾害名称" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="灾害位置" prop="disasterAddress">
            <Input v-model="formModel1.fields.disasterAddress" placeholder="灾害位置" :readonly="true" />
          </FormItem>
        </Row>
        <!-- <Row :gutter="16">
          <FormItem label="处理状态" prop="disasterStaues">
            <Input v-model="formModel1.fields.disasterStaues" placeholder="处理状态" :readonly="true" />
          </FormItem>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="经度" prop="lon">
              <Input v-model="formModel1.fields.lon" placeholder="经度" :readonly="true" />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="纬度" prop="lat">
              <Input v-model="formModel1.fields.lat" placeholder="纬度" :readonly="true" />
            </FormItem>
          </Col>
        </Row>-->
      </Form>
      <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formModel1.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="灾害点信息导入"
      v-model="formimport.opened"
      width="600"
      :mask-closable="true"
      :mask="true"
    >
      <div>
        <input
          ref="inputer"
          id="upload"
          type="file"
          @change="importfxx"
          accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
        />
        <Button
          icon="ios-cloud-download"
          type="warning"
          @click="handleimportmodel"
          title="灾害点信息导入模板下载"
        >灾害点信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleimport"
          :disabled="importdisable"
        >导入</Button>

        <Tabs value="name1">
          <TabPane label="成功" name="name1" v-html="successmsg"></TabPane>
          <TabPane label="重复" name="name2" v-html="repeatmsg"></TabPane>
          <TabPane label="失败" name="name3" v-html="defaultmsg"></TabPane>
        </Tabs>
      </div>
    </Drawer>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import {
  GetList, //显示列表
  GetCreate, //新增
  GetfoGet, //获取选定信息
  GetEdit, //编辑
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  GetImport, //导入
  GetExport //导出
} from "@/api/WeifangInfo/QtzaihaiInfo";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "QtzaihaiInfo",
  components: {
    DzTable
  },
  data() {
    let validateLon = (rule, value, callback) => {
      if (value !== "" && value !== null) {
        let reg = /^[\-\+]?(0?\d{1,2}\.\d{1,6}|1[0-7]?\d{1}\.\d{1,6}|180\.0{1,6})$/;
        if (!reg.test(value)) {
          callback(new Error("格式错误！范围在-180.0~180.0(小数点至多6位)"));
        }
        callback();
      } else {
        callback(new Error("经度不能为空"));
      }
      callback();
    };
    let validateLat = (rule, value, callback) => {
      if (value !== "" && value !== null) {
        let reg = /^[\-\+]?([0-8]?\d{1}\.\d{1,6}|90\.0{1,6})$/;
        if (!reg.test(value)) {
          callback(new Error("格式错误！范围在-90.0~90.0(小数点至多6位)"));
        }
        callback();
      } else {
        callback(new Error("纬度不能为空"));
      }
      callback();
    };
    return {
      //导入
      url: config.baseUrl.dev,
      importdisable: false,

      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
      },

      //地图
      isnullbrochure: false,
      zsb: false,
      row: {},
      returnTime: "",
      currentUser: {},
      schoolName: "",
      button: {
        add: "info",
        edit: "default"
      },
      ebPlan: {},
      checktoAdd: true,
      center: { lng: 119.250357, lat: 30.112423 },
      zoom: 18,
      lngs: [],
      lats: [],
      markers: [],
      edit: false,
      polygonPaths: [],
      showAlert: false,
      typeList: [],
      checkTypeList: [],

      actionurl: "",
      postheaders: "",
      loadingStatus: false,
      updisabled: false,
      visible: false,
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        Import: { name: "Import", title: "导入" },
        export: { name: "export", title: "导出" }
      },
      inglist: [],
      model1: [],
      model2: [],
      stores: {
        userinfo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2: "",
            isDelete: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID"
              }
            ]
          },
          sources: {},
          //列表显示
          columns: [
            { type: "selection", minwidth: 50, key: "disasterInfoUuid" },
            {
              title: "灾害名称",
              key: "disasterName",
              minWidth: 120,
              sortable: true
            },
            {
              title: "灾害位置",
              key: "disasterAddress",
              minWidth: 150,
              sortable: true
            },
            // {
            //   title: "处理状态",
            //   key: "disasterStaues",
            //   minWidth: 100,
            //   sortable: true
            // },

            // {
            //   title: "经度",
            //   key: "lon",
            //   minWidth: 100,
            //   sortable: true
            // },
            // {
            //   title: "纬度",
            //   key: "lat",
            //   minWidth: 100,
            //   sortable: true
            // },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 100,

              className: "table-command-column",
              slot: "action"
            }
          ],
          data: []
        }
      },
      formModel: {
        opened: false,
        title: "创建申请",
        mode: "create",
        dFileName: "xxxx",
        selection: [],
        fields: {
          disasterName: "",
          disasterAddress: "",
          mapRegion: ""
          // disasterStaues: "",
          // disasterTime: "",
          // disasterInfoUuid: "",
          // lon: "",
          // lat: ""
        },
        rules: {
          disasterName: [
            { type: "string", required: true, message: "灾害名称不能为空" }
          ]
          // lon: [{ type: "string", required: true, validator: validateLon }],
          // lat: [{ type: "string", required: true, validator: validateLat }]
        }
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          disasterName: "",
          disasterAddress: "",
          mapRegion: ""
          // disasterStaues: "",
          // disasterTime: "",
          // disasterInfoUuid: "",
          // lon: "",
          // lat: ""
        }
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增信息";
      }
      if (this.formModel.mode === "edit") {
        return "编辑信息";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.disasterInfoUuid);
    } //删除
  },
  methods: {
    //关闭编辑
    checkNoEdit() {
      this.checktoAdd = true;
      this.button.add = "info";
      this.button.edit = "default";
      this.edit = false;
      // console.log("添加新点");
      // console.log(this.polygonPaths);
    },
    //开启编辑
    checkEdit() {
      this.checktoAdd = false;
      this.button.add = "default";
      this.button.edit = "info";
      this.edit = true;
      if (this.polygonPaths[this.polygonPaths.length - 1].length !== 0) {
        this.polygonPaths.push([]);
      }
    },
    //设置点
    paintPolyline(e) {
      //  console.log("e");
      // console.log(e);

      if (this.checktoAdd === false) {
        return;
      }
      if (this.polygonPaths.length === 0) {
        this.polygonPaths.push([]);
      }

      this.polygonPaths[this.polygonPaths.length - 1].push(e.point);
      // let map=this.$refs.sbdmp.map;
      // let marker=new BMap.Marker(e.point);
      // map.addOverlay(marker);
    },
    //右键结束区域构建
    newPolyline() {
      if (!this.checktoAdd) {
        return;
      }
      if (this.polygonPaths[this.polygonPaths.length - 1].length !== 0) {
        this.polygonPaths.push([]);
      }
    },
    //加点时的覆盖变化
    syncPolyline(e) {
      if (!this.checktoAdd) {
        return;
      }

      if (!this.polygonPaths.length) {
        return;
      }
      const path = this.polygonPaths[this.polygonPaths.length - 1];
      if (!path.length) {
        return;
      }
      console.log(e.point);
      if (path.length === 1) {
        path.push(e.point);
      }
      this.$set(path, path.length - 1, e.point);
    },
    //移除所有覆盖物
    removeMarkers() {
      //  console.log("xxxxxxxx");
      this.polygonPaths = [];
      this.markers = [];
      let map = this.$refs.sbdmp.map;
      //  console.log(map);
      // map.clearOverlays();
      map.setCenter(new BMap.Point(119.250357, 30.112423));
    },
    //设置标注点
    setmap() {
      let map = this.$refs.sbdmp.map;
      //  console.log("polygonpath.length");
      //  console.log(this.polygonPaths.length);
      for (let i = 0; i < this.polygonPaths.length; i++) {
        for (let j = 0; j < this.polygonPaths[i].length; j++) {
          let marker = new BMap.Marker(this.polygonPaths[i][j]);
          map.addOverlay(marker);
        }
      }
    },
    //多边形覆盖物属性改变事件
    updatePolygonPath(e, index) {
      console.log("index");
      console.log(index);
      console.log("e");
      console.log(e);
      console.log(e.target.getPath());
      if (e.target === undefined) {
        return;
      }
      // if (e.target.Ra === undefined) {
      //   return;
      // }
      // if (e.target.Ra.pM === undefined) {
      //   return;
      // }
      console.log(e);
      //  console.log(e.target.Ra.toString());
      let map = this.$refs.sbdmp.map;

      this.polygonPaths[index] = e.target.getPath();
      console.log("polygonpath");
      console.log(this.polygonPaths);
    },

    //页面加载
    loadDispatchList() {
      GetList(this.stores.userinfo.query).then(res => {
        //console.log(res.data.data);
        this.stores.userinfo.data = res.data.data;
        this.stores.userinfo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.userinfo.query.currentPage = page;
      this.loadDispatchList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.userinfo.query.pageSize = pageSize;
      this.loadDispatchList();
    },
    //行样式
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //搜索
    handleSearchDispatch() {
      this.loadDispatchList();
    },
    //刷新
    handleRefresh() {
      this.loadDispatchList();
    },
    //清空
    handleResetFormDispatch() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch"].resetFields();
    },
    //清空
    handleResetFormDispatch22() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch22"].resetFields();
    },
    //右边删除（单个删除）
    handleDelete(row) {
      this.doDelete(row.disasterInfoUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteDepartment(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //右上边删除按钮
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doBatchCommand(command);
        }
      });
    },
    //右上边删除
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },

    xz(e) {
      this.stores.userinfo.query.kw = e;
      this.loadDispatchList();
    },
    //添加按钮
    handleShowCreateWindow() {
      this.markers = [];
      this.polygonPaths = [];
      this.formModel.mode = "create";
      this.handleResetFormDispatch(); //清空表单
      this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      this.markers = [];
      this.polygonPaths = [];
      this.formModel.mode = "edit";
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.disasterInfoUuid);
    },
    //右边详情
    handleDetail(row) {
      this.markers = [];
      this.polygonPaths = [];
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(row.disasterInfoUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      GetfoGet(id).then(res => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          let data = res.data.data[0];
          this.formModel.fields = data;
          this.formModel1.fields = data;
          //初始化地图元素过程
          //  console.log("查询时获得的mapRegion");
          //  console.log(this.formModel.fields.mapRegion);
          //  console.log(this.$refs.sbdmp);
          //获取地图元素
          var map = this.$refs.sbdmp.map;
          //  console.log(map);
          this.lngs = [];
          this.lats = [];
          this.polygonPaths = [];
          //初始化地图覆盖物坐标点
          let s = this.formModel.fields.mapRegion;
          //  console.log(s.charAt(s.length - 1));
          if (s == null || s == "") {
            return;
          }
          //清除末尾的|
          if (s.charAt(s.length - 1) === "|") {
            s = s.substr(0, s.length - 1);
          }
          //  console.log(s);
          //将数据分成多个覆盖物
          let sc = s.split("-");
          ////console.log("sc");
          //  console.log(sc);

          for (let n = 0; n < sc.length; n++) {
            //将单个覆盖物数据分成多个坐标数据
            //将两头的|去除
            if (sc[n].charAt(0) === "|") {
              sc[n] = sc[n].substr(1);
            }
            if (sc[n].charAt(sc[n].length - 1) === "|") {
              sc[n] = sc[n].substr(0, sc[n].length - 1);
            }
            //  console.log(sc[n]);
            let pl = sc[n].split("|");

            // console.log("pl");
            //  console.log(pl);
            //初始化存储坐标的数组
            let path = [];
            for (let i = 0; i < pl.length; i++) {
              //将单个坐标数据分出经纬值
              let ps = pl[i].split(",");
              this.lngs.push(parseFloat(ps[0]));
              this.lats.push(parseFloat(ps[1]));
              let p = new BMap.Point(parseFloat(ps[0]), parseFloat(ps[1]));
              //  console.log("p");
              //  console.log(p);
              path.push(p);
            }
            // console.log("第" + n + "个覆盖物");
            //  console.log(path);
            this.polygonPaths.push(path);
          }

          // else{   //新数据处理方式("x,y|x,y|...|x,y")
          //  //console.log("新数据");
          //     let pl=s.split("|");
          //  //console.log("pl");
          //  //console.log(pl);
          //     for(let i=0;i<pl.length;i++){
          //         //将单个坐标数据分出经纬值
          //         let ps=pl[i].split(",");
          //         this.lngs.push(parseFloat(ps[0]));
          //         this.lats.push(parseFloat(ps[1]));
          //         let p=new BMap.Point(parseFloat(ps[0]),parseFloat(ps[1]));
          //      //console.log("p");
          //      //console.log(p);
          //         this.polygonPaths.push(p);
          //     }
          //  //console.log("新数据polygon");
          //  //console.log(this.polygonPaths);
          // }
          //添加覆盖物
          // let pg=new BMap.Polygon(this.polygonPath,{fillColor:"violet", strokeWeight:2, strokeOpacity:0.4});
          // map.addOverlay(pg);
          //设置中心点,及放大级别
          let clng =
            (Math.max.apply(null, this.lngs) +
              Math.min.apply(null, this.lngs)) /
            2;
          let clat =
            (Math.max.apply(null, this.lats) +
              Math.min.apply(null, this.lats)) /
            2;
          //  console.log(clng);
          // console.log(clat);
          if (clng === 0 && clat === 0) {
            map.setCenter({ lng: 120.248056, lat: 29.296082 });
          }
          if (!isNaN(clng) && !isNaN(clat)) {
            map.setCenter(new BMap.Point(clng, clat));
          }
          map.setZoom(15);
          //添加标注提示点
          // this.setmap();
          // for(let i=0;i<this.polygonPath.length;i++){
          //     var marker = new BMap.Marker(this.polygonPath[i]);
          //
          //     marker.setLabel(new BMap.Label("index:"+i,{offset:new BMap.Size(20,-10)}));
          //     map.addOverlay(marker);
          // }
          //编辑时启用覆盖物可编辑
          // if (this.formModel.mode === "edit") {
          //     this.edit = true;
          // }
          // if (this.formModel.mode === "show") {
          //     this.edit = false;
          // }
          //  console.log("数据加载完成后的paths");
          //  console.log(this.polygonPaths);
          this.polygonPaths.push([]);
          //  console.log(this.polygonPaths);
        }
      });
    },
    validateUserForm() {
      let _valid = false;
      this.$refs["formdispatch"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //保存按钮
    handleSubmitConsumable() {
      let valid = this.validateUserForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateDispatch();
        }
        if (this.formModel.mode === "edit") {
          this.doEditDispatch();
        }
      }
    },
    //添加（保存）
    docreateDispatch() {
      //写入地图覆盖物路径
      let spath = "";
      for (let i = 0; i < this.polygonPaths.length; i++) {
        if (spath !== "") {
          spath += "-";
        }
        for (let j = 0; j < this.polygonPaths[i].length; j++) {
          if (spath !== "" && spath.charAt(spath.length - 1) !== "-") {
            spath += "|";
          }
          spath +=
            this.polygonPaths[i][j].lng + "," + this.polygonPaths[i][j].lat;
        }
      }
      if (spath.charAt(spath.length - 1) === "-") {
        spath = spath.substr(0, spath.length - 1);
      }
      this.formModel.fields.mapRegion = spath;
      GetCreate(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      //写入地图覆盖物路径
      let spath = "";
      for (let i = 0; i < this.polygonPaths.length; i++) {
        if (spath !== "") {
          spath += "-";
        }
        for (let j = 0; j < this.polygonPaths[i].length; j++) {
          if (spath !== "" && spath.charAt(spath.length - 1) !== "-") {
            spath += "|";
          }
          spath +=
            this.polygonPaths[i][j].lng + "," + this.polygonPaths[i][j].lat;
        }
      }
      if (spath.charAt(spath.length - 1) === "-") {
        spath = spath.substr(0, spath.length - 1);
      }
      //  console.log("保存的path");
      //  console.log(spath);
      // this.formModel.fields.mapRegion="";
      this.formModel.fields.mapRegion = spath;
      GetEdit(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //导入相关操作
    handleImportCuisine() {
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      this.$refs.inputer.value = "";
      this.isexitexcel = false;
      this.formimport.opened = true;
    },
    //下载模板
    handleimportmodel() {
      console.log(this.url);
      window.location.href =
        this.url + "UploadFiles/ImportUserInfoModel/灾害点信息导入模板.xlsx";
    },
    //导入
    importfxx(e) {
      let inputDOM = this.$refs.inputer;
      //声明一个FormDate对象
      let formData = new FormData();
      let file = inputDOM.files[0];
      let AllUpExt = ".xls|.xlsx|";
      let extName = file.name
        .substring(file.name.lastIndexOf("."))
        .toLowerCase();
      if (AllUpExt.indexOf(extName + "|") == "-1") {
        this.$refs.inputer.value = "";
        this.$Message.warning("文件格式不正确!");
      } else {
        if (file != null && file != undefined) {
          this.isexitexcel = true;
          formData.append("excelFile", file);
          this.exceldata = formData;
        } else {
          this.isexitexcel = false;
        }
      }
    },
    async handleimport() {
      this.importdisable = true;
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      if (this.isexitexcel) {
        await GetImport(this.exceldata).then(res => {
          if (res.data.code == 200) {
            this.time = res.data.data.time;
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadDispatchList();
          } else {
            this.$Message.warning(res.data.message);
          }
          this.$refs.inputer.value = "";
          this.exceldata = new FormData();
          this.isexitexcel = false;
        });
      } else {
        this.$Message.warning("请选择文件");
      }
      this.importdisable = false;
    },
    //导出
    handleExportCuisine(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doCuisineExport(command);
        }
      });
    },
    doCuisineExport(command) {
      console.log(this.selectedRowsId.join(","));
      GetExport(this.selectedRowsId.join(",")).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.selection = [];
          console.log(res);
          window.location.href = config.baseUrl.dev + res.data.data;
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    //一键导出
    handleExportCuisineyj(command) {
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doCuisineExport(command);
        }
      });
    }
  },
  mounted() {
    this.loadDispatchList(); //页面加载
  },
  created() {
    this.postheaders = {
      Authorization: "Bearer " + getToken()
      //Accept: "application/json, text/plain, */*"
    };
    this.actionurl = config.baseUrl.dev + "api/v1/HqCommuna/HqCommuna/UpLoad";
  }
};
</script>
<style>
.file1:hover + .fileimg1:hover {
  transform: scale(1.01, 1.01);
  box-shadow: 0 0 3px #1783ba;
}
.fileimg1 {
  width: 300px;
  height: 169px;
  float: left;
  z-index: 2;
}

.demo-upload-list {
  display: inline-block;
  width: 120px;
  height: 120px;
  text-align: center;
  line-height: 120px;
  border: 1px solid transparent;
  border-radius: 4px;
  overflow: hidden;
  background: #fff;
  position: relative;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
  margin-right: 4px;
}
.demo-upload-list img {
  width: 100%;
  height: 100%;
}
.demo-upload-list-cover {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.6);
}
.demo-upload-list:hover .demo-upload-list-cover {
  display: block;
}
.demo-upload-list-cover i {
  color: #fff;
  font-size: 20px;
  cursor: pointer;
  margin: 0 2px;
}
</style>