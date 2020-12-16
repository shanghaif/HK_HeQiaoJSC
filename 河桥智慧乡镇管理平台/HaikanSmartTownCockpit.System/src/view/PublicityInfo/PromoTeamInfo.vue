<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.PromoTeamInfo.query.totalCount"
        :pageSize="stores.PromoTeamInfo.query.pageSize"
        :currentPage="stores.PromoTeamInfo.query.currentPage"
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
                      v-model="stores.PromoTeamInfo.query.kw"
                      placeholder="输入姓名搜索..."
                      @on-search="handleSearchPromoTeamInfo()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.PromoTeamInfo.query.isDeleted"
                        @on-change="handleSearchPromoTeamInfo"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.PromoTeamInfo.sources.isDeletedSources"
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
                  title="新增"
                >新增</Button>
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
          :data="stores.PromoTeamInfo.data"
          :columns="stores.PromoTeamInfo.columns"
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

            <!-- <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleShow(row)"
              ></Button>
            </Tooltip>-->
          </template>
        </Table>
      </dz-table>
    </Card>
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="500"
      :mask-closable="true"
      :mask="true"
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
            <FormItem label="姓名" prop="teamCaptain">
              <Input
                v-model="formModel.fields.teamCaptain"
                placeholder="请输入姓名"
                style="width: 400px"
              />
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="出生日期" prop="birth">
              <DatePicker
                v-model="formModel.fields.birth"
                format="yyyy-MM-dd"
                type="date"
                placeholder="出生日期"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="面貌" prop="politics">
              <Input v-model="formModel.fields.politics" placeholder="请输入面貌" style="width: 400px" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="村别" prop="cunz">
              <Input v-model="formModel.fields.cunz" placeholder="请输入村别" style="width: 400px" />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="性别" prop="sex" label-position="right">
              <Select v-model="formModel.fields.sex" placeholder="性别">
                <Option
                  v-for="item in sexList"
                  :value="item.value"
                  :key="item.value"
                >{{ item.label }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import Tables from "_c/tables";
import {
  getPromoTeamInfoList,
  createPromoTeamInfo,
  loadPromoTeamInfo,
  editPromoTeamInfo,
  batchCommand,
  deletePromoTeamInfo,
  zhnedilx
} from "@/api/PublicityInfo/PromoTeamInfo";
import { deletetoFile } from "@/api/common/common";
import { getToken } from "@/libs/util";
import config from "@/config";
export default {
  name: "PromoTeamInfo",
  components: {
    Tables,
    DzTable
  },
  data() {
    return {
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
      showdetails: false,
      details: "",
      typeList: [],
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },

      formModel1: {
        opened: false
      },
      formModel: {
        opened: false,
        title: "创建",
        mode: "create",
        selection: [],
        fields: {
          teamCaptain: "",
          promoTeamUuid: "",
          birth: "",
          politics: "",
          cunz: "",
          sex: ""
        },
        rules: {
          teamCaptain: [
            {
              type: "string",
              required: true,
              message: "请输入姓名",
              trigger: "blur"
            }
          ]
        }
      },
      stores: {
        PromoTeamInfo: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            isDeleted: 0,
            state: -1,
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
              { value: 0, text: "保存中" },
              { value: 1, text: "已发布" }
            ]
          },
          columns: [
            { type: "selection", width: 50, key: "handle" },
            {
              title: "姓名",
              key: "teamCaptain",
              tooltip: true,
              ellipsis: true
            },
            {
              title: "村别",
              key: "cunz",
              tooltip: true,
              ellipsis: true
            },
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
        sex: 0,
        promoTeamUuid: "",
        cover: "",
        teamCaptain: "",
        title: "",
        politics: "",
        address: "",
        price: "",
        picture: "",
        state: 0
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增";
      }
      if (this.formModel.mode === "edit") {
        return "编辑";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.promoTeamUuid);
    }
  },
  methods: {
    async covershowUpResult(response, file, fileList) {
      this.coverloadingStatus = false;
      this.coverupdisabled = false;
      if (response.code == "200") {
        this.$Message.success(response.message);
        if (this.formModel.fields.cover != null) {
          if (this.formModel.fields.cover.length > 0) {
            this.formModel.fields.cover += ",";
          }
          this.formModel.fields.cover += response.data.fname;
        } else {
          this.formModel.fields.cover = response.data.fname;
        }
        await this.coveruploadList.push({
          url: config.baseUrl.dev + response.data.strpath.replace("\\", "/"),
          status: "finished",
          name: response.data.strpath,
          fileName: response.data.fname
        });
      } else {
        this.$Message.warning(response.message);
      }
    },
    covertoUpResult() {
      if (this.$refs.coverupload.fileList.length > 1) {
        this.$refs.coverupload.fileList.shift();
      }
      this.coverloadingStatus = true;
      this.coverupdisabled = true;
    },
    coverhandleFormatError(file) {
      this.$Notice.warning({
        title: "The file format is incorrect",
        desc: "文件: " + file.name + " 不是png,jpg"
      });
    },
    coverhandleMaxSize(file) {
      this.$Notice.warning({
        title: "Exceeding file size limit",
        desc: "文件 " + file.name + " 太大,超过5M."
      });
    },
    coverhandleBeforeUpload() {
      return true;
    },
    coverhandleRemove(file) {
      this.coveruploadList = this.coveruploadList.filter(x => x.name != file);
      this.formModel.fields.cover = this.coveruploadList
        .map(x => x.fileName)
        .join(",");
    },

    async showUpResult(response, file, fileList) {
      this.loadingStatus = false;
      this.updisabled = false;
      if (response.code == "200") {
        this.$Message.success(response.message);
        if (this.formModel.fields.picture != null) {
          if (this.formModel.fields.picture.length > 0) {
            this.formModel.fields.picture += ",";
          }
          this.formModel.fields.picture += response.data.fname;
        } else {
          this.formModel.fields.picture = response.data.fname;
        }
        await this.uploadList.push({
          url: config.baseUrl.dev + response.data.strpath.replace("\\", "/"),
          status: "finished",
          name: response.data.strpath,
          fileName: response.data.fname
        });
        // console.log(
        //   (this.$refs.upload.fileList[0].name = e.data.dataPath.split("\\")[1])
        // );
        // console.log(this.defaultfilelist);
        // if (this.departmentlist.length >= 1) {
        //   this.defaultfilelist = [
        //     { name: this.formModel.fields.name, url: e.data.path }
        //   ];
        // }
      } else {
        this.$Message.warning(response.message);
      }
    },
    toUpResult() {
      console.log(this.$refs.upload.fileList);
      //console.log(this.$refs.upload.fileList);
      if (this.$refs.upload.fileList.length > 1) {
        this.$refs.upload.fileList.shift();
        // this.$refs.upload.clearFiles();
        // this.$refs.upload.push({})
      }
      this.loadingStatus = true;
      this.updisabled = true;
    },
    handleFormatError(file) {
      this.$Notice.warning({
        title: "The file format is incorrect",
        desc: "文件: " + file.name + " 不是png,jpg"
      });
    },
    handleMaxSize(file) {
      this.$Notice.warning({
        title: "Exceeding file size limit",
        desc: "文件 " + file.name + " 太大,超过5M."
      });
    },
    handleBeforeUpload() {
      // const check = this.uploadList.length < 5;
      // if (!check) {
      //   this.$Notice.warning({
      //     title: "Up to five pictures can be uploaded."
      //   });
      // }
      // return check;
      return true;
    },
    handleView(name) {
      this.imgName = name;
      this.visible = true;
      console.log(this.imgName);
    },
    handleRemove(file) {
      // const fileList = this.$refs.upload.fileList;
      // this.$refs.upload.fileList.splice(fileList.indexOf(file), 1);
      this.uploadList = this.uploadList.filter(x => x.name != file);
      this.formModel.fields.picture = this.uploadList
        .map(x => x.fileName)
        .join(",");
      // deletetoFile({ filePath: file }).then(res => {
      //   if (res.data.code == "200") {
      //
      //   } else {
      //     this.uploadList = this.uploadList.filter(x => x.name != file);
      //     this.formModel.fields.picture = this.uploadList
      //       .map(x => x.fileName)
      //       .join(",");
      //     this.$Message.warning(res.data.message)
      //   }
      // });
    },

    renderState(state) {
      let _status = "保存中";
      switch (state) {
        case 0:
          _status = "保存中";
          break;
        case 1:
          _status = "已发布";
          break;
      }
      return _status;
    },
    loadPromoTeamInfoList() {
      getPromoTeamInfoList(this.stores.PromoTeamInfo.query).then(res => {
        this.stores.PromoTeamInfo.data = res.data.data;
        this.stores.PromoTeamInfo.query.totalCount = res.data.totalCount;
        console.log(res);
      });
    },
    handleSearchPromoTeamInfo() {
      this.stores.PromoTeamInfo.query.currentPage = 1;
      this.loadPromoTeamInfoList();
    },
    handleRefresh() {
      this.stores.PromoTeamInfo.query.currentPage = 1;
      this.loadPromoTeamInfoList();
    },
    //创建，修改
    handleSubmitPlan() {
      let valid = this.validateForm();
      if (valid) {
        if (this.coveruploadList.length >= 1) {
          this.$Message.warning("封面请只上传1张图片");
          return;
        }
        // if (this.uploadList.length != 3) {
        //   this.$Message.warning("面貌图片请上传3张图片");
        //   return;
        // }
        if (this.formModel.mode === "create") {
          this.docreatePromoTeamInfo();
        }
        if (this.formModel.mode === "edit") {
          this.doEditPlan();
        }
      }
    },
    docreatePromoTeamInfo() {
      createPromoTeamInfo(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadPromoTeamInfoList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doEditPlan() {
      editPromoTeamInfo(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadPromoTeamInfoList();
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
          this.loadPromoTeamInfoList();
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
      this.doDelete(row.promoTeamUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deletePromoTeamInfo(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadPromoTeamInfoList();
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
      this.doLoadPromoTeamInfo(row.promoTeamUuid);
    },
    handleShow(row) {
      this.formModel1.opened = true;
      this.handleResetFormRole();
      this.doLoadPromoTeamInfo(row.promoTeamUuid);
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
    handleSwitchFormModeToShow() {
      this.showdetails = true;
    },
    handleResetFormRole() {
      this.formModel.fields = JSON.parse(JSON.stringify(this.initdatacopy));
      this.uploadList = [];
      this.coveruploadList = [];
      //this.$refs["formPlan"].resetFields();
    },
    doLoadPromoTeamInfo(guid) {
      // console.log(111);
      // console.log(guid);
      loadPromoTeamInfo({ guid: guid }).then(res => {
        console.log(res.data.data);
        this.formModel.fields = res.data.data;
      });
    },
    handlePageChanged(page) {
      this.stores.PromoTeamInfo.query.currentPage = page;
      this.loadPromoTeamInfoList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.PromoTeamInfo.query.pageSize = pageSize;
      this.loadPromoTeamInfoList();
    },
    getzhnedilx() {
      zhnedilx().then(res => {
        console.log(res);
        this.typeList = res.data.data;
        console.log(this.typeList);
      });
    }
  },
  mounted() {
    this.loadPromoTeamInfoList();
    this.getzhnedilx();
  },
  created() {
    this.postheaders = {
      Authorization: "Bearer " + getToken()
      //Accept: "application/json, text/plain, */*"
    };
  }
};
</script>
<style scoped>
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
