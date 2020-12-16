<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.securitycodestatistic.query.totalCount"
        :pageSize="stores.securitycodestatistic.query.pageSize"
        :currentPage="stores.securitycodestatistic.query.currentPage"
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
                      v-model="stores.securitycodestatistic.query.kw"
                      placeholder="输入区域名称搜索..."
                      @on-search="handleSearchSecurityCodeStatistic()"
                    >
                      <!--                      <Select-->
                      <!--                        slot="prepend"-->
                      <!--                        v-model="stores.securitycodestatistic.query.isDeleted"-->
                      <!--                        @on-change="handleSearchSecurityCodeStatistic"-->
                      <!--                        placeholder="删除状态"-->
                      <!--                        style="width:60px;"-->
                      <!--                      >-->
                      <!--                        <Option-->
                      <!--                          v-for="item in stores.securitycodestatistic.sources.isDeletedSources"-->
                      <!--                          :value="item.value"-->
                      <!--                          :key="item.value"-->
                      <!--                        >{{item.text}}</Option>-->
                      <!--                      </Select>-->
<!--                      <Select-->
<!--                        slot="prepend"-->
<!--                        v-model="stores.securitycodestatistic.query.year"-->
<!--                        @on-change="handleSearchSecurityCodeStatistic"-->
<!--                        placeholder="年份"-->
<!--                        style="width:100px;"-->
<!--                      >-->
<!--                        <Option-->
<!--                          v-for="item in stores.securitycodestatistic.sources.yearSources"-->
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

                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
                <!--                <Button-->
                <!--                  v-can="'create'"-->
                <!--                  icon="md-create"-->
                <!--                  type="primary"-->
                <!--                  @click="handleShowCreateWindow"-->
                <!--                  title="新增安全码"-->
                <!--                >新增安全码</Button>-->
                <!--                <Button-->
                <!--                  v-can="'import'"-->
                <!--                  icon="ios-cloud-upload"-->
                <!--                  type="success"-->
                <!--                  @click="handleImport"-->
                <!--                  title="导入"-->
                <!--                >导入</Button>-->
                <!--                <Button-->
                <!--                  v-can="'export'"-->
                <!--                  icon="ios-cloud-download-outline"-->
                <!--                  type="primary"-->
                <!--                  @click="handleExportSecurityCodeStatistic('export')"-->
                <!--                  title="导出"-->
                <!--                >导出</Button>-->
                <!--                <Button-->
                <!--                  v-can="'yjexport'"-->
                <!--                  icon="ios-cloud-download-outline"-->
                <!--                  type="primary"-->
                <!--                  @click="handleExportSecurityCodeStatisticyj('export')"-->
                <!--                  title="一键导出"-->
                <!--                >一键导出</Button>-->
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
          :data="stores.securitycodestatistic.data"
          :columns="stores.securitycodestatistic.columns"
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
<!--            <Poptip confirm :transfer="true" title="确定要删除吗?" @on-ok="handleDelete(row)">-->
<!--              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">-->
<!--                <Button v-can="'delete'" type="error" size="small" shape="circle" icon="md-trash"></Button>-->
<!--              </Tooltip>-->
<!--            </Poptip>-->
<!--            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">-->
<!--              <Button-->
<!--                v-can="'edit'"-->
<!--                type="primary"-->
<!--                size="small"-->
<!--                shape="circle"-->
<!--                icon="md-create"-->
<!--                @click="handleEdit(row)"-->
<!--              ></Button>-->
<!--            </Tooltip>-->
            <Tooltip placement="top" content="查看" :delay="1000" :transfer="true">
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
          :title="'详情'"
          v-model="formModel.opened"
          width="1400"
          :mask-closable="false"
          :mask="false"
          :styles="styles"
        >
          <Card>
            <dz-table
              :totalCount="stores.securitycode.query.totalCount"
              :pageSize="stores.securitycode.query.pageSize"
              :currentPage="stores.securitycode.query.currentPage"
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
                            v-model="stores.securitycode.query.kw"
                            placeholder="输入区域/姓名搜索..."
                            @on-search="handleSearchSecuritycode()"
                          >
                            <Select
                              slot="prepend"
                              v-model="stores.securitycode.query.isDeleted"
                              @on-change="handleSearchSecuritycode"
                              placeholder="删除状态"
                              style="width:60px;"
                            >
                              <Option
                                v-for="item in stores.securitycode.sources.isDeletedSources"
                                :value="item.value"
                                :key="item.value"
                              >{{item.text}}</Option>
                            </Select>

                            <Select
                              slot="prepend"
                              v-model="stores.securitycode.query.state"
                              @on-change="handleSearchSecuritycode"
                              placeholder="删除状态"
                              style="width:60px;"
                            >
                              <Option
                                v-for="item in stores.securitycode.sources.isStateSources"
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
<!--                        <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>-->
                      </ButtonGroup>
                    </Col>
                  </Row>
                </section>
              </div>
              <Table
                slot="table"
                ref="tables1"
                :border="false"
                size="small"
                :highlight-row="true"
                :data="stores.securitycode.data"
                :columns="stores.securitycode.columns"
                @on-selection-change="handleSelectionChange"
                @on-refresh="handleRefresh"
                :row-class-name="rowClsRender"
                @on-page-change="handlePageChanged"
                @on-page-size-change="handlePageSizeChanged"
              >
                <template slot-scope="{row,index}" slot="state">
                  <span>{{renderState(row.state)}}</span>
                </template>
              </Table>
            </dz-table>
          </Card>

        </Drawer>

  </div>
</template>
<script>
  import DzTable from "_c/tables/dz-table.vue";
  import Tables from "_c/tables";
  import {
    getSecurityCodeStatisticList,
    getSecuritycodeList
  } from "@/api/safe/securitycodestatistic";
  import { globalvalidatePhone } from "@/global/validate"
  import config from "@/config";
  export default {
    name: "securitycodestatistic",
    components: {
      Tables,
      DzTable
    },
    data() {

      return {
        //导入
        url: config.baseUrl.dev,
        importdisable: false,
        isexitexcel:false,
        successmsg: "",
        repeatmsg: "",
        defaultmsg: "",
        formimport: {
          opened: false
        },

        commands: {
          export: { name: "export", title: "导出" },
          delete: { name: "delete", title: "删除" },
          recover: { name: "recover", title: "恢复" },
          forbidden: { name: "forbidden", title: "禁用" },
          normal: { name: "normal", title: "启用" }
        },
        formModel: {
          opened: false,
          title: "创建安全码",
          mode: "create",
          selection: [],
          fields: {
            parkingLotName:"",
            parkingLotAddress: "",
            zchewei:"",
            ychewei:"",
            schewei:"",
            parkingLotsru:""
          },
          rules: {
            parkingLotName: [{type: "string",required: true,message: "请输入名称",trigger:'blur'}],
            parkingLotAddress: [{type: "string",required: true,message: "请输入位置",trigger:'blur'}],
            zchewei: [{type: "string",required: true,message: "请输入总车位",trigger:'blur'}],
            ychewei: [{type: "string",required: true,message: "请输入已用车位",trigger:'blur'}],
            schewei: [{type: "string",required: true,message: "请输入剩余车位",trigger:'blur'}],
            parkingLotsru: [{type: "string",required: true,message: "请输入收入",trigger:'blur'}],
          }
        },
        stores: {
          securitycodestatistic: {
            query: {
              totalCount: 0,
              pageSize: 20,
              currentPage: 1,
              kw: "",
              isDeleted: 0,
              year:new Date().getFullYear(),
              status: -1,
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
              yearSources: [
                { value: new Date().getFullYear(), text: new Date().getFullYear() },
                { value: new Date().getFullYear()-1, text: new Date().getFullYear()-1 },
                { value: new Date().getFullYear()-2, text: new Date().getFullYear()-2 }
              ],
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "区域", key: "address", sortable: true },
              { title: "总人数", key: "count", sortable: true },
              { title: "已转移人数", key: "ycount", sortable: true },
              { title: "未转移人数", key: "wcount", sortable: true },
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
          },
          securitycode: {
            query: {
              totalCount: 0,
              pageSize: 20,
              currentPage: 1,
              kw: "",
              state:-1,
              isDeleted: 0,
              status: -1,
              address:"",
              sort: [
                {
                  direct: "DESC",
                  field: "ID"
                }
              ]
            },
            sources: {
              isStateSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "未扫码" },
                { value: 1, text: "已扫码" }
              ],
              isDeletedSources: [
                { value: -1, text: "全部" },
                { value: 0, text: "正常" },
                { value: 1, text: "已删" }
              ],
            },
            columns: [
              { type: "selection", width: 50, key: "handle" },
              { title: "区域", key: "address", sortable: true },
              { title: "姓名", key: "name" , sortable: true},
              { title: "状态", key: "state", sortable: true ,slot:"state"},
              { title: "负责人", key: "chargeName" , sortable: true},
              { title: "负责人电话", key: "chargePhone" , sortable: true},
              { title: "扫码时间", key: "scannTime" , sortable: true},
              // {
              //   title: "操作",
              //   fixed: "right",
              //   align: "center",
              //   width: 150,
              //   className: "table-command-column",
              //   slot: "action"
              // }
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
          parkingLotName:"",
          parkingLotAddress: "",
          zchewei:"",
          ychewei:"",
          schewei:"",
          parkingLotsru:""
        }
      };
    },
    computed: {
      formTitle() {
        if (this.formModel.mode === "create") {
          return "新增安全码";
        }
        if (this.formModel.mode === "edit") {
          return "编辑安全码";
        }
        return "";
      },
      selectedRows() {
        return this.formModel.selection;
      },
      selectedRowsId() {
        return this.formModel.selection.map(x => x.parkingLotUuid);
      }
    },
    methods: {
      renderState(state){
        let _state = "未知";
        switch (state) {
          case 0:
            _state="未扫码"
            break;
          case 1:
            _state="已扫码"
            break;
        }
        return _state;
      },
      loadSecurityCodeStatisticList() {
        getSecurityCodeStatisticList(this.stores.securitycodestatistic.query).then(res => {
          this.stores.securitycodestatistic.data = res.data.data;
          this.stores.securitycodestatistic.query.totalCount = res.data.totalCount;

        });
      },
      handleSearchSecurityCodeStatistic() {
        this.stores.securitycodestatistic.query.currentPage = 1;
        this.loadSecurityCodeStatisticList();
      },
      handleRefresh() {
        this.stores.securitycodestatistic.query.currentPage = 1;
        this.loadSecurityCodeStatisticList();
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


      //查看
      handleShow(row) {
        this.stores.securitycode.data = [];
        this.stores.securitycode.query.totalCount = 0;
        this.formModel.opened = true;
        this.stores.securitycode.query.address = row.address;
        this.loadSecuritycodeList();
      },
      loadSecuritycodeList() {
        getSecuritycodeList(this.stores.securitycode.query).then(res => {
          this.stores.securitycode.data = res.data.data;
          this.stores.securitycode.query.totalCount = res.data.totalCount;
          //console.log(this.stores.educaplan.data);
        });
      },
      handleSearchSecuritycode() {
        this.stores.securitycode.query.currentPage = 1;
        this.loadSecuritycodeList();
      },

      handlePageChanged(page) {
        this.stores.securitycodestatistic.query.currentPage = page;
        this.loadSecurityCodeStatisticList();
      },
      handlePageSizeChanged(pageSize) {
        this.stores.securitycodestatistic.query.pageSize = pageSize;
        this.loadSecurityCodeStatisticList();
      },
    },
    mounted() {
      this.loadSecurityCodeStatisticList();
    }
  };
</script>
<style scoped>
</style>
