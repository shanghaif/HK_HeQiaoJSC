<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.rescueteam.query.totalCount"
        :pageSize="stores.rescueteam.query.pageSize"
        :currentPage="stores.rescueteam.query.currentPage"
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
                      v-model="stores.rescueteam.query.kw"
                      placeholder="输入名称搜索..."
                      @on-search="handleSearchRescueteam()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.rescueteam.query.isDeleted"
                        @on-change="handleSearchRescueteam"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.rescueteam.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
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
          :data="stores.rescueteam.data"
          :columns="stores.rescueteam.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{row,index}" slot="isEnable">
            <Tag :color="renderIsEnable(row.isEnable).color">{{renderIsEnable(row.isEnable).text}}</Tag>
          </template>
          <template slot-scope="{ row, index }" slot="detail">
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="primary"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleShow(row)"
              ></Button>
            </Tooltip>
          </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>
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
      width="450"
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
          <FormItem label="行政村" prop="town">
            <Select
              filterable
              v-model="formModel.fields.town"
              style="width:400px;"
              placeholder="请选择行政村"
            >
              <Option
                v-for="item in stores.rescueteam.sources.townList"
                :value="item.townName"
                :key="item.townName"
              >{{ item.townName }}</Option>
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="姓名" prop="household">
              <Input
                v-model="formModel.fields.household"
                placeholder="请输入姓名"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="联系方式" prop="phone">
              <Input
                v-model="formModel.fields.phone"
                placeholder="请输入联系方式"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="身份证号" prop="identityCard">
              <Input
                v-model="formModel.fields.identityCard"
                placeholder="请输入身份证号"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="规划许可证号" prop="projectCred">
              <Input
                v-model="formModel.fields.projectCred"
                placeholder="请输入规划许可证号"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="用地呈报表编号" prop="landNum">
              <Input
                v-model="formModel.fields.landNum"
                placeholder="请输入用地呈报表编号"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="建筑面积" prop="buildArea">
              <Input
                v-model="formModel.fields.buildArea"
                placeholder="请输入建筑面积"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="占地面积" prop="occupyArea">
              <Input
                v-model="formModel.fields.occupyArea"
                placeholder="请输入占地面积"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <FormItem label="方式" prop="way">
            <Select
              filterable
              v-model="formModel.fields.way"
              style="width:400px;"
              placeholder="请选择方式"
            >
              <Option
                v-for="item in stores.rescueteam.sources.wayList"
                :value="item.value"
                :key="item.value"
              >{{ item.label }}</Option>
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="工匠证书" prop="artisanCred">
              <Input
                v-model="formModel.fields.artisanCred"
                placeholder="请输入工匠证书"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row> 
        <Row :gutter="16">
          <FormItem label="审批时间" prop="approveTime">
            <DatePicker
              v-model="formModel.fields.approveTime"
              @on-change="formModel.fields.approveTime=$event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="请选择审批时间"
              style="width: 400px;"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="位置" prop="houseAddress">
              <Input
                v-model="formModel.fields.houseAddress"
                placeholder="请输入位置"
                style="width: 400px"
                :maxlength="50"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="经度" prop="lon">
              <Input
                v-model="formModel.fields.lon"
                placeholder="请输入经度"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="纬度" prop="lat">
              <Input
                v-model="formModel.fields.lat"
                placeholder="请输入纬度"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="监控编号" prop="monitorHouseId">
              <Input
                v-model="formModel.fields.monitorHouseId"
                placeholder="请输入监控编号"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="网络员" prop="administratorUuid">
              <Input
                v-model="formModel.fields.administratorUuid"
                placeholder="请输入网络员"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div>
        <Button icon="md-checkrescueteam-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer
      title="农民建房信息导入"
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
          title="农民建房信息导入模板下载"
        >农民建房信息导入模板下载</Button>
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
import Tables from "_c/tables";
import {
  getRescueteamList,
  createRescueteam,
  loadRescueteam,
  editRescueteam,
  deleteRescueteam,
  batchCommand,
  getTownList,
  HqCommunaImport, //导入
  HqCommunaExport //导出
} from "@/api/Environmental/farmHouse";
import { globalvalidatePhone } from "@/global/validate";
import config from "@/config";
export default {
  name: "rescueteam",
  components: {
    Tables,
    DzTable
  },
  data() {
    // let validateLon = (rule, value, callback) => {
    //   if (value !== "" && value !== null) {
    //     let reg = /^[\-\+]?(0?\d{1,2}\.\d{1,6}|1[0-7]?\d{1}\.\d{1,6}|180\.0{1,6})$/;
    //     if (!reg.test(value)) {
    //       callback(new Error("格式错误！范围在-180.0~180.0(小数点至多6位)"));
    //     }
    //     callback();
    //   } else {
    //     callback(new Error("经度不能为空"));
    //   }
    //   callback();
    // };
    // let validateLat = (rule, value, callback) => {
    //   if (value !== "" && value !== null) {
    //     let reg = /^[\-\+]?([0-8]?\d{1}\.\d{1,6}|90\.0{1,6})$/;
    //     if (!reg.test(value)) {
    //       callback(new Error("格式错误！范围在-90.0~90.0(小数点至多6位)"));
    //     }
    //     callback();
    //   } else {
    //     callback(new Error("纬度不能为空"));
    //   }
    //   callback();
    // };
    let validateIdentity = (rule, value, callback) => {
      if (value !== "" && value !== null) {
        let reg = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;
        if (!reg.test(value)) {
          callback(new Error("格式错误,请输入正确的身份证号信息"));
        }
        callback();
      } else {
        callback(new Error("身份证号不能为空"));
      }
      callback();
    };
    let validateName = (rule, value, callback) => {
      if (value !== "" && value !== null) {
        let reg = /^[^\s]+$/;
        if (!reg.test(value)) {
          callback(new Error("格式错误！请勿输入非法字符)"));
        }
        callback();
      } else {
        callback(new Error("姓名不能为空"));
      }
      callback();
    };
    return {
      showdetails: false,
      details: "",

      //导入
      url: config.baseUrl.dev,
      importdisable: false,
      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
      },

      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" },
        Import: { name: "Import", title: "导入" },
        export: { name: "export", title: "导出" }
      },
      formModel: {
        opened: false,
        title: "添加农民建房信息",
        mode: "create",
        selection: [],
        fields: {
          // buildHouseUuid:"",
          household: "",
          // houseAddress: "",
          lon: "",
          lat: "",
          identityCard: "",
          phone: "",
          monitorHouseId: "",
          administratorUuid: "",
          town:"",
          projectCred:"",
          landNum:"",
          buildArea:"",
          occupyArea:"",
          way:"",
          artisanCred:"",
          approveTime:""
        },
        rules: {
          household: [
            {
              type: "string",
              required: true,
              message: "请输入姓名",
              validator: validateName
            }
          ],
          // houseAddress: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入位置",
          //     trigger: "blur"
          //   }
          // ],
          identityCard: [
            {
              required: true,
              message: "请输入身份证号",
              validator: validateIdentity
            }
          ],
          // lon: [{ type: "string", required: true, validator: validateLon }],
          // lat: [{ type: "string", required: true, validator: validateLat }],
          phone: [
            { required: true, message: "请输入联系方式", trigger: "blur" }
          ],
          // monitorHouseId: [
          //   { required: true, message: "请输入监控编号", trigger: "blur" }
          // ],
          // administratorUuid: [
          //   { required: true, message: "请输入网络员", trigger: "blur" }
          // ]
        }
      },
      stores: {
        rescueteam: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            isDeleted: 0,
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
            townList:[],
            wayList: [
              { value: "拆建", text: "拆建" },
              { value: "拆扩建", text: "拆扩建" },
              { value: "全部", text: "新建" }
            ],
          },
          columns: [
            { type: "selection", width: 50, key: "buildHouseUuid" },
            { title: "行政村", key: "town", sortable: true },
            { title: "户主", key: "household", sortable: true },
            // { title: "经度", key: "lon", sortable: true },
            // { title: "纬度", key: "lat", sortable: true },
            { title: "联系方式", key: "phone", sortable: true },
            { title: "身份证号", key: "identityCard", sortable: true },
            { title: "建筑面积", key: "buildArea", sortable: true },
            { title: "占地面积", key: "occupyArea", sortable: true },
            { title: "方式", key: "way", sortable: true },
            // { title: "监控编号", key: "monitorHouseId", sortable: true },
            // { title: "网络员", key: "administratorUuid", sortable: true },
            // { title: "注册时间", key: "addTime", sortable: true },
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
          // buildHouseUuid:"",
        household: "",
          // houseAddress: "",
          lon: "",
          lat: "",
          identityCard: "",
          phone: "",
          monitorHouseId: "",
          administratorUuid: "",
          town:"",
          projectCred:"",
          landNum:"",
          buildArea:"",
          occupyArea:"",
          way:"",
          artisanCred:"",
          approveTime:""
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增建房信息";
      }
      if (this.formModel.mode === "edit") {
        return "编辑建房信息";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.buildHouseUuid);
    }
  },
  methods: {
    loadRescueteamList() {
      getRescueteamList(this.stores.rescueteam.query).then(res => {
        this.stores.rescueteam.data = res.data.data;
        this.stores.rescueteam.query.totalCount = res.data.totalCount;
        //console.log(this.stores.educaplan.data);
      });
    },
    handleSearchRescueteam() {
      this.stores.rescueteam.query.currentPage = 1;
      this.loadRescueteamList();
    },
    handleRefresh() {
      this.stores.rescueteam.query.currentPage = 1;
      this.loadRescueteamList();
    },
    //创建，修改
    handleSubmitPlan() {
      let valid = this.validateForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateRescueteam();
        }
        if (this.formModel.mode === "edit") {
          this.doEditPlan();
        }
      }
    },
    docreateRescueteam() {

      createRescueteam(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadRescueteamList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    dogetTownList(){
      getTownList().then(res=>{
        this.stores.rescueteam.sources.townList=res.data.data;
      })
    },
    doEditPlan() {
      if (
        this.formModel.fields.approveTime != "" &&
        this.formModel.fields.approveTime != null
      ) {
        this.formModel.fields.approveTime = new Date(
          Date.parse(new Date(this.formModel.fields.approveTime)) + 8 * 3600 * 1000
        );
      }
      editRescueteam(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadRescueteamList();
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
          this.loadRescueteamList();
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
      this.doDelete(row.buildHouseUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteRescueteam(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRescueteamList();
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
      this.doLoadRescueteam(row.buildHouseUuid);
      this.dogetTownList();
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
        this.url +
        "UploadFiles/ImportEnvironmentalModel/农民建房导入模板.xlsx";
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
        await HqCommunaImport(this.exceldata).then(res => {
          if (res.data.code == 200) {
            this.time = res.data.data.time;
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadRescueteamList();
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
      HqCommunaExport(this.selectedRowsId.join(",")).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.selection = [];
          console.log(res);
          window.location.href = config.baseUrl.dev + res.data.data;
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
    },

    handleShowCreateWindow() {
      this.handleSwitchFormModeToCreate();
      this.dogetTownList();
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
    handleSwitchFormModeToShow() {
      this.showdetails = true;
    },
    handleResetFormRole() {
      this.formModel.fields = JSON.parse(JSON.stringify(this.initdatacopy));
      //this.$refs["formPlan"].resetFields();
    },
    doLoadRescueteam(guid) {
      loadRescueteam({ guid: guid }).then(res => {
        this.formModel.fields = res.data.data;
      });
    },
    handlePageChanged(page) {
      this.stores.rescueteam.query.currentPage = page;
      this.loadRescueteamList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.rescueteam.query.pageSize = pageSize;
      this.loadRescueteamList();
    }
  },
  mounted() {
    this.loadRescueteamList();
  }
};
</script>
<style scoped>
</style>

