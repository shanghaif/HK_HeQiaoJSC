<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.responseinit.query.totalCount"
        :pageSize="stores.responseinit.query.pageSize"
        :currentPage="stores.responseinit.query.currentPage"
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
                      v-model="stores.responseinit.query.kw"
                      placeholder="输入响应等级搜索..."
                      @on-search="handleSearchResponseinit()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.responseinit.query.isDeleted"
                        @on-change="handleSearchResponseinit"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.responseinit.sources.isDeletedSources"
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
                  title="新增响应发起"
                >新增响应发起</Button>
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
                  @click="handleExportResponseinit('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportResponseinityj('export')"
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
          :data="stores.responseinit.data"
          :columns="stores.responseinit.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{row,index}" slot="releaseState">
            <span>{{renderReleaseState(row.releaseState)}}</span>
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
            <Poptip confirm :transfer="true" title="确定要发布吗?" @on-ok="handleSend(row)" v-if="row.releaseState==0">
              <Tooltip placement="top" content="发布" :delay="1000" :transfer="true">
                <Button v-can="'send'" type="warning" size="small" shape="circle" icon="ios-redo"></Button>
              </Tooltip>
            </Poptip>
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
            <FormItem label="村庄" >
              <Select
                filterable
                multiple
                v-model="formModel.villageschose"
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
            <FormItem label="响应等级" prop="level">
              <Input
                v-model.trim="formModel.fields.level"
                placeholder="请输入响应等级"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="响应情况" prop="situation">
              <Input
                v-model.trim="formModel.fields.situation"
                placeholder="请输入响应情况"
                style="width: 400px"
                :maxlength="200"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkresponseinit-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="响应发起信息导入"
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
          title="响应发起信息导入模板下载"
        >响应发起信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportResponseinit"
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
    getResponseinitList,
    createResponseinit,
    loadResponseinit,
    editResponseinit,
    deleteResponseinit,
    batchCommand,
    SendInit,
    ResponseinitImport,
    ResponseinitExport
  } from "@/api/emergency/emergencyresponse/responseinit";
  import {
    getVillageList
  } from "@/api/emergency/villagemember";
  import { globalvalidatePhone } from "@/global/validate"
  import config from "@/config";
  export default {
    name: "responseinit",
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
          title: "创建响应发起",
          mode: "create",
          selection: [],
          villageschose: [],
          villageList:[],
          fields: {
            villages:"",
            level: "",
            situation:"",
          },
          rules: {
            villages: [{type: "string",required: true,message: "请选择村庄",trigger:'blur'}],
            level: [{type: "string",required: true,message: "请输入响应等级",trigger:'blur'}],
            situation: [{type: "string",required: true,message: "请输入情况",trigger:'blur'}],
          }
        },
        stores: {
          responseinit: {
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
              { title: "响应村庄", key: "villageName", sortable: true },
              { title: "响应等级", key: "level", sortable: true },
              { title: "响应情况", key: "situation" , sortable: true},
              { title: "响应发起状态", key: "releaseState", sortable: true ,slot: "releaseState"},
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
          villages:"",
          level: "",
          situation:"",
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增响应发起";
        }
        if (this.formModel.mode === "edit") {
          return "编辑响应发起";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.responseInitUuid);
      }
    },
    methods: {
      renderReleaseState(state){
        let _state = "未知";
        switch (state) {
          case 0:
            _state="未发起";
            break;
            case 1:
              _state="已发起"
                break;
        }
        return _state;
      },
      loadResponseinitList() {
        getResponseinitList(this.stores.responseinit.query).then(res => {
          this.stores.responseinit.data = res.data.data;
          this.stores.responseinit.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchResponseinit() {
        this.stores.responseinit.query.currentPage = 1;
        this.loadResponseinitList();
      },
      handleRefresh() {
        this.stores.responseinit.query.currentPage = 1;
        this.loadResponseinitList();
      },
      //创建，修改
      handleSubmitPlan() {
        let valid0 = this.dealvillages();
        if(valid0)
        {
          let valid = this.validateForm();
          if (valid) {
            if (this.formModel.mode === "create") {
              this.docreateResponseinit();
            }
            if (this.formModel.mode === "edit") {
              this.doEditPlan();
            }
          }
        }

      },
      dealvillages(){
        this.formModel.fields.villages=this.formModel.villageschose.join(',');
        if(this.formModel.villageschose.length<=0||this.formModel.fields.villages=="")
        {
          this.$Message.warning("请选择村庄");
          return false;
        }
        else
          return true;
      },
      docreateResponseinit() {
        createResponseinit(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadResponseinitList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      doEditPlan() {
        editResponseinit(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadResponseinitList();
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
            this.loadResponseinitList();
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
      //发送响应
      handleSend(row)
      {
        this.$Spin.show({
          render: (h) => {
            return h('div', [
              h('Icon', {
                'class': 'demo-spin-icon-load',
                props: {
                  type: 'ios-loading',
                  size: 18
                }
              }),
              h('div', '发送中请勿关闭')
            ])
          }
        });

        SendInit({guid:row.responseInitUuid}).then(res=>{
          if(res.data.code==200)
          {
            this.$Message.success(res.data.message);
            this.loadResponseinitList();
          }
          else
          {
            this.$Message.warning(res.data.message);
          }
          this.$Spin.hide();
        })
      },
      //单条删除
      handleDelete(row) {
        this.doDelete(row.responseInitUuid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteResponseinit(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadResponseinitList();
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
        this.doLoadResponseinit(row.responseInitUuid);
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
        this.formModel.villageschose=[];
        //this.$refs["formPlan"].resetFields();
      },
      doLoadResponseinit(guid) {
        loadResponseinit({ guid: guid }).then(res => {
          this.formModel.fields = res.data.data;
          this.formModel.villageschose = this.formModel.fields.villages.split(',');
        });
      },
      handlePageChanged(page) {
        this.stores.responseinit.query.currentPage = page;
        this.loadResponseinitList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.responseinit.query.pageSize = pageSize;
        this.loadResponseinitList();
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
          this.url + "UploadFiles/ImportEmergencyModel/响应发起信息导入模板.xlsx";
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
      async handleImportResponseinit() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await ResponseinitImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
              this.successmsg = res.data.data.successmsg;
              this.repeatmsg = res.data.data.repeatmsg;
              this.defaultmsg = res.data.data.defaultmsg;
              this.loadResponseinitList();
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
      handleExportResponseinit(command) {
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
            this.doResponseinitExport(command);
          }
        });
      },
      //一键导出
      handleExportResponseinityj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doResponseinitExport(command);
          }
        });
      },
      doResponseinitExport(command) {
        ResponseinitExport(this.selectedRowsId.join(",")).then(res => {
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
      this.loadResponseinitList();
      this.dogetVillageList();
    }
  };
</script>
<style scoped>

  .demo-spin-icon-load{
    animation: ani-demo-spin 1s linear infinite;
  }
  @keyframes ani-demo-spin {
    from { transform: rotate(0deg);}
    50%  { transform: rotate(180deg);}
    to   { transform: rotate(360deg);}
  }
  .demo-spin-col{
    height: 100px;
    position: relative;
    border: 1px solid #eee;
  }
</style>
