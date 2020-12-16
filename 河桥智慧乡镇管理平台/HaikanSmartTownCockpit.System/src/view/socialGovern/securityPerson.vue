<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.demo.query.totalCount"
        :pageSize="stores.demo.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="14">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.demo.query.kw"
                      placeholder="请输入姓名"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="10" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button
                    v-can="'delete'"
                    class="txt-danger"
                    icon="md-trash"
                    title="删除"
                    @click="handleBatchCommand('delete')"
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
          :data="stores.demo.data"
          :columns="stores.demo.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" v-can="'deletes'" size="small" shape="circle" icon="md-trash"></Button>
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
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleShowInfo(row)"
              ></Button>
            </Tooltip>
            <Tooltip placement="top" content="记录" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-paper"
                @click="handleShowReport(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <!-- 外层添加修改详情窗口 -->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formModel.fields"
        ref="formdispatch"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="16">
            <FormItem label="姓名" prop="securityName">
              <Input
                :readonly="checkShow()"
                v-model="formModel.fields.securityName"
                placeholder="姓名"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="16">
            <FormItem label="地点" prop="securityAddress">
              <Input
                :readonly="checkShow()"
                v-model="formModel.fields.securityAddress"
                placeholder="地点"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="16">
            <FormItem label="时间" prop="securityTime">
              <DatePicker
                v-model="formModel.fields.securityTime"
                @on-change="formModel.fields.securityTime=$event"
                format="yyyy-MM-dd"
                type="date"
                placeholder="时间"
                style="width: 240px"
                :disabled="checkShow()"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="16">
            <FormItem label="身份证号" prop="identityCard">
              <Input
                :readonly="checkShow()"
                v-model="formModel.fields.identityCard"
                placeholder="身份证号"
                :maxlength="18"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="16">
            <FormItem label="状态" prop="securityStaues">
              <Input
                :readonly="checkShow()"
                v-model="formModel.fields.securityStaues"
                placeholder="状态"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="16">
            <FormItem label="电话" prop="phone">
              <Input
                :readonly="checkShow()"
                v-model="formModel.fields.phone"
                placeholder="电话"
                :maxlength="11"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>
      <div class="demo-drawer-footer" v-if="!checkShow()">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formopen1">取 消</Button>
      </div>
    </Drawer>

    <Drawer
      title="上报记录"
      v-model="stores1.opened"
      width="80%"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Card>
        <dz-table
          :totalCount="stores1.demo.query.totalCount"
          :pageSize="stores1.demo.query.pageSize"
          @on-page-change="handlePageChanged1"
          @on-page-size-change="handlePageSizeChanged1"
        >
          <div slot="searcher">
            <section class="dnc-toolbar-wrap">
              <Row :gutter="16">
                <!-- <Col span="10">
                  <Form inline @submit.native.prevent>
                    <FormItem>
                      <Input
                        style="width: 150px;"
                        type="text"
                        search
                        :clearable="true"
                        v-model="stores1.demo.query.kw2"
                        placeholder="请输入姓名"
                        @on-search="handleSearchDispatch1()"
                      ></Input>
                    </FormItem>
                    <FormItem>
                      <Input
                        style="width: 150px;"
                        type="text"
                        search
                        :clearable="true"
                        v-model="stores1.demo.query.kw1"
                        placeholder="身份证号"
                        @on-search="handleSearchDispatch1()"
                      ></Input>
                    </FormItem>
                  </Form>
                </Col>-->
                <Col span="24" class="dnc-toolbar-btns">
                  <ButtonGroup class="mr3">
                    <Button
                      v-can="'delete'"
                      class="txt-danger"
                      icon="md-trash"
                      title="删除"
                      @click="handleBatchCommand2('delete')"
                    ></Button>
                    <Button icon="md-refresh" title="刷新" @click="handleRefresh1"></Button>
                  </ButtonGroup>
                  <Button
                    v-can="'create'"
                    icon="md-create"
                    type="primary"
                    @click="handleShowCreateWindow1"
                    title="添加"
                  >添加</Button>
                  <!-- <Button
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
                  >导出</Button>-->
                  <!-- <Button
                    v-can="'yjexport'"
                    icon="ios-cloud-download-outline"
                    type="primary"
                    @click="handleExportCuisineyj('export')"
                    title="一键导出"
                  >一键导出</Button>-->
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
            :data="stores1.demo.data"
            :columns="stores1.demo.columns"
            @on-select="handleSelect2"
            @on-selection-change="handleSelectionChange2"
            @on-refresh="handleRefresh1"
            :row-class-name="rowClsRender"
            @on-page-change="handlePageChanged1"
            @on-page-size-change="handlePageSizeChanged1"
          >
            <template slot-scope="{ row, index }" slot="action">
              <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete2(row)">
                <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                  <Button
                    type="error"
                    v-can="'deletes'"
                    size="small"
                    shape="circle"
                    icon="md-trash"
                  ></Button>
                </Tooltip>
              </Poptip>
              <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
                <Button
                  v-can="'edit'"
                  type="primary"
                  size="small"
                  shape="circle"
                  icon="md-create"
                  @click="handleEdit1(row)"
                ></Button>
              </Tooltip>
              <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
                <Button
                  v-can="'show'"
                  type="warning"
                  size="small"
                  shape="circle"
                  icon="md-search"
                  @click="handleShowInfo1(row)"
                ></Button>
              </Tooltip>
            </template>
          </Table>
        </dz-table>
      </Card>

      <Drawer
        title="安防人员信息导入"
        v-model="formimport.opened"
        width="600"
        :mask-closable="false"
        :mask="true"
        :styles="styles"
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
            title="安防人员信息导入模板下载"
          >安防人员信息导入模板下载</Button>
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
        <div class="demo-drawer-footer">
          <Button style="margin-left: 30px" icon="md-close" @click="formopen3">关 闭</Button>
        </div>
      </Drawer>
      
      <!-- <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formopen">关 闭</Button>
      </div>-->
    </Drawer>
    <!-- 内层添加修改详情窗口 -->
      <Drawer
        :title="formTitle2"
        v-model="formModel1.opened"
        width="400"
        :mask-closable="false"
        :mask="false"
        :styles="styles"
      >
        <Form
          :model="formModel1.fields"
          ref="formreport"
          :rules="formModel1.rules"
          label-position="top"
        >
          <!-- <Row :gutter="16">
          <Col span="16">
            <FormItem label="姓名" prop="securityName">
              <Input :readonly="checkShow()" v-model="formModel1.fields.securityName" placeholder="姓名" />
            </FormItem>
          </Col>
          
          </Row>-->
          <!-- <Row :gutter="16">
          <Col span="16">
            <FormItem label="地点" prop="securityAddress">
              <Input :readonly="checkShow()" v-model="formModel1.fields.securityAddress" placeholder="地点" />
            </FormItem>
          </Col>
          
          </Row>-->
          <Row :gutter="16">
            <Col span="16">
              <FormItem label="时间" prop="time">
                <DatePicker
                  v-model="formModel1.fields.time"
                  @on-change="formModel1.fields.time=$event"
                  format="yyyy-MM-dd"
                  type="date"
                  placeholder="时间"
                  style="width: 240px"
                  :disabled="checkShow2()"
                ></DatePicker>
              </FormItem>
            </Col>
          </Row>
          <!-- <Row :gutter="16">
          <Col span="16">
            <FormItem label="身份证号" prop="identityCard">
              <Input :readonly="checkShow()" v-model="formModel1.fields.identityCard" placeholder="身份证号" :maxlength=18 />
            </FormItem>
          </Col>
          </Row>-->
          <Row :gutter="16">
            <Col span="16">
              <FormItem label="状态" prop="state">
                <Input :readonly="checkShow2()" v-model="formModel1.fields.state" placeholder="状态" />
              </FormItem>
            </Col>
          </Row>
          <Row :gutter="16">
            <Col span="16">
              <FormItem label="上报情况" prop="situation">
                <Input
                  type="textarea"
                  :autosize="{minRows: 2,maxRows: 5}"
                  :readonly="checkShow2()"
                  v-model="formModel1.fields.situation"
                  placeholder="上报情况"
                />
              </FormItem>
            </Col>
          </Row>
        </Form>
        <div class="demo-drawer-footer" v-if="!checkShow2()">
          <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable2">保 存</Button>
          <Button style="margin-left: 30px" icon="md-close" @click="formModel1.opened=false">取 消</Button>
        </div>
      </Drawer>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
// import {
//   townlist, //显示村镇列表
//   CpcList, //显示党员列表
//   CpcCreate, //新增
//   CpcShow, //获取选定信息
//   batchCommand, //批量删除
//   deleteDepartment, //单个删除
//   CpcEdit, //编辑
//   UserInfoImport, //导入
//   UserInfoExport, //导出
// } from "@/api/cpcman/cpcman";
import {
  SecList,
  SecCreate,
  SecEdit,
  SecLoad,
  SecDelete,
  SecBatchCommand, //批量删除
  SecImport, //导入
  SecExport, //导出
  SerList,
  SerCreate,
  SerEdit,
  SerLoad,
  SerDelete,
  SerBatchCommand,
} from "@/api/socialGovern/securityPerson";
import config from "@/config";
import { getToken } from "@/libs/util";
import {
  globalvalidateIDCardNoMust,
  globalvalidatepwd,
  globalvalidateIsNotEmpty,
} from "@/global/validate";
export default {
  name: "securityPerson",
  components: {
    DzTable,
  },
  data() {
    return {
      //导入
      url: config.baseUrl.dev,
      importdisable: false,

      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false,
      },

      actionurl: "",
      postheaders: "",
      loadingStatus: false,
      updisabled: false,
      visible: false,
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        Import: { name: "Import", title: "导入" },
        export: { name: "export", title: "导出" },
      },
      stores: {
        demo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            isDelete: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          sources: {},
          //列表显示
          columns: [
            { type: "selection", width: 50, key: "securityUuid" },
            { title: "姓名", key: "securityName", sortable: true },
            { title: "时间", key: "securityTime" },
            { title: "身份证号", key: "identityCard" },
            { title: "联系电话", key: "phone" },
            { title: "状态", key: "securityStaues" },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 150,
              className: "table-command-column",
              slot: "action",
            },
          ],
          data: [],
        },
      },
      stores1: {
        opened: false,
        demo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2: "",
            isDelete: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          sources: {
            sexList: [
              {
                value: "男",
                label: "男",
              },
              {
                value: "女",
                label: "女",
              },
            ],
            xueliList: [],
            zhengzhiList: [],
            minzuList: [],
          },
          columns: [
            { type: "selection", width: 50, key: "securityReportUuid" },
            { title: "时间", key: "time" },
            { title: "状态", key: "state" },
            { title: "上报情况", key: "situation", ellipsis: true },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 150,

              className: "table-command-column",
              slot: "action",
            },
          ],
          data: [],
        },
      },
      formModel: {
        opened: false,
        title: "创建申请",
        mode: "create",
        dFileName: "xxxx",
        selection: [],
        fields: {
          securityUuid: "",
          securityName: "",
          securityAddress: "",
          identityCard: "",
          phone: "",
          securityTime: "",
          securityStaues: "",
          isDeleted: "",
          addTime: "",
          addPeople: "",
        },
        rules: {
          securityName: [
            {
              validator: globalvalidateIsNotEmpty,
              type: "string",
              required: true,
              message: "姓名不能为空",
            },
          ],
          identityCard: [
            {
              message: "请输入正确的身份证号",
              required: true,
            },
          ],
        },
      },
      formModel1: {
        opened: false,
        selection: [],
        mode: "create",
        fields: {
          securityReportUuid: "",
          name: "",
          time: "",
          state: "",
          situation: "",
          securityUuid: "",
          isDeleted: "",
        },
        rules: {
          time: [
            {
              required: true,
              message: "时间不能为空",
            },
          ],
          situation: [
            {
              message: "上报情况不能为空",
              required: true,
            },
          ],
        },
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static",
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增信息";
      }
      if (this.formModel.mode === "show") {
        return "详情";
      }
      if (this.formModel.mode === "edit") {
        return "编辑信息";
      }
      return "";
    },
    formTitle2() {
      if (this.formModel1.mode === "create") {
        return "新增信息";
      }
      if (this.formModel1.mode === "show") {
        return "详情";
      }
      if (this.formModel1.mode === "edit") {
        return "编辑信息";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.securityUuid);
    }, //删除
    selectedRows2() {
      return this.formModel1.selection;
    },
    selectedRowsId2() {
      return this.formModel1.selection.map((x) => x.securityReportUuid);
    }, //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      SecList(this.stores.demo.query).then((res) => {
        this.stores.demo.data = res.data.data;
        this.stores.demo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    handleSelect2(selection, row) {},
    //多选
    handleSelectionChange2(selection) {
      this.formModel1.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.demo.query.currentPage = page;
      this.loadDispatchList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.demo.query.pageSize = pageSize;
      this.loadDispatchList();
    },
    //翻页
    handlePageChanged1(page) {
      this.stores1.demo.query.currentPage = page;
      this.doSerList();
    },
    //显示条数改变
    handlePageSizeChanged1(pageSize) {
      this.stores1.demo.query.pageSize = pageSize;
      this.doSerList();
    },
    //行样式
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //搜索
    handleSearchDispatch() {
      this.loadDispatchList();
    },
    //搜索
    handleSearchDispatch1() {
      this.doSerList();
    },
    //刷新
    handleRefresh() {
      this.loadDispatchList();
    },
    //刷新
    handleRefresh1() {
      this.doSerList();
    },
    //记录上报显示
    handleShowReport(row) {
      this.stores1.opened = true;
      this.stores1.demo.query.kw = row.securityUuid;
      this.doSerList();
    },
    doSerList() {
      SerList(this.stores1.demo.query).then((res) => {
        this.stores1.demo.data = res.data.data;
        this.stores1.demo.query.totalCount = res.data.totalCount;
      });
    },
    formopen1() {
      this.formModel.opened = false;
      this.loadDispatchList();
    },
    formopen2() {
      this.formModel1.opened = false;
      this.doSerList();
    },
    formopen3() {
      this.formimport.opened = false;
      this.loadDispatchList();
    },
    formopen() {
      this.stores1.opened = false;
      this.loadDispatchList();
    },
    //党员列表显示
    handleDetail1(e) {
      this.stores1.opened = false;
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(e.securityUuid);
    },
    //清空
    handleResetFormDispatch() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch"].resetFields();
    },
    //清空
    handleResetFormDispatch2() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formreport"].resetFields();
    },
    //右边删除（单个删除）
    handleDelete(row) {
      this.doDelete(row.securityUuid);
    },
    handleDelete2(row) {
      this.doDelete2(row.securityReportUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      SecDelete(ids).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          // this.stores1.opened = true;
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doDelete2(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      SerDelete(ids).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.doSerList();
          // this.stores1.opened = true;
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //右上边删除按钮
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
    },
    handleBatchCommand2(command) {
      if (!this.selectedRowsId2 || this.selectedRowsId2.length <= 0) {
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
          this.doBatchCommand2(command);
        },
      });
    },
    //右上边删除
    doBatchCommand(command) {
      SecBatchCommand({
        command: command,
        ids: this.selectedRowsId.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.selection = [];
          this.loadDispatchList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    doBatchCommand2(command) {
      SerBatchCommand({
        command: command,
        ids: this.selectedRowsId2.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel1.selection = [];
          this.doSerList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    //添加按钮
    handleShowCreateWindow() {
      this.formModel.mode = "create";
      this.handleResetFormDispatch(); //清空表单
      // this.stores1.opened = false;
      this.formModel.opened = true;
    },
    //内添加按钮
    handleShowCreateWindow1() {
      this.formModel1.mode = "create";
      this.handleResetFormDispatch2(); //清空表单
      this.formModel1.opened = true;
      // this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      this.formModel.mode = "edit";
      // this.stores1.opened = false;
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.securityUuid);
    },
    //右边编辑
    handleEdit1(row) {
      this.formModel1.mode = "edit";
      // this.stores1.opened = false;
      this.formModel1.opened = true;
      this.handleResetFormDispatch2(); //清空表单
      this.doLoadData2(row.securityReportUuid);
    },
    handleShowInfo(row) {
      this.formModel.mode = "show";
      // this.stores1.opened = false;
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.securityUuid);
    },
    handleShowInfo1(row) {
      this.formModel1.mode = "show";
      // this.stores1.opened = false;
      this.formModel1.opened = true;
      this.handleResetFormDispatch2(); //清空表单
      this.doLoadData2(row.securityReportUuid);
    },
    //查询当前行信息
    doLoadData(guid) {
      SecLoad(guid).then((res) => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          // let data = res.data.data[0];
          this.formModel.fields = res.data.data;
          // this.formModel1.fields = data;
        }
      });
    },
    //查询当前行信息
    doLoadData2(guid) {
      SerLoad(guid).then((res) => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          // let data = res.data.data[0];
          this.formModel1.fields = res.data.data;
          // this.formModel1.fields = data;
        }
      });
    },
    //保存按钮
    handleSubmitConsumable() {
      if(this.formModel.fields.identityCard!=""){
        let reg1 = /^(^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$)|(^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])((\d{4})|\d{3}[Xx])$)$/;
        if (!reg1.test(this.formModel.fields.identityCard)) {
          this.$Message.warning("身份证号不合法!");
          return;
        }
      }else {
        this.$Message.warning("身份证号不能为空!");
        return;
      }
      if (this.formModel.fields.phone != "") {
        let reg2 = /^[1][3,4,5,7,8][0-9]{9}$/;
        if (!reg2.test(this.formModel.fields.phone)) {
          this.$Message.warning("手机号不合法!");
          return;
        }
      }
      if (this.formModel.mode === "create") {
        this.docreateDispatch();
      }
      if (this.formModel.mode === "edit") {
        this.doEditDispatch();
      }
    },
    handleSubmitConsumable2(){
      if (this.formModel1.mode === "create") {
        this.docreateDispatch2();
      }
      if (this.formModel1.mode === "edit") {
        this.doEditDispatch2();
      }
    },
    //添加（保存）
    docreateDispatch() {
      SecCreate(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    docreateDispatch2() {
      console.log(this.stores1.demo.query.kw);
      this.formModel1.fields.securityUuid=this.stores1.demo.query.kw;
      console.log(this.stores1.demo.query.kw);
      SerCreate(this.formModel1.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel1.opened = false; //关闭表单
          this.doSerList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      //this.datadeal();
      SecEdit(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doEditDispatch2() {
      //this.datadeal();
      SerEdit(this.formModel1.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel1.opened = false; //关闭表单
          this.doSerList();
          // this.stores1.opened = true;
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //导入相关操作
    handleImportCuisine() {
      this.successmsg = "";
      this.repeatmsg = "";
      this.defaultmsg = "";
      this.$refs.inputer.value = "";
      this.isexitexcel = false;
      this.stores1.opened = false;
      this.formimport.opened = true;
    },
    //下载模板
    handleimportmodel() {
      console.log(this.url);
      window.location.href =
        this.url + "UploadFiles/ImportUserInfoModel/安防人员信息导入.xlsx";
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
        await SecImport(this.exceldata).then((res) => {
          if (res.data.code == 200) {
            this.time = res.data.data.time;
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.loadDispatchList();
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
        },
      });
    },
    doCuisineExport(command) {
      console.log(this.selectedRowsId.join(","));
      SecExport(this.selectedRowsId.join(",")).then((res) => {
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
        },
      });
    },
    checkShow() {
      return this.formModel.mode == "show";
    },
    checkShow2() {
      return this.formModel1.mode == "show";
    },
  },
  mounted() {
    this.loadDispatchList(); //页面加载
  },
};
</script>
<style>
</style>