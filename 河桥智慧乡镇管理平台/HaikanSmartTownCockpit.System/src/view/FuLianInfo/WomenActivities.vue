<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.PromoInfo.query.totalCount"
        :pageSize="stores.PromoInfo.query.pageSize"
        :currentPage="stores.PromoInfo.query.currentPage"
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
                      v-model="stores.PromoInfo.query.kw"
                      placeholder="输入标题搜索..."
                      @on-search="handleSearchPromoInfo()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.PromoInfo.query.isDeleted"
                        @on-change="handleSearchPromoInfo"
                        placeholder="删除状态"
                        style="width:60px;"
                      >
                        <Option
                          v-for="item in stores.PromoInfo.sources.isDeletedSources"
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
                  title="新增妇联活动"
                >新增妇联活动</Button>
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
          :data="stores.PromoInfo.data"
          :columns="stores.PromoInfo.columns"
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

            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="warning"
                size="small"
                shape="circle"
                icon="md-search"
                @click="handleShow(row)"
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
            <FormItem label="标题" prop="title">
              <Input
                v-model="formModel.fields.title"
                placeholder="请输入标题"
                style="width: 400px"
                :maxlength="20"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="内容" prop="content">
              <Input
                v-model="formModel.fields.content"
                placeholder="请输入内容"
                style="width: 400px"
                type="textarea"
                :maxlength="200"
              />
            </FormItem>
          </Col>
        </Row>
     
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="活动时间" prop="releaseTime">
              <DatePicker
                type="date"
                v-model="formModel.fields.releaseTime"
                placeholder="请选择活动时间"
                :editable="false"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <fieldset>
          <legend class="legend">内容图片(推荐大小530*230)</legend>
          <Row style="padding: 15px">
            <div class="demo-upload-list" v-for="item in uploadList">
              <template v-if="item.status === 'finished'">
                <img :src="item.url" />
                <div class="demo-upload-list-cover">
                  <Icon type="ios-eye-outline" @click.native="handleView(item.url)"></Icon>
                  <Icon type="ios-trash-outline" @click.native="handleRemove(item.name)"></Icon>
                </div>
              </template>
              <template v-else>
                <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
              </template>
            </div>
            <Divider dashed />
            <Upload
              ref="upload"
              :show-upload-list="false"
              :default-file-list="defaultList"
              :on-success="showUpResult"
              :on-progress="toUpResult"
              :format="['jpg','jpeg','png']"
              :max-size="5120"
              :data="{fileSavePath:'PFrontsInfo'}"
              :on-format-error="handleFormatError"
              :on-exceeded-size="handleMaxSize"
              :before-upload="handleBeforeUpload"
              :headers="postheaders"
              type="drag"
              :action="actionurl"
              style="display: inline-block;width:58px;"
            >
              <div style="width: 58px;height:58px;line-height: 58px;">
                <Icon type="ios-camera" size="20"></Icon>
              </div>
            </Upload>
            <!--            <Modal title="查看图片" v-model="visible">-->
            <!--              <img :src="imgName" v-if="visible" style="width: 100%" />-->
            <!--            </Modal>-->
          </Row>
        </fieldset>
      </Form>

      <div style="margin-top: 100px">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitPlan">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <Drawer
      :title="'详情'"
      v-model="formModel1.opened"
      width="500"
      :mask-closable="true"
      :mask="true"
      :styles="styles"
    >
      <Form
        v-if="formModel1.opened"
        :model="formModel.fields"
        ref="formPlan"
        :rules="formModel.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="标题">
              <Input
                v-model="formModel.fields.title"
                placeholder="请输入标题"
                style="width: 400px"
                :maxlength="20"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>  
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="内容">
              <Input
                v-model="formModel.fields.content"
                placeholder="请输入内容"
                style="width: 400px"
                type="textarea"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="活动时间" prop="releaseTime">
              <DatePicker
                type="date"
                v-model="formModel.fields.releaseTime"
                :editable="false"
                :disabled="true"
                style="width: 400px"
              ></DatePicker>
            </FormItem>
          </Col>
        </Row>
        <fieldset>
          <legend class="legend">活动图片</legend>
          <Row style="padding: 15px">
            <div class="demo-upload-list" v-for="item in uploadList">
              <template v-if="item.status === 'finished'">
                <img :src="item.url" />
                <div class="demo-upload-list-cover">
                  <Icon type="ios-eye-outline" @click.native="handleView(item.url)"></Icon>
                  <!--                  <Icon type="ios-trash-outline" @click.native="handleRemove(item.name)"></Icon>-->
                </div>
              </template>
              <template v-else>
                <Progress v-if="item.showProgress" :percent="item.percentage" hide-info></Progress>
              </template>
            </div>
          </Row>
        </fieldset>
      </Form>
    </Drawer>
    <Modal title="查看图片" v-model="visible">
      <img :src="imgName" v-if="visible" style="width: 100%" />
    </Modal>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import Tables from "_c/tables";
import {
  getPromoInfoList,
  createPromoInfo,
  loadPromoInfo,
  editPromoInfo,
  batchCommand,
  deletePromoInfo,
} from "@/api/FuLianInfo/WomenActivitiesInfo";
import { deletetoFile } from "@/api/common/common";
import { getToken } from "@/libs/util";
import config from "@/config";
export default {
  name: "WomenActivities",
  components: {
    Tables,
    DzTable
  },
  data() {
    return {
      showdetails: false,
      details: "",
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },
      coveruploadList: [],
      coverdefaultList: [],
      coverloadingStatus: false,
      coverupdisabled: false,

      uploadList: [],
      defaultList: [],
      actionurl: config.baseUrl.dev + "api/v1/common/common/UpLoadPicture",
      postheaders: "",
      imgName: "",
      loadingStatus: false,
      updisabled: false,
      visible: false,

      formModel1: {
        opened: false
      },
      formModel: {
        opened: false,
        title: "创建妇联活动",
        mode: "create",
        selection: [],
        fields: {

          title: "",
          content: "",
          releaseTime: "",
          photo: "",

          state: 0
        },
        rules: {
          title: [
            {
              type: "string",
              required: true,
              message: "请输入标题",
              trigger: "blur"
            }
          ],
          content: [
            {
              type: "string",
              required: true,
              message: "请输入内容",
              trigger: "blur"
            }
          ],
          releaseTime: [
            {
              required: true,
              message: "请选择时间"
            }
          ],

        }
      },
      stores: {
        PromoInfo: {
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
              title: "标题",
              key: "title",
              tooltip: true,
              ellipsis: true
            },
            {
              title: "活动时间",
              key: "releaseTime",
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
        cover: "",
        title: "",
        content: "",
        address: "",
        price: "",
        photo: "",
        state: 0
      }
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增妇联活动";
      }
      if (this.formModel.mode === "edit") {
        return "编辑妇联活动";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map(x => x.womenActivitiesUuid);
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
        if (this.formModel.fields.photo != null) {
          if (this.formModel.fields.photo.length > 0) {
            this.formModel.fields.photo += ",";
          }
          this.formModel.fields.photo += response.data.fname;
        } else {
          this.formModel.fields.photo = response.data.fname;
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
      this.formModel.fields.photo = this.uploadList
        .map(x => x.fileName)
        .join(",");
      // deletetoFile({ filePath: file }).then(res => {
      //   if (res.data.code == "200") {
      //
      //   } else {
      //     this.uploadList = this.uploadList.filter(x => x.name != file);
      //     this.formModel.fields.photo = this.uploadList
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
    loadPromoInfoList() {
      getPromoInfoList(this.stores.PromoInfo.query).then(res => {
        this.stores.PromoInfo.data = res.data.data;
        this.stores.PromoInfo.query.totalCount = res.data.totalCount;
        console.log(res);
      });
    },
    handleSearchPromoInfo() {
      this.stores.PromoInfo.query.currentPage = 1;
      this.loadPromoInfoList();
    },
    handleRefresh() {
      this.stores.PromoInfo.query.currentPage = 1;
      this.loadPromoInfoList();
    },
    //创建，修改
    handleSubmitPlan() {
      let valid = this.validateForm();
      if (valid) {
        // if (this.coveruploadList.length >= 1) {
        //   this.$Message.warning("封面请只上传1张图片");
        //   return;
        // }
        // if (this.uploadList.length != 3) {
        //   this.$Message.warning("内容图片请上传3张图片");
        //   return;
        // }
        if (this.uploadList.length != 1) {
          this.$Message.warning("请只上传一张图片");
          return;
        }
        if (this.formModel.mode === "create") {
          this.docreatePromoInfo();
        }
        if (this.formModel.mode === "edit") {
          this.doEditPlan();
        }
      }
    },
    docreatePromoInfo() {
      createPromoInfo(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadPromoInfoList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    doEditPlan() {
      editPromoInfo(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.handleCloseFormWindow();
          this.loadPromoInfoList();
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
          this.loadPromoInfoList();
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
      this.doDelete(row.womenActivitiesUuid);
    },
    doDelete(ids) {
      if (!ids) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      deletePromoInfo(ids).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadPromoInfoList();
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
      this.doLoadPromoInfo(row.womenActivitiesUuid);
    },
    handleShow(row) {
      this.formModel1.opened = true;
      this.handleResetFormRole();
      this.doLoadPromoInfo(row.womenActivitiesUuid);
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
    doLoadPromoInfo(guid) {
      console.log(guid);
      loadPromoInfo({ guid: guid }).then(res => {
        //console.log(res);
        this.formModel.fields = res.data.data;
        if (res.data.data.photo != null && res.data.data.photo != "") {
          let list = res.data.data.photo.split(",");
          for (let i = 0; i < list.length; i++) {
            this.uploadList.push({
              url: config.baseUrl.dev + "UploadFiles/PFrontsInfo/" + list[i],
              status: "finished",
              name: "UploadFiles/PFrontsInfo/" + list[i],
              fileName: list[i]
            });
          }
        }
      });
    },
    handlePageChanged(page) {
      this.stores.PromoInfo.query.currentPage = page;
      this.loadPromoInfoList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.PromoInfo.query.pageSize = pageSize;
      this.loadPromoInfoList();
    },

  },
  mounted() {
    this.loadPromoInfoList();
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
