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
                      placeholder="请输入重点工作名称"
                      @on-search="handleSearchRole()"
                    ></Input>
                  </FormItem>
                  <FormItem>
                    <Select
                      v-model="stores.role.query.kwfzr"
                      :clearable="true"
                      style="width: 180px;"
                      placeholder="请选择负责人"
                      filterable
                    >
                      <Option
                        v-for="item in fuzerendata"
                        :value="item.systemUserUUID"
                      >{{item.realName}}</Option>
                    </Select>
                  </FormItem>
                  <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.role.query.kwendtime"
                      @on-change="stores.role.query.kwendtime=$event"
                      format="yyyy-MM-dd"
                      placeholder="开始时间"
                    ></DatePicker>
                  </FormItem>
                  <FormItem>
                    <DatePicker
                      type="date"
                      style="width: 180px;"
                      :value="stores.role.query.kwendtime2"
                      @on-change="stores.role.query.kwendtime2=$event"
                      format="yyyy-MM-dd"
                      placeholder="结束时间"
                    ></DatePicker>
                  </FormItem>
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
                  title="新增重点工作"
                >新增重点工作</Button>
              </Col>
            </Row>
          </section>
        </div>

        <Table
          slot="table"
          ref="tables"
          :row-class-name="rowClassName"
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
            <Poptip confirm :transfer="true" title="你确定要删除了吗?" @on-ok="handleDelete(row)">
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
        <FormItem label="重点工作标题" prop="priorityHeadline" label-position="left">
          <Input v-model="formModel.fields.priorityHeadline" placeholder="请输入重点工作志标题" />
        </FormItem>
        <FormItem label="重点工作描述" label-position="top">
          <Input
            type="textarea"
            v-model="formModel.fields.priorityDescribe"
            :rows="4"
            placeholder="请输入重点工作描述"
          />
        </FormItem>
        <FormItem label="负责人" prop="principal">
          <Input
            v-model="formModel.fields.principalname"
            @on-change="selectfuze"
            style="width: 150px;"
            placeholder="请选择负责人"
            :readonly="true"
          />
          <i-button type="info" @click="selectfuze(rowid)">选择负责人</i-button>
        </FormItem>
        <FormItem label="参与人" prop="participant">
          <Input
            v-model="formModel.fields.participant"
            @on-change="selectcanyu"
            style="width: 150px;"
            placeholder="请选择参与人"
            :readonly="true"
          />
          <i-button type="info" @click="selectcanyu(rowid)">选择参与人</i-button>
        </FormItem>
        <FormItem label="结束时间" prop="endTime">
          <Date-picker
            type="datetime"
            format="yyyy-MM-dd HH:mm"
            placeholder="请选择时间"
            :options="options3"
            @on-change="formModel.fields.endTime=$event"
            :value="formModel.fields.endTime"
          ></Date-picker>
        </FormItem>
        <FormItem label="排序字段" prop="sortord" label-position="left" v-show="false">
          <InputNumber :max="999999" :min="1" v-model="formModel.fields.sortord"></InputNumber>
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitRole">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>
    <!--负责人穿梭框-->
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
		  :checkbox-config="{labelField: 'name', highlight: false}"
          @checkbox-change="selectChangeEvent">
		  <vxe-table-column type="checkbox" field="name" title="姓名/科室名" width="400" tree-node>
          </vxe-table-column>
		  <vxe-table-column>
            <!-- <template v-slot:header="{ row }">
              <input v-model="filterName" type="type" placeholder="输入姓名/科室名查询" @keyup="searchEvent">
            </template> -->
          </vxe-table-column>
        </vxe-table>

						<Input v-model="this.formModel.fields.principalname" :readonly="true" placeholder="点击上方列表选择人员" type="textarea" :rows="5" />







        <!-- <FormItem label="所属科室" prop="uuid">
          <Select
            v-model="formMode4.fields.uuid"
            @on-change="binddata(formMode4.fields.uuid)"
            placeholder="所属科室"
            filterable
          >
            <Option
              v-for="item in keshidatalist"
              :value="item.priorityUUID"
              :title="item.priorityHeadline"
            >{{item.priorityHeadline}}</Option>
          </Select>
        </FormItem>
        <FormItem label="负责人" prop="principal">
          <Transfer
            :data="formModelfuze.fields.participantleft"
            :target-keys="formModelfuze.fields.participantright"
            :list-style="{width: '240px',height: '400px'}"
            :titles="['所有人员','选择的人员']"
            filterable
            @on-change="handleSchoolChangefuze"
          ></Transfer>
        </FormItem> -->
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelfuze.opened = false">确 定</Button>
      </div>
    </Drawer>
    <!--参与人穿梭框-->
    <Drawer
      title="选择参与人"
      v-model="formModelcanyu.opened"
      width="600"
      :mask-closable="false"
      :mask="true"
      :styles="styles"
    >
      <Form :model="formModelcanyu.fields" ref="formselect" label-position="top">






		<vxe-table
          ref="xTree"
          max-height="600"
          :loading="loading"
          :data="tableData"
          :tree-config="{children: 'children'}"
		  :checkbox-config="{labelField: 'name', highlight: false}"
          @checkbox-change="selectChangeEventcanyu">
		  <vxe-table-column type="checkbox" field="name" title="姓名/科室名" width="400" tree-node>
          </vxe-table-column>
		  <vxe-table-column>
            <!-- <template v-slot:header="{ row }">
              <input v-model="filterName" type="type" placeholder="输入姓名/科室名查询" @keyup="searchEvent">
            </template> -->
          </vxe-table-column>
        </vxe-table>

						<Input v-model="this.formModel.fields.participant" :readonly="true" placeholder="点击上方列表选择人员" type="textarea" :rows="5" />








        <!-- <FormItem label="所属科室" prop="uuid">
          <Select
            v-model="formMode4.fields.uuid"
            @on-change="binddataid(formMode4.fields.uuid)"
            placeholder="所属科室"
            filterable
          >
            <Option
              v-for="item in keshidatalist"
              :value="item.priorityUUID"
              :title="item.priorityHeadline"
            >{{item.priorityHeadline}}</Option>
          </Select>
        </FormItem>
        <FormItem label="参与人" prop="participant">
          <Transfer
            :data="formModelcanyu.fields.participantleft"
            :target-keys="formModelcanyu.fields.participantright"
            :list-style="{width: '240px',height: '400px'}"
            :titles="['所有人员','选择的人员']"
            filterable
            @on-change="handleSchoolChange"
          ></Transfer>
        </FormItem> -->
      </Form>
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="formModelcanyu.opened = false">确 定</Button>
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
        <FormItem label="标题">
          <Input v-model="formMode2.fields.priorityHeadline" :readonly="true" />
        </FormItem>
        <FormItem label="负责人">
          <Input v-model="formMode2.fields.principalname" :readonly="true" />
        </FormItem>
        <FormItem label="描述" label-position="top">
          <Input
            type="textarea"
            v-model="formMode2.fields.priorityDescribe"
            :rows="4"
            :readonly="true"
          />
        </FormItem>
        <FormItem label="参与人">
          <Input v-model="formMode2.fields.participant" :readonly="true" />
        </FormItem>
        <FormItem label="排序字段" v-show="false">
          <Input v-model="formMode2.fields.sortord" :readonly="true" />
        </FormItem>
        <FormItem label="创建时间">
          <Input v-model="formMode2.fields.establishTime" :readonly="true" />
        </FormItem>
        <FormItem label="创建人">
          <Input v-model="formMode2.fields.establishName" :readonly="true" />
        </FormItem>
        <FormItem label="备注" label-position="top">
          <Input v-model="formMode2.fields.remark" :readonly="true" />
        </FormItem>
        <FormItem>
          <!-- <Button style="margin-left: 0px;" icon="md-close" @click="formMode2.opened = false">取 消</Button> -->
        </FormItem>
      </Form>
      <div class="demo-drawer-footer">
        <!-- <Button icon="md-checkmark-circle" type="primary" @click="dopriorityAccomplishRole">完 结</Button> -->
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
  /* background-color: #ff6600; */
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

	import Vue from 'vue'
    import XEUtils from 'xe-utils'
import DzTable from "_c/tables/dz-table.vue";
import config from "@/config";
import {
  establishListRole, //显示数据
  createRole, //创建
  loadRole, //编辑-显示数据
  editRole, //保存编辑
  deleteRole, //删除-标记
  batchCommand, //批量操作-标记
  RegistPicture,
  binddataok,
  binddataok2,
  priorityAccomplishRole, //完结
  getcanyuren, //穿梭框
  getfuzerenkuang, //负责人穿梭框
  getfuzeren, //下拉框
} from "@/api/EmphasisWork/PriorityWork";
import {
  keshidata,
  systemuserlistuuid,
  systemuserlistid,
  depadnuser,//树形控件加载数据 
} from "@/api/taskpai/taskapis";
export default {
  name: "rbac_role_page",
  components: {
    DzTable,
  },
  data() {
    return {


				tableData: [],
				filterName: '',
				loading: false,
        originData: [],





      panduanrowid: 0,
      rowid: "", //选择行的id
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" },
      },
      //编辑
      formModel: {
        opened: false,
        title: "创建重点工作",
        mode: "create",
        selection: [],
        fields: {
          id: "",
          sortord: null,
          priorityHeadline: "",
          principal: "", //负责人
          principalid: "", //负责人id
          endTime: "",
          participant: "", //参与人姓名
          participantid: "", //参与人id
          principalname: "", //参与人姓名
        },
        rules: {
          // priorityHeadline: [
          //   {
          //     type: "string",
          //     required: true,
          //     message: "请输入重点工作标题",
          //     min: 2,
          //   },
          // ],
          principal: [
            {
              type: "string",
              required: true,
              message: "请输入负责人",
              min: 2,
            },
          ],
          participant: [
            {
              type: "string",
              required: true,
              message: "请输入参与人",
              min: 2,
            },
          ],
          endTime: [
            {
              type: "string",
              required: true,
              message: "请选择结束时间",
            },
          ],
        },
      },
      //选择科室绑定数据
      formMode4: {
        fields: {
          id: "",
          uuid: "",
          name: "",
        },
      },
      keshidatalist: [],
      //详情
      formMode2: {
        opened: false,
        title: "重点工作详情",
        mode: "show",
        selection: [],
        fields: {
          id: "",
        },
      },

      fuzerendata: [], //负责人
      //选择负责人员
      formModelfuze: {
        opened: false,
        title: "选择负责人员",
        mode: "select",
        fields: {
          participantleft: [],
          participantright: [],
        },
      },
      //选择参与人员
      formModelcanyu: {
        opened: false,
        title: "选择人员",
        mode: "select",
        fields: {
          participantleft: [],
          participantright: [],
        },
      },

      stores: {
        role: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kwfzr: "",
            kwendtime: "",
            kwendtime2: "",
            isDeleted: 0,
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
            statusSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" },
            ],
            statusFormSources: [
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" },
            ],
          },
          data: [],
        },
        //绑定数据
        columns: [
          { type: "selection", width: 50, key: "handle" },
          {
            title: "名称",
            key: "priorityHeadline",
            ellipsis: true,
            tooltip: true,
          },
          { title: "负责人", ellipsis: true, tooltip: true, key: "princial" },
          { title: "完成进度", key: "unfinished" },
          { title: "创建时间", key: "establishTime" },
          { title: "结束时间", key: "endTime" },
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
        return "创建重点工作";
      }
      if (this.formModel.mode === "edit") {
        return "编辑重点工作";
      }
      return "";
    },
    selectedRows() {
      return this.formModel.selection;
    },
    selectedRowsId() {
      return this.formModel.selection.map((x) => x.priorityUuid);
    },
  },

//树形控件数据绑定
			created () {
				this.shuxingonload();
          },




  methods: {




shuxingonload(){
			//this.tableData = window.MOCK_TREE_DATA_LIST
			depadnuser().then(res => {
					//this.tableData=res.data;

					setTimeout(() => {
              this.loading = false
              this.originData = res.data
              this.handleSearch()
            }, 300)					
				});
},


handleSearch () {
			  let filterName = XEUtils.toString(this.filterName).trim()
              if (filterName) {
                let options = { children: 'children' }
                let searchProps = ['name']
                this.tableData = XEUtils.searchTree(this.originData, item => searchProps.some(key => XEUtils.toString(item[key]).indexOf(filterName) > -1), options)
                // 搜索之后默认展开所有子节点
                this.$nextTick(() => {
                  this.$refs.xTree.setAllTreeExpand(true)
                })
              } else {
                this.tableData = this.originData
              }
            },
            // 创建一个防反跳策略函数，调用频率间隔 500 毫秒
            searchEvent: XEUtils.debounce(function () {
              this.handleSearch()
			}, 500, { leading: false, trailing: true }),
			

			//负责人选择
			selectChangeEvent ({ records }) {
				
			  var selename="";//姓名
			  var seleuuid="";//uuid
			  var seleid="";//id
			  for(let i=0;i<records.length;i++)
			  {
				  if(records[i].uuid)
				  {
					  selename+=records[i].name+",";
					  seleuuid+=records[i].uuid+",";
					  seleid+=records[i].id+",";
				  }
			  }

			  this.formModel.fields.principalname=selename;
			  this.formModel.fields.principal=seleuuid;
			},
			
			//参与人选择
			selectChangeEventcanyu({ records }) {
				
			  var selename="";//姓名
			  var seleuuid="";//uuid
			  var seleid="";//id
			  for(let i=0;i<records.length;i++)
			  {
				  if(records[i].uuid)
				  {
					  selename+=records[i].name+",";
					  seleuuid+=records[i].uuid+",";
					  seleid+=records[i].id+",";
				  }
			  }

			  this.formModel.fields.participant=selename;
			  this.formModel.fields.participantid=seleid;
			},









    //科室的数据获取
    keshijiazai() {
      keshidata().then((res) => {
        this.keshidatalist = this.keshidatalistid = res.data.data;
      });
    },
    //选择科室重新绑定人员信息-负责人数据
    binddata(data) {
      systemuserlistuuid({ uuid: data }).then((res) => {
        this.formModelfuze.fields.participantleft = res.data.data;
      });
    },
    //选择科室重新绑定人员信息-参与人数据
    binddataid(data) {
      systemuserlistid({ uuid: data }).then((res) => {
        console.log("拉去科室获得的参与人数据");
        console.log(this.formModelcanyu.fields.participantleft);
        this.formModelcanyu.fields.participantleft = res.data.data;
      });
    },
    //逾期的颜色
    rowClassName(row, index) {
      if (row.accomplish === "2") {
        return "demo-table-error-row";
      }
      return "";
    },
    //查询数据列表
    loadRoleList() {
      establishListRole(this.stores.role.query).then((res) => {
        console.log(res);
        this.stores.role.data = res.data.data;
        this.stores.role.query.totalCount = res.data.totalCount;
      });
    },
    //打开创建重点工作右侧导航
    handleOpenFormWindow() {
      this.formModel.opened = true;
    },
    //关闭创建重点工作右侧导航
    handleCloseFormWindow() {
      this.formModel.opened = false;
    },
    //创建重点工作页面
    handleSwitchFormModeToCreate() {
      this.formModel.mode = "create";
    },
    //编辑重点工作页面
    handleSwitchFormModeToEdit() {
      this.formModel.mode = "edit";
      this.handleOpenFormWindow();
    },
    //编辑重点工作页面
    handleEdit(params) {
      this.rowid = params.id;
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadRole(params.id);
    },

    //详情重点工作页面
    handleshow(params) {
      this.handleSwitchFormModeToShow();
      this.handleResetFormRole();
      //this.doLoadShow(params);
      console.log("行ID");
      console.log(params.id);
      loadRole({ id: params.id }).then((res) => {
        this.formMode2.fields = res.data.data;
      });
    },
    //详情重点工作页面
    handleSwitchFormModeToShow() {
      this.formMode2.mode = "show";
      this.handleOpenFormShow();
    },
    //打开详情重点工作右侧导航
    handleOpenFormShow() {
      this.formMode2.opened = true;
    },
    //详情绑定数据
    // doLoadShow(params) {
    //   console.log("行ID");
    //   console.log(params.id);
    //   loadRole({ id: params.id }).then((res) => {
    //     this.formMode2.fields = res.data.data;
    //   });
    // },

    //完结
    dopriorityAccomplishRole() {
      priorityAccomplishRole(this.formMode2.fields).then((res) => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseF();
      });
    },
    //关闭创建重点工作右侧导航-详情
    handleCloseF() {
      this.formMode2.opened = false;
    },
    //负责人穿梭框
    loaselectfuze() {
      getfuzerenkuang().then((res) => {
        this.formModelfuze.fields.participantleft = res.data.data;
      });
    },
    //参与人穿梭框
    loaselect() {
      getcanyuren().then((res) => {
        this.formModelcanyu.fields.participantleft = res.data.data;
      });
    },
    //打开选择负责人窗口
    selectfuze(rowid) {
      this.formModelfuze.opened = true;
      // this.formModel.fields.participant = this.formModel.fields.participant;
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
                this.formModelfuze.fields.participantright = this.formModelfuze.fields.participantright.concat(
                  res.data.data[i].key
                );
              }
            }
          }
          this.panduanrowid = rowid; //将本行的id保存
        });
      }
    },
    //打开选择参与人窗口
    selectcanyu(rowid) {
      this.formModelcanyu.opened = true;
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
      // this.formModel.fields.participant = this.formModel.fields.participant; //情况原先的
    },
    //选择负责人
    handleSchoolChangefuze(newTargetKeys, direction, moveKeys) {
      this.formModelfuze.fields.participantright = newTargetKeys;
      this.formModel.fields.principalname = "";
      this.formModel.fields.principal = ""; //每次选择则负责人都重新赋值
      for (
        let i = 0;
        i < this.formModelfuze.fields.participantright.length;
        i++
      ) {
        for (
          let j = 0;
          j < this.formModelfuze.fields.participantleft.length;
          j++
        ) {
          if (
            this.formModelfuze.fields.participantright[i] ===
            this.formModelfuze.fields.participantleft[j].key
          ) {
            this.formModel.fields.principal +=
              this.formModelfuze.fields.participantleft[j].key + ","; //id

            this.formModel.fields.principalname +=
              this.formModelfuze.fields.participantleft[j].label + ","; //名称
          }
        }
      }
    },
    //选择参与人
    handleSchoolChange(newTargetKeys, direction, moveKeys) {
      this.formModelcanyu.fields.participantright = newTargetKeys;
      this.formModel.fields.participant = this.formModel.fields.participant =
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
            this.formModel.fields.participant +=
              this.formModelcanyu.fields.participantleft[j].label + ","; //名称
            this.formModel.fields.participantid +=
              this.formModelcanyu.fields.participantleft[j].key + ","; //id
          }
        }
      }
    },
    //负责人下拉框
    loadfuzeren() {
      getfuzeren().then((res) => {
        this.fuzerendata = res.data.data;
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
    //新增创建-日志
    handleShowCreateWindow() {
      this.formModel.fields.principalname = "";
      this.formModel.fields.priorityDescribe = "";
      this.formModelcanyu.fields.participantright = []; //重新打开窗口，清空选择的人员信息
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      console.log("检测数据");
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
      this.$refs["formRole"].resetFields();
    },

    //添加
    doCreateRole() {
      console.log("创建数据！");
      console.log(this.formModel.fields);
      createRole(this.formModel.fields).then((res) => {
        console.log("进入返回数据");
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
    doLoadRole(id) {
      loadRole({ id: id }).then((res) => {
        console.log("asjasx");
        console.log(res);
        this.formModel.fields = res.data.data;
      });
    },
    handleDelete(params) {
      this.doDelete(params.priorityUuid);
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
    this.loadRoleList(); //显示数据
    this.loadfuzeren(); //负责人下拉框
    this.loaselect(); //穿梭框
    this.loaselectfuze(); //负责人穿梭框
    this.keshijiazai(); //科室下拉框
  },
};
</script>
