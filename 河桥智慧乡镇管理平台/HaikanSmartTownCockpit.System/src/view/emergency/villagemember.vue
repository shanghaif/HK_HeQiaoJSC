<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.villagemember.query.totalCount"
        :pageSize="stores.villagemember.query.pageSize"
        :currentPage="stores.villagemember.query.currentPage"
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
                      v-model="stores.villagemember.query.kw"
                      placeholder="输入村庄/姓名搜索..."
                      @on-search="handleSearchVillagemember()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.villagemember.query.isDeleted"
                        @on-change="handleSearchVillagemember"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.villagemember.sources.isDeletedSources"
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
                  title="新增干部"
                >新增干部</Button>
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
                  @click="handleExportVillagemember('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportVillagememberyj('export')"
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
          :data="stores.villagemember.data"
          :columns="stores.villagemember.columns"
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
            <FormItem label="村庄" prop="villageUuid">
              <Select
                filterable
                v-model="formModel.fields.villageUuid"
                style="width:400px"
              >
                <Option
                  v-for="item in villageList"
                  :value="item.villageUuid"
                  :key="item.villageUuid"
                >{{ item.villageName }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="姓名" prop="memberName">
              <Input
                v-model.trim="formModel.fields.memberName"
                placeholder="请输入姓名"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="性别" prop="memberSex">
              <Select
                filterable
                v-model="formModel.fields.memberSex"
                style="width:400px"
                placeholder="性别"
              >
                <Option
                  v-for="item in stores.villagemember.sources.sexList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="联系电话" prop="memberPhone">
              <Input
                v-model.trim="formModel.fields.memberPhone"
                placeholder="请输入联系电话"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkvillagemember-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="村庄成员信息导入"
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
          title="村庄信息导入模板下载"
        >村庄信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportVillagemember"
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
    getVillagememberList,
    createVillagemember,
    loadVillagemember,
    editVillagemember,
    deleteVillagemember,
    batchCommand,
    getVillageList,
    VillagememberImport,
    VillagememberExport
  } from "@/api/emergency/villagemember";
  import { globalvalidatePhone } from "@/global/validate"
  import config from "@/config";
  export default {
    name: "villagemember",
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

        villageList:[],
        commands: {
          export: { name: "export", title: "导出" },
          delete: { name: "delete", title: "删除" },
          recover: { name: "recover", title: "恢复" },
          forbidden: { name: "forbidden", title: "禁用" },
          normal: { name: "normal", title: "启用" }
        },
        formModel: {
          opened: false,
          title: "创建干部",
          mode: "create",
          selection: [],
          fields: {
            villageUuid:"",
            memberName: "",
            memberSex:"",
            memberPhone:"",
          },
          rules: {
            villageUuid: [{type: "string",required: true,message: "请选择村庄",trigger:'blur'}],
            memberName: [{type: "string",required: true,message: "请输入姓名",trigger:'blur'}],
            memberSex: [{type: "string",required: true,message: "请选择性别",trigger:'blur'}],
            memberPhone: [{type: "string",required: true,message: "请输入联系电话",trigger:'blur'},
              { validator: globalvalidatePhone, trigger: "blur" }
            ]
          }
        },
        stores: {
          villagemember: {
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
              { title: "村庄", key: "villageName", sortable: true },
              { title: "姓名", key: "memberName" , sortable: true},
              { title: "性别", key: "memberSex", sortable: true },
              { title: "联系电话", key: "memberPhone" , sortable: true},         
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
          villageUuid:"",
          memberName: "",
          memberSex:"",
          memberPhone:"",
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增干部";
        }
        if (this.formModel.mode === "edit") {
          return "编辑干部";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.villageMemberUuid);
      }
    },
    methods: {
      loadVillagememberList() {
        getVillagememberList(this.stores.villagemember.query).then(res => {
          this.stores.villagemember.data = res.data.data;
          this.stores.villagemember.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchVillagemember() {
        this.stores.villagemember.query.currentPage = 1;
        this.loadVillagememberList();
      },
      handleRefresh() {
        this.stores.villagemember.query.currentPage = 1;
        this.loadVillagememberList();
      },
      //创建，修改
      handleSubmitPlan() {
        let valid = this.validateForm();
        if (valid) {
          if (this.formModel.mode === "create") {
            this.docreateVillagemember();
          }
          if (this.formModel.mode === "edit") {
            this.doEditPlan();
          }
        }
      },
      docreateVillagemember() {
        createVillagemember(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadVillagememberList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      doEditPlan() {
        editVillagemember(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadVillagememberList();
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
            this.loadVillagememberList();
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
        this.doDelete(row.villageMemberUuid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteVillagemember(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadVillagememberList();
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
        this.doLoadVillagemember(row.villageMemberUuid);
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
      doLoadVillagemember(guid) {
        loadVillagemember({ guid: guid }).then(res => {
          this.formModel.fields = res.data.data;
        });
      },
      handlePageChanged(page) {
        this.stores.villagemember.query.currentPage = page;
        this.loadVillagememberList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.villagemember.query.pageSize = pageSize;
        this.loadVillagememberList();
      },
      dogetVillageList(){
        getVillageList().then(res=>{
          if(res.data.code==200)
          {
            this.villageList=res.data.data;
          }
        })
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
          this.url + "UploadFiles/ImportEmergencyModel/村庄成员信息导入模板.xlsx";
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
      async handleImportVillagemember() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await VillagememberImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
              this.successmsg = res.data.data.successmsg;
              this.repeatmsg = res.data.data.repeatmsg;
              this.defaultmsg = res.data.data.defaultmsg;
              this.loadVillagememberList();
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
      handleExportVillagemember(command) {
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
            this.doVillagememberExport(command);
          }
        });
      },
      //一键导出
      handleExportVillagememberyj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doVillagememberExport(command);
          }
        });
      },
      doVillagememberExport(command) {
        VillagememberExport(this.selectedRowsId.join(",")).then(res => {
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
      this.loadVillagememberList();
      this.dogetVillageList();
    }
  };
</script>
<style scoped>
</style>
