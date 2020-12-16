<template>
  <div>
    <Card>
      <Form
        :model="formModel4.fields"
        ref="fromcreat1"
        :rules="formModel4.rules"
        label-position="top"
      >
        <dz-table
          class="carlist"
          :totalCount="formModel4.buslist.query.totalCount"
          :pageSize="formModel4.buslist.query.pageSize"
          @on-page-change="handlePageChanged2"
          @on-page-size-change="handlePageSizeChanged2"
        >
          <div slot="searcher">
            <section class="dnc-toolbar-wrap">
              <Row :gutter="16" style="margin-left: 4px">
                <Col span="16">
                  <Form inline @submit.native.prevent>
                    <FormItem>
                      <Input
                        style="width: 150px"
                        type="text"
                        search
                        :clearable="true"
                        v-model="formModel4.buslist.query.kw"
                        placeholder="操作类型"
                        @on-search="handleSearchDispatch()"
                      ></Input>
                      <DatePicker
                        type="date"
                        v-model="formModel4.buslist.query.kw1"
                        @on-change="formModel4.buslist.query.kw1 = $event"
                        format="yyyy-MM-dd"
                        placement="top"
                        placeholder="操作日期"
                        style="width: 200px"
                        :confirm="true"
                        :editable="false"
                        @on-ok="handleSearchDispatch()"
                      ></DatePicker>
                    </FormItem>
                  </Form>
                </Col>
                <Col span="8" class="dnc-toolbar-btns">
                  <ButtonGroup class="mr3">
                    <Button
                      icon="md-refresh"
                      title="刷新"
                      @click="handleRefresh"
                    ></Button>
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
            @on-select="BushandleSelect"
            @on-selection-change="BushandleSelectionChange"
            :data="formModel4.buslist.data"
            :columns="formModel4.buslist.columns"
            :row-class-name="rowClsRender_xxx"
          >
            <template slot-scope="{ row, index }" slot="state">
              <span>{{ CheckState(row.state) }}</span>
            </template>
            <template slot-scope="{ row, index }" slot="action">
              <!-- <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
                <Button
                  v-can="'busedit'"
                  type="primary"
                  size="small"
                  shape="circle"
                  icon="md-create"
                  @click="bushandleEdit(row)"
                ></Button>
              </Tooltip>-->
            </template>
          </Table>
        </dz-table>
      </Form>
    </Card>

    <Drawer
      :title="formTitle1"
      v-model="busEditModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
    >
      <Form
        :model="busEditModel.mobile"
        ref="BusDispatchDetail"
        label-position="left"
        :rules="busEditModel.rules"
      >
        <Row :gutter="16">
          <FormItem label="行业名称:" prop="industryName">
            <Input
              v-model="busEditModel.mobile.industryName"
              placeholder="请输入行业名称"
              style="width: 85%"
            />
          </FormItem>
        </Row>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="busClick"
          >保 存</Button
        >
        <Button
          style="margin-left: 30px"
          icon="md-close"
          @click="busEditModel.opened = false"
          >取 消</Button
        >
      </div>
    </Drawer>
  </div>
</template>

<script>
// import JSZip from "jszip";
//import FileSaver from "file-saver";
import DzTable from "_c/tables/dz-table.vue";

import { getLogList } from "@/api/rbac/log";
// import { loadDataSystemLog } from "@/api/rbac/systemlog";
import config from "@/config";
export default {
  name: "systemlog",
  components: {
    DzTable,
  },
  data() {
    return {
      userGuid: "",
      userName: "",
      panduanrowid: 0,
      rowid: "", //选择行的id
      usName: "",
      usGuid: "",
      shangjiUuid: "",

      commands: {
        delete: { name: "delete", title: "删除" },
      },

      //form保存参数
      formModel4: {
        selection: [],
        opened: false,
        title: "行业列表",
        buslist: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            isDelete: 0,
            status: -1,
            time: "",
            kw: "",
            kw1: "",
            sysUuid: "",
            ClientUUID: "",
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          sources: {},
          columns: [
            { type: "selection", width: 35, key: "systemLogUuid" },
            {
              title: "操作用户",
              key: "userName",
              minWidth: 80,
              maxWidth: 110,
            },
            {
              title: "操作内容",
              key: "operationContent",
              minWidth: 250,
            },
            {
              title: "操作时间",
              key: "operationTime",
              minWidth: 80,
              maxWidth: 130,
            },
            {
              title: "操作类型",
              key: "type",
              minWidth: 80,
              maxWidth: 90,
            },
            // {
            //   title: "操作",
            //   align: "center",
            //   width: 120,
            //   className: "table-command-column",
            //   slot: "action"
            // }
          ],
          data: [],
        },
        //自定义样式
        styles: {},
      },

      busEditModel: {
        opened: false,
        title: "修改行业",
        mode: "edit",

        mobile: {
          industryUuid: "",
          industryName: "",
        },
        rules: {
          industryName: [
            { type: "string", required: true, message: "请输入名称" },
          ],
        },
      },

      //...
      //用于提交及一些保持性数据
      stores: {
        //selection: [],
        //该实例对象相关数据
        contactPerson: {
          sources: {
            clientName: [
              {
                value: "",
                label: "",
              },
            ],
          },

          //一些自定义附件处理数据,用于下拉列表之类

          //提交请求参数
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            isDeleted: 0,
            status: -1,
            //自定义参数
            kw: "",
            kw1: "",
            kw2: "",
            //查询排序方式
            sort: [
              {
                direct: "DESC",
                field: "ID",
              },
            ],
          },
          //请求获得的数据集合,用于填入表格
          data: [],
          //一些自定义附件处理数据,用于下拉列表之类

          // url:'',

          //table的列项名称
          columns: [
            //选择框
            { type: "selection", width: 50, key: "systemLogUuid" },
            {
              title: "操作用户",
              key: "userName",
              minWidth: 100,
              sortable: true,
            },
            {
              title: "操作",
              align: "center",
              fixed: "right",
              width: 80,
              className: "table-command-column",
              slot: "action",
            },
          ],
          data: [],
        },
        xxx2: {},
        //...
      },
      //自定义样式
      styles: {},
    };
  },
  //计算属性,以名称命名的方法体
  computed: {
    formTitle1() {
      if (this.busEditModel.mode === "create") {
        return "添加行业信息";
      }
      if (this.busEditModel.mode === "edit") {
        return "编辑行业信息";
      }
      return "";
    },
    //删除
    selectRusRowsId() {
      return this.formModel4.selection.map((x) => x.systemLogUuid);
    },
  },
  //方法集合
  methods: {
    handleRefresh() {
      this.loadListAddress();
    },
    //加载表格数据
    loadListAddress() {
      getLogList(this.formModel4.buslist.query).then((res) => {
        console.log(res);
        this.formModel4.buslist.data = res.data.data;
        this.formModel4.buslist.query.totalCount = res.data.totalCount;
      });
    },

    //行业刷新
    loadBusList() {
      this.loadListAddress();
    },

    //清空
    handleResetFormDispatch() {
      this.$refs["formdispatccc2"].resetFields();
    },

    //非空验证提示
    validateDispatchForm() {
      let _valid = false;
      this.$refs["formdispatccc2"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },

    //清空
    handleResetFormDispatch1() {
      this.$refs["formCarDispatchDetail"].resetFields();
    },

    //打开窗口
    handleOpenFormWindow1() {
      this.formModel.opened = true;
    },
    //自动关闭窗口
    handleCloseFormWindow1() {
      this.formModel.opened = false;
    },

    //表格行样式
    rowClsRender_Address(row, index) {
      // if (row.isDeleted) {
      //   return "table-row-disabled";
      // }
      // return "";
    },

    //刷新
    handleRefresh_Address() {
      this.loadListAddress();
    },
    //行选框的改变
    handleSelect(selection, row) {},
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //排序改变(忽略?)
    handleSortChange(column) {
      this.stores.contactPerson.query.sort.direction = column.order;
      this.stores.contactPerson.query.sort.field = column.key;
      this.loadVoteinitiateList();
    },
    //搜索
    handleSearchDispatch() {
      this.loadListAddress();
    },
    //表格翻页
    handlePageChanged(page) {
      this.stores.contactPerson.query.currentPage = page;
      this.loadListAddress();
    },
    //表格页大小改变
    handlePageSizeChanged(pageSize) {
      this.stores.contactPerson.query.pageSize = pageSize;
      this.loadListAddress();
    },

    //行业添加
    handleBusCreateWindow() {
      this.busEditModel.opened = true;
      this.BusFormModeToCreate();
      this.handleBusDispatchDetail();
    },

    BusFormModeToCreate() {
      this.busEditModel.mode = "create";
    },
    //清空
    handleBusDispatchDetail() {
      this.$refs["BusDispatchDetail"].resetFields();
    },

    BusFormModeToEdit() {
      this.busEditModel.mode = "edit";
    },
    //行业修改
    bushandleEdit(row) {
      this.busEditModel.opened = true;
      this.BusFormModeToEdit();
      this.$refs["BusDispatchDetail"].resetFields();
      this.busEditModel.mobile.industryUuid = row.industryUuid;
      let data = row.industryUuid;
      IdunesyView(data).then((res) => {
        if (res.data.code == 200) {
          this.busEditModel.mobile = res.data.data[0];
        }
      });
    },
    //行业添加
    busClickCreate() {
      let data = this.busEditModel.mobile;
      IdunesyAdd(data).then((res) => {
        if (res.data.code == 200) {
          this.$Message.success("行业添加成功");
          this.busEditModel.opened = false;
          this.loadBusList();
        } else {
          this.$Message.warning("行业添加失败");
        }
      });
    },
    //行业修改保存
    busClickEdit() {
      let data = this.busEditModel.mobile;
      //console.log(data);
      IdunesyEdit(data).then((res) => {
        if (res.data.code == 200) {
          this.$Message.success("行业修改成功");
          this.busEditModel.opened = false;
          this.loadBusList();
        } else {
          this.$Message.warning("行业修改失败");
        }
      });
    },
    busClick() {
      if (this.busEditModel.mode === "create") {
        this.busClickCreate();
      }
      if (this.busEditModel.mode === "edit") {
        this.busClickEdit();
      }
    },
    BushandleSelect(selection, row) {},
    //多选
    BushandleSelectionChange(selection) {
      this.formModel4.selection = selection;
    },
    BushandleSelect1(selection, row) {},

    //行业右上边删除按钮
    handleBatchCommand(command) {
      if (!this.selectRusRowsId || this.selectRusRowsId.length <= 0) {
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
    //行业右上边删除
    doBatchCommand(command) {
      Batch({
        command: command,
        ids: this.selectRusRowsId.join(","),
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadBusList();
          this.formModel4.selection = [];
        } else {
          this.$Message.warning(res.data.message);
        }
        this.$Modal.remove();
      });
    },
    //翻页
    handlePageChanged2(page) {
      this.formModel4.buslist.query.currentPage = page;
      this.loadListAddress();
    },
    //显示条数改变
    handlePageSizeChanged2(pageSize) {
      this.formModel4.buslist.query.pageSize = pageSize;
      this.loadListAddress();
    },
    //翻页
    handlePageChanged4(page) {
      this.formModel4.buslist.query.currentPage = page;
      this.loadListAddress();
    },

    //显示条数改变
    handlePageSizeChanged4(pageSize) {
      this.formModel4.buslist.query.pageSize = pageSize;
      this.loadListAddress();
    },
    rowClsRender_xxx(row, index) {
      // if (row.isDeleted) {
      //   return "table-row-disabled";
      // }
      // return "";
    },
    //---------------第二个模型-------------------

    //----------------slot-----------------
    Is_xxx(type) {
      let rType = "未知";
      // switch(type){
      //   case 0:
      //     rType = "否";
      //     break;
      //   case 1:
      //     rType = "是";
      //     break;
      // }
      return rType;
    },
    CheckState(state) {
      if (state == 0) {
        return "否";
      }
      if (state == 1) {
        return "是";
      }
    },
    //------------------杂项---------------------
    //将日期转为YY-MM-DD hh:mm:ss字符串格式
    dateToString(date) {
      let year = date.getFullYear();
      let month = date.getMonth() + 1;
      let day = date.getDate();
      let hour = date.getHours();
      let minute = date.getMinutes();
      let second = date.getSeconds();
      return (
        year +
        "-" +
        month +
        "-" +
        day +
        " " +
        hour +
        ":" +
        minute +
        ":" +
        second
      );
    },
  },

  //页面加载时执行
  mounted() {
    this.loadListAddress();
  },
};
</script>
<style>
.demo-spin-col {
  height: 100px;
  position: absolute;
  left: 35%;
  top: 50%;
}

.demo-spin-icon-load {
  animation: ani-demo-spin 1s linear infinite;
}
</style>
