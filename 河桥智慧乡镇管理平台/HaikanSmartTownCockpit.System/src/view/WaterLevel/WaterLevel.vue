<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.demo.query.totalCount"
        :pageSize="stores.demo.query.pageSize"
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
                      v-model="stores.demo.query.kw"
                      placeholder="请输入名称"
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
                  v-can="'importjiankong'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImportCuisinejk"
                  title="导入监控信息"
                >导入监控信息</Button>
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
          :data="stores.demo.data"
          :columns="stores.demo.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{ row, index }" slot="actionjk">
            <Tooltip placement="top" content="查看监控" :delay="1000" :transfer="true">
              <Button
                v-can="'chajiankong'"
                shape="circle"
                type="success"
                @click="handleDetailchakan(row)"
              >查看监控</Button>
            </Tooltip>
          </template>
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
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleDetail1(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      title="五水共治监控信息"
      v-model="stores1.opened"
      width="80%"
      :mask-closable="false"
      :mask="true"
    >
      <Card>
        <dz-table
          :totalCount="stores1.demo.query.totalCount"
          :pageSize="stores1.demo.query.pageSize"
          @on-page-change="handlePageChanged1"
          @on-page-size-change="handlePageSizeChanged1"
        >
          <div slot="searcher">
            <section class="dnc-toolbar-wrap">
              <Row :gutter="16">
                <Col span="10">
                  <Form inline @submit.native.prevent>
                    <FormItem></FormItem>
                  </Form>
                </Col>
                <Col span="14" class="dnc-toolbar-btns"></Col>
              </Row>
            </section>
          </div>
          <Table
            slot="table"
            ref="tables"
            :border="false"
            size="small"
            :highlight-row="true"
            :data="stores1.demo.data"
            :columns="stores1.demo.columns"
            @on-select="handleSelect"
            @on-selection-change="handleSelectionChange"
            @on-refresh="handleRefresh1"
            :row-class-name="rowClsRender"
            @on-page-change="handlePageChanged1"
            @on-page-size-change="handlePageSizeChanged1"
          >
            <template slot-scope="{ row, index }" slot="action"></template>
          </Table>
        </dz-table>
      </Card>
      <Drawer
        :title="formTitle"
        v-model="formModel.opened"
        width="500"
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
            <FormItem label="名称" prop="waterName">
              <Input v-model="formModel.fields.waterName" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="基本信息" prop="waterInfo">
              <Input v-model="formModel.fields.waterInfo" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="位置" prop="weizhi">
              <Input v-model="formModel.fields.weizhi" />
            </FormItem>
          </Row>
          <!-- <Row :gutter="16">
            <FormItem label="精准线" prop="accurate">
              <Input v-model="formModel.fields.accurate" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="水位" prop="watersw">
              <Input v-model="formModel.fields.watersw" />
            </FormItem>
          </Row>-->
          <Row :gutter="16">
            <FormItem label="经度" prop="lon">
              <Input v-model="formModel.fields.lon" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="纬度" prop="lat">
              <Input v-model="formModel.fields.lat" />
            </FormItem>
          </Row>
        </Form>
        <div class="demo-drawer-footer">
          <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
          <Button style="margin-left: 30px" icon="md-close" @click="formopen1">取 消</Button>
        </div>
      </Drawer>

      <Drawer
        title="详情"
        v-model="formModel1.opened"
        width="600"
        :mask-closable="false"
        :mask="false"
      >
        <Form :model="formModel1.fields" ref="formdispatch22" label-position="top">
          <Row :gutter="16">
            <FormItem label="名称" prop="waterName">
              <Input v-model="formModel1.fields.waterName" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="基本信息" prop="waterInfo">
              <Input v-model="formModel1.fields.waterInfo" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="位置" prop="weizhi">
              <Input v-model="formModel1.fields.weizhi" :readonly="true" />
            </FormItem>
          </Row>
          <!-- <Row :gutter="16">
            <FormItem label="精准线" prop="accurate">
              <Input v-model="formModel1.fields.accurate" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="水位" prop="watersw">
              <Input v-model="formModel1.fields.watersw" :readonly="true" />
            </FormItem>
          </Row>-->
          <Row :gutter="16">
            <FormItem label="经度" prop="lon">
              <Input v-model="formModel1.fields.lon" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="纬度" prop="lat">
              <Input v-model="formModel1.fields.lat" :readonly="true" />
            </FormItem>
          </Row>
        </Form>
        <div class="demo-drawer-footer">
          <Button style="margin-left: 30px" icon="md-close" @click="formopen2">取 消</Button>
        </div>
      </Drawer>
      <Drawer
        title="五水共治信息导入"
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
            title="五水共治信息导入模板下载"
          >五水共治信息导入模板下载</Button>
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
        <!-- <div class="demo-drawer-footer">
          <Button style="margin-left: 30px" icon="md-close" @click="formopen3">关 闭</Button>
        </div>-->
      </Drawer>
      <Drawer
        title="五水共治监控设备信息导入"
        v-model="formimportjk.opened"
        width="600"
        :mask-closable="true"
        :mask="true"
      >
        <div>
          <input
            ref="inputer"
            id="upload"
            type="file"
            @change="importfjk"
            accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel"
          />
          <Button
            icon="ios-cloud-download"
            type="warning"
            @click="handleimportmodeljk"
            title="五水共治监控设备信息导入模板下载"
          >五水共治监控设备信息导入模板下载</Button>
          <Button
            icon="md-checkmark-circle"
            type="primary"
            @click="handleimportjk"
            :disabled="importdisable"
          >导入</Button>

          <Tabs value="name1">
            <TabPane label="成功" name="name1" v-html="successmsg"></TabPane>
            <TabPane label="重复" name="name2" v-html="repeatmsg"></TabPane>
            <TabPane label="失败" name="name3" v-html="defaultmsg"></TabPane>
          </Tabs>
        </div>
        <!-- <div class="demo-drawer-footer">
          <Button style="margin-left: 30px" icon="md-close" @click="formopen3">关 闭</Button>
        </div>-->
      </Drawer>
      <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formopen">关 闭</Button>
      </div>
    </Drawer>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import {
  GetList, //显示列表
  Getlishijk, //查看历史监控
  GetCreate, //新增
  GetfoGet, //获取选定信息
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  GetEdit, //编辑
  GetImport, //导入
  GetImportjk, //监控导入
  GetExport //导出
} from "@/api/WaterLevel/WaterLevel";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "WaterLevel",
  components: {
    DzTable
  },
  data() {
    let validateName = (rule, value, callback) => {
      if (value !== "" && value !== null) {
        let reg = /^[^\s]+$/;
        if (!reg.test(value)) {
          callback(new Error("名称不合法"));
        }
        callback();
      } else {
        callback(new Error("名称不能为空"));
      }
      callback();
    };
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
      waterNamedd: "",
      //导入
      url: config.baseUrl.dev,
      importdisable: false,
      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
      },
      formimportjk: {
        opened: false
      },

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
      stores: {
        demo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2: "",
            kw3: "",
            kw4: "",
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
            { type: "selection", width: 50, key: "waterLevelUuid" },
            { title: "名称", key: "waterName" },
            { title: "基本信息", key: "waterInfo" },
            { title: "位置", key: "weizhi" },
            // { title: "精准线", key: "accurate" },
            // { title: "水位", key: "watersw" },
            { title: "经度", key: "lon" },
            { title: "纬度", key: "lat" },
            {
              title: "查看监控",
              align: "center",
              width: 100,
              className: "table-command-column",
              slot: "actionjk"
            },
            {
              title: "操作",
              align: "center",
              width: 200,
              className: "table-command-column",
              slot: "action"
            }
          ],
          data: []
        }
      },
      stores1: {
        opened: false,
        demo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2: "",
            kw3: "",
            kw4: "",
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
          columns: [
            { type: "selection", width: 50, key: "waterLevelShebUuid" },
            { title: "水库名称", key: "weizhi" },
            { title: "设备编号", key: "monitorWaterId" },
            { title: "设备类型", key: "shebeiType" },
            { title: "管理人", key: "adminPepo" }
            // { title: "报警数值", key: "yujingNum" },
            // { title: "预警状态", key: "yujingType" }
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
          lon: "",
          lat: "",
          waterInfo: "",
          weizhi: "",
          watersw: "",
          accurate: "",
          waterName: "",
          waterLevelUuid: ""
        },
        rules: {
          waterName: [
            { type: "string", required: true, validator: validateName }
          ],
          weizhi: [{ type: "string", required: true, message: "请输入位置" }],
          lon: [{ type: "string", required: true, validator: validateLon }],
          lat: [{ type: "string", required: true, validator: validateLat }]
        }
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          lon: "",
          lat: "",
          waterInfo: "",
          weizhi: "",
          watersw: "",
          accurate: "",
          waterName: "",
          waterLevelUuid: ""
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
      return this.formModel.selection.map(x => x.waterLevelUuid);
    } //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      GetList(this.stores.demo.query).then(res => {
        console.log(res);
        this.stores.demo.data = res.data.data;
        this.stores.demo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.demo.query.currentPage = page;
      this.loadDispatchList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.demo.query.pageSize = pageSize;
      this.loadDispatchList();
    },
    //翻页
    handlePageChanged1(page) {
      this.stores1.demo.query.currentPage = page;
      this.doCpcList();
    },
    //显示条数改变
    handlePageSizeChanged1(pageSize) {
      this.stores1.demo.query.pageSize = pageSize;
      this.doCpcList();
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
    //搜索
    handleSearchDispatch1() {
      this.doCpcList();
    },
    //刷新
    handleRefresh() {
      this.loadDispatchList();
    },
    //刷新
    handleRefresh1() {
      this.doCpcList();
    },
    //监控列表显示
    handleDetail(e) {
      console.log(e);
      this.stores1.opened = true;
      this.stores1.demo.query.kw2 = e.waterName;
      this.waterNamedd = e.waterName;
      this.doCpcList();
    },
    doCpcList() {
      HoruzhuGet(this.stores1.demo.query).then(res => {
        this.stores1.demo.data = res.data.data;
        this.stores1.demo.query.totalCount = res.data.totalCount;
      });
    },
    formopen1() {
      this.formModel.opened = false;
      this.doCpcList();
      this.stores1.opened = true;
    },
    formopen2() {
      this.formModel1.opened = false;
      this.doCpcList();
      this.stores1.opened = true;
    },
    formopen3() {
      this.formimport.opened = false;
      this.doCpcList();
      this.stores1.opened = true;
    },
    formopen() {
      this.stores1.opened = false;
      this.loadDispatchList();
    },
    //列表显示
    handleDetail1(e) {
      this.stores1.opened = false;
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(e.waterLevelUuid);
    },
    handleDetailchakan(e) {
      this.stores1.opened = true;
      this.doLoadjiankong(e.waterName);
    },
    //查询当前行信息
    doLoadjiankong(waterName) {
      this.stores1.demo.query.kw1 = waterName;
      console.log(this.stores1.demo.query.kw1);
      Getlishijk(this.stores1.demo.query).then(res => {
        this.stores1.demo.data = res.data.data;
        this.stores1.demo.query.totalCount = res.data.totalCount;
      });
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
      this.doDelete(row.waterLevelUuid);
    },
    doDelete(ids) {
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
          this.formModel.selection = [];
          this.loadDispatchList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    //添加按钮
    handleShowCreateWindow() {
      this.formModel.mode = "create";
      this.handleResetFormDispatch(); //清空表单
      this.stores1.opened = false;
      this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      this.formModel.mode = "edit";
      this.stores1.opened = false;
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.waterLevelUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      GetfoGet(id).then(res => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          let data = res.data.data[0];
          // let ttime = [Date.parse(data.rtime), Date.parse(data.ltime)];
          let ttime = [data.rtime, data.ltime];
          this.formModel.fields = data;
          this.formModel.fields.ruzhuTime = ttime;
          this.formModel1.fields = data;
          this.formModel1.fields.ruzhuTime = ttime;
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
      if (this.formModel.fields.waterInfo != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.waterInfo)) {
          this.$Message.warning("基本信息不合法!");
          return;
        }
      }
      if (this.formModel.fields.weizhi != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.weizhi)) {
          this.$Message.warning("位置不合法!");
          return;
        }
      } else {
        this.$Message.warning("位置不能为空!");
        return;
      }

      console.log(this.formModel.fields);
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
      if (this.formModel.fields.waterInfo != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.waterInfo)) {
          this.$Message.warning("基本信息不合法!");
          return;
        }
      }
      if (this.formModel.fields.weizhi != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.weizhi)) {
          this.$Message.warning("位置不合法!");
          return;
        }
      } else {
        this.$Message.warning("位置不能为空!");
        return;
      }
      //this.datadeal();
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
      this.stores1.opened = false;
      this.formimport.opened = true;
    },
    //下载模板
    handleimportmodel() {
      console.log(this.url);
      window.location.href =
        this.url + "UploadFiles/ImportUserInfoModel/五水共治信息导入模板.xlsx";
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

    //监控信息导入相关操作
    handleImportCuisinejk() {
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      this.$refs.inputer.value = "";
      this.isexitexceljk = false;
      this.stores1.opened = false;
      this.formimportjk.opened = true;
    },
    //下载模板
    handleimportmodeljk() {
      console.log(this.url);
      window.location.href =
        this.url +
        "UploadFiles/ImportUserInfoModel/五水共治监控设备信息导入模板.xlsx";
    },
    //导入
    importfjk(e) {
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
          this.isexitexceljk = true;
          formData.append("excelFile", file);
          this.exceldata = formData;
        } else {
          this.isexitexceljk = false;
        }
      }
    },
    async handleimportjk() {
      this.importdisable = true;
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      if (this.isexitexceljk) {
        await GetImportjk(this.exceldata).then(res => {
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
          this.isexitexceljk = false;
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
  }
};
</script>
<style>
</style>