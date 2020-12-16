<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.rectifyinfo.query.totalCount"
        :pageSize="stores.rectifyinfo.query.pageSize"
        :currentPage="stores.rectifyinfo.query.currentPage"
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
                      v-model="stores.rectifyinfo.query.kw"
                      placeholder="输入单位/姓名搜索..."
                      @on-search="handleSearchRectifyInfo()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.rectifyinfo.query.isDeleted"
                        @on-change="handleSearchRectifyInfo"
                        placeholder="删除状态"
                        style="width: 60px"
                      >
                        <Option
                          v-for="item in stores.rectifyinfo.sources
                            .isDeletedSources"
                          :value="item.value"
                          :key="item.value"
                          >{{ item.text }}</Option
                        >
                      </Select>
                      <!--                      <Select-->
                      <!--                        slot="prepend"-->
                      <!--                        v-model="stores.rectifyinfo.query.state"-->
                      <!--                        @on-change="handleSearchRectifyInfo"-->
                      <!--                        placeholder="响应状态"-->
                      <!--                        style="width:100px;"-->
                      <!--                      >-->
                      <!--                        <Option-->
                      <!--                          v-for="item in stores.rectifyinfo.sources.stateSources"-->
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

                  <Button
                    icon="md-refresh"
                    title="刷新"
                    @click="handleRefresh"
                  ></Button>
                </ButtonGroup>
                <Button
                  v-can="'create'"
                  icon="md-create"
                  type="primary"
                  @click="handleShowCreateWindow"
                  title="新增社区矫正"
                  >新增社区矫正</Button
                >
                <Button
                  v-can="'import'"
                  icon="ios-cloud-upload"
                  type="success"
                  @click="handleImport"
                  title="导入"
                  >导入</Button
                >
                <Button
                  v-can="'export'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportRectifyInfo('export')"
                  title="导出"
                  >导出</Button
                >
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportRectifyInfoyj('export')"
                  title="一键导出"
                  >一键导出</Button
                >
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
          :data="stores.rectifyinfo.data"
          :columns="stores.rectifyinfo.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{ row, index }" slot="state">
            <span>{{ renderState(row.state) }}</span>
          </template>
          <!--          <template slot-scope="{row,index}" slot="rectifyTiem">-->
          <!--            <span>{{renderformatTimeToStr(row.rectifyTiem)}}</span>-->
          <!--          </template>-->
          <!--          <template slot-scope="{row,index}" slot="kaishiTime">-->
          <!--            <span>{{renderformatTimeToStr(row.kaishiTime)}}</span>-->
          <!--          </template>-->
          <!--          <template slot-scope="{row,index}" slot="jiesuTime">-->
          <!--            <span>{{renderformatTimeToStr(row.jiesuTime)}}</span>-->
          <!--          </template>-->

          <template slot-scope="{ row, index }" slot="action">
            <Poptip
              confirm
              :transfer="true"
              title="确定要删除吗?"
              @on-ok="handleDelete(row)"
            >
              <Tooltip
                placement="top"
                content="删除"
                :delay="1000"
                :transfer="true"
              >
                <Button
                  v-can="'deletes'"
                  type="error"
                  size="small"
                  shape="circle"
                  icon="md-trash"
                ></Button>
              </Tooltip>
            </Poptip>
            <Tooltip
              placement="top"
              content="编辑"
              :delay="1000"
              :transfer="true"
            >
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
            <FormItem label="矫正单位" prop="rectifyInfoUnit">
              <Input
                v-model.trim="formModel.fields.rectifyInfoUnit"
                placeholder="请输入矫正单位"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="姓名" prop="rectifyInfoName">
              <Input
                v-model.trim="formModel.fields.rectifyInfoName"
                placeholder="请输入姓名"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="矫正状态" prop="rectifyInfoStaues">
              <Input
                v-model.trim="formModel.fields.rectifyInfoStaues"
                placeholder="请输入矫正状态"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="定位手机" prop="dweiPhone">
              <Input
                v-model.trim="formModel.fields.dweiPhone"
                placeholder="请输入定位手机"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="司法部上报状态" prop="shangbanStaues">
              <Input
                v-model.trim="formModel.fields.shangbanStaues"
                placeholder="请输入司法部上报状态"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="矫正类别" prop="rectifyType">
              <Input
                v-model.trim="formModel.fields.rectifyType"
                placeholder="请输入矫正类别"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="入矫时间" prop="rectifyTiem">
              <DatePicker
                type="date"
                format="yyyy-MM-dd"
                @on-change="formModel.fields.rectifyTiem = $event"
                :value="formModel.fields.rectifyTiem"
                placeholder="请输入时间"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="矫正开始时间" prop="kaishiTime">
              <DatePicker
                type="date"
                format="yyyy-MM-dd"
                @on-change="formModel.fields.kaishiTime = $event"
                :value="formModel.fields.kaishiTime"
                placeholder="请输入时间"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="矫正结束时间" prop="jiesuTime">
              <DatePicker
                type="date"
                format="yyyy-MM-dd"
                @on-change="formModel.fields.jiesuTime = $event"
                :value="formModel.fields.jiesuTime"
                placeholder="请输入时间"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="性别" prop="sex">
              <Input
                v-model.trim="formModel.fields.sex"
                placeholder="请输入性别"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="出生日期" prop="birthday">
              <DatePicker
                type="date"
                format="yyyy-MM-dd"
                @on-change="formModel.fields.birthday = $event"
                :value="formModel.fields.birthday"
                placeholder="请输入出生日期"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="身份证" prop="identityCard">
              <Input
                v-model.trim="formModel.fields.identityCard"
                placeholder="请输入身份证"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="原判刑期" prop="cirterion">
              <Input
                v-model.trim="formModel.fields.cirterion"
                placeholder="请输入原判刑期"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="现住地址" prop="address">
              <Input
                v-model.trim="formModel.fields.address"
                placeholder="请输入现住地址"
                style="width: 400px"
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
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="有无服务成员" prop="service">
              <Input
                v-model.trim="formModel.fields.service"
                placeholder="请输入文化程度"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="服务时间" prop="servicingTime">
              <Input
                v-model.trim="formModel.fields.servicingTime"
                placeholder="请输入服务时间"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="婚姻状态" prop="marriage">
              <Input
                v-model.trim="formModel.fields.marriage"
                placeholder="请输入婚姻状态"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>
      <div style="margin-top: 100px">
        <Button
          icon="md-checkrectifyinfo-circle"
          type="primary"
          @click="handleSubmitPlan"
          >保 存</Button
        >
        <Button
          style="margin-left: 8px"
          icon="md-close"
          @click="formModel.opened = false"
          >取 消</Button
        >
      </div>
    </Drawer>
    <Drawer
      title="社区矫正信息导入"
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
          title="社区矫正信息导入模板下载"
          >社区矫正信息导入模板下载</Button
        >
        <Button
          icon="md-checkmark-circle"
          type="primary"
          @click="handleImportRectifyInfo"
          :disabled="importdisable"
          >导入</Button
        >

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
  getRectifyInfoList,
  createRectifyInfo,
  loadRectifyInfo,
  editRectifyInfo,
  deleteRectifyInfo,
  batchCommand,
  RectifyInfoImport,
  RectifyInfoExport,
} from "@/api/socialGovern/rectifyinfo";
import { globalvalidatePhone } from "@/global/validate";
import { formatTimeToStr } from "@/global/untils";
import config from "@/config";
export default {
  name: "rectifyinfo",
  components: {
    Tables,
    DzTable,
  },
  data() {
    let checkEndDate = (rule, value, callback, source) => {
      let etime = new Date(Date.parse(value.replace(/-/g, "/")));
      let stime1 = null;
      let stime2 = null;
      let fields = this.formModel.fields;
      let p = Object.keys(source)[0];
      let error1 = "矫正结束时间必须大于矫正开始时间";
      let error2 = "矫正结束时间必须大于入矫时间";
      if (p !== null && p !== "") {
        if (
          p == "jiesuTime" &&
          fields.kaishiTime != "" &&
          fields.rectifyTiem != ""
        ) {
          stime1 = new Date(Date.parse(fields.kaishiTime.replace(/-/g, "/")));
          stime2 = new Date(Date.parse(fields.rectifyTiem.replace(/-/g, "/")));
        }
        if (etime < stime1) {
          callback(new Error(error1));
        }
        if (etime < stime2) {
          callback(new Error(error2));
        }
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
        opened: false,
      },

      commands: {
        export: { name: "export", title: "导出" },
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" },
      },
      formModel: {
        opened: false,
        title: "创建社区矫正",
        mode: "create",
        selection: [],
        fields: {
          rectifyInfoUnit: "",
          rectifyInfoName: "",
          rectifyInfoStaues: "",
          dweiPhone: "",
          shangbanStaues: "",
          rectifyType: "",
          rectifyTiem: "",
          kaishiTime: "",
          jiesuTime: "",
          sex: "",
          birthday: "",
          identityCard: "",
          cirterion: "",
          address: "",
          nation: "",
          standard: "",
          service: "",
          servicingTime: "",
          marriage: "",
        },
        rules: {
          // rectifyInfoUnit: [{type: "string",required: true,message: "请输入矫正单位",trigger:'blur'}],
          rectifyInfoName: [
            {
              type: "string",
              required: true,
              message: "请输入姓名",
              trigger: "blur",
            },
          ],
          // rectifyInfoStaues: [{type: "string",required: true,message: "请输入矫正状态",trigger:'blur'}],
          // dweiPhone: [{type: "string",required: true,message: "请输入定位手机",trigger:'blur'},{validator:globalvalidatePhone,trigger:'blur'}],
          // shangbanStaues: [{type: "string",required: true,message: "请输入司法部上报状态",trigger:'blur'}],
          // rectifyType: [{type: "string",required: true,message: "请输入矫正类别",trigger:'blur'}],
          // rectifyTiem: [{type: "string",required: true,message: "请输入入矫时间",trigger:'blur'}],
          // kaishiTime: [{type: "string",required: true,message: "请输入矫正开始时间",trigger:'blur'}],
          // jiesuTime: [{type: "string",required: true,message: "请输入矫正结束时间",trigger:'blur'},{validator:checkEndDate,trigger:'blur'}]
        },
      },
      stores: {
        rectifyinfo: {
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
                field: "ID",
              },
            ],
          },
          sources: {
            isDeletedSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "正常" },
              { value: 1, text: "已删" },
            ],
            stateSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "未响应" },
              { value: 1, text: "已响应" },
            ],
          },
          columns: [
            { type: "selection", width: 50, key: "handle" },
            { title: "矫正单位", key: "rectifyInfoUnit", sortable: true },
            { title: "姓名", key: "rectifyInfoName", sortable: true },
            { title: "矫正状态", key: "rectifyInfoStaues", sortable: true },
            { title: "定位手机", key: "dweiPhone", sortable: true },
            { title: "司法部上报状态", key: "shangbanStaues", sortable: true },
            { title: "矫正类别", key: "rectifyType", sortable: true },
            { title: "入矫时间", key: "rectifyTiem", sortable: true },
            { title: "矫正开始时间", key: "kaishiTime", sortable: true },
            { title: "矫正结束时间", key: "jiesuTime", sortable: true },
            {
              title: "操作",
              fixed: "right",
              align: "center",
              width: 150,
              className: "table-command-column",
              slot: "action",
            },
          ],
          data: [],
        },
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static",
      },
      initdatacopy: {
        rectifyInfoUnit: "",
        rectifyInfoName: "",
        rectifyInfoStaues: "",
        dweiPhone: "",
        shangbanStaues: "",
        rectifyType: "",
        rectifyTiem: "",
        kaishiTime: "",
        jiesuTime: "",
        sex: "",
        birthday: "",
        identityCard: "",
        cirterion: "",
        address: "",
        nation: "",
        standard: "",
        service: "",
        servicingTime: "",
        marriage: "",
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增社区矫正人员";
      }
      if (this.formModel.mode === "edit") {
        return "编辑社区矫正人员";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.rectifyInfoUuid);
    },
  },
  methods: {
    renderformatTimeToStr(time) {
      return formatTimeToStr(time, "yyyy-MM-dd");
    },
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
    loadRectifyInfoList() {
      getRectifyInfoList(this.stores.rectifyinfo.query).then((res) => {
        this.stores.rectifyinfo.data = res.data.data;
        this.stores.rectifyinfo.query.totalCount = res.data.totalCount;
        //console.log(this.stores.educaplan.data);
      });
    },
    handleSearchRectifyInfo() {
      this.stores.rectifyinfo.query.currentPage = 1;
      this.loadRectifyInfoList();
    },
    handleRefresh() {
      this.stores.rectifyinfo.query.currentPage = 1;
      this.loadRectifyInfoList();
    },
    //创建，修改
    handleSubmitPlan() {
      let valid = this.validateForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateRectifyInfo();
        }
        if (this.formModel.mode === "edit") {
          this.doEditPlan();
        }
      }
    },
    docreateRectifyInfo() {
      createRectifyInfo(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadRectifyInfoList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doEditPlan() {
      editRectifyInfo(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadRectifyInfoList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    validateForm() {
      let _valid = false;
      this.$refs["formPlan"].validate((valid) => {
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
        },
      });
      //addsystemlog("delete","删除年度市级招生方案列表");
    },
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRectifyInfoList();
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
      this.doDelete(row.rectifyInfoUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteRectifyInfo(ids).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRectifyInfoList();
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
      this.doLoadRectifyInfo(row.rectifyInfoUuid);
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
    doLoadRectifyInfo(guid) {
      loadRectifyInfo({ guid: guid }).then((res) => {
        this.formModel.fields = res.data.data;
      });
    },
    handlePageChanged(page) {
      this.stores.rectifyinfo.query.currentPage = page;
      this.loadRectifyInfoList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.rectifyinfo.query.pageSize = pageSize;
      this.loadRectifyInfoList();
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
        "UploadFiles/ImportSocialGovernModel/社区矫正信息导入模板.xlsx";
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
    async handleImportRectifyInfo() {
      this.importdisable = true;
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      if (this.isexitexcel) {
        await RectifyInfoImport(this.exceldata).then((res) => {
          if (res.data.code == 200) {
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadRectifyInfoList();
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
    handleExportRectifyInfo(command) {
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
          this.doRectifyInfoExport(command);
        },
      });
    },
    //一键导出
    handleExportRectifyInfoyj(command) {
      this.$Modal.confirm({
        title: "操作提示",
        content:
          "<p>确定要执行当前 [" +
          this.commands[command].title +
          "] 操作吗?</p>",
        loading: true,
        onOk: () => {
          this.doRectifyInfoExport(command);
        },
      });
    },
    doRectifyInfoExport(command) {
      RectifyInfoExport(this.selectedRowsId.join(",")).then((res) => {
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
    this.loadRectifyInfoList();
  },
};
</script>
<style scoped>
</style>
