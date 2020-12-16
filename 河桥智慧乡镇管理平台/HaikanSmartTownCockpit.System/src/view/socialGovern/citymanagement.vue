<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.citymanagement.query.totalCount"
        :pageSize="stores.citymanagement.query.pageSize"
        :currentPage="stores.citymanagement.query.currentPage"
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
                      v-model="stores.citymanagement.query.kw"
                      placeholder="输入事件搜索..."
                      @on-search="handleSearchCityManagement()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.citymanagement.query.isDeleted"
                        @on-change="handleSearchCityManagement"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.citymanagement.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                      <!--                      <Select-->
                      <!--                        slot="prepend"-->
                      <!--                        v-model="stores.citymanagement.query.state"-->
                      <!--                        @on-change="handleSearchCityManagement"-->
                      <!--                        placeholder="响应状态"-->
                      <!--                        style="width:100px;"-->
                      <!--                      >-->
                      <!--                        <Option-->
                      <!--                          v-for="item in stores.citymanagement.sources.stateSources"-->
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
                  title="新增城管执法"
                >新增城管执法</Button>
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
                  @click="handleExportCityManagement('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCityManagementyj('export')"
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
          :data="stores.citymanagement.data"
          :columns="stores.citymanagement.columns"
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
            <FormItem label="事件" prop="incident">
              <Input
                v-model.trim="formModel.fields.incident"
                placeholder="请输入事件"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="时间" prop="zhifaTime">
              <DatePicker
                type="datetime"
                format="yyyy-MM-dd HH:mm"
                @on-change="formModel.fields.zhifaTime=$event"
                :value="formModel.fields.zhifaTime"
                placeholder="请输入时间"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="地址" prop="zhifaAddress">
              <Input
                v-model.trim="formModel.fields.zhifaAddress"
                placeholder="请输入地址"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="处理人" prop="chuliren">
              <Input
                v-model.trim="formModel.fields.chuliren"
                placeholder="请输入处理人"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="被处理人" prop="beiChulirren">
              <Input
                v-model.trim="formModel.fields.beiChulirren"
                placeholder="请输入被处理人"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="执法人" prop="zhifaRen">
              <Input
                v-model.trim="formModel.fields.zhifaRen"
                placeholder="请输入执法人"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <!-- <FormItem label="处理情况" prop="chuliQingk">
              <Input
                v-model.trim="formModel.fields.chuliQingk"
                placeholder="请输入处理情况"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>-->
            <FormItem label="处理情况" prop="chuliQingk">
              <Select
                filterable
                v-model="formModel.fields.chuliQingk"
                style="width:180px"
                placeholder="请输入处理情况"
              >
                <Option
                  v-for="item in stores.citymanagement.sources.sexList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkcitymanagement-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="城管执法信息导入"
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
          title="城管执法信息导入模板下载"
        >城管执法信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportCityManagement"
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
  getCityManagementList,
  createCityManagement,
  loadCityManagement,
  editCityManagement,
  deleteCityManagement,
  batchCommand,
  CityManagementImport,
  CityManagementExport
} from "@/api/socialGovern/citymanagement";
import { globalvalidatePhone } from "@/global/validate";
import config from "@/config";
export default {
  name: "citymanagement",
  components: {
    Tables,
    DzTable
  },
  data() {
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
        title: "创建城管执法",
        mode: "create",
        selection: [],
        fields: {
          incident: "",
          zhifaTime: "",
          zhifaAddress: "",
          chuliren: "",
          beiChulirren: "",
          zhifaRen: "",
          chuliQingk: ""
        },
        rules: {
          incident: [
            {
              type: "string",
              required: true,
              message: "请输入事件",
              trigger: "blur"
            }
          ],
          zhifaTime: [
            {
              type: "string",
              required: true,
              message: "请输入时间",
              trigger: "blur"
            }
          ],
          zhifaAddress: [
            {
              type: "string",
              required: true,
              message: "请输入地址",
              trigger: "blur"
            }
          ],
          chuliren: [
            {
              type: "string",
              required: true,
              message: "请输入处理人",
              trigger: "blur"
            }
          ],
          beiChulirren: [
            {
              type: "string",
              required: true,
              message: "请输入被处理人",
              trigger: "blur"
            }
          ],
          zhifaRen: [
            {
              type: "string",
              required: true,
              message: "请输入执法人",
              trigger: "blur"
            }
          ],
          chuliQingk: [
            {
              type: "string",
              required: true,
              message: "请输入处理情况",
              trigger: "blur"
            }
          ]
        }
      },
      stores: {
        citymanagement: {
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
            sexList: [
              {
                value: "已解决",
                label: "已解决"
              },
              {
                value: "未解决",
                label: "未解决"
              }
            ],
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
            { title: "事件", key: "incident", sortable: true },
            { title: "时间", key: "zhifaTime", sortable: true },
            { title: "地址", key: "zhifaAddress", sortable: true },
            { title: "处理人", key: "chuliren", sortable: true },
            { title: "被处理人", key: "beiChulirren", sortable: true },
            { title: "执法人", key: "zhifaRen", sortable: true },
            { title: "处理情况", key: "chuliQingk", sortable: true },
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
        incident: "",
        zhifaTime: "",
        zhifaAddress: "",
        chuliren: "",
        beiChulirren: "",
        zhifaRen: "",
        chuliQingk: ""
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增城管执法";
      }
      if (this.formModel.mode === "edit") {
        return "编辑城管执法";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.cityManagementUuid);
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
    loadCityManagementList() {
      getCityManagementList(this.stores.citymanagement.query).then(res => {
        this.stores.citymanagement.data = res.data.data;
        this.stores.citymanagement.query.totalCount = res.data.totalCount;
        //console.log(this.stores.educaplan.data);
      });
    },
    handleSearchCityManagement() {
      this.stores.citymanagement.query.currentPage = 1;
      this.loadCityManagementList();
    },
    handleRefresh() {
      this.stores.citymanagement.query.currentPage = 1;
      this.loadCityManagementList();
    },
    //创建，修改
    handleSubmitPlan() {
      let valid = this.validateForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateCityManagement();
        }
        if (this.formModel.mode === "edit") {
          this.doEditPlan();
        }
      }
    },
    docreateCityManagement() {
      createCityManagement(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadCityManagementList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doEditPlan() {
      editCityManagement(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadCityManagementList();
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
          this.loadCityManagementList();
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
      this.doDelete(row.cityManagementUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteCityManagement(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadCityManagementList();
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
      this.doLoadCityManagement(row.cityManagementUuid);
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
    doLoadCityManagement(guid) {
      loadCityManagement({ guid: guid }).then(res => {
        this.formModel.fields = res.data.data;
      });
    },
    handlePageChanged(page) {
      this.stores.citymanagement.query.currentPage = page;
      this.loadCityManagementList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.citymanagement.query.pageSize = pageSize;
      this.loadCityManagementList();
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
        "UploadFiles/ImportSocialGovernModel/城管执法信息导入模板.xlsx";
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
    async handleImportCityManagement() {
      this.importdisable = true;
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      if (this.isexitexcel) {
        await CityManagementImport(this.exceldata).then(res => {
          if (res.data.code == 200) {
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadCityManagementList();
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
    handleExportCityManagement(command) {
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
          this.doCityManagementExport(command);
        }
      });
    },
    //一键导出
    handleExportCityManagementyj(command) {
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doCityManagementExport(command);
        }
      });
    },
    doCityManagementExport(command) {
      CityManagementExport(this.selectedRowsId.join(",")).then(res => {
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
    this.loadCityManagementList();
  }
};
</script>
<style scoped>
</style>
