<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.evacuate.query.totalCount"
        :pageSize="stores.evacuate.query.pageSize"
        :currentPage="stores.evacuate.query.currentPage"
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
                      v-model="stores.evacuate.query.kw"
                      placeholder="输入景区名称搜索..."
                      @on-search="handleSearchEvacuate()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.evacuate.query.isDeleted"
                        @on-change="handleSearchEvacuate"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.evacuate.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                      <Select
                        slot="prepend"
                        v-model="stores.evacuate.query.state"
                        @on-change="handleSearchEvacuate"
                        placeholder="疏散状态"
                        style="width:100px;"
                      >
                        <Option
                          v-for="item in stores.evacuate.sources.stateSources"
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
                  title="新增疏散"
                >新增疏散</Button>

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
                  @click="handleExportEvacuate('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportEvacuateyj('export')"
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
          :data="stores.evacuate.data"
          :columns="stores.evacuate.columns"
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
            <FormItem label="景区" prop="sceneSpot">
              <Input
                v-model.trim="formModel.fields.sceneSpot"
                placeholder="请输入景区"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="疏散时间" prop="evacuateTime">
              <DatePicker
                :value="formModel.fields.evacuateTime"
                @on-change="formModel.fields.evacuateTime=$event"
                format="yyyy-MM-dd HH:mm"
                type="datetime"
                placeholder="请输入疏散时间"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="姓名" prop="name">
              <Input
                v-model.trim="formModel.fields.name"
                placeholder="请输入姓名"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="性别" prop="sex">
              <Select
                filterable
                v-model="formModel.fields.sex"
                style="width:400px"
                placeholder="性别"
              >
                <Option
                  v-for="item in stores.evacuate.sources.sexList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="身份证号" prop="idCard">
              <Input
                v-model.trim="formModel.fields.idCard"
                placeholder="请输入身份证号"
                style="width: 400px"
                :maxlength="18"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="民族" prop="nation">
              <Input
                v-model.trim="formModel.fields.nation"
                placeholder="请输入民族"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="来源" prop="origion">
              <Input
                v-model.trim="formModel.fields.origion"
                placeholder="请输入来源"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="联系电话" prop="phone">
              <Input
                v-model.trim="formModel.fields.phone"
                placeholder="请输入联系电话"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="状态" prop="state" label-position="left">
              <i-switch
                size="large"
                v-model="formModel.fields.state"
                :true-value="1"
                :false-value="0"
              >
                <span slot="open">已疏散</span>
                <span slot="close">未疏散</span>
              </i-switch>
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkevacuate-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer
      title="疏散情况信息导入"
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
          title="疏散情况信息导入模板下载"
        >疏散情况信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportEvacuate"
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
    getEvacuateList,
    createEvacuate,
    loadEvacuate,
    editEvacuate,
    deleteEvacuate,
    batchCommand,
    EvacuateImport,
    EvacuateExport
  } from "@/api/emergency/evacuate";
  import { globalvalidatePhone,
    globalvalidateIDCard} from "@/global/validate"
  import { formatTimeToStr} from "@/global/untils"
  import config from "@/config";
  export default {
    name: "evacuate",
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
          title: "创建疏散",
          mode: "create",
          selection: [],
          fields: {
            sceneSpot:"",
            evacuateTime: "",
            name:"",
            sex:"",
            idCard:"",
            nation: "",
            origion:"",
            phone:"",
            state:0
          },
          rules: {
            sceneSpot: [{type: "string",required: true,message: "请输入景区",trigger:'blur'}],
            evacuateTime: [{type: "string",required: true,message: "请选择疏散时间",trigger:'blur'}],
            name: [{type: "string",required: true,message: "请输入姓名",trigger:'blur'}],
            sex: [{type: "string",required: true,message: "请选择性别",trigger:'blur'}],
            idCard: [{type: "string",required: true,message: "请输入身份证号",trigger:'blur'},
              {validator: globalvalidateIDCard, trigger: "blur"}],
            nation: [{type: "string",required: true,message: "请输入民族",trigger:'blur'}],
            origion: [{type: "string",required: true,message: "请输入来源",trigger:'blur'}],
            phone: [{type: "string",required: true,message: "请输入联系电话",trigger:'blur'},
              { validator: globalvalidatePhone, trigger: "blur" }
            ],

          }
        },
        stores: {
          evacuate: {
            query: {
              totalCount: 0,
              pageSize: 20,
              currentPage: 1,
              kw: "",
              isDeleted: 0,
              state:-1,
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
              stateSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "未疏散" },
                { value: 1, text: "已疏散" }
              ],
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "景区名称", key: "sceneSpot", sortable: true },
              { title: "疏散时间", key: "evacuateTime", sortable: true },
              { title: "姓名", key: "name" , sortable: true},
              { title: "性别", key: "sex", sortable: true },
              { title: "民族", key: "nation", sortable: true },
              { title: "联系电话", key: "phone" , sortable: true},
              { title: "状态", key: "state", sortable: true ,slot: "state"},
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
          sceneSpot:"",
          evacuateTime: "",
          name:"",
          sex:"",
          idCard:"",
          nation: "",
          origion:"",
          phone:"",
          state:0
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增疏散";
        }
        if (this.formModel.mode === "edit") {
          return "编辑疏散";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.evacuateUuid);
      }
    },
    methods: {
      renderState(state){
        let _state = "未知";
        switch (state) {
          case 0:
            _state="未疏散"
                break;
          case 1:
            _state="已疏散"
            break;
        }
        return _state;
      },
      loadEvacuateList() {
        getEvacuateList(this.stores.evacuate.query).then(res => {
          this.stores.evacuate.data = res.data.data;
          this.stores.evacuate.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchEvacuate() {
        this.stores.evacuate.query.currentPage = 1;
        this.loadEvacuateList();
      },
      handleRefresh() {
        this.stores.evacuate.query.currentPage = 1;
        this.loadEvacuateList();
      },
      //创建，修改
      handleSubmitPlan() {
        let valid = this.validateForm();
        if (valid) {
          if (this.formModel.mode === "create") {
            this.docreateEvacuate();
          }
          if (this.formModel.mode === "edit") {
            this.doEditPlan();
          }
        }
      },
      docreateEvacuate() {
        createEvacuate(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadEvacuateList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      doEditPlan() {
        editEvacuate(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadEvacuateList();
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
            this.loadEvacuateList();
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
        this.doDelete(row.evacuateUuid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteEvacuate(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadEvacuateList();
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
        this.doLoadEvacuate(row.evacuateUuid);
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
      doLoadEvacuate(guid) {
        loadEvacuate({ guid: guid }).then(res => {
          this.formModel.fields = res.data.data;
          //console.log(new Date(this.formModel.fields.evacuateTime,"yyyy-MM-dd hh:mm"))
          this.formModel.fields.evacuateTime=formatTimeToStr(this.formModel.fields.evacuateTime,"yyyy-MM-dd hh:mm");
          //console.log(this.formModel.fields.evacuateTime)
        });
      },
      handlePageChanged(page) {
        this.stores.evacuate.query.currentPage = page;
        this.loadEvacuateList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.evacuate.query.pageSize = pageSize;
        this.loadEvacuateList();
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
          this.url + "UploadFiles/ImportEmergencyModel/疏散情况信息导入模板.xlsx";
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
      async handleImportEvacuate() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await EvacuateImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
              this.successmsg = res.data.data.successmsg;
              this.repeatmsg = res.data.data.repeatmsg;
              this.defaultmsg = res.data.data.defaultmsg;
              this.loadEvacuateList();
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
      handleExportEvacuate(command) {
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
            this.doEvacuateExport(command);
          }
        });
      },
      //一键导出
      handleExportEvacuateyj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doEvacuateExport(command);
          }
        });
      },
      doEvacuateExport(command) {
        EvacuateExport(this.selectedRowsId.join(",")).then(res => {
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
      this.loadEvacuateList();
    }
  };
</script>
<style scoped>
</style>
