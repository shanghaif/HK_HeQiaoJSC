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
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.demo.query.kw"
                      placeholder="请输入村镇名称"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
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
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleDetail(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer title="党员信息" v-model="stores1.opened" width="80%" :mask-closable="false" :mask="true" @on-close="onclose">
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
                <Col span="10">
                  <Form inline @submit.native.prevent>
                    <FormItem>
                      <Input
                        style="width: 150px;"
                        type="text"
                        search
                        :clearable="true"
                        v-model="stores1.demo.query.kw1"
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
                        v-model="stores1.demo.query.kw2"
                        placeholder="家庭住址"
                        @on-search="handleSearchDispatch1()"
                      ></Input>
                    </FormItem>
                  </Form>
                </Col>
                <Col span="14" class="dnc-toolbar-btns">
                  <ButtonGroup class="mr3">
                    <Button
                      v-can="'delete'"
                      class="txt-danger"
                      icon="md-trash"
                      title="删除"
                      @click="handleBatchCommand('delete')"
                    ></Button>
                    <Button icon="md-refresh" title="刷新" @click="handleRefresh1"></Button>
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
            :data="stores1.demo.data"
            :columns="stores1.demo.columns"
            @on-select="handleSelect"
            @on-selection-change="handleSelectionChange"
            @on-refresh="handleRefresh1"
            :row-class-name="rowClsRender"
            @on-page-change="handlePageChanged1"
            @on-page-size-change="handlePageSizeChanged1"
          >
            <template slot-scope="{ row, index }" slot="action">
              <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">
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
                  @click="handleEdit(row)"
                ></Button>
              </Tooltip>
              <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
                <Button
                  v-can="'show1'"
                  type="warning"
                  size="small"
                  shape="circle"
                  icon="md-search"
                  @click="handleDetail1(row)"
                ></Button>
              </Tooltip>
            </template>
          </Table>
        </dz-table>
      </Card>
    </Drawer>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="true"
    >
      <Form
        :model="formModel.fields"
        ref="formdispatch"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
            <FormItem label="姓名" prop="realName">
              <Input v-model="formModel.fields.realName" placeholder="姓名" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="性别" prop="sex">
              <Select filterable v-model="formModel.fields.sex" style="width:100%" placeholder="性别">
                <Option
                  v-for="item in stores1.demo.sources.sexList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="出生日期" prop="birth">
            <DatePicker
              v-model="formModel.fields.birth"
              @on-change="formModel.fields.birth=$event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="请选择出生日期"
              style="width: 100%"
              :options="options3"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="学历" prop="education">
              <Select
                filterable
                v-model="formModel.fields.education"
                style="width:100%"
                placeholder="学历"
              >
                <Option
                  v-for="item in stores1.demo.sources.xueliList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="人员类别" prop="category">
              <Select
                filterable
                v-model="formModel.fields.category"
                style="width:100%"
                placeholder="人员类别"
              >
                <Option
                  v-for="item in stores1.demo.sources.categoryList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="所在党支部" prop="partybranch">
              <Select filterable v-model="formModel.fields.partybranch" style="width:100%" placeholder="所在党支部">
                <Option
                  v-for="item in stores1.demo.sources.departList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="加入党组织时间" prop="joinDate">
            <DatePicker
              v-model="formModel.fields.joinDate"
              @on-change="formModel.fields.joinDate=$event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="请选择加入党组织时间"
              style="width: 100%"
              :options="options3"
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="联系方式" prop="phone">
            <Input v-model="formModel.fields.phone"  placeholder="请输入联系方式" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="家庭住址" prop="residence">
            <Input v-model="formModel.fields.residence" placeholder="请输入家庭住址" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="工作单位" prop="work">
            <Input v-model="formModel.fields.work"   placeholder="请输入工作单位" />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formopen1">取 消</Button>
      </div>
    </Drawer>

    <Drawer title="详情" v-model="formModel1.opened" width="400" :mask-closable="false" :mask="false">
      <Form :model="formModel1.fields" ref="formdispatch22" label-position="top">
        <Row :gutter="16">
            <FormItem label="姓名" prop="realName">
              <Input v-model="formModel.fields.realName" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="性别" prop="sex">
              <Input v-model="formModel1.fields.sex" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="年龄" prop="age">
              <Input v-model="formModel1.fields.age" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="出生日期" prop="birth">
              <DatePicker
                v-model="formModel1.fields.birth"
                @on-change="formModel1.fields.birth=$event"
                format="yyyy-MM-dd"
                type="date"
                placeholder="出生日期"
                style="width: 100%"
                disabled
              ></DatePicker>
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="学历" prop="education">
              <Input v-model="formModel1.fields.education" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="人员类别" prop="category">
              <Input v-model="formModel1.fields.category" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="所在党支部" prop="partybranch">
              <Input v-model="formModel1.fields.partybranch" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="加入党组织时间" prop="joinDate">
            <DatePicker
              v-model="formModel1.fields.joinDate"
              @on-change="formModel1.fields.joinDate=$event"
              format="yyyy-MM-dd"
              type="date"
              placeholder="请选择加入党组织时间"
              style="width: 100%"
              disabled
            ></DatePicker>
          </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="联系方式" prop="phone">
              <Input v-model="formModel1.fields.phone" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="家庭住址" prop="residence">
              <Input v-model="formModel1.fields.residence" :readonly="true" />
            </FormItem>
        </Row>
        <Row :gutter="16">
            <FormItem label="工作单位" prop="work">
              <Input v-model="formModel1.fields.work" :readonly="true" />
            </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formopen2">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="人员信息导入"
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
          v-can="'template'"
          icon="ios-cloud-download"
          type="warning"
          @click="handleimportmodel"
          title="人员信息导入模板下载"
        >人员信息导入模板下载</Button>
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
    </Drawer>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import {
  townlist, //显示村镇列表
  CpcList, //显示党员列表
  CpcCreate, //新增
  CpcShow, //获取选定信息
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  CpcEdit, //编辑
  UserInfoImport, //导入
  UserInfoExport, //导出
} from "@/api/cpcman/cpcman";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "cpcman",
  components: {
    DzTable,
  },
  data() {
    return {
       options3: {
        disabledDate(date) {
          return date && date.valueOf() > Date.now();
        }
      },
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
            { title: "村镇名称", key: "townName" },
            { title: "总人口", key: "townPeople" },
            { title: "党员人数", key: "partyMember" },
            {
              title: "操作",
              align: "center",
              width: 200,
              className: "table-command-column",
              slot: "action",
            },
          ],
          data: [],
        },
      },
      stores1: {
        opened: false,
        selection: [],
        demo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kw1: "",
            kw2:"",
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
            categoryList: [
              {
                value: "正式党员",
                label: "正式党员",
              },
              {
                value: "预备党员",
                label: "预备党员",
              },
              {
                value: "发展对象",
                label: "发展对象",
              },
              {
                value: "积极分子",
                label: "积极分子",
              },
            ],
            departList: [
              {
                value: "机关党组织",
                label: "机关党组织",
              },
              {
                value: "村级党支部",
                label: "村级党支部",
              },
              {
                value: "企业党支部",
                label: "企业党支部",
              },
            ],
            xueliList: [
              {
                value: "小学",
                label: "小学",
              },
              {
                value: "初中",
                label: "初中",
              },
              {
                value: "高中",
                label: "高中",
              },
              {
                value: "中专",
                label: "中专",
              },
              {
                value: "大专",
                label: "大专",
              },
              {
                value: "本科",
                label: "本科",
              },
              {
                value: "硕士",
                label: "硕士",
              },
              {
                value: "博士",
                label: "博士",
              },
            ]
          },
          columns: [
            { type: "selection", width: 50, key: "userInfoUuid" },
            { title: "姓名", key: "realName", sortable: true },
            { title: "性别", key: "sex" },
            { title: "出生日期", key: "birth" },
            { title: "学历", key: "education" },
            { title: "联系方式", key: "phone" },
            { title: "家庭住址", key: "residence", ellipsis: true, tooltip: true },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 100,

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
          town:"",
          realName: "",
          sex: "",
          birth: "",
          education: "",
          category: "",
          partybranch: "",
          joinDate: "",
          phone: "",
          residence: "",
          work: "",
          userInfoUuid: "",
        },
        rules: {
          realName: [
            { type: "string", required: true, message: "姓名不能为空" },
          ],
          birth: [
            { required: true, message: "出生日期不能为空" },
          ],
          residence: [
            { type: "string", required: true, message: "家庭住址不能为空" },
          ],
        },
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          town:"",
          realName: "",
          sex: "",
          birth: "",
          education: "",
          category: "",
          partybranch: "",
          joinDate: "",
          phone: "",
          residence: "",
          work: "",
          userInfoUuid: "",
          age:""
        },
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增信息";
      }
      if (this.formModel.mode === "edit") {
        return "编辑信息";
      }
      return "";
    },
    selectedRows() {
      return this.stores1.selection;
    },
    selectedRowsId() {
      return this.stores1.selection.map((x) => x.userInfoUuid);
    }, //删除
  },
  methods: {
    //页面加载
    loadDispatchList() {
      townlist(this.stores.demo.query).then((res) => {
        this.stores.demo.data = res.data.data;
        this.stores.demo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.stores1.selection = selection;
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
      this.doCpcList();
    },
    //显示条数改变
    handlePageSizeChanged1(pageSize) {
      this.stores1.demo.query.pageSize = pageSize;
      this.doCpcList();
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
      this.doCpcList();
    },
    //刷新
    handleRefresh() {
      this.loadDispatchList();
    },
    //刷新
    handleRefresh1() {
      this.doCpcList();
    },
    onclose(){
      this.loadDispatchList();
    },
    //党员列表显示
    handleDetail(e) {
      this.stores1.opened = true;
      this.stores1.demo.query.kw = e.townName;
      this.doCpcList();
    },
    doCpcList() {
      CpcList(this.stores1.demo.query).then((res) => {
        this.stores1.demo.data = res.data.data;
        this.stores1.demo.query.totalCount = res.data.totalCount;
      });
    },
    formopen1() {
      this.formModel.opened = false;
      this.doCpcList();
    },
    formopen2() {
      this.formModel1.opened = false;
      this.doCpcList();
    },
    //党员列表显示
    handleDetail1(e) {
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(e.userInfoUuid);
    },
    //清空
    handleResetFormDispatch() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch"].resetFields();
    },
    //清空
    handleResetFormDispatch22() {
      // this.$refs["formdispatch"].resetFields();
      this.$refs["formdispatch22"].resetFields();
    },
    //右边删除（单个删除）
    handleDelete(row) {
      this.doDelete(row.userInfoUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteDepartment(ids).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.doCpcList();
          this.stores1.selection = [];
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
    //右上边删除
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.stores1.selection = [];
          this.doCpcList();
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
      this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      this.formModel.mode = "edit";
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.userInfoUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      CpcShow(id).then((res) => {
        if (res.data.code === 200) {
          let data = res.data.data[0];
          this.formModel.fields = data;
          this.formModel1.fields = data;
        }
      });
    },
    //保存按钮
    handleSubmitConsumable() {
      if(this.formModel.fields.realName!=""){
        let reg = /^([\u4e00-\u9fa5]){2,7}$/;
        if (!reg.test(this.formModel.fields.realName)) {
          this.$Message.warning("姓名不合法!");
          return;
        }
      }else{
        this.$Message.warning("姓名不能为空!");
        return;
      }
      if(this.formModel.fields.phone!="" && this.formModel.fields.phone!=null){
        let reg2 = /^[1][3,4,5,7,8][0-9]{9}$/;
        if (!reg2.test(this.formModel.fields.phone)) {
          this.$Message.warning("联系方式不合法!");
          return;
        }
      }
      if(this.formModel.fields.residence!=""){
        let reg3 = /^[^\s]*$/;
        if (!reg3.test(this.formModel.fields.residence)) {
          this.$Message.warning("家庭住址不合法!");
          return;
        }
      }else{
        this.$Message.warning("家庭住址不能为空!");
        return;
      }
      if(this.formModel.fields.birth==""){
          this.$Message.warning("出生日期不能为空!");
          return;
      }
      this.formModel.fields.town=this.stores1.demo.query.kw;
      if (this.formModel.mode === "create") {
        this.docreateDispatch();
      }
      if (this.formModel.mode === "edit") {
        this.doEditDispatch();
      }
    },
    //添加（保存）
    docreateDispatch() {
      CpcCreate(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.doCpcList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      if (
        this.formModel.fields.birth != "" &&
        this.formModel.fields.birth != null
      ) {
        this.formModel.fields.birth = new Date(
          Date.parse(new Date(this.formModel.fields.birth)) + 8 * 3600 * 1000
        );
      }
      if (
        this.formModel.fields.joinDate != "" &&
        this.formModel.fields.joinDate != null
      ) {
        this.formModel.fields.joinDate = new Date(
          Date.parse(new Date(this.formModel.fields.joinDate)) + 8 * 3600 * 1000
        );
      }
      CpcEdit(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.doCpcList();
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
      this.formimport.opened = true;
    },
    //下载模板
    handleimportmodel() {
      window.location.href =
        this.url + "UploadFiles/ImportBuidingModel/人员信息导入模板.xlsx";
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
        await UserInfoImport(this.exceldata).then((res) => {
          if (res.data.code == 200) {
            this.time = res.data.data.time;
            this.successmsg = res.data.data.successmsg;
            this.repeatmsg = res.data.data.repeatmsg;
            this.defaultmsg = res.data.data.defaultmsg;
            this.doCpcList();
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
        let data={ids:this.selectedRowsId.join(","),town:this.stores1.demo.query.kw};
      UserInfoExport(data).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.stores1.selection = [];
          window.location.href = config.baseUrl.dev + res.data.data;
        this.doCpcList();
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
  },
  mounted() {
    this.loadDispatchList(); //页面加载
  },
};
</script>
<style>
</style>