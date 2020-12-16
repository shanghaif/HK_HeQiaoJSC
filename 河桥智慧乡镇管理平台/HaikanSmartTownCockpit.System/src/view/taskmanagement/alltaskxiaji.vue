<template>
  <div>
    <Card>
      <dz-table
        :totalCount="stores.vote.query.totalCount"
        :pageSize="stores.vote.query.pageSize"
        @on-page-change="handlePageChanged"
        @on-page-size-change="handlePageSizeChanged"
      >
        <div slot="searcher">
          <section class="dnc-toolbar-wrap">
            <Row :gutter="16">
              <Col span="16">
                <Form inline @submit.native.prevent>
                  <!-- <FormItem>
		<Select style="width: 180px;" v-model="stores.vote.query.kwsszdgz" placeholder="所属重点工作" :clearable="true" filterable>
			<Option v-for="item in prioritydata" :key="item.priorityUUID" :value="item.priorityUUID" :title="item.priorityHeadlinelong">{{item.priorityHeadline}}</Option>
		</Select>
                  </FormItem>-->

                  <FormItem>
                    <Input
                      type="text"
                      style="width: 180px;"
                      :clearable="true"
                      v-model="stores.vote.query.kwbt"
                      placeholder="输入任务标题搜索..."
                    ></Input>
                  </FormItem>
                  <!-- <FormItem>
					  <Select v-model="stores.vote.query.kwfzr" :clearable="true" style="width: 180px;" placeholder="请选择负责人" filterable>
					  	<Option v-for="item in fuzerendata" :value="item.systemUserUUID" :key="item.systemUserUUID">{{item.realName}}</Option>
					  </Select>
                  </FormItem>-->

                  <!-- <FormItem>
              <Select v-model="stores.vote.query.zt" placeholder="状态" clearable>
                <Option value="0">进行中</Option>
				<Option value="1">审核中</Option>
				<Option value="2">已完成</Option>
              </Select>
                  </FormItem>-->
                  <!-- <FormItem>
              <Select v-model="stores.vote.query.yxj" placeholder="等级" clearable>
                <Option value="普通">普通</Option>
				<Option value="紧急">紧急</Option>
				<Option value="非常紧急">非常紧急</Option>
              </Select>
                  </FormItem>-->
                  <!-- <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.vote.query.kwstartime"
                      @on-change="stores.vote.query.kwstartime=$event"
                      format="yyyy-MM-dd"
                      placeholder="开始时间"
                    ></DatePicker>
                  </FormItem>
                  <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.vote.query.kwendtime"
                      @on-change="stores.vote.query.kwendtime=$event"
                      format="yyyy-MM-dd"
                      placeholder="结束时间"
                    ></DatePicker>
                  </FormItem>-->
                  <FormItem>
                    <Button
                      v-can="'search'"
                      icon="md-search"
                      type="primary"
                      @click="handleSearchs()"
                      title="查询"
                    >查询</Button>
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                    <Button
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
                >新增任务</Button>
              </Col>
            </Row>
          </section>
        </div>
        <Table
          slot="table"
          ref="tables"
          :border="false"
          size="small"
          :row-class-name="rowClassName"
          :highlight-row="true"
          :data="stores.vote.data"
          :columns="stores.columns"
          @on-selection-change="handleSelectionChange"
          @on-refresh="handleRefresh"
          @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged"
          @on-sort-change="handleSortChange"
        >
          <template slot-scope="{row,index}" slot="auditStatus">
            <span>{{getauditStatus(row)}}</span>
          </template>
          <template slot-scope="{ row, index }" slot="action">
            <Poptip
              confirm
              :transfer="true"
              title="确定删除?"
              @on-ok="handleDelete(row)"
              >
              <Tooltip placement="top" content="删除" :delay="1000" :transfer="true">
                <Button type="error" size="small" shape="circle" icon="md-trash"></Button>
              </Tooltip>
            </Poptip>
            <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
              <Button
                v-can="'show'"
                type="success"
                size="small"
                shape="circle"
                icon="md-search"
                @click="lookEwq(row)"
              ></Button>
            </Tooltip>
            <Tooltip placement="top" content="编辑" :delay="1000" :transfer="true">
              <Button
                v-can="'edit'"
                v-if="editquanxian(row)"
                type="primary"
                size="small"
                shape="circle"
                icon="md-create"
                @click="handleEdit(row)"
              ></Button>
            </Tooltip>
          </template>
          <template slot-scope="{ row, index }" slot="sort">
            <Tooltip placement="top" content="置顶" :delay="1000" :transfer="true">
              <Button
                type="error"
                size="small"
                shape="circle"
                icon="md-arrow-up"
                @click="upasc(row)"
              ></Button>
            </Tooltip>
            <Tooltip placement="top" content="取消置顶" :delay="1000" :transfer="true">
              <Button
                type="primary"
                size="small"
                shape="circle"
                icon="md-arrow-down"
                @click="updesc(row)"
              ></Button>
            </Tooltip>
          </template>
        </Table>
      </dz-table>
    </Card>

    <!-- 添加、修改的表单 -->
    <Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModel.fields" ref="formtask" :rules="formModel.rules" label-position="top">
        <Row :gutter="16">
          <Col span="12">
          <FormItem label="任务标题" prop="missionHeadline">
            <Input v-model="formModel.fields.missionHeadline" placeholder="请输入任务标题" />
          </FormItem>
          </Col>
          <Col span="12">
          <FormItem label="开始时间" prop="startTime">
						<Date-picker type="datetime" format="yyyy-MM-dd HH:mm" style="width:100%;" placeholder="请选择时间" @on-change="formModel.fields.startTime=$event"
						 :value="formModel.fields.startTime"></Date-picker>
					</FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <FormItem label="问题详情" prop="missionDescribe">
            <Input
              v-model="formModel.fields.missionDescribe"
              placeholder="问题详情"
              type="textarea"
              :rows="5"
            />
          </FormItem>
        </Row>

        <Row :gutter="16">
          <Col span="12">
            <FormItem label="科室/村庄" prop="administrativeOffice2">
              <!-- <Input v-model="formModel.fields.selectfuzename" @on-change="selectfuze" style="width: 150px;" placeholder="请选择负责人" :readonly="true"/>
              <i-button type="info" @click="selectfuze(rowid)">选择负责人</i-button>-->

              <Select
                v-model="formModel.fields.administrativeOffice2"
                placeholder="科室/村庄"
                filterable
                multiple
              >
                <Option v-for="item in keshidatalist" :value="item.value">{{item.label}}</Option>
              </Select>
              <!-- <Select
                v-model="formModel.fields.keshilist"
                multiple
                placeholder="科室"
                @on-change="keshichange()"
                filterable
                clearable
              >
                <Option v-for="item in keshidatalist" :value="item.value">{{item.label}}</Option>
              </Select> -->
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="等级" prop="priority">
              <Select v-model="formModel.fields.priority">
                <Option value="普通">普通</Option>
                <Option value="紧急">紧急</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
      </Form>

      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmit">提 交</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>

    <!-- 查看的表单 -->
    <Drawer
      title="查看任务"
      v-model="formModel2.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form
        :model="formModel2.fields"
        ref="formtasklook"
        :rules="formModel2.rules"
        label-position="top"
      >
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="任务标题" prop="missionHeadline">
              <Input
                v-model="formModel2.fields.missionHeadline"
                :title="formModel2.fields.missionHeadline"
                :readonly="true"
                placeholder="未知..."
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="等级" prop="priority">
              <Input v-model="formModel2.fields.priority" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="12">
            <FormItem label="逾期时间" prop="finishTime">
              <Input v-model="formModel2.fields.finishTime" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem label="科室/村庄" prop="keshiname">
              <Input v-model="formModel2.fields.keshiname" :readonly="true" placeholder="未知..." />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16">
          <Col span="50">
            <FormItem label="问题描述" prop="missionDescribe">
              <Input
                v-model="formModel2.fields.missionDescribe"
                placeholder="请输入任务描述"
                type="textarea"
                :rows="5"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="16" v-if="yijianshow">
          <Col span="50">
            <FormItem label="审核意见" prop="auditOpinion">
              <Input
                v-model="formModel2.fields.auditOpinion"
                placeholder="审核意见"
                type="textarea"
                :rows="5"
                :readonly="true"
              />
            </FormItem>
          </Col>
        </Row>
      </Form>
    </Drawer>

    <Drawer
      title="选择参与人"
      v-model="formModelcanyu.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelcanyu.fields" ref="formselect" label-position="top">
        <FormItem label="参与人" prop="participant">
          <Transfer
            :data="formModelcanyu.fields.participantleft"
            :target-keys="formModelcanyu.fields.participantright"
            :list-style="{width: '240px',height: '400px'}"
            :titles="['所有人员','选择的人员']"
            filterable
            @on-change="handleSchoolChange"
          ></Transfer>
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
      </div>
    </Drawer>

    <Drawer
      title="选择负责人"
      v-model="formModelfuze.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelfuze.fields" ref="formselect" label-position="top">
        <vxe-table
          ref="xTree"
          max-height="600"
          :loading="loading"
          :data="tableData"
          :tree-config="{children: 'children'}"
          :checkbox-config="{labelField: 'name'}"
          @checkbox-change="selectChangeEvent"
        >
          <vxe-table-column type="checkbox" field="name" title="姓名/科室名" width="400" tree-node></vxe-table-column>
          <vxe-table-column>
            <!-- <template v-slot:header="{ row }">
              <input v-model="filterName" type="type" placeholder="输入姓名/科室名查询" @keyup="searchEvent">
            </template>-->
          </vxe-table-column>
        </vxe-table>

        <Input
          v-model="this.formModel.fields.selectfuzename"
          :readonly="true"
          placeholder="点击上方列表选择人员"
          type="textarea"
          :rows="5"
        />

        <!-- <FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode4.fields.uuid" @on-change="binddata(formMode4.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>


            <FormItem label="负责人" prop="fuze">
            <Transfer
                :data="formModelfuze.fields.fuzeleft"
                :target-keys="formModelfuze.fields.fuzeright"
                :list-style="{width: '240px',height: '400px'}"
                :titles="['所有人员','选择的人员']"
                filterable
                @on-change="handleSchoolChange2"
              ></Transfer>
        </FormItem>-->
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelfuze.opened = false">确 定</Button>
      </div>
    </Drawer>

    <Drawer
      title="选择参与人"
      v-model="formModelcanyu.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <vxe-table
        ref="xTree"
        max-height="600"
        :loading="loading"
        :data="tableData"
        :tree-config="{children: 'children'}"
        :checkbox-config="{labelField: 'name', highlight: false}"
        @checkbox-change="selectChangeEventcanyu"
      >
        <vxe-table-column type="checkbox" field="name" title="姓名/科室名" width="400" tree-node></vxe-table-column>
        <vxe-table-column>
          <!-- <template v-slot:header="{ row }">
              <input v-model="filterName" type="type" placeholder="输入姓名/科室名查询" @keyup="searchEvent">
          </template>-->
        </vxe-table-column>
      </vxe-table>

      <Input
        v-model="this.formModel.fields.selectcanyurenname"
        :readonly="true"
        placeholder="点击上方列表选择人员"
        type="textarea"
        :rows="5"
      />

      <!-- <Form :model="formModelcanyu.fields" ref="formselect" label-position="top">

					<FormItem label="所属科室" prop="uuid">
						<Select v-model="formMode5.fields.uuid" @on-change="binddataid(formMode5.fields.uuid)" placeholder="所属科室" filterable>
							<Option v-for="item in keshidatalist" :value="item.priorityUUID" :title="item.priorityHeadline">{{item.priorityHeadline}}</Option>
						</Select>
					</FormItem>


				<FormItem label="参与人" prop="participant">
					<Transfer :data="formModelcanyu.fields.participantleft" :target-keys="formModelcanyu.fields.participantright"
					 :list-style="{width: '240px',height: '400px'}" :titles="['所有人员','选择的人员']" filterable @on-change="handleSchoolChange"></Transfer>
				</FormItem>
      </Form>-->
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
      </div>
    </Drawer>
  </div>
</template>
<style>
.ivu-table .demo-table-info-row td {
  background-color: #2db7f5;
  color: #fff;
}
.ivu-table .demo-table-error-row td {
  /* background-color: #ccc; */
  color: red;
}
.ivu-table .demo-table-error-row2 td {
  background-color: #ccc;
  /* color: red; */
}
.ivu-table .demo-table-error-row3 td {
  background-color: #ccc;
  color: red;
}
.ivu-table td.demo-table-info-column {
  background-color: #2db7f5;
  color: #fff;
}
.ivu-table .demo-table-info-cell-name {
  background-color: #2db7f5;
  color: #fff;
}
.ivu-table .demo-table-info-cell-age {
  background-color: #ff6600;
  color: #fff;
}
.ivu-table .demo-table-info-cell-address {
  background-color: #187;
  color: #fff;
}
</style>
<script>
import Vue from "vue";
import XEUtils from "xe-utils";
import DzTable from "_c/tables/dz-table.vue";
import config from "@/config";
import {
  depdata, //根据任务绑定科室
  userdata, //绑定人员信息
  selecehuibao, //查看汇报
  hblooks, //查看汇报详细
} from "@/api/taskpai/wofuzetask";
import {
  getdatalist,
  getpersonaldiary,
  getfuzeren,
  getcanyuren,
  loaddata,
  baocunedit,
  binddataok,
  create,
  deletelist,
  binddataok2,
  depadnuser, //树形控件加载数据
  updateIsascRole, //置顶
  updateIsdescRole, //取消置顶
} from "@/api/taskpai/taskapis";

import { keshidata,keshidata2 } from "@/api/taskpai/taskapis2";

export default {
  name: "rbac_user_page",
  components: {
    DzTable,
  },
  data() {
    return {
      tableData: [],
      keshidatalist: [], //科室
      filterName: "",
      loading: false,
      originData: [],
      xuanzeleft: [],
      shows: false,
      commands: {
        delete: { name: "delete", title: "删除" },
      },
      options3: {
        disabledDate(date) {
          return date && date.valueOf() < Date.now() - 86400000;
        },
      },
      panduanrowid: 0,
      yijianshow: false, //审核意见显示
      beizhushow: false, //备注显示
      prioritydata: [], //重点工作
      fuzerendata: [], //负责人
      approver: [], //审批人
      rowid: "", //选择行的id
      formModel: {
        opened: false,
        title: "创建任务",
        mode: "create",
        selection: [],
        fields: {
          missionUUID: "",
          priorityUUID: "", //所属重点工作
          missionHeadline: "", //任务标题
          principal: "", //负责人
          selectfuzename: "", //负责人
          startTime: "", //开始时间
          finishTime: "", //结束时间
          missionDescribe: "", //任务描述
          priority: "", //优先级
          manhour: 1, //计划工时
          approver: "", //审批人
          participant: "", //选择的参与人id
          selectcanyurenname: "", //选择的参与人姓名
          establishName: "", //创建人uuid
          establisthtruename: "", //创建人姓名
          establishTime: "", //创建时间
          auditStatus: "", //审核状态
          administrativeOffice: "", //下派科室
          missiontype:"",
          keshilist: [],
          administrativeOffice2:[]
        },
        rules: {
          missionHeadline: [
            { type: "string", required: true, message: "请输入任务标题" },
          ],
          startTime: [
            { type: "string", required: true, message: "请选择开始时间" },
          ],
          missionDescribe: [
            { type: "string", required: true, message: "请输入问题详情" },
          ],
          priority: [{ type: "string", required: true, message: "请选择等级" }],
          // administrativeOffice: [
          //   { type: "string", required: true, message: "选择下派科室" },
          // ],
          


          //   startTime:[{type: "string", required: true, message: "请选择开始时间" }],
          //   finishTime:[{type: "string", required: true, message: "请选择结束时间" }],
          //   selectcanyurenname:[{type: "string", required: true, message: "请选择参与人" }],
          //approver:[{type: "string", required: true, message: "请选择审批人" }]
        },
      },
      formModel2: {
        opened: false,
        title: "查看任务",
        mode: "create",
        selection: [],
        fields: {
          missionUUID: "",
          priorityUUID: "", //所属重点工作
          missionHeadline: "", //任务标题
          principal: "", //负责人
          selectfuzename: "", //负责人
          startTime: "", //开始时间
          finishTime: "", //结束时间
          missionDescribe: "", //任务描述
          priority: "", //优先级
          manhourt: "", //计划工时
          approver: "", //审批人
          selectshenpiname: "",
          participant: "", //选择的参与人id
          selectcanyurenname: "", //选择的参与人姓名
          establishName: "", //创建人uuid
          establisthtruename: "", //创建人姓名
          establishTime: "", //创建时间
          administrativeOffice: "", //科室
        },
      },
      formModelcanyu: {
        opened: false,
        title: "选择人员",
        mode: "select",
        fields: {
          participantleft: [],
          participantright: [],
        },
      },
      formModelfuze: {
        opened: false,
        title: "选择人员",
        mode: "select",
        fields: {
          fuzeleft: [],
          fuzeright: [],
        },
      },
      stores: {
        vote: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kwsszdgz: "",
            kwbt: "",
            kwfzr: "",
            kwstartime: "",
            kwendtime: "",
            canshu: "",
            zt: "",
            misstype: "xia",
            yxj: "",
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
        },
        columns: [
          { type: "selection", width: 50, key: "handle" },
          { title: "任务标题", key: "missionHeadline", ellipsis: true },
          { title: "任务等级", key: "priority" },
          //{ title: "所属重点工作", key: "priorityHeadline",ellipsis: true},
          { title: "状态", key: "auditStatus", slot: "auditStatus" },
          { title: "逾期时间", key: "finishTime" },
          { title: "下派部门", key: "administrativeOffice" },
          {
            title: "操作",
            align: "left",
            width: 150,
            className: "table-command-column",
            slot: "action",
          },
          // 	        {
          //     title: "是否置顶",
          //     align: "center",
          //     key: "handle",
          //     width: 150,
          //     className: "table-command-column",
          //     slot: "sort",
          //   },
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
        return "创建任务";
      }
      if (this.formModel.mode === "edit") {
        return "编辑任务";
      }
      return "";
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.id);
    },
  },

  //树形控件数据绑定
  created() {
    this.shuxingonload();
  },

  methods: {
    shuxingonload() {
      //this.tableData = window.MOCK_TREE_DATA_LIST
      depadnuser().then((res) => {
        //this.tableData=res.data;
        setTimeout(() => {
          this.loading = false;
          this.originData = res.data;
          this.handleSearch();
        }, 300);
      });
    },

    handleSearch() {
      let filterName = XEUtils.toString(this.filterName).trim();
      if (filterName) {
        let options = { children: "children" };
        let searchProps = ["name"];
        this.tableData = XEUtils.searchTree(
          this.originData,
          (item) =>
            searchProps.some(
              (key) => XEUtils.toString(item[key]).indexOf(filterName) > -1
            ),
          options
        );
        // 搜索之后默认展开所有子节点
        this.$nextTick(() => {
          this.$refs.xTree.setAllTreeExpand(true);
        });
      } else {
        this.tableData = this.originData;
      }
    },
    // 创建一个防反跳策略函数，调用频率间隔 500 毫秒
    searchEvent: XEUtils.debounce(
      function () {
        this.handleSearch();
      },
      500,
      { leading: false, trailing: true }
    ),

    //负责人选择
    selectChangeEvent({ records }) {
      var selename = ""; //姓名
      var seleuuid = ""; //uuid
      var seleid = ""; //id
      for (let i = 0; i < records.length; i++) {
        if (records[i].uuid) {
          selename += records[i].name + ",";
          seleuuid += records[i].uuid + ",";
          seleid += records[i].id + ",";
        }
      }

      this.formModel.fields.selectfuzename = selename;
      this.formModel.fields.principal = seleuuid;
    },

    //参与人选择
    selectChangeEventcanyu({ records }) {
      var selename = ""; //姓名
      var seleuuid = ""; //uuid
      var seleid = ""; //id
      for (let i = 0; i < records.length; i++) {
        if (records[i].uuid) {
          selename += records[i].name + ",";
          seleuuid += records[i].uuid + ",";
          seleid += records[i].id + ",";
        }
      }

      this.formModel.fields.selectcanyurenname = selename;
      this.formModel.fields.participant = seleid;
    },

    //打开选择负责人窗口
    selectfuze(rowid) {
      this.formModelfuze.opened = true;
      //修改
      if (rowid != "") {
        console.log(rowid);
        binddataok2({ id: rowid }).then((res) => {
          if (this.panduanrowid != rowid) {
            //判断是否是本行的数据
            //不是本行数据
            this.formModelfuze.fields.fuzeright = []; //清除上次选择的人员
          }
          //如果没有选择人员则显示数据库中的内容（表示选择了新行）
          if (this.formModelfuze.fields.fuzeright.length == 0) {
            if (res.data.data.length > 0) {
              for (let i = 0; i < res.data.data.length; i++) {
                //显示数据库保存的人员
                this.formModelfuze.fields.fuzeright = this.formModelfuze.fields.fuzeright.concat(
                  res.data.data[i].key
                );
              }
            }
          }
          this.panduanrowid = rowid; //将本行的id保存
        });
      }
    },

    //置顶、逾期的颜色
    rowClassName(row, index) {
      //判断置顶
      //   if (row.zhidingshijian.search("9999")!=-1) {
      //     return "demo-table-error-row2";
      //   }
      //判断逾期
      if (row.isouttime === "1") {
        return "demo-table-error-row";
      }
      //判断完成
      if (row.accomplish === "1") {
        return "demo-table-info-row";
      }

      return "";
    },

    //获取调度数据
    loadVoteinitiateList() {
      this.stores.vote.query.canshu = this.$route.query.data; //地址栏参数
      getdatalist(this.stores.vote.query).then((res) => {
        console.log("所有的数据");
        console.log(res);
        this.stores.vote.data = res.data.data;
        this.stores.vote.query.totalCount = res.data.totalCount;
      });
    },
    //***************************************************************************
    handleSortChange(column) {
      this.stores.query.sort.direction = column.order;
      this.stores.query.sort.field = column.key;
      this.loadVoteinitiateList();
    },
    //刷新
    handleRefresh() {
      this.loadVoteinitiateList();
    },
    //获取选择的行数据
    handleSelectionChange(selection) {
      this.formModel.selection = selection;
    },
    //翻页
    handlePageChanged(page) {
      this.stores.vote.query.currentPage = page;
      this.loadVoteinitiateList();
    },
    //显示条数改变
    handlePageSizeChanged(pageSize) {
      this.stores.vote.query.pageSize = pageSize;
      this.loadVoteinitiateList();
    },
    //搜索
    handleSearchs() {
      this.loadVoteinitiateList();
    },
    //点击添加打开窗口
    handleShowCreateWindow() {
      this.filterName = "";
      //this.shuxingonload();
      this.formModel.mode = "create"; //添加
      this.formModel.opened = true; //打开窗口
      this.formModel.fields.selectcanyurenname = "";
      this.formModel.fields.selectfuzename = "";
      this.$refs["formtask"].resetFields(); //清空填写的表单信息
      this.formModelcanyu.fields.participantright = []; //重新打开窗口，清空选择的人员信息
      this.formModel.fields.missiontype="1";//任务类型为下级上传
      this.formModel.fields.establishName = this.$store.state.user.userName; //创建人
    },
    //点击编辑打开窗口
    handleEdit(row) {
      this.filterName = "";
      //this.shuxingonload();
      this.formModel.mode = "edit"; //修改
      this.formModel.opened = true;
      this.rowid = row.id; //保存选择行的id
      this.formModelcanyu.fields.participantright = []; //重新打开窗口，清空选择的人员信息
      loaddata({ id: row.id }).then((res) => {
        this.formModel.fields = res.data.data;
        this.formModel.fields.keshilist = res.data.data.administrativeOffice.split(",");
        this.formModel.fields.administrativeOffice2=res.data.data.administrativeOffice.split(',');
      });
    },
    //重点工作下拉框
    // 	  loadpersonaldiary(){
    // 		  getpersonaldiary().then(res => {
    // 			this.prioritydata = res.data.data
    // /* 			console.log("重点工作数据")
    // 			console.log(this.prioritydata) */
    // 		});
    // 	  },
    //负责人和审批人下拉框
    loadfuzeren() {
      getfuzeren().then((res) => {
        this.approver = this.fuzerendata = res.data.data;
      });
    },
    //穿梭框
    loaselect() {
      getcanyuren().then((res) => {
        this.formModelcanyu.fields.participantleft = res.data.data;
      });
    },
    //打开选择参与人窗口
    selectcanyu(rowid) {
      this.formModelcanyu.opened = true;
      //修改
      if (rowid != "") {
        binddataok({ id: rowid }).then((res) => {
          if (this.panduanrowid != rowid) {
            //判断是否是本行的数据
            //不是本行数据
            this.formModelcanyu.fields.participantright = []; //清除上次选择的人员
          }
          //如果没有选择人员则显示数据库中的内容（表示选择了新行）
          if (this.formModelcanyu.fields.participantright.length == 0) {
            if (res.data.data.length > 0) {
              for (let i = 0; i < res.data.data.length; i++) {
                //显示数据库保存的人员
                this.formModelcanyu.fields.participantright = this.formModelcanyu.fields.participantright.concat(
                  res.data.data[i].key
                );
              }
            }
          }
          this.panduanrowid = rowid; //将本行的id保存
        });
      }
    },
    //选择参与人
    handleSchoolChange(newTargetKeys, direction, moveKeys) {
      this.formModelcanyu.fields.participantright = newTargetKeys;
      this.formModel.fields.selectcanyurenname = this.formModel.fields.participant =
        ""; //每次选择则参与人都重新赋值
      for (
        let i = 0;
        i < this.formModelcanyu.fields.participantright.length;
        i++
      ) {
        for (
          let j = 0;
          j < this.formModelcanyu.fields.participantleft.length;
          j++
        ) {
          if (
            this.formModelcanyu.fields.participantright[i] ===
            this.formModelcanyu.fields.participantleft[j].key
          ) {
            this.formModel.fields.selectcanyurenname +=
              this.formModelcanyu.fields.participantleft[j].label + ",";
            this.formModel.fields.participant +=
              this.formModelcanyu.fields.participantleft[j].key + ",";
          }
        }
      }
      /* 		  console.log("选择的人员")
		  console.log(this.selectcanyurenname) */
    },
    //点击保存
    handleSubmit() {
      let valid = this.validateForm();
      if (valid) {
        if (this.formModel.mode === "create") {
          //保存添加的内容
          this.formModel.fields.establisthtruename = this.$store.state.user.userGuid; //当前登录账号guid(数据库保存创建人)
          create(this.formModel.fields).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.formModel.opened = false;
              this.loadVoteinitiateList();
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        }
        if (this.formModel.mode === "edit") {
          //保存修改的内容
          baocunedit(this.formModel.fields).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.formModel.opened = false;
              this.loadVoteinitiateList();
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        }
      }
    },
    //验证表单信息
    validateForm() {
      let _valid = false;
      this.$refs["formtask"].validate((valid) => {
        if (!valid) {
          this.$Message.error("请完善表单信息");
        } else {
          _valid = true;
        }
      });
      return _valid;
    },
    //删除多条数据
    handleBatchCommand(command) {
      if (!this.selectedRowsId || this.selectedRowsId.length <= 0) {
        this.$Message.warning("请选择至少一条数据");
        return;
      }
      this.$Modal.confirm({
        title: "操作提示",
        content: "<p>确定要将所选任务删除吗?</p>",
        loading: true,
        onOk: () => {
          deletelist({ ids: this.selectedRowsId.join(",") }).then((res) => {
            if (res.data.code === 200) {
              this.$Message.success(res.data.message);
              this.loadVoteinitiateList();
              this.$Modal.remove(); //关闭提示框
            } else {
              this.$Message.warning(res.data.message);
            }
          });
        },
      });
    },
      //删除单条数据
    handleDelete(row) {
      /* console.log(row.id) */
	  deletelist({ ids: row.id}).then(res => {
	    if (res.data.code === 200) {
	      this.$Message.success(res.data.message);
	  		this.loadVoteinitiateList();		  
		  $Message.success("操作成功");

	    } else {
	      this.$Message.warning(res.data.message);
	    }
	  });
    },

    //查看
    lookEwq(row) {
      this.yijianshow = this.beizhushow = false;
      this.formModel2.opened = true;
      loaddata({ id: row.id }).then((res) => {
        console.log("查看");
        console.log(res);
        this.formModel2.fields = res.data.data;
        if (row.auditStatus != "0") {
          //已提交完成则显示备注
          this.beizhushow = true;
        }
        if (
          res.data.data.auditOpinion != "" &&
          res.data.data.auditOpinion != null
        ) {
          //显示审核信息
          this.yijianshow = true;
        }
      });
    },

    //选择下派科室
    keshichange() {
      this.formModel.fields.administrativeOffice = "";
      for (let i = 0; i < this.formModel.fields.keshilist.length; i++) {
        this.formModel.fields.administrativeOffice +=
          this.formModel.fields.keshilist[i] + ",";
      }
      console.log(this.formModel.fields.administrativeOffice);
    },

    //置顶
    upasc(params) {
      var sortord = "";
      updateIsascRole({
        sortord: sortord,
        guid: params.missionUuid,
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadVoteinitiateList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },
    //取消置顶
    updesc(params) {
      var sortord = "";
      updateIsdescRole({
        sortord: sortord,
        guid: params.missionUuid,
      }).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadVoteinitiateList();
        } else {
          this.$Message.warning(res.data.message);
        }
      });
    },

    //编辑按钮权限
    editquanxian(row){
      var show=true;
      if(row.accomplish=='1')
      {
        show=false;
      }
      return show;
    },

getauditStatus(row) {
      var auditStatusText = "待指派";
        switch (row.auditStatus) {
          case "0":
            auditStatusText = "办理中";
            break;
          case "1":
            auditStatusText = "审核中";
            break;
          case "2":
            auditStatusText = "已完成";
            break;
        }
      return auditStatusText;
    },
  },
  mounted() {
    this.loadVoteinitiateList(); //加载所有数据
    //   this.loadpersonaldiary();//重点工作下拉框
    this.loadfuzeren(); //负责人下拉框
    this.loaselect(); //穿梭框
    keshidata().then((res) => {
      this.keshidatalist = this.keshidatalistid = res.data.data;
      console.log("科室数据");
      console.log(this.keshidatalist);
    });
  },
};
</script>

<style>
</style>
