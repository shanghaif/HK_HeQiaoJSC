<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.userinfo.query.totalCount"
        :pageSize="stores.userinfo.query.pageSize"
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
                      v-model="stores.userinfo.query.kw"
                      placeholder="请输入涉侨人员姓名"
                      @on-search="handleSearchDispatch()"
                    ></Input>
                  </FormItem>
                  <FormItem>
                    <Input
                      style="width: 150px;"
                      type="text"
                      search
                      :clearable="true"
                      v-model="stores.userinfo.query.kw1"
                      placeholder="请输入涉侨类型"
                      @on-search="handleSearchDispatch()"
                    ></Input>
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
          :data="stores.userinfo.data"
          :columns="stores.userinfo.columns"
          @on-select="handleSelect"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <!-- <template slot-scope="{row, index}" slot="auditState">
            <span>{{renderAuditState(row.auditState)}}</span>
          </template>-->
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
                @click="handleDetail(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
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
          <FormItem label="证件类型" prop="userIdCardType">
            <Input v-model="formModel.fields.userIdCardType" placeholder="证件类型" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="证件号码" prop="userIdCardNum">
            <Input v-model="formModel.fields.userIdCardNum" placeholder="证件号码" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="手机号" prop="phone">
            <Input v-model="formModel.fields.phone" placeholder="手机号" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="邮箱" prop="email">
            <Input v-model="formModel.fields.email" placeholder="邮箱" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="性别" prop="sex">
            <Select filterable v-model="formModel.fields.sex" style="width:180px" placeholder="性别">
              <Option
                v-for="item in stores.userinfo.sources.sexList"
                :value="item.value"
                :key="item.value"
              >{{ item.label }}</Option>
            </Select>
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="国外所在地" prop="gwaddress">
            <Input v-model="formModel.fields.gwaddress" placeholder="国外所在地" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="中国居住地" prop="cnaddress">
            <Input v-model="formModel.fields.cnaddress" placeholder="中国居住地" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="现居地址" prop="xjaddress">
            <Input v-model="formModel.fields.xjaddress" placeholder="现居地址" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="涉侨类型" prop="concerningType">
            <Input
              v-model="formModel.fields.concerningType"
              placeholder="归侨、侨眷、港澳同胞眷属、归国留学人员、海外留学人员眷属、华侨、港澳同胞及外籍华人"
            />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitConsumable">保 存</Button>
        <Button style="margin-left: 30px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <Drawer title="详情" v-model="formModel1.opened" width="600" :mask-closable="false" :mask="false">
      <Form :model="formModel1.fields" ref="formdispatch22" label-position="top">
        <Row :gutter="16">
          <FormItem label="姓名" prop="realName">
            <Input v-model="formModel1.fields.realName" placeholder="姓名" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="性别" prop="sex">
            <Input v-model="formModel1.fields.sex" placeholder="性别" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="证件类型" prop="userIdCardType">
            <Input v-model="formModel1.fields.userIdCardType" placeholder="证件类型" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="证件号码" prop="userIdCardNum">
            <Input v-model="formModel1.fields.userIdCardNum" placeholder="证件号码" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="手机号" prop="phone">
            <Input v-model="formModel1.fields.phone" placeholder="手机号" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="邮箱" prop="email">
            <Input v-model="formModel1.fields.email" placeholder="邮箱" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="国外所在地" prop="gwaddress">
            <Input v-model="formModel1.fields.gwaddress" placeholder="国外所在地" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="中国居住地" prop="cnaddress">
            <Input v-model="formModel1.fields.cnaddress" placeholder="中国居住地" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="现居地址" prop="xjaddress">
            <Input v-model="formModel1.fields.xjaddress" placeholder="现居地址" :readonly="true" />
          </FormItem>
        </Row>
        <Row :gutter="16">
          <FormItem label="涉侨类型" prop="concerningType">
            <Input
              v-model="formModel1.fields.concerningType"
              placeholder="归侨、侨眷、港澳同胞眷属、归国留学人员、海外留学人员眷属、华侨、港澳同胞及外籍华人"
              :readonly="true"
            />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button style="margin-left: 30px" icon="md-close" @click="formModel1.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      title="涉侨人员信息导入"
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
          title="涉侨人员信息导入模板下载"
        >涉侨人员信息导入模板下载</Button>
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
  HomeAddressList, //显示列表
  HomeAddressCreate, //新增
  HomeAddressfoGet, //获取选定信息
  HomeAddressEdit, //编辑
  batchCommand, //批量删除
  deleteDepartment, //单个删除
  HomeAddressImport, //导入
  HomeAddressExport //导出
} from "@/api/ConcerningQiaoInfo/ConcerningQiaoInfo";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "ConcerningQiaoInfo",
  components: {
    DzTable
  },
  data() {
    let validateName = (rule, value, callback) => {
      if (value !== "" && value !== null) {
        let reg = /^[^\s]+$/;
        if (!reg.test(value)) {
          callback(new Error(" 姓名不合法"));
        }
        callback();
      } else {
        callback(new Error("姓名不能为空"));
      }
      callback();
    };
    return {
      //导入
      url: config.baseUrl.dev,
      importdisable: false,

      successmsg: "",
      repeatmsg: "",
      defaultmsg: "",
      formimport: {
        opened: false
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
        export: { name: "export", title: "导出" }
      },
      inglist: [],
      model1: [],
      model2: [],
      stores: {
        userinfo: {
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
            ]
          },
          //列表显示
          columns: [
            { type: "selection", minwidth: 50, key: "concerningQiaoUuid" },
            {
              title: "姓名",
              key: "realName",
              minWidth: 100,
              sortable: true
            },
            {
              title: "证件类型",
              key: "userIdCardType",
              minWidth: 150,
              sortable: true
            },
            {
              title: "证件号码",
              key: "userIdCardNum",
              minWidth: 120,
              sortable: true
            },
            {
              title: "涉侨类型",
              key: "concerningType",
              minWidth: 120,
              sortable: true
            },

            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 100,

              className: "table-command-column",
              slot: "action"
            }
          ],
          data: []
        }
      },
      formModel: {
        opened: false,
        title: "创建申请",
        mode: "create",
        dFileName: "xxxx",
        selection: [],
        fields: {
          realName: "",
          userIdCardType: "",
          userIdCardNum: "",
          phone: "",
          email: "",
          gwaddress: "",
          sex: "",
          xjaddress: "",
          concerningType: "",
          concerningQiaoUuid: "",
          cnaddress: ""
        },
        rules: {
          address: [{ type: "string", required: true, validator: validateName }]
        }
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          realName: "",
          userIdCardType: "",
          userIdCardNum: "",
          phone: "",
          email: "",
          gwaddress: "",
          sex: "",
          xjaddress: "",
          concerningType: "",
          concerningQiaoUuid: "",
          cnaddress: ""
        }
      }
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
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.concerningQiaoUuid);
    } //删除
  },
  methods: {
    handleToken() {
      console.log(9898989);
      btnToken().then(res => {
        console.log(res);
      });
    },
    //页面加载
    loadDispatchList() {
      HomeAddressList(this.stores.userinfo.query).then(res => {
        console.log(res.data.data);
        this.stores.userinfo.data = res.data.data;
        this.stores.userinfo.query.totalCount = res.data.totalCount;
      });
    },
    handleSelect(selection, row) {},
    //多选
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.userinfo.query.currentPage = page;
      this.loadDispatchList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.userinfo.query.pageSize = pageSize;
      this.loadDispatchList();
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
    //刷新
    handleRefresh() {
      this.loadDispatchList();
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
      this.doDelete(row.concerningQiaoUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteDepartment(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
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
        }
      });
    },
    //右上边删除
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadDispatchList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },

    xz(e) {
      this.stores.userinfo.query.kw = e;
      this.loadDispatchList();
    },
    //添加按钮
    handleShowCreateWindow() {
      this.formModel.mode = "create";
      this.handleResetFormDispatch(); //清空表单
      this.formModel.opened = true;
    },
    //右边编辑
    handleEdit(row) {
      console.log(row);
      this.formModel.mode = "edit";
      this.formModel.opened = true;
      this.handleResetFormDispatch(); //清空表单
      this.doLoadData(row.concerningQiaoUuid);
    },
    //右边详情
    handleDetail(row) {
      this.formModel1.opened = true;
      this.handleResetFormDispatch22(); //清空表单
      this.doLoadData(row.concerningQiaoUuid);
    },
    //查询当前行信息
    doLoadData(id) {
      HomeAddressfoGet(id).then(res => {
        if (res.data.code === 200) {
          console.log(res.data.data);
          let data = res.data.data[0];
          this.formModel.fields = data;
          this.formModel1.fields = data;
        }
      });
    },
    validateUserForm() {
      let _valid = false;
      this.$refs["formdispatch"].validate(valid => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //保存按钮
    handleSubmitConsumable() {
      let valid = this.validateUserForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.docreateDispatch();
        }
        if (this.formModel.mode === "edit") {
          this.doEditDispatch();
        }
      }
    },
    //添加（保存）
    docreateDispatch() {
      // if (this.formModel.fields.address != "") {
      //   let reg = /^[^\s]+$/;
      //   if (!reg.test(this.formModel.fields.address)) {
      //     this.$Message.warning("地址不合法!");
      //     return;
      //   }
      // } else {
      //   this.$Message.warning("地址不能为空!");
      //   return;
      // }
      HomeAddressCreate(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑（保存）
    doEditDispatch() {
      // if (this.formModel.fields.address != "") {
      //   let reg = /^[^\s]+$/;
      //   if (!reg.test(this.formModel.fields.address)) {
      //     this.$Message.warning("地址不合法!");
      //     return;
      //   }
      // } else {
      //   this.$Message.warning("地址不能为空!");
      //   return;
      // }
      //this.datadeal();
      HomeAddressEdit(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.opened = false; //关闭表单
          this.loadDispatchList();
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
      console.log(this.url);
      window.location.href =
        this.url + "UploadFiles/ImportUserInfoModel/涉侨人员信息导入模板.xlsx";
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
        await HomeAddressImport(this.exceldata).then(res => {
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
        }
      });
    },
    doCuisineExport(command) {
      console.log(this.selectedRowsId.join(","));
      HomeAddressExport(this.selectedRowsId.join(",")).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.formModel.selection = [];
          console.log(res);
          window.location.href = config.baseUrl.dev + res.data.data;
          this.loadDispatchList();
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
        }
      });
    }
  },
  mounted() {
    this.loadDispatchList(); //页面加载
  },
  created() {
    this.postheaders = {
      Authorization: "Bearer " + getToken()
      //Accept: "application/json, text/plain, */*"
    };
    this.actionurl =
      config.baseUrl.dev + "api/v1/HomeAddress/HomeAddress/UpLoad";
  }
};
</script>
<style>
.file1:hover + .fileimg1:hover {
  transform: scale(1.01, 1.01);
  box-shadow: 0 0 3px #1783ba;
}
.fileimg1 {
  width: 300px;
  height: 169px;
  float: left;
  z-index: 2;
}

.demo-upload-list {
  display: inline-block;
  width: 120px;
  height: 120px;
  text-align: center;
  line-height: 120px;
  border: 1px solid transparent;
  border-radius: 4px;
  overflow: hidden;
  background: #fff;
  position: relative;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.2);
  margin-right: 4px;
}
.demo-upload-list img {
  width: 100%;
  height: 100%;
}
.demo-upload-list-cover {
  display: none;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.6);
}
.demo-upload-list:hover .demo-upload-list-cover {
  display: block;
}
.demo-upload-list-cover i {
  color: #fff;
  font-size: 20px;
  cursor: pointer;
  margin: 0 2px;
}
</style>