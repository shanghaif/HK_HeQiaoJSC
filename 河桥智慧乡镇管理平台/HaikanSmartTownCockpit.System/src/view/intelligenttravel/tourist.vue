<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.tourist.query.totalCount"
        :pageSize="stores.tourist.query.pageSize"
        :currentPage="stores.tourist.query.currentPage"
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
                      v-model="stores.tourist.query.kw"
                      placeholder="输入景区名称搜索..."
                      @on-search="handleSearchTourist()"
                    >
                      <Select
                        slot="prepend"
                        v-model="stores.tourist.query.sType"
                        @on-change="handleSearchTourist"
                        placeholder="统计类型"
                        style="width: 100px"
                      >
                        <Option
                          v-for="item in stores.tourist.sources.statisticsType"
                          :value="item.value"
                          :key="item.value"
                          >{{ item.text }}</Option
                        >
                      </Select>
                      <Select
                      v-if="stores.tourist.query.sType!=1"
                        slot="prepend"
                        v-model="stores.tourist.query.year"
                        @on-change="handleSearchTourist"
                        placeholder="年份"
                        style="width: 100px"
                      >
                        <Option
                          v-for="item in stores.tourist.sources.yearSources"
                          :value="item.value"
                          :key="item.value"
                          >{{ item.text }}</Option
                        >
                      </Select>
                      <Select
                      v-if="stores.tourist.query.sType!=1&&stores.tourist.query.sType!=2"
                        slot="prepend"
                        v-model="stores.tourist.query.month"
                        @on-change="handleSearchTourist"
                        placeholder="月份"
                        style="width: 100px"
                      >
                        <Option
                          v-for="item in stores.tourist.sources.month"
                          :value="item.value"
                          :key="item.value"
                          >{{ item.text }}</Option
                        >
                      </Select>
                    </Input>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <!--                  <Button-->
                  <!--                    v-can="'delete'"-->
                  <!--                    class="txt-danger"-->
                  <!--                    icon="md-trash"-->
                  <!--                    title="删除"-->
                  <!--                    @click="handleBatchCommand('delete')"-->
                  <!--                  ></Button>-->
                  <!--                  <Button-->
                  <!--                    class="txt-success"-->
                  <!--                    icon="md-redo"-->
                  <!--                    title="恢复"-->
                  <!--                    @click="handleBatchCommand('recover')"-->
                  <!--                  ></Button>-->

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
          :data="stores.tourist.data"
          :columns="stores.tourist.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          :row-class-name="rowClsRender"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
        >
          <template slot-scope="{ row, index }" slot="state">
            <span>{{ renderState(row.state) }}</span>
          </template>
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
                  v-can="'delete'"
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
  </div>
</template>
<script>
import DzTable from "_c/tables/dz-table.vue";
import Tables from "_c/tables";
import {
  getTouristList,
  // createTourist,
  // loadTourist,
  // editTourist,
  // deleteTourist,
  // batchCommand,
  // TouristImport,
  // TouristExport
} from "@/api/intelligenttravel/tourist";
import { globalvalidatePhone } from "@/global/validate";
import config from "@/config";
export default {
  name: "tourist",
  components: {
    Tables,
    DzTable,
  },
  data() {
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
        title: "创建停车场",
        mode: "create",
        selection: [],
        fields: {
          parkingLotName: "",
          parkingLotAddress: "",
          zchewei: "",
          ychewei: "",
          schewei: "",
          parkingLotsru: "",
        },
        rules: {
          parkingLotName: [
            {
              type: "string",
              required: true,
              message: "请输入名称",
              trigger: "blur",
            },
          ],
          parkingLotAddress: [
            {
              type: "string",
              required: true,
              message: "请输入位置",
              trigger: "blur",
            },
          ],
          zchewei: [
            {
              type: "string",
              required: true,
              message: "请输入总车位",
              trigger: "blur",
            },
          ],
          ychewei: [
            {
              type: "string",
              required: true,
              message: "请输入已用车位",
              trigger: "blur",
            },
          ],
          schewei: [
            {
              type: "string",
              required: true,
              message: "请输入剩余车位",
              trigger: "blur",
            },
          ],
          parkingLotsru: [
            {
              type: "string",
              required: true,
              message: "请输入收入",
              trigger: "blur",
            },
          ],
        },
      },
      stores: {
        tourist: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            sType: 1,
            month: 1,
            isDeleted: 0,
            year: new Date().getFullYear(),
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
            yearSources: [
              {
                value: new Date().getFullYear(),
                text: new Date().getFullYear(),
              },
              {
                value: new Date().getFullYear() - 1,
                text: new Date().getFullYear() - 1,
              },
              {
                value: new Date().getFullYear() - 2,
                text: new Date().getFullYear() - 2,
              },
            ],
            statisticsType: [
              { value: 1, text: "年统计" },
              { value: 2, text: "月统计" },
              { value: 3, text: "周统计" },
              { value: 4, text: "日统计" },
            ],
            month: [
              { value: 1, text: "1月" },
              { value: 2, text: "2月" },
              { value: 3, text: "3月" },
              { value: 4, text: "4月" },
              { value: 5, text: "5月" },
              { value: 6, text: "6月" },
              { value: 7, text: "7月" },
              { value: 8, text: "8月" },
              { value: 9, text: "9月" },
              { value: 10, text: "10月" },
              { value: 11, text: "11月" },
              { value: 12, text: "12月" },
            ],
            week:['first','second','third','fourth','fifth','sixth'],
          },
          columns: [],
          columns1: [
            { title: "景区名称", key: "scenicName", sortable: true },
            { title: "总人数", key: "sum", sortable: true },
            { title: "", key: "first", sortable: true },
            { title: "", key: "second", sortable: true },
            { title: "", key: "third", sortable: true },
            { title: "", key: "fourth", sortable: true },
            { title: "", key: "fifth", sortable: true },
            { title: "", key: "sixth", sortable: true },
            { title: "", key: "seventh", sortable: true },
            { title: "", key: "eighth", sortable: true },
            { title: "", key: "ninth", sortable: true },
            { title: "", key: "tenth", sortable: true },
          ],
          columns2: [
            // { type: "selection", width: 50, key: "handle" },
            { title: "景区名称", key: "scenicName", sortable: true },
            { title: "总人数", key: "sum", sortable: true },
            { title: "一月", key: "jan", sortable: true },
            { title: "二月", key: "feb", sortable: true },
            { title: "三月", key: "mar", sortable: true },
            { title: "四月", key: "apr", sortable: true },
            { title: "五月", key: "may", sortable: true },
            { title: "六月", key: "jun", sortable: true },
            { title: "七月", key: "jul", sortable: true },
            { title: "八月", key: "aug", sortable: true },
            { title: "九月", key: "sep", sortable: true },
            { title: "十月", key: "oct", sortable: true },
            { title: "十一月", key: "nov", sortable: true },
            { title: "十二月", key: "dec", sortable: true },
            // {
            //   title: "操作",
            //   fixed: "right",
            //   align: "center",
            //   width: 150,
            //   className: "table-command-column",
            //   slot: "action"
            // }
          ],
          columns3: [
            
          ],
          columns4: [
            // { type: "selection", width: 50, key: "handle" },
            { title: "景区名称", key: "scenicName", sortable: true },
            { title: "总人数", key: "sum", sortable: true },
            { title: "1日", key: "first", sortable: true },
            { title: "2日", key: "second", sortable: true },
            { title: "3日", key: "third", sortable: true },
            { title: "4日", key: "fourth", sortable: true },
            { title: "5日", key: "fifth", sortable: true },
            { title: "6日", key: "sixth", sortable: true },
            { title: "7日", key: "seventh", sortable: true },
            { title: "8日", key: "eighth", sortable: true },
            { title: "9日", key: "ninth", sortable: true },
            { title: "10日", key: "tenth", sortable: true },
            { title: "11日", key: "eleventh", sortable: true },
            { title: "12日", key: "twelfth", sortable: true },
            { title: "13日", key: "thirteenth", sortable: true },
            { title: "14日", key: "fourteenth", sortable: true },
            { title: "15日", key: "fifteenth", sortable: true },
            { title: "16日", key: "sixteenth", sortable: true },
            { title: "17日", key: "seventeenth", sortable: true },
            { title: "18日", key: "eighteenth", sortable: true },
            { title: "19日", key: "nineteenth", sortable: true },
            { title: "20日", key: "twentieth", sortable: true },
            { title: "21日", key: "twenty_first", sortable: true },
            { title: "22日", key: "twenty_second", sortable: true },
            { title: "23日", key: "twenty_third", sortable: true },
            { title: "24日", key: "twenty_fourth", sortable: true },
            { title: "25日", key: "twenty_fifth", sortable: true },
            { title: "26日", key: "twenty_sixth", sortable: true },
            { title: "27日", key: "twenty_seventh", sortable: true },
            { title: "28日", key: "twenty_eighth", sortable: true },
            { title: "29日", key: "twenty_ninth", sortable: true },
            { title: "30日", key: "thirtieth", sortable: true },
            { title: "31日", key: "thirty_first", sortable: true },
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
        parkingLotName: "",
        parkingLotAddress: "",
        zchewei: "",
        ychewei: "",
        schewei: "",
        parkingLotsru: "",
      },
    };
  },
  computed: {
    formTitle() {
      if (this.formModel.mode === "create") {
        return "新增停车场";
      }
      if (this.formModel.mode === "edit") {
        return "编辑停车场";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.parkingLotUuid);
    },
  },
  methods: {
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
    loadTouristList() {
      getTouristList(this.stores.tourist.query).then((res) => {
        console.log(res);

        
        if (this.stores.tourist.query.sType == 1) {
          let maxyear = res.data.data.maxYear;
          console.log(maxyear);
          for (let i = 0; i < 10; i++) {
            this.stores.tourist.columns1[i + 2].title = (maxyear - i*1);
          }
          console.log(this.stores.tourist.columns1);
          this.stores.tourist.columns = this.stores.tourist.columns1;
          this.stores.tourist.data = res.data.data.touristYearDatas;
          this.stores.tourist.query.totalCount = res.data.data.totalCount;
        }else if(this.stores.tourist.query.sType == 2){
          this.stores.tourist.columns = this.stores.tourist.columns2;
          this.stores.tourist.data=res.data.data.touristMonthDatas;
          this.stores.tourist.query.totalCount = res.data.data.totalCount;
        }else if(this.stores.tourist.query.sType == 3){
          this.stores.tourist.columns3=[{ title: "景区名称", key: "scenicName", sortable: true },
            { title: "总人数", key: "sum", sortable: true }];
          let columns=res.data.data.columns;
          console.log(columns);
          for(let i=0;i<columns.length;i++){
            this.stores.tourist.columns3.push({title: columns[i], key: this.stores.tourist.sources.week[i], sortable: true });
          }
          console.log(this.stores.tourist.columns3);
          this.stores.tourist.columns = this.stores.tourist.columns3;
          this.stores.tourist.data=res.data.data.touristWeekDatas;
          this.stores.tourist.query.totalCount = res.data.data.totalCount;
        }else if(this.stores.tourist.query.sType == 4){
          var days=res.data.data.days;
          this.stores.tourist.columns = this.stores.tourist.columns4.slice(0,days+2);
          this.stores.tourist.data=res.data.data.touristDayDatas;
          this.stores.tourist.query.totalCount = res.data.data.totalCount;
        }


        // this.stores.tourist.data = res.data.data;
        // this.stores.tourist.query.totalCount = res.data.totalCount;
      });
    },
    handleSearchTourist() {
      this.stores.tourist.query.currentPage = 1;
      this.loadTouristList();
    },
    handleRefresh() {
      this.stores.tourist.query.currentPage = 1;
      this.loadTouristList();
    },
    //创建，修改
    // handleSubmitPlan() {
    //   let valid = this.validateForm();
    //   if (valid) {
    //     if (this.formModel.mode === "create") {
    //       this.docreateTourist();
    //     }
    //     if (this.formModel.mode === "edit") {
    //       this.doEditPlan();
    //     }
    //   }
    // },
    // docreateTourist() {
    //   createTourist(this.formModel.fields).then(res => {
    //     if (res.data.code === 200) {
    //       this.$Message.success(res.data.message);
    //       this.handleCloseFormWindow();
    //       this.loadTouristList();
    //     } else {
    //       this.$Message.warning(res.data.message);
    //     }
    //   });
    // },
    // doEditPlan() {
    //   editTourist(this.formModel.fields).then(res => {
    //     if (res.data.code === 200) {
    //       this.$Message.success(res.data.message);
    //       this.handleCloseFormWindow();
    //       this.loadTouristList();
    //     } else {
    //       this.$Message.warning(res.data.message);
    //     }
    //   });
    // },
    // validateForm() {
    //   let _valid = false;
    //   this.$refs["formPlan"].validate(valid => {
    //     if (!valid) {
    //       this.$Message.error("请完善表单信息");
    //     } else {
    //       _valid = true;
    //     }
    //   });
    //   return _valid;
    // },
    //批量操作
    // handleBatchCommand(command) {
    //   if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
    //     this.$Message.warning("请选择至少一条数据");
    //     return;
    //   }
    //   this.$Modal.confirm({
    //     title: "操作提示",
    //     content:
    //       "<p>确定要执行当前 [" +
    //       this.commands[command].title +
    //       "] 操作吗?</p>",
    //     loading: true,
    //     onOk: () => {
    //       this.doBatchCommand(command);
    //     }
    //   });
    //   //addsystemlog("delete","删除年度市级招生方案列表");
    // },
    // doBatchCommand(command) {
    //   batchCommand({
    //     command: command,
    //     ids: this.selectedRowsId.join(",")
    //   }).then(res => {
    //     if (res.data.code === 200) {
    //       this.$Message.success(res.data.message);
    //       this.loadTouristList();
    //       this.formModel.selection = [];
    //     } else {
    //       this.$Message.warning(res.data.message);
    //     }
    //     this.$Modal.remove();
    //   });
    // },
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
    // handleDelete(row) {
    //   this.doDelete(row.parkingLotUuid);
    // },
    // doDelete(ids) {
    //   if (!ids) {
    //     this.$Message.warning("请选择至少一条数据");
    //     return;
    //   }
    //   deleteTourist(ids).then(res => {
    //     if (res.data.code === 200) {
    //       this.$Message.success(res.data.message);
    //       this.loadTouristList();
    //       this.formModel.selection = [];
    //     } else {
    //       this.$Message.warning(res.data.message);
    //     }
    //   });
    // },
    //控制弹出子窗体
    // handleOpenFormWindow() {
    //   this.formModel.opened = true;
    // },
    // handleCloseFormWindow() {
    //   this.formModel.opened = false;
    // },
    //编辑
    // handleEdit(row) {
    //   this.handleSwitchFormModeToEdit();
    //   this.handleResetFormRole();
    //   this.doLoadTourist(row.parkingLotUuid);
    // },
    //
    // handleShowCreateWindow() {
    //   this.handleSwitchFormModeToCreate();
    //   this.handleResetFormRole();
    // },
    // handleSwitchFormModeToCreate() {
    //   this.formModel.mode = "create";
    //   this.handleOpenFormWindow();
    // },
    // handleSwitchFormModeToEdit() {
    //   this.formModel.mode = "edit";
    //   this.handleOpenFormWindow();
    // },
    // handleResetFormRole() {
    //   this.formModel.fields = JSON.parse(JSON.stringify(this.initdatacopy));
    //   //this.$refs["formPlan"].resetFields();
    // },
    // doLoadTourist(guid) {
    //   loadTourist({ guid: guid }).then(res => {
    //     this.formModel.fields = res.data.data;
    //   });
    // },
    handlePageChanged(page) {
      this.stores.tourist.query.currentPage = page;
      this.loadTouristList();
    },
    handlePageSizeChanged(pageSize) {
      this.stores.tourist.query.pageSize = pageSize;
      this.loadTouristList();
    },
  },
  mounted() {
    this.loadTouristList();
  },
};
</script>
<style scoped>
</style>
