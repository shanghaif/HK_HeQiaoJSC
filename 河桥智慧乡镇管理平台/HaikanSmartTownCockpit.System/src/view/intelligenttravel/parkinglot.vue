<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.parkinglot.query.totalCount"
        :pageSize="stores.parkinglot.query.pageSize"
        :currentPage="stores.parkinglot.query.currentPage"
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
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.parkinglot.query.kw"
                      placeholder="输入名称/位置搜索..."
                      @on-search="handleSearchParkinglot()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.parkinglot.query.isDeleted"
                        @on-change="handleSearchParkinglot"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.parkinglot.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                      <!--                      <Select-->
                      <!--                        slot="prepend"-->
                      <!--                        v-model="stores.parkinglot.query.state"-->
                      <!--                        @on-change="handleSearchParkinglot"-->
                      <!--                        placeholder="响应状态"-->
                      <!--                        style="width:100px;"-->
                      <!--                      >-->
                      <!--                        <Option-->
                      <!--                          v-for="item in stores.parkinglot.sources.stateSources"-->
                      <!--                          :value="item.value"-->
                      <!--                          :key="item.value"-->
                      <!--                        >{{item.text}}</Option>-->
                      <!--                      </Select>-->
                    </Input>
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
                  <Button
                    v-can="'recover'"
                    class="txt-success"
                    icon="md-redo"
                    title="恢复"
                    @click="handleBatchCommand('recover')"
                  ></Button>

                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增停车场"
                >新增停车场</Button>
                <Button
                  v-can="'import'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImport"
                  title="导入"
                >导入</Button>
                <Button
                  v-can="'export'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportParkinglot('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportParkinglotyj('export')"
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
          :data="stores.parkinglot.data"
          :columns="stores.parkinglot.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{row,index}" slot="state">
            <span>{{renderState(row.state)}}</span>
          </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button v-can="'deletes'" type="error" size="small" shape="circle" icon="md-trash"></Button>
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
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="false"
      :styles="styles"
    >
      <Form
        v-if="formModel.opened"
        :model="formModel.fields"
        ref="formPlan"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="名称" prop="parkingLotName">
              <Input
                v-model.trim="formModel.fields.parkingLotName"
                placeholder="请输入名称"
                style="width: 500px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="位置" prop="parkingLotAddress">
              <Input
                v-model.trim="formModel.fields.parkingLotAddress"
                placeholder="请输入位置"
                style="width: 500px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="总车位" prop="zchewei">
              <Input
                v-model.trim="formModel.fields.zchewei"
                placeholder="请输入总车位"
                style="width: 500px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="已用车位" prop="ychewei">
              <Input
                v-model.trim="formModel.fields.ychewei"
                placeholder="请输入已用车位"
                style="width: 500px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="剩余车位" prop="schewei">
              <Input
                v-model.trim="formModel.fields.schewei"
                placeholder="请输入剩余车位"
                style="width: 500px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <!-- <Row :gutter="16">
          <Col span="12">
            <FormItem label="收入" prop="parkingLotsru">
              <Input
                v-model.trim="formModel.fields.parkingLotsru"
                placeholder="请输入收入"
                style="width: 500px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row> -->
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="经度" prop="lon">
              <Input v-model="formModel.fields.lon" style="width: 500px" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="纬度" prop="lat">
              <Input v-model="formModel.fields.lat" style="width: 500px" />
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkparkinglot-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="停车场信息导入"
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
          title="停车场信息导入模板下载"
        >停车场信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportParkinglot"
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
import Tables from "_c/tables";
import {
  getParkinglotList,
  createParkinglot,
  loadParkinglot,
  editParkinglot,
  deleteParkinglot,
  batchCommand,
  ParkinglotImport,
  ParkinglotExport
} from "@/api/intelligenttravel/parkinglot";
import { checkLon, checkLat } from "@/global/validate";
import config from "@/config";
export default {
  name: "parkinglot",
  components: {
    Tables,
    DzTable
  },
  data() {
    const isNum = (rule, value, callback) => {
      const age = /^[+]{0,1}(\d+)$|^[+]{0,1}(\d+\.\d+)$/;
      if (!age.test(value)) {
        callback(new Error("只能为大于等于0的数字"));
      } else {
        callback();
      }
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
      //导入
      url: config.baseUrl.dev,
      importdisable: false,
      isexitexcel: false,
      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
      },

      commands: {
        export: { name: "export", title: "导出" },
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },
      formModel: {
        opened: false,
        title: "创建停车场",
        mode: "create",
        selection: [],
        fields: {
          parkingLotName: "",
          parkingLotAddress: "",
          zchewei: "",
          ychewei: "",
          schewei: "",
          parkingLotsru: "",
          lon: "",
          lat: ""
        },
        rules: {
          // parkingLotName: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入名称",
          //     trigger: "blur"
          //   }
          // ],
          // parkingLotAddress: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入位置",
          //     trigger: "blur"
          //   }
          // ],
          // zchewei: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入总车位",
          //     trigger: "blur"
          //   }
          // ],
          // ychewei: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入已用车位",
          //     trigger: "blur"
          //   }
          // ],
          // schewei: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入剩余车位",
          //     trigger: "blur"
          //   }
          // ],
          // // parkingLotsru: [
          // //   {
          // //     type: "string",
          // //     required: true,
          // //     message: "请输入收入",
          // //     trigger: "blur"
          // //   },
          // //   { validator: isNum, trigger: "blur" }
          // // ],
          // lon: [{ type: "string", required: true, validator: validateLon }],
          // lat: [{ type: "string", required: true, validator: validateLat }]
        }
      },
      stores: {
        parkinglot: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            isDeleted: 0,
            //state:-1,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID"
              }
            ]
          },
          sources: {
            isDeletedSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "正常" },
              { value: 1, text: "已删" }
            ],
            stateSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "未响应" },
              { value: 1, text: "已响应" }
            ]
          },
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "名称", key: "parkingLotName", sortable: true },
            { title: "位置", key: "parkingLotAddress", sortable: true },
            { title: "总车位", key: "zchewei", sortable: true },
            { title: "已用车位", key: "ychewei", sortable: true },
            { title: "剩余车位", key: "schewei", sortable: true },
            // { title: "收入", key: "parkingLotsru", sortable: true },
            { title: "经度", key: "lon", sortable: true },
            { title: "纬度", key: "lat", sortable: true },
            {
              title: "操作",
              fixed: "right",
              align: "center",
              width: 150,
              className: "table-command-column",
              slot: "action"
            }
          ],
          data: []
        }
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      },
      initdatacopy: {
        parkingLotName: "",
        parkingLotAddress: "",
        zchewei: "",
        ychewei: "",
        schewei: "",
        parkingLotsru: "",
        lon: "",
        lat: ""
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增停车场";
      }
      if (this.formModel.mode === "edit") {
        return "编辑停车场";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.parkingLotUuid);
    }
  },
  methods: {
    renderState(state) {
      let _state = "未知";
      switch (state) {
        case 0:
          _state = "未响应";
          break;
        case 1:
          _state = "已响应";
          break;
      }
      return _state;
    },
    loadParkinglotList() {
      getParkinglotList(this.stores.parkinglot.query).then(res => {
        this.stores.parkinglot.data = res.data.data;
        this.stores.parkinglot.query.totalCount = res.data.totalCount;
        //console.log(this.stores.educaplan.data);
      });
    },
    handleSearchParkinglot() {
      this.stores.parkinglot.query.currentPage = 1;
      this.loadParkinglotList();
    },
    handleRefresh() {
      this.stores.parkinglot.query.currentPage = 1;
      this.loadParkinglotList();
    },
    //创建，修改
    handleSubmitPlan() {
      let valid = this.validateForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateParkinglot();
        }
        if (this.formModel.mode === "edit") {
          this.doEditPlan();
        }
      }
    },
    docreateParkinglot() {
      createParkinglot(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadParkinglotList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doEditPlan() {
      editParkinglot(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadParkinglotList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    validateForm() {
      let _valid = false;
      this.$refs["formPlan"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //批量操作
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
      //addsystemlog("delete","删除年度市级招生方案列表");
    },
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadParkinglotList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //单条删除
    handleDelete(row) {
      this.doDelete(row.parkingLotUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteParkinglot(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadParkinglotList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //控制弹出子窗体
    handleOpenFormWindow() {
      this.formModel.opened = true;
    },
    handleCloseFormWindow() {
      this.formModel.opened = false;
    },
    //编辑
    handleEdit(row) {
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadParkinglot(row.parkingLotUuid);
    },

    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.handleResetFormRole();
    },
    handleSwitchFormModeToCreate() {
      this.formModel.mode = "create";
      this.handleOpenFormWindow();
    },
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    handleResetFormRole() {
      this.formModel.fields = JSON.parse(JSON.stringify(this.initdatacopy));
      //this.$refs["formPlan"].resetFields();
    },
    doLoadParkinglot(guid) {
      loadParkinglot({ guid: guid }).then(res => {
        this.formModel.fields = res.data.data;
      });
    },
    handlePageChanged(page) {
      this.stores.parkinglot.query.currentPage = page;
      this.loadParkinglotList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.parkinglot.query.pageSize = pageSize;
      this.loadParkinglotList();
    },

    //导入相关操作
    handleImport() {
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
        this.url +
        "UploadFiles/ImportIntelligenttravelModel/停车场信息导入模板.xlsx";
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
    async handleImportParkinglot() {
      this.importdisable = true;
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      if (this.isexitexcel) {
        await ParkinglotImport(this.exceldata).then(res => {
          if (res.data.code == 200) {
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadParkinglotList();
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
    handleExportParkinglot(command) {
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
          this.doParkinglotExport(command);
        }
      });
    },
    //一键导出
    handleExportParkinglotyj(command) {
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doParkinglotExport(command);
        }
      });
    },
    doParkinglotExport(command) {
      ParkinglotExport(this.selectedRowsId.join(",")).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.selection = [];

          window.location.href = config.baseUrl.dev + res.data.data;
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    }
  },
  mounted() {
    this.loadParkinglotList();
  }
};
</script>
<style scoped>
</style>
