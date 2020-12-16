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
                      placeholder="输入队名/队长搜索..."
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
                  title="新增小队"
                >新增小队</Button>
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
                  @click="handleExportRescueteam('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportRescueteamyj('export')"
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
            <FormItem label="队名" prop="teamName">
              <Input
                v-model.trim="formModel.fields.teamName"
                placeholder="请输入队名"
                style="width: 400px"
                
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="人数" prop="teamRenshu">
              <Input
                v-model.trim="formModel.fields.teamRenshu"
                @keyup.native="formModel.fields.teamRenshu=formModel.fields.teamRenshu.replace(/[^\d]/g,'')"
                placeholder="请输入人数"
                style="width: 400px"
                :maxlength="3"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="队长" prop="teamCaptain">
              <Input
                v-model.trim="formModel.fields.teamCaptain"
                placeholder="请输入队长姓名"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="联系电话" prop="teamPhone">
              <Input
                v-model.trim="formModel.fields.teamPhone"
                placeholder="请输入联系电话"
                style="width: 400px"
                :maxlength="11"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkrescueteam-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="救援小队信息导入"
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
          title="救援小队信息导入模板下载"
        >救援小队信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportRescueteam"
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
    RescueteamImport,
    RescueteamExport
  } from "@/api/emergency/rescueteam";
  import { globalvalidatePhone } from "@/global/validate"
  import config from "@/config";
  export default {
    name: "rescueteam",
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

        showdetails: false,
        details: "",
        commands: {
          export: { name: "export", title: "导出" },
          delete: { name: "delete", title: "删除" },
          recover: { name: "recover", title: "恢复" },
          forbidden: { name: "forbidden", title: "禁用" },
          normal: { name: "normal", title: "启用" }
        },
        formModel: {
          opened: false,
          title: "创建小队",
          mode: "create",
          selection: [],
          fields: {
            teamName: "",
            teamRenshu:"",
            teamCaptain:"",
            teamPhone:""
          },
          rules: {
            teamName: [{type: "string",required: true,message: "请输入队名",trigger:'blur'}],
            teamCaptain: [{type: "string",required: true,message: "请输入队长姓名",trigger:'blur'}],
            teamPhone: [{type: "string",required: true,message: "请输入联系电话",trigger:'blur'},
              { validator: globalvalidatePhone, trigger: "blur" }],
            teamRenshu: [{ required: true, message: "请输入人数" },
              { validator: checkNum, trigger: "blur" }],
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
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "队名", key: "teamName", sortable: true },
              { title: "人数", key: "teamRenshu" , sortable: true},
              { title: "队长", key: "teamCaptain", sortable: true },
              { title: "联系电话", key: "teamPhone" , sortable: true},
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
          teamName: "",
          teamRenshu:"",
          teamCaptain:"",
          teamPhone:""
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增小队";
        }
        if (this.formModel.mode === "edit") {
          return "编辑小队";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.rescueTeamUuid);
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
      doEditPlan() {
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
        this.doDelete(row.rescueTeamUuid);
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
        this.doLoadRescueteam(row.rescueTeamUuid);
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
          this.url + "UploadFiles/ImportEmergencyModel/救援小队信息导入模板.xlsx";
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
      async handleImportRescueteam() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await RescueteamImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
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
      handleExportRescueteam(command) {
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
            this.doRescueteamExport(command);
          }
        });
      },
      //一键导出
      handleExportRescueteamyj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doRescueteamExport(command);
          }
        });
      },
      doRescueteamExport(command) {
        RescueteamExport(this.selectedRowsId.join(",")).then(res => {
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
      this.loadRescueteamList();
    }
  };
</script>
<style scoped>
</style>
