<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.xunchaduty.query.totalCount"
        :pageSize="stores.xunchaduty.query.pageSize"
        :currentPage="stores.xunchaduty.query.currentPage"
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
                      v-model="stores.xunchaduty.query.kw"
                      placeholder="输入巡查人/巡查地点搜索..."
                      @on-search="handleSearchXunchaduty()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.xunchaduty.query.isDeleted"
                        @on-change="handleSearchXunchaduty"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.xunchaduty.sources.isDeletedSources"
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
                  title="新增巡查安排"
                >新增巡查安排</Button>

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
                  @click="handleExportXunchaduty('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportXunchadutyyj('export')"
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
          :data="stores.xunchaduty.data"
          :columns="stores.xunchaduty.columns"
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
      width="500"
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
            <FormItem label="巡查人" prop="xunchaInfoRen">
              <Input
                v-model.trim="formModel.fields.xunchaInfoRen"
                placeholder="请输入巡查人"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="巡查地点" prop="xunchaAddress">
              <Input
                v-model.trim="formModel.fields.xunchaAddress"
                placeholder="请输入巡查地点"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="巡查时间" prop="xunchaTime">
              <DatePicker
                v-model="formModel.fields.xunchaTime"
                @on-change="formModel.fields.xunchaTime=$event"
                format="yyyy-MM-dd"
                type="date"
                placeholder="巡查时间"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="联系电话" prop="fuzerPhone">
              <Input
                v-model.trim="formModel.fields.fuzerPhone"
                placeholder="请输入联系电话"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>

      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkxunchaduty-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="巡查安排信息导入"
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
          title="巡查安排信息导入模板下载"
        >巡查安排信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportXunchaduty"
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
    getXunchadutyList,
    createXunchaduty,
    loadXunchaduty,
    editXunchaduty,
    deleteXunchaduty,
    batchCommand,
    XunchadutyImport,
    XunchadutyExport
  } from "@/api/emergency/emergencyresponse/xunchaduty";
  import { globalvalidatePhone } from "@/global/validate"
  import config from "@/config";
  export default {
    name: "xunchaduty",
    components: {
      Tables,
      DzTable
    },
    data() {
      let checkNum = (rule, value, callback) => {
        if (value === "") {
          callback(new Error("请输入"));
        } else if (value <= 0 ) {
          callback(new Error("请输入大于0的数字"));
        } else {
          callback();
        }
      };
      return {
        //导入
        url: config.baseUrl.dev,
        importdisable: false,
        isexitexcel:false,
        successmsg: "",
        repeatmsg: "",
        defaultmsg: "",
        formimport: {
          opened: false
        },
        rescueTeamList:[],
        commands: {
          export: { name: "export", title: "导出" },
          delete: { name: "delete", title: "删除" },
          recover: { name: "recover", title: "恢复" },
          forbidden: { name: "forbidden", title: "禁用" },
          normal: { name: "normal", title: "启用" }
        },
        formModel: {
          opened: false,
          title: "创建巡查安排",
          mode: "create",
          selection: [],
          fields: {
            xunchaInfoRen:"",
            xunchaAddress: "",
            xunchaTime:"",
            fuzerPhone:"",
          },
          rules: {
            xunchaInfoRen: [{type: "string",required: true,message: "请输入姓名",trigger:'blur'}],
            xunchaAddress: [{type: "string",required: true,message: "请输入地点",trigger:'blur'}],
            xunchaTime: [{type: "string",required: true,message: "请选择时间",trigger:'blur'}],
            fuzerPhone: [{type: "string",required: true,message: "请输入联系电话",trigger:'blur'},
              { validator: globalvalidatePhone, trigger: "blur" }
            ]
          }
        },
        stores: {
          xunchaduty: {
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
              sexList: [
                {
                  value: "男",
                  label: "男"
                },
                {
                  value: "女",
                  label: "女"
                }
              ],
              isDeletedSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "正常" },
                { value: 1, text: "已删" }
              ],
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "巡查人", key: "xunchaInfoRen", sortable: true },
              { title: "巡查地点", key: "xunchaAddress" , sortable: true},
              { title: "巡查时间", key: "xunchaTime", sortable: true },
              { title: "联系电话", key: "fuzerPhone" , sortable: true},
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
          xunchaInfoRen:"",
          xunchaAddress: "",
          xunchaTime:"",
          fuzerPhone:"",
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增巡查安排";
        }
        if (this.formModel.mode === "edit") {
          return "编辑巡查安排";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.xunchaDutyUuid);
      }
    },
    methods: {
      loadXunchadutyList() {
        getXunchadutyList(this.stores.xunchaduty.query).then(res => {
          this.stores.xunchaduty.data = res.data.data;
          this.stores.xunchaduty.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchXunchaduty() {
        this.stores.xunchaduty.query.currentPage = 1;
        this.loadXunchadutyList();
      },
      handleRefresh() {
        this.stores.xunchaduty.query.currentPage = 1;
        this.loadXunchadutyList();
      },
      //创建，修改
      handleSubmitPlan() {
        let valid = this.validateForm();
        if (valid) {
          if (this.formModel.mode === "create") {
            this.docreateXunchaduty();
          }
          if (this.formModel.mode === "edit") {
            this.doEditPlan();
          }
        }
      },
      docreateXunchaduty() {
        createXunchaduty(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadXunchadutyList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      doEditPlan() {
        editXunchaduty(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadXunchadutyList();
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
            this.loadXunchadutyList();
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
        this.doDelete(row.xunchaDutyUuid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteXunchaduty(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadXunchadutyList();
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
        this.doLoadXunchaduty(row.xunchaDutyUuid);
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
      doLoadXunchaduty(guid) {
        loadXunchaduty({ guid: guid }).then(res => {
          this.formModel.fields = res.data.data;
        });
      },
      handlePageChanged(page) {
        this.stores.xunchaduty.query.currentPage = page;
        this.loadXunchadutyList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.xunchaduty.query.pageSize = pageSize;
        this.loadXunchadutyList();
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
          this.url + "UploadFiles/ImportEmergencyModel/巡查安排信息导入模板.xlsx";
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
      async handleImportXunchaduty() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await XunchadutyImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
              this.successmsg = res.data.data.successmsg;
              this.repeatmsg = res.data.data.repeatmsg;
              this.defaultmsg = res.data.data.defaultmsg;
              this.loadXunchadutyList();
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
      handleExportXunchaduty(command) {
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
            this.doXunchadutyExport(command);
          }
        });
      },
      //一键导出
      handleExportXunchadutyyj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doXunchadutyExport(command);
          }
        });
      },
      doXunchadutyExport(command) {
        XunchadutyExport(this.selectedRowsId.join(",")).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.formModel.selection = [];

            window.location.href = config.baseUrl.dev + res.data.data;
          } else {
            this.$Message.warning(res.data.message);
          }
          this.$Modal.remove();
        });
      },
    },
    mounted() {
      this.loadXunchadutyList();
    }
  };
</script>
<style scoped>
</style>
