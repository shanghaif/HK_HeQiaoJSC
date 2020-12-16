<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.uav.query.totalCount"
        :pageSize="stores.uav.query.pageSize"
        :currentPage="stores.uav.query.currentPage"
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
                      v-model="stores.uav.query.kw"
                      placeholder="输入无人机名称搜索..."
                      @on-search="handleSearchUav()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.uav.query.isDeleted"
                        @on-change="handleSearchUav"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.uav.sources.isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                        >{{item.text}}</Option>
                      </Select>
                      <!--                      <Select-->
                      <!--                        slot="prepend"-->
                      <!--                        v-model="stores.uav.query.state"-->
                      <!--                        @on-change="handleSearchUav"-->
                      <!--                        placeholder="疏散状态"-->
                      <!--                        style="width:100px;"-->
                      <!--                      >-->
                      <!--                        <Option-->
                      <!--                          v-for="item in stores.uav.sources.stateSources"-->
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
                                  title="新增无人机"
                                >新增无人机</Button>
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
                  @click="handleExportUav('export')"
                  title="导出"
                >导出</Button>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportUavyj('export')"
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
          :data="stores.uav.data"
          :columns="stores.uav.columns"
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
                      <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
                        <Button
                          v-can="'show'"
                          type="success"
                          size="small"
                          shape="circle"
                          icon="md-search"
                          @click="handleEditUrl(row)"
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
            <FormItem label="无人机名称" prop="uavname">
              <Input
                v-model.trim="formModel.fields.uavname"
                placeholder="请输入无人机名称"
                style="width: 400px"
                :maxlength="10"
              />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="设别编号" prop="uavnumber">
              <Input
                v-model.trim="formModel.fields.uavnumber"
                placeholder="请输入设别编号"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="链接" prop="uavurl">
              <Input
                v-model.trim="formModel.fields.uavurl"
                placeholder="请输入链接"
                style="width: 400px"
                :maxlength="50"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="监控地址" prop="uavaddress">
              <Input
                v-model.trim="formModel.fields.uavaddress"
                placeholder="请输入监控地址"
                style="width: 400px"
                :maxlength="40"
              />
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
          :title="'监控'"
          v-model="formModel1.opened"
          width="500"
          :mask-closable="false"
          :mask="false"
          :styles="styles"
        >
          <!--在视频外面加一个容器-->
          <div class="input_video" v-if="formModel1.opened">
            <video-player ref="playerObj"></video-player>
            <a @click="playVideo">播放视频</a>
          </div>
        </Drawer>
    <Drawer
      title="无人机信息导入"
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
          title="无人机信息导入模板下载"
        >无人机信息导入模板下载</Button>
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportUav"
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
    getUavList,
    loadUavUrl,
    createUav,
    loadUav,
    editUav,
    deleteUav,
    batchCommand,
    UavImport,
    UavExport
  } from "@/api/emergency/uav";
  import { globalvalidatePhone } from "@/global/validate"
  import VideoPlayer from '@/components/VideoPlayer'
  import config from "@/config";
  export default {
    name: "uav",
    components: {
      Tables,
      DzTable,
      VideoPlayer
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
        formModel1: {
          opened: false,
        },
        formModel: {
          opened: false,
          title: "创建无人机",
          mode: "create",
          selection: [],
          fields: {
            uavname:"",
            uavnumber: "",
            uavurl:"",
            uavaddress:"",
          },
          rules: {
            uavname: [{type: "string",required: true,message: "请输入无人机名称",trigger:'blur'}],
            uavnumber: [{type: "string",required: true,message: "请输入设备编号",trigger:'blur'}],
            uavurl: [{type: "string",required: true,message: "请输入链接",trigger:'blur'}],
            uavaddress: [{type: "string",required: true,message: "请输入监控地址",trigger:'blur'}
            ]
          }
        },
        stores: {
          uav: {
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
              stateSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "已疏散" },
                { value: 1, text: "未疏散" }
              ],
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "无人机名称", key: "uavname", sortable: true },
              { title: "设备编号", key: "uavnumber", sortable: true },
              { title: "链接", key: "uavurl", sortable: true },
              { title: "监控地址", key: "uavaddress", sortable: true },
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
          // rescueTeamUuid:"",
          // memberName: "",
          // memberSex:"",
          // memberPhone:"",
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "无人机";
        }
        if (this.formModel.mode === "edit") {
          return "无人机";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.uavuuid);
      }
    },
    methods: {
      renderState(state){
        let _state = "未知";
        switch (state) {
          case 0:
            _state="未响应"
            break;
          case 1:
            _state="已响应"
            break;
        }
        return _state;
      },
      loadUavList() {
        getUavList(this.stores.uav.query).then(res => {
          this.stores.uav.data = res.data.data;
          this.stores.uav.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchUav() {
        this.stores.uav.query.currentPage = 1;
        this.loadUavList();
      },
      handleRefresh() {
        this.stores.uav.query.currentPage = 1;
        this.loadUavList();
      },
      //创建，修改
      handleSubmitPlan() {
        let valid = this.validateForm();
        if (valid) {
          if (this.formModel.mode === "create") {
            this.docreateUav();
          }
          if (this.formModel.mode === "edit") {
            this.doEditPlan();
          }
        }
      },
      docreateUav() {
        createUav(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadUavList();
          } else {
            this.$Message.warning(res.data.message);
          }
        });
      },
      doEditPlan() {
        editUav(this.formModel.fields).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.handleCloseFormWindow();
            this.loadUavList();
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
            this.loadUavList();
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
        this.doDelete(row.uavuuid);
      },
      doDelete(ids) {
        if (!ids) {
          this.$Message.warning("请选择至少一条数据");
          return;
        }
        deleteUav(ids).then(res => {
          if (res.data.code === 200) {
            this.$Message.success(res.data.message);
            this.loadUavList();
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
        this.doLoadUav(row.uavuuid);
      },
      handleEditUrl(row) {
        // this.handleSwitchFormModeToEdit();
        // this.handleResetFormRole();
        this.formModel1.opened=true;
        this.doLoadUavUrl(row.uavuuid);
      },
      //
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
      doLoadUav(guid) {
        loadUav({ guid: guid }).then(res => {
          this.formModel.fields = res.data.data;
        });
      },
      doLoadUavUrl(guid) {
        loadUavUrl({ guid: guid }).then(res => {
          this.$refs['playerObj'].videoSrc = res.data.data.uavurl;
          this.$refs['playerObj'].playerOptions.sources[0].src = res.data.data.uavurl;
        });
      },
      playVideo() {
        this.$refs['playerObj'].videoSrc = 'rtmp://xxxxxxxx'
        this.$refs['playerObj'].playerOptions.sources[0].src = 'rtmp://xxxxxxxx'
      },
      handlePageChanged(page) {
        this.stores.uav.query.currentPage = page;
        this.loadUavList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.uav.query.pageSize = pageSize;
        this.loadUavList();
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
          this.url + "UploadFiles/ImportEmergencyModel/无人机信息导入模板.xlsx";
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
      async handleImportUav() {
        this.importdisable = true;
        this.successmsg = "";
        this.repeatmsg = "";
        this.defaultmsg = "";
        if (this.isexitexcel) {
          await UavImport(this.exceldata).then(res => {
            if (res.data.code == 200) {
              this.successmsg = res.data.data.successmsg;
              this.repeatmsg = res.data.data.repeatmsg;
              this.defaultmsg = res.data.data.defaultmsg;
              this.loadUavList();
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
      handleExportUav(command) {
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
            this.doUavExport(command);
          }
        });
      },
      //一键导出
      handleExportUavyj(command) {
        this.$Modal.confirm({
          title: "操作提示",
          content:
            "<p>确定要执行当前 [" +
            this.commands[command].title +
            "] 操作吗?</p>",
          loading: true,
          onOk: () => {
            this.doUavExport(command);
          }
        });
      },
      doUavExport(command) {
        UavExport(this.selectedRowsId.join(",")).then(res => {
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
      this.loadUavList();
    }
  };
</script>
<style scoped>
  .input_video{
    width: 400px;
    height: 400px;
    margin: 0 auto;

  }
</style>
