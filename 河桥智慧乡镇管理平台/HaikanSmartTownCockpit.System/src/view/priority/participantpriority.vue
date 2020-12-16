<template>
  <div>
    <Card>
     	<dz-table :totalCount="stores.role.query.totalCount" :pageSize="stores.role.query.pageSize" @on-page-change="handlePageChanged"
			 @on-page-size-change="handlePageSizeChanged">
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
                      @on-search="handleSearchRole()">
                    </Input>
                  </FormItem>
                  <FormItem>
                    <Select v-model="stores.role.query.kwfzr" :clearable="true" style="width: 180px;" placeholder="请选择负责人" filterable>
                      <Option v-for="item in fuzerendata" 
                    :value="item.systemUserUUID">{{item.realName}}</Option>
                    </Select>
                  </FormItem>
                  <FormItem>
                    <DatePicker type="date" 
                    style="width: 180px;"
                    :value="stores.role.query.kwendtime" 
                    @on-change="stores.role.query.kwendtime=$event" 
                    format="yyyy-MM-dd"
                    placeholder="开始时间">
                    </DatePicker>
                    </FormItem>
                  <FormItem>
                    <DatePicker type="date" 
                    style="width: 180px;"
                    :value="stores.role.query.kwendtime2" 
                    @on-change="stores.role.query.kwendtime2=$event" 
                    format="yyyy-MM-dd"
                    placeholder="结束时间" >
                    </DatePicker>
                  </FormItem>
                  <FormItem>
                   <Button
                    v-can="'search'"
                    icon="md-search"
                    type="primary"
                    @click="handleSearchRole()"
                    title="查询">查询
                  </Button>                   
                  </FormItem>
                </Form>
              </Col>
              <Col span="8" class="dnc-toolbar-btns">
                <ButtonGroup class="mr3">
                  <Button icon="md-refresh" title="刷新" @click="handleRefresh"></Button>
                </ButtonGroup>
              </Col>
            </Row>
          </section>
        </div>

	      <Table slot="table" ref="tables" :border="false" size="small" :row-class-name="rowClassName" :highlight-row="true" :data="stores.role.data"
          :columns="stores.columns" @on-selection-change="handleSelectionChange" @on-refresh="handleRefresh" @on-page-change="handlePageChanged"
          @on-page-size-change="handlePageSizeChanged">
            <template slot-scope="{row,index}" slot="auditStatus">
              <span>{{getauditStatus(row.auditStatus)}}</span>
            </template>
            <template slot-scope="{ row, index }" slot="action">
              <Tooltip placement="top" content="详情" :delay="1000" :transfer="true">
                <Button v-can="'show'" type="success" size="small" shape="circle" icon="md-search" @click="handleshow(row)"></Button>
              </Tooltip>
            </template>
				</Table>
    	</dz-table>
    </Card>
    <!--添加编辑表单-->
    <!--<Drawer
      :title="formTitle"
      v-model="formModel.opened"
      width="400"
      :mask-closable="false"
      :mask="false"
      :styles="styles">
      <Form :model="formModel.fields" ref="formRole" :rules="formModel.rules" label-position="left">
          <FormItem label="重点工作标题" prop="priorityHeadline" label-position="left">
             <Input v-model="formModel.fields.priorityHeadline" placeholder="请输入重点工作志标题"/>
          </FormItem>
          <FormItem label="重点工作描述" label-position="top">
              <Input type="textarea" v-model="formModel.fields.priorityDescribe" :rows="4" placeholder="请输入重点工作描述"/>
          </FormItem>
          <FormItem label="负责人" prop="principal" label-position="left">
              <Input v-model="formModel.fields.principal" placeholder="请输入负责人"/>
          </FormItem>
          <FormItem label="参与人" prop="participant" label-position="left">
              <Input v-model="formModel.fields.participant" placeholder="请输入参与人"/>
          </FormItem>
          <FormItem label="备注" label-position="top">
              <Input type="textarea" v-model="formModel.fields.remark" :rows="4" placeholder="请输入备注"/>
          </FormItem>
          <FormItem label="排序字段" prop="sortord" label-position="left">
            <InputNumber :max="999999" :min="1" v-model="formModel.fields.sortord"></InputNumber>
          </FormItem>
      </Form> 
      <div class="demo-drawer-footer">
        <Button icon="md-checkmark-circle" type="primary" @click="handleSubmitRole">保 存</Button>
        <Button style="margin-left: 8px" icon="md-close" @click="formModel.opened = false">取 消</Button>
      </div>
    </Drawer>-->
    
    <!--详情-->
    <Drawer
	    title="详情"
	    v-model="formMode2.opened"
	    width="400"
	    :mask-closable="false"
	    :mask="false"
	    :styles="styles">
        <Form :model="formMode2.fields" ref="详情"  label-position="left">
          <FormItem label="标题">
              <Input v-model="formMode2.fields.priorityHeadline" :readonly="true"/>
          </FormItem>
          <FormItem label="负责人">
              <Input v-model="formMode2.fields.principalname" :readonly="true"/>
          </FormItem>
          <FormItem label="描述" label-position="top">
            <Input type="textarea" v-model="formMode2.fields.priorityDescribe" :rows="4" :readonly="true" />
          </FormItem>
          <FormItem label="参与人">
              <Input v-model="formMode2.fields.participant" :readonly="true"/>
          </FormItem>
          <FormItem label="备注">
              <Input v-model="formMode2.fields.remark" :readonly="true"/>
          </FormItem>
          <FormItem label="排序字段">
              <Input v-model="formMode2.fields.sortord" :readonly="true"/>
          </FormItem>
          <FormItem label="创建时间">
              <Input v-model="formMode2.fields.establishTime" :readonly="true"/>
          </FormItem>
          <FormItem label="结束时间" prop="endTime">
						<Input v-model="formMode2.fields.endTime" :readonly="true"/>
					</FormItem>
          <FormItem label="创建人">
              <Input v-model="formMode2.fields.establishName" :readonly="true"/>
          </FormItem>
        </Form>
	  </Drawer>
  </div>
</template>
<style>
    .ivu-table .demo-table-info-row td{
        background-color: #2db7f5;
        color: #fff;
    }
    .ivu-table .demo-table-error-row td{
        /* background-color: #ff6600; */
        color: red;
    }
    .ivu-table td.demo-table-info-column{
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
import DzTable from "_c/tables/dz-table.vue";
import config from '@/config';
import {
  participantListRole,
  createRole,
  loadRole,
  editRole,
  deleteRole,
  batchCommand,
  RegistPicture,
  getfuzeren//下拉框
} from "@/api/EmphasisWork/PriorityWork";

export default {
  name: "rbac_role_page",
  components: {
    DzTable
  },
  data() {
    return {
      commands: {
        delete: { name: "delete", title: "删除" },
        recover: { name: "recover", title: "恢复" },
        forbidden: { name: "forbidden", title: "禁用" },
        normal: { name: "normal", title: "启用" }
      },
      //编辑
      formModel: {
        opened: false,
        title: "创建重点工作",
        mode: "create",
        selection: [],
        fields: {
          id: "",
          priorityHeadline:"",
          principal:"",//负责人
          participant:"",//参与人姓名
          endTime:"",
          participantid:"",//参与人id
          principalname:""
        },
        rules: {
          priorityHeadline: [
            {
              type: "string",
              required: true,
              message: "请输入重点工作标题",
              min: 2
            }
          ],
          principal: [
            {
              type: "string",
              required: true,
              message: "请输入负责人",
              min: 2
            }
          ],
          Participant: [
            {
              type: "string",
              required: true,
              message: "请输入参与人",
              min: 2
            }
          ],
          endTime: [{
							type: "string",
							required: true,
							message: "请选择结束时间"
						}],
          sortord: [
            {
              type: "string",
              required: true,
              message: "请输入排序字段",
              min: 2
            }
          ],
        }
      },
      //详情
      formMode2:{
        opened: false,
        title: "重点工作详情",
        mode: "show",
        selection: [],
        fields: {
        id: "",
        },
      },
      fuzerendata:[],//负责人
      stores: {
        role: {
          query: {
            totalCount: 0,
            pageSize: 20,
            currentPage: 1,
            kw: "",
            kwfzr:"",
            kwendtime:"",
            kwendtime2:"",
            isDeleted: 0,
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
            statusSources: [
              { value: -1, text: "全部" },
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ],
            statusFormSources: [
              { value: 0, text: "禁用" },
              { value: 1, text: "正常" }
            ]
          },     
          data: []
        },
          //绑定数据
          columns: [
            {type:  "selection", width: 50, key: "handle" },
            {title: "名称", key: "priorityHeadline", ellipsis: true ,tooltip: true},
            {title: "负责人",ellipsis: true,tooltip: true,key: "princial"},
            {title: "完成进度", key: "unfinished"},
            {title: "创建时间", key: "establishTime"},
            { title: "结束时间", key: "endTime" },
            {
              title: "操作",
              align: "center",
              key: "handle",
              width: 150,
              className: "table-command-column",
              slot: "action"
            }
          ],
      },
      styles: {
        height: "calc(100% - 55px)",
        overflow: "auto",
        paddingBottom: "53px",
        position: "static"
      }
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
      return this.formModel.selection.map(x => x.priorityUuid);
    }
  },
  methods: {
    rowClassName (row, index) {
              if (row.accomplish === "2") {
                    return 'demo-table-error-row';
                }
                return '';
            },
	  //负责人下拉框
	  loadfuzeren(){
        getfuzeren().then(res => {
        this.fuzerendata = res.data.data
      });
	  },

    //查询数据列表
    loadRoleList() {
      participantListRole(this.stores.role.query).then(res => {
        console.log("asdhkj");
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
      this.handleSwitchFormModeToEdit();
      this.handleResetFormRole();
      this.doLoadRole(params.priorityUuid);
    },

    //详情重点工作页面
    handleshow(params) {
      this.handleSwitchFormModeToShow();
      // this.handleResetFormRole();
      // this.doLoadShow(params.priorityUuid);
      loadRole({ id: params.id}).then(res => {
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
    // doLoadShow(priorityUuid) {
    //   loadRole({ guid: priorityUuid}).then(res => {
    //     this.formMode2.fields = res.data.data;
    //   });
    // },

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
      this.handleSwitchFormModeToCreate();
      this.handleOpenFormWindow();
      this.handleResetFormRole();
		  this.formModelcanyu.fields.participantright=[]//重新打开窗口，清空选择的人员信息
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
      createRole(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseFormWindow();
      });
    },
    //编辑
    doEditRole() {
      editRole(this.formModel.fields).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
        } else {
          this.$Message.warning(res.data.message);
        }
        this.handleCloseFormWindow();
      });
    },

    //提交非空验证
    validateRoleForm() {
      let _valid = false;
      this.$refs["formRole"].validate(valid => {
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
    doLoadRole(priorityUuid) {
      loadRole({ guid: priorityUuid}).then(res => {
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
      deleteRole(ids).then(res => {
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
        }
      });
    },
    //批量操作
    doBatchCommand(command) {
      batchCommand({
        command: command,
        ids: this.selectedRowsId.join(",")
      }).then(res => {
        if (res.data.code === 200) {
          this.$Message.success(res.data.message);
          this.loadRoleList();
          this.formModel.selection=[];
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
    }
  },
  mounted() {
    this.loadRoleList();
    this.loadfuzeren();//负责人下拉框
  }
};
</script>
