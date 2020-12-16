<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.fangxun.query.totalCount"
        :pageSize="stores.fangxun.query.pageSize"
        :currentPage="stores.fangxun.query.currentPage"
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
                      v-model="stores.fangxun.query.kw"
                      placeholder="输入人员姓名搜索..."
                      @on-search="handleSearchFangxun()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.fangxun.query.isDeleted"
                        @on-change="handleSearchFangxun"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.fangxun.sources.isDeletedSources"
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
                                  title="新增防汛工作"
                                >新增防汛工作</Button>
                <!-- <Button
                  v-can="'import'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImport"
                  title="导入"
                >导入</Button> -->
                <Button
                  v-can="'export'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportFangXun('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportFangXunyyj('export')"
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
          :data="stores.fangxun.data"
          :columns="stores.fangxun.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <!-- <template slot-scope="{row,index}" slot="state">
            <span>{{renderState(row.state)}}</span>
          </template> -->
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
                <FormItem label="工作专班" prop="workingclass">
                  <Input
                    v-model.trim="formModel.fields.workingclass"
                    placeholder="请输入工作专班"
                    style="width: 400px"
                    :maxlength="10"
                  />
                </FormItem>
              </Col>
            </Row>
            <Row :gutter="16">
              <Col span="12">
                <FormItem label="专班组长" prop="teamleader">
                  <Input
                    v-model.trim="formModel.fields.teamleader"
                    placeholder="请输入专班组长"
                    style="width: 400px"
                    :maxlength="10"
                  />
                </FormItem>
              </Col>
            </Row>
            <Row :gutter="16">
              <Col span="12">
                <FormItem label="机关干部" prop="governmentofficials">
                  <Input
                    v-model.trim="formModel.fields.governmentofficials"
                    placeholder="请输入机关干部"
                    style="width: 400px"
                    :maxlength="10"
                  />
                </FormItem>
              </Col>
            </Row>
            <Row :gutter="16">
              <Col span="12">
                <FormItem label="村、企负责人" prop="personinchargeofvillageenterprise">
                  <Input
                    v-model.trim="formModel.fields.personinchargeofvillageenterprise"
                    placeholder="请输入负责人"
                    style="width: 400px"
                    :maxlength="20"
                  />
                </FormItem>
              </Col>
            </Row>
            <Row :gutter="16">
              <Col span="12">
                <FormItem label="工作要求" prop="jobrequirements">
                  <Input
                    v-model.trim="formModel.fields.jobrequirements"
                    type="textarea"
                    placeholder="请输入工作要求"
                    style="width: 400px"
                  />
                </FormItem>
              </Col>
            </Row>
          </Form>

          <div style="margin-top: 100px">
            <Button icon="md-checkrenyuzhuany-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
            <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
          </div>
        </Drawer>
    <!-- <Drawer
      title="人员转移信息导入"
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
          title="防汛防台应急预案任务信息导入模板下载"
        >人员转移信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportRenyuzhuany"
          :disabled="importdisable"
        >导入</Button>

        <Tabs value="name1">
          <TabPane label="成功" name="name1" v-html="successmsg"></TabPane>
          <TabPane label="重复" name="name2" v-html="repeatmsg"></TabPane>
          <TabPane label="失败" name="name3" v-html="defaultmsg"></TabPane>
        </Tabs>
      </div>
    </Drawer> -->
  </div>
</template>
<script>
  import DzTable from "_c/tables/dz-table.vue";
  import Tables from "_c/tables";
  import {
    getFangXunList,
    createFangXun,
    loadFangXun,
    editFangXun,
    deleteFangXun,
    batchCommand,
    // RenyuzhuanyImport,
    FangXunExport
  } from "@/api/emergency/emergencyresponse/fangxun";
//   import { globalvalidatePhone } from "@/global/validate"
  import config from "@/config";
  export default {
    name: "fangxun",
    components: {
      Tables,
      DzTable
    },
    data() {

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

        commands: {
          export: { name: "export", title: "导出" },
          delete: { name: "delete", title: "删除" },
          recover: { name: "recover", title: "恢复" },
          forbidden: { name: "forbidden", title: "禁用" },
          normal: { name: "normal", title: "启用" }
        },
        formModel: {
          opened: false,
          title: "创建防汛工作",
          mode: "create",
          selection: [],
          fields: {
            workingclass:"",
            teamleader:"",
            governmentofficials: "",
            personinchargeofvillageenterprise:"",
            jobrequirements:"",
          },
          rules: {
            // xzhuanyi: [{type: "string",required: true,message: "请输入转移人员",trigger:'blur'}],
            // xiangyingDj: [{type: "string",required: true,message: "请输入响应等级",trigger:'blur'}],
            // zhaunyiQingk: [{type: "string",required: true,message: "请选择转移情况",trigger:'blur'}],
            // fuzeren: [{type: "string",required: true,message: "请输入负责人",trigger:'blur'}],
            // fuzerenPhone: [{type: "string",required: true,message: "请输入联系电话",trigger:'blur'},
            //   { validator: globalvalidatePhone, trigger: "blur" }
            // ]
          }
        },
        stores: {
          fangxun: {
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
              zhaunyiQingkList:[
                { value: '未转移', text: "未转移" },
                { value: '已转移', text: "已转移" },
              ],
              isDeletedSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "正常" },
                { value: 1, text: "已删" }
              ],
              stateSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "已疏散" },
                { value: 1, text: "未疏散" }
              ],
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "工作专班", key: "workingclass", sortable: true },
              { title: "专班组长", key: "teamleader", sortable: true },
              { title: "机关干部（责任人信息员）", key: "governmentofficials" , sortable: true},
              { title: "村、企负责人员", key: "personinchargeofvillageenterprise", sortable: true },
              { title: "工作要求", key: "jobrequirements", sortable: true },
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
          workingclass:"",
          teamleader:"",
          governmentofficials: "",
          personinchargeofvillageenterprise:"",
          jobrequirements:"",
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增防汛工作";
        }
        if (this.formModel.mode === "edit") {
          return "编辑防汛工作";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.renyuZhuanyUuid);
      }
    },
    methods: {
    //   renderState(state){
    //     let _state = "未知";
    //     switch (state) {
    //       case 0:
    //         _state="已疏散"
    //         break;
    //       case 1:
    //         _state="未疏散"
    //         break;
    //     }
    //     return _state;
    //   },
      loadFangXunList() {
        getFangXunList(this.stores.fangxun.query).then(res => {
          this.stores.fangxun.data = res.data.data;
          this.stores.fangxun.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchFangxun() {
        this.stores.fangxun.query.currentPage = 1;
        this.loadFangXunList();
      },
      handleRefresh() {
        this.stores.fangxun.query.currentPage = 1;
        this.loadFangXunList();
      },
      //创建，修改
      handleSubmitPlan() {
        let valid = this.validateForm();
        if (valid) {
          if (this.formModel.mode === "create") {
            this.docreateFangXun();
          }
          if (this.formModel.mode === "edit") {
            this.doEditPlan();
          }
        }
      },
      docreateFangXun() {
          
        createFangXun(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadFangXunList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      doEditPlan() {
        editFangXun(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadFangXunList();
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
      },
      doBatchCommand(command) {
        batchCommand({
          command: command,
          ids: this.selectedRowsId.join(",")
        }).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadFangXunList();
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
        this.doDelete(row.renyuZhuanyUuid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteFangXun(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadFangXunList();
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
        this.doLoadFangXun(row.guid);
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
      doLoadFangXun(guid) {
        loadFangXun({ guid: guid }).then(res => {
          this.formModel.fields = res.data.data;
        });
      },
      handlePageChanged(page) {
        this.stores.fangxun.query.currentPage = page;
        this.loadFangXunList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.fangxun.query.pageSize = pageSize;
        this.loadFangXunList();
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
          this.url + "UploadFiles/ImportEmergencyModel/人员转移信息导入模板.xlsx";
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
      async handleImportRenyuzhuany() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await RenyuzhuanyImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
              this.successmsg = res.data.data.successmsg;
              this.repeatmsg = res.data.data.repeatmsg;
              this.defaultmsg = res.data.data.defaultmsg;
              this.loadFangXunList();
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
      handleExportFangXun(command) {
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
            this.doFangXunExport(command);
          }
        });
      },
      //一键导出
      handleExportFangXunyyj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doFangXunExport(command);
          }
        });
      },
      doFangXunExport(command) {
        FangXunExport(this.selectedRowsId.join(",")).then(res => {
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
      this.loadFangXunList();
    }
  };
</script>
<style scoped>
</style>
