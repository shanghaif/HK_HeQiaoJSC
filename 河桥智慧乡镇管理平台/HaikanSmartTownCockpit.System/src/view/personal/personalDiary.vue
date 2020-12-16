<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.role.query.totalCount"
        :pageSize="stores.role.query.pageSize"
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
                      v-model="stores.role.query.kw"
                      placeholder="输入关键字搜索..."
                      @on-search="handleSearchRole()"
                    ></Input>
                  </FormItem>
                  <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.role.query.kwstartime"
                      @on-change="stores.role.query.kwstartime=$event"
                      format="yyyy-MM-dd"
                      placeholder="撰写时间"
                    ></DatePicker>
                  </FormItem>
                  <!-- <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.role.query.kwendtime"
                      @on-change="stores.role.query.kwendtime=$event"
                      format="yyyy-MM-dd"
                      placeholder="结束时间"
                    ></DatePicker>
                  </FormItem> -->
                  <FormItem>
                    <Button
                      v-can="'search'"
                      icon="md-search"
                      type="primary"
                      @click="handleSearchRole()"
                      title="查询"
                    >查询</Button>
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
                  title="新增任务"
                >新增日志</Button>
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
          :data="stores.role.data"
          :columns="stores.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{row,index}" slot="auditStatus">
            <span>{{getauditStatus(row.auditStatus)}}</span>
          </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip confirm :transfer="true" title="你确定要删除吗?" @on-ok="handleDelete(row)">
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="success"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleshow(row)"
              ></Button>
            </Tooltip>
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

    <!--添加编辑表单-->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="false"
      :styles="styles"
    >
      <Form :model="formModel.fields" ref="formRole" :rules="formModel.rules" label-position="left">
        <FormItem label="个人日志标题" prop="headline" label-position="left">
          <Input v-model="formModel.fields.headline" placeholder="请输入个人日志标题" />
        </FormItem>
        <FormItem label="撰写时间" prop="writeTime" label-position="left" style="width:50%">
			  <DatePicker
            type="datetime"			
            :value="formModel.fields.writeTime"
            @on-change="formModel.fields.writeTime=$event"
            format="yyyy-MM-dd HH:mm:ss"
            placeholder="请选择撰写时间"
          ></DatePicker>
		</FormItem>
        <FormItem label="内容" label-position="top">
          <Input type="textarea" v-model="formModel.fields.content" :rows="4" placeholder="个人日志内容" />
        </FormItem>
        <FormItem label="原附件">
          <Input v-model="formModel.fields.accessory" :readonly="true" />
        </FormItem>
        <FormItem label="选择新附件" prop="photograph">
          <input
            style="cursor:default;"
            class="ivu-input"
            type="file"
            @change="addImg1"
            ref="inputer1"
          />
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitRole">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <!--详情-->
    <Drawer
      title="详情"
      v-model="formMode2.opened"
      width="400"
      :mask-closable="false"
      :mask="false"
      :styles="styles"
    >
      <Form :model="formMode2.fields" ref="详情" label-position="left">
        <FormItem label="个人日志标题">
          <Input v-model="formMode2.fields.headline" :readonly="true" />
        </FormItem>
        <FormItem label="撰写时间">
          <Input v-model="formMode2.fields.writeTime" :readonly="true" />
        </FormItem>
        <FormItem label="内容" label-position="top">
          <Input type="textarea" v-model="formMode2.fields.content" :rows="4" :readonly="true" />
        </FormItem>
        <FormItem label="附件">
          <Button
            icon="ios-cloud-download-outline"
            shape="circle"
            size="small"
            type="primary"
            @click="handleimportmodel"
            title="下载"
            下载
          ></Button>
          <Input v-model="formMode2.fields.accessory" :readonly="true" />
        </FormItem>
        <FormItem label="创建时间">
          <Input v-model="formMode2.fields.establishTime" :readonly="true" />
        </FormItem>
        <FormItem label="创建人">
          <Input v-model="formMode2.fields.establishName" :readonly="true" />
        </FormItem>
      </Form>
    </Drawer>
  </div>
</template>

<script>
import DzTable from "_c/tables/dz-table.vue";
import config from "@/config";
import {
  getRoleList,
  createRole,
  loadRole,
  editRole,
  deleteRole,
  batchCommand,
  RegistPicture,
} from "@/api/Personal/personaldiary";
export default {
  url: "",
  name: "rbac_role_page",
  components: {
    DzTable,
  },
  data() {
    return {
      commands: {
        delete: {
          name: "delete",
          title: "删除",
        },
        recover: {
          name: "recover",
          title: "恢复",
        },
        forbidden: {
          name: "forbidden",
          title: "禁用",
        },
        normal: {
          name: "normal",
          title: "启用",
        },
      },
      //编辑
      formModel: {
        opened: false,
        title: "创建个人日志",
        mode: "create",
        selection: [],
        fields: {
          id: "",
          headline: "",
          writeTime: new Date(),
        },
        rules: {
          headline: [
            {
              type: "string",
              required: true,
              message: "请输入个人日志标题",
              min: 2,
            },
          ],
        },
      },
      accessoryas: "",
      //详情
      formMode2: {
        opened: false,
        title: "个人日志详情",
        mode: "show",
        selection: [],
        fields: {
          id: "",
        },
      },

      stores: {
        role: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kwstartime:"",//开始时间
            kwendtime:"",//结束时间
            isDeleted: 0,
            status: -1,
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          data: [],
          sources: {
            isDeletedSources: [
              {
                value: -1,
                text: "全部",
              },
              {
                value: 0,
                text: "正常",
              },
              {
                value: 1,
                text: "已删",
              },
            ],
            statusSources: [
              {
                value: -1,
                text: "全部",
              },
              {
                value: 0,
                text: "禁用",
              },
              {
                value: 1,
                text: "正常",
              },
            ],
            statusFormSources: [
              {
                value: 0,
                text: "禁用",
              },
              {
                value: 1,
                text: "正常",
              },
            ],
          },
        },
        //绑定数据
        columns: [
          {
            type: "selection",
            width: 50,
            key: "handle",
          },
          {
            title: "个人日志标题",
            key: "headline",
            ellipsis: true,
            tooltip: true,
          },
          {
            title: "撰写时间",
            ellipsis: true,
            tooltip: true,
            key: "writeTime",
          },
          {
            title: "创建者",
            key: "establishName",
          },
          {
            title: "操作",
            align: "center",
            key: "handle",
            width: 150,
            className: "table-command-column",
            slot: "action",
          },
        ],
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
        return "创建个人日志";
      }
      if (this.formModel.mode === "edit") {
        return "编辑个人日志";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.personalDiaryUuid);
    },
  },
  methods: {
    //文件上传
    addImg1(event) {
      this.imgname = ""; //清除选择的文件
      let inputDOM = this.$refs.inputer1;
      let formData = new FormData();
      let file = inputDOM.files[0];
      let extName = file.name
        .substring(file.name.lastIndexOf("."))
        .toLowerCase();
      if (extName == ".exe" || extName == ".EXE") {
        alert("文件格式不正确!");
        return false;
      }
      // 通过DOM取文件数据
      this.fil = inputDOM.files;
      for (let i = 0; i < this.fil.length; i++) {
        let size = Math.floor(this.fil[i].size / 1024);
        if (size > 2 * 1024 * 1024) {
          alert("请选择2M以内的文件！");
          return false;
        }
        formData.append("multipartFiles", this.fil[i], this.fil[i].name);
        RegistPicture(formData).then((res) => {
          if (res.data.code == "200") {
            this.$Message.success(res.data.msg);
            this.img1 = this.url + res.data.path;
            this.imgshow1 = true;
            this.imgcopy1 = res.data.path;
            this.imgname = res.data.path;
            this.formModel.fields.accessory = res.data.path;
          } else {
            this.$Message.warning(res.data.msg);
          }
        });
      }
    },
    //下载附件
    handleimportmodel() {
      window.location.href =
        this.url + "UploadFiles/PersonalDiary/" + this.accessoryas;
    },

    //查询数据列表
    loadRoleList() {
      getRoleList(this.stores.role.query).then((res) => {
        this.stores.role.data = res.data.data;
        // console.log(this.stores.role.data)
        this.stores.role.query.totalCount = res.data.totalCount;
      });
    },
    //打开创建个人日志右侧导航
    handleOpenFormWindow() {
      this.formModel.opened = true;
      this.formMode2.fields.content = "";
      this.$refs.inputer1.value = "";
    },
    //关闭创建个人日志右侧导航
    handleCloseFormWindow() {
      this.formModel.opened = false;
    },
    //创建个人日志页面
    handleSwitchFormModeToCreate() {
      this.formModel.mode = "create";
    },
    //编辑个人日志页面
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    //编辑个人日志页面
    handleEdit(params) {
      // console.log(params.personalDiaryUuid)
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadRole(params.personalDiaryUuid);
    },

    //详情个人日志页面
    handleshow(params) {
      this.handleSwitchFormModeToShow();
      this.handleResetFormRole();
      this.doLoadShow(params.personalDiaryUuid);
    },
    //详情个人日志页面
    handleSwitchFormModeToShow() {
      this.formMode2.mode = "show";
      this.handleOpenFormShow();
    },
    //打开详情个人日志右侧导航
    handleOpenFormShow() {
      this.formMode2.opened = true;
    },
    //详情绑定数据
    doLoadShow(personalDiaryUuid) {
      loadRole({
        guid: personalDiaryUuid,
      }).then((res) => {
        this.formMode2.fields = res.data.data;
        this.accessoryas = this.formMode2.fields.accessory;
      });
    },

    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //刷新
    handleRefresh() {
      this.loadRoleList();
    },
    //新增日志
    handleShowCreateWindow() {
	  this.formModel.fields.accessory="请选择新附件！";
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      this.handleResetFormRole();
    },

    //保存
    handleSubmitRole() {
      let valid = this.validateRoleForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          this.doCreateRole();
        }
        if (this.formModel.mode === "edit") {
          this.doEditRole();
        }
      }
    },

    handleResetFormRole() {
      this.formModel.fields.content = "";
      this.$refs["formRole"].resetFields();
    },

    //添加
    doCreateRole() {
      createRole(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.handleCloseFormWindow(); //关闭
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //编辑
    doEditRole() {
      editRole(this.formModel.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.handleCloseFormWindow(); //关闭
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },

    //提交非空验证
    validateRoleForm() {
      let _valid = false;
      this.$refs["formRole"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
          _valid = false;
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //绑定编辑数据
    doLoadRole(personalDiaryUuid) {
      loadRole({
        guid: personalDiaryUuid,
      }).then((res) => {
        this.formModel.fields = res.data.data;
        console.log("akjshxkb");
        console.log(res);
      });
    },
    handleDelete(params) {
      this.doDelete(params.personalDiaryUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deleteRole(ids).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
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
    },
    //批量操作
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.formModel.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    handleSearchRole() {
      this.loadRoleList();
    },
    rowClsRender(row, index) {
      if (row.isDeleted) {
        return "table-row-disabled";
      }
      return "";
    },
    //分页
    handlePageChanged(page) {
      this.stores.role.query.currentPage = page;
      this.loadRoleList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.role.query.pageSize = pageSize;
      this.loadRoleList();
    },
  },
  mounted() {
    this.loadRoleList();
    this.url = config.baseUrl.dev;
  },
};
</script>
