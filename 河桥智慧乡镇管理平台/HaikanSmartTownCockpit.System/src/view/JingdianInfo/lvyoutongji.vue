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
                  <div>
                    <p style="margin-left:20px" slot="title">旅游收入年度统计</p>
                  </div>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <Button
                  v-can="'yjexport'"
                  icon="ios-cloud-download-outline"
                  type="primary"
                  @click="handleExportCuisineyj('export')"
                  title="生成年度报表"
                >生成年度报表</Button>
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
          <template slot-scope="{ row, index }" slot="action"></template>
        </Table>
      </dz-table>
    </Card>
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import {
  GetList, //显示列表
  GetExport
} from "@/api/JingdianInfo/lvyoutongji";
import config from "@/config";
import { getToken } from "@/libs/util";
export default {
  name: "HqCommuna",
  components: {
    DzTable
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
          sources: {},
          //列表显示
          columns: [
            {
              title: "年份",
              key: "gjYear",
              minWidth: 120,
              sortable: true
            },
            {
              title: "第一季度",
              key: "giOne",
              minWidth: 150,
              sortable: true
            },
            {
              title: "第二季度",
              key: "gjTwo",
              minWidth: 120,
              sortable: true
            },
            {
              title: "第三季度",
              key: "gjThree",
              minWidth: 200,
              sortable: true
            },
            {
              title: "第四季度",
              key: "gjFour",
              minWidth: 120,
              sortable: true
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
          scenicName: "",
          scenicGrade: "",
          scenicTickets: "",
          scenicAddress: "",
          scenicRemarks: "",
          lon: "",
          lat: "",
          hqCommunalUuid: ""
        },
        rules: {
          scenicName: [
            { type: "string", required: true, message: "设施名不能为空" }
          ]
        }
      },
      formModel1: {
        opened: false,
        selection: [],
        fields: {
          scenicName: "",
          scenicGrade: "",
          scenicTickets: "",
          scenicAddress: "",
          scenicRemarks: "",
          lon: "",
          lat: "",
          hqCommunalUuid: ""
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
      return this.formModel.selection.map(x => x.hqCommunalUuid);
    } //删除
  },
  methods: {
    doCuisineExport(command) {
      console.log(this.selectedRowsId.join(","));
      GetExport(this.selectedRowsId.join(",")).then(res => {
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
    },
    //页面加载
    loadDispatchList() {
      GetList(this.stores.userinfo.query).then(res => {
        console.log(res);
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
    this.actionurl = config.baseUrl.dev + "api/v1/HqCommuna/HqCommuna/UpLoad";
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