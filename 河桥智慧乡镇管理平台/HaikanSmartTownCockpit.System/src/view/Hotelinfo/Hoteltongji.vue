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
                      placeholder="请输入酒店民宿名称"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
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
          <template slot-scope="{ row, index }" slot="action">
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleDetail(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      title="酒店民宿入住信息"
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
                    <FormItem>
                      <Input
                        style="width: 150px;"
                        type="text"
                        search
                        :clearable="true"
                        v-model="stores1.demo.query.kw3"
                        placeholder="登记姓名"
                        @on-search="handleSearchDispatch1()"
                      ></Input>
                    </FormItem>
                  </Form>
                </Col>
                <Col span="14" class="dnc-toolbar-btns">
                  <ButtonGroup class="mr3">
                    <Button
                      v-can="'delete'"
                      class="txt-danger"
                      icon="md-trash"
                      title="删除"
                      @click="handleBatchCommand('delete')"
                    ></Button>
                    <Button icon="md-refresh" title="刷新" @click="handleRefresh1"></Button>
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
            :data="stores1.demo.data"
            :columns="stores1.demo.columns"
            @on-select="handleSelect"
            @on-selection-change="handleSelectionChange"
            @on-refresh="handleRefresh1"
            :row-class-name="rowClsRender"
            @on-page-change="handlePageChanged1"
            @on-page-size-change="handlePageSizeChanged1"
          >
            <template slot-scope="{ row, index }" slot="action">
              <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
                <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                  <Button
                    type="error"
                    v-can="'deletes'"
                    size="small"
                    shape="circle"
                    icon="md-trash"
                  ></Button>
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
                  v-can="'show1'"
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
            <FormItem label="民宿名称" prop="hotelName">
              <Input v-model="formModel.fields.hotelName" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="入住登记姓名" prop="ruzhuName">
              <Input v-model="formModel.fields.ruzhuName" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="入住人数" prop="ruzhuPeople">
              <!-- <Input v-model="formModel.fields.ruzhuPeople" /> -->
              <Input
                v-model="formModel.fields.ruzhuPeople"
                @keyup.native="formModel.fields.ruzhuPeople=formModel.fields.ruzhuPeople.replace(/[^\d]/g,'')"
                style="width: 400px"
                :maxlength="3"
              />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="价格" prop="ruzhuMoney">
              <Input v-model="formModel.fields.ruzhuMoney" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="房间号" prop="ruzhuRoom">
              <Input v-model="formModel.fields.ruzhuRoom" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="住店时间" prop="ruzhuTime">
              <DatePicker
                type="daterange"
                v-model="formModel.fields.ruzhuTime"
                format="yyyy-MM-dd"
                placement="top"
                placeholder="输入入住时间的"
                style="width: 100%"
              ></DatePicker>
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
            <FormItem label="民宿名称" prop="hotelName">
              <Input v-model="formModel1.fields.hotelName" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="入住登记姓名" prop="ruzhuName">
              <Input v-model="formModel1.fields.ruzhuName" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="入住人数" prop="ruzhuPeople">
              <Input v-model="formModel1.fields.ruzhuPeople" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="价格" prop="ruzhuMoney">
              <Input v-model="formModel1.fields.ruzhuMoney" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="房间号" prop="ruzhuRoom">
              <Input v-model="formModel1.fields.ruzhuRoom" :readonly="true" />
            </FormItem>
          </Row>
          <Row :gutter="16">
            <FormItem label="住店时间" prop="ruzhuTime">
              <DatePicker
                type="daterange"
                v-model="formModel1.fields.ruzhuTime"
                format="yyyy-MM-dd"
                placement="top"
                placeholder="输入入住时间的"
                style="width: 100%"
                disabled
              ></DatePicker>
            </FormItem>
          </Row>
        </Form>
        <div class="demo-drawer-footer">
          <Button style="margin-left: 30px" icon="md-close" @click="formopen2">取 消</Button>
        </div>
      </Drawer>
      <Drawer
        title="民宿入住信息导入"
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
            title="民宿入住信息导入模板下载"
          >民宿入住信息导入模板下载</Button>
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
        </div> -->
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
  RuzhuList, //显示全部酒店民宿入住信息统计列表
  HoruzhuGet, //显示当前酒店民宿入住信息列表
  RuzhuCreate, //新增
  RuzhufoGet, //获取选定信息
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  RuzhuEdit, //编辑
  RuzhuImport, //导入
  RuzhuExport //导出
} from "@/api/Hotelinfo/Hoteltongji";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "Hoteltongji",
  components: {
    DzTable
  },
  data() {
    let checkNum = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请输入"));
      } else if (value <= 0) {
        callback(new Error("请输入大于0的数字"));
      } else {
        callback();
      }
    };
    return {
      hotelNamedd: "",
      //导入
      url: config.baseUrl.dev,
      importdisable: false,

      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
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
            { title: "民宿名称", key: "hotelName" },
            { title: "日订单", key: "rdingdan" },
            { title: "日入住人数", key: "rrenshu" },
            { title: "月订单", key: "ydingdan" },
            { title: "月入住人数", key: "yrenshu" },
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
            { type: "selection", width: 50, key: "ruzhuInfoUuid" },
            { title: "入住登记姓名", key: "ruzhuName" },
            { title: "入住人数", key: "ruzhuPeople" },
            { title: "房间号", key: "ruzhuRoom" },
            { title: "价格", key: "ruzhuMoney" },
            { title: "入住天数", key: "ztianshu" },
            { title: "入住时间", key: "ruzhuTime" },
            { title: "离开时间", key: "likaiTime" },
            { title: "民宿名称", key: "hotelName" },
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
          ruzhuName: "",
          ruzhuPeople: "",
          ruzhuRoom: "",
          ruzhuMoney: "",
          ruzhuDay: "",
          ruzhuTime: [],
          likaiTime: "",
          hotelName: "",
          ruzhuInfoUuid: ""
        },
        rules: {
          ruzhuName: [
            { type: "string", required: true, message: "请输入入住登记姓名" }
          ],
          // ruzhuPeople: [
          //   {required: true, message: "请输入入住人数" }
          // ],
          ruzhuTime: [{ required: true, message: "选择入住时间" }],
          ruzhuPeople: [
            { required: true, message: "请输入入住人数" },
            { validator: checkNum, trigger: "blur" }
          ],
          ruzhuRoom: [
            { type: "string", required: true, message: "请输入房间号" }
          ]
        }
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          ruzhuName: "",
          ruzhuPeople: "",
          ruzhuRoom: "",
          ruzhuMoney: "",
          ruzhuDay: "",
          ruzhuTime: [],
          likaiTime: "",
          hotelName: "",
          ruzhuInfoUuid: ""
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
      return this.formModel.selection.map(x => x.ruzhuInfoUuid);
    } //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      RuzhuList(this.stores.demo.query).then(res => {
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
    //酒店民宿列表显示
    handleDetail(e) {
      console.log(e);
      this.stores1.opened = true;
      this.stores1.demo.query.kw2 = e.hotelName;
      this.hotelNamedd = e.hotelName;
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
      this.doLoadData(e.ruzhuInfoUuid);
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
      this.doDelete(row.ruzhuInfoUuid);
    },
    doDelete(ids) {
      deleteDepartment(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.doCpcList();
          this.stores1.opened = true;
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
          this.doCpcList();
          this.stores1.opened = true;
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
      this.formModel.fields.hotelName = this.hotelNamedd;
    },
    //右边编辑
    handleEdit(row) {
      this.formModel.mode = "edit";
      this.stores1.opened = false;
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.ruzhuInfoUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      RuzhufoGet(id).then(res => {
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
      if (this.formModel.fields.ruzhuTime[0] == "") {
        this.$Message.warning("请选择住店时间!");
        return;
      }
      if (this.formModel.fields.ruzhuName != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.ruzhuName)) {
          this.$Message.warning("入住登记姓名不合法!");
          return;
        }
      } else {
        this.$Message.warning("入住登记姓名不能为空!");
        return;
      }
      console.log(this.formModel.fields);
      RuzhuCreate(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.doCpcList();
          this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      if (this.formModel.fields.ruzhuName != "") {
        let reg = /^[^\s]+$/;
        if (!reg.test(this.formModel.fields.ruzhuName)) {
          this.$Message.warning("入住登记姓名不合法!");
          return;
        }
      } else {
        this.$Message.warning("入住登记姓名不能为空!");
        return;
      }
      //this.datadeal();
      RuzhuEdit(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.doCpcList();
          this.stores1.opened = true;
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
        this.url +
        "UploadFiles/ImportUserInfoModel/民宿入住信息导入模板.xlsx";
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
        await RuzhuImport(this.exceldata).then(res => {
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
      let data = {
        ids: this.selectedRowsId.join(","),
        htname: this.hotelNamedd
      };
      RuzhuExport(data).then(res => {
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